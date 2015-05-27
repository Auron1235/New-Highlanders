using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Map_Generator
{
    class Wolf
    {
        public Vector2 pos;
        public Rectangle bounds;
        Texture2D image;

        public void Initialize(Vector2 pos, Rectangle bounds, Texture2D image)
        {
            this.pos = pos;
            this.bounds = bounds;
            this.image = image;
        }

    }
}
