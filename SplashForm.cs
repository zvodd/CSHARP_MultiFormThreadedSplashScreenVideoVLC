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

namespace RemoteLog
{
    public partial class SplashForm : Form
    {
        private Action mainThreadreadyCallback;

        LibVLC _libVLC;
        MediaPlayer _mediaPlayer;
        public SplashForm( Action mainThreadreadyCallback)
        {
            this.mainThreadreadyCallback = mainThreadreadyCallback;
            InitializeComponent();

        }

        private void SplashForm_Load(object sender, EventArgs e)
        {
            Core.Initialize();

            _libVLC = new LibVLC();
            _mediaPlayer = new MediaPlayer(_libVLC);

            videoView1.MediaPlayer = _mediaPlayer;

            _mediaPlayer.Play(new Media(_libVLC, new Uri("file:///./video.mp4")));

            _mediaPlayer.EndReached += firstPlayEndReached;
        }


        void firstPlayEndReached(object sender, EventArgs e)
        {
            _mediaPlayer.EndReached -= firstPlayEndReached;
            OnSplashDone(new EventArgs());
        }


        public event EventHandler SplashDoneSignal = delegate { };

        protected virtual void OnSplashDone(EventArgs e)
        {
            if (this.InvokeRequired)
            {
                this.Invoke(new Action(() => this.OnSplashDone(e)));
            }
            else
            {
                SplashDoneSignal?.Invoke(this, e);

                mainThreadreadyCallback();
                this.WindowState = FormWindowState.Minimized;
            }
        }

        public event EventHandler RequestRefocusSignal = delegate { };
        public virtual void OnRequestRefocus(EventArgs e)
        {
            if (this.InvokeRequired)
            {
                this.Invoke(new Action(() => this.OnRequestRefocus(e)));
            }
            else
            {
                this.WindowState = FormWindowState.Maximized;
                this.Activate();
                this.Focus();
                _mediaPlayer.Play(new Media(_libVLC, new Uri("file:///./video.mp4")));
                _mediaPlayer.EndReached += firstPlayEndReached;
            }
        }

        public event EventHandler RequestCloseSignal = delegate { };
        public virtual void OnRequestClose(EventArgs e)
        {
            if (this.InvokeRequired)
            {
                this.Invoke(new Action(() => this.OnRequestClose(e)));
            }
            else
            {
                this.Close();
            }
        }

        private void SplashForm_Resize(object sender, EventArgs e)
        {
            videoView1.SetBounds(0, 0, this.Bounds.Width, this.Bounds.Height);
        }


    }
}
