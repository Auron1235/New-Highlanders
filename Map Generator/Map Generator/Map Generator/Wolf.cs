using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Map_Generator
{
    public class Wolf : Sprite
    {
        private Player target;

        int senseRange;
        int attackRange;

        public Wolf(Vector2 position, Texture2D animationSheet, int width, int height)
        {
            this.position = position;
            this.width = width;
            this.height = height;
            this.boundingBox = new Rectangle((int)position.X, (int)position.Y, width, height);
            this.animationSheet = animationSheet;

            senseRange = 150;
            attackRange = 10;

            maxHealth = 8;
            curHealth = 8;
            attack = 2;
        }

        public void Update(Player player1, Player player2)
        {
            if (Vector2.Distance(player1.position, position) < Vector2.Distance(player2.position, position) && Vector2.Distance(player1.position, position) < senseRange)
            {
                target = player1;
                velocity = target.position - position;
                velocity.Normalize();
                velocity *= 3;
            }
            if (Vector2.Distance(player2.position, position) < Vector2.Distance(player1.position, position) && Vector2.Distance(player2.position, position) < senseRange)
            {
                target = player2;
                velocity = target.position - position;
                velocity.Normalize();
                velocity *= 3;
            }
            if (Vector2.Distance(target.position, position) < attackRange)
            {
                if (Attack(target)) target.curHealth--;
            }
            boundingBox = new Rectangle((int)position.X, (int)position.Y, width, height);
        }

        bool Attack(Player target)
        {
            velocity = Vector2.Zero;
            Rectangle attackBox = Rectangle.Empty;
            if (target.position.X > position.X + width / 2) attackBox = new Rectangle((int)position.X + width / 2, (int)position.Y, 20, height);
            if (target.position.X < position.X + width / 2) attackBox = new Rectangle((int)position.X + width / 2 - 20, (int)position.Y, 20, height);
            if (attackBox.Intersects(target.boundingBox))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
