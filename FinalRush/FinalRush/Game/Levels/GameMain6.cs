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
    class GameMain6
    {
        // FIELDS
        public static Editeur edit = new Editeur();
        public int[,] map = edit.Edition(6);
        public int size = 64;

        public Player LocalPlayer;
        public Boss boss;
        public List<Wall> Walls;
        public List<Bonus> bonus;
        public List<HealthBonus> healthbonus;
        public List<VitesseBonus> speedbonus;
        public List<Piques> piques;
        public List<Enemy> enemies;
        public List<Enemy2> enemies2;
        Random random = new Random();
        MainMenu menu;
        Texture2D background = Resources.Environnment6;
        Texture2D foreground = Resources.Foreground6;
        public int framecolumn;
        bool resetlave;
        int comptlave = 0;

        // CONSTRUCTOR

        public GameMain6()
        {
            menu = new MainMenu(Global.Handler, 0f);
            boss = new Boss(2600, 276, Resources.Elite);
            LocalPlayer = new Player();
            Walls = new List<Wall>();
            bonus = new List<Bonus>();
            healthbonus = new List<HealthBonus>();
            speedbonus = new List<VitesseBonus>();
            enemies = new List<Enemy>();
            piques = new List<Piques>();
            enemies2 = new List<Enemy2>();
            framecolumn = 1;

            Global.GameMain6 = this;

            piques.Add(new Piques(128, 425, Resources.Lave, 64, 64, Color.White));
            piques.Add(new Piques(192, 425, Resources.Lave, 64, 64, Color.White));
            piques.Add(new Piques(320, 425, Resources.Lave, 64, 64, Color.White));
            piques.Add(new Piques(384, 425, Resources.Lave, 64, 64, Color.White));
            piques.Add(new Piques(1024, 425, Resources.Lave, 64, 64, Color.White));
            piques.Add(new Piques(1152, 425, Resources.Lave, 64, 64, Color.White));

            for (int x = 0; x < map.GetLength(1); x++)
            {
                for (int y = 0; y < map.GetLength(0); y++)
                {
                    int number = map[y, x];
                    if (number == 1)
                        Walls.Add(new Wall(x * size, y * size + size / 2, Resources.Rock_top, size, size, Color.White));
                    if (number == 2)
                        Walls.Add(new Wall(x * size, y * size + size / 2, Resources.Rock, size, size, Color.White));
                    if (number == 3)
                        enemies.Add(new Enemy(x * size, y * size, Resources.Zombie));
                    if (number == 4)
                        enemies2.Add(new Enemy2(x * size, y * size, Resources.Elite));
                    if (number == 5)
                        bonus.Add(new Bonus(x * size, y * size, Resources.Coin, 20, 20, Color.White));
                    if (number == 6)
                        healthbonus.Add(new HealthBonus(x * size + 40, y * size + 74, Resources.Health, 20, 20, Color.White));
                    if (number == 7)
                        speedbonus.Add(new VitesseBonus(x * size + 32, y * size + 64, Resources.Speed, 20, 20, Color.White));
                    if (number == 8)
                        Walls.Add(new Wall(x * size, y * size + size / 2, Resources.Platform, 100, 16, Color.OrangeRed));
                    
                }
            }

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
                if (i != 2 & i != 3 & i != 5 & i != 6 & i != 16 & i != 18 & i != 21 & i != 22 & i != 23 & i != 24 & i != 25 & i != 20 & i != 71)
                    Walls.Add(new Wall(64 * i, 416, Resources.Rock_top, 64, 64, Color.White));
            #endregion

            #region Bonus

            #endregion
        }

        // UPDATE & DRAW

        public void Update(MouseState souris, KeyboardState clavier)
        {
            GameTime gametime = new GameTime();
            LocalPlayer.Update(souris, clavier, Walls, bonus);
            menu.Update(gametime);
            boss.Update(Walls);
            foreach (Enemy enemy in enemies)
                enemy.Update(Walls, random.Next(10, 1000));
            foreach (Enemy2 enemy2 in enemies2)
                enemy2.Update(Walls);
            if (resetlave)
            {
                resetlave = false;
                framecolumn = 1;
                comptlave = 0;
            }
            else
            {
                if (comptlave % 5 == 0) framecolumn++;
                if (framecolumn == 32) resetlave = true;
                comptlave++;
            }
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

            if (!boss.isDead)
                boss.Draw(spritebatch);

            foreach (Enemy2 enemy2 in enemies2)
                enemy2.Draw(spritebatch);

            foreach (Wall wall in Walls)
                wall.Draw(spritebatch);

            foreach (Bonus b in bonus)
                b.Draw(spritebatch);

            foreach (Piques p in piques)
            {
                p.Draw(spritebatch);
                spritebatch.Draw(Resources.Lave, p.Hitbox, new Rectangle((framecolumn - 1) * 64, 0, p.Hitbox.Width, p.Hitbox.Height), Color.White, 0f, new Vector2(0, 0), SpriteEffects.None, 0f);
            }

            foreach (HealthBonus hb in healthbonus)
                hb.Draw(spritebatch);

            foreach (VitesseBonus sb in speedbonus)
                sb.Draw(spritebatch);
        }
    }
}