using EyeXFramework;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GazeToolBar
{
    partial class SettingsHome
    {
        int buttonClickDelay = 500;
        Color main = Program.readSettings.mainColour;
        Color second = Program.readSettings.secondColour;

        private void setupMap()
        {

        }

        private void setupSettingHomeMap()
        {
            behavSetting.Add(backButton, new GazeAwareBehavior(backButton_Click) { DelayMilliseconds = buttonClickDelay }); //look at button to activate it
            behavSetting.Add(backPanel, new GazeAwareBehavior(OnGazeChangeBTColour));                                    //look at the panel to light up the area behind the button so you know where you are looking on the keyboard

            behavSetting.Add(generalButton, new GazeAwareBehavior(generalButton_Click) { DelayMilliseconds = buttonClickDelay }); //look at button to activate it
            behavSetting.Add(generalPanel, new GazeAwareBehavior(OnGazeChangeBTColour));                                    //look at the panel to light up the area behind the button so you know where you are looking on the keyboard

            behavSetting.Add(zoomButton, new GazeAwareBehavior(zoomButton_Click) { DelayMilliseconds = buttonClickDelay }); //look at button to activate it
            behavSetting.Add(zoomPanel, new GazeAwareBehavior(OnGazeChangeBTColour));                                    //look at the panel to light up the area behind the button so you know where you are looking on the keyboard

            behavSetting.Add(crossButton, new GazeAwareBehavior(crossButton_Click) { DelayMilliseconds = buttonClickDelay }); //look at button to activate it
            behavSetting.Add(crossPanel, new GazeAwareBehavior(OnGazeChangeBTColour));                                    //look at the panel to light up the area behind the button so you know where you are looking on the keyboard

            behavSetting.Add(arrangeButton, new GazeAwareBehavior(arrangeButton_Click) { DelayMilliseconds = buttonClickDelay }); //look at button to activate it
            behavSetting.Add(arrangePanel, new GazeAwareBehavior(OnGazeChangeBTColour));                                    //look at the panel to light up the area behind the button so you know where you are looking on the keyboard

            behavSetting.Add(shortcutButton, new GazeAwareBehavior(shortcutButton_Click) { DelayMilliseconds = buttonClickDelay }); //look at button to activate it
            behavSetting.Add(shortcutPanel, new GazeAwareBehavior(OnGazeChangeBTColour));                                    //look at the panel to light up the area behind the button so you know where you are looking on the keyboard

            behavSetting.Add(KeyboardSettings, new GazeAwareBehavior(KeyboardSettings_Click) { DelayMilliseconds = buttonClickDelay }); //look at button to activate it
            behavSetting.Add(keyboardPanel, new GazeAwareBehavior(OnGazeChangeBTColour));                                    //look at the panel to light up the area behind the button so you know where you are looking on the keyboard

            behavSetting.Add(colourSettings, new GazeAwareBehavior(colourSettings_Click) { DelayMilliseconds = buttonClickDelay }); //look at button to activate it
            behavSetting.Add(colourPanel, new GazeAwareBehavior(OnGazeChangeBTColour));

            behavSetting.Add(button1, new GazeAwareBehavior(button1_Click) { DelayMilliseconds = buttonClickDelay }); //look at button to activate it
            behavSetting.Add(defaultPanel, new GazeAwareBehavior(OnGazeChangeBTColour));
        }

        private void connectBehaveMap()
        {
            eyeXHost.Connect(behavSetting);

            setupMap();
            setupSettingHomeMap();
        }


        //toggle border on and off on gaze to gaze to give feed back.
        private void OnGazeChangeBTColour(object s, GazeAwareEventArgs e)
        {
            var sentButton = s as Panel;
            if (sentButton != null)
            {
                sentButton.BackColor = (e.HasGaze) ? second : main;
            }
        }





    }
}
