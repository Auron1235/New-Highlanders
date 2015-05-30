using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;

namespace Map_Generator
{
    //THIS WAS ALL ADAM'S IDEA! I didn't want static anywhere, but he made me do it! I'M SORRRRRRYYYYYYYYY!!!
    //He let me remove it.
    public class ScreenManager
    {
        public enum Screen { splash, mainMenu, pauseSettings, menuSettings, playerSelect, gamePlay, pause, lost, won };
        private Screen curScreen;

        private Texture2D splashPic;
        private Texture2D mainMenuPic;
        private Texture2D settingsPic;
        private Texture2D playerSelectPic;
        private Texture2D gamePlayPic;
        private Texture2D pausePic;
        private Texture2D lostPic;
        private Texture2D wonPic;

        private GamePadState oldPadState;
        private GamePadState newPadState;
        private KeyboardState oldKState;
        private KeyboardState newKState;

        private bool debugToggle;
        public bool gameExit;
        public Screen CurScreen
        {
            get { return curScreen; }
            set { curScreen = value; }
        }


        //constructor
        public ScreenManager()
        {
            
        }

        public void Initialize(ContentManager Content)
        {
            curScreen = Screen.splash;

            //splashPic = Content.Load<Texture2D>("");
            //mainMenuPic = Content.Load<Texture2D>("");
            //settingsPic = Content.Load<Texture2D>("");
            //playerSelectPic = Content.Load<Texture2D>("");
            //gamePlayPic = Content.Load<Texture2D>("");
            //pausePic = Content.Load<Texture2D>("");
            //lostPic = Content.Load<Texture2D>("");
            //wonPic = Content.Load<Texture2D>("");

            oldPadState = GamePad.GetState(PlayerIndex.One);
            oldKState = Keyboard.GetState();

            debugToggle = true;
            gameExit = false;
        }

        public void Update(Player player, GameTime gameTime, Camera2D camera2D, List<Wolf> wolves, List<Bear> bears, ChunkManager chunkManager, AudioManager audioManager)
        {
            newPadState = GamePad.GetState(PlayerIndex.One);
            newKState = Keyboard.GetState();
            if (newKState.IsKeyDown(Keys.F1) && oldKState.IsKeyUp(Keys.F1)) debugToggle = !debugToggle;
            switch (curScreen)
            {
                case Screen.splash: SplashUpdate(); break;
                case Screen.mainMenu: MainMenuUpdate(); break;
                case Screen.pauseSettings: PauseSettingsUpdate() ;break;
                case Screen.menuSettings: MenuSettingsUpdate(); break;
                case Screen.playerSelect: PlayerSelectUpdate(); break;
                case Screen.gamePlay: GamePlayUpdate(player, gameTime, camera2D, wolves, bears, chunkManager, audioManager); break;
                case Screen.pause: PauseUpdate(); break;
                case Screen.lost: EndUpdate(); break;
                case Screen.won: EndUpdate(); break;
            }
            oldPadState = newPadState;
            oldKState = newKState;
        }

        public void Draw(SpriteBatch spriteBatch, SpriteFont smallFont, Player player, Camera2D camera2D, ChunkManager chunkManager)
        {
            switch (curScreen)
            {
                case Screen.splash: SplashDraw(spriteBatch); break;
                case Screen.mainMenu: MainMenuDraw(spriteBatch, smallFont); break;
                case Screen.pauseSettings: SettingsDraw(spriteBatch, smallFont); break;
                case Screen.menuSettings: SettingsDraw(spriteBatch, smallFont); break;
                case Screen.playerSelect: PlayerSelectDraw(spriteBatch, smallFont); break;
                case Screen.gamePlay: GamePlayDraw(spriteBatch, smallFont, player, chunkManager, camera2D); break;
                case Screen.pause: PauseDraw(spriteBatch, smallFont); break;
                case Screen.lost: LostDraw(spriteBatch); break;
                case Screen.won: WonDraw(spriteBatch); break;
            }
        }

