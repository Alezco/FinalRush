using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace FinalRush.Game.Levels
{
    class Flames
    {
        public Rectangle Hitbox;
        int framecolumn = 1;
        Color color = new Color(255, 255, 255, 255);

        public Flames(int larg, int haut)
        {
            Hitbox.X = larg;
            Hitbox.Y = haut;
            Hitbox.Height = 150;
            Hitbox.Width = 150;
        }

        public void Update()
        {
            if (framecolumn == 6) framecolumn = 1;
            else framecolumn++;
            Hitbox.Y++;
            if (Hitbox.Y > 480) Hitbox.Y = -100;
        }

        public void Draw(SpriteBatch spritebatch)
        {
            spritebatch.Draw(Resources.flames, Hitbox, new Rectangle((framecolumn - 1) * 150, 0, Hitbox.Width, Hitbox.Height), Color.White);
        }
    }
}
