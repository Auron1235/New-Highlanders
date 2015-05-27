using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
//using Microsoft.Xna.Framework.Media;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;

namespace Map_Generator
{
    class AudioManager
    {
        #region DECLARATIONS
        private float mSfxVolume;
        private float mMusicVolume;

        private const float mDefaultSfxVolume = 0.6f;
        private const float mDefaultMusicVolume = 0.6f;

        private List<SoundEffect> mMusicTracks;
        private SoundEffectInstance mCurrentMusic;

        private SoundEffect Theme;
        private SoundEffect BattleHunger;
        private SoundEffect GoFourth;
        private SoundEffect TheGloomyMorning;
        private SoundEffect TheRoyalKingdom;
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
            Theme = Content.Load<SoundEffect>("Music/Theme");
            BattleHunger = Content.Load<SoundEffect>("Music/BattleHunger");
            GoFourth = Content.Load<SoundEffect>("Music/GoFourth");
            TheGloomyMorning = Content.Load<SoundEffect>("Music/TheGloomyMorning");
            TheRoyalKingdom = Content.Load<SoundEffect>("Music/TheRoyalKingdom");

            mMusicTracks.Add(Theme);
            mMusicTracks.Add(BattleHunger);
            mMusicTracks.Add(GoFourth);
            mMusicTracks.Add(TheGloomyMorning);
            mMusicTracks.Add(TheRoyalKingdom);
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