        private void SplashUpdate()
        {
            //Normal Controls
            if (newPadState.Buttons.A == ButtonState.Pressed && oldPadState.Buttons.A != ButtonState.Pressed) curScreen = Screen.mainMenu;

            //Debug Controls
            if (debugToggle)
            {
                if (newKState.IsKeyDown(Keys.E) && !oldKState.IsKeyDown(Keys.E)) curScreen = Screen.mainMenu;
            }
        }
        private void SplashDraw(SpriteBatch spriteBatch)
        {

            spriteBatch.Begin();
            //spriteBatch.Draw(splashPic, new Vector2(400, 400), Color.White);
            spriteBatch.End();

        }


        private void MainMenuUpdate()
        {
            //Normal Controls
            if (newPadState.Buttons.A == ButtonState.Pressed && oldPadState.Buttons.A != ButtonState.Pressed) curScreen = Screen.playerSelect;
            if (newPadState.Buttons.Y == ButtonState.Pressed && oldPadState.Buttons.Y != ButtonState.Pressed) curScreen = Screen.menuSettings;
            if (newPadState.Buttons.Back == ButtonState.Pressed && oldPadState.Buttons.Back != ButtonState.Pressed) gameExit = true;

            //DebugToggle Controls
            if (debugToggle)
            {
                if (newKState.IsKeyDown(Keys.E) && !oldKState.IsKeyDown(Keys.E)) curScreen = Screen.playerSelect;
                if (newKState.IsKeyDown(Keys.R) && !oldKState.IsKeyDown(Keys.R)) curScreen = Screen.menuSettings;
                if (Keyboard.GetState().IsKeyDown(Keys.Escape)) gameExit = true;
            }
        }
        private void MainMenuDraw(SpriteBatch spriteBatch, SpriteFont smallFont)
        {

            if (debugToggle)
            {
                //Debug control info
                spriteBatch.Begin();
                spriteBatch.DrawString(smallFont, "The Highlanders", new Vector2(400, 200), Color.White);
                spriteBatch.DrawString(smallFont, "Press E - Play", new Vector2(400, 400), Color.White);
                spriteBatch.DrawString(smallFont, "Press R - Settings", new Vector2(400, 450), Color.White);
                spriteBatch.DrawString(smallFont, "Press Esc - Exit", new Vector2(400, 500), Color.White);
                spriteBatch.End();
            }
            else
            {
                //Normal control info
                spriteBatch.Begin();
                //spriteBatch.Draw(mainMenuPic, new Vector2(0, 0), Color.White);
                spriteBatch.End();
            }

        }


        private void MenuSettingsUpdate()
        {
            //Normal Controls
            if (newPadState.Buttons.A == ButtonState.Pressed && oldPadState.Buttons.A != ButtonState.Pressed) { } //Volume Down
            if (newPadState.Buttons.Y == ButtonState.Pressed && oldPadState.Buttons.Y != ButtonState.Pressed) { } // Volume Up
            if (newPadState.Buttons.B == ButtonState.Pressed && oldPadState.Buttons.B != ButtonState.Pressed) curScreen = Screen.mainMenu;

            //DebugToggle Controls
            if (debugToggle)
            {
                if (newKState.IsKeyDown(Keys.E) && !oldKState.IsKeyDown(Keys.E)) { }   //Volume down
                if (newKState.IsKeyDown(Keys.R) && !oldKState.IsKeyDown(Keys.R)) { }   //Volume up
                if (newKState.IsKeyDown(Keys.Q) && !oldKState.IsKeyDown(Keys.Q)) curScreen = Screen.mainMenu;
            }
        }
        private void SettingsDraw(SpriteBatch spriteBatch, SpriteFont smallFont)
        {
            if (debugToggle)
            {
                //Debug control info
                spriteBatch.Begin();
                spriteBatch.DrawString(smallFont, "Settings", new Vector2(400, 200), Color.White);
                spriteBatch.DrawString(smallFont, "Press E - Volume +", new Vector2(400, 400), Color.White);
                spriteBatch.DrawString(smallFont, "Press R - Volume -", new Vector2(400, 450), Color.White);
                spriteBatch.DrawString(smallFont, "Press Q - Return", new Vector2(400, 500), Color.White);
                spriteBatch.End();
            }
            else
            {
                //Normal control info
                spriteBatch.Begin();
                //spriteBatch.Draw(settingsPic, new Vector2(0, 0), Color.White);
                spriteBatch.End();
            }

        }


