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
    class GameMain6
    {
        // FIELDS

        public Player LocalPlayer;
        public List<Wall> Walls;
        public List<Bonus> bonus;
        public List<Enemy> enemies;
        public List<Enemy2> enemies2;
        Random random = new Random();
        MainMenu menu;
        Texture2D background = Resources.Environnment6;
        Texture2D foreground = Resources.Foreground6;


        // CONSTRUCTOR

        public GameMain6()
        {
            menu = new MainMenu(Global.Handler, 0f);
            LocalPlayer = new Player();
            Walls = new List<Wall>();
            bonus = new List<Bonus>();
            enemies = new List<Enemy>();
            enemies2 = new List<Enemy2>();
            Global.GameMain6 = this;

            #region Ennemis
            //Ennemis

            //enemies.Add(new Enemy(1100, 350, Resources.Zombie));

            #endregion

            #region Plateformes
            //Plateformes

            //Walls.Add(new Wall(425, 245, Resources.Platform, 50, 16, Color.IndianRed));

            #endregion

            #region Terrain
            //Colonnes et sol

            //Walls.Add(new Wall(896, 416, Resources.Ground, 64, 64, Color.White));

            //Sol

            for (int i = 0; i < 80; i++)
                if (i != 2 & i != 3 & i != 5 & i != 6 & i != 16 & i != 20 & i != 21 & i != 30)
                Walls.Add(new Wall(64 * i, 416, Resources.Rock_top, 64, 64, Color.White));
            #endregion

            #region Bonus
            ////Bonus

            //bonus.Add(new Bonus(700, 345, Resources.Coin, 20, 20, Color.White));
            //bonus.Add(new Bonus(1200, 100, Resources.Coin, 20, 20, Color.White));
            //bonus.Add(new Bonus(1940, 76, Resources.Coin, 20, 20, Color.White));
            //bonus.Add(new Bonus(1984, 320, Resources.Coin, 20, 20, Color.White));
            //bonus.Add(new Bonus(1984, 396, Resources.Coin, 20, 20, Color.White));
            //bonus.Add(new Bonus(2520, 320, Resources.Coin, 20, 20, Color.White));
            //bonus.Add(new Bonus(2650, 320, Resources.Coin, 20, 20, Color.White));
            //bonus.Add(new Bonus(2816, 332, Resources.Coin, 20, 20, Color.White));
            //bonus.Add(new Bonus(3200, 140, Resources.Coin, 20, 20, Color.White));
            //bonus.Add(new Bonus(4020, 320, Resources.Coin, 20, 20, Color.White));
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
        }

        public void Draw(SpriteBatch spritebatch)
        {
            if (LocalPlayer.Hitbox.X <= 400)
                spritebatch.Draw(background, new Rectangle(0, 0, 800, 480), Color.White);
            else if (LocalPlayer.Hitbox.X >= 4200)
                spritebatch.Draw(background, new Rectangle(3800, 0, 800, 480), Color.White);
            else
                spritebatch.Draw(background, new Rectangle(LocalPlayer.Hitbox.X + LocalPlayer.Hitbox.Width / 2 - 400, 0, 800, 480), Color.White);
            for (int i = 0; i <= 2; i++)
                spritebatch.Draw(foreground, new Rectangle(1600 * i, 0, 1600, 480), Color.White); 
            LocalPlayer.Draw(spritebatch);

            foreach (Enemy enemy in enemies)
                enemy.Draw(spritebatch);

            foreach (Enemy2 enemy2 in enemies2)
                enemy2.Draw(spritebatch);

            foreach (Wall wall in Walls)
                wall.Draw(spritebatch);

            foreach (Bonus b in bonus)
                b.Draw(spritebatch);
        }
    }
}