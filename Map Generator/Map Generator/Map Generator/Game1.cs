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
        SpriteFont titleFont;
        SpriteFont smallFont;
        KeyboardState oldKState;
        KeyboardState newKState;
        Random rand;

        enum currentScreen { splashScreen, menuScreen, settingsScreen, playerScreen, gameplayScreen, pauseScreen, completeScreen, gameoverScreen };
        currentScreen screen = new currentScreen();
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
            screen = currentScreen.splashScreen;
            oldKState = Keyboard.GetState();
            camera = new Camera();
            graphics.PreferredBackBufferWidth = screenWidth;
            graphics.PreferredBackBufferHeight = screenHeight;
            graphics.ApplyChanges();

            playerPic = Content.Load<Texture2D>("playerPic");
            spriteSheet = Content.Load<Texture2D>("SpriteSheet");

            chunks = new List<Chunks>();

            //Creates each of the first 9 chunks in a 3x3 grid
            //Middle
            chunks.Add(new Chunks(spriteSheet, new Vector2(0, 0)));
            chunks[0].playerPresent = true;

            //Top-Left
            chunks.Add(new Chunks(spriteSheet, new Vector2(-640, -640)));
            chunks[1].playerPresent = true;

            //Top
            chunks.Add(new Chunks(spriteSheet, new Vector2(0, -640)));
            chunks[2].playerPresent = true;

            //Top-Right
            chunks.Add(new Chunks(spriteSheet, new Vector2(640, -640)));
            chunks[3].playerPresent = true;

            //Left
            chunks.Add(new Chunks(spriteSheet, new Vector2(-640, 0)));
            chunks[4].playerPresent = true;

            //Right
            chunks.Add(new Chunks(spriteSheet, new Vector2(640, 0)));
            chunks[5].playerPresent = true;

            //Bottom-Left
            chunks.Add(new Chunks(spriteSheet, new Vector2(-640, 640)));
            chunks[6].playerPresent = true;

            //Bottom
            chunks.Add(new Chunks(spriteSheet, new Vector2(0, 640)));
            chunks[7].playerPresent = true;

            //Bottom-Right
            chunks.Add(new Chunks(spriteSheet, new Vector2(640, 640)));
            chunks[8].playerPresent = true;

            player = new Player(new Vector2(screenWidth / 2, screenHeight / 2), playerPic);

            base.Initialize();
        }

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);
            titleFont = Content.Load<SpriteFont>("titleFont");
            smallFont = Content.Load<SpriteFont>("smallFont");

        }

        protected override void UnloadContent()
        {
        }

        protected override void Update(GameTime gameTime)
        {
            newKState = Keyboard.GetState();
            switch (screen)
            {
                case currentScreen.splashScreen:
                    {
                        if (newKState.IsKeyDown(Keys.E) && !oldKState.IsKeyDown(Keys.E))
                        {
                            screen = currentScreen.menuScreen;
                        }
                        break;
                    }
                case currentScreen.menuScreen:
                    {
                        if (newKState.IsKeyDown(Keys.E) && !oldKState.IsKeyDown(Keys.E))
                        {
                            screen = currentScreen.playerScreen;
                        }
                        if (newKState.IsKeyDown(Keys.R) && !oldKState.IsKeyDown(Keys.R))
                        {
                            screen = currentScreen.settingsScreen;
                        }
                        if (Keyboard.GetState().IsKeyDown(Keys.Escape))
                        {
                            this.Exit();
                        }
                        break;
                    }
                case currentScreen.playerScreen:
                    {

                        break;
                    }
                case currentScreen.pauseScreen:
                    {

                        break;
                    }
                case currentScreen.gameplayScreen:
                    {
                        player.Update(gameTime, Keyboard.GetState());
                        for (int i = 0; i < chunks.Count; i++)
                        {
                            player.Collisions(chunks[i].wallTiles);
                        }

                        camera.Move(player.velocity);
                        break;
                    }
                case currentScreen.completeScreen:
                    {

                        break;
                    }
                case currentScreen.gameoverScreen:
                    {

                        break;
                    }
                case currentScreen.settingsScreen:
                    {

                        break;
                    }
            }
            oldKState = newKState;
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            switch (screen)
            {
                case currentScreen.splashScreen:
                    {
                        GraphicsDevice.Clear(Color.CornflowerBlue);
                        break;
                    }
                case currentScreen.menuScreen:
                    {
                        GraphicsDevice.Clear(Color.CornflowerBlue);

                        spriteBatch.Begin();
                        spriteBatch.DrawString(titleFont, "The Highlanders", new Vector2(400, 200), Color.White);
                        spriteBatch.DrawString(smallFont, "Press E - Play", new Vector2(400, 400), Color.White);
                        spriteBatch.DrawString(smallFont, "Press R - Settings", new Vector2(400, 450), Color.White);
                        spriteBatch.DrawString(smallFont, "Press Esc - Exit", new Vector2(400, 500), Color.White);
                        spriteBatch.End();

                        break;
                    }
                case currentScreen.playerScreen:
                    {
                        GraphicsDevice.Clear(Color.CornflowerBlue);
                        break;
                    }
                case currentScreen.pauseScreen:
                    {
                        GraphicsDevice.Clear(Color.CornflowerBlue);
                        break;
                    }
                case currentScreen.gameplayScreen:
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
                        break;
                    }
                case currentScreen.completeScreen:
                    {
                        GraphicsDevice.Clear(Color.CornflowerBlue);
                        break;
                    }
                case currentScreen.gameoverScreen:
                    {
                        GraphicsDevice.Clear(Color.CornflowerBlue);
                        break;
                    }
                case currentScreen.settingsScreen:
                    {
                        GraphicsDevice.Clear(Color.CornflowerBlue);
                        break;
                    }
            }

            base.Draw(gameTime);
        }
    }
}