        private void PlayerSelectUpdate()
        {
            //Normal Controls
            if (newPadState.Buttons.A == ButtonState.Pressed && oldPadState.Buttons.A != ButtonState.Pressed) curScreen = Screen.gamePlay;
            if (newPadState.Buttons.B == ButtonState.Pressed && oldPadState.Buttons.B != ButtonState.Pressed) curScreen = Screen.mainMenu;

            //DebugToggle Controls
            if (debugToggle)
            {
                if (newKState.IsKeyDown(Keys.E) && !oldKState.IsKeyDown(Keys.E)) curScreen = Screen.gamePlay;
                if (newKState.IsKeyDown(Keys.Q) && !oldKState.IsKeyDown(Keys.Q)) curScreen = Screen.mainMenu;
            }
        }
        private void PlayerSelectDraw(SpriteBatch spriteBatch, SpriteFont smallFont)
        {
            if (debugToggle)
            {
                //Debug control info
                spriteBatch.Begin();
                spriteBatch.DrawString(smallFont, "Player 1 Image", new Vector2(200, 400), Color.White);
                spriteBatch.DrawString(smallFont, "Player 2 Image", new Vector2(600, 400), Color.White);
                spriteBatch.DrawString(smallFont, "Press Q - Return", new Vector2(400, 600), Color.White);
                spriteBatch.End();
            }
            else
            {
                //Normal control info
                spriteBatch.Begin();
                spriteBatch.Draw(playerSelectPic, new Vector2(0, 0), Color.White);
                spriteBatch.DrawString(smallFont, "Player 1 Image", new Vector2(200, 400), Color.White);
                spriteBatch.DrawString(smallFont, "Player 2 Image", new Vector2(600, 400), Color.White);
                spriteBatch.End();
            }
        }


        private void GamePlayUpdate(Player player, GameTime gameTime, Camera2D camera2D, List<Wolf> wolves, List<Bear> bears, ChunkManager chunkManager, AudioManager audioManager)
        {
            player.Update(gameTime, Keyboard.GetState(), camera2D, wolves, bears);

            camera2D.Target = player.position;

            chunkManager.Update(camera2D.Target);

            //Normal control info
            if (newPadState.Buttons.Start == ButtonState.Pressed && oldPadState.Buttons.Start != ButtonState.Pressed) curScreen = Screen.pause;

            //DebugToggle control info
            if (newKState.IsKeyDown(Keys.Space) && !oldKState.IsKeyDown(Keys.Space)) curScreen = Screen.pause;

            
            camera2D.UpdateViewPort(Vector2.Zero);

        }
        private void GamePlayDraw(SpriteBatch spriteBatch, SpriteFont smallFont, Player player, ChunkManager chunkManager, Camera2D camera2D)
        {
            //GraphicsDevice.Clear(Color.CornflowerBlue);

            //Draw calls are fully managed on Chunk Manager, including necessary overloads to SpriteBatch.
            //DO NOT USE spriteBatch.Begin() or .End(); THEY ARE NOT REQUIRED.
            chunkManager.Draw(spriteBatch, camera2D);
            //DO NOT USE spriteBatch.Begin() or .End(); THEY ARE NOT REQUIRED.

            spriteBatch.Begin(SpriteSortMode.BackToFront, BlendState.AlphaBlend, null, null, null, null, camera2D.GetViewPortMatrix);

            //TODO insert enemy drawing here

            player.NewDraw(spriteBatch);
            spriteBatch.End();
        }


