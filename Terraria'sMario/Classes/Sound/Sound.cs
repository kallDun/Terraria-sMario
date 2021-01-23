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

        public Sound(string URL_, SoundType type = SoundType.Sound)
        {
            wplayer.URL = $@"{Environment.CurrentDirectory}\{URL_}";
            wplayer.controls.stop();

            this.type = type;

            if (type == SoundType.Music) 
                wplayer.settings.playCount = 5;
        }

        private void setVolume()
        {
            wplayer.settings.volume =
                type == SoundType.Music ? SoundParameters.music_volume_level :
                type == SoundType.AmbientSound ? SoundParameters.ambient_volume_level :
                type == SoundType.Sound ? SoundParameters.sound_volume_level : 100;
        }

        public void Play()
        {
            if (wplayer.playState == WMPLib.WMPPlayState.wmppsPlaying) return;

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
