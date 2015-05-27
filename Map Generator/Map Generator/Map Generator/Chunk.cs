﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Map_Generator
{
    public class Chunk
    {
        private int mChunkID;
        private Vector2 mChunkPos;
        private Random mSeededRandom;
        private bool mPlayerSpawn;
        private Texture2D mSpriteSheet;
        private List<Rectangle> mTiles;
        private List<Rectangle> mTileImages;
        private List<Vector2> mEnemySpawnLocations;
        //private List<Sprite> mObstacles;

        private int mChunkDimensions;
        private int mTileDimensions;

        #region GET SETS
        public int ChunkID
        {
            get { return mChunkID; }
            //set { mChunkID = value; }
        }
        public Vector2 ChunkPos
        {
            get { return mChunkPos; }
            set { mChunkPos = value; }
        }
        public Random SeededRandom
        {
            get { return mSeededRandom; }
            //set { mSeededRandom = value; }
        }
        public bool PlayerSpawn
        {
            get { return mPlayerSpawn; }
            set { mPlayerSpawn = value; }
        }
        public Texture2D SpriteSheet
        {
            get { return mSpriteSheet; }
            set { mSpriteSheet = value; }
        }
        public List<Rectangle> Tiles
        {
            get { return mTiles; }
            //set { mTiles = value; }
        }
        public List<Rectangle> TileImages
        {
            get { return mTileImages; }
            //set { mTileImages = value; }
        }
        //public List<Sprite> Obstacles
        //{
        //    get { return mObstacles; }
        //    set { mObstacles = value; }
        //}
        public List<Vector2> EnemySpawnLocations
        {
            get { return mEnemySpawnLocations; }
            set { mEnemySpawnLocations = value; }
        }
        public int ChunkDimensions
        {
            get { return mChunkDimensions; }
            //set { mChunkDimensions = value; }
        }
        public int TileDimensions
        {
            get { return mTileDimensions; }
            //set { mTileDimensions = value; }
        }


        #endregion

        // constructor
        public Chunk(int initialChunkID, Vector2 initialChunkPos, int initialChunkDimensions,
            int initialTileDimensions, Texture2D initialSpriteSheet)
        {
            mChunkID = initialChunkID;
            mChunkPos = initialChunkPos;
            mChunkDimensions = initialChunkDimensions;
            mTileDimensions = initialTileDimensions;
            mSpriteSheet = initialSpriteSheet;

            mTiles = new List<Rectangle>();
            mTileImages = new List<Rectangle>();

            //creats this Chunks random Map_Generator based on it's unique ID.
            mSeededRandom = new Random(initialChunkID);

            // fills list of tiles with rectangles
            for (int x = 0; x < mChunkDimensions; x++)
            {
                for (int y = 0; y < mChunkDimensions; y++)
                {

                    mTiles.Add(new Rectangle(
                        ((int)mChunkPos.X * ChunkDimensions * mTileDimensions) + (x * mTileDimensions),
                        ((int)mChunkPos.Y * ChunkDimensions * mTileDimensions) + (y * mTileDimensions),
                        mTileDimensions,
                        mTileDimensions));
                        
                }
            }

            for (int i = 0; i < mTiles.Count; i++)
            {
                mTileImages.Add(new Rectangle(mSeededRandom.Next(0, 4) * mTileDimensions, 0, mTileDimensions, mTileDimensions));
            }

        //    //Picks random areas to be set as collidables and gives it a
        //    //random wall pic to use.
        //    for (int i = 0; i < wallCount; i++)
        //    {
        //        wallTiles.Add(new Rectangle(rand.Next(40, 600) + (int)chunkPos.X, rand.Next(40, 600) + (int)chunkPos.Y, tileSize, tileSize));
        //        wallImages.Add(new Rectangle(rand.Next(0, 4) * tileSize, tileSize, tileSize, tileSize));
        //    }
        //}

        }

        public void Draw(SpriteBatch spriteBatch, Camera2D camera2D)
        {
            //Draws the background layer
            //for (int i = 0; i < mTiles.Count; i++)
            //{
            //    spriteBatch.Draw(mSpriteSheet, new Vector2(mTiles[i].X, mTiles[i].Y), mTileImages[i], Color.White);
            //}

            for (int i = 0; i < mTiles.Count; i++)
            {
                if (camera2D.ObjectIsVisible(mTiles[i]))
                {
                    spriteBatch.Draw(mSpriteSheet, mTiles[i], mTileImages[i], Color.White);
                }
            }
            //for (int i = 0; i < wallTiles.Count; i++)
            //{
            //    sb.Draw(spriteSheet, new Vector2(wallTiles[i].X, wallTiles[i].Y), wallImages[i], Color.White);
            //}
        }

        public Vector2 ChunkCentre()
        {
            Vector2 chunkCentre = new Vector2(
                mChunkPos.X + ((mChunkDimensions * mTileDimensions) / 2),
                mChunkPos.Y + ((mChunkDimensions * mTileDimensions) / 2));
            return chunkCentre;
        }


    //    Random rand;
    //    public Vector2 chunkPos;
    //    public List<Rectangle> grassTiles;
    //    public List<Rectangle> wallTiles;
    //    List<Rectangle> grassImages;
    //    List<Rectangle> wallImages;
    //    int wallCount = 15;
    //    int tileSize = 32;
    //    Texture2D spriteSheet;
    //    public Rectangle chunkBounds;
    //    public bool playerPresent = false;

    //    int width = 20;
    //    int height = 20;

    //    public void Initialize(Texture2D image, Vector2 pos)
    //    {
    //        rand = new Random();
    //        chunkPos = pos;
    //        spriteSheet = image;
    //        chunkBounds = new Rectangle((int)pos.X, (int)pos.Y, tileSize * width, tileSize * height);

    //        //Makes all tiles into floor as a base layer
    //        grassTiles = new List<Rectangle>();
    //        grassImages = new List<Rectangle>();

    //        wallTiles = new List<Rectangle>();
    //        wallImages = new List<Rectangle>();

    //        CreateAChunk();
    //    }

    //    public void CreateAChunk()
    //    {
    //        for (int x = 0; x < width; x++)
    //        {
    //            for (int y = 0; y < height; y++)
    //            {
    //                grassTiles.Add(new Rectangle((int)(x * tileSize + chunkPos.X), (int)(y * tileSize + chunkPos.Y), tileSize, tileSize));
    //            }
    //        }

    //        //Randomly picks between the 4 floor tiles in the 
    //        //spritesheet and gives all the grass tiles their images.
    //        for (int i = 0; i < grassTiles.Count; i++)
    //        {
    //            grassImages.Add(new Rectangle(rand.Next(0, 4) * tileSize, 0, tileSize, tileSize));
    //        }

    //        //Picks random areas to be set as collidables and gives it a
    //        //random wall pic to use.
    //        for (int i = 0; i < wallCount; i++)
    //        {
    //            wallTiles.Add(new Rectangle(rand.Next(40, 600) + (int)chunkPos.X, rand.Next(40, 600) + (int)chunkPos.Y, tileSize, tileSize));
    //            wallImages.Add(new Rectangle(rand.Next(0, 4) * tileSize, tileSize, tileSize, tileSize));
    //        }
    //    }

    //    public void Draw(SpriteBatch sb)
    //    {
    //        //Draws the background layer
    //        for (int i = 0; i < grassTiles.Count; i++)
    //        {
    //            sb.Draw(spriteSheet, new Vector2(grassTiles[i].X, grassTiles[i].Y), grassImages[i], Color.White);
    //        }
    //        for (int i = 0; i < wallTiles.Count; i++)
    //        {
    //            sb.Draw(spriteSheet, new Vector2(wallTiles[i].X, wallTiles[i].Y), wallImages[i], Color.White);
    //        }
    //    }
    }
}