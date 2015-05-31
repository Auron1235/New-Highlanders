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
    public class ScreenManager
    {
        public enum Screen { splash, mainMenu, pauseSettings, menuSettings, playerSelect, gamePlay, pause, lost, won };
        private Screen curScreen;

        private Texture2D player1Portrait;
        private Texture2D player2Portrait;

        private Texture2D maxHpPic;
        private Texture2D curHpPic;
        private Texture2D damagePic;
        private Texture2D defensePic;

        private Texture2D splashPic;
        private Texture2D mainMenuPic;
        private Texture2D settingsPic;
        private Texture2D playerSelectPic;
        private Texture2D gamePlayPic;
        private Texture2D pausePic;
        private Texture2D lostPic;
        private Texture2D wonPic;

        private GamePadState oldPad1State;
        private GamePadState newPad1State;
        private GamePadState oldPad2State;
        private GamePadState newPad2State;
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
            curScreen = Screen.gamePlay; //DEBUG faster testing, changed it to gameplay for now

            player1Portrait = Content.Load<Texture2D>("UI/player1UI");
            player2Portrait = Content.Load<Texture2D>("UI/player2UI");
            maxHpPic = Content.Load<Texture2D>("UI/EmptyHeartUI");
            curHpPic = Content.Load<Texture2D>("UI/FullHeartUI");
            damagePic = Content.Load<Texture2D>("UI/AttackUI");
            defensePic = Content.Load<Texture2D>("UI/DefenseUI");

            //splashPic = Content.Load<Texture2D>("");
            //mainMenuPic = Content.Load<Texture2D>("");
            //settingsPic = Content.Load<Texture2D>("");
            //playerSelectPic = Content.Load<Texture2D>("");
            //gamePlayPic = Content.Load<Texture2D>("");
            //pausePic = Content.Load<Texture2D>("");
            //lostPic = Content.Load<Texture2D>("");
            //wonPic = Content.Load<Texture2D>("");

            oldPad1State = GamePad.GetState(PlayerIndex.One);
            oldPad2State = GamePad.GetState(PlayerIndex.Two);
            oldKState = Keyboard.GetState();

            debugToggle = true;
            gameExit = false;
        }

<<<<<<< HEAD
        public void Update(Player player, GameTime gameTime, Camera2D camera2D, List<Wolf> wolves, List<Bear> bears, ChunkManager chunkManager, AudioManager audioManager, EnemyManager enemyManager)
=======
        public void Update(Player player1, Player player2, GameTime gameTime, Camera2D camera2D, List<Wolf> wolves, List<Bear> bears, ChunkManager chunkManager, AudioManager audio)
>>>>>>> origin/devLatest
        {
            newPad1State = GamePad.GetState(PlayerIndex.One);
            newPad2State = GamePad.GetState(PlayerIndex.Two);
            newKState = Keyboard.GetState();
            if (newKState.IsKeyDown(Keys.F1) && oldKState.IsKeyUp(Keys.F1)) debugToggle = !debugToggle;
            switch (curScreen)
            {
<<<<<<< HEAD
                case Screen.splash: SplashUpdate(); break;
                case Screen.mainMenu: MainMenuUpdate(); break;
                case Screen.pauseSettings: PauseSettingsUpdate() ;break;
                case Screen.menuSettings: MenuSettingsUpdate(); break;
                case Screen.playerSelect: PlayerSelectUpdate(chunkManager, enemyManager); break;
                case Screen.gamePlay: GamePlayUpdate(player, gameTime, camera2D, wolves, bears, chunkManager, audioManager, enemyManager); break;
                case Screen.pause: PauseUpdate(); break;
                case Screen.lost: EndUpdate(); break;
                case Screen.won: EndUpdate(); break;
=======
                case Screen.splash: SplashUpdate(audio); break;
                case Screen.mainMenu: MainMenuUpdate(audio); break;
                case Screen.pauseSettings: PauseSettingsUpdate(audio) ;break;
                case Screen.menuSettings: MenuSettingsUpdate(audio); break;
                case Screen.playerSelect: PlayerSelectUpdate(audio); break;
                case Screen.gamePlay: GamePlayUpdate(player1, player2, gameTime, camera2D, wolves, bears, chunkManager, audio); break;
                case Screen.pause: PauseUpdate(audio); break;
                case Screen.lost: EndUpdate(audio); break;
                case Screen.won: EndUpdate(audio); break;
>>>>>>> origin/devLatest
            }
            oldPad1State = newPad1State;
            oldPad2State = newPad2State;
            oldKState = newKState;
        }

