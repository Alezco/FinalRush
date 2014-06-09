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
                        Walls.Add(new Wall(x * size, y * size, Resources.Ground, size, size, Color.White));
                    if (number == 2)
                        Walls.Add(new Wall(x * size, y * size, Resources.Herbe, size, size, Color.White));
                    if (number == 3)
                        Walls.Add(new Wall(x * size, y * size, Resources.Herbe_neige, size, size, Color.White));
                    if (number == 4)
                        Walls.Add(new Wall(x * size, y * size, Resources.Ice, size, size, Color.White));
                    if (number == 5)
                        Walls.Add(new Wall(x * size, y * size, Resources.Ice_top, size, size, Color.White));
                    if (number == 6)
                        enemies.Add(new Enemy(x * size, y, Resources.Zombie));
                }
            }

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

            foreach (HealthBonus hb in healthbonus)
                hb.Draw(spritebatch);

            foreach (VitesseBonus sb in speedbonus)
                sb.Draw(spritebatch);
        }
    }
}