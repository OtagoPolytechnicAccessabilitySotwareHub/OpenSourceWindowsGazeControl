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
        //public static SettingJSON readSettings { get; set; }
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

                //path = Application.StartupPath + "\\" + "Settings.json";
                //ReadWriteJson();
                readSettings = new Settings(path);
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

        //public static void ReadWriteJson()
        //{
        //    if (!File.Exists(path))
        //    {
        //        SettingJSON defaultSetting = new SettingJSON();
        //        defaultSetting.fixationTimeLength = Constants.DEFAULT_TIME_LENGTH;
        //        defaultSetting.fixationTimeOut = Constants.DEFAULT_TIME_OUT;
        //        defaultSetting.leftClick = Constants.KEY_FUNCTION_UNASSIGNED_MESSAGE;
        //        defaultSetting.doubleClick = Constants.KEY_FUNCTION_UNASSIGNED_MESSAGE;
        //        defaultSetting.rightClick = Constants.KEY_FUNCTION_UNASSIGNED_MESSAGE;
        //        defaultSetting.scroll = Constants.KEY_FUNCTION_UNASSIGNED_MESSAGE;
        //        defaultSetting.micInput = Constants.KEY_FUNCTION_UNASSIGNED_MESSAGE;
        //        defaultSetting.micInputOff = Constants.KEY_FUNCTION_UNASSIGNED_MESSAGE;
        //        defaultSetting.sidebar = new string[] { "right_click", "left_click", "double_left_click", "scroll", "keyboard", "settings" };
        //        defaultSetting.maxZoom = 3;
        //        defaultSetting.Crosshair = 3;
        //        defaultSetting.zoomWindowSize = 10;
        //        defaultSetting.stickyLeftClick = false;
        //        defaultSetting.selectionFeedback = true;
        //        defaultSetting.dynamicZoom = false;
        //        string JSONstr = JsonConvert.SerializeObject(defaultSetting);
        //        File.AppendAllText(path, JSONstr);

        //        //readSettings = defaultSetting;
        //    }
        //    else
        //    {
        //        string s = File.ReadAllText(path);
        //        //readSettings = JsonConvert.DeserializeObject<SettingJSON>(s);
        //        Console.WriteLine("Settings loaded");
        //    }
        //}
    }
}
