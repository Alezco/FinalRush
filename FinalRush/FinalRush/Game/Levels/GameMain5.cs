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
    class GameMain5
    {
        // FIELDS

        // Trois Varibles
        public static Editeur edit = new Editeur();
        public int[,] map = edit.Edition(5);
        public int size = 64;

        public Player LocalPlayer;
        public List<Wall> Walls;
        public List<Bonus> bonus;
        public List<HealthBonus> healthbonus;
        public List<VitesseBonus> speedbonus;
        public List<Enemy> enemies;
        public List<Enemy2> enemies2;
        Random random = new Random();
        MainMenu menu;
        Texture2D background = Resources.Environnment5;
        Texture2D foreground = Resources.Foreground5;


        // CONSTRUCTOR

        public GameMain5()
        {
            menu = new MainMenu(Global.Handler, 0f);
            LocalPlayer = new Player();
            Walls = new List<Wall>();
            bonus = new List<Bonus>();
            healthbonus = new List<HealthBonus>();
            speedbonus = new List<VitesseBonus>();
            enemies = new List<Enemy>();
            enemies2 = new List<Enemy2>();
            Global.GameMain5 = this;

            // Double Boucle qui fait marcher l'éditeur

            for (int x = 0; x < map.GetLength(1); x++)
            {
                for (int y = 0; y < map.GetLength(0); y++)
                {
                    int number = map[y, x];
                    if (number == 1)
                        Walls.Add(new Wall(x * size, y * size + size / 2, Resources.Sand_top, size, size, Color.White));
                    if (number == 2)
                        Walls.Add(new Wall(x * size, y * size + size / 2, Resources.Sand, size, size, Color.White));
                    if (number == 3)
                        enemies.Add(new Enemy(x * size, y * size, Resources.Zombie));
                    if (number == 4)
                        enemies2.Add(new Enemy2(x * size, y * size, Resources.Elite));
                    if (number == 5)
                        bonus.Add(new Bonus(x * size, y * size, Resources.Coin, 20, 20, Color.White));
                    if (number == 6)
                        healthbonus.Add(new HealthBonus(x * size, y * size, Resources.Health, 20, 20, Color.White));
                    if (number == 7)
                        speedbonus.Add(new VitesseBonus(x * size, y * size, Resources.Speed, 20, 20, Color.White));
                }
            }

            Walls.Add(new Wall(3600, 300, Resources.Platform, 100, 16, Color.LightYellow));
            Walls.Add(new Wall(3750, 250, Resources.Platform, 100, 16, Color.LightYellow));
            Walls.Add(new Wall(3900, 190, Resources.Platform, 50, 16, Color.LightYellow));
            Walls.Add(new Wall(4000, 150, Resources.Platform, 30, 10, Color.LightYellow));
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

            foreach (HealthBonus hb in healthbonus)
                hb.Draw(spritebatch);

            foreach (VitesseBonus sb in speedbonus)
                sb.Draw(spritebatch);
        }
    }
}