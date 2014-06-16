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

        public MainMenu main;
        public Camera camera;
        GameMain Main;
        GameMain2 Main2;
        GameMain3 Main3;
        GameMain4 Main4;
        GameMain5 Main5;
        GameMain6 Main6;
        GameMainMulti MainMulti;
        SpriteFont scoring, timer;
        Texture2D bullet_texture, timer_texture;
        Color color;

        public List<Bullets> bullets;
        SoundEffect saut;
        Texture2D HealthBar;

        int screenwidth = 800;
        int screenheight = 480;
        SpriteFont piece_font;
        public int ammo_left = 6;
        public int recharge_left;
        SoundEffectInstance reloading_instance;
        bool reloading = false;
        #endregion

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            recharge_left = 30;
            bullets = new List<Bullets>();
            color = Color.Black;
            graphics.PreferredBackBufferWidth = screenwidth;
            graphics.PreferredBackBufferHeight = screenheight;
            Window.AllowUserResizing = true;
            IsMouseVisible = true;
            Global.Handler = this;
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
            bullet_texture = Content.Load<Texture2D>(@"SpriteFonts\Bullet_Texture");
            timer_texture = Content.Load<Texture2D>(@"SpriteFonts\Timer_Text");
            spriteBatch = new SpriteBatch(GraphicsDevice);
            Resources.LoadContent(Content);
            saut = Content.Load<SoundEffect>(@"Sons\Bruitages\saut_mario");
            Main = new GameMain();
            Main2 = new GameMain2();
            Main3 = new GameMain3();
            Main4 = new GameMain4();
            Main5 = new GameMain5();
            Main6 = new GameMain6();
            MainMulti = new GameMainMulti(Global.MainMenu.ip);
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

            if (main.comptlevel == 1 || main.comptlevel == 6)
                color = Color.White;
            else
                color = Color.Black;

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
                    recharge_left = recharge_left - (6 - ammo_left);
                    ammo_left = 6;
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
            else if (main.comptlevel == 7)
            {
                MainMulti.Update(Mouse.GetState(), Keyboard.GetState());
                camera.Update(gameTime, MainMulti.player);
            }
            else
                camera.Update(gameTime, Main.LocalPlayer);
            #endregion

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);

            spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, null, null, null, null, camera.transform);
            main.Draw(spriteBatch);
            spriteBatch.End();
            foreach (Bullets bullet in bullets)
                bullet.Draw(spriteBatch);

            #region HUD
            if (main.enjeu && main.gameState != MainMenu.GameState.InGameMulti)
            {
                if (Global.Player.health > 0 && Global.Player.health < 40)
                {
                    spriteBatch.Begin();
                    spriteBatch.Draw(HealthBar, new Rectangle(50, 20, 100, 20), Color.White);
                    spriteBatch.Draw(HealthBar, new Rectangle(50, 20, Global.Player.health, 20), Color.Red);
                    spriteBatch.DrawString(Resources.pourcent_life, Global.Player.health + " %", new Vector2(50, 22), color);
                    spriteBatch.End();
                }
                else if (Global.Player.health > 0 && Global.Player.health > 40)
                {
                    spriteBatch.Begin();
                    spriteBatch.Draw(HealthBar, new Rectangle(50, 20, 100, 20), Color.White);
                    spriteBatch.Draw(HealthBar, new Rectangle(50, 20, Global.Player.health, 20), Color.Red);
                    spriteBatch.DrawString(Resources.pourcent_life, Global.Player.health + " %", new Vector2(10 + Global.Player.health, 22), color);
                    spriteBatch.End();
                }
                if (main.comptlevel == 6)
                {
                    spriteBatch.Begin();
                    if (!Global.Boss.isDead)
                    {
                        spriteBatch.DrawString(piece_font, "Boss: ", new Vector2(Window.ClientBounds.Width / 2 + 185, 15), Color.White);
                        spriteBatch.Draw(HealthBar, new Rectangle(640, 20, 4 * 30, 20), Color.White);
                        spriteBatch.Draw(HealthBar, new Rectangle(640,20, 4 * Global.Boss.pv, 20), Color.Red);
                        spriteBatch.Draw(timer_texture, new Rectangle(Window.ClientBounds.Width / 2 - 150, 10, 35, 35), Color.White);
                    }
                    spriteBatch.End();
                }
                else
                {
                    spriteBatch.Begin();
                    spriteBatch.Draw(timer_texture, new Rectangle(Window.ClientBounds.Width / 2 - 150, 10, 35, 35), Color.Black);
                    spriteBatch.End();
                }
                spriteBatch.Begin();
                spriteBatch.DrawString(timer, ": " + main.Text, new Vector2(Window.ClientBounds.Width / 2 - 120, 10), color);
                spriteBatch.Draw(bullet_texture, new Rectangle(Window.ClientBounds.Width / 2 + 87, 10, 10, 35), Color.White);
                spriteBatch.DrawString(Resources.ammo_font, ": " + ammo_left + "/" + recharge_left, new Vector2(Window.ClientBounds.Width / 2 + 100, 10), color);
                spriteBatch.End();
            }
            if (main.gameState == MainMenu.GameState.Won)
            {
                spriteBatch.Begin();
                spriteBatch.Draw(timer_texture, new Rectangle(Window.ClientBounds.Width / 2 - 60, 200, 35, 35), Color.Black);
                spriteBatch.DrawString(timer, ": " + main.Text + " s", new Vector2(Window.ClientBounds.Width / 2 - 20, 200), Color.White);
                spriteBatch.DrawString(scoring, "Score : " + main.score + " points", new Vector2(Window.ClientBounds.Width / 2 - 100, 360), Color.White);
                spriteBatch.DrawString(piece_font, "x " + main.nb_pieces, new Vector2(Window.ClientBounds.Width / 2, 250), Color.White);
                spriteBatch.End();
            }

            #endregion

            base.Draw(gameTime);
        }
    }
}