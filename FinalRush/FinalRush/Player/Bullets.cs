using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace FinalRush
{
    class Bullets
    {
        public Texture2D texture;

        public Vector2 position;
        public int velocity;
        public bool isVisible;


        public Bullets(Texture2D newTexture)
        {
            texture = newTexture;
            isVisible = false;
            Global.Bullets = this;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, position, null, Color.White, 0f, new Vector2(), 1f, SpriteEffects.None, 0);
        }
    }
}
