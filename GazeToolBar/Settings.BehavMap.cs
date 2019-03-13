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


    partial class Settings
    {

        int buttonClickDelay = 500;
        String notAssigned = "N/A";
        SettingState currentSelection = SettingState.General;

        private void connectBehaveMap()
        {
            eyeXHost.Connect(bhavSettingMap);
            eyeXHost.Connect(bhavGeneralMap);

            setupMap();
            setupGeneralMap();
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
                    setupGeneralMap();
                    currentSelection = SettingState.General;
                    break;
                case SettingState.Zoom: //Zoom settings
                    eyeXHost.Connect(bhavZoomMap);
                    setupZoomMap();
                    currentSelection = SettingState.Zoom;
                    break;
                case SettingState.Shortcut: //Shortcut settings
                    eyeXHost.Connect(bhavShortcutMap);
                    setupShortcutMap();
                    currentSelection = SettingState.Shortcut;
                    break;
                case SettingState.Rearrange: //Rearrange settings
                    eyeXHost.Connect(bhavRearrangeMap);
                    setupRearrangeMap();
                    currentSelection = SettingState.Rearrange;
                    break;
                case SettingState.Crosshair: //Crosshair settings
                    eyeXHost.Connect(bhavCrosshairMap);
                    setupCrosshairMap();
                    currentSelection = SettingState.Crosshair;
                    break;
                case SettingState.Confirm: //Confirm page 
                    eyeXHost.Connect(bhavConfirmMap);
                    setupConfirmMap();
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

        private void setupGeneralMap()
        {
            bhavGeneralMap.Add(btnFixTimeLengthMins, new GazeAwareBehavior(OnbtnFixTimeLengthMins_Click) { DelayMilliseconds = buttonClickDelay });
            bhavGeneralMap.Add(btnFixTimeLengthPlus, new GazeAwareBehavior(OnbtnFixTimeLengthPlus_Click) { DelayMilliseconds = buttonClickDelay });
            bhavGeneralMap.Add(btnFixTimeOutMins, new GazeAwareBehavior(OnbtnFixTimeOutMins_Click) { DelayMilliseconds = buttonClickDelay });
            bhavGeneralMap.Add(btnFixTimeOutPlus, new GazeAwareBehavior(OnbtnFixTimeOutPlus_Click) { DelayMilliseconds = buttonClickDelay });
            bhavGeneralMap.Add(btnAutoStart, new GazeAwareBehavior(OnbtnAutoStart_Click) { DelayMilliseconds = buttonClickDelay });
            bhavGeneralMap.Add(buttonStickyLeftClick, new GazeAwareBehavior(OnButtonStickyLeftClick_Click) { DelayMilliseconds = buttonClickDelay });
            bhavGeneralMap.Add(btnDefaults, new GazeAwareBehavior(OnBtnDefaults_Click) { DelayMilliseconds = buttonClickDelay });

            bhavGeneralMap.Add(pnlFTLMins, new GazeAwareBehavior(OnGazeChangeBTColour));
            bhavGeneralMap.Add(pnlFTLPlus, new GazeAwareBehavior(OnGazeChangeBTColour));
            bhavGeneralMap.Add(pnlFTOMins, new GazeAwareBehavior(OnGazeChangeBTColour));
            bhavGeneralMap.Add(pnlFTOPlus, new GazeAwareBehavior(OnGazeChangeBTColour));
            bhavGeneralMap.Add(pnlOtherAuto, new GazeAwareBehavior(OnGazeChangeBTColour));
            bhavGeneralMap.Add(pnlStickyLeft, new GazeAwareBehavior(OnGazeChangeBTColour));
            bhavGeneralMap.Add(pnlDefaults, new GazeAwareBehavior(OnGazeChangeBTColour));
        }

        private void setupZoomMap()
        {
            bhavZoomMap.Add(btnZoomSizeMinus, new GazeAwareBehavior(OnBtnZoomSizeMinus_Click) { DelayMilliseconds = buttonClickDelay });
            bhavZoomMap.Add(btnZoomSizePlus, new GazeAwareBehavior(OnBtnZoomSizePlus_Click) { DelayMilliseconds = buttonClickDelay });
            bhavZoomMap.Add(btnZoomAmountMinus, new GazeAwareBehavior(OnBtnZoomAmountMinus_Click) { DelayMilliseconds = buttonClickDelay });
            bhavZoomMap.Add(btnZoomAmountPlus, new GazeAwareBehavior(OnBtnZoomAmountPlus_Click) { DelayMilliseconds = buttonClickDelay });
            bhavZoomMap.Add(btnStaticZoomMode, new GazeAwareBehavior(OnStaticZoomMode_Click) { DelayMilliseconds = buttonClickDelay });
            bhavZoomMap.Add(btnDynamicZoomMode, new GazeAwareBehavior(OnDynamicZoomMode_Click) { DelayMilliseconds = buttonClickDelay });

            bhavZoomMap.Add(pnlZWSMinus, new GazeAwareBehavior(OnGazeChangeBTColour));
            bhavZoomMap.Add(pnlZWSPlus, new GazeAwareBehavior(OnGazeChangeBTColour));
            bhavZoomMap.Add(pnlZIAMinus, new GazeAwareBehavior(OnGazeChangeBTColour));
            bhavZoomMap.Add(pnlZIAPlus, new GazeAwareBehavior(OnGazeChangeBTColour));
            bhavZoomMap.Add(pnlStaticZoomMode, new GazeAwareBehavior(OnGazeChangeBTColour));
            bhavZoomMap.Add(pnlDynamicZoomMode, new GazeAwareBehavior(OnGazeChangeBTColour));
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
            bhavShortcutMap.Add(pnlSetMic, new GazeAwareBehavior(OnGazeChangeBTColour));
            bhavShortcutMap.Add(pnlClearMic, new GazeAwareBehavior(OnGazeChangeBTColour));
            bhavShortcutMap.Add(pnlSetMicOff, new GazeAwareBehavior(OnGazeChangeBTColour));
        }

        private void setupRearrangeMap()
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
            bhavRearrangeMap.Add(btnActionMic, new GazeAwareBehavior(OnBtnActionMic_Click) { DelayMilliseconds = buttonClickDelay });

            bhavRearrangeMap.Add(pnlMoveUpButton, new GazeAwareBehavior(OnGazeChangeBTColour));
            bhavRearrangeMap.Add(pnlMoveDownButton, new GazeAwareBehavior(OnGazeChangeBTColour));
            bhavRearrangeMap.Add(pnlRemoveButton, new GazeAwareBehavior(OnGazeChangeBTColour));
            bhavRearrangeMap.Add(pnlDoubleLeftClickButton, new GazeAwareBehavior(OnGazeChangeBTColour));
            bhavRearrangeMap.Add(pnlKeyboardButton, new GazeAwareBehavior(OnGazeChangeBTColour));
            bhavRearrangeMap.Add(pnlLeftClickButton, new GazeAwareBehavior(OnGazeChangeBTColour));
            bhavRearrangeMap.Add(pnlRightClickButton, new GazeAwareBehavior(OnGazeChangeBTColour));
            bhavRearrangeMap.Add(pnlScrollClickButton, new GazeAwareBehavior(OnGazeChangeBTColour));
            bhavRearrangeMap.Add(pnlSettingsButton, new GazeAwareBehavior(OnGazeChangeBTColour));
            bhavRearrangeMap.Add(pnlMicButton, new GazeAwareBehavior(OnGazeChangeBTColour));
        }

        private void setupCrosshairMap()
        {
            bhavCrosshairMap.Add(buttonCrosshairDown, new GazeAwareBehavior(OnButtonCrosshairDown_Click) { DelayMilliseconds = buttonClickDelay });
            bhavCrosshairMap.Add(buttonCrosshairUp, new GazeAwareBehavior(OnButtonCrosshairUp_Click) { DelayMilliseconds = buttonClickDelay });
            bhavCrosshairMap.Add(btnFeedback, new GazeAwareBehavior(OnBtnFeedback_Click) { DelayMilliseconds = buttonClickDelay });

            bhavCrosshairMap.Add(pnlCrosshairDownButton, new GazeAwareBehavior(OnGazeChangeBTColour));
            bhavCrosshairMap.Add(pnlCrosshairUpButton, new GazeAwareBehavior(OnGazeChangeBTColour));
            bhavCrosshairMap.Add(pnlFeedbackContent, new GazeAwareBehavior(OnGazeChangeBTColour));
        }

        private void setupConfirmMap()
        {
            bhavConfirmMap.Add(btnDefaultConfirmYes, new GazeAwareBehavior(OnBtnDefaultConfirmYes_Click) { DelayMilliseconds = buttonClickDelay });
            bhavConfirmMap.Add(btnDefaultConfirmNo, new GazeAwareBehavior(OnBtnDefaultConfirmNo_Click) { DelayMilliseconds = buttonClickDelay });

            bhavConfirmMap.Add(pnlDefaultConfirmYes, new GazeAwareBehavior(OnGazeChangeBTColour));
            bhavConfirmMap.Add(pnlDefaultConfirmNo, new GazeAwareBehavior(OnGazeChangeBTColour));
        }
        private void setupMap()
        {
            bhavSettingMap.Add(btnSave, new GazeAwareBehavior(OnbtnSave_Click) { DelayMilliseconds = buttonClickDelay });
            bhavSettingMap.Add(btnCancel, new GazeAwareBehavior(OnbtnCancel_Click) { DelayMilliseconds = buttonClickDelay });
            bhavSettingMap.Add(btnZoomSettings, new GazeAwareBehavior(OnBtnZoomSettings_Click) { DelayMilliseconds = buttonClickDelay });
            bhavSettingMap.Add(btnShortCutKeySetting, new GazeAwareBehavior(OnBtnKeyboardSetting_Click) { DelayMilliseconds = buttonClickDelay });
            bhavSettingMap.Add(btnGeneralSetting, new GazeAwareBehavior(OnBtnGeneralSetting_Click) { DelayMilliseconds = buttonClickDelay });
            bhavSettingMap.Add(btnRearrangeSetting, new GazeAwareBehavior(OnBtnRearrangeSetting_Click) { DelayMilliseconds = buttonClickDelay });
            bhavSettingMap.Add(buttonCrosshairSetting, new GazeAwareBehavior(OnbuttonCrosshairSetting_Click) { DelayMilliseconds = buttonClickDelay });

            bhavSettingMap.Add(pnlGeneralButton, new GazeAwareBehavior(OnGazeChangeBTColour));
            bhavSettingMap.Add(pnlKeysButton, new GazeAwareBehavior(OnGazeChangeBTColour));
            bhavSettingMap.Add(pnlZoomButton, new GazeAwareBehavior(OnGazeChangeBTColour));
            bhavSettingMap.Add(pnlRearrangeButton, new GazeAwareBehavior(OnGazeChangeBTColour));
            bhavSettingMap.Add(panelCrosshairButton, new GazeAwareBehavior(OnGazeChangeBTColour));
            bhavSettingMap.Add(pnlSave, new GazeAwareBehavior(OnGazeChangeBTColour));
            bhavSettingMap.Add(pnlCancel, new GazeAwareBehavior(OnGazeChangeBTColour));
        }

        //toggle border on and off on gaze to gaze to give feed back.
        private void OnGazeChangeBTColour(object s, GazeAwareEventArgs e)
        {
            var sentButton = s as Panel;
            if (sentButton != null)
            {
                sentButton.BackColor = (e.HasGaze) ? Color.Red : Color.Black;
            }
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

        private void OnbtnCancel_Click(object sender, GazeAwareEventArgs e)
        {
            btnCancel.PerformClick();
        }

        private void OnbtnSave_Click(object sender, GazeAwareEventArgs e)
        {
            btnSave.PerformClick();
        }

        private void OnbtnAutoStart_Click(object sender, GazeAwareEventArgs e)
        {
            if (e.HasGaze) btnAutoStart.PerformClick();
        }

        private void OnBtnGeneralSetting_Click(object sender, GazeAwareEventArgs e)
        {
            if (e.HasGaze) btnGeneralSetting.PerformClick();
        }

        private void OnBtnKeyboardSetting_Click(object sender, GazeAwareEventArgs e)
        {
            if (e.HasGaze) btnShortCutKeySetting.PerformClick();
        }

        private void OnBtnZoomSettings_Click(object sender, GazeAwareEventArgs e)
        {
            if (e.HasGaze) btnZoomSettings.PerformClick();
        }

        private void OnBtnRearrangeSetting_Click(object sender, GazeAwareEventArgs e)
        {
            if (e.HasGaze) btnRearrangeSetting.PerformClick();
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

        private void OnbuttonCrosshairSetting_Click(object sender, GazeAwareEventArgs e)
        {
            if (e.HasGaze) buttonCrosshairSetting.PerformClick();
        }

        private void OnButtonCrosshairDown_Click(object sender, GazeAwareEventArgs e)
        {
            if (e.HasGaze) buttonCrosshairDown.PerformClick();
        }

        private void OnButtonCrosshairUp_Click(object sender, GazeAwareEventArgs e)
        {
            if (e.HasGaze) buttonCrosshairUp.PerformClick();
        }

        private void OnBtnFeedback_Click(object sender, GazeAwareEventArgs e)
        {
            if (e.HasGaze) btnFeedback.PerformClick();
        }

        private void OnButtonStickyLeftClick_Click(object sender, GazeAwareEventArgs e)
        {
            if (e.HasGaze) buttonStickyLeftClick.PerformClick();
        }

        private void OnBtnDefaults_Click(object sender, GazeAwareEventArgs e)
        {
            if (e.HasGaze) btnDefaults.PerformClick();
        }

        private void OnBtnActionMic_Click(object sender, GazeAwareEventArgs e)
        {
            if (e.HasGaze) btnActionMic.PerformClick();
        }

        private void OnBtnDefaultConfirmYes_Click(object sender, GazeAwareEventArgs e)
        {
            if (e.HasGaze) btnDefaultConfirmYes.PerformClick();
        }
        private void OnBtnDefaultConfirmNo_Click(object sender, GazeAwareEventArgs e)
        {
            if (e.HasGaze) btnDefaultConfirmNo.PerformClick();
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
