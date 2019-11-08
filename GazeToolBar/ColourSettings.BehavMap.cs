using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using EyeXFramework;
using EyeXFramework.Forms;

namespace GazeToolBar
{
    public partial class ColourSettings : Form
    {
        int buttonClickDelay = 500;

        private void connectBehaveMap()
        {
            eyeXHost.Connect(bhavColourSettings);

            setupMap();
        }

        private void setupMap()
        {
            bhavColourSettings.Add(btnSave, new GazeAwareBehavior(OnSave_Click) { DelayMilliseconds = buttonClickDelay });
            bhavColourSettings.Add(btnCancel, new GazeAwareBehavior(OnCancel_Click) { DelayMilliseconds = buttonClickDelay });
            bhavColourSettings.Add(colourOptionButton1, new GazeAwareBehavior(colourOptionButton1_Click) { DelayMilliseconds = buttonClickDelay });
            bhavColourSettings.Add(colourOptionPanel1, new GazeAwareBehavior(OnGazeChangeBTColour));
            bhavColourSettings.Add(colourOptionButton2, new GazeAwareBehavior(colourOptionButton2_Click) { DelayMilliseconds = buttonClickDelay });
            bhavColourSettings.Add(colourOptionPanel2, new GazeAwareBehavior(OnGazeChangeBTColour));
            bhavColourSettings.Add(colourOptionButton3, new GazeAwareBehavior(colourOptionButton3_Click) { DelayMilliseconds = buttonClickDelay });
            bhavColourSettings.Add(colourOptionPanel3, new GazeAwareBehavior(OnGazeChangeBTColour));
            bhavColourSettings.Add(colourOptionButton4, new GazeAwareBehavior(colourOptionButton4_Click) { DelayMilliseconds = buttonClickDelay });
            bhavColourSettings.Add(colourOptionPanel4, new GazeAwareBehavior(OnGazeChangeBTColour));
            bhavColourSettings.Add(colourOptionButton5, new GazeAwareBehavior(colourOptionButton5_Click) { DelayMilliseconds = buttonClickDelay });
            bhavColourSettings.Add(colourOptionPanel5, new GazeAwareBehavior(OnGazeChangeBTColour));
            bhavColourSettings.Add(colourOptionButton6, new GazeAwareBehavior(colourOptionButton6_Click) { DelayMilliseconds = buttonClickDelay });
            bhavColourSettings.Add(colourOptionPanel6, new GazeAwareBehavior(OnGazeChangeBTColour));
            bhavColourSettings.Add(colourOptionButton7, new GazeAwareBehavior(colourOptionButton7_Click) { DelayMilliseconds = buttonClickDelay });
            bhavColourSettings.Add(colourOptionPanel7, new GazeAwareBehavior(OnGazeChangeBTColour));
            bhavColourSettings.Add(colourOptionButton8, new GazeAwareBehavior(colourOptionButton8_Click) { DelayMilliseconds = buttonClickDelay });
            bhavColourSettings.Add(colourOptionPanel8, new GazeAwareBehavior(OnGazeChangeBTColour));
            bhavColourSettings.Add(colourOptionButton9, new GazeAwareBehavior(colourOptionButton9_Click) { DelayMilliseconds = buttonClickDelay });
            bhavColourSettings.Add(colourOptionPanel9, new GazeAwareBehavior(OnGazeChangeBTColour));
            bhavColourSettings.Add(colourOptionButton10, new GazeAwareBehavior(colourOptionButton10_Click) { DelayMilliseconds = buttonClickDelay });
            bhavColourSettings.Add(colourOptionPanel10, new GazeAwareBehavior(OnGazeChangeBTColour));
            bhavColourSettings.Add(colourOptionButton11, new GazeAwareBehavior(colourOptionButton11_Click) { DelayMilliseconds = buttonClickDelay });
            bhavColourSettings.Add(colourOptionPanel11, new GazeAwareBehavior(OnGazeChangeBTColour));
            bhavColourSettings.Add(colourOptionButton12, new GazeAwareBehavior(colourOptionButton12_Click) { DelayMilliseconds = buttonClickDelay });
            bhavColourSettings.Add(colourOptionPanel12, new GazeAwareBehavior(OnGazeChangeBTColour));
            bhavColourSettings.Add(colourOptionButton13, new GazeAwareBehavior(colourOptionButton13_Click) { DelayMilliseconds = buttonClickDelay });
            bhavColourSettings.Add(colourOptionPanel13, new GazeAwareBehavior(OnGazeChangeBTColour));
            bhavColourSettings.Add(colourOptionButton14, new GazeAwareBehavior(colourOptionButton14_Click) { DelayMilliseconds = buttonClickDelay });
            bhavColourSettings.Add(colourOptionPanel14, new GazeAwareBehavior(OnGazeChangeBTColour));
            bhavColourSettings.Add(colourOptionButton15, new GazeAwareBehavior(colourOptionButton15_Click) { DelayMilliseconds = buttonClickDelay });
            bhavColourSettings.Add(colourOptionPanel15, new GazeAwareBehavior(OnGazeChangeBTColour));
            bhavColourSettings.Add(colourOptionButton16, new GazeAwareBehavior(colourOptionButton16_Click) { DelayMilliseconds = buttonClickDelay });
            bhavColourSettings.Add(colourOptionPanel16, new GazeAwareBehavior(OnGazeChangeBTColour));
            bhavColourSettings.Add(colourOptionButton17, new GazeAwareBehavior(colourOptionButton17_Click) { DelayMilliseconds = buttonClickDelay });
            bhavColourSettings.Add(colourOptionPanel17, new GazeAwareBehavior(OnGazeChangeBTColour));
            bhavColourSettings.Add(colourOptionButton18, new GazeAwareBehavior(colourOptionButton18_Click) { DelayMilliseconds = buttonClickDelay });
            bhavColourSettings.Add(colourOptionPanel18, new GazeAwareBehavior(OnGazeChangeBTColour));



            bhavColourSettings.Add(btnHighLight, new GazeAwareBehavior(OnhighlightClick) { DelayMilliseconds = buttonClickDelay });
            bhavColourSettings.Add(pnlHighlightColour, new GazeAwareBehavior(OnGazeChangeBTColour));
            bhavColourSettings.Add(btnMainColour, new GazeAwareBehavior(OnMainClick) { DelayMilliseconds = buttonClickDelay });
            bhavColourSettings.Add(pnlBackColour, new GazeAwareBehavior(OnGazeChangeBTColour));
            bhavColourSettings.Add(btnIcon, new GazeAwareBehavior(OnIconClick) { DelayMilliseconds = buttonClickDelay });
            bhavColourSettings.Add(pnlIconColour, new GazeAwareBehavior(OnGazeChangeBTColour));
            bhavColourSettings.Add(pnlSave, new GazeAwareBehavior(OnGazeChangeBTColour));
            bhavColourSettings.Add(pnlCancel, new GazeAwareBehavior(OnGazeChangeBTColour));
        }

        private void setupFORMMap()
        {

        }

        //toggle border on and off on gaze to gaze to give feed back.
        private void OnGazeChangeBTColour(object s, GazeAwareEventArgs e)
        {
            var sentButton = s as Panel;
            if (sentButton != null)
            {
                sentButton.BackColor = (e.HasGaze) ? Color.Red : Program.readSettings.mainColour;
            }
        }
        private void OnhighlightClick(object sender, GazeAwareEventArgs e)
        {
            if (e.HasGaze) btnHighLight.PerformClick();
        }

        private void OnMainClick(object sender, GazeAwareEventArgs e)
        {
            if (e.HasGaze) btnMainColour.PerformClick();
        }

        private void OnIconClick(object sender, GazeAwareEventArgs e)
        {
            if (e.HasGaze) btnIcon.PerformClick();
        }

        private void OnCancel_Click(object sender, GazeAwareEventArgs e)
        {
            if (e.HasGaze) btnCancel.PerformClick();
        }
        private void OnSave_Click(object sender, GazeAwareEventArgs e)
        {
            if (e.HasGaze) btnSave.PerformClick();
        }


    }
}
