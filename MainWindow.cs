using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace VideoSplashScreenDemo
{
    public partial class MainWindow : Form
    {
        SplashForm _splashForm;
        public MainWindow(SplashForm splashForm)
        {
            _splashForm = splashForm;
            InitializeComponent();
            Closing += MainWindow_Closing;
        }


        private void PullFocus()
        {
            WindowState = FormWindowState.Maximized;
            this.Invoke(new Action(() => _splashForm.WindowState = FormWindowState.Minimized));
            this.Activate();
            this.Focus();
        }

        private void MainWindow_Closing(object sender, EventArgs e)
        {
            this.Invoke(new Action(() => _splashForm.Close()));
        }
        private void MainWindow_Leave(object sender, EventArgs e)
        {
            Console.WriteLine("did we lose main form focus?");
            this.Invoke(new Action(() => _splashForm.RefocusAndPlay()));
        }

        private void MainWindow_Load(object sender, EventArgs e)
        {
            PullFocus();
        }

        private void switchToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Invoke(new Action(() => _splashForm.RefocusAndPlay()));
        }

        private void MainWindow_Deactivate(object sender, EventArgs e)
        {
            this.Invoke(new Action(() => _splashForm.RefocusAndPlay()));
        }
    }
}
