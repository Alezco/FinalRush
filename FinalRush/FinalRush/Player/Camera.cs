using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace FinalRush
{
    class Camera
    {
        public Matrix transform;
        public Viewport view;
        public Vector2 centre;
        MainMenu menu;
        Collisions collisions;
        GameMain main;

        public int screenwidth = 800;
        public int screenheight = 480;

        public Camera(Viewport newView, MainMenu menu)
        {
            view = newView;
            this.menu = Global.MainMenu;
            collisions = Global.Collisions;
            main = Global.GameMain;
            Global.Camera = this;
        }

        public void Update(GameTime gametime, Player player)
        {
            if (menu.EnJeu(menu.enjeu))
            {
                centre = new Vector2(player.Hitbox.X + player.Hitbox.Width / 2 - screenwidth / 2, 0);
                transform = Matrix.CreateTranslation(new Vector3(-centre.X, -centre.Y, 0));

                if (player.Hitbox.X < 400)
                    transform = Matrix.CreateTranslation(new Vector3(0, 0, 0));
                if (player.Hitbox.X > 4200)
                    transform = Matrix.CreateTranslation(new Vector3(-4200 + screenwidth / 2, -centre.Y, 0));
            }

            else
            {
                centre = new Vector2(0, 0);
                transform = Matrix.CreateTranslation(new Vector3(-centre.X, -centre.Y, 0));
            }
        }
    }
}