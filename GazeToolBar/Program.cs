using EyeXFramework.Forms;
using System;
using System.Windows.Forms;
using System.IO;
using Newtonsoft.Json;
using System.Threading;
using System.Collections.Generic;
using System.Linq;



namespace GazeToolBar
{
    static class Program
    {
        public static string path { get; set; }
        public static Settings readSettings { get; set; }
        public static bool onStartUp { get; set; }

        static Mutex mutex = new Mutex(true, "51427aea-a311-11e7-abc4-cec278b6b50a");

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            if (mutex.WaitOne(TimeSpan.Zero, true))
            {

                var roamingDirectory = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
                path = Path.Combine(roamingDirectory, "GazeToolBar\\Settings.json");

                System.IO.FileInfo file = new System.IO.FileInfo(path);
                file.Directory.Create(); // If the directory already exists, this method does nothing.

                readSettings = new Settings(path);//Load settings
                onStartUp = AutoStart.IsOn();
                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);
                Application.Run(new Form1());

                mutex.ReleaseMutex();
            }
            else
            {
                MessageBox.Show("There is already an instance of Gaze Toolbar running!");
            }

        }
    }
}
