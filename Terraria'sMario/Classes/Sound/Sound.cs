using System;
using System.IO;
using Terraria_sMario.Classes.Sound;
using Terraria_sMario.Sound_res;

namespace Visual_Novel_Engine
{
    class Sound 
    {
        private WMPLib.WindowsMediaPlayer wplayer = new WMPLib.WindowsMediaPlayer();
        public bool isPlaying { get; private set; } = false;
        public SoundType type { get; private set; }

        public Sound(string URL, SoundType type = SoundType.Sound)
        {

            wplayer.controls.stop();

            this.type = type;

            if (type == SoundType.Music) 
                wplayer.settings.playCount = 5;
        }

        private void setVolume()
        {
            if (type == SoundType.Music)
            {
                wplayer.settings.volume = 10;
            }
            else if (type == SoundType.AmbientSound)
            {
                wplayer.settings.volume = 10;
            }
            else if (type == SoundType.Sound)
            {
                wplayer.settings.volume = 10;
            }
        }

        public void Play()
        {
            wplayer.controls.play();
            isPlaying = true;
        }
        public void Stop()
        {
            wplayer.controls.stop();
            isPlaying = false;
        }
        public void Pause()
        {
            wplayer.controls.pause();
            isPlaying = false;
        }
    }
}
