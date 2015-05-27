using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Map_Generator
{
    class PickUp : Sprite
    {
        enum Type{ hpHeal, hpUp, attackUp, defenseUp };
        Type pickUpType;

        public PickUp(string pickUpType, Vector2 position, Texture2D animationSheet, int width, int height)
        {
            if (pickUpType == "hpHeal") this.pickUpType = Type.hpHeal;
            if (pickUpType == "hpUp") this.pickUpType = Type.hpUp;
            if (pickUpType == "attackUp") this.pickUpType = Type.attackUp;
            if (pickUpType == "defenseUp") this.pickUpType = Type.defenseUp;

            this.position = position;
            this.boundingBox = boundingBox;
            this.animationSheet = animationSheet;
            this.width = width;
            this.height = height;
        }

        public void Upgrade(Player player)
        {
            if (player.boundingBox.Intersects(boundingBox))
            {
                if (pickUpType == Type.hpHeal)
                {
                    player.curHealth++;
                }
                if (pickUpType == Type.hpUp)
                {
                    player.maxHealth++;
                }
                if (pickUpType == Type.attackUp)
                {
                    player.attack++;
                }
                if (pickUpType == Type.defenseUp)
                {
                    player.defense++;
                }
            }
        }
    }
}
