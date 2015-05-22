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
        public Vector2 position;
        public Vector2 velocity;
        public Rectangle bounds;
        public Rectangle feet;
        public Texture2D image;
        public int health = 10;
        public Vector2 previousPosition;
        public Vector2 origin;

        public Player(Vector2 position, Texture2D image)
        {
            this.position = position;
            this.bounds = new Rectangle((int)position.X, (int)position.Y, 30, 40);
            this.feet = new Rectangle((int)position.X, (int)position.Y + 10, 30, 30);
            this.image = image;
            velocity = Vector2.Zero;
            origin = new Vector2(bounds.X + bounds.Width / 2, bounds.Y + bounds.Height / 2);
        }

        public void Collisions(List<Rectangle> walls)
        {
            for (int x = 0; x < walls.Count; x++)
            {
                Rectangle PreviousHitBox = new Rectangle((int)previousPosition.X, (int)previousPosition.Y + 10, feet.Width, feet.Height);

                if (feet.Intersects(walls[x]))
                {
                    if (PreviousHitBox.Right <= walls[x].X)
                    {
                        position = previousPosition;
                        feet = new Rectangle((int)position.X, (int)position.Y, feet.Width, feet.Height);
                        velocity.X = 0;
                        position.Y += velocity.Y;
                    }
                    if (PreviousHitBox.Left >= walls[x].X + 30)
                    {
                        position = previousPosition;
                        feet = new Rectangle((int)position.X, (int)position.Y, feet.Width, feet.Height);
                        velocity.X = 0;
                        position.Y += velocity.Y;
                    }
                    if (PreviousHitBox.Bottom <= walls[x].Y)
                    {
                        position = previousPosition;
                        feet = new Rectangle((int)position.X, (int)position.Y, feet.Width, feet.Height);
                        velocity.Y = 0;
                        position.X += velocity.X;
                    }
                    if (PreviousHitBox.Top >= walls[x].Y + 30)
                    {
                        position = previousPosition;
                        feet = new Rectangle((int)position.X, (int)position.Y, feet.Width, feet.Height);
                        velocity.Y = 0;
                        position.X += velocity.X;
                    }
                }

            }
        }

        public void Update(GameTime gameTime, KeyboardState key)
        {
            previousPosition = position;

            if (key.IsKeyDown(Keys.W))
            {
                velocity.Y -= 0.2f;
            }
            if (key.IsKeyDown(Keys.S))
            {
                velocity.Y += 0.2f;
            }
            if (key.IsKeyDown(Keys.A))
            {
                velocity.X -= 0.2f;
            }
            if (key.IsKeyDown(Keys.D))
            {
                velocity.X += 0.2f;
            }

            if (velocity.X > 2)
            {
                velocity.X = 2;
            }
            if (velocity.Y > 2)
            {
                velocity.Y = 2;
            }
            if (velocity.X < -2)
            {
                velocity.X = -2;
            }
            if (velocity.Y < -2)
            {
                velocity.Y = -2;
            }

            position += velocity;

            bounds.X = (int)position.X;
            bounds.Y = (int)position.Y;
            feet.X = (int)position.X;
            feet.Y = (int)position.Y + 10;

            velocity *= 0.98f;
        }

        public void Draw(SpriteBatch sb)
        {
            sb.Draw(image, bounds, Color.White);
        }
    }
}
