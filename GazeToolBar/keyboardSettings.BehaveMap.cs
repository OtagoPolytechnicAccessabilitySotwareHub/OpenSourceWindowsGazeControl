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
    public partial class keyboardSettings : Form
    {
        int buttonClickDelay = 500;

        private void connectBehaveMap()
        {
            eyeXHost.Connect(bhavkeyboardSettings);

            setupMap();
        }

        private void setupMap()
        {
            bhavkeyboardSettings.Add(btnSave, new GazeAwareBehavior(OnSave_Click) { DelayMilliseconds = buttonClickDelay });
            bhavkeyboardSettings.Add(btnCancel, new GazeAwareBehavior(OnCancel_Click) { DelayMilliseconds = buttonClickDelay });
            bhavkeyboardSettings.Add(btnAbcOn, new GazeAwareBehavior(OnAbcOn_Click) { DelayMilliseconds = buttonClickDelay });
            bhavkeyboardSettings.Add(btn123On, new GazeAwareBehavior(On123On_Click) { DelayMilliseconds = buttonClickDelay });
            bhavkeyboardSettings.Add(btnAccOn, new GazeAwareBehavior(OnAccOn_Click) { DelayMilliseconds = buttonClickDelay });
            bhavkeyboardSettings.Add(btnespOn, new GazeAwareBehavior(OnEspOn_Click) { DelayMilliseconds = buttonClickDelay });



            bhavkeyboardSettings.Add(pnlAbcOn, new GazeAwareBehavior(OnGazeChangeBTColour));
            bhavkeyboardSettings.Add(pnl123On, new GazeAwareBehavior(OnGazeChangeBTColour));
            bhavkeyboardSettings.Add(pnlAccOn, new GazeAwareBehavior(OnGazeChangeBTColour));
            bhavkeyboardSettings.Add(pnlespOn, new GazeAwareBehavior(OnGazeChangeBTColour));



            bhavkeyboardSettings.Add(btnAbcOff, new GazeAwareBehavior(OnAbcOff_Click) { DelayMilliseconds = buttonClickDelay });
            bhavkeyboardSettings.Add(btn123Off, new GazeAwareBehavior(On123Off_Click) { DelayMilliseconds = buttonClickDelay });
            bhavkeyboardSettings.Add(btnAccOff, new GazeAwareBehavior(OnAccOff_Click) { DelayMilliseconds = buttonClickDelay });
            bhavkeyboardSettings.Add(btnespOff, new GazeAwareBehavior(OnEspOff_Click) { DelayMilliseconds = buttonClickDelay });


            bhavkeyboardSettings.Add(pnlAbcOff, new GazeAwareBehavior(OnGazeChangeBTColour));
            bhavkeyboardSettings.Add(pnl123Off, new GazeAwareBehavior(OnGazeChangeBTColour));
            bhavkeyboardSettings.Add(pnlAccOff, new GazeAwareBehavior(OnGazeChangeBTColour));
            bhavkeyboardSettings.Add(pnlespOff, new GazeAwareBehavior(OnGazeChangeBTColour));
            bhavkeyboardSettings.Add(pnlSave, new GazeAwareBehavior(OnGazeChangeBTColour));
            bhavkeyboardSettings.Add(pnlCancel, new GazeAwareBehavior(OnGazeChangeBTColour));
        }

        private void OnCancel_Click(object sender, GazeAwareEventArgs e)
        {
            if (e.HasGaze) btnCancel.PerformClick();
        }
        private void OnSave_Click(object sender, GazeAwareEventArgs e)
        {
            if (e.HasGaze) btnSave.PerformClick();
        }

        private void OnAbcOn_Click(object sender, GazeAwareEventArgs e)
        {
            if (e.HasGaze) btnAbcOn.PerformClick();
        }

        private void On123On_Click(object sender, GazeAwareEventArgs e)
        {
            if (e.HasGaze) btn123On.PerformClick();
        }

        private void OnAccOn_Click(object sender, GazeAwareEventArgs e)
        {
            if (e.HasGaze) btnAccOn.PerformClick();
        }

        private void OnEspOn_Click(object sender, GazeAwareEventArgs e)
        {
            if (e.HasGaze) btnespOn.PerformClick();
        }



        private void OnAbcOff_Click(object sender, GazeAwareEventArgs e)
        {
            if (e.HasGaze) btnAbcOff.PerformClick();
        }

        private void On123Off_Click(object sender, GazeAwareEventArgs e)
        {
            if (e.HasGaze) btn123Off.PerformClick();
        }

        private void OnAccOff_Click(object sender, GazeAwareEventArgs e)
        {
            if (e.HasGaze) btnAccOff.PerformClick();
        }

        private void OnEspOff_Click(object sender, GazeAwareEventArgs e)
        {
            if (e.HasGaze) btnespOff.PerformClick();
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
    }
}
