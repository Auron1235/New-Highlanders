using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;

namespace Map_Generator
{
    public class EnemyManager
    {
        private List<List<Wolf>> mWolfPacks;
        private Texture2D wolfSheet;

        private List<Bear> mBears;
        private Texture2D bearSheet;

        public List<List<Wolf>> Wolves
        {
            get { return mWolfPacks; }
            set { mWolfPacks = value; }
        }
        public List<Bear> Bears
        {
            get { return mBears; }
            set { mBears = value; }
        }

        public EnemyManager()
        {
        }

        public void Initialize(ContentManager Content)
        {
            wolfSheet = Content.Load<Texture2D>("wolfSpriteSheet");
            bearSheet = Content.Load<Texture2D>("bearSpriteSheet");

            mBears = new List<Bear>();
            mWolfPacks = new List<List<Wolf>>();
        }

        public void Update(GameTime gameTime, Player player1, Player player2, ChunkManager chunkManager, AudioManager audioManager)
        {
            foreach (List<Wolf> wolfPack in mWolfPacks)
            {
                foreach (Wolf wolf in wolfPack)
                {
                    wolf.Update(player1, player2);
                }
            }
            foreach (Bear bear in mBears)
            {
                bear.Update(player1, player2);
            }
        }

        public void GenerateWolves(List<Vector2> positions)
        {
            List<Wolf> wolfPack = new List<Wolf>();
            for (int i = 0; i < positions.Count; i++)
            {
                Wolf spawnedWolf = new Wolf(new Vector2(positions[i].X, positions[i].Y),wolfSheet, 40, 25);
                wolfPack.Add(spawnedWolf);
            }
            mWolfPacks.Add(wolfPack);
        }
        public void GenerateBears(Vector2 position)
        {
            Bear spawnedBear = new Bear(position, bearSheet, 40, 30);
            mBears.Add(spawnedBear);
        }

        public void Draw(SpriteBatch spriteBatch, Camera2D camera2D)
        {
            //Draws Wolves
            List<Wolf> viewableWolves = new List<Wolf>();
            foreach (List<Wolf> wolfPack in mWolfPacks)
            {
                foreach (Wolf wolf in wolfPack)
                {
                    if (camera2D.ObjectIsVisible(wolf.boundingBox))
                    {
                        viewableWolves.Add(wolf);
                    }
                }
            }
            List<Wolf> sortedWolves = new List<Wolf>();
            sortedWolves = viewableWolves.OrderBy(x => x.boundingBox.Bottom).ThenBy(x => x.boundingBox.X).ToList();

            for (int i = 0; i < sortedWolves.Count; i++)
            {
                spriteBatch.Begin(SpriteSortMode.Immediate, BlendState.AlphaBlend, null, null, null, null, camera2D.GetViewPortMatrix);
                sortedWolves[i].Draw(spriteBatch, wolfSheet);
                spriteBatch.End();
            }
            //Draws Bears
            List<Bear> viewableBears = new List<Bear>();
            foreach (Bear bear in mBears)
            {
                if (camera2D.ObjectIsVisible(bear.boundingBox))
                {
                    viewableBears.Add(bear);
                }
            }
            List<Bear> sortedBears = new List<Bear>();
            sortedBears = viewableBears.OrderBy(x => x.boundingBox.Bottom).ThenBy(x => x.boundingBox.X).ToList();

            for (int i = 0; i < sortedBears.Count; i++)
            {
                spriteBatch.Begin(SpriteSortMode.Immediate, BlendState.AlphaBlend, null, null, null, null, camera2D.GetViewPortMatrix);
                sortedBears[i].Draw(spriteBatch, bearSheet);
                spriteBatch.End();
            }
        }
    }
}
