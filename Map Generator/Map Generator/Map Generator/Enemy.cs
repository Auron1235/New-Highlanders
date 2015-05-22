using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Map_Generator
{
    class Enemy : Sprite
    {
        public bool isAgroed = false;
        private Random Rng = new Random();
        public float speed = 1.0f;

        public void Update()
        {
            position += velocity;
        }

    }
}
