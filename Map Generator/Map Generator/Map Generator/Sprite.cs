using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Map_Generator
{
    class Sprite
    {
        public Vector2 position;
        public Vector2 velocity;
        public Rectangle bounds;
        public Rectangle feet;
        public Texture2D image;
        public int health = 10;

        public void Draw(SpriteBatch sb)
        {
            sb.Draw(image, bounds, Color.White);
        }

        public void BasicAttack()
        {

        }
    }
}
