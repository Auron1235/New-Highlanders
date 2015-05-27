using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Map_Generator
{
    class Sprite
    {
        public Vector2 position;
        public Vector2 velocity;
        public Texture2D image;
        public Rectangle boundingBox;
        public Rectangle boundingFootprint;

        public enum AnimationState
        {
            Idle,
            WalkingLeft,
            WalkingRight,
            Attacking,
            Dying,
            Dead
        }
        public static AnimationState curAnimState = AnimationState.Idle;

        private List<Rectangle> mFrames = new List<Rectangle>();
        private int mCurrentFrame;
        private float mFrameTime = 0.0833f;
        private float mTimeForCurrentFrame;

        #region CONSTRUCTORS

        public Sprite()
        { }
        //public Sprite(Vector2 initialWorldLocation, Texture2D initialSpriteSheet,
        //    Rectangle initialFrame, Vector2 initialVelocity)
        //{
        //    position = initialWorldLocation;
        //    image = initialSpriteSheet;
        //    mFrames.Add(initialFrame);
        //    velocity = initialVelocity;
        //}
        #endregion

        #region GETSETS -DRAWING AND ANIMATION
        public int FrameWidth
        {
            get { return mFrames[0].Width; }
        }
        public int FrameHeight
        {
            get { return mFrames[0].Height; }
        }
        public int Frame
        {
            get { return mCurrentFrame; }
            set { mCurrentFrame = (int)MathHelper.Clamp(value, 0, mFrames.Count - 1); }
        }
        public float FrameTime
        {
            get { return mFrameTime; }
            set { mFrameTime = MathHelper.Clamp(0, value, mFrames.Count - 1); }
        }
        public Rectangle Source
        {
            get { return mFrames[mCurrentFrame]; }
            set { mFrames[mCurrentFrame] = value; }
        }
        #endregion


        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(image, boundingBox, Color.White);
        }
    }
}