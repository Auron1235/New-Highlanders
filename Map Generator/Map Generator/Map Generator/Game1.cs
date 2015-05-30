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
        SpriteFont smallFont;
        Random rand;
        bool debugToggle = true;

        enum Screens { splashScreen, menuScreen, gamesettingsScreen, menusettingsScreen, playerScreen, gameplayScreen, pauseScreen, completeScreen, gameoverScreen };
        Screens currentScreen = new Screens();

        Camera2D m_camera;
        int screenWidth = 1024;
        int screenHeight = 768;

        AudioManager audioManager;
        ChunkManager chunkManager;
        ScreenManager screenManager;

        Texture2D spriteSheet;

        List<Wolf> wolves = new List<Wolf>();
        List<Bear> bears = new List<Bear>();

        Player player;
        KeyboardState oldKState;
        KeyboardState newKState;
        GamePadState oldPadState;
        GamePadState newPadState;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }

        protected override void Initialize()
        {
            //spriteSheet = Content.Load<Texture2D>("SpriteSheet");
            spriteSheet = Content.Load<Texture2D>("ScenerySpriteSheet");

            rand = new Random();
            smallFont = Content.Load<SpriteFont>("smallFont");

            m_camera = new Camera2D(0, 0, screenWidth, screenHeight);
            graphics.PreferredBackBufferWidth = screenWidth;
            graphics.PreferredBackBufferHeight = screenHeight;
            graphics.ApplyChanges();

            audioManager = new AudioManager();
            audioManager.Initialize(Content);

            chunkManager = new ChunkManager();
            chunkManager.Initialize(499, spriteSheet); //TODO - DEBUG - replace "499" with the elapsed milliseconds at time of entring gameplay.

            player = new Player(new Vector2(screenWidth / 2, screenHeight / 2), Content.Load<Texture2D>("playerSheet"), 21, 32);
            //DEBUG - sets player position to centre chunk position
            player.position = chunkManager.FindChunkCentre(new Vector2(2, 2));

            screenManager = new ScreenManager();
            screenManager.Initialize(Content);

            oldKState = Keyboard.GetState();
            oldPadState = GamePad.GetState(PlayerIndex.One);

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
            screenManager.Update(player, gameTime, m_camera, wolves, bears, chunkManager, audioManager);

            //newKState = Keyboard.GetState();
            //newPadState = GamePad.GetState(PlayerIndex.One);

            ////Toggles the debug controls
            //if (newKState.IsKeyDown(Keys.F1) && oldKState.IsKeyUp(Keys.F1)) debugToggle = !debugToggle;

            //switch (currentScreen)
            //{
            //    case Screens.splashScreen:
            //        {
            //            //Normal Controls
            //            if (newPadState.Buttons.A == ButtonState.Pressed && oldPadState.Buttons.A != ButtonState.Pressed)   currentScreen = Screens.menuScreen;

            //            //DebugToggle Controls
            //            if (debugToggle)
            //            {
            //                if (newKState.IsKeyDown(Keys.E) && !oldKState.IsKeyDown(Keys.E)) currentScreen = Screens.menuScreen;
            //            }
                        
            //            break;
            //        }
            //    case Screens.menuScreen:
            //        {
            //            //Normal Controls
            //            if (newPadState.Buttons.A == ButtonState.Pressed && oldPadState.Buttons.A != ButtonState.Pressed) currentScreen = Screens.playerScreen;
            //            if (newPadState.Buttons.Y == ButtonState.Pressed && oldPadState.Buttons.Y != ButtonState.Pressed) currentScreen = Screens.menusettingsScreen;
            //            if (newPadState.Buttons.Back == ButtonState.Pressed && oldPadState.Buttons.Back != ButtonState.Pressed) this.Exit();

            //            //DebugToggle Controls
            //            if (debugToggle)
            //            {
            //                if (newKState.IsKeyDown(Keys.E) && !oldKState.IsKeyDown(Keys.E)) currentScreen = Screens.playerScreen;
            //                if (newKState.IsKeyDown(Keys.R) && !oldKState.IsKeyDown(Keys.R)) currentScreen = Screens.menusettingsScreen;
            //                if (Keyboard.GetState().IsKeyDown(Keys.Escape)) this.Exit();
            //            }

            //            break;
            //        }
            //    case Screens.playerScreen:
            //        {
            //            //Normal Controls
            //            if (newPadState.Buttons.A == ButtonState.Pressed && oldPadState.Buttons.A != ButtonState.Pressed) currentScreen = Screens.gameplayScreen;
            //            if (newPadState.Buttons.B == ButtonState.Pressed && oldPadState.Buttons.B != ButtonState.Pressed) currentScreen = Screens.menuScreen;

            //            //DebugToggle Controls
            //            if (debugToggle)
            //            {
            //                if (newKState.IsKeyDown(Keys.E) && !oldKState.IsKeyDown(Keys.E)) currentScreen = Screens.gameplayScreen;
            //                if (newKState.IsKeyDown(Keys.Q) && !oldKState.IsKeyDown(Keys.Q)) currentScreen = Screens.menuScreen;
            //            }

            //            break;
            //        }
            //    case Screens.pauseScreen:
            //        {
            //            //Normal Controls
            //            if (newPadState.Buttons.B == ButtonState.Pressed && oldPadState.Buttons.B != ButtonState.Pressed) currentScreen = Screens.gameplayScreen;
            //            if (newPadState.Buttons.Y == ButtonState.Pressed && oldPadState.Buttons.Y != ButtonState.Pressed) currentScreen = Screens.gamesettingsScreen;
            //            if (newPadState.Buttons.Back == ButtonState.Pressed && oldPadState.Buttons.Back != ButtonState.Pressed) this.Exit();

            //            //DebugToggle Controls
            //            if (debugToggle)
            //            {
            //                if (newKState.IsKeyDown(Keys.Q) && !oldKState.IsKeyDown(Keys.Q)) currentScreen = Screens.gameplayScreen;
            //                if (newKState.IsKeyDown(Keys.R) && !oldKState.IsKeyDown(Keys.R)) currentScreen = Screens.gamesettingsScreen;
            //                if (newKState.IsKeyDown(Keys.Escape) && !oldKState.IsKeyDown(Keys.Escape)) this.Exit();
            //            }

            //            break;
            //        }
            //    case Screens.gameplayScreen:
            //        {
            //            //DEBUG causes camera to follow the player, change to the midpoint between players when 2player implemented
            //            m_camera.Target = player.position;

            //            player.Update(gameTime, Keyboard.GetState(), m_camera, wolves, bears);

            //            //updates the chunks using the cameraTarget
            //            chunkManager.Update(m_camera.Target);

            //            //DEBUG TEXT
            //            Console.WriteLine("Player pos: " + player.position);
            //            Console.WriteLine("chunks: " + chunkManager.Chunks.Count);
            //            int obscount = 0;
            //            foreach (Chunk chunk in chunkManager.Chunks)
            //            {
            //                obscount += chunk.Obstacles.Count;
            //            }
            //            Console.WriteLine("obsCount: " + obscount);

            //            //Normal control info
            //            if (newPadState.Buttons.Start == ButtonState.Pressed && oldPadState.Buttons.Start != ButtonState.Pressed) currentScreen = Screens.pauseScreen;

            //            //DebugToggle control info
            //            if (newKState.IsKeyDown(Keys.Space) && !oldKState.IsKeyDown(Keys.Space)) currentScreen = Screens.pauseScreen;


            //            //updates camera viewport position for the coming draw call
            //            m_camera.UpdateViewPort(new Vector2(0, 0));
            //            break;
            //        }
            //    case Screens.completeScreen:
            //        {
            //            //Normal Controls
            //            if (newPadState.Buttons.A == ButtonState.Pressed && oldPadState.Buttons.A != ButtonState.Pressed) currentScreen = Screens.menuScreen;

            //            //DebugToggle Controls
            //            if (debugToggle)
            //            {
            //                if (newKState.IsKeyDown(Keys.E) && !oldKState.IsKeyDown(Keys.E)) currentScreen = Screens.menuScreen;
            //            }

            //            break;
            //        }
            //    case Screens.gameoverScreen:
            //        {
            //            //Normal Controls
            //            if (newPadState.Buttons.A == ButtonState.Pressed && oldPadState.Buttons.A != ButtonState.Pressed) currentScreen = Screens.menuScreen;

            //            //DebugToggle Controls
            //            if (debugToggle)
            //            {
            //                if (newKState.IsKeyDown(Keys.E) && !oldKState.IsKeyDown(Keys.E)) currentScreen = Screens.menuScreen;
            //            }

            //            break;
            //        }
            //    case Screens.gamesettingsScreen:
            //        {
            //            //Normal Controls
            //            if (newPadState.DPad.Down == ButtonState.Pressed && oldPadState.Buttons.A != ButtonState.Pressed) { } //Volume Down
            //            if (newPadState.DPad.Up == ButtonState.Pressed && oldPadState.Buttons.Y != ButtonState.Pressed) { } // Volume Up
            //            if (newPadState.Buttons.B == ButtonState.Pressed && oldPadState.Buttons.B != ButtonState.Pressed) currentScreen = Screens.pauseScreen;

            //            //DebugToggle Controls
            //            if (debugToggle)
            //            {
            //                if (newKState.IsKeyDown(Keys.E) && !oldKState.IsKeyDown(Keys.E)) { }   //Volume down
            //                if (newKState.IsKeyDown(Keys.R) && !oldKState.IsKeyDown(Keys.R)) { }   //Volume up
            //                if (newKState.IsKeyDown(Keys.Q) && !oldKState.IsKeyDown(Keys.Q)) currentScreen = Screens.pauseScreen;
            //            }

            //            break;
            //        }
            //    case Screens.menusettingsScreen:
            //        {
            //            //Normal Controls
            //            if (newPadState.Buttons.A == ButtonState.Pressed && oldPadState.Buttons.A != ButtonState.Pressed) { } //Volume Down
            //            if (newPadState.Buttons.Y == ButtonState.Pressed && oldPadState.Buttons.Y != ButtonState.Pressed) { } // Volume Up
            //            if (newPadState.Buttons.B == ButtonState.Pressed && oldPadState.Buttons.B != ButtonState.Pressed) currentScreen = Screens.menuScreen;

            //            //DebugToggle Controls
            //            if (debugToggle)
            //            {
            //                if (newKState.IsKeyDown(Keys.E) && !oldKState.IsKeyDown(Keys.E)) { }   //Volume down
            //                if (newKState.IsKeyDown(Keys.R) && !oldKState.IsKeyDown(Keys.R)) { }   //Volume up
            //                if (newKState.IsKeyDown(Keys.Q) && !oldKState.IsKeyDown(Keys.Q)) currentScreen = Screens.menuScreen;
            //            }

            //            break;
            //        }
            //}
            //oldKState = newKState;
            //oldPadState = newPadState;

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            screenManager.Draw(spriteBatch, smallFont, player, m_camera, chunkManager);
            //switch (currentScreen)
            //{
            //        //DEBUG new camera code, may need to be integrated into each screen individually.
            //        //spriteBatch.Begin(SpriteSortMode.BackToFront,
            //        //    BlendState.AlphaBlend,
            //        //    null,
            //        //    null,
            //        //    null,
            //        //    null,
            //        //    m_camera.GetViewPortMatrix);

            //        //DRAW CODE GOES HERE - My sample debug text pasted for reference..
            //        //spriteBatch.DrawString(debugFont,
            //        //    chunkManager.GetChunkCoordinate(player.position).ToString(),
            //        //    new Vector2(player.position.X, player.position.Y + 20),
            //        //    Color.White);
            //        //spriteBatch.DrawString(debugFont,
            //        //    player.position.ToString(),
            //        //    new Vector2(player.position.X, player.position.Y + 40),
            //        //    Color.White);
            //        //spriteBatch.DrawString(
            //        //    debugFont,
            //        //    "Chunk Count: " + chunkManager.Chunks.Count,
            //        //    new Vector2(player.position.X, player.position.Y + 60),
            //        //    Color.White);

            //        //end as normal
            //        //spriteBatch.End();

            //    case Screens.splashScreen:
            //        {
            //            GraphicsDevice.Clear(Color.CornflowerBlue);

            //            spriteBatch.Begin();
            //            spriteBatch.DrawString(smallFont, "Image goes here", new Vector2(400, 200), Color.White);
            //            spriteBatch.DrawString(smallFont, "for splash", new Vector2(400, 230), Color.White);
            //            spriteBatch.End();

            //            break;
            //        }
            //    case Screens.menuScreen:
            //        {
            //            GraphicsDevice.Clear(Color.CornflowerBlue);

            //            if (debugToggle)
            //            {
            //                //Debug control info
            //                spriteBatch.Begin();
            //                spriteBatch.DrawString(smallFont, "The Highlanders", new Vector2(400, 200), Color.White);
            //                spriteBatch.DrawString(smallFont, "Press E - Play", new Vector2(400, 400), Color.White);
            //                spriteBatch.DrawString(smallFont, "Press R - Settings", new Vector2(400, 450), Color.White);
            //                spriteBatch.DrawString(smallFont, "Press Esc - Exit", new Vector2(400, 500), Color.White);
            //                spriteBatch.End();
            //            }
            //            else
            //            {
            //                //Normal control info
            //                spriteBatch.Begin();
            //                spriteBatch.DrawString(smallFont, "The Highlanders", new Vector2(400, 200), Color.White);
            //                spriteBatch.DrawString(smallFont, "Press A - Play", new Vector2(400, 400), Color.White);
            //                spriteBatch.DrawString(smallFont, "Press Y - Settings", new Vector2(400, 450), Color.White);
            //                spriteBatch.DrawString(smallFont, "Press Back - Exit", new Vector2(400, 500), Color.White);
            //                spriteBatch.End();
            //            }

            //            break;
            //        }
            //    case Screens.playerScreen:
            //        {
            //            GraphicsDevice.Clear(Color.CornflowerBlue);

            //            if (debugToggle)
            //            {
            //                //Debug control info
            //                spriteBatch.Begin();
            //                spriteBatch.DrawString(smallFont, "Player 1 Image", new Vector2(200, 400), Color.White);
            //                spriteBatch.DrawString(smallFont, "Player 2 Image", new Vector2(600, 400), Color.White);
            //                spriteBatch.DrawString(smallFont, "Press Q - Return", new Vector2(400, 600), Color.White);
            //                spriteBatch.End();
            //            }
            //            else
            //            {
            //                //Normal control info
            //                spriteBatch.Begin();
            //                spriteBatch.DrawString(smallFont, "Player 1 Image", new Vector2(200, 400), Color.White);
            //                spriteBatch.DrawString(smallFont, "Player 2 Image", new Vector2(600, 400), Color.White);
            //                spriteBatch.DrawString(smallFont, "Press B - Return", new Vector2(400, 600), Color.White);
            //                spriteBatch.End();
            //            }

            //            break;
            //        }
            //    case Screens.pauseScreen:
            //        {
            //            GraphicsDevice.Clear(Color.CornflowerBlue);

            //            if (debugToggle)
            //            {
            //                //Debug control info
            //                spriteBatch.Begin();
            //                spriteBatch.DrawString(smallFont, "Paused", new Vector2(400, 200), Color.White);
            //                spriteBatch.DrawString(smallFont, "Press Q - Resume", new Vector2(400, 400), Color.White);
            //                spriteBatch.DrawString(smallFont, "Press R - Settings", new Vector2(400, 450), Color.White);
            //                spriteBatch.DrawString(smallFont, "Press Escape - Exit Game", new Vector2(400, 500), Color.White);
            //                spriteBatch.End();
            //            }
            //            else
            //            {
            //                //Normal control info
            //                spriteBatch.Begin();
            //                spriteBatch.DrawString(smallFont, "Paused", new Vector2(400, 200), Color.White);
            //                spriteBatch.DrawString(smallFont, "Press B - Resume", new Vector2(400, 400), Color.White);
            //                spriteBatch.DrawString(smallFont, "Press Y - Settings", new Vector2(400, 450), Color.White);
            //                spriteBatch.DrawString(smallFont, "Press Back - Exit Game", new Vector2(400, 500), Color.White);
            //                spriteBatch.End();
            //            }

            //            break;
            //        }
            //    case Screens.gameplayScreen:
            //        {
            //            GraphicsDevice.Clear(Color.CornflowerBlue);

            //            //Draw calls are fully managed on Chunk Manager, including necessary overloads to SpriteBatch.
            //            //DO NOT USE spriteBatch.Begin() or .End(); THEY ARE NOT REQUIRED.
            //            chunkManager.Draw(spriteBatch, m_camera);
            //            //DO NOT USE spriteBatch.Begin() or .End(); THEY ARE NOT REQUIRED.

            //            spriteBatch.Begin(SpriteSortMode.BackToFront, BlendState.AlphaBlend, null, null, null, null, m_camera.GetViewPortMatrix);
                        
            //            //TODO insert enemy drawing here

            //            player.NewDraw(spriteBatch);
            //            spriteBatch.End();

            //            break;
            //        }
            //    case Screens.completeScreen:
            //        {
            //            GraphicsDevice.Clear(Color.CornflowerBlue);

            //            if (debugToggle)
            //            {
            //                //Debug control info
            //                spriteBatch.Begin();
            //                spriteBatch.DrawString(smallFont, "You Win", new Vector2(400, 200), Color.White);
            //                spriteBatch.DrawString(smallFont, "Press E - Continue", new Vector2(400, 600), Color.White);
            //                spriteBatch.End();
            //            }
            //            else
            //            {
            //                //Normal control info
            //                spriteBatch.Begin();
            //                spriteBatch.DrawString(smallFont, "You Win", new Vector2(400, 200), Color.White);
            //                spriteBatch.DrawString(smallFont, "Press A - Continue", new Vector2(400, 600), Color.White);
            //                spriteBatch.End();
            //            }

            //            break;
            //        }
            //    case Screens.gameoverScreen:
            //        {
            //            GraphicsDevice.Clear(Color.CornflowerBlue);

            //            if (debugToggle)
            //            {
            //                //Debug control info
            //                spriteBatch.Begin();
            //                spriteBatch.DrawString(smallFont, "You Lose", new Vector2(400, 200), Color.White);
            //                spriteBatch.DrawString(smallFont, "Press E - Continue", new Vector2(400, 600), Color.White);
            //                spriteBatch.End();
            //            }
            //            else
            //            {
            //                //Normal control info
            //                spriteBatch.Begin();
            //                spriteBatch.DrawString(smallFont, "You Lose", new Vector2(400, 200), Color.White);
            //                spriteBatch.DrawString(smallFont, "Press A - Continue", new Vector2(400, 600), Color.White);
            //                spriteBatch.End();
            //            }

            //            break;
            //        }
            //    case Screens.gamesettingsScreen:
            //        {
            //            GraphicsDevice.Clear(Color.CornflowerBlue);

            //            if (debugToggle)
            //            {
            //                //Debug control info
            //                spriteBatch.Begin();
            //                spriteBatch.DrawString(smallFont, "Settings", new Vector2(400, 200), Color.White);
            //                spriteBatch.DrawString(smallFont, "Press E - Volume +", new Vector2(400, 400), Color.White);
            //                spriteBatch.DrawString(smallFont, "Press R - Volume -", new Vector2(400, 450), Color.White);
            //                spriteBatch.DrawString(smallFont, "Press Q - Return", new Vector2(400, 500), Color.White);
            //                spriteBatch.End();
            //            }
            //            else
            //            {
            //                //Normal control info
            //                spriteBatch.Begin();
            //                spriteBatch.DrawString(smallFont, "Settings", new Vector2(400, 200), Color.White);
            //                spriteBatch.DrawString(smallFont, "Press A - Volume +", new Vector2(400, 400), Color.White);
            //                spriteBatch.DrawString(smallFont, "Press Y - Volume -", new Vector2(400, 450), Color.White);
            //                spriteBatch.DrawString(smallFont, "Press B - Return", new Vector2(400, 500), Color.White);
            //                spriteBatch.End();
            //            }

            //            break;
            //        }
            //    case Screens.menusettingsScreen:
            //        {
            //            GraphicsDevice.Clear(Color.CornflowerBlue);

            //            if (debugToggle)
            //            {
            //                //Debug control info
            //                spriteBatch.Begin();
            //                spriteBatch.DrawString(smallFont, "Settings", new Vector2(400, 200), Color.White);
            //                spriteBatch.DrawString(smallFont, "Press E - Volume +", new Vector2(400, 400), Color.White);
            //                spriteBatch.DrawString(smallFont, "Press R - Volume -", new Vector2(400, 450), Color.White);
            //                spriteBatch.DrawString(smallFont, "Press Q - Return", new Vector2(400, 500), Color.White);
            //                spriteBatch.End();
            //            }
            //            else
            //            {
            //                //Normal control info
            //                spriteBatch.Begin();
            //                spriteBatch.DrawString(smallFont, "Settings", new Vector2(400, 200), Color.White);
            //                spriteBatch.DrawString(smallFont, "Press A - Volume +", new Vector2(400, 400), Color.White);
            //                spriteBatch.DrawString(smallFont, "Press Y - Volume -", new Vector2(400, 450), Color.White);
            //                spriteBatch.DrawString(smallFont, "Press B - Return", new Vector2(400, 500), Color.White);
            //                spriteBatch.End();
            //            }

            //            break;
            //        }
            //}
            base.Draw(gameTime);
        }
    }
}
