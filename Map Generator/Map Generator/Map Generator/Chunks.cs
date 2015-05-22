using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Map_Generator
{
    public class Chunks
    {
        Random rand;
        public Vector2 chunkPos;
        public List<Rectangle> grassTiles;
        public List<Rectangle> wallTiles;
        List<Rectangle> grassImages;
        List<Rectangle> wallImages;
        int wallCount = 15;
        int tileSize = 32;
        Texture2D spriteSheet;
        public Rectangle chunkBounds;
        public bool playerPresent = false;

        int width = 20;
        int height = 20;

        public Chunks(Texture2D image, Vector2 pos)
        {
            rand = new Random();
            chunkPos = pos;
            spriteSheet = image;
            chunkBounds = new Rectangle((int)pos.X - 640, (int)pos.Y - 640, tileSize * width * 3, tileSize * height * 3);

            //Makes all tiles into floor as a base layer
            grassTiles = new List<Rectangle>();
            grassImages = new List<Rectangle>();

            wallTiles = new List<Rectangle>();
            wallImages = new List<Rectangle>();

            CreateAChunk();
        }

        public void CreateAChunk()
        {
            for (int x = 0; x < width; x++)
            {
                for (int y = 0; y < height; y++)
                {
                    grassTiles.Add(new Rectangle((int)(x * tileSize + chunkPos.X), (int)(y * tileSize + chunkPos.Y), tileSize, tileSize));
                }
            }

            //Randomly picks between the 4 floor tiles in the 
            //spritesheet and gives all the grass tiles their images.
            for (int i = 0; i < grassTiles.Count; i++)
            {
                grassImages.Add(new Rectangle(rand.Next(0, 4) * tileSize, 0, tileSize, tileSize));
            }

            //Picks random areas to be set as collidables and gives it a
            //random wall pic to use.
            for (int i = 0; i < wallCount; i++)
            {
                wallTiles.Add(new Rectangle(rand.Next(40, 600) + (int)chunkPos.X, rand.Next(40, 600) + (int)chunkPos.Y, tileSize, tileSize));
                wallImages.Add(new Rectangle(rand.Next(0, 4) * tileSize, tileSize, tileSize, tileSize));
            }
        }

        public void Draw(SpriteBatch sb)
        {
            //Draws the background layer
            for (int i = 0; i < grassTiles.Count; i++)
            {
                sb.Draw(spriteSheet, new Vector2(grassTiles[i].X, grassTiles[i].Y), grassImages[i], Color.White);
            }
            for (int i = 0; i < wallTiles.Count; i++)
            {
                sb.Draw(spriteSheet, new Vector2(wallTiles[i].X, wallTiles[i].Y), wallImages[i], Color.White);
            }
        }
    }
}
