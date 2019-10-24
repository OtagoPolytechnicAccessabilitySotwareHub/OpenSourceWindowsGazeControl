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
    public partial class GeneralSettingsForm : Form
    {
        int buttonClickDelay = 500;
        Color main = Program.readSettings.mainColour;
        Color second = Program.readSettings.secondColour;

        private void connectBehaveMap()
        {
            eyeXHost.Connect(bhavGeneralMap);

            setupMap();
        }

        private void setupMap()
        {
            bhavGeneralMap.Add(btnFixTimeLengthMins, new GazeAwareBehavior(OnbtnFixTimeLengthMins_Click) { DelayMilliseconds = buttonClickDelay });
            bhavGeneralMap.Add(btnFixTimeLengthPlus, new GazeAwareBehavior(OnbtnFixTimeLengthPlus_Click) { DelayMilliseconds = buttonClickDelay });
            bhavGeneralMap.Add(btnFixTimeOutMins, new GazeAwareBehavior(OnbtnFixTimeOutMins_Click) { DelayMilliseconds = buttonClickDelay });
            bhavGeneralMap.Add(btnFixTimeOutPlus, new GazeAwareBehavior(OnbtnFixTimeOutPlus_Click) { DelayMilliseconds = buttonClickDelay });
            bhavGeneralMap.Add(btnAutoStart, new GazeAwareBehavior(OnbtnAutoStart_Click) { DelayMilliseconds = buttonClickDelay });
            

            bhavGeneralMap.Add(pnlFTLMins, new GazeAwareBehavior(OnGazeChangeBTColour));
            bhavGeneralMap.Add(pnlFTLPlus, new GazeAwareBehavior(OnGazeChangeBTColour));
            bhavGeneralMap.Add(pnlFTOMins, new GazeAwareBehavior(OnGazeChangeBTColour));
            bhavGeneralMap.Add(pnlFTOPlus, new GazeAwareBehavior(OnGazeChangeBTColour));
            bhavGeneralMap.Add(pnlOtherAuto, new GazeAwareBehavior(OnGazeChangeBTColour));
            bhavGeneralMap.Add(pnlStickyLeft, new GazeAwareBehavior(OnGazeChangeBTColour));
        }

        private void OnGazeChangeBTColour(object s, GazeAwareEventArgs e)
        {
            var sentButton = s as Panel;
            if (sentButton != null)
            {
                sentButton.BackColor = (e.HasGaze) ? second : main;
            }
        }

        private void OnbtnAutoStart_Click(object sender, GazeAwareEventArgs e)
        {
            if (e.HasGaze) btnAutoStart.PerformClick();
        }

        private void OnbtnFixTimeOutPlus_Click(object sender, GazeAwareEventArgs e)
        {
            if (e.HasGaze) btnFixTimeOutPlus.PerformClick();
        }

        private void OnbtnFixTimeOutMins_Click(object sender, GazeAwareEventArgs e)
        {
            if (e.HasGaze) btnFixTimeOutMins.PerformClick();
        }

        private void OnbtnFixTimeLengthPlus_Click(object sender, GazeAwareEventArgs e)
        {
            if (e.HasGaze) btnFixTimeLengthPlus.PerformClick();
        }

        private void OnbtnFixTimeLengthMins_Click(object sender, GazeAwareEventArgs e)
        {
            if (e.HasGaze) btnFixTimeLengthMins.PerformClick();
        }
    }
}
