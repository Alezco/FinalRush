using System;
using System.Collections.Generic;
using System.Linq;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace FinalRush
{
    class Game1 : Microsoft.Xna.Framework.Game
    {
        #region Variables
        public GraphicsDeviceManager graphics;
        public SpriteBatch spriteBatch;

        MainMenu main;
        public Camera camera;
        GameMain Main;
        GameMain2 Main2;
        GameMain3 Main3;
        GameMain4 Main4;
        GameMain5 Main5;
        GameMain6 Main6;
        SpriteFont scoring, timer;

        public List<Bullets> bullets;
        SoundEffect saut;
        Texture2D HealthBar;
        public int health;

        int screenwidth = 800;
        int screenheight = 480;
        SpriteFont piece_font;
        public int ammo_left = 6;
        public int recharge_left = 5;
        SoundEffectInstance reloading_instance;
        bool reloading = false;
        #endregion

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            health = 100;
            bullets = new List<Bullets>();
            graphics.PreferredBackBufferWidth = screenwidth;
            graphics.PreferredBackBufferHeight = screenheight;
            IsMouseVisible = true;
            Global.Handler = this;
            Global.Multi = new Multi();
        }

        protected override void Initialize()
        {
            base.Initialize();
            camera = new Camera(GraphicsDevice.Viewport, main);
            main.Initialize();
        }

        protected override void LoadContent()
        {
            piece_font = Content.Load<SpriteFont>(@"SpriteFonts\piece_font");
            timer = Content.Load<SpriteFont>(@"SpriteFonts\TimerFont");
            scoring = Content.Load<SpriteFont>(@"SpriteFonts\ScoringFont");
            HealthBar = Content.Load<Texture2D>(@"Sprites\Hero\HealthBar");
            spriteBatch = new SpriteBatch(GraphicsDevice);
            Resources.LoadContent(Content);
            saut = Content.Load<SoundEffect>(@"Sons\Bruitages\saut");
            Main = new GameMain();
            Main2 = new GameMain2();
            Main3 = new GameMain3();
            Main4 = new GameMain4();
            Main5 = new GameMain5();
            Main6 = new GameMain6();
            main = new MainMenu(this, 0f);
            reloading_instance = Resources.reload_sound.CreateInstance();

            //Menu
            main.LoadContent(Content);
            //main.Font = Content.Load<SpriteFont>("TimerFont");
        }

        protected override void UnloadContent()
        {
            Content.Unload();
        }

        protected override void Update(GameTime gameTime)
        {
            MouseState souris = Mouse.GetState();

            #region Timer & SpriteFont
            if (main.EnJeu(main.enjeu))
            {
                KeyboardState ks = Keyboard.GetState();
                main.Started = true;
                main.Paused = false;
                if (ks.IsKeyDown(Keys.P) || ks.IsKeyDown(Keys.Escape))
                    main.Paused = true;

                if (Global.Player.shot)
                    ammo_left--;

                if (Keyboard.GetState().IsKeyDown(Keys.R) && recharge_left > 0 && ammo_left < 6 && !reloading)
                    reloading = true;
                if (reloading && Keyboard.GetState().IsKeyUp(Keys.R))
                {
                    reloading = false;
                    ammo_left = 6;
                    recharge_left--;
                    reloading_instance.Play();
                }
            }
            #endregion

            #region Choix camera
            main.Update(gameTime);
            if (main.comptlevel == 1)
            {
                Main.Update(Mouse.GetState(), Keyboard.GetState());
                camera.Update(gameTime, main.player);
            }
            else if (main.comptlevel == 2)
            {
                Main2.Update(Mouse.GetState(), Keyboard.GetState());
                camera.Update(gameTime, main.player2);
            }
            else if (main.comptlevel == 3)
            {
                Main3.Update(Mouse.GetState(), Keyboard.GetState());
                camera.Update(gameTime, main.player3);
            }
            else if (main.comptlevel == 4)
            {
                Main4.Update(Mouse.GetState(), Keyboard.GetState());
                camera.Update(gameTime, main.player4);
            }
            else if (main.comptlevel == 5)
            {
                Main5.Update(Mouse.GetState(), Keyboard.GetState());
                camera.Update(gameTime, main.player5);
            }
            else if (main.comptlevel == 6)
            {
                Main6.Update(Mouse.GetState(), Keyboard.GetState());
                camera.Update(gameTime, main.player6);
            }
            else
                camera.Update(gameTime, Main.LocalPlayer);
            #endregion

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, null, null, null, null, camera.transform);
            main.Draw(spriteBatch);
            spriteBatch.End();
            foreach (Bullets bullet in bullets)
            {
                bullet.Draw(spriteBatch);
            }

            #region HUD
            if (main.enjeu)
            {
                if (health > 0)
                {
                    spriteBatch.Begin();
                    spriteBatch.Draw(HealthBar, new Rectangle(50, 20, 100, 20), Color.White);
                    spriteBatch.Draw(HealthBar, new Rectangle(50, 20, Global.Player.health, 20), Color.Red);
                    spriteBatch.End();
                }
                spriteBatch.Begin();
                if (main.comptlevel == 1)
                {
                    spriteBatch.DrawString(timer, "Temps : " + main.Text, new Vector2(Window.ClientBounds.Width / 2 - 120, 0), Color.White);
                    //spriteBatch.DrawString(scoring, "Score : " + main.score + " pts", new Vector2(Window.ClientBounds.Width / 2 - 200, 0), Color.White);
                    spriteBatch.DrawString(Resources.ammo_font, "Munitions restantes: " + ammo_left + "/" + recharge_left, new Vector2(Window.ClientBounds.Width / 2 + 100, 0), Color.White);
                }
                else
                {
                    spriteBatch.DrawString(timer, "Temps : " + main.Text, new Vector2(Window.ClientBounds.Width / 2 - 120, 0), Color.Black);
                    //spriteBatch.DrawString(scoring, "Score : " + main.score + " pts", new Vector2(Window.ClientBounds.Width / 2 - 200, 0), Color.White);
                    spriteBatch.DrawString(Resources.ammo_font, "Munitions restantes: " + ammo_left + "/" + recharge_left, new Vector2(Window.ClientBounds.Width / 2 + 100, 0), Color.Black);
                }
                spriteBatch.End();
            }
            if (main.gameState == MainMenu.GameState.Won)
            {
                spriteBatch.Begin();
                spriteBatch.DrawString(timer, "Bravo, tu as termine le niveau en " + main.Text + " secondes", new Vector2(Window.ClientBounds.Width / 2 - 300, 200), Color.White);
                spriteBatch.DrawString(scoring, "Ton score est de : " + main.score + " points", new Vector2(Window.ClientBounds.Width / 2 - 150, 300), Color.White);
                spriteBatch.DrawString(piece_font, "x " + main.nb_pieces, new Vector2(Window.ClientBounds.Width / 2 - 70, 250), Color.White);
                spriteBatch.End();
            }

            #endregion

            base.Draw(gameTime);
        }
    }
}