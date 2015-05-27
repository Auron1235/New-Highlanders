using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Map_Generator
{
    class Player
    {
        public Sprite sprite;

        public Vector2 position;
        public Vector2 velocity;
        public Rectangle boundingBox;
        public Rectangle boundingFootPrint;
        public Texture2D image;
        public int health = 10;
        public Vector2 previousPosition;
        public Vector2 origin;

        private const float speedMax = 1.9f;
        private const float speedIncrement = 0.25f;

        public Player(Vector2 position, Texture2D image)
        {
            this.position = position;
            this.boundingBox = new Rectangle((int)position.X, (int)position.Y, image.Width, image.Height);
            this.boundingFootPrint = new Rectangle((int)position.X, (int)position.Y + 10, image.Width, image.Height);
            this.image = image;
            velocity = Vector2.Zero;
            origin = new Vector2(boundingBox.X + boundingBox.Width / 2, boundingBox.Y + boundingBox.Height / 2);
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

        public void Update(GameTime gameTime, KeyboardState curKeyState)
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

            //bounding - ADAM TO COMMENT
            boundingBox.X = (int)position.X;
            boundingBox.Y = (int)position.Y;
            boundingFootPrint.X = (int)position.X;
            boundingFootPrint.Y = (int)position.Y + 10;

            //player friction, will slow player to a stop.
            velocity *= 0.89f;
        }

        public void Draw(SpriteBatch spritebatch)
        {
            spritebatch.Draw(image, boundingBox, Color.White);
        }
    }
}
