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
        int defaultDelay = 800;
        Color main = Program.readSettings.mainColour;
        Color second = Program.readSettings.secondColour;

        private void setupMap()
        {

        }

        private void setupSettingHomeMap()
        {
            behavSetting.Add(backButton, new GazeAwareBehavior(btbackButton_Click) { DelayMilliseconds = buttonClickDelay }); //look at button to activate it
            behavSetting.Add(zoomButton, new GazeAwareBehavior(btzoomButton_Click) { DelayMilliseconds = buttonClickDelay });
            behavSetting.Add(generalButton, new GazeAwareBehavior(btgeneralButton_Click) { DelayMilliseconds = buttonClickDelay }); //look at button to activate it

            behavSetting.Add(generalPanel, new GazeAwareBehavior(OnGazeChangeBTColour));                                    //look at the panel to light up the area behind the button so you know where you are looking on the keyboard
            behavSetting.Add(backPanel, new GazeAwareBehavior(OnGazeChangeBTColour));
            behavSetting.Add(zoomPanel, new GazeAwareBehavior(OnGazeChangeBTColour));  
            

            behavSetting.Add(crossButton, new GazeAwareBehavior(btcrossButton_Click) { DelayMilliseconds = buttonClickDelay }); //look at button to activate it
            behavSetting.Add(crossPanel, new GazeAwareBehavior(OnGazeChangeBTColour));                                    //look at the panel to light up the area behind the button so you know where you are looking on the keyboard

            behavSetting.Add(arrangeButton, new GazeAwareBehavior(btarrangeButton_Click) { DelayMilliseconds = buttonClickDelay }); //look at button to activate it
            behavSetting.Add(arrangePanel, new GazeAwareBehavior(OnGazeChangeBTColour));                                    //look at the panel to light up the area behind the button so you know where you are looking on the keyboard

            behavSetting.Add(shortcutButton, new GazeAwareBehavior(btshortcutButton_Click) { DelayMilliseconds = buttonClickDelay }); //look at button to activate it
            behavSetting.Add(shortcutPanel, new GazeAwareBehavior(OnGazeChangeBTColour));                                    //look at the panel to light up the area behind the button so you know where you are looking on the keyboard

            behavSetting.Add(KeyboardSettings, new GazeAwareBehavior(btKeyboardSettings_Click) { DelayMilliseconds = buttonClickDelay }); //look at button to activate it
            behavSetting.Add(keyboardPanel, new GazeAwareBehavior(OnGazeChangeBTColour));                                    //look at the panel to light up the area behind the button so you know where you are looking on the keyboard

            behavSetting.Add(colourSettings, new GazeAwareBehavior(btcolourSettings_Click) { DelayMilliseconds = buttonClickDelay }); //look at button to activate it
            behavSetting.Add(colourPanel, new GazeAwareBehavior(OnGazeChangeBTColour));

            behavSetting.Add(button1, new GazeAwareBehavior(btbutton1_Click) { DelayMilliseconds = defaultDelay }); //look at button to activate it
            behavSetting.Add(defaultPanel, new GazeAwareBehavior(OnGazeChangeBTColour));
        }

        private void btbutton1_Click(object sender, GazeAwareEventArgs e)
        {
            if (e.HasGaze) button1.PerformClick();
        }

        private void btcolourSettings_Click(object sender, GazeAwareEventArgs e)
        {
            if (e.HasGaze) colourSettings.PerformClick();
        }

        private void btKeyboardSettings_Click(object sender, GazeAwareEventArgs e)
        {
            if (e.HasGaze) KeyboardSettings.PerformClick();
        }

        private void btshortcutButton_Click(object sender, GazeAwareEventArgs e)
        {
            if (e.HasGaze) shortcutButton.PerformClick();
        }

        private void btarrangeButton_Click(object sender, GazeAwareEventArgs e)
        {
            if (e.HasGaze) arrangeButton.PerformClick();
        }

        private void btcrossButton_Click(object sender, GazeAwareEventArgs e)
        {
            if (e.HasGaze) crossButton.PerformClick();
        }

        private void btgeneralButton_Click(object sender, GazeAwareEventArgs e)
        {
            if (e.HasGaze) generalButton.PerformClick();
        }

        private void btzoomButton_Click(object sender, GazeAwareEventArgs e)
        {
            if (e.HasGaze) zoomButton.PerformClick();
        }

        private void btbackButton_Click(object sender, GazeAwareEventArgs e)
        {
            if (e.HasGaze) backButton.PerformClick();
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
