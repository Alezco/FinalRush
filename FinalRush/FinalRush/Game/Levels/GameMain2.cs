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
    class GameMain2
    {
        // FIELDS

        public Player LocalPlayer;
        public List<Wall> Walls;
        public List<Bonus> bonus;
        public List<HealthBonus> healthbonus;
        public List<VitesseBonus> speedbonus;
        public List<Enemy> enemies;
        public List<Enemy2> enemies2;
        Random random = new Random();
        MainMenu menu;
        Texture2D background = Resources.Environnment2;
        Texture2D foreground = Resources.Foreground2;

        // CONSTRUCTOR

        public GameMain2()
        {
            menu = new MainMenu(Global.Handler, 0f);
            LocalPlayer = new Player();
            Walls = new List<Wall>();
            bonus = new List<Bonus>();
            healthbonus = new List<HealthBonus>();
            speedbonus = new List<VitesseBonus>();
            enemies = new List<Enemy>();
            enemies2 = new List<Enemy2>();
            Global.GameMain2 = this;

            #region Ennemis
            enemies.Add(new Enemy(1288, 56, Resources.Zombie));
            enemies.Add(new Enemy(1188, 56, Resources.Zombie));
            enemies.Add(new Enemy(1388, 56, Resources.Zombie));
            enemies.Add(new Enemy(1150, 180, Resources.Zombie));
            enemies.Add(new Enemy(1150, 180, Resources.Zombie));
            enemies.Add(new Enemy(1088, 260, Resources.Zombie));
            enemies.Add(new Enemy(1302, 300, Resources.Zombie));
            enemies.Add(new Enemy(1302, 300, Resources.Zombie));
            enemies.Add(new Enemy(1150, 340, Resources.Zombie));

            enemies.Add(new Enemy(4500, 340, Resources.Zombie));
            enemies2.Add(new Enemy2(4500, 340, Resources.Zombie));
            enemies2.Add(new Enemy2(1150, 340, Resources.Zombie));
            enemies2.Add(new Enemy2(1150, 340, Resources.Zombie));
            #endregion

            #region Plateformes
            Walls.Add(new Wall(240, 400, Resources.Platform, 100, 16, Color.White));
            Walls.Add(new Wall(340, 330, Resources.Platform, 100, 16, Color.White));
            Walls.Add(new Wall(490, 380, Resources.Platform, 100, 16, Color.White));
            Walls.Add(new Wall(1088, 96, Resources.Platform, 450, 16, Color.White));
            Walls.Add(new Wall(1150, 180, Resources.Platform, 450, 16, Color.White));
            Walls.Add(new Wall(1088, 260, Resources.Platform, 450, 16, Color.White));
            Walls.Add(new Wall(1150, 340, Resources.Platform, 450, 16, Color.White));
            Walls.Add(new Wall(2440, 340, Resources.Platform, 50, 16, Color.White));
            Walls.Add(new Wall(2500, 280, Resources.Platform, 50, 16, Color.White));
            Walls.Add(new Wall(2580, 220, Resources.Platform, 50, 16, Color.White));
            Walls.Add(new Wall(2640, 160, Resources.Platform, 240, 16, Color.White));
            #endregion

            #region Terrain
            Walls.Add(new Wall(768, 416, Resources.Terre_neige, 64, 64, Color.White));
            Walls.Add(new Wall(768, 352, Resources.Herbe_neige, 64, 64, Color.White));
            Walls.Add(new Wall(832, 416, Resources.Terre_neige, 64, 64, Color.White));
            Walls.Add(new Wall(832, 352, Resources.Terre_neige, 64, 64, Color.White));
            Walls.Add(new Wall(832, 288, Resources.Herbe_neige, 64, 64, Color.White));
            Walls.Add(new Wall(896, 416, Resources.Terre_neige, 64, 64, Color.White));
            Walls.Add(new Wall(896, 352, Resources.Terre_neige, 64, 64, Color.White));
            Walls.Add(new Wall(896, 288, Resources.Terre_neige, 64, 64, Color.White));
            Walls.Add(new Wall(896, 224, Resources.Herbe_neige, 64, 64, Color.White));
            Walls.Add(new Wall(960, 416, Resources.Terre_neige, 64, 64, Color.White));
            Walls.Add(new Wall(960, 352, Resources.Terre_neige, 64, 64, Color.White));
            Walls.Add(new Wall(960, 288, Resources.Terre_neige, 64, 64, Color.White));
            Walls.Add(new Wall(960, 224, Resources.Terre_neige, 64, 64, Color.White));
            Walls.Add(new Wall(960, 160, Resources.Herbe_neige, 64, 64, Color.White));
            Walls.Add(new Wall(1024, 416, Resources.Terre_neige, 64, 64, Color.White));
            Walls.Add(new Wall(1024, 352, Resources.Terre_neige, 64, 64, Color.White));
            Walls.Add(new Wall(1024, 288, Resources.Terre_neige, 64, 64, Color.White));
            Walls.Add(new Wall(1024, 224, Resources.Terre_neige, 64, 64, Color.White));
            Walls.Add(new Wall(1024, 160, Resources.Terre_neige, 64, 64, Color.White));
            Walls.Add(new Wall(1024, 96, Resources.Herbe_neige, 64, 64, Color.White));
            Walls.Add(new Wall(1600, 288, Resources.Terre_neige, 64, 64, Color.White));
            Walls.Add(new Wall(1600, 224, Resources.Terre_neige, 64, 64, Color.White));
            Walls.Add(new Wall(1600, 160, Resources.Terre_neige, 64, 64, Color.White));
            Walls.Add(new Wall(1600, 96, Resources.Terre_neige, 64, 64, Color.White));
            Walls.Add(new Wall(1600, 32, Resources.Terre_neige, 64, 64, Color.White));
            Walls.Add(new Wall(1600, -32, Resources.Terre_neige, 64, 64, Color.White));
            Walls.Add(new Wall(1920, 416, Resources.Terre_neige, 64, 64, Color.White));
            Walls.Add(new Wall(1984, 416, Resources.Terre_neige, 64, 64, Color.White));
            Walls.Add(new Wall(2112, 416, Resources.Terre_neige, 64, 64, Color.White));
            Walls.Add(new Wall(2176, 416, Resources.Terre_neige, 64, 64, Color.White));
            Walls.Add(new Wall(2240, 416, Resources.Terre_neige, 64, 64, Color.White));
            Walls.Add(new Wall(1920, 352, Resources.Herbe_neige, 64, 64, Color.White));
            Walls.Add(new Wall(1984, 352, Resources.Herbe_neige, 64, 64, Color.White));
            Walls.Add(new Wall(2112, 352, Resources.Herbe_neige, 64, 64, Color.White));
            Walls.Add(new Wall(2176, 352, Resources.Herbe_neige, 64, 64, Color.White));
            Walls.Add(new Wall(2240, 352, Resources.Herbe_neige, 64, 64, Color.White));
            Walls.Add(new Wall(2880, 416, Resources.Terre_neige, 64, 64, Color.White));
            Walls.Add(new Wall(2880, 352, Resources.Terre_neige, 64, 64, Color.White));
            Walls.Add(new Wall(2880, 288, Resources.Terre_neige, 64, 64, Color.White));
            Walls.Add(new Wall(2880, 224, Resources.Terre_neige, 64, 64, Color.White));
            Walls.Add(new Wall(2880, 160, Resources.Terre_neige, 64, 64, Color.White));
            Walls.Add(new Wall(2880, 64, Resources.Terre_neige, 64, 64, Color.White));
            Walls.Add(new Wall(2880, 0, Resources.Terre_neige, 64, 64, Color.White));
            Walls.Add(new Wall(3200, 416, Resources.Terre_neige, 64, 64, Color.White));
            Walls.Add(new Wall(3200, 352, Resources.Herbe_neige, 64, 64, Color.White));
            Walls.Add(new Wall(3328, 416, Resources.Terre_neige, 64, 64, Color.White));
            Walls.Add(new Wall(3328, 352, Resources.Terre_neige, 64, 64, Color.White));
            Walls.Add(new Wall(3328, 288, Resources.Herbe_neige, 64, 64, Color.White));
            Walls.Add(new Wall(3456, 416, Resources.Terre_neige, 64, 64, Color.White));
            Walls.Add(new Wall(3456, 352, Resources.Terre_neige, 64, 64, Color.White));
            Walls.Add(new Wall(3456, 288, Resources.Terre_neige, 64, 64, Color.White));
            Walls.Add(new Wall(3456, 224, Resources.Herbe_neige, 64, 64, Color.White));
            Walls.Add(new Wall(3584, 416, Resources.Terre_neige, 64, 64, Color.White));
            Walls.Add(new Wall(3584, 352, Resources.Terre_neige, 64, 64, Color.White));
            Walls.Add(new Wall(3584, 288, Resources.Terre_neige, 64, 64, Color.White));
            Walls.Add(new Wall(3584, 224, Resources.Terre_neige, 64, 64, Color.White));
            Walls.Add(new Wall(3584, 160, Resources.Herbe_neige, 64, 64, Color.White));
            Walls.Add(new Wall(3712, 416, Resources.Terre_neige, 64, 64, Color.White));
            Walls.Add(new Wall(3712, 352, Resources.Terre_neige, 64, 64, Color.White));
            Walls.Add(new Wall(3712, 288, Resources.Terre_neige, 64, 64, Color.White));
            Walls.Add(new Wall(3712, 224, Resources.Terre_neige, 64, 64, Color.White));
            Walls.Add(new Wall(3712, 160, Resources.Terre_neige, 64, 64, Color.White));
            Walls.Add(new Wall(3712, 96, Resources.Herbe_neige, 64, 64, Color.White));
            Walls.Add(new Wall(3904, 416, Resources.Terre_neige, 64, 64, Color.White));
            Walls.Add(new Wall(3904, 352, Resources.Terre_neige, 64, 64, Color.White));
            Walls.Add(new Wall(3904, 288, Resources.Terre_neige, 64, 64, Color.White));
            Walls.Add(new Wall(3904, 224, Resources.Terre_neige, 64, 64, Color.White));
            Walls.Add(new Wall(3904, 160, Resources.Herbe_neige, 64, 64, Color.White));

            for (int i = 0; i < 80; i++)
                if (i != 3 & i != 4 & i != 5 & i != 6 & i != 7 & i != 8 & i != 9
                           & i != 12 & i != 13 & i != 14 & i != 15 & i != 16 & i != 27 & i != 28
                           & i != 30 & i != 31 & i != 32 & i != 33 & i != 34 & i != 35 & i != 45
                           & i != 50 & i != 51 & i != 52 & i != 53 & i != 54 & i != 55 & i != 56 & i != 57 & i != 58 & i != 59 & i != 60 & i != 61 & i != 62 & i != 63)
                    Walls.Add(new Wall(64 * i, 416, Resources.Herbe_neige, 64, 64, Color.White));
            #endregion

            #region Bonus
            bonus.Add(new Bonus(384, 300, Resources.Coin, 20, 20, Color.White));
            bonus.Add(new Bonus(4000, 100, Resources.Coin, 20, 20, Color.White));
            bonus.Add(new Bonus(1100, 240, Resources.Coin, 20, 20, Color.White));
            bonus.Add(new Bonus(1288, 56, Resources.Coin, 20, 20, Color.White));
            bonus.Add(new Bonus(2060, 300, Resources.Coin, 20, 20, Color.White));
            healthbonus.Add(new HealthBonus(1500, 320, Resources.Health, 20, 20, Color.White));
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
            foreach (Enemy e in enemies)
                e.Draw(spritebatch);

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
