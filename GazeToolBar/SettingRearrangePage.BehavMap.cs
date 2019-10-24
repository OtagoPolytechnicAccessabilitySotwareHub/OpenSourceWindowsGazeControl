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
    public partial class SettingRearrangePage : Form
    {
        int buttonClickDelay = 500;
        Color main = Program.readSettings.mainColour;
        Color second = Program.readSettings.secondColour;

        private void connectBehaveMap()
        {
            eyeXHost.Connect(bhavRearrangeMap);

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
            bhavRearrangeMap.Add(btnMoveUp, new GazeAwareBehavior(OnBtnMoveUp_Click) { DelayMilliseconds = buttonClickDelay });
            bhavRearrangeMap.Add(btnMoveDown, new GazeAwareBehavior(OnBtnMoveDown_Click) { DelayMilliseconds = buttonClickDelay });
            bhavRearrangeMap.Add(btnRemove, new GazeAwareBehavior(OnBtnRemove_Click) { DelayMilliseconds = buttonClickDelay });
            bhavRearrangeMap.Add(btnActionDoubleLeftClick, new GazeAwareBehavior(OnBtnActionDoubleLeftClick_Click) { DelayMilliseconds = buttonClickDelay });
            bhavRearrangeMap.Add(btnActionKeyboard, new GazeAwareBehavior(OnBtnActionKeyboard_Click) { DelayMilliseconds = buttonClickDelay });
            bhavRearrangeMap.Add(btnActionLeftClick, new GazeAwareBehavior(OnBtnActionLeftClick_Click) { DelayMilliseconds = buttonClickDelay });
            bhavRearrangeMap.Add(btnActionRightClick, new GazeAwareBehavior(OnBtnActionRightClick_Click) { DelayMilliseconds = buttonClickDelay });
            bhavRearrangeMap.Add(btnActionScrollClick, new GazeAwareBehavior(OnBtnActionScrollClick_Click) { DelayMilliseconds = buttonClickDelay });
            bhavRearrangeMap.Add(btnActionSettings, new GazeAwareBehavior(OnBtnActionSettings_Click) { DelayMilliseconds = buttonClickDelay });

            bhavRearrangeMap.Add(pnlMoveUpButton, new GazeAwareBehavior(OnGazeChangeBTColour));
            bhavRearrangeMap.Add(pnlMoveDownButton, new GazeAwareBehavior(OnGazeChangeBTColour));
            bhavRearrangeMap.Add(pnlRemoveButton, new GazeAwareBehavior(OnGazeChangeBTColour));
            bhavRearrangeMap.Add(pnlDoubleLeftClickButton, new GazeAwareBehavior(OnGazeChangeBTColour));
            bhavRearrangeMap.Add(pnlKeyboardButton, new GazeAwareBehavior(OnGazeChangeBTColour));
            bhavRearrangeMap.Add(pnlLeftClickButton, new GazeAwareBehavior(OnGazeChangeBTColour));
            bhavRearrangeMap.Add(pnlRightClickButton, new GazeAwareBehavior(OnGazeChangeBTColour));
            bhavRearrangeMap.Add(pnlScrollClickButton, new GazeAwareBehavior(OnGazeChangeBTColour));
            bhavRearrangeMap.Add(pnlSettingsButton, new GazeAwareBehavior(OnGazeChangeBTColour));
        }   

        private void OnBtnMoveUp_Click(object sender, GazeAwareEventArgs e)
        {
            if (e.HasGaze) btnMoveUp.PerformClick();
        }

        private void OnBtnMoveDown_Click(object sender, GazeAwareEventArgs e)
        {
            if (e.HasGaze) btnMoveDown.PerformClick();
        }

        private void OnBtnRemove_Click(object sender, GazeAwareEventArgs e)
        {
            if (e.HasGaze) btnRemove.PerformClick();
        }

        private void OnBtnActionDoubleLeftClick_Click(object sender, GazeAwareEventArgs e)
        {
            if (e.HasGaze) btnActionDoubleLeftClick.PerformClick();
        }

        private void OnBtnActionKeyboard_Click(object sender, GazeAwareEventArgs e)
        {
            if (e.HasGaze) btnActionKeyboard.PerformClick();
        }

        private void OnBtnActionLeftClick_Click(object sender, GazeAwareEventArgs e)
        {
            if (e.HasGaze) btnActionLeftClick.PerformClick();
        }

        private void OnBtnActionRightClick_Click(object sender, GazeAwareEventArgs e)
        {
            if (e.HasGaze) btnActionRightClick.PerformClick();
        }

        private void OnBtnActionScrollClick_Click(object sender, GazeAwareEventArgs e)
        {
            if (e.HasGaze) btnActionScrollClick.PerformClick();
        }

        private void OnBtnActionSettings_Click(object sender, GazeAwareEventArgs e)
        {
            if (e.HasGaze) btnActionSettings.PerformClick();
        }



    }
}
