using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework;

namespace Map_Generator
{
    class Settings
    {

        //float sfxVolume;
        //float musicVolume;

        public void VolumeSettings(float volume)
        {
            if (Keyboard.GetState().IsKeyDown(Keys.Left))
            {
                volume -= 0.1f;
            }
            if (Keyboard.GetState().IsKeyDown(Keys.Right))
            {
                volume += 0.1f;
            }
            if (GamePad.GetState(PlayerIndex.One).DPad.Right == ButtonState.Pressed)
            {
                volume += 0.1f;
            }
            if (GamePad.GetState(PlayerIndex.One).DPad.Left == ButtonState.Pressed)
            {
                volume -= 0.1f;
            }            
        }


    }
}
