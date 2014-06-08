using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;

namespace FinalRush
{
    class Collisions
    {
        public bool collision_speed = false;
        public Collisions()
        {
            Global.Collisions = this;
        }

        #region Collisions Walls
        public bool CollisionRight(Rectangle Hitbox, List<Wall> walls, int speed) //Collision à droite du perso
        {
            Rectangle newHitbox = new Rectangle(Hitbox.X + speed, Hitbox.Y, Hitbox.Width, Hitbox.Height); //représente la Hitbox du perso à l'instant suivant
            bool collision = false;

            foreach (Wall wall in walls)
            {
                if (newHitbox.Intersects(wall.Hitbox) && Hitbox.X < wall.Hitbox.X)
                {
                    collision = true;
                    break;
                }
            }

            return collision;
        }

        public bool CollisionLeft(Rectangle Hitbox, List<Wall> walls, int speed) //Collision à gauche du perso
        {
            Rectangle newHitbox = new Rectangle(Hitbox.X - speed, Hitbox.Y, Hitbox.Width, Hitbox.Height);
            bool collision = false;

            foreach (Wall wall in walls)
            {
                if (newHitbox.Intersects(wall.Hitbox) && Hitbox.X > wall.Hitbox.X)
                {
                    collision = true;
                    break;
                }
            }

            return collision;
        }

        public bool CollisionUp(Rectangle Hitbox, List<Wall> walls, int speed) //Collision plafond et perso
        {
            Rectangle newHitbox = new Rectangle(Hitbox.X, Hitbox.Y - speed, Hitbox.Width, Hitbox.Height);
            bool collision = false;

            foreach (Wall wall in walls)
            {
                if (newHitbox.Intersects(wall.Hitbox) && Hitbox.Y > wall.Hitbox.Y)
                {
                    collision = true;
                    break;
                }
            }

            return collision;
        }

        public bool CollisionDown(Rectangle Hitbox, List<Wall> walls, int speed) //Collision sol et perso
        {
            Rectangle newHitbox = new Rectangle(Hitbox.X, Hitbox.Y + speed, Hitbox.Width, Hitbox.Height);
            bool collision = false;

            foreach (Wall wall in walls)
            {
                if ((newHitbox.Intersects(wall.Hitbox) && Hitbox.Y < wall.Hitbox.Y))
                {
                    collision = true;
                    break;
                }
            }

            return collision;
        }
        #endregion

        #region Collisions Bonus

        public bool CollisionBonus(Rectangle Hitbox, List<Bonus> bonus)
        {
            Rectangle newHitbox = new Rectangle(Hitbox.X, Hitbox.Y, Hitbox.Width, Hitbox.Height);
            bool collision = false;

            foreach (Bonus b in bonus)
            {
                if ((newHitbox.Intersects(b.Hitbox)))
                {
                    b.Hitbox = new Rectangle(0, 0, 0, 0);
                    //Global.GameMain.bonus.Remove(b);
                    collision = true;
                    break;
                }
            }
            return collision;
        }

        public void CollisionHealthBonus(Rectangle Hitbox, List<HealthBonus> healthbonus)
        {
            Rectangle newHitbox = new Rectangle(Hitbox.X, Hitbox.Y, Hitbox.Width, Hitbox.Height);

            foreach (HealthBonus hb in healthbonus)
            {
                if ((newHitbox.Intersects(hb.Hitbox)))
                {
                    hb.Hitbox = new Rectangle(0, 0, 0, 0);
                    Global.Player.health += 20;
                    if (Global.Player.health > 100)
                        Global.Player.health = 100;
                }
            }
        }

        public void CollisionSpeedBonus(Rectangle Hitbox, List<VitesseBonus> speedbonus, GameTime gameTime)
        {
            Rectangle newHitbox = new Rectangle(Hitbox.X, Hitbox.Y, Hitbox.Width, Hitbox.Height);

            foreach (VitesseBonus sb in speedbonus)
            {
                if ((newHitbox.Intersects(sb.Hitbox)))
                {
                    sb.Hitbox = new Rectangle(0, 0, 0, 0);
                    collision_speed = true;
                }
            }
        }
        #endregion

        #region Collisions Ennemis
        public bool CollisionEnemy(Rectangle Hitbox, List<Enemy> enemy)
        {
            Rectangle newHitbox = new Rectangle(Hitbox.X, Hitbox.Y, Hitbox.Width, Hitbox.Height);
            bool collision = false;

            foreach (Enemy e in enemy)
            {
                if (newHitbox.Intersects(e.Hitbox))
                    Global.Player.health--;
                if (Global.Player.health < 0)
                    Global.Player.health = 0;
            }

            return collision;
        }

        public bool CollisionEnemy2(Rectangle Hitbox, List<Enemy2> enemy)
        {
            Rectangle newHitbox = new Rectangle(Hitbox.X, Hitbox.Y, Hitbox.Width, Hitbox.Height);
            bool collision = false;

            foreach (Enemy2 e in enemy)
            {
                if (newHitbox.Intersects(e.Hitbox))
                    Global.Player.health--;
                if (Global.Player.health < 0)
                    Global.Player.health = 0;
            }
            return collision;
        }
        #endregion

        #region Collisions Piques
        public void CollisionPiques(Rectangle Hitbox, List<Piques> piques)
        {
            Rectangle newHitbox = new Rectangle(Hitbox.X, Hitbox.Y, Hitbox.Width, Hitbox.Height);

            foreach (Piques p in piques)
            {
                if (newHitbox.Intersects(p.Hitbox))
                    Global.Player.dead = true;
            }
        }
        #endregion
    }
}