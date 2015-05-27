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

        AudioManager audioManager;
        ChunkManager chunkManager;
        Camera2D m_camera;

        Texture2D spriteSheet;

        int screenWidth = 1024;
        int screenHeight = 768;

        Player player;
        Texture2D playerPic;

        Settings settings;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }

        protected override void Initialize()
        {
            rand = new Random();
            spriteSheet = Content.Load<Texture2D>("SpriteSheet");

            m_camera = new Camera2D(0, 0, screenWidth, screenHeight);

            graphics.PreferredBackBufferWidth = screenWidth;
            graphics.PreferredBackBufferHeight = screenHeight;
            graphics.ApplyChanges();

            audioManager = new AudioManager();
            audioManager.Initialize(Content);

            settings = new Settings();

            playerPic = Content.Load<Texture2D>("playerPic");
            player = new Player(new Vector2(screenWidth/2, screenHeight/2), playerPic);

            //new chunk code
            chunkManager = new ChunkManager();
            chunkManager.Initialize(499, spriteSheet); //TODO - DEBUG - replace "499" with the elapsed milliseconds at time of entring gameplay.

            //debug player positioning code
            for (int i = 0; i < chunkManager.Chunks.Count; i++)
            {
                if (chunkManager.Chunks[i].PlayerSpawn == true)
                {
                    player.position = chunkManager.Chunks[i].ChunkCentre();
                    m_camera.Target = player.position;
                    break;
                }
            }

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
            if (Keyboard.GetState().IsKeyDown(Keys.Escape)) this.Exit();
            player.Update(gameTime, Keyboard.GetState());

            m_camera.Target = player.position;

            //if (GamePad.GetState(PlayerIndex.One).Buttons.Start == ButtonState.Pressed)
            //{                
            //    settings.MusicSettings(audioManager);
            //}
            if (Keyboard.GetState().IsKeyDown(Keys.Enter))
            {               
                int testRand = rand.Next(0, 9);                               
                audioManager.PlaySfx(audioManager.mFootSteps[testRand]);                               
            }
            if (Keyboard.GetState().IsKeyDown(Keys.P))
            {
                int testRand = rand.Next(0,35);
                audioManager.PlaySfx(audioManager.mHitSounds[testRand]);
            }
            if (Keyboard.GetState().IsKeyDown(Keys.Pause))
            {
                int testRand = rand.Next(0,25);
                audioManager.PlaySfx(audioManager.mSwooshSounds[testRand]);

            }
            if (Keyboard.GetState().IsKeyDown(Keys.V))
            {                
                audioManager.PlaySfx(audioManager.mMenuBack);
            }
            if (Keyboard.GetState().IsKeyDown(Keys.B))
            {
                audioManager.PlaySfx(audioManager.mMenuSelect);
            }

            //for (int i = 0; i < chunks.Count; i++)
            //{
            //    player.Collisions(chunks[i].wallTiles);
            //}

            //TODO DEBUG - Only draw what is on the screen.
            m_camera.UpdateViewPort(new Vector2(0,0));
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            spriteBatch.Begin(SpriteSortMode.BackToFront,
                        BlendState.AlphaBlend,
                        null,
                        null,
                        null,
                        null,
                        m_camera.GetViewPortMatrix);

            chunkManager.Draw(spriteBatch, m_camera);

            spriteBatch.End();


            spriteBatch.Begin(SpriteSortMode.BackToFront,
                        BlendState.AlphaBlend,
                        null,
                        null,
                        null,
                        null,
                        m_camera.GetViewPortMatrix);
            player.Draw(spriteBatch);
            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
