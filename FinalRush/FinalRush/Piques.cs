using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace FinalRush
{
    class Piques
    {
      // FIELDS

        public Rectangle Hitbox;
        Texture2D Texture;
        Color color;


        // CONSTRUCTOR

        public Piques(int x, int y, Texture2D Texture, int width, int height, Color color)
        {
            this.Texture = Texture;
            this.Hitbox = new Rectangle(x, y, width, height);
            this.color = color;
            Global.Piques = this;
        }

        // UPDATE & DRAW

        public void Update(MouseState souris, KeyboardState clavier, List<Wall> walls)
        {

        }

        public void Draw(SpriteBatch spritebatch)
        {
            spritebatch.Draw(Texture, Hitbox, color);
        }
    }
}
