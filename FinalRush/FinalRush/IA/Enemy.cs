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
    class Enemy
    {
        public Rectangle Hitbox;
        Direction Direction;
        int speed;
        int fallspeed;
        int speedjump = 1;
        int random;
        int framecolumn;
        int compt = 0;
        int playerDistance;
        bool playerProche;
        bool left;
        public bool isDead;
        List<Bullets> bullets;
        Random rand = new Random();
        SpriteEffects effect;
        Collisions collisions = new Collisions();

        public Enemy(int x, int y, Texture2D newTexture)
        {
            framecolumn = 1;
            speed = 1;
            fallspeed = 5;
            playerProche = false;
            isDead = false;
            random = rand.Next(7, 15);
            effect = SpriteEffects.None;
            Direction = Direction.Right;
            Hitbox.Width = 37;
            Hitbox.Height = 39;
            Hitbox = new Rectangle(x, y, Hitbox.Width, Hitbox.Height);
            bullets = Global.Player.bullets;
            Global.Enemy = this;
        }

        public void Update(List<Wall> walls, int random)
        {
            compt++; // Cette petite ligne correspond à l'IA ( WAAAW Gros QI )

            playerDistance = Hitbox.X - Global.Player.Hitbox.X;
            playerProche = Math.Abs(playerDistance) < 200;


            #region Mort Ennemi
            for (int i = 0; i < bullets.Count(); i++)
            {
                if (!isDead & this.Hitbox.Intersects(new Rectangle((int)bullets[i].position.X, (int)bullets[i].position.Y, 30, 30)))
                {
                    isDead = true;
                    bullets[i].isVisible = false;
                    bullets.RemoveAt(i);
                    i--;
                }
            }

            if (!isDead && Keyboard.GetState().IsKeyDown(Keys.D) && Global.Player.Hitbox.Intersects(Hitbox))
                isDead = true;

            #endregion

            #region Animation

            if (framecolumn > 15) framecolumn = 1;
            else if (compt % 2 == 0) framecolumn++;

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

            if (!playerProche)
            {

                if (compt > random)
                {
                    if (left) left = false;
                    else left = true;
                    compt = 1;
                }

                if (left)
                {
                    if (!collisions.CollisionRight(Hitbox, walls, this.speed) && collisions.CollisionDown(new Rectangle(Hitbox.X + Hitbox.Width, Hitbox.Y, Hitbox.Width, Hitbox.Height), walls, speed))
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

                if (!left)
                {
                    if (!collisions.CollisionLeft(Hitbox, walls, this.speed) && Hitbox.X > 0 && collisions.CollisionDown(new Rectangle(Hitbox.X - Hitbox.Width, Hitbox.Y, Hitbox.Width, Hitbox.Height), walls, speed) && Hitbox.X < 4600)
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
            }
            else
            {
                if (playerDistance > 0 && !collisions.CollisionLeft(Hitbox, walls, this.speed))
                {
                    this.Hitbox.X -= speed;
                    this.Direction = Direction.Left;
                }
                else if (playerDistance < 0 &&  !collisions.CollisionRight(Hitbox, walls, this.speed))
                {
                    this.Hitbox.X += speed;
                    this.Direction = Direction.Right;
                }
            }

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
        }

        public void Draw(SpriteBatch spritebatch)
        {
            if (!isDead)
                spritebatch.Draw(Resources.Zombie, Hitbox, new Rectangle((framecolumn - 1) * 37, 0, Hitbox.Width, Hitbox.Height), Color.White, 0f, new Vector2(0, 0), effect, 0f);
        }
    }
}