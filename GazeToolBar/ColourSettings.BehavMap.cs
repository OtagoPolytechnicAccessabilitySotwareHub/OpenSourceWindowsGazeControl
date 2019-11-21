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
            bhavColourSettings.Add(colourOptionButton1, new GazeAwareBehavior(btcolourOptionButton1_Click) { DelayMilliseconds = buttonClickDelay });
            bhavColourSettings.Add(colourOptionPanel1, new GazeAwareBehavior(OnGazeChangeBTColour));
            bhavColourSettings.Add(colourOptionButton2, new GazeAwareBehavior(btcolourOptionButton2_Click) { DelayMilliseconds = buttonClickDelay });
            bhavColourSettings.Add(colourOptionPanel2, new GazeAwareBehavior(OnGazeChangeBTColour));
            bhavColourSettings.Add(colourOptionButton3, new GazeAwareBehavior(btcolourOptionButton3_Click) { DelayMilliseconds = buttonClickDelay });
            bhavColourSettings.Add(colourOptionPanel3, new GazeAwareBehavior(OnGazeChangeBTColour));
            bhavColourSettings.Add(colourOptionButton4, new GazeAwareBehavior(btcolourOptionButton4_Click) { DelayMilliseconds = buttonClickDelay });
            bhavColourSettings.Add(colourOptionPanel4, new GazeAwareBehavior(OnGazeChangeBTColour));
            bhavColourSettings.Add(colourOptionButton5, new GazeAwareBehavior(btcolourOptionButton5_Click) { DelayMilliseconds = buttonClickDelay });
            bhavColourSettings.Add(colourOptionPanel5, new GazeAwareBehavior(OnGazeChangeBTColour));
            bhavColourSettings.Add(colourOptionButton6, new GazeAwareBehavior(btcolourOptionButton6_Click) { DelayMilliseconds = buttonClickDelay });
            bhavColourSettings.Add(colourOptionPanel6, new GazeAwareBehavior(OnGazeChangeBTColour));
            bhavColourSettings.Add(colourOptionButton7, new GazeAwareBehavior(btcolourOptionButton7_Click) { DelayMilliseconds = buttonClickDelay });
            bhavColourSettings.Add(colourOptionPanel7, new GazeAwareBehavior(OnGazeChangeBTColour));
            bhavColourSettings.Add(colourOptionButton8, new GazeAwareBehavior(btcolourOptionButton8_Click) { DelayMilliseconds = buttonClickDelay });
            bhavColourSettings.Add(colourOptionPanel8, new GazeAwareBehavior(OnGazeChangeBTColour));
            bhavColourSettings.Add(colourOptionButton9, new GazeAwareBehavior(btcolourOptionButton9_Click) { DelayMilliseconds = buttonClickDelay });
            bhavColourSettings.Add(colourOptionPanel9, new GazeAwareBehavior(OnGazeChangeBTColour));
            bhavColourSettings.Add(colourOptionButton10, new GazeAwareBehavior(btcolourOptionButton10_Click) { DelayMilliseconds = buttonClickDelay });
            bhavColourSettings.Add(colourOptionPanel10, new GazeAwareBehavior(OnGazeChangeBTColour));
            bhavColourSettings.Add(colourOptionButton11, new GazeAwareBehavior(btcolourOptionButton11_Click) { DelayMilliseconds = buttonClickDelay });
            bhavColourSettings.Add(colourOptionPanel11, new GazeAwareBehavior(OnGazeChangeBTColour));
            bhavColourSettings.Add(colourOptionButton12, new GazeAwareBehavior(btcolourOptionButton12_Click) { DelayMilliseconds = buttonClickDelay });
            bhavColourSettings.Add(colourOptionPanel12, new GazeAwareBehavior(OnGazeChangeBTColour));
            bhavColourSettings.Add(colourOptionButton13, new GazeAwareBehavior(btcolourOptionButton13_Click) { DelayMilliseconds = buttonClickDelay });
            bhavColourSettings.Add(colourOptionPanel13, new GazeAwareBehavior(OnGazeChangeBTColour));
            bhavColourSettings.Add(colourOptionButton14, new GazeAwareBehavior(btcolourOptionButton14_Click) { DelayMilliseconds = buttonClickDelay });
            bhavColourSettings.Add(colourOptionPanel14, new GazeAwareBehavior(OnGazeChangeBTColour));
            bhavColourSettings.Add(colourOptionButton15, new GazeAwareBehavior(btcolourOptionButton15_Click) { DelayMilliseconds = buttonClickDelay });
            bhavColourSettings.Add(colourOptionPanel15, new GazeAwareBehavior(OnGazeChangeBTColour));
            bhavColourSettings.Add(colourOptionButton16, new GazeAwareBehavior(btcolourOptionButton16_Click) { DelayMilliseconds = buttonClickDelay });
            bhavColourSettings.Add(colourOptionPanel16, new GazeAwareBehavior(OnGazeChangeBTColour));
            bhavColourSettings.Add(colourOptionButton17, new GazeAwareBehavior(btcolourOptionButton17_Click) { DelayMilliseconds = buttonClickDelay });
            bhavColourSettings.Add(colourOptionPanel17, new GazeAwareBehavior(OnGazeChangeBTColour));
            bhavColourSettings.Add(colourOptionButton18, new GazeAwareBehavior(btcolourOptionButton18_Click) { DelayMilliseconds = buttonClickDelay });
            bhavColourSettings.Add(colourOptionPanel18, new GazeAwareBehavior(OnGazeChangeBTColour));



            bhavColourSettings.Add(iconbtnBlack, new GazeAwareBehavior(OniconbtnBlack) { DelayMilliseconds = buttonClickDelay });
            bhavColourSettings.Add(iconPanelBlack, new GazeAwareBehavior(OnGazeChangeBTColour));
            bhavColourSettings.Add(iconbtnWhite, new GazeAwareBehavior(OniconbtnWhite) { DelayMilliseconds = buttonClickDelay });
            bhavColourSettings.Add(iconPanelWhite, new GazeAwareBehavior(OnGazeChangeBTColour));
            bhavColourSettings.Add(iconbtnRed, new GazeAwareBehavior(OniconbtnRed) { DelayMilliseconds = buttonClickDelay });
            bhavColourSettings.Add(iconPanelRed, new GazeAwareBehavior(OnGazeChangeBTColour));
            bhavColourSettings.Add(iconbtnBlue, new GazeAwareBehavior(OniconbtnBlue) { DelayMilliseconds = buttonClickDelay });
            bhavColourSettings.Add(iconPanelBlue, new GazeAwareBehavior(OnGazeChangeBTColour));
            bhavColourSettings.Add(iconbtnGreen, new GazeAwareBehavior(OniconbtnGreen) { DelayMilliseconds = buttonClickDelay });
            bhavColourSettings.Add(iconPanelGreen, new GazeAwareBehavior(OnGazeChangeBTColour));
            bhavColourSettings.Add(iconbtnYellow, new GazeAwareBehavior(OniconbtnYellow) { DelayMilliseconds = buttonClickDelay });
            bhavColourSettings.Add(iconPanelYellow, new GazeAwareBehavior(OnGazeChangeBTColour));




            bhavColourSettings.Add(btnHighLight, new GazeAwareBehavior(OnhighlightClick) { DelayMilliseconds = buttonClickDelay });
            bhavColourSettings.Add(pnlHighlightColour, new GazeAwareBehavior(OnGazeChangeBTColour));
            bhavColourSettings.Add(btnMainColour, new GazeAwareBehavior(OnMainClick) { DelayMilliseconds = buttonClickDelay });
            bhavColourSettings.Add(pnlBackColour, new GazeAwareBehavior(OnGazeChangeBTColour));
            bhavColourSettings.Add(btnIcon, new GazeAwareBehavior(OnIconClick) { DelayMilliseconds = buttonClickDelay });
            bhavColourSettings.Add(pnlIconColour, new GazeAwareBehavior(OnGazeChangeBTColour));
            bhavColourSettings.Add(pnlSave, new GazeAwareBehavior(OnGazeChangeBTColour));
            bhavColourSettings.Add(pnlCancel, new GazeAwareBehavior(OnGazeChangeBTColour));
        }

        private void OniconbtnYellow(object sender, GazeAwareEventArgs e)
        {
            if (e.HasGaze) iconbtnYellow.PerformClick();
        }

        private void OniconbtnGreen(object sender, GazeAwareEventArgs e)
        {
            if (e.HasGaze) iconbtnGreen.PerformClick();
        }

        private void OniconbtnBlue(object sender, GazeAwareEventArgs e)
        {
            if (e.HasGaze) iconbtnBlue.PerformClick();
        }

        private void OniconbtnRed(object sender, GazeAwareEventArgs e)
        {
            if (e.HasGaze) iconbtnRed.PerformClick();
        }

        private void OniconbtnWhite(object sender, GazeAwareEventArgs e)
        {
            if (e.HasGaze) iconbtnWhite.PerformClick();
        }

        private void OniconbtnBlack(object sender, GazeAwareEventArgs e)
        {
            if (e.HasGaze) iconbtnBlack.PerformClick();
        }

        private void btcolourOptionButton18_Click(object sender, GazeAwareEventArgs e)
        {
            if (e.HasGaze) colourOptionButton18.PerformClick();
        }

        private void btcolourOptionButton17_Click(object sender, GazeAwareEventArgs e)
        {
            if (e.HasGaze) colourOptionButton17.PerformClick();
        }

        private void btcolourOptionButton16_Click(object sender, GazeAwareEventArgs e)
        {
            if (e.HasGaze) colourOptionButton16.PerformClick();
        }

        private void btcolourOptionButton15_Click(object sender, GazeAwareEventArgs e)
        {
            if (e.HasGaze) colourOptionButton15.PerformClick();
        }

        private void btcolourOptionButton14_Click(object sender, GazeAwareEventArgs e)
        {
            if (e.HasGaze) colourOptionButton14.PerformClick();
        }

        private void btcolourOptionButton13_Click(object sender, GazeAwareEventArgs e)
        {
            if (e.HasGaze) colourOptionButton13.PerformClick();
        }

        private void btcolourOptionButton12_Click(object sender, GazeAwareEventArgs e)
        {
            if (e.HasGaze) colourOptionButton12.PerformClick();
        }

        private void btcolourOptionButton11_Click(object sender, GazeAwareEventArgs e)
        {
            if (e.HasGaze) colourOptionButton11.PerformClick();
        }

        private void btcolourOptionButton10_Click(object sender, GazeAwareEventArgs e)
        {
            if (e.HasGaze) colourOptionButton10.PerformClick();
        }

        private void btcolourOptionButton9_Click(object sender, GazeAwareEventArgs e)
        {
            if (e.HasGaze) colourOptionButton9.PerformClick();
        }

        private void btcolourOptionButton8_Click(object sender, GazeAwareEventArgs e)
        {
            if (e.HasGaze) colourOptionButton8.PerformClick();
        }

        private void btcolourOptionButton7_Click(object sender, GazeAwareEventArgs e)
        {
            if (e.HasGaze) colourOptionButton7.PerformClick();
        }

        private void btcolourOptionButton6_Click(object sender, GazeAwareEventArgs e)
        {
            if (e.HasGaze) colourOptionButton6.PerformClick();
        }

        private void btcolourOptionButton5_Click(object sender, GazeAwareEventArgs e)
        {
            if (e.HasGaze) colourOptionButton5.PerformClick();
        }

        private void btcolourOptionButton4_Click(object sender, GazeAwareEventArgs e)
        {
            if (e.HasGaze) colourOptionButton4.PerformClick();
        }

        private void btcolourOptionButton3_Click(object sender, GazeAwareEventArgs e)
        {
            if (e.HasGaze) colourOptionButton3.PerformClick();
        }

        private void btcolourOptionButton2_Click(object sender, GazeAwareEventArgs e)
        {
            if (e.HasGaze) colourOptionButton2.PerformClick();
        }

        private void btcolourOptionButton1_Click(object sender, GazeAwareEventArgs e)
        {
            if (e.HasGaze) colourOptionButton1.PerformClick();
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
                sentButton.BackColor = (e.HasGaze) ? Program.readSettings.secondColour : Program.readSettings.mainColour;
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
