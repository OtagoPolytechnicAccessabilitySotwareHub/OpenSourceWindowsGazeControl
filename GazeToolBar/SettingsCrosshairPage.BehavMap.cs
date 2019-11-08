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
    public partial class SettingsCrosshairPage : Form
    {
        int buttonClickDelay = 500;
        Color main = Program.readSettings.mainColour;
        Color second = Program.readSettings.secondColour;

        private void connectBehaveMap()
        {
            eyeXHost.Connect(bhavCrosshairMap);

            setupMap();
        }

        private void OnGazeChangeBTColour(object s, GazeAwareEventArgs e)
        {
            var sentButton = s as Panel;
            if (sentButton != null)
            {
                sentButton.BackColor = (e.HasGaze) ? second : main;
            }
        }

        private void setupMap()
        {
            bhavCrosshairMap.Add(buttonCrosshairDown, new GazeAwareBehavior(OnButtonCrosshairDown_Click) { DelayMilliseconds = buttonClickDelay });
            bhavCrosshairMap.Add(buttonCrosshairUp, new GazeAwareBehavior(OnButtonCrosshairUp_Click) { DelayMilliseconds = buttonClickDelay });
            bhavCrosshairMap.Add(btnSave, new GazeAwareBehavior(OnSave_Click) { DelayMilliseconds = buttonClickDelay });
            bhavCrosshairMap.Add(btnCancel, new GazeAwareBehavior(OnCancel_Click) { DelayMilliseconds = buttonClickDelay });

            bhavCrosshairMap.Add(pnlSave, new GazeAwareBehavior(OnGazeChangeBTColour));
            bhavCrosshairMap.Add(pnlCancel, new GazeAwareBehavior(OnGazeChangeBTColour));

            bhavCrosshairMap.Add(pnlCrosshairDownButton, new GazeAwareBehavior(OnGazeChangeBTColour));
            bhavCrosshairMap.Add(pnlCrosshairUpButton, new GazeAwareBehavior(OnGazeChangeBTColour));
        }

        private void OnButtonCrosshairDown_Click(object sender, GazeAwareEventArgs e)
        {
            if (e.HasGaze) buttonCrosshairDown.PerformClick();
        }

        private void OnButtonCrosshairUp_Click(object sender, GazeAwareEventArgs e)
        {
            if (e.HasGaze) buttonCrosshairUp.PerformClick();
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
