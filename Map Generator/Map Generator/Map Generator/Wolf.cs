using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Map_Generator
{
    class Wolf : Enemy
    {

        //DO AS YOU PLEASE IF YOU THINK IT WILL WORK BETTER


        public Vector2 previousPosition;
        public Vector2 origin;

        public Wolf(Vector2 position, Texture2D image, int wolfWidth, int wolfHeight)
        {
            this.bounds = new Rectangle((int)position.X, (int)position.Y, wolfWidth, wolfHeight);
            this.feet = new Rectangle((int)position.X, (int)position.Y + 10, 30, 30);
            this.position = position;
            this.image = image;
            velocity = Vector2.Zero;
            origin = new Vector2(bounds.X + bounds.Width / 2, bounds.Y + bounds.Height / 2);
        }


        //OLD COLLISION CODE, NEEDS REPLACING
        public void Collisions(List<bool> isWall, List<Rectangle> walls)
        {
            for (int x = 0; x < walls.Count; x++)
            {
                Rectangle PreviousHitBox = new Rectangle((int)previousPosition.X, (int)previousPosition.Y + 10, feet.Width, feet.Height);

                if (feet.Intersects(walls[x]) && isWall[x] == true)
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
        public void Update(GameTime gameTime, Player player)
        {
            Vector2 move = Vector2.Subtract(player.position, position);
            previousPosition = position;
            position += velocity;
            velocity = (Vector2.Subtract(player.position, position)) * speed * 0.03f;


            bounds.X = (int)position.X;
            bounds.Y = (int)position.Y;


        }
    }
}
