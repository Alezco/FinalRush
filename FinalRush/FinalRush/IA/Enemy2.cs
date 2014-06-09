using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Audio;

namespace FinalRush
{
    class Enemy2
    {
        public Rectangle Hitbox;
        Direction Direction;
        int speed;
        int pv;
        int fallspeed;
        int speedjump = 1;
        int random;
        int framecolumn;
        int origin;
        SoundEffectInstance mort_enemies2;
        int compt = 0;
        bool left;
        public int enemy2_dead;
        int distance2player = 2000;
        List<Bullets> bullets;
        public List<Bullets> enemy_bullets;
        public bool a_portee;
        SoundEffectInstance shot_sound_instance;
        Color color;

        Random rand = new Random();
        SpriteEffects effect;
        Collisions collisions = new Collisions();

        public Enemy2(int x, int y, Texture2D newTexture)
        {
            framecolumn = 1;
            speed = 3;
            pv = 3;
            mort_enemies2 = Resources.enemies_sound.CreateInstance();
            fallspeed = 5;
            random = rand.Next(7, 15);
            effect = SpriteEffects.None;
            Direction = Direction.Right;
            Hitbox.Width = 30;
            Hitbox.Height = 38;
            Hitbox = new Rectangle(x, y, Hitbox.Width, Hitbox.Height);
            left = true;
            bullets = Global.Player.bullets;
            enemy_bullets = new List<Bullets>();
            Global.Enemy2 = this;
            a_portee = false;
            shot_sound_instance = Resources.tir_rafale.CreateInstance();
            origin = Hitbox.X;
        }

