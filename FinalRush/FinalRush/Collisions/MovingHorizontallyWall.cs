using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace FinalRush
{
    class MovingHorizontallyWall
    {
        // FIELDS

        public Rectangle Hitbox;
        Texture2D Texture;
        Color color;
        Collisions collisions = new Collisions();
        int speed;
        int bornegauche;
        int bornedroite;

        // CONSTRUCTOR

        public MovingHorizontallyWall(int x, int y, Texture2D Texture, int width, int height, int bornegauche, int bornedroite, Color color)
        {
            this.Texture = Texture;
            this.Hitbox = new Rectangle(x, y, width, height);
            this.color = color;
            this.bornegauche = bornegauche;
            this.bornedroite = bornedroite;
            Global.MovingHorizontallyWall = this;
            speed = 1;
        }

        // UPDATE & DRAW

        public void Update(MouseState souris, KeyboardState clavier)
        {
            if (Hitbox.X >= bornegauche)
                if (Hitbox.X != bornedroite)
                    Hitbox.X += speed;

            if (Hitbox.X <= bornedroite)
                if (Hitbox.X != bornegauche)
                    Hitbox.X -= speed;
        }

        public void Draw(SpriteBatch spritebatch)
        {
            spritebatch.Draw(Texture, Hitbox, color);
        }
    }
}
