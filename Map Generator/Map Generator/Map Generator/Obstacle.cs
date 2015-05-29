using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace Map_Generator
{
    public class Obstacle
    {
        private Rectangle mBounds;
        private Rectangle mImage;

        public Rectangle Bounds
        {
            get { return mBounds; }
            set { mBounds = value; }
        }
        public Rectangle Image
        {
            get { return mImage; }
            set { mImage = value; }
        }

        public Obstacle(Rectangle initialBounds, Rectangle initialImage)
        {
            mBounds = initialBounds;
            mImage = initialImage;
        }
    }
}
