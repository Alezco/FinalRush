using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace FinalRush.Game.Levels
{
    class Particules
    {
        public Rectangle Hitbox;
        int hauteur, largeur, entier;
        Random rand = new Random();
        Color color = new Color(10, 255, 255, 255);
        Texture2D texture;

        public Particules(int larg, int haut, int ent)
        {
            largeur = larg;
            hauteur = haut;
            Hitbox.X = largeur;
            Hitbox.Y = hauteur;
            entier = ent;
            switch (entier)
            {
                case 0:
                    Hitbox.Width = 10;
                    Hitbox.Height = 11;
                    texture = Resources.particule1;
                    break;
                case 1:
                    Hitbox.Width = 15;
                    Hitbox.Height = 16;
                    texture = Resources.particule2;
                    break;
                case 2:
                    Hitbox.Width = 20;
                    Hitbox.Height = 22;
                    texture = Resources.particule3;
                    break;
            }
        }

        public void Update()
        {
            Hitbox.Y++;
            if (Hitbox.Y > 480) Hitbox.Y = 0;
        }

        public void Draw(SpriteBatch spritebatch)
        {
            spritebatch.Draw(texture, Hitbox, Color.White);
        }
    }
}
