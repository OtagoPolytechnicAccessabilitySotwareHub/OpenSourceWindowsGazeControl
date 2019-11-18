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


    partial class SettingsForm
    {

        int buttonClickDelay = 800;
        String notAssigned = "N/A";
        SettingState currentSelection = SettingState.General;

        private void connectBehaveMap()
        {
            eyeXHost.Connect(bhavSettingMap);
            eyeXHost.Connect(bhavGeneralMap);

            setupMap();
            setupShortcutMap();
        }
        
        private void removeCurrentMap()
        {
            switch (currentSelection)
            {
                case SettingState.General: //General settings
                    bhavGeneralMap.Dispose();
                    break;
                case SettingState.Zoom: //Zoom settings
                    bhavZoomMap.Dispose();
                    break;
                case SettingState.Shortcut: //Shortcut settings
                    bhavShortcutMap.Dispose();
                    break;
                case SettingState.Rearrange: //Rearrange settings
                    bhavRearrangeMap.Dispose();
                    break;
                case SettingState.Crosshair: //Crosshair settings
                    bhavCrosshairMap.Dispose();
                    break;
                case SettingState.Confirm: //Confirm page
                    bhavConfirmMap.Dispose();
                    break;
                default:
                    break;
            }
        }
        
        
        public void UseMap(SettingState mapToAdd)
        {
            removeCurrentMap();
            switch (mapToAdd)
            {
                case SettingState.General: //General settings
                    eyeXHost.Connect(bhavGeneralMap);
                    currentSelection = SettingState.General;
                    break;
                case SettingState.Zoom: //Zoom settings
                    eyeXHost.Connect(bhavZoomMap);
                    currentSelection = SettingState.Zoom;
                    break;
                case SettingState.Shortcut: //Shortcut settings
                    eyeXHost.Connect(bhavShortcutMap);
                    setupShortcutMap();
                    currentSelection = SettingState.Shortcut;
                    break;
                case SettingState.Rearrange: //Rearrange settings
                    eyeXHost.Connect(bhavRearrangeMap);
                    currentSelection = SettingState.Rearrange;
                    break;
                case SettingState.Crosshair: //Crosshair settings
                    eyeXHost.Connect(bhavCrosshairMap);
                    currentSelection = SettingState.Crosshair;
                    break;
                case SettingState.Confirm: //Confirm page 
                    eyeXHost.Connect(bhavConfirmMap);
                    currentSelection = SettingState.Confirm;
                    break;
                default:
                    break;
            }
        }
        

        public void RemoveAndAddMainBhavMap(string removeOrAdd)
        {
            if (removeOrAdd == "add")
            {
                eyeXHost.Connect(bhavSettingMap);
                setupMap();
            }
            else if (removeOrAdd == "remove")
            {
                bhavSettingMap.Dispose();
            }
        }

        

        private void setupShortcutMap()
        {
            bhavShortcutMap.Add(btFKeyLeftClick, new GazeAwareBehavior(btFKeyLeftClick_Click) { DelayMilliseconds = buttonClickDelay });
            bhavShortcutMap.Add(btFKeyRightClick, new GazeAwareBehavior(btFKeyRightClick_Click) { DelayMilliseconds = buttonClickDelay });
            bhavShortcutMap.Add(btFKeyDoubleClick, new GazeAwareBehavior(btFKeyDoubleClick_Click) { DelayMilliseconds = buttonClickDelay });
            bhavShortcutMap.Add(btFKeyScroll, new GazeAwareBehavior(btFKeyScroll_Click) { DelayMilliseconds = buttonClickDelay });
            bhavShortcutMap.Add(btClearFKeyLeftClick, new GazeAwareBehavior(btClearFKeyLeftClick_Click) { DelayMilliseconds = buttonClickDelay });
            bhavShortcutMap.Add(btClearFKeyRightClick, new GazeAwareBehavior(btClearFKeyRightClick_Click) { DelayMilliseconds = buttonClickDelay });
            bhavShortcutMap.Add(btClearFKeyDoubleClick, new GazeAwareBehavior(btClearFKeyDoubleClick_Click) { DelayMilliseconds = buttonClickDelay });
            bhavShortcutMap.Add(btClearFKeyScroll, new GazeAwareBehavior(btClearFKeyScroll_Click) { DelayMilliseconds = buttonClickDelay });
            //bhavShortcutMap.Add(btnSetMic, new GazeAwareBehavior(btnSetMic_Click) { DelayMilliseconds = buttonClickDelay });
            //bhavShortcutMap.Add(btnClearMic, new GazeAwareBehavior(btnClearMic_Click) { DelayMilliseconds = buttonClickDelay });
            //bhavShortcutMap.Add(btnSetMicOff, new GazeAwareBehavior(btnSetMicOff_Click) { DelayMilliseconds = buttonClickDelay });

            bhavShortcutMap.Add(pnlFKeyHighlight1, new GazeAwareBehavior(OnGazeChangeBTColour));
            bhavShortcutMap.Add(pnlFKeyHighlight2, new GazeAwareBehavior(OnGazeChangeBTColour));
            bhavShortcutMap.Add(pnlFKeyHighlight3, new GazeAwareBehavior(OnGazeChangeBTColour));
            bhavShortcutMap.Add(pnlFKeyHighlight4, new GazeAwareBehavior(OnGazeChangeBTColour));
            bhavShortcutMap.Add(pnlFKeyHighlight5, new GazeAwareBehavior(OnGazeChangeBTColour));
            bhavShortcutMap.Add(pnlFKeyHighlight6, new GazeAwareBehavior(OnGazeChangeBTColour));
            bhavShortcutMap.Add(pnlFKeyHighlight7, new GazeAwareBehavior(OnGazeChangeBTColour));
            bhavShortcutMap.Add(pnlFKeyHighlight8, new GazeAwareBehavior(OnGazeChangeBTColour));
        }

        

        
        private void setupMap()
        {
            bhavSettingMap.Add(btnSave, new GazeAwareBehavior(OnbtnSave_Click) { DelayMilliseconds = buttonClickDelay });
            bhavSettingMap.Add(btnCancel, new GazeAwareBehavior(OnbtnCancel_Click) { DelayMilliseconds = buttonClickDelay });

            bhavSettingMap.Add(pnlSave, new GazeAwareBehavior(OnGazeChangeBTColour));
            bhavSettingMap.Add(pnlCancel, new GazeAwareBehavior(OnGazeChangeBTColour));
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
        
        private void OnbtnCancel_Click(object sender, GazeAwareEventArgs e)
        {
            if (e.HasGaze) btnCancel.PerformClick();
        }

        private void OnbtnSave_Click(object sender, GazeAwareEventArgs e)
        {
            if (e.HasGaze) btnSave.PerformClick();
        }

       


        //====================================================================================


        //Shortcut keys panel buy button event methods. 


        //====================================================================================

        ActionToBePerformed actionToAssignKey;

        private void btFKeyLeftClick_Click(object sender, EventArgs e)
        {
            WaitForUserKeyPress = true;
            actionToAssignKey = ActionToBePerformed.LeftClick;
            lbFKeyFeedback.Text = "please press a key";
        }

        private void btFKeyRightClick_Click(object sender, EventArgs e)
        {
            WaitForUserKeyPress = true;
            actionToAssignKey = ActionToBePerformed.RightClick;
            lbFKeyFeedback.Text = "please press a key";
        }

        private void btFKeyDoubleClick_Click(object sender, EventArgs e)
        {
            WaitForUserKeyPress = true;
            actionToAssignKey = ActionToBePerformed.DoubleClick;
            lbFKeyFeedback.Text = "please press a key";
        }

        private void btFKeyScroll_Click(object sender, EventArgs e)
        {
            WaitForUserKeyPress = true;
            actionToAssignKey = ActionToBePerformed.Scroll;
            lbFKeyFeedback.Text = "please press a key";
        }

        /*
        private void btnSetMic_Click(object sender, EventArgs e)
        {
            WaitForUserKeyPress = true;
            actionToAssignKey = ActionToBePerformed.MicInput;
            lbFKeyFeedback.Text = "please press a key";
        }
        */
        /*
        private void btnSetMicOff_Click(object sender, EventArgs e)
        {
            WaitForUserKeyPress = true;
            actionToAssignKey = ActionToBePerformed.MicInputOff;
            lbFKeyFeedback.Text = "please press a key";
        }
        */
        //private void btFKeyDrapAndDrop_Click(object sender, EventArgs e)
        //{

        //}


        //Clear key map

        private void btClearFKeyLeftClick_Click(object sender, EventArgs e)
        {
            form1.shortCutKeyWorker.keyAssignments[ActionToBePerformed.LeftClick] = notAssigned;
            lbLeft.Text = notAssigned;
        }

        private void btClearFKeyRightClick_Click(object sender, EventArgs e)
        {
            form1.shortCutKeyWorker.keyAssignments[ActionToBePerformed.RightClick] = notAssigned;
            lbRight.Text = notAssigned;
        }

        private void btClearFKeyDoubleClick_Click(object sender, EventArgs e)
        {
            form1.shortCutKeyWorker.keyAssignments[ActionToBePerformed.DoubleClick] = notAssigned;
            lbDouble.Text = notAssigned;
        }

        private void btClearFKeyScroll_Click(object sender, EventArgs e)
        {
            form1.shortCutKeyWorker.keyAssignments[ActionToBePerformed.Scroll] = notAssigned;
            lbScroll.Text = notAssigned;
        }

        /*
        private void btnClearMic_Click(object sender, EventArgs e)
        {
            form1.shortCutKeyWorker.keyAssignments[ActionToBePerformed.MicInput] = notAssigned;
            lbMicOn.Text = notAssigned;
            form1.shortCutKeyWorker.keyAssignments[ActionToBePerformed.MicInputOff] = notAssigned;
            lbMicOff.Text = notAssigned;
        }
        */

        //private void btClearFKeyDrapAndDrop_Click(object sender, EventArgs e)
        //{

        //}


    }
}
