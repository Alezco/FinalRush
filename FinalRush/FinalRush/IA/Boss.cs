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
    class Boss
    {
        public Rectangle Hitbox;
        Direction Direction;
        int speed;
        public int pv;
        int fallspeed;
        int speedjump = 1;
        int random;
        int framecolumn;
        int origin;
        int compteur = 0, compt = 0;
        bool left, cut = false;
        public bool isDead;
        int distance2player = 2000;
        List<Bullets> bullets;
        public List<Bullets> enemy_bullets;
        public bool a_portee;
        SoundEffectInstance shot_sound_instance, Boss_dead_instance;
        Color color;

        Random rand = new Random();
        SpriteEffects effect;
        Collisions collisions = new Collisions();

        public Boss(int x, int y, Texture2D newTexture)
        {
            framecolumn = 1;
            speed = 2;
            pv = 30;
            fallspeed = 5;
            random = rand.Next(7, 15);
            effect = SpriteEffects.None;
            Direction = Direction.Right;
            Hitbox.Width = 152;
            Hitbox.Height = 94;
            Hitbox = new Rectangle(x, y, Hitbox.Width, Hitbox.Height);
            left = true;
            isDead = false;
            bullets = Global.Player.bullets;
            enemy_bullets = new List<Bullets>();
            Global.Boss = this;
            a_portee = false;
            shot_sound_instance = Resources.tir_rafale.CreateInstance();
            Boss_dead_instance = Resources.boss_mort_sound.CreateInstance();
            origin = Hitbox.X;
        }

        public void Update(List<Wall> walls)
        {
            compt++; // Cette petite ligne correspond à l'IA ( WAAAW Gros QI )
            distance2player = Hitbox.X - Global.Player.Hitbox.X;

            #region Mort Boss
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
                        isDead = true;
                }
            }

            if (!isDead && Keyboard.GetState().IsKeyDown(Keys.D) && Global.Player.Hitbox.Intersects(Hitbox))
            {
                if (compteur == 1) cut = false;
                else
                {
                    cut = true;
                    compteur++;
                }
            }
            else compteur = 0;

            if (!isDead && cut && Keyboard.GetState().IsKeyDown(Keys.D) && Global.Player.Hitbox.Intersects(Hitbox))
            {
                if (pv > 1)
                    pv -= 3;
                else
                    isDead = true;
            }


            #endregion

            #region Animation
            if (!isDead)
            {
                if (pv <= 15)
                    speed += 5;

                if (distance2player != 0)
                {
                    if (framecolumn > 8)
                        framecolumn = 2;
                    else if (compt % 5 == 0)
                        framecolumn++;
                }
                else
                    framecolumn = 9;
            }
            #endregion

            #region Gravité
            if (!isDead)
            {
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
            }
            #endregion

            #region Déplacements
            if (!isDead)
            {
                if (distance2player == 0)
                    speed = 0;
                else
                {
                    if (pv > 20)
                        speed = 1;
                    else if (pv > 10)
                        speed = 2;
                    else
                        speed = 3;
                    if (distance2player < 0 && distance2player >= -800)
                    {
                        left = false;
                    }
                    else
                        if (distance2player > 0 && distance2player <= 800)
                        {
                            left = true;
                        }
                        else
                            if (distance2player > 800 || distance2player <= -800)
                            {
                                if (this.Hitbox.X <= origin - 800)
                                {
                                    left = false;
                                }
                                else if (this.Hitbox.X >= origin + 800)
                                {
                                    left = true;
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
                        framecolumn = 2;
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
                        framecolumn = 2;
                        compt += 10;
                    }
                }
            }

            #endregion

            switch (Direction)
            {
                case Direction.Left:
                    effect = SpriteEffects.FlipHorizontally;
                    break;
                case Direction.Right:
                    effect = SpriteEffects.None;
                    break;
            }
            switch (pv)
            {
                case 1:
                    color.A = 20;
                    break;
                case 5:
                    color.A = 40;
                    break;
                case 10:
                    color.A = 80;
                    break;
                case 15:
                    color.A = 120;
                    break;
                case 20:
                    color.A = 160;
                    break;
                case 25:
                    color.A = 200;
                    break;
                case 30:
                    color = Color.White;
                    break;
            }
        }

        public void Draw(SpriteBatch spritebatch)
        {
            if (!isDead)
                spritebatch.Draw(Resources.Boss, Hitbox, new Rectangle((framecolumn - 1) * 152, 0, Hitbox.Width, Hitbox.Height), color, 0f, new Vector2(0, 0), effect, 0f);
        }
    }
}