        public void Update(List<Wall> walls)
        {
            compt++; // Cette petite ligne correspond à l'IA ( WAAAW Gros QI )
            distance2player = Hitbox.X - Global.Player.Hitbox.X;

            #region Mort Ennemi
            for (int i = 0; i < bullets.Count(); i++)
            {
                if (Hitbox.Intersects(new Rectangle((int)bullets[i].position.X, (int)bullets[i].position.Y, 30, 30)))
                {
                    bullets[i].isVisible = false;
                    bullets.RemoveAt(i);
                    i--;
                    if (pv > 1)
                        pv--;
                    else
                    {
                        mort_enemies2.Play();
                        enemy2_dead++;
                        Hitbox.Width = 0;
                        Hitbox.Height = 0;
                    }
                }
            }

            if (Keyboard.GetState().IsKeyDown(Keys.D) && Global.Player.Hitbox.Intersects(Hitbox))
            {
                Hitbox.Width = 0;
                Hitbox.Height = 0;
            }


            #endregion

            #region Animation

            if (distance2player != 0)
            {
                if (framecolumn > 8)
                    framecolumn = 1;
                else if (compt % 2 == 0)
                    framecolumn++;
            }
            else
                framecolumn = 9;

            #endregion

            #region Gravité

            bool gravity = collisions.CollisionDown(Hitbox, walls, this.fallspeed);

            if (!gravity)
            {
                if (collisions.CollisionUp(Hitbox, walls, fallspeed))
                    speedjump = 0;
                if (speedjump > 1)
                {
                    this.Hitbox.Y -= speedjump;
                    speedjump -= 1;
                }
                else
                    this.Hitbox.Y += this.fallspeed;
            }
            else
                if (!collisions.CollisionDown(Hitbox, walls, this.fallspeed - 4))
                    if (!collisions.CollisionDown(Hitbox, walls, this.fallspeed - 2))
                        this.Hitbox.Y += 2;
                    else
                        this.Hitbox.Y++;

            #endregion

            #region Déplacements

            if (distance2player == 0)
                speed = 0;
            else
            {
                speed = 1;
                if (distance2player < 0 && distance2player >= -200)
                {
                    left = false;
                    compt = 1;
                }
                else
                    if (distance2player > 0 && distance2player <= 200)
                    {
                        left = true;
                        compt = 1;
                    }
                    else
                        if (distance2player > 200 || distance2player <= -200)
                        {
                            if (this.Hitbox.X <= origin - 200)
                            {
                                left = false;
                                compt = 1;
                            }
                            else if (this.Hitbox.X >= origin + 200)
                            {
                                left = true;
                                compt = 1;
                            }
                        }
            }
            if (left)
            {
                if (!collisions.CollisionLeft(Hitbox, walls, speed) && Hitbox.X > 0 && collisions.CollisionDown(new Rectangle(Hitbox.X - Hitbox.Width, Hitbox.Y, Hitbox.Width, Hitbox.Height), walls, speed))
                {
                    this.Hitbox.X -= speed;
                    this.Direction = Direction.Left;
                }
                else
                {
                    framecolumn = 1;
                    compt += 10;
                }
            }

            if (!left)
            {
                if (!collisions.CollisionRight(Hitbox, walls, speed) && collisions.CollisionDown(new Rectangle(Hitbox.X + Hitbox.Width, Hitbox.Y, Hitbox.Width, Hitbox.Height), walls, speed))
                {
                    this.Hitbox.X += speed;
                    this.Direction = Direction.Right;
                }
                else
                {
                    framecolumn = 1;
                    compt += 10;
                }
            }


            #endregion

            #region Tir

            //enemy_bullets.Capacity = 5;
            //if ((distance2player >= -100 && distance2player <= 0) || (distance2player > 0 && distance2player <= 100))
            //    a_portee = true;
            //else
            //    a_portee = false;
            //if (Math.Abs(Hitbox.Y - Global.Player.Hitbox.Y) < 3 && a_portee && !enemy_shot && enemy_bullets.Count < 5)
            //{
            //    shot_sound_instance.Play();
            //    enemy_shot = true;
            //    Bullets bullet = new Bullets(Resources.bullet);
            //    bullet.velocity = 5;
            //    bullet.isVisible = true;
            //    enemy_bullets.Add(bullet);
            //    if (Direction == Direction.Right)
            //        bullet.position = new Vector2(Hitbox.X + Hitbox.Width / 2, Hitbox.Y + Hitbox.Height / 3) + new Vector2(bullet.velocity * 5, 0);
            //    else
            //        bullet.position = new Vector2(Hitbox.X, Hitbox.Y + Hitbox.Height / 3) + new Vector2(bullet.velocity * 5, 0);

            //}
            //else
            //    enemy_shot = false;

            //// la balle disparait si elle parcourt la distance ou rencontre un obstacle


            //foreach (Bullets bullet in enemy_bullets)
            //{
            //    if (Direction == Direction.Right)
            //        bullet.position.X += bullet.velocity; // va vers la droite
            //    else
            //        bullet.position.X -= bullet.velocity; // va vers la gauche
            //    if (Vector2.Distance(bullet.position, new Vector2(Hitbox.X, Hitbox.Y)) >= 100)
            //        bullet.isVisible = false;
            //    else
            //        if (Global.Player.Hitbox.Intersects(new Rectangle((int)bullet.position.X, (int)bullet.position.Y, 3, 3)))
            //        {
            //            Global.Player.health++;
            //            bullet.isVisible = false;
            //        }

            //    foreach (Wall wall in Global.GameMain.Walls)
            //    {
            //        if (wall.Hitbox.Intersects(new Rectangle((int)bullet.position.X, (int)bullet.position.Y, 5, 2)))
            //            bullet.isVisible = false;
            //    }
            //}

            //for (int i = 0; i < enemy_bullets.Count; i++)
            //{
            //    if (!enemy_bullets[i].isVisible)
            //    {
            //        enemy_bullets.RemoveAt(i);
            //        i--;
            //    }
            //}

            #endregion

            switch (Direction)
            {
                case Direction.Right:
                    effect = SpriteEffects.FlipHorizontally;
                    break;
                case Direction.Left:
                    effect = SpriteEffects.None;
                    break;
            }
            switch (pv)
            {
                case 1:
                    color.A = 100;
                    break;
                case 2:
                    color.A = 200;
                    break;
                case 3:
                    color = Color.White;
                    break;
            }
        }


        public void Draw(SpriteBatch spritebatch)
        {
            spritebatch.Draw(Resources.Elite, Hitbox, new Rectangle((framecolumn - 1) * 30, 0, Hitbox.Width, Hitbox.Height), color, 0f, new Vector2(0, 0), effect, 0f);
        }
    }
}
