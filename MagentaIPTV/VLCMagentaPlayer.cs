using LibVLCSharp.Shared;
using LibVLCSharp.WPF;
using System;
using System.Threading.Tasks;
using System.Windows;

namespace MagentaIPTV
{
    public class VLCMagentaPlayer
    {
        public event EventHandler PlaybackChanged;

        public static VLCMagentaPlayer Instance { get; private set; } = new VLCMagentaPlayer();

        public VideoView VideoView;
        public LibVLC LibVLC { get; private set; }
        private MediaPlayer mediaPlayer;

        public static string SDP = "sdp://" +
                            "v=0\n" +
                            "o=- 0 0 IN IP4 127.0.0.1\n" +
                            "s=-\n" +
                            "c=IN IP4 127.0.0.1\n" +
                            "t=0 0\n" +
                            "m=video 5004 RTP/AVP 96\n" +
                            "a=rtpmap:96 VP8/90000";

        public VLCMagentaPlayer()
        {
            if (MainWindow.VideoViewInstance != null)
            {
                this.VideoView = MainWindow.VideoViewInstance;
                this.LibVLC = new LibVLC();
                this.mediaPlayer = new MediaPlayer(LibVLC);
                this.VideoView.MediaPlayer = this.mediaPlayer;
            }
        }




        public bool IsPaused() => this.VideoView != null && !this.VideoView.MediaPlayer.IsPlaying;

        public void Pause()
        {
            this.VideoView.MediaPlayer.Pause();
            PlaybackChanged?.Invoke(this, EventArgs.Empty);
        }

        public int Volume
        {
            get { return this.VideoView.MediaPlayer.Volume; }
            set { this.VideoView.MediaPlayer.Volume = value; } 
        }

        public bool HasMedia()
        {
            return this.VideoView.MediaPlayer.Media != null;
        }

        public Media GetMedia()
        {
            return this.VideoView.MediaPlayer.Media;
        }

        public void PlayMedia(Media media)
        {
            Core.Initialize();
            this.VideoView.MediaPlayer.Play(media);
            PlaybackChanged?.Invoke(this, EventArgs.Empty);
        }

        public void ToggleMute()
        {
            this.VideoView.MediaPlayer.ToggleMute();
        }

        public bool IsMuted() => this.VideoView.MediaPlayer.Mute;



        public void Stop()
        {
            this.VideoView.MediaPlayer.Stop();
        }
    }
}
