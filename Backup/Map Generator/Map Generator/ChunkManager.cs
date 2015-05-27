﻿using System;
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

        private const int ChunkDimensions = 64;
        private const int TileDimensions = 32;
        private const int LoadedArea = 5;

        private Vector2 mCurFocusChunk;
        private Vector2 mPrevFocusChunk;

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
        public Vector2 CurFocusChunk
        {
            get { return mCurFocusChunk; }
            //set { mCurrentFocusChunk = value; }
        }
        public Vector2 PrevFocusChunk
        {
            get { return mPrevFocusChunk; }
            //set { mPrevFocusChunk = value; }
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
            mPrevFocusChunk = new Vector2(2, 2); //sets start position to the centre of the 5x5 grid we generate.

            for (int x = 0; x < LoadedArea; x++)
            {
                for (int y = 0; y < LoadedArea; y++)
                {
                    CreateChunk(x, y);
                }
            }
        }


        private void CreateChunk(Vector2 chunkPosition)
        {
            CreateChunk((int)chunkPosition.X, (int)chunkPosition.Y);
        }
        private void CreateChunk(int x, int y)
        {
            int chunkSeed = mSeededRandom.Next(0, 999);
            int chunkID = GenerateChunkID(chunkSeed, x, y);

            Chunk chunk = new Chunk(chunkID, new Vector2(x, y), ChunkDimensions, TileDimensions, mSpriteSheet);
            //if (x == 3 && y == 3) chunk.PlayerSpawn = true;
            mChunks.Add(chunk);
        }

        public int GenerateChunkID(int chunkSeed, int chunkXPos, int chunkYPos)
        {
            int generatedID = (chunkSeed * 1000000) + ((chunkXPos + 499) * 1000) + (chunkYPos + 499);

            return generatedID;
        }

        public void Update(Vector2 cameraFocusPoint)
        {
            //gets the current chunk X,Y coordinate
            mCurFocusChunk = GetChunkCoordinate(cameraFocusPoint);

            if (mCurFocusChunk != mPrevFocusChunk)
            {
                Vector2 direction = mPrevFocusChunk - mCurFocusChunk;
                Vector2 dirX = new Vector2(direction.X, 0);
                Vector2 dirY = new Vector2(0, direction.Y);

                //if (dirX.X != 0) LoadUnloadChunks(dirX, mCurFocusChunk);
                //if (dirY.Y != 0) LoadUnloadChunks(dirY, mCurFocusChunk);

                if (dirX.X != 0) LoadUnloadChunksX(direction, mCurFocusChunk);
                if (dirY.Y != 0) LoadUnloadChunksY(direction, mCurFocusChunk);

            }

            mPrevFocusChunk = mCurFocusChunk;
        }

        //public void LoadUnloadChunks(Vector2 direction, Vector2 mCurFocusChunk)
        //{
        //    // extends our direction Vector to be the row/column from source that we will load/unload.
        //    Vector2 loadDir = direction * 3;
        //    Vector2 unloadDir = direction * -3;

        //    //loads chunks
        //    for (int i = 0; i < LoadedArea; i++)
        //    {
        //        Vector2 loadingPos = mCurFocusChunk + direction;
        //        CreateChunk(loadingPos);
        //    }

        //    //unloads chunks
        //    //for (int i = mChunks.Count - 1; i >= 0; i--)
        //    //{
        //    //    if ((mChunks[i].ChunkPos.X == mCurFocusChunk.X + 2)
        //    //        || (mChunks[i].ChunkPos.X == mCurFocusChunk.X - 2)
        //    //            || (mChunks[i].ChunkPos.Y == mCurFocusChunk.Y + 2)
        //    //                || (mChunks[i].ChunkPos.Y == mCurFocusChunk.Y + 2))
        //    //    {

        //    //        mChunks.RemoveAt(i);
        //    //    }

        //    //}


        //    //foreach (Chunk chunk in mChunks)
        //    //{
        //    //    //if 
        //    //}


        //}

        public void LoadUnloadChunksX(Vector2 direction, Vector2 mCurFocusChunk)
        {
            // extends our direction Vector to be the row/column from source that we will load/unload.
            Vector2 loadDir = direction * 3;
            Vector2 unloadDir = direction * -3;

            //loads chunks
            for (int y = 0; y < LoadedArea; y++)
            {
                //Vector2 loadingPos = mCurFocusChunk + direction;
                //CreateChunk(loadingPos);
                CreateChunk(new Vector2(mCurFocusChunk.X + loadDir.X, (mCurFocusChunk.Y - 2) + y));
            }

            ///unloads chunks
            for (int i = mChunks.Count - 1; i >= 0; i--)
            {
                if (mChunks[i].ChunkPos.X == mCurFocusChunk.X + 3)
                {
                    mChunks.RemoveAt(i);
                }
                //if (mChunks[i].ChunkPos.X == mCurFocusChunk.X - 3)
                //{
                //    mChunks.RemoveAt(i);
                //}

            }

            int test = 0;


            //unloads chunks
            //for (int i = mChunks.Count - 1; i >= 0; i--)
            //{
            //    if ((mChunks[i].ChunkPos.X == mCurFocusChunk.X + 2)
            //        || (mChunks[i].ChunkPos.X == mCurFocusChunk.X - 2)
            //            || (mChunks[i].ChunkPos.Y == mCurFocusChunk.Y + 2)
            //                || (mChunks[i].ChunkPos.Y == mCurFocusChunk.Y + 2))
            //    {

            //        mChunks.RemoveAt(i);
            //    }

            //}
        }

        public void LoadUnloadChunksY(Vector2 direction, Vector2 mCurFocusChunk)
        {
            // extends our direction Vector to be the row/column from source that we will load/unload.
            Vector2 loadDir = direction * 3;
            Vector2 unloadDir = direction * -3;

            //loads chunks
            for (int x = 0; x < LoadedArea; x++)
            {
                //Vector2 loadingPos = mCurFocusChunk + direction;
                //CreateChunk(loadingPos);
                CreateChunk(new Vector2(((mCurFocusChunk.X - 2) + x), mCurFocusChunk.Y + loadDir.Y));
            }

            ///unloads chunks
            for (int i = mChunks.Count - 1; i >= 0; i--)
            {
                if (mChunks[i].ChunkPos.Y == mCurFocusChunk.Y + unloadDir.Y) mChunks.RemoveAt(i);
            }




            //unloads chunks
            //for (int i = mChunks.Count - 1; i >= 0; i--)
            //{
            //    if ((mChunks[i].ChunkPos.X == mCurFocusChunk.X + 2)
            //        || (mChunks[i].ChunkPos.X == mCurFocusChunk.X - 2)
            //            || (mChunks[i].ChunkPos.Y == mCurFocusChunk.Y + 2)
            //                || (mChunks[i].ChunkPos.Y == mCurFocusChunk.Y + 2))
            //    {

            //        mChunks.RemoveAt(i);
            //    }

            //}
        }

        public void Draw(SpriteBatch spriteBatch, Camera2D camera2D)
        {
            foreach (Chunk chunk in mChunks)
            {
                chunk.Draw(spriteBatch, camera2D);
            }
        }

        //takes a vector2 position and finds out which chunk it is on.
        public Vector2 GetChunkCoordinate(Vector2 Target)
        {
            Vector2 chunkCoordinate;
            float positionX = (Target.X / (TileDimensions * ChunkDimensions)) - 0.5f;
            float positionY = (Target.Y / (TileDimensions * ChunkDimensions)) - 0.5f;

            positionX = (float)Math.Round(positionX);
            positionY = (float)Math.Round(positionY);

            chunkCoordinate = new Vector2(positionX, positionY);

            return chunkCoordinate;
        }
        public Vector2 FindChunkCentre(Vector2 chunkPos)
        {
            Vector2 desiredCentre = new Vector2(chunkPos.X + 1, chunkPos.Y + 1);
            return desiredCentre * ChunkDimensions * TileDimensions;
        }
    }
}
