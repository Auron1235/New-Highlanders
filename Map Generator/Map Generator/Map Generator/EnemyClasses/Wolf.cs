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
            this.boundingBox = new Rectangle((int)position.X, (int)position.Y, wolfWidth, wolfHeight);
            this.boundingFootprint = new Rectangle((int)position.X, (int)position.Y + 10, 30, 30);
            this.position = position;
            this.image = image;
            velocity = Vector2.Zero;
            origin = new Vector2(boundingBox.X + boundingBox.Width / 2, boundingBox.Y + boundingBox.Height / 2);
        }


        //OLD COLLISION CODE, NEEDS REPLACING
        public void Collisions(List<bool> isWall, List<Rectangle> walls)
        {
            for (int x = 0; x < walls.Count; x++)
            {
                Rectangle PreviousHitBox = new Rectangle((int)previousPosition.X, (int)previousPosition.Y + 10, boundingFootprint.Width, boundingFootprint.Height);

                if (boundingFootprint.Intersects(walls[x]) && isWall[x] == true)
                {
                    if (PreviousHitBox.Right <= walls[x].X)
                    {
                        position = previousPosition;
                        boundingFootprint = new Rectangle((int)position.X, (int)position.Y, boundingFootprint.Width, boundingFootprint.Height);
                        velocity.X = 0;
                        position.Y += velocity.Y;
                    }
                    if (PreviousHitBox.Left >= walls[x].X + 30)
                    {
                        position = previousPosition;
                        boundingFootprint = new Rectangle((int)position.X, (int)position.Y, boundingFootprint.Width, boundingFootprint.Height);
                        velocity.X = 0;
                        position.Y += velocity.Y;
                    }
                    if (PreviousHitBox.Bottom <= walls[x].Y)
                    {
                        position = previousPosition;
                        boundingFootprint = new Rectangle((int)position.X, (int)position.Y, boundingFootprint.Width, boundingFootprint.Height);
                        velocity.Y = 0;
                        position.X += velocity.X;
                    }
                    if (PreviousHitBox.Top >= walls[x].Y + 30)
                    {
                        position = previousPosition;
                        boundingFootprint = new Rectangle((int)position.X, (int)position.Y, boundingFootprint.Width, boundingFootprint.Height);
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


            boundingBox.X = (int)position.X;
            boundingBox.Y = (int)position.Y;


        }
    }
}
