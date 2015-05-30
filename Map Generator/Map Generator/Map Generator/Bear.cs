using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Map_Generator
{
    public class Bear : Sprite
    {
        public Player target;

        int senseRange;
        int attackRange;

        public Bear(Vector2 position, Rectangle boundingBox, Texture2D animationSheet, int width, int height)
        {
            this.position = position;
            this.boundingBox = boundingBox;
            this.animationSheet = animationSheet;
            senseRange = 100;
            attackRange = 20;
            this.width = width;
            this.height = height;

            maxHealth = 20;
            curHealth = 20;
            attack = 4;
        }

        public void Update(Player player1, Player player2)
        {
            if (Vector2.Distance(player1.position, position) < Vector2.Distance(player2.position, position) && Vector2.Distance(player1.position, position) < senseRange)
            {
                target = player1;
                velocity = target.position - position;
                velocity.Normalize();
                velocity *= 2;
            }
            if (Vector2.Distance(player2.position, position) < Vector2.Distance(player1.position, position) && Vector2.Distance(player2.position, position) < senseRange)
            {
                target = player2;
                velocity = target.position - position;
                velocity.Normalize();
                velocity *= 2;
            }
            if (Vector2.Distance(target.position, position) < attackRange)
            {
                if (Attack(target)) target.curHealth--;
            }
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
