using System;
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
        private List<Rectangle> mObstacles;
        private List<Rectangle> mObstacleImages;
        private List<Rectangle> mObsRectangles;
        private int mObsIslandDensity; // amount of islands of obstacles that appear on a chunk
        private int mObsDensity; //amount of items making up the islands

        private int mChunkDimensions;
        private int mTileDimensions;

        private Rectangle mChunkRectangle;

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
        public List<Rectangle> Obstacles
        {
            get { return mObstacles; }
            set { mObstacles = value; }
        }
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
        public Rectangle ChunkRectangle
        {
            get { return mChunkRectangle; }
            //set { mChunkRectangle = value; }
        }
        public int ObsDensity
        {
            get { return mObsIslandDensity; }
            //set { mObsDensity = value; }
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
            mChunkRectangle = new Rectangle(
                (int)(mChunkPos.X * ChunkDimensions * mTileDimensions),
                (int)(mChunkPos.Y * ChunkDimensions * mTileDimensions),
                TileDimensions * ChunkDimensions, 
                TileDimensions * ChunkDimensions);
            mSpriteSheet = initialSpriteSheet;

            mTiles = new List<Rectangle>();
            mTileImages = new List<Rectangle>();

            mObstacles = new List<Rectangle>();
            mObstacleImages = new List<Rectangle>();
            mObsIslandDensity = 10;
            mObsDensity = 50;

            mObsRectangles = new List<Rectangle>();
            mObsRectangles.Add(new Rectangle(0, 32, 32, 32)); //heather, 0
            mObsRectangles.Add(new Rectangle(32, 32, 32, 32)); //rock, 1
            mObsRectangles.Add(new Rectangle(0, 64, 35, 65)); //tree, 2 -- 35x65 is normal size.

            //creats this Chunks random Map_Generator based on it's unique ID.
            mSeededRandom = new Random(initialChunkID);

            // fills list of tiles with rectangles
            for (int x = 0; x < mChunkDimensions; x++)
            {
                for (int y = 0; y < mChunkDimensions; y++)
                {

                    mTiles.Add(new Rectangle(
                        mChunkRectangle.X + (x * mTileDimensions),
                        mChunkRectangle.Y + (y * mTileDimensions),
                        //((int)mChunkPos.X * ChunkDimensions * mTileDimensions) + (x * mTileDimensions),
                        //((int)mChunkPos.Y * ChunkDimensions * mTileDimensions) + (y * mTileDimensions),
                        mTileDimensions,
                        mTileDimensions));
                        
                }
            }
            //and adds thier grass images
            for (int i = 0; i < mTiles.Count; i++)
            {
                mTileImages.Add(new Rectangle(mSeededRandom.Next(0, 8) * mTileDimensions, 0, mTileDimensions, mTileDimensions));
            }

            //Picks random areas to be set as collidables and gives it a random wall pic to use.
            GenerateObsSpawns();
        }

        public void DrawGround(SpriteBatch spriteBatch, Camera2D camera2D)
        {
            for (int i = 0; i < mTiles.Count; i++)
            {
                if (camera2D.ObjectIsVisible(mTiles[i]))
                {
                    spriteBatch.Draw(mSpriteSheet, mTiles[i], mTileImages[i], Color.White);
                }
            }
        }

        public void DrawObstacles(SpriteBatch spriteBatch, Camera2D Camera2D)
        {
            //obstacle drawing
            for (int i = 0; i < mObstacles.Count; i++)
            {
                spriteBatch.Draw(mSpriteSheet, new Vector2(mObstacles[i].X, mObstacles[i].Y), mObstacleImages[i], Color.White);
                //spriteBatch.Draw(mSpriteSheet, new Vector2(mObstacles[i].X, mObstacles[i].Y), mObstacleImages[i], Color.White, 0.0f, Vector2.Zero, 1.0f, SpriteEffects.None, (float)(mObstacles[i].Y / Camera2D.ViewPortRect.Height));
            }
        }

        public Vector2 ChunkCentre()
        {
            Vector2 chunkCentre = new Vector2(
                mChunkPos.X + ((mChunkDimensions * mTileDimensions) / 2),
                mChunkPos.Y + ((mChunkDimensions * mTileDimensions) / 2));
            return chunkCentre;
        }

        public void GenerateObsSpawns()
        {
            List<Vector2> spawnLocations = new List<Vector2>();
            List<Rectangle> mObstaclesUnsorted = new List<Rectangle>();

            //selects a random density of islands to be placed based on the density value of the chunk.
            for (int islandCount = 0; islandCount < mObsIslandDensity; islandCount++) //DEBUG replace 250 with islandDensity
            {
                //generates a new spawn location inside the bounds of the Chunk and adds to spawn location list.
                Vector2 newspawn = new Vector2(
                    mSeededRandom.Next(mChunkRectangle.X, (mChunkRectangle.X + mChunkRectangle.Width)),
                    mSeededRandom.Next(mChunkRectangle.Y, (mChunkRectangle.Y + mChunkRectangle.Height)));
                spawnLocations.Add(newspawn);
             }
            //creates a set of radial points from each location to fluff out the area.
            for (int objectCount = spawnLocations.Count; objectCount > 0; objectCount--)
            {
                int radialPoints = mSeededRandom.Next(5, mObsDensity);

                for (int i = 0; i <= radialPoints; i++)
                {
                    //generate distance in pixels.
                    int distance = mSeededRandom.Next(35, 150);

                    //generate angle
                    Vector2 angle = new Vector2((float)Math.Cos((mSeededRandom.Next(-314, 314) / 100)), (float)Math.Sin((mSeededRandom.Next(-314, 314) / 100)));
                    
                    //add the angle * distance to the span point for a new vector.
                    spawnLocations.Add(spawnLocations[objectCount - 1] + angle * distance); //add to the list.
                }
            }
            //spawnLocations.Sort((X, Y) => (X.Y.CompareTo(Y.Y)));

            //creates something at each of the spawns.
            foreach (Vector2 spawn in spawnLocations)
            {
                int i = mSeededRandom.Next(0, 3);

                mObstaclesUnsorted.Add(new Rectangle((int)spawn.X, (int)spawn.Y, mObsRectangles[i].Width, mObsRectangles[i].Height));
                mObstacleImages.Add(new Rectangle(mObsRectangles[i].X, mObsRectangles[i].Y, mObsRectangles[i].Width, mObsRectangles[i].Height));
            }
            mObstaclesUnsorted.Sort((X, Y) => (X.Bottom.CompareTo(Y.Bottom))); // sorts the list against the bottom of thier rectangles for Drawing.
            
            //List<Rectangle>() a;
            //List<Rectangle> b = a.OrderBy(x => x.x).ThenBy(x => x.y).ToList();
            //List<Rectangle>() a;
            mObstacles = mObstaclesUnsorted.OrderBy(x => x.Right).ThenBy(x => x.Bottom).ToList();
            mObstacles.Reverse();
        }
    }
}
