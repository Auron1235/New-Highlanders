using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
//using Microsoft.Xna.Framework.Media;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;

namespace Map_Generator
{
    public class AudioManager
    {
        #region DECLARATIONS
        private float mSfxVolume;
        private float mMusicVolume;

        private const float mDefaultSfxVolume = 0.3f;
        private const float mDefaultMusicVolume = 0.8f;

        private List<SoundEffect> mMusicTracks;
        private SoundEffectInstance mCurrentMusic;

        private List<SoundEffect> FootSteps = new List<SoundEffect>();
        private List<SoundEffect> HitSounds = new List<SoundEffect>();
        private List<SoundEffect> SwooshSounds = new List<SoundEffect>();
        private List<SoundEffect> DropSounds = new List<SoundEffect>();
        private List<SoundEffect> WolfSounds = new List<SoundEffect>();
        private List<SoundEffect> BearSounds = new List<SoundEffect>();

        private SoundEffect MenuBack;
        private SoundEffect MenuSelect;

        #endregion

        #region GET SETS
        public float SfxVolume
        {
            get { return mSfxVolume; }
            set { mSfxVolume = value; }
        }
        public float MusicVolume
        {
            get { return mMusicVolume; }
            set { mMusicVolume = value; }
        }
        public SoundEffectInstance CurrentMusic
        {
            get { return mCurrentMusic; }
            //set { mCurrentMusic = value; }
        }
        public List<SoundEffect> MusicTracks
        {
            get { return mMusicTracks; }
            //set { mMusicTracks = value; }
        }
        public List<SoundEffect> mFootSteps
        {
            get { return FootSteps; }
            //set { FootSteps = value; }
        }
        public List<SoundEffect> mHitSounds
        {
            get { return HitSounds; }
            //set { HitSounds = value; }
        }
        public List<SoundEffect> mSwooshSounds
        {
            get { return SwooshSounds; }
            //set { SwooshSounds = value; }
        }
        public List<SoundEffect> mDropSounds
        {
            get { return DropSounds; }
            //set { DropSounds = value; }
        }
        public SoundEffect mMenuBack
        {
            get { return MenuBack; }
            set { MenuBack = value; }
        }
        public SoundEffect mMenuSelect
        {
            get { return MenuSelect; }
            set { MenuSelect = value; }
        }
        public List<SoundEffect> mWolfSounds
        {
            get { return WolfSounds; }
            //set { WolfSounds = value; }
        }
        public List<SoundEffect> mBearSounds
        {
            get { return BearSounds; }
            //set { BearSounds = value; }
        }
        #endregion

        //CONSTRUCTOR
        public AudioManager()
        {
            mSfxVolume = mDefaultSfxVolume;
            mMusicVolume = mDefaultMusicVolume;
            mMusicTracks = new List<SoundEffect>();
        }

        public void Initialize(ContentManager Content)
        {
            AudioLoadContent(Content);

            // sets the theme as the current track and plays it, the theme will play on launch.
            mCurrentMusic = mMusicTracks[0].CreateInstance();
            mCurrentMusic.IsLooped = true;
            mCurrentMusic.Play();
        }

        public void AudioLoadContent(ContentManager Content)
        {

            //Music
            mMusicTracks.Add(Content.Load<SoundEffect>("Music/Theme"));
            mMusicTracks.Add(Content.Load<SoundEffect>("Music/BattleHunger"));
            mMusicTracks.Add(Content.Load<SoundEffect>("Music/GoFourth"));
            mMusicTracks.Add(Content.Load<SoundEffect>("Music/TheGloomyMorning"));
            mMusicTracks.Add(Content.Load<SoundEffect>("Music/TheRoyalKingdom"));
            //Sound effects - footsteps
            for (int i = 0; i < 9; i++)
            {
                FootSteps.Add(Content.Load<SoundEffect>("Sound Effects/Footsteps/footstep" + i.ToString()));
            }
            //sound effects - hitsounds
            for (int i = 0; i < 35; i++)
            {
                HitSounds.Add(Content.Load<SoundEffect>("Sound Effects/HitSounds/hit" + i.ToString()));
            }
            //Sound effects - Swoosh
            for (int i = 0; i < 25; i++)
            {
                SwooshSounds.Add(Content.Load<SoundEffect>("Sound Effects/Swoosh/swosh-" + i.ToString()));
            }
            //Item Drop Sounds
            mDropSounds.Add(Content.Load<SoundEffect>("Sound Effects/Ui and Drops/bubble1"));
            mDropSounds.Add(Content.Load<SoundEffect>("Sound Effects/Ui and Drops/bubble2"));
            mDropSounds.Add(Content.Load<SoundEffect>("Sound Effects/Ui and Drops/itemPickUp"));
            mDropSounds.Add(Content.Load<SoundEffect>("Sound Effects/Ui and Drops/shieldDrop"));
            mDropSounds.Add(Content.Load<SoundEffect>("Sound Effects/Ui and Drops/swordDrop"));

            //Ui Sounds
            mMenuBack = Content.Load<SoundEffect>("Sound Effects/Ui and Drops/menuBack");
            mMenuSelect = Content.Load<SoundEffect>("Sound Effects/Ui and Drops/menuSelect");

            //Wolf Sounds
            for (int i = 0; i < 5; i++)
            {
                mWolfSounds.Add(Content.Load<SoundEffect>("Sound Effects/WolfSounds/Growl" + i.ToString()));
            }
            mWolfSounds.Add(Content.Load<SoundEffect>("Sound Effects/WolfSounds/Howl"));
            mWolfSounds.Add(Content.Load<SoundEffect>("Sound Effects/WolfSounds/Whine"));

            //Bear Sounds
            for (int i = 0; i < 3; i++)
            {
                mBearSounds.Add(Content.Load<SoundEffect>("Sound Effects/BearSounds/BearGrowl" + i.ToString()));
            }

            mBearSounds.Add(Content.Load<SoundEffect>("Sound Effects/BearSounds/BearHowl"));
            mBearSounds.Add(Content.Load<SoundEffect>("Sound Effects/BearSounds/BearWhine"));
        }

        public void PlaySfx(SoundEffect soundEffect)
        {
            PlaySfx(soundEffect.CreateInstance());
        }
        public void PlaySfx(SoundEffectInstance soundEffectInstance)
        {
            soundEffectInstance.Volume = mSfxVolume;
            soundEffectInstance.IsLooped = false;
            soundEffectInstance.Play();
        }

        public void PlayMusic(SoundEffect musicTrack)
        {
            PlayMusic(musicTrack.CreateInstance());
        }
        public void PlayMusic(SoundEffectInstance musicTrack)
        {
            mCurrentMusic = musicTrack;
            mCurrentMusic.Volume = mMusicVolume;
            mCurrentMusic.IsLooped = true;
            mCurrentMusic.Play();
        }
    }
}
