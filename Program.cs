using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RemoteLog
{
    internal static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            AutoResetEvent readyEvent = new AutoResetEvent(false);

            Action readyCallback = () =>
            {
                // Signal the main thread that SplashForm is ready
                readyEvent.Set();
            };

            // Create a new thread for SplashForm
            var splashForm = new SplashForm(readyCallback);
            var splashThread = new Thread(() =>
            {   
                Application.Run(splashForm); // Run SplashForm on the thread
            });

            // Set the apartment state to STA for UI thread compatibility
            splashThread.SetApartmentState(ApartmentState.STA);
            splashThread.Start();

            readyEvent.WaitOne();

            // Now that SplashForm is ready, show Form2
            MainWindow mainwin = new MainWindow(splashForm);
            Application.Run(mainwin);
        }

    }
}
