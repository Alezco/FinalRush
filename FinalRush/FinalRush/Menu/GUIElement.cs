using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace FinalRush
{
    class GUIElement
    {
        private Texture2D GUITexture;
        private Rectangle GUIRect;
        Color colour = new Color(255, 255, 255, 255);
        bool colorUp = false;

        private string assetName;
        public Song MusiqueMain;
        public Song MusiqueMenu;
        public bool HasClicked = false;

        public string AssetName
        {
            get { return assetName; }
            set { assetName = value; }
        }

        public delegate void ElementClicked(string element);
        public event ElementClicked clickEvent;

        public GUIElement(string assetName)
        {
            this.assetName = assetName;
            Global.GUIElement = this;
        }

        public void LoadContent(ContentManager content)
        {
            GUITexture = content.Load<Texture2D>(assetName);
            GUIRect = new Rectangle(0, 0, GUITexture.Width, GUITexture.Height);
            MusiqueMain = content.Load<Song>(@"Sons\Musiques\MusiqueTest");
            MusiqueMenu = content.Load<Song>(@"Sons\Musiques\Musique_Menu_Test");
        }

        public void Update()
        {
            if (GUIRect.Contains(new Point(Mouse.GetState().X, Mouse.GetState().Y)))
            {
                if (colour.R > 250) colorUp = false;
                if (colour.R < 5) colorUp = true;
                if (colorUp)
                {
                    colour.R += 4;
                    colour.G += 2;
                    colour.B += 2;
                }
                else
                {
                    colour.R -= 4;
                    colour.G -= 2;
                    colour.B -= 2;
                }
                if (Mouse.GetState().LeftButton == ButtonState.Pressed) HasClicked = true;
                else
                    if (HasClicked)
                    {
                        clickEvent(assetName);
                        HasClicked = false;
                    }
            }
            else
            {
                colour.R = 255;
                colour.G = 255;
                colour.B = 255;
            }
            /*if (GUIRect.Contains(new Point(Mouse.GetState().X, Mouse.GetState().Y)) && Mouse.GetState().LeftButton == ButtonState.Pressed)
            {
                HasClicked = true;
            }
            if (GUIRect.Contains(new Point(Mouse.GetState().X, Mouse.GetState().Y)) && Mouse.GetState().LeftButton == ButtonState.Released && HasClicked)
            {
                clickEvent(assetName);
                HasClicked = false;
            }*/
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(GUITexture, GUIRect, colour);
        }

        public void CenterElement(int height, int width)
        {
            GUIRect = new Rectangle((width / 2) - (this.GUITexture.Width) / 2, (height / 2) - (this.GUITexture.Height) / 2, this.GUITexture.Width, this.GUITexture.Height);
        }

        public void MoveElement(int x, int y)
        {
            GUIRect = new Rectangle(GUIRect.X += x, GUIRect.Y += y, GUIRect.Width, GUIRect.Height);
        }
    }
}