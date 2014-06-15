using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;

namespace FinalRush
{
    class Collisions
    {
        public bool collision_speed;
        public bool LowSpeed;
        SoundEffectInstance bonus_bruitages, marco_touched_instance;
        public Color marco_color;
        public int accu = 0;

        public Collisions()
        {
            marco_color = Color.White;
            Global.Collisions = this;
            bonus_bruitages = Resources.bonus_bruitage.CreateInstance();
            marco_touched_instance = Resources.marco_touched_sound.CreateInstance();
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
                    bonus_bruitages.Play();
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
                    bonus_bruitages.Play();
                    sb.Hitbox = new Rectangle(0, 0, 0, 0);
                    collision_speed = true;
                }
            }
        }
        #endregion

        #region Collisions Ennemis
        public void CollisionEnemy(Rectangle Hitbox, List<Enemy> enemy)
        {
            Rectangle newHitbox = new Rectangle(Hitbox.X, Hitbox.Y, Hitbox.Width, Hitbox.Height);

            foreach (Enemy e in enemy)
            {
                if (!e.isDead && newHitbox.Intersects(e.Hitbox))
                {
                    accu = 20;
                    Global.Player.health--;
                    marco_touched_instance.Play();
                }
                if (Global.Player.health < 0)
                    Global.Player.health = 0;
            }
        }

        public void CollisionEnemy2(Rectangle Hitbox, List<Enemy2> enemy)
        {
            Rectangle newHitbox = new Rectangle(Hitbox.X, Hitbox.Y, Hitbox.Width, Hitbox.Height);

            foreach (Enemy2 e in enemy)
            {
                if (!e.isDead && newHitbox.Intersects(e.Hitbox))
                {
                    accu = 20;
                    Global.Player.health--;
                    marco_touched_instance.Play();
                }
                if (Global.Player.health < 0)
                    Global.Player.health = 0;
            }
        }

        public void CollisionBoss(Rectangle Hitbox,  List<Boss> boss)
        {
            Rectangle newHitbox = new Rectangle(Hitbox.X, Hitbox.Y, Hitbox.Width, Hitbox.Height);
            //bool collision = false;
            foreach (Boss dragon in boss)
            {
                if (!dragon.isDead && Hitbox.Intersects(dragon.Hitbox))
                {
                    Global.Player.health--;
                    accu = 20;
                    marco_touched_instance.Play();
                }
                if (Global.Player.health < 0)
                    Global.Player.health = 0;
            }
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

        public void CollisionLow(Rectangle Hitbox, List<LowSpeedArea> low)
        {
            Rectangle newHitbox = new Rectangle(Hitbox.X, Hitbox.Y, Hitbox.Width, Hitbox.Height);

            foreach (LowSpeedArea lsa in low)
            {
                if (newHitbox.Intersects(lsa.Hitbox))
                    LowSpeed = true;
                else
                    LowSpeed = false;
            }
        }
       
    }
}