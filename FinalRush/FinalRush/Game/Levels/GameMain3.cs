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
    class GameMain3
    {
        // FIELDS

        public Player LocalPlayer;
        public List<Wall> Walls;
        public List<Bonus> bonus;
        public List<HealthBonus> healthbonus;
        public List<VitesseBonus> speedbonus;
        public List<Enemy> enemies;
        public List<Enemy2> enemies2;
        public List<Piques> piques;
        Random random = new Random();
        MainMenu menu;
        Texture2D background = Resources.Environnment3;
        Texture2D foreground = Resources.Foreground3;

        // CONSTRUCTOR

        public GameMain3()
        {
            menu = new MainMenu(Global.Handler, 0f);
            LocalPlayer = new Player();
            Walls = new List<Wall>();
            bonus = new List<Bonus>();
            healthbonus = new List<HealthBonus>();
            speedbonus = new List<VitesseBonus>();
            enemies = new List<Enemy>();
            enemies2 = new List<Enemy2>();
            piques = new List<Piques>();
            Global.GameMain3 = this;

            #region Ennemis
            enemies.Add(new Enemy(700, 350, Resources.Zombie));
            enemies.Add(new Enemy(3780, 150, Resources.Zombie));
            enemies.Add(new Enemy(3850, 150, Resources.Zombie));
            enemies.Add(new Enemy(3920, 150, Resources.Zombie));
            #endregion

            #region Piques
            piques.Add(new Piques(2112, 228, Resources.Piques, 64, 60, Color.White));
            piques.Add(new Piques(2240, 228, Resources.Piques, 64, 60, Color.White));
            piques.Add(new Piques(2368, 228, Resources.Piques, 64, 60, Color.White));
            piques.Add(new Piques(2496, 228, Resources.Piques, 64, 60, Color.White));
            piques.Add(new Piques(2624, 228, Resources.Piques, 64, 60, Color.White));
            piques.Add(new Piques(2752, 228, Resources.Piques, 64, 60, Color.White));
            #endregion

            #region Plateformes
            Walls.Add(new Wall(250, 400, Resources.Platform, 40, 12, Color.DodgerBlue));
            Walls.Add(new Wall(375, 350, Resources.Platform, 40, 12, Color.DodgerBlue));
            Walls.Add(new Wall(500, 400, Resources.Platform, 40, 12, Color.DodgerBlue));
            Walls.Add(new Wall(1164, 350, Resources.Platform, 40, 12, Color.DodgerBlue));
            Walls.Add(new Wall(1277, 416, Resources.Platform, 3, 12, Color.DodgerBlue)); //mini
            Walls.Add(new Wall(1802, 340, Resources.Platform, 54, 12, Color.DodgerBlue));
            Walls.Add(new Wall(1664, 308, Resources.Platform, 54, 12, Color.DodgerBlue));
            Walls.Add(new Wall(1610, 105, Resources.Platform, 54, 12, Color.DodgerBlue));
            Walls.Add(new Wall(2242, 468, Resources.Platform, 60, 12, Color.DodgerBlue));
            Walls.Add(new Wall(2626, 468, Resources.Platform, 60, 12, Color.DodgerBlue));
            Walls.Add(new Wall(3580, 370, Resources.Platform, 96, 12, Color.DodgerBlue));
            Walls.Add(new Wall(3490, 290, Resources.Platform, 96, 12, Color.DodgerBlue));
            Walls.Add(new Wall(3400, 210, Resources.Platform, 96, 12, Color.DodgerBlue));
            Walls.Add(new Wall(3640, 458, Resources.Platform, 40, 12, Color.DodgerBlue));
            Walls.Add(new Wall(3775, 410, Resources.Platform, 96, 12, Color.DodgerBlue));
            Walls.Add(new Wall(3550, 140, Resources.Platform, 96, 12, Color.DodgerBlue));
            Walls.Add(new Wall(3700, 80, Resources.Platform, 300, 12, Color.DodgerBlue));
            Walls.Add(new Wall(3700, 190, Resources.Platform, 300, 12, Color.DodgerBlue));
            Walls.Add(new Wall(3880, 320, Resources.Platform, 80, 12, Color.DodgerBlue));
            Walls.Add(new Wall(3880, 320, Resources.Platform, 80, 12, Color.DodgerBlue));
            Walls.Add(new Wall(4140, 416, Resources.Platform, 20, 12, Color.DodgerBlue));
            #endregion

            #region Terrain
            Walls.Add(new Wall(640, 415, Resources.Ice_top, 1, 1, Color.White));
            for (int i = 0; i <= 72; i++) //pour une taille de 4600
            {
                if (i != 3 & i != 4 & i != 5 & i != 6 & i != 7 & i != 8 & i != 9 & i != 12 & i != 13 & i != 14 & i != 15
                    & i != 16 & i != 17 & i != 18 & i != 19 & i != 22 & i != 23 & i != 25 & i != 34 & i != 35 & i != 36 & i != 40 & i != 41 & i != 42
                    & i != 55 & i != 56 & i != 57 & i != 58 & i != 59 & i != 60 & i != 61 & i != 62 & i != 63 & i != 64 & i != 69 & i != 70 & i != 71)
                    Walls.Add(new Wall(64 * i, 416, Resources.Ice_top, 64, 64, Color.White)); // Le sol
            }
            for (int i = 30; i <= 46; i++)
            {
                Walls.Add(new Wall(64 * i, 288, Resources.Ice, 64, 64, Color.White));
                if (i != 33 & i != 35 & i != 37 & i != 39 & i != 41 & i != 43 & i != 46)
                {
                    Walls.Add(new Wall(64 * i, 224, Resources.Ice, 64, 64, Color.White));
                    Walls.Add(new Wall(64 * i, 160, Resources.Ice_top, 64, 64, Color.White));
                }
            }
            //Passage 1
            for (int i = 12; i <= 24; i++)
            {
                if (i != 15 & i != 17 & i != 18 & i != 19)
                {
                    if (i != 20 & i != 21)
                    {
                        Walls.Add(new Wall(64 * i, 352, Resources.Ice_top, 64, 64, Color.White));
                        Walls.Add(new Wall(64 * i, 416, Resources.Ice, 64, 64, Color.White));
                    }

                    if (i != 12 & i != 24)
                    {
                        Walls.Add(new Wall(64 * i, 288, Resources.Ice_top, 64, 64, Color.White));
                        if (i != 20 & i != 21)
                            Walls.Add(new Wall(64 * i, 352, Resources.Ice, 64, 64, Color.White));
                    }
                }
            }
            //Passage 2
            Walls.Add(new Wall(1472, 0, Resources.Ice, 64, 64, Color.White));
            Walls.Add(new Wall(1472, 64, Resources.Ice, 64, 64, Color.White));
            Walls.Add(new Wall(1472, 128, Resources.Ice, 64, 64, Color.White));
            Walls.Add(new Wall(1536, 192, Resources.Ice, 64, 64, Color.White));
            Walls.Add(new Wall(1600, 256, Resources.Ice, 64, 64, Color.White));
            Walls.Add(new Wall(1664, 96, Resources.Ice_top, 64, 64, Color.White));
            Walls.Add(new Wall(1728, 96, Resources.Ice_top, 64, 64, Color.White));
            Walls.Add(new Wall(1792, 96, Resources.Ice_top, 64, 64, Color.White));
            Walls.Add(new Wall(1792, 160, Resources.Ice, 64, 64, Color.White));
            Walls.Add(new Wall(1856, 96, Resources.Ice_top, 64, 64, Color.White));
            Walls.Add(new Wall(1856, 288, Resources.Ice, 64, 64, Color.White));
            Walls.Add(new Wall(1856, 224, Resources.Ice, 64, 64, Color.White));
            Walls.Add(new Wall(1856, 160, Resources.Ice, 64, 64, Color.White));
            Walls.Add(new Wall(2944, 224, Resources.Ice_top, 64, 64, Color.White));
            Walls.Add(new Wall(3008, 288, Resources.Ice_top, 64, 64, Color.White));
            Walls.Add(new Wall(4414, 416, Resources.Ice, 64, 64, Color.White));
            Walls.Add(new Wall(4478, 416, Resources.Ice, 64, 64, Color.White));
            Walls.Add(new Wall(4542, 416, Resources.Ice, 64, 64, Color.White));
            Walls.Add(new Wall(4414, 352, Resources.Ice_top, 64, 64, Color.White));
            Walls.Add(new Wall(4478, 352, Resources.Ice, 64, 64, Color.White));
            Walls.Add(new Wall(4542, 352, Resources.Ice, 64, 64, Color.White));
            Walls.Add(new Wall(4478, 288, Resources.Ice_top, 64, 64, Color.White));
            Walls.Add(new Wall(4542, 288, Resources.Ice, 64, 64, Color.White));
            Walls.Add(new Wall(4542, 224, Resources.Ice_top, 64, 64, Color.White));
            #endregion

            #region Bonus
            bonus.Add(new Bonus(260, 380, Resources.Coin, 20, 20, Color.White));
            bonus.Add(new Bonus(1335, 390, Resources.Coin, 20, 20, Color.White));
            bonus.Add(new Bonus(2262, 448, Resources.Coin, 20, 20, Color.White));
            bonus.Add(new Bonus(2262, 448, Resources.Coin, 20, 20, Color.White));
            bonus.Add(new Bonus(2646, 448, Resources.Coin, 20, 20, Color.White));
            bonus.Add(new Bonus(2870, 140, Resources.Coin, 20, 20, Color.White));
            bonus.Add(new Bonus(3650, 438, Resources.Coin, 20, 20, Color.White));
            bonus.Add(new Bonus(3840, 170, Resources.Coin, 20, 20, Color.White));
            bonus.Add(new Bonus(3910, 300, Resources.Coin, 20, 20, Color.White));

            speedbonus.Add(new VitesseBonus(2224, 396, Resources.Speed, 20, 20, Color.White));
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

            foreach (Wall wall in Walls)
                wall.Draw(spritebatch);

            foreach (Bonus b in bonus)
                b.Draw(spritebatch);

            foreach (Enemy enemy in enemies)
                enemy.Draw(spritebatch);

            foreach (Enemy2 enemy2 in enemies2)
                enemy2.Draw(spritebatch);

            foreach (Piques p in piques)
                p.Draw(spritebatch);

            foreach (HealthBonus hb in healthbonus)
                hb.Draw(spritebatch);

            foreach (VitesseBonus sb in speedbonus)
                sb.Draw(spritebatch);
        }
    }
}
