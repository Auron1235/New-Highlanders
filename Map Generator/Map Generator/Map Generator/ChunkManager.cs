using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace Map_Generator
{
    class ChunkManager
    {
        private List<Chunk> mChunks;
        private Random mSeededRandom;
        private int mSeed;
        private Texture2D mSpriteSheet;

        private const int chunkDimensions = 64;
        private const int tileDimensions = 32;

        #region GET SETS
        public List<Chunk> Chunks
        {
            get { return mChunks; }
            set { mChunks = value; }
        }

        public Random SeededRandom
        {
            get { return mSeededRandom; }
            set { mSeededRandom = value; }
        }

        public int Seed
        {
            get { return mSeed; }
            set { mSeed = value; }
        }

        public Texture2D SpriteSheet
        {
            get { return mSpriteSheet; }
            set { mSpriteSheet = value; }
        }
        
        #endregion

        //constructor
        public ChunkManager()
        {
            mChunks = new List<Chunk>();
        }

        public void Initialize(int initialSeed, Texture2D initialSpritesheet)
        {
            int mSeed = initialSeed;
            mSeededRandom = new Random(mSeed);
            mSpriteSheet = initialSpritesheet;

            for (int x = 0; x < 5; x++)
            {
                for (int y = 0; y < 5; y++)
                {
                    int chunkSeed = mSeededRandom.Next(0, 999);
                    int chunkID = GenerateChunkID(chunkSeed, x, y);

                    Chunk chunk = new Chunk(chunkID, new Vector2(x, y), chunkDimensions, tileDimensions, mSpriteSheet);
                    if (x == 3 && y == 3) chunk.PlayerSpawn = true;
                    mChunks.Add(chunk);
                }
            }
        }

        public int GenerateChunkID(int chunkSeed, int chunkXPos, int chunkYPos)
        {
            int generatedID = (chunkSeed * 1000000) + ((chunkXPos + 499) * 1000) + (chunkYPos + 499);

            return generatedID;
        }

        public void Draw(SpriteBatch spriteBatch, Camera2D camera2D)
        {
            foreach (Chunk chunk in mChunks)
            {
                chunk.Draw(spriteBatch, camera2D);
            }
        }

    }
}