<<<<<<< HEAD
        public void Draw(SpriteBatch spriteBatch, SpriteFont smallFont, Player player, Camera2D camera2D, ChunkManager chunkManager, EnemyManager enemyManager)
=======
        public void Draw(SpriteBatch spriteBatch, SpriteFont smallFont, Player player1, Player player2, Camera2D camera2D, ChunkManager chunkManager)
>>>>>>> origin/devLatest
        {
            switch (curScreen)
            {
                case Screen.splash: SplashDraw(spriteBatch); break;
                case Screen.mainMenu: MainMenuDraw(spriteBatch, smallFont); break;
                case Screen.pauseSettings: SettingsDraw(spriteBatch, smallFont); break;
                case Screen.menuSettings: SettingsDraw(spriteBatch, smallFont); break;
                case Screen.playerSelect: PlayerSelectDraw(spriteBatch, smallFont); break;
<<<<<<< HEAD
                case Screen.gamePlay: GamePlayDraw(spriteBatch, smallFont, player, chunkManager, camera2D, enemyManager); break;
=======
                case Screen.gamePlay: GamePlayDraw(spriteBatch, smallFont, player1, player2, chunkManager, camera2D); break;
>>>>>>> origin/devLatest
                case Screen.pause: PauseDraw(spriteBatch, smallFont); break;
                case Screen.lost: LostDraw(spriteBatch); break;
                case Screen.won: WonDraw(spriteBatch); break;
            }
        }

        private void SplashUpdate(AudioManager audio)
        {
            //Normal Controls
            if (newPad1State.Buttons.A == ButtonState.Pressed && oldPad1State.Buttons.A != ButtonState.Pressed) curScreen = Screen.mainMenu;

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

        private void MainMenuUpdate(AudioManager audio)
        {
            //Normal Controls
            if (newPad1State.Buttons.A == ButtonState.Pressed && oldPad1State.Buttons.A != ButtonState.Pressed) curScreen = Screen.playerSelect;
            if (newPad1State.Buttons.Y == ButtonState.Pressed && oldPad1State.Buttons.Y != ButtonState.Pressed) curScreen = Screen.menuSettings;
            if (newPad1State.Buttons.Back == ButtonState.Pressed && oldPad1State.Buttons.Back != ButtonState.Pressed) gameExit = true;

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


        private void MenuSettingsUpdate(AudioManager audio)
        {
            //Normal Controls
            if (newPad1State.Buttons.A == ButtonState.Pressed && oldPad1State.Buttons.A != ButtonState.Pressed) { } //Volume Down
            if (newPad1State.Buttons.Y == ButtonState.Pressed && oldPad1State.Buttons.Y != ButtonState.Pressed) { } // Volume Up
            if (newPad1State.Buttons.B == ButtonState.Pressed && oldPad1State.Buttons.B != ButtonState.Pressed) curScreen = Screen.mainMenu;

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


<<<<<<< HEAD
        private void PlayerSelectUpdate(ChunkManager chunkManager, EnemyManager enemyManager)
=======
        private void PlayerSelectUpdate(AudioManager audio)
>>>>>>> origin/devLatest
        {
            //Normal Controls
            if (newPad1State.Buttons.A == ButtonState.Pressed && oldPad1State.Buttons.A != ButtonState.Pressed) curScreen = Screen.gamePlay;
            if (newPad1State.Buttons.B == ButtonState.Pressed && oldPad1State.Buttons.B != ButtonState.Pressed) curScreen = Screen.mainMenu;

            //DebugToggle Controls
            if (debugToggle)
            {
                if (newKState.IsKeyDown(Keys.E) && !oldKState.IsKeyDown(Keys.E))
                {
                    chunkManager.CreateInitialArea(enemyManager);
                    curScreen = Screen.gamePlay;
                }
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


<<<<<<< HEAD
        private void GamePlayUpdate(Player player, GameTime gameTime, Camera2D camera2D, List<Wolf> wolves, List<Bear> bears, ChunkManager chunkManager, AudioManager audioManager, EnemyManager enemyManager)
=======
        private void GamePlayUpdate(Player player1, Player player2, GameTime gameTime, Camera2D camera2D, List<Wolf> wolves, List<Bear> bears, ChunkManager chunkManager, AudioManager audioManager)
>>>>>>> origin/devLatest
        {
            player1.Update(gameTime, oldPad1State, newPad1State, camera2D, wolves, bears);
            player2.Update(gameTime, oldPad2State, newPad2State, camera2D, wolves, bears);

            camera2D.Target = (player1.origin + player2.origin) / 2;

            chunkManager.Update(camera2D.Target, enemyManager);
            enemyManager.Update(gameTime, player, player, chunkManager, audioManager);

            //Normal control info
            if (newPad1State.Buttons.Start == ButtonState.Pressed && oldPad1State.Buttons.Start != ButtonState.Pressed) curScreen = Screen.pause;

            //DebugToggle control info
            if (newKState.IsKeyDown(Keys.Space) && !oldKState.IsKeyDown(Keys.Space)) curScreen = Screen.pause;

            
            camera2D.UpdateViewPort(Vector2.Zero);

        }
<<<<<<< HEAD
        private void GamePlayDraw(SpriteBatch spriteBatch, SpriteFont smallFont, Player player, ChunkManager chunkManager, Camera2D camera2D, EnemyManager enemyManager)
=======
        private void GamePlayDraw(SpriteBatch spriteBatch, SpriteFont smallFont, Player player1, Player player2, ChunkManager chunkManager, Camera2D camera2D)
>>>>>>> origin/devLatest
        {
            //GraphicsDevice.Clear(Color.CornflowerBlue);

            //Draw calls are fully managed on Chunk Manager, including necessary overloads to SpriteBatch.
            //DO NOT USE spriteBatch.Begin() or .End(); THEY ARE NOT REQUIRED.
            chunkManager.Draw(spriteBatch, camera2D);
            enemyManager.Draw(spriteBatch, camera2D);
            //DO NOT USE spriteBatch.Begin() or .End(); THEY ARE NOT REQUIRED.


<<<<<<< HEAD
            spriteBatch.Begin(SpriteSortMode.BackToFront, BlendState.AlphaBlend, null, null, null, null, camera2D.GetViewPortMatrix);
            player.NewDraw(spriteBatch);
=======
            player1.Draw(spriteBatch);
            player2.Draw(spriteBatch);
            spriteBatch.End();

            spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, null, null, null, null, camera2D.GetViewPortMatrix);

            spriteBatch.Draw(player1Portrait, new Rectangle(camera2D.ViewPortRect.X + 10, camera2D.ViewPortRect.Y + 10, 80, 80), Color.White);
            for (int i = 0; i < player1.curHealth; i++)
            {
                spriteBatch.Draw(curHpPic, new Rectangle(camera2D.ViewPortRect.X + 100 + (35 * i), camera2D.ViewPortRect.Y + 10, 30, 30), Color.White);
            }
            for (int i = 0; i < player1.maxHealth; i++)
            {
                spriteBatch.Draw(maxHpPic, new Rectangle(camera2D.ViewPortRect.X + 100 + (35 * i), camera2D.ViewPortRect.Y + 10, 30, 30), Color.White);
            }
            for (int i = 0; i < player1.attack; i++)
            {
                spriteBatch.Draw(damagePic, new Rectangle(camera2D.ViewPortRect.X + 100 + (35 * i), camera2D.ViewPortRect.Y + 40, 30, 30), Color.White);
            }
            for (int i = 0; i < player1.defense; i++)
            {
                spriteBatch.Draw(defensePic, new Rectangle(camera2D.ViewPortRect.X + 100 + (35 * i), camera2D.ViewPortRect.Y + 70, 30, 30), Color.White);
            }

            spriteBatch.Draw(player2Portrait, new Rectangle(camera2D.ViewPortRect.X + camera2D.ViewPortRect.Width - 90,
                                                                camera2D.ViewPortRect.Y + 10, 80, 80), Color.White);
            for (int i = player2.curHealth; i > 0; i--)
            {
                spriteBatch.Draw(curHpPic, new Rectangle(camera2D.ViewPortRect.X + camera2D.ViewPortRect.Width - 100 - (35 * i), 
                                                             camera2D.ViewPortRect.Y + 10, 30, 30), Color.White);
            }
            for (int i = player2.curHealth; i > 0; i--)
            {
                spriteBatch.Draw(maxHpPic, new Rectangle(camera2D.ViewPortRect.X + camera2D.ViewPortRect.Width - 100 - (35 * i), 
                                                             camera2D.ViewPortRect.Y + 10, 30, 30), Color.White);
            }
            for (int i = player2.attack; i > 0; i--)
            {
                spriteBatch.Draw(damagePic, new Rectangle(camera2D.ViewPortRect.X + camera2D.ViewPortRect.Width - 100 - (35 * i),
                                                             camera2D.ViewPortRect.Y + 40, 30, 30), Color.White);
            }
            for (int i = player2.defense; i > 0; i--)
            {
                spriteBatch.Draw(defensePic, new Rectangle(camera2D.ViewPortRect.X + camera2D.ViewPortRect.Width - 100 - (35 * i),
                                                             camera2D.ViewPortRect.Y + 70, 30, 30), Color.White);
            }

>>>>>>> origin/devLatest
            spriteBatch.End();
        }


        private void PauseUpdate(AudioManager audio)
        {
            //Normal Controls
            if (newPad1State.Buttons.B == ButtonState.Pressed && oldPad1State.Buttons.B != ButtonState.Pressed) curScreen = Screen.gamePlay;
            if (newPad1State.Buttons.Y == ButtonState.Pressed && oldPad1State.Buttons.Y != ButtonState.Pressed) curScreen = Screen.pauseSettings;
            if (newPad1State.Buttons.Back == ButtonState.Pressed && oldPad1State.Buttons.Back != ButtonState.Pressed) gameExit = true;

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


        private void PauseSettingsUpdate(AudioManager audio)
        {
            //Normal Controls
            if (newPad1State.DPad.Left == ButtonState.Pressed) 
            { 
                audio.MusicVolume -= 0.1f;
                audio.SfxVolume -= 0.1f;
            }
            if (newPad1State.DPad.Right == ButtonState.Pressed)
            {
                audio.MusicVolume += 0.1f;
                audio.SfxVolume += 0.1f;
            }
            if (newPad1State.Buttons.B == ButtonState.Pressed && oldPad1State.Buttons.B != ButtonState.Pressed) curScreen = Screen.pause;

            //DebugToggle Controls
            if (debugToggle)
            {
                if (newKState.IsKeyDown(Keys.E) && !oldKState.IsKeyDown(Keys.E)) { }   //Volume down
                if (newKState.IsKeyDown(Keys.R) && !oldKState.IsKeyDown(Keys.R)) { }   //Volume up
                if (newKState.IsKeyDown(Keys.Q) && !oldKState.IsKeyDown(Keys.Q)) curScreen = Screen.pause;
            }
        }


        private void EndUpdate(AudioManager audio)
        {
            //Normal Controls
            if (newPad1State.Buttons.A == ButtonState.Pressed && oldPad1State.Buttons.A != ButtonState.Pressed) curScreen = Screen.mainMenu;

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
