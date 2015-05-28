using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Map_Generator
{
    class Player : Sprite
    {
        public Vector2 previousPosition;

        public Rectangle boundingFootPrint;

        private const float speedMax = 1.9f;
        private const float speedIncrement = 0.25f;
        public bool attacking;

        public Player(Vector2 position, Texture2D animationSheet, int width, int height)
        {
            this.position = position;
            this.animationSheet = animationSheet;
            this.boundingBox = new Rectangle((int)position.X, (int)position.Y, width * 2, height * 2);
            this.boundingFootPrint = new Rectangle((int)position.X, (int)position.Y + 10, width, height);
            velocity = Vector2.Zero;
            origin = new Vector2(boundingBox.X + boundingBox.Width / 2, boundingBox.Y + boundingBox.Height / 2);

            this.width = width;
            this.height = height;

            maxHealth = 10;
            curHealth = 10;
            attack = 2;
            defense = 2;
            attacking = false;

            animationType = 2;
            spriteEffect = SpriteEffects.None;
        }

        public void Collisions(List<Rectangle> walls)
        {
            for (int x = 0; x < walls.Count; x++)
            {
                Rectangle PreviousHitBox = new Rectangle((int)previousPosition.X, (int)previousPosition.Y + 10, boundingFootPrint.Width, boundingFootPrint.Height);

                //the plus 32's on the two statements below are for tileWidth and tileHeight.
                //This will need updated if there is time to a refernced value rather than hardcoding.
                if (boundingFootPrint.Intersects(walls[x]))
                {
                    if (PreviousHitBox.Right <= walls[x].X)
                    {
                        position = previousPosition;
                        boundingFootPrint = new Rectangle((int)position.X, (int)position.Y, boundingFootPrint.Width, boundingFootPrint.Height);
                        velocity.X = 0;
                        position.Y += velocity.Y;
                    }
                    if (PreviousHitBox.Left >= walls[x].X + 32)
                    {
                        position = previousPosition;
                        boundingFootPrint = new Rectangle((int)position.X, (int)position.Y, boundingFootPrint.Width, boundingFootPrint.Height);
                        velocity.X = 0;
                        position.Y += velocity.Y;
                    }
                    if (PreviousHitBox.Bottom <= walls[x].Y)
                    {
                        position = previousPosition;
                        boundingFootPrint = new Rectangle((int)position.X, (int)position.Y, boundingFootPrint.Width, boundingFootPrint.Height);
                        velocity.Y = 0;
                        position.X += velocity.X;
                    }
                    if (PreviousHitBox.Top >= walls[x].Y + 32)
                    {
                        position = previousPosition;
                        boundingFootPrint = new Rectangle((int)position.X, (int)position.Y, boundingFootPrint.Width, boundingFootPrint.Height);
                        velocity.Y = 0;
                        position.X += velocity.X;
                    }
                }
            }
        }

        public void Update(GameTime gameTime, KeyboardState curKeyState, Camera2D camera, List<Wolf> wolves, List<Bear> bears)
        {
            previousPosition = position;

            //player movement, input processing
            if (curKeyState.IsKeyDown(Keys.W)) velocity.Y -= speedIncrement;
            if (curKeyState.IsKeyDown(Keys.S)) velocity.Y += speedIncrement;
            if (curKeyState.IsKeyDown(Keys.A)) velocity.X -= speedIncrement;
            if (curKeyState.IsKeyDown(Keys.D)) velocity.X += speedIncrement;

            //limit player speed
            if (velocity.X > speedMax) velocity.X = speedMax;
            if (velocity.Y > speedMax) velocity.Y = speedMax;
            if (velocity.X < -speedMax) velocity.X = -speedMax;
            if (velocity.Y < -speedMax) velocity.Y = -speedMax;

            //move player
            position += velocity;

            //bounding boxes position being update each frame 
            boundingBox.X = (int)position.X;
            boundingBox.Y = (int)position.Y;
            boundingFootPrint.X = (int)position.X;
            boundingFootPrint.Y = (int)position.Y + 10;

            //player friction, will slow player to a stop.
            velocity *= 0.89f;

            if (curKeyState.IsKeyDown(Keys.D))
            {
                animationFrame++;
                spriteEffect = SpriteEffects.None;
            }
            if (curKeyState.IsKeyDown(Keys.A))
            {
                animationFrame++;
                spriteEffect = SpriteEffects.FlipHorizontally;
            }
            if (curKeyState.IsKeyDown(Keys.W) || curKeyState.IsKeyDown(Keys.S))
            {
                
                animationFrame++;
                animationType = 2;
            }

            if (curKeyState.IsKeyDown(Keys.E) || attacking == true)
            {
                Attack(wolves, bears, gameTime);
            }

            //Updates the animation every frame
            //animationFrame++;
            if (animationFrame >= 11)
            {
                animationFrame = 0;
            }
        }

        public void Attack(List<Wolf> wolves, List<Bear> bears, GameTime gameTime)
        {
            if (attacking == false)
            {
                animationFrame = 0;
            }
            attacking = true;
            animationType = 0;
            if (attacking == true)
            {
                animationFrame++;
            }
            if (animationFrame >= 11)
            {
                velocity *= 0.8f;
                Rectangle attackBox = new Rectangle((int)position.X + width / 2, (int)position.Y, 20, height);
                for (int i = 0; i < wolves.Count; i++)
                {
                    if (wolves[i].boundingBox.Intersects(boundingBox))
                    {
                        wolves[i].curHealth--;
                    }
                }
                for (int i = 0; i < bears.Count; i++)
                {
                    if (bears[i].boundingBox.Intersects(boundingBox))
                    {
                        bears[i].curHealth--;
                    }
                }
                animationType = 2;
                attacking = false;
            }
        }

        public void NewDraw(SpriteBatch spritebatch)
        {
            if (animationType == 0)
            {
                spritebatch.Draw(animationSheet, boundingBox, new Rectangle(width * animationFrame, height * animationType, width, height), Color.White, 0.0f, new Vector2(0, 0), spriteEffect, 0.0f);
            }
            else
            {
                spritebatch.Draw(animationSheet, boundingBox, new Rectangle(width * animationFrame, (height * animationType) - 5, width, height - 7), Color.White, 0.0f, new Vector2(0, 0), spriteEffect, 0.0f);
            }
        }
    }
}
