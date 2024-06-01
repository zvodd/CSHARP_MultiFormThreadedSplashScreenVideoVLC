using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RemoteLog
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

        private void MainWindow_Closing(object sender, EventArgs e)
        {
            this.Invoke(new Action(() => _splashForm.OnRequestClose(e)));
        }
        private void MainWindow_Leave(object sender, EventArgs e)
        {
            Console.WriteLine("did we lose main form focus?");
            this.Invoke(new Action(() => _splashForm.OnRequestRefocus(e)));
        }

        private void MainWindow_Load(object sender, EventArgs e)
        {
            WindowState = FormWindowState.Maximized;
            this.Activate();
            this.Focus();
        }

        private void switchToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Invoke(new Action(() => _splashForm.OnRequestRefocus(e)));
        }

        private void MainWindow_Deactivate(object sender, EventArgs e)
        {
            this.Invoke(new Action(() => _splashForm.OnRequestRefocus(e)));
        }
    }
}
