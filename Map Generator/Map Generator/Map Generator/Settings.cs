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

        public void MusicSettings(AudioManager audio)
        {
            if (GamePad.GetState(PlayerIndex.One).DPad.Right == ButtonState.Pressed)
            {
                audio.MusicVolume += 0.1f;
            }
            if (GamePad.GetState(PlayerIndex.One).DPad.Left == ButtonState.Pressed)
            {
                audio.MusicVolume -= 0.1f;
            }

            
        }


    }
}
