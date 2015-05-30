using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Map_Generator
{
    class EnemyManager
    {
        private List<Sprite> mEnemies;

        public List<Sprite> Enemies
        {
            get { return mEnemies; }
            set { mEnemies = value; }
        }

        public EnemyManager(Texture2D animationSheet)
        {
            mEnemies = new List<Sprite>();
        }
    }
}
