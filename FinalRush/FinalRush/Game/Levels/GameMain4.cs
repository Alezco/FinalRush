using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using FinalRush.Game.Levels;

namespace FinalRush
{
    class GameMain4
    {
        // FIELDS

        public Player LocalPlayer;
        public List<Wall> Walls;
        public List<Bonus> bonus;
        public List<HealthBonus> healthbonus;
        public List<VitesseBonus> speedbonus;
        public List<MovingHorizontallyWall> movinghorizontallywalls;
        public List<Enemy> enemies;
        public List<Enemy2> enemies2;
        Random random = new Random();
        MainMenu menu;
        Texture2D background = Resources.Environnment4;
        Texture2D foreground = Resources.Foreground4;

        // Editeur
        static Editeur edit = new Editeur();
        int[,] map = edit.Edition(4);
        int size = 64;

        // CONSTRUCTOR

        public GameMain4()
        {
            menu = new MainMenu(Global.Handler, 0f);
            LocalPlayer = new Player();
            Walls = new List<Wall>();
            bonus = new List<Bonus>();
            healthbonus = new List<HealthBonus>();
            speedbonus = new List<VitesseBonus>();
            enemies = new List<Enemy>();
            enemies2 = new List<Enemy2>();
            movinghorizontallywalls = new List<MovingHorizontallyWall>();
            Global.GameMain4 = this;

            for (int x = 0; x < map.GetLength(1); x++)
            {
                for (int y = 0; y < map.GetLength(0); y++)
                {
                    int number = map[y, x];
                    if (number == 1)
                        Walls.Add(new Wall(x * size, y * size + size / 2, Resources.Roche_top, size, size, Color.White));
                    if (number == 2)
                        Walls.Add(new Wall(x * size, y * size + size / 2, Resources.Roche, size, size, Color.White));
                    if (number == 3)
                        enemies.Add(new Enemy(x * size, y * size, Resources.Zombie));
                    if (number == 4)
                        enemies2.Add(new Enemy2(x * size, y * size, Resources.Elite));

                }

            }

            #region Plateformes
            //Plateformes

            Walls.Add(new Wall(425, 275, Resources.Platform, 90, 16, Color.FloralWhite));
            Walls.Add(new Wall(560, 220, Resources.Platform, 90, 16, Color.FloralWhite));
            Walls.Add(new Wall(695, 170, Resources.Platform, 256, 16, Color.FloralWhite));
            Walls.Add(new Wall(1030, 220, Resources.Platform, 90, 16, Color.FloralWhite));
            Walls.Add(new Wall(1344, 470, Resources.Platform, 384, 16, Color.FloralWhite));

            #endregion

            #region Terrain

            #endregion

            #region Bonus

            bonus.Add(new Bonus(812, 270, Resources.Coin, 20, 20, Color.White));
            bonus.Add(new Bonus(1045, 200, Resources.Coin, 20, 20, Color.White));
            bonus.Add(new Bonus(1526, 450, Resources.Coin, 20, 20, Color.White));
            bonus.Add(new Bonus(2200, 396, Resources.Coin, 20, 20, Color.White));
            bonus.Add(new Bonus(2646, 332, Resources.Coin, 20, 20, Color.White));
            bonus.Add(new Bonus(4200, 150, Resources.Coin, 20, 20, Color.White));
            speedbonus.Add(new VitesseBonus(512, 332, Resources.Speed, 20, 20, Color.White));
            speedbonus.Add(new VitesseBonus(3050, 332, Resources.Speed, 20, 20, Color.White));
            healthbonus.Add(new HealthBonus(1085, 200, Resources.Health, 20, 20, Color.White));
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

            for (int i = 0; i < enemies2.Count(); i++)
            {
                if (enemies2[i].isDead)
                {
                    enemies2.RemoveAt(i);
                    i--;
                }
            }

            foreach (Enemy2 enemy2 in enemies2)
                if (!enemy2.isDead)
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

            foreach (MovingHorizontallyWall mhw in movinghorizontallywalls)
                mhw.Draw(spritebatch);

            foreach (HealthBonus hb in healthbonus)
                hb.Draw(spritebatch);

            foreach (VitesseBonus sb in speedbonus)
                sb.Draw(spritebatch);
        }
    }
}