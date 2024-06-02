using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace VideoSplashScreenDemo
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

            AutoResetEvent firstSplashDoneEvent = new AutoResetEvent(false);

            Action splashdonecallback = () =>
            {
                // Signal the main thread that SplashForm has comple
                firstSplashDoneEvent.Set();
            };

            // Create a new thread for SplashForm
            var splashForm = new SplashForm();
            
            splashForm.SplashDoneSignal += (EventHandler)((sender, e) => splashdonecallback());
            var splashThread = new Thread(() =>
            {   
                Application.Run(splashForm); // Run SplashForm on the thread
            });

            // Set the apartment state to STA for UI thread compatibility
            splashThread.SetApartmentState(ApartmentState.STA);
            splashThread.Start();

            firstSplashDoneEvent.WaitOne();

            // Now that SplashForm is completed its action, show MainWindow
            MainWindow mainwin = new MainWindow(splashForm);
            Application.Run(mainwin);
        }

    }
}
