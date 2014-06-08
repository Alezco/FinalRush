using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Audio;


namespace FinalRush
{
    class Bonus
    {
        // FIELDS

        public Rectangle Hitbox;
        public Texture2D Texture;
        public Color color;
        // CONSTRUCTOR

        public Bonus(int x, int y, Texture2D Texture, int width, int height, Color color)
        {
            this.Texture = Texture;
            this.Hitbox = new Rectangle(x, y, width, height);
            this.color = color;
            Global.Bonus = this;
        }

        // UPDATE & DRAW

        public void Update(MouseState souris, KeyboardState clavier, List<Bonus> bonus)
        {

        }

        public void Draw(SpriteBatch spritebatch)
        {
            spritebatch.Draw(Texture, Hitbox, color);
        }
    }
}