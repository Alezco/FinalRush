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
    class GameMain5
    {
        // FIELDS

        public Player LocalPlayer;
        public List<Wall> Walls;
        public List<Bonus> bonus;
        public List<Enemy> enemies;
        public List<Enemy2> enemies2;
        Random random = new Random();
        MainMenu menu;
        Texture2D background = Resources.Environnment5;


        // CONSTRUCTOR

        public GameMain5()
        {
            menu = new MainMenu(Global.Handler, 0f);
            LocalPlayer = new Player();
            Walls = new List<Wall>();
            bonus = new List<Bonus>();
            enemies = new List<Enemy>();
            enemies2 = new List<Enemy2>();
            Global.GameMain5 = this;

            #region Ennemis
            ////Ennemis

            //enemies.Add(new Enemy(1100, 350, Resources.Zombie));
            //enemies.Add(new Enemy(1150, 350, Resources.Zombie));
            //enemies.Add(new Enemy(1180, 350, Resources.Zombie));
            //enemies.Add(new Enemy(2550, 250, Resources.Zombie));
            //enemies.Add(new Enemy(2650, 250, Resources.Zombie));

            //enemies2.Add(new Enemy2(2400, 376, Resources.Zombie));

            //#endregion

            //#region Plateformes
            ////Plateformes

            //Walls.Add(new Wall(425, 245, Resources.Platform, 50, 16, Color.IndianRed));
            //Walls.Add(new Wall(130, 360, Resources.Platform, 92, 16, Color.IndianRed));
            //Walls.Add(new Wall(280, 300, Resources.Platform, 92, 16, Color.IndianRed));
            //Walls.Add(new Wall(680, 365, Resources.Platform, 92, 16, Color.IndianRed));
            //Walls.Add(new Wall(520, 300, Resources.Platform, 92, 16, Color.IndianRed));
            //Walls.Add(new Wall(1160, 200, Resources.Platform, 92, 16, Color.IndianRed));
            //Walls.Add(new Wall(1266, 415, Resources.Platform, 1, 1, Color.IndianRed));
            //Walls.Add(new Wall(1664, 350, Resources.Platform, 92, 16, Color.IndianRed));
            //Walls.Add(new Wall(1828, 280, Resources.Platform, 92, 16, Color.IndianRed));
            //Walls.Add(new Wall(1828, 140, Resources.Platform, 92, 16, Color.IndianRed));
            //Walls.Add(new Wall(1664, 210, Resources.Platform, 92, 16, Color.IndianRed));
            //Walls.Add(new Wall(1984, 140, Resources.Platform, 92, 16, Color.IndianRed));
            //Walls.Add(new Wall(2100, 240, Resources.Platform, 92, 16, Color.IndianRed));
            //Walls.Add(new Wall(2200, 340, Resources.Platform, 92, 16, Color.IndianRed));
            //Walls.Add(new Wall(1984, 340, Resources.Platform, 50, 16, Color.IndianRed));
            //Walls.Add(new Wall(2500, 340, Resources.Platform, 200, 16, Color.IndianRed));
            //Walls.Add(new Wall(2500, 339, Resources.Platform, 1, 1, Color.IndianRed));
            //Walls.Add(new Wall(2700, 339, Resources.Platform, 1, 1, Color.IndianRed));
            //Walls.Add(new Wall(3520, 350, Resources.Platform, 92, 16, Color.IndianRed));
            //Walls.Add(new Wall(3620, 300, Resources.Platform, 40, 16, Color.IndianRed));
            //Walls.Add(new Wall(3680, 250, Resources.Platform, 20, 16, Color.IndianRed));
            //Walls.Add(new Wall(3720, 200, Resources.Platform, 20, 16, Color.IndianRed));
            //Walls.Add(new Wall(3860, 400, Resources.Platform, 92, 16, Color.IndianRed));
            //Walls.Add(new Wall(4000, 340, Resources.Platform, 92, 16, Color.IndianRed));

            #endregion

            #region Terrain
            //Colonnes et sol

            //Walls.Add(new Wall(896, 416, Resources.Ground, 64, 64, Color.White));


            //Sol

            for (int i = 0; i < 80; i++)
                //if (i != 2 )
                Walls.Add(new Wall(64 * i, 416, Resources.Sand_top, 64, 64, Color.White));
            #endregion

            #region Bonus
            ////Bonus

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
        }
    }
}