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

namespace GazeToolBar
{
    public partial class ZoomSettingsForm : Form
    {
        int buttonClickDelay = 500;
        Color main = Program.readSettings.mainColour;
        Color second = Program.readSettings.secondColour;


        private void connectBehaveMap()
        {
            eyeXHost.Connect(bhavZoomMap);

            setupMap();
        }

        private void setupMap()
        {
            bhavZoomMap.Add(btnZoomSizeMinus, new GazeAwareBehavior(OnBtnZoomSizeMinus_Click) { DelayMilliseconds = buttonClickDelay });
            bhavZoomMap.Add(btnZoomSizePlus, new GazeAwareBehavior(OnBtnZoomSizePlus_Click) { DelayMilliseconds = buttonClickDelay });
            bhavZoomMap.Add(btnZoomAmountMinus, new GazeAwareBehavior(OnBtnZoomAmountMinus_Click) { DelayMilliseconds = buttonClickDelay });
            bhavZoomMap.Add(btnZoomAmountPlus, new GazeAwareBehavior(OnBtnZoomAmountPlus_Click) { DelayMilliseconds = buttonClickDelay });
            bhavZoomMap.Add(btnStaticZoomMode, new GazeAwareBehavior(OnStaticZoomMode_Click) { DelayMilliseconds = buttonClickDelay });
            bhavZoomMap.Add(btnDynamicZoomMode, new GazeAwareBehavior(OnDynamicZoomMode_Click) { DelayMilliseconds = buttonClickDelay });
            bhavZoomMap.Add(btnCornerZoomMode, new GazeAwareBehavior(OnCornerZoomMode_Click) { DelayMilliseconds = buttonClickDelay });
            bhavZoomMap.Add(btnSave, new GazeAwareBehavior(OnSave_Click) { DelayMilliseconds = buttonClickDelay });
            bhavZoomMap.Add(btnCancel, new GazeAwareBehavior(OnCancel_Click) { DelayMilliseconds = buttonClickDelay });

            bhavZoomMap.Add(pnlSave, new GazeAwareBehavior(OnGazeChangeBTColour));
            bhavZoomMap.Add(pnlCancel, new GazeAwareBehavior(OnGazeChangeBTColour));
            bhavZoomMap.Add(pnlZWSMinus, new GazeAwareBehavior(OnGazeChangeBTColour));
            bhavZoomMap.Add(pnlZWSPlus, new GazeAwareBehavior(OnGazeChangeBTColour));
            bhavZoomMap.Add(pnlZIAMinus, new GazeAwareBehavior(OnGazeChangeBTColour));
            bhavZoomMap.Add(pnlZIAPlus, new GazeAwareBehavior(OnGazeChangeBTColour));
            bhavZoomMap.Add(pnlStaticZoomMode, new GazeAwareBehavior(OnGazeChangeBTColour));
            bhavZoomMap.Add(pnlDynamicZoomMode, new GazeAwareBehavior(OnGazeChangeBTColour));
            bhavZoomMap.Add(pnlCornerZoomMode, new GazeAwareBehavior(OnGazeChangeBTColour));
        }




        private void OnGazeChangeBTColour(object s, GazeAwareEventArgs e)
        {
            var sentButton = s as Panel;
            if (sentButton != null)
            {
                sentButton.BackColor = (e.HasGaze) ? second : main;
            }
        }

        private void OnCancel_Click(object sender, GazeAwareEventArgs e)
        {
            if (e.HasGaze) btnCancel.PerformClick();
        }
        private void OnSave_Click(object sender, GazeAwareEventArgs e)
        {
            if (e.HasGaze) btnSave.PerformClick();
        }


        private void OnBtnZoomSizeMinus_Click(object sender, GazeAwareEventArgs e)
        {
            if (e.HasGaze) btnZoomSizeMinus.PerformClick();
        }

        private void OnBtnZoomSizePlus_Click(object sender, GazeAwareEventArgs e)
        {
            if (e.HasGaze) btnZoomSizePlus.PerformClick();
        }

        private void OnBtnZoomAmountMinus_Click(object sender, GazeAwareEventArgs e)
        {
            if (e.HasGaze) btnZoomAmountMinus.PerformClick();
        }


        private void OnBtnZoomAmountPlus_Click(object sender, GazeAwareEventArgs e)
        {
            if (e.HasGaze) btnZoomAmountPlus.PerformClick();
        }

        private void OnStaticZoomMode_Click(object sender, GazeAwareEventArgs e)
        {
            if (e.HasGaze) btnStaticZoomMode.PerformClick();
        }

        private void OnDynamicZoomMode_Click(object sender, GazeAwareEventArgs e)
        {
            if (e.HasGaze) btnDynamicZoomMode.PerformClick();
        }

        private void OnCornerZoomMode_Click(object sender, GazeAwareEventArgs e)
        {
            if (e.HasGaze) btnCornerZoomMode.PerformClick();
        }


    }
}
