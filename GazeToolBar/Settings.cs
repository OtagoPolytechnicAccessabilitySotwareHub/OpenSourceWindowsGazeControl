using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using Newtonsoft.Json;
using System.Threading;
using System.Drawing;

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

        public bool centerZoom { get; set; }

        public bool eng { get; set; }

        public bool k123 { get; set; }
        public bool kacc { get; set; }

        public bool autocomplete { get; set; }

        public bool spanish { get; set; }
        public Color mainColour { get; set; }
        public Color secondColour { get; set; }
        public Color iconColour { get; set; }
        public int iconNumber { get; set; }

        SettingJSON settingsJson;
        public Settings(string path)
        {
            if (!File.Exists(path))
            {
                defaultSettings();
            }
            else
            {
                try
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
                    dynamicZoom = settingsJson.dynamicZoom;                                  //Changed for TESTINGVARIABLE
                    centerZoom = settingsJson.centerZoom;
                    eng = settingsJson.eng;
                    k123 = settingsJson.k123;
                    kacc = settingsJson.kacc;
                    spanish = settingsJson.spanish;
                    autocomplete = settingsJson.autocomplete;
                    mainColour = settingsJson.mainColour;
                    secondColour = settingsJson.secondColour;
                    iconNumber = settingsJson.iconColour;
                    switch (settingsJson.iconColour)
                    {
                        case 0:
                            iconColour = Color.Black;
                            break;
                        case 1:
                            iconColour = Color.White;
                            break;
                        case 2:
                            iconColour = Color.Cyan;
                            break;
                        case 3:
                            iconColour = Color.Red;
                            break;
                        case 4:
                            iconColour = Color.Yellow;
                            break;
                        case 5:
                            iconColour = Color.Lime;
                            break;
                    }
                    //iconColour = settingsJson.iconColour;
                    //dynamicZoom = false;
                    //centerZoom = false;

                }
                catch (Exception e)//If loading fails. Create new default settings.
                {
                    defaultSettings();
                }
            }
        }


        public void createJSON(string[] side)
        {
            SettingJSON setting = new SettingJSON();

            setting.fixationTimeLength = fixationTimeLength;
            setting.fixationTimeOut = fixationTimeOut;
            setting.leftClick = leftClick;
            setting.doubleClick = doubleClick;
            setting.rightClick = rightClick;
            setting.scroll = scroll;
            setting.sidebar = sidebar;
            setting.Crosshair = Crosshair;
            setting.maxZoom = maxZoom;
            setting.zoomWindowSize = zoomWindowSize;
            setting.stickyLeftClick = stickyLeftClick;
            setting.selectionFeedback = selectionFeedback;
            setting.dynamicZoom = dynamicZoom;                                  //Changed for TESTINGVARIABLE
            setting.centerZoom = centerZoom;
            setting.eng = eng;
            setting.k123 = k123;
            setting.kacc = kacc;
            setting.spanish = spanish;
            setting.autocomplete = autocomplete;
            //setting.dynamicZoom = false;
            //centerZoom = false;
            setting.mainColour = mainColour;
            setting.secondColour = secondColour;
            setting.iconColour = iconNumber;
            string settings = JsonConvert.SerializeObject(setting);
            File.WriteAllText(Program.path, settings);
        }


        public void defaultSettings()
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
            dynamicZoom = false;                                  //Changed for TESTINGVARIABLE
            centerZoom = false;
            eng = true;
            k123 = true;
            kacc = true;
            spanish = true;
            autocomplete = true;
            mainColour = Color.Black;
            secondColour = Color.White;
            iconNumber = 3;
            switch (iconNumber)
            {
                case 0:
                    iconColour = Color.Black;
                    break;
                case 1:
                    iconColour = Color.White;
                    break;
                case 2:
                    iconColour = Color.Cyan;
                    break;
                case 3:
                    iconColour = Color.Red;
                    break;
                case 4:
                    iconColour = Color.Yellow;
                    break;
                case 5:
                    iconColour = Color.Lime;
                    break;
            }
        }



    }
}
