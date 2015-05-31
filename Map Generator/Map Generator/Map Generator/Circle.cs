using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace Map_Generator
{
    public struct Circle
    {
        public Vector2 Center { get; set; }
        public float Radius { get; set; }

        public Circle(Vector2 center, float radius)
        {
            Center = center;
            Radius = radius;
        }

        public bool Contains(Vector2 point)
        {
            return ((point - Center).Length() <= Radius);
        }

        public bool Intersects(Circle other)
        {
            return ((other.Center - Center).Length() < (other.Radius - Radius));
        }
    }
}
