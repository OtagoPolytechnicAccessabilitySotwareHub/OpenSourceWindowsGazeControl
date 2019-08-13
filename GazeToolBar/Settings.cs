using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using Newtonsoft.Json;
using System.Threading;
namespace GazeToolBar
{
    class Settings
    {
        public int fixationTimeLength { get; set; }
        public int fixationTimeOut { get; set; }
        public string leftClick { get; set; }
        public string doubleClick { get; set; }
        public string rightClick { get; set; }
        public string scroll { get; set; }
        public string micInput { get; set; }
        public string micInputOff { get; set; }
        public string[] sidebar { get; set; }
        public int maxZoom { get; set; }
        public int Crosshair { get; set; }
        public int zoomWindowSize { get; set; }
        public bool stickyLeftClick { get; set; }
        public bool selectionFeedback { get; set; }
        public bool dynamicZoom { get; set; }

        SettingJSON settingsJson;
        public Settings(string path)
        {
            if (!File.Exists(path))
            {
                fixationTimeLength = Constants.DEFAULT_TIME_LENGTH;
                fixationTimeOut = Constants.DEFAULT_TIME_OUT;
                leftClick = Constants.KEY_FUNCTION_UNASSIGNED_MESSAGE;
                doubleClick = Constants.KEY_FUNCTION_UNASSIGNED_MESSAGE;
                rightClick = Constants.KEY_FUNCTION_UNASSIGNED_MESSAGE;
                scroll = Constants.KEY_FUNCTION_UNASSIGNED_MESSAGE;
                micInput = Constants.KEY_FUNCTION_UNASSIGNED_MESSAGE;
                micInputOff = Constants.KEY_FUNCTION_UNASSIGNED_MESSAGE;
                sidebar = new string[] { "right_click", "left_click", "double_left_click", "scroll", "keyboard", "settings" };
                maxZoom = 3;
                Crosshair = 3;
                zoomWindowSize = 10;
                stickyLeftClick = false;
                selectionFeedback = true;
                dynamicZoom = false;
            }
            else
            {
                string s = File.ReadAllText(path);
                settingsJson = JsonConvert.DeserializeObject<SettingJSON>(s);
                fixationTimeLength = settingsJson.fixationTimeLength;
                fixationTimeOut = settingsJson.fixationTimeOut;
                leftClick = settingsJson.leftClick;
                doubleClick = settingsJson.doubleClick;
                rightClick = settingsJson.rightClick;
                scroll = settingsJson.scroll;
                micInput = settingsJson.micInput;
                micInputOff = settingsJson.micInputOff;
                sidebar = settingsJson.sidebar;
                maxZoom = settingsJson.maxZoom;
                Crosshair = settingsJson.Crosshair;
                zoomWindowSize = settingsJson.zoomWindowSize;
                stickyLeftClick = settingsJson.stickyLeftClick;
                selectionFeedback = settingsJson.selectionFeedback;
                //dynamicZoom = settingsJson.dynamicZoom;                                  //Changed for testing
                dynamicZoom = false;
                Console.WriteLine("Settings loaded");
            }
}


        public void createJSON()
        {

        }






    }
}
