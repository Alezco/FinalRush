using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;

namespace FinalRush
{
    class GameMain4
    {
        // FIELDS

        public Player LocalPlayer;
        public List<Wall> Walls;
        public List<Bonus> bonus;
        public List<MovingHorizontallyWall> movinghorizontallywalls;
        public List<Enemy> enemies;
        public List<Enemy2> enemies2;
        Random random = new Random();
        MainMenu menu;
        Texture2D background = Resources.Environnment4;


        // CONSTRUCTOR

        public GameMain4()
        {
            menu = new MainMenu(Global.Handler, 0f);
            LocalPlayer = new Player();
            Walls = new List<Wall>();
            bonus = new List<Bonus>();
            enemies = new List<Enemy>();
            enemies2 = new List<Enemy2>();
            movinghorizontallywalls = new List<MovingHorizontallyWall>();
            Global.GameMain4 = this;

            #region Ennemis
            //Ennemis

            //enemies.Add(new Enemy(1100, 350, Resources.Zombie));
            movinghorizontallywalls.Add(new MovingHorizontallyWall(435, 345, Resources.Platform, 50, 16, 425, 445,Color.IndianRed));

            #endregion

            #region Plateformes
            //Plateformes

            Walls.Add(new Wall(425, 245, Resources.Platform, 90, 16, Color.IndianRed));

            #endregion

            #region Terrain
                  
            //Sol

            for (int i = 0; i < 80; i++)
                //if (i != 2 )
                    Walls.Add(new Wall(64 * i, 416, Resources.Roche_top, 64, 64, Color.White));
            #endregion

            #region Bonus
            //Bonus

            //bonus.Add(new Bonus(700, 345, Resources.Coin, 20, 20, Color.White));

            #endregion
        }

        // UPDATE & DRAW

        public void Update(MouseState souris, KeyboardState clavier)
        {
            GameTime gametime = new GameTime();
            LocalPlayer.Update(souris, clavier, Walls, bonus);
            menu.Update(gametime);
            foreach (Enemy enemy in enemies)
                enemy.Update(Walls, random.Next(10, 1000));
            foreach (Enemy2 enemy2 in enemies2)
                enemy2.Update(Walls);
            foreach (MovingHorizontallyWall mhw in movinghorizontallywalls)
                mhw.Update(souris, clavier);
        }

        public void Draw(SpriteBatch spritebatch)
        {
            if (LocalPlayer.Hitbox.X <= 400)
                spritebatch.Draw(background, new Rectangle(0, 0, 800, 480), Color.White);
            else if (LocalPlayer.Hitbox.X >= 4200)
                spritebatch.Draw(background, new Rectangle(3800, 0, 800, 480), Color.White);
            else
                spritebatch.Draw(background, new Rectangle(LocalPlayer.Hitbox.X + LocalPlayer.Hitbox.Width / 2 - 400, 0, 800, 480), Color.White);
            LocalPlayer.Draw(spritebatch);

            foreach (Enemy enemy in enemies)
                enemy.Draw(spritebatch);

            foreach (Enemy2 enemy2 in enemies2)
                enemy2.Draw(spritebatch);

            foreach (Wall wall in Walls)
                wall.Draw(spritebatch);

            foreach (Bonus b in bonus)
                b.Draw(spritebatch);

            foreach (MovingHorizontallyWall mhw in movinghorizontallywalls)
                mhw.Draw(spritebatch);
        }
    }
}