        private void PauseUpdate()
        {
            //Normal Controls
            if (newPadState.Buttons.B == ButtonState.Pressed && oldPadState.Buttons.B != ButtonState.Pressed) curScreen = Screen.gamePlay;
            if (newPadState.Buttons.Y == ButtonState.Pressed && oldPadState.Buttons.Y != ButtonState.Pressed) curScreen = Screen.pauseSettings;
            if (newPadState.Buttons.Back == ButtonState.Pressed && oldPadState.Buttons.Back != ButtonState.Pressed) gameExit = true;

            //DebugToggle Controls
            if (debugToggle)
            {
                if (newKState.IsKeyDown(Keys.Q) && !oldKState.IsKeyDown(Keys.Q)) curScreen = Screen.gamePlay;
                if (newKState.IsKeyDown(Keys.R) && !oldKState.IsKeyDown(Keys.R)) curScreen = Screen.pauseSettings;
                if (newKState.IsKeyDown(Keys.Escape) && !oldKState.IsKeyDown(Keys.Escape)) gameExit = true;
            }
        }
        private void PauseDraw(SpriteBatch spriteBatch, SpriteFont smallFont)
        {
            if (debugToggle)
            {
                //Debug control info
                spriteBatch.Begin();
                spriteBatch.DrawString(smallFont, "Paused", new Vector2(400, 200), Color.White);
                spriteBatch.DrawString(smallFont, "Press Q - Resume", new Vector2(400, 400), Color.White);
                spriteBatch.DrawString(smallFont, "Press R - Settings", new Vector2(400, 450), Color.White);
                spriteBatch.DrawString(smallFont, "Press Escape - Exit Game", new Vector2(400, 500), Color.White);
                spriteBatch.End();
            }
            else
            {
                //Normal control info
                spriteBatch.Begin();
                spriteBatch.Draw(pausePic, new Vector2(0, 0), Color.White);
                spriteBatch.End();
            }
        }


        private void PauseSettingsUpdate()
        {
            //Normal Controls
            if (newPadState.Buttons.A == ButtonState.Pressed && oldPadState.Buttons.A != ButtonState.Pressed) { } //Volume Down
            if (newPadState.Buttons.Y == ButtonState.Pressed && oldPadState.Buttons.Y != ButtonState.Pressed) { } // Volume Up
            if (newPadState.Buttons.B == ButtonState.Pressed && oldPadState.Buttons.B != ButtonState.Pressed) curScreen = Screen.pause;

            //DebugToggle Controls
            if (debugToggle)
            {
                if (newKState.IsKeyDown(Keys.E) && !oldKState.IsKeyDown(Keys.E)) { }   //Volume down
                if (newKState.IsKeyDown(Keys.R) && !oldKState.IsKeyDown(Keys.R)) { }   //Volume up
                if (newKState.IsKeyDown(Keys.Q) && !oldKState.IsKeyDown(Keys.Q)) curScreen = Screen.pause;
            }
        }


        private void EndUpdate()
        {
            //Normal Controls
            if (newPadState.Buttons.A == ButtonState.Pressed && oldPadState.Buttons.A != ButtonState.Pressed) curScreen = Screen.mainMenu;

            //DebugToggle Controls
            if (debugToggle)
            {
                if (newKState.IsKeyDown(Keys.E) && !oldKState.IsKeyDown(Keys.E)) curScreen = Screen.mainMenu;
            }
        }
        private void LostDraw(SpriteBatch spriteBatch)
        {
             spriteBatch.Begin();
             spriteBatch.Draw(lostPic, new Vector2(0, 0), Color.White);
             spriteBatch.End();
        }
        private void WonDraw(SpriteBatch spriteBatch)
        {
            spriteBatch.Begin();
            spriteBatch.Draw(wonPic, new Vector2(0, 0), Color.White);
            spriteBatch.End();
        }
    }
}
