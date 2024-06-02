using LibVLCSharp.Shared;
using LibVLCSharp.WinForms;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows;

namespace VideoSplashScreenDemo
{
    public partial class SplashForm : Form
    {

        LibVLC _libVLC;
        MediaPlayer _mediaPlayer;

        private bool firstPlay = true;


        public SplashForm( )
        {
            InitializeComponent();
        }


        public event EventHandler SplashDoneSignal = delegate { };

        protected virtual void OnSplashDone(EventArgs e)
        {
            SplashDoneSignal?.Invoke(this, e);
        }

        private void SplashForm_Load(object sender, EventArgs e)
        {
            Core.Initialize();

            _libVLC = new LibVLC();
            _mediaPlayer = new MediaPlayer(_libVLC);

            videoView1.MediaPlayer = _mediaPlayer;
            _mediaPlayer.EndReached += mediaplayer_EndReached;

            _mediaPlayer.Play(new Media(_libVLC, new Uri("file:///./video.mp4")));
        }


        private void mediaplayer_EndReached(object sender, EventArgs e)
        {
            if (firstPlay) {
                firstPlay = false;
                OnSplashDone(EventArgs.Empty);
            }
        }


        private void SplashForm_Resize(object sender, EventArgs e)
        {
            videoView1.SetBounds(0, 0, this.Bounds.Width, this.Bounds.Height);
        }


        public void RefocusAndPlay()
        {
            this.WindowState = FormWindowState.Maximized;
            this.Activate();
            this.Focus();
            _mediaPlayer.Play(new Media(_libVLC, new Uri("file:///./video.mp4")));
        }

    }
}
