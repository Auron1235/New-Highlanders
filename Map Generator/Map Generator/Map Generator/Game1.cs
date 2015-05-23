using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace Map_Generator
{
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        Random rand;

        Texture2D spriteSheet;
        List<Chunks> chunks;

        Camera camera;
        int screenWidth = 1024;
        int screenHeight = 768;

        Player player;
        Texture2D playerPic;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }

        protected override void Initialize()
        {
            rand = new Random();
            spriteSheet = Content.Load<Texture2D>("SpriteSheet");

            camera = new Camera();
            graphics.PreferredBackBufferWidth = screenWidth;
            graphics.PreferredBackBufferHeight = screenHeight;
            graphics.ApplyChanges();

            chunks = new List<Chunks>();

            playerPic = Content.Load<Texture2D>("playerPic");
            player = new Player(new Vector2(screenWidth/2, screenHeight/2), playerPic);

            chunks.Add(new Chunks());
            chunks[0].Initialize(spriteSheet, new Vector2(0, 0));
            chunks[0].playerPresent = true;

            chunks.Add(new Chunks());
            chunks[1].Initialize(spriteSheet, new Vector2(-640, -640));
            chunks[1].playerPresent = true;

            chunks.Add(new Chunks());
            chunks[2].Initialize(spriteSheet, new Vector2(0, -640));
            chunks[2].playerPresent = true;

            chunks.Add(new Chunks());
            chunks[3].Initialize(spriteSheet, new Vector2(640, -640));
            chunks[3].playerPresent = true;

            chunks.Add(new Chunks());
            chunks[4].Initialize(spriteSheet, new Vector2(-640, 0));
            chunks[4].playerPresent = true;

            chunks.Add(new Chunks());
            chunks[5].Initialize(spriteSheet, new Vector2(640, 0));
            chunks[5].playerPresent = true;

            chunks.Add(new Chunks());
            chunks[6].Initialize(spriteSheet, new Vector2(-640, 640));
            chunks[6].playerPresent = true;

            chunks.Add(new Chunks());
            chunks[7].Initialize(spriteSheet, new Vector2(0, 640));
            chunks[7].playerPresent = true;

            chunks.Add(new Chunks());
            chunks[8].Initialize(spriteSheet, new Vector2(640, 640));
            chunks[8].playerPresent = true;

            base.Initialize();
        }

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);
        }

        protected override void UnloadContent()
        {
        }

        protected override void Update(GameTime gameTime)
        {
            player.Update(gameTime, Keyboard.GetState(), camera);

            for (int i = 0; i < chunks.Count; i++)
            {
                player.Collisions(chunks[i].wallTiles);
            }

            camera.Move(-player.velocity);
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            spriteBatch.Begin(0, null, null, null, null, null, camera.getTransformation());
            foreach (Chunks chunk in chunks)
            {
                chunk.Draw(spriteBatch);
            }
            spriteBatch.End();

            spriteBatch.Begin();
            player.Draw(spriteBatch);
            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
