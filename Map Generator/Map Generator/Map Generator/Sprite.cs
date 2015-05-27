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
        public Vector2 origin;
        public Vector2 velocity;

        public Rectangle boundingBox;

        public Texture2D animationSheet;
        public int animationFrame = 0;
        public int animationType = 0;
        public SpriteEffects spriteEffect;

        public int maxHealth;
        public int curHealth;
        public int attack;
        public int defense;

        public int width;
        public int height;

        public void Update()
        {
            //Updates the animation every frame
            animationFrame++;
            if (animationFrame >= 12) animationFrame = 0;
        }

        public void Draw(SpriteBatch spritebatch)
        {
            spritebatch.Draw(animationSheet, boundingBox, new Rectangle(width * animationFrame, height * animationType, width, height), Color.White);
        }
    }
}
