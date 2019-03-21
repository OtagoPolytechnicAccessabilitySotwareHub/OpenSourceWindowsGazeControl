using System;
using System.Windows.Forms;
using ShellLib;
using System.Drawing;
using Newtonsoft.Json;
using System.IO;
using System.Collections.Generic;
using EyeXFramework.Forms;
using System.Linq;

namespace GazeToolBar
{
    public enum SettingState { General, Zoom, Shortcut, Rearrange, Crosshair, Confirm }
    public partial class Settings : Form
    {
        private Form1 form1;
        private bool[] onOff;

        private Panel shownPanel;

        private Size confirmSize = new Size(595, 300);
        private bool WaitForUserKeyPress;
        private static FormsEyeXHost eyeXHost;
        private List<Point> sidebarActionInitPositions = new List<Point>();
        private List<String> selectedActions = new List<String>();
        private List<Button> actionButtons = new List<Button>();
        private List<Panel> actionPanels = new List<Panel>();
        private String selectionButton = "";
        private Dictionary<String, Button> buttonMap = new Dictionary<string, Button>();
        private bool stickyLeft, selectionFeedback, dynamicZoom;

        private List<Panel> fKeyPannels;

        public Settings(Form1 form1, FormsEyeXHost EyeXHost)
        {
            eyeXHost = EyeXHost;
            InitializeComponent();
            InitSidebarActions();
            pnlPageKeyboard.Hide();
            pnlCrosshairPage.Hide();
            pnlRearrange.Hide();
            pnlZoomSettings.Hide();
            pnlDefaultConfirm.Hide();
            ChangeButtonColor(btnGeneralSetting, true, true);
            this.form1 = form1;
            //This code make setting form full screen
            FormBorderStyle = FormBorderStyle.None;
            WindowState = FormWindowState.Maximized;
            //End
            controlRelocateAndResize();
            //tabControlMain.Size = ReletiveSize.TabControlSize;
            onOff = new bool[5];
            for (int i = 0; i < onOff.Length; i++)
            {
                onOff[i] = false;
            }

            shownPanel = pnlGeneral;

            //Store reference to short cut assignment panels in a list so they can be iterated over and set their on screen positions relative form size.
            fKeyPannels = new List<Panel>() { pnlLeftClick, pnlRightClick, pnlDoubleClick, pnlScroll, pnlMic };// pnlDragAndDrop };
            //Set panel positions.
            setFkeyPanelWidth(fKeyPannels);

            //set initial values of mapped keys to on screen label.
            lbDouble.Text = form1.FKeyMapDictionary[ActionToBePerformed.DoubleClick];
            lbRight.Text = form1.FKeyMapDictionary[ActionToBePerformed.RightClick];
            lbLeft.Text = form1.FKeyMapDictionary[ActionToBePerformed.LeftClick];
            lbScroll.Text = form1.FKeyMapDictionary[ActionToBePerformed.Scroll];
            //lbMicOn.Text = form1.FKeyMapDictionary[ActionToBePerformed.MicInput];
            //lbMicOff.Text = form1.FKeyMapDictionary[ActionToBePerformed.MicInputOff];
            WaitForUserKeyPress = false;

            stickyLeft = Program.readSettings.stickyLeftClick;
            if (stickyLeft)
                buttonStickyLeftClick.BackColor = Color.White;

            selectionFeedback = Program.readSettings.selectionFeedback;
            if (selectionFeedback)
                btnFeedback.BackColor = Color.White;

            dynamicZoom = Program.readSettings.dynamicZoom;
            if (dynamicZoom)
            {
                btnDynamicZoomMode.BackColor = Color.White;
                btnDynamicZoomMode.ForeColor = Color.Black;
            }
            else
            {
                btnStaticZoomMode.BackColor = Color.White;
                btnStaticZoomMode.ForeColor = Color.Black;
            }

            form1.LowLevelKeyBoardHook.OnKeyPressed += GetKeyPress;

        }

        private void controlRelocateAndResize()
        {
            int percentageSize = 400; //Higher number for smaller trackbars
            pnlSwitchSetting.Location = ReletiveSize.panelSwitchSettingLocation(pnlSwitchSetting.Width, pnlSwitchSetting.Height);
            panelSaveAndCancel.Location = ReletiveSize.panelSaveAndCancel(panelSaveAndCancel.Width, panelSaveAndCancel.Height);

            //General Settings size and location
            pnlGeneral.Location = ReletiveSize.mainPanelLocation(pnlSwitchSetting.Location.Y, pnlSwitchSetting.Height);
            pnlGeneral.Size = ReletiveSize.panelGeneralSize(panelSaveAndCancel.Location.Y, pnlGeneral.Location.Y);
            //Precision panel
            panelPrecision.Location = ReletiveSize.distribute(pnlGeneral, panelPrecision.Location.X, 1, 3, "h", 0);
            panelPrecision.Size = new Size(pnlGeneral.Size.Width, panelPrecision.Size.Height);
            pnlFixTimeLengthContent.Location = ReletiveSize.distribute(panelPrecision, pnlFixTimeLengthContent.Location.Y, 1, 1, "w", 0.1);
            pnlFixTimeLengthContent.Size = ReletiveSize.controlLength(panelPrecision, pnlFixTimeLengthContent.Size.Height, 0.9);
            double generalPercentage = (double)(pnlFixTimeLengthContent.Size.Width - percentageSize) / (double)pnlFixTimeLengthContent.Size.Width;
            trackBarFixTimeLength.Size = ReletiveSize.controlLength(pnlFixTimeLengthContent, trackBarFixTimeLength.Size.Height, generalPercentage);
            pnlFTLPlus.Location = ReletiveSize.reletiveLocation(trackBarFixTimeLength, pnlFTLPlus.Location.Y, 7, 'v');
            lblFixationDetectionTimeLength.Location = ReletiveSize.labelPosition(panelPrecision, lblFixationDetectionTimeLength);
            //Fixation time out panel
            pnlFixationTimeOut.Location = ReletiveSize.distributeToBottom(pnlGeneral, pnlFixationTimeOut.Location.X, pnlFixationTimeOut.Height, 2, 3, "h", 0);
            pnlFixationTimeOut.Size = new Size(pnlGeneral.Size.Width, pnlFixationTimeOut.Size.Height);
            pnlFixTimeOutContent.Location = new Point(pnlFixTimeLengthContent.Location.X, pnlFixTimeOutContent.Location.Y);
            pnlFixTimeOutContent.Size = pnlFixTimeLengthContent.Size;
            trackBarFixTimeOut.Size = trackBarFixTimeLength.Size;
            pnlFTOPlus.Location = new Point(pnlFTLPlus.Location.X, pnlFTOPlus.Location.Y);
            lblSpeed.Location = ReletiveSize.labelPosition(pnlFixationTimeOut, lblSpeed);
            //Panel other
            panelOther.Location = ReletiveSize.distributeToBottom(pnlGeneral, panelOther.Location.X, panelOther.Height, 3, 3, "h", 0);
            panelOther.Size = new Size(pnlGeneral.Size.Width, panelOther.Size.Height);
            pnlDefaults.Location = ReletiveSize.distribute(panelOther, pnlDefaults.Location.Y, 1, 3, "w", 0.15);
            pnlOtherAuto.Location = ReletiveSize.distribute(panelOther, pnlOtherAuto.Location.Y, 2, 3, "w", 0.45);
            pnlStickyLeft.Location = ReletiveSize.distribute(panelOther, pnlStickyLeft.Location.Y, 3, 3, "w", 0.75);
            lblOther.Location = ReletiveSize.labelPosition(panelOther, lblOther);
            //Panel confirm defaults
            pnlDefaultConfirm.Size = confirmSize;
            pnlDefaultConfirm.Location = ReletiveSize.centerLocation(pnlGeneral, pnlDefaultConfirm);
            //Shortcut settings panel
            //pnlPageKeyboard.Width = Constants.SCREEN_SIZE.Width - 20;
            pnlPageKeyboard.Location = ReletiveSize.mainPanelLocation(pnlSwitchSetting.Location.Y, pnlSwitchSetting.Height);
            pnlPageKeyboard.Size = ReletiveSize.panelGeneralSize(panelSaveAndCancel.Location.Y, pnlPageKeyboard.Location.Y);
            //Set feed back label to the center of the screen.
            lbFKeyFeedback.Location = new Point((pnlPageKeyboard.Width / 2) - (lbFKeyFeedback.Width / 2), lbFKeyFeedback.Location.Y);
            //pnlPageKeyboard.Location = ReletiveSize.mainPanelLocation(pnlSwitchSetting.Location.Y, pnlSwitchSetting.Height);
            //Zoom Settings size and location
            //Main Panel
            pnlZoomSettings.Location = ReletiveSize.mainPanelLocation(pnlSwitchSetting.Location.Y, pnlSwitchSetting.Height);
            pnlZoomSettings.Size = ReletiveSize.panelGeneralSize(panelSaveAndCancel.Location.Y, pnlZoomSettings.Location.Y);
            //Zoom size panel
            pnlZoomSize.Location = ReletiveSize.distribute(pnlZoomSettings, pnlZoomSize.Location.X, 1, 2, "h", 0);
            pnlZoomSize.Size = new Size(pnlZoomSettings.Size.Width, pnlZoomSize.Size.Height);
            pnlZoomSizeContent.Location = ReletiveSize.distribute(pnlZoomSize, pnlZoomSizeContent.Location.Y, 1, 1, "w", 0.1);
            pnlZoomSizeContent.Size = ReletiveSize.controlLength(pnlZoomSettings, pnlZoomSizeContent.Size.Height, 0.9);
            double zoomPercentage = (double)(pnlZoomSizeContent.Size.Width - percentageSize) / (double)pnlZoomSizeContent.Size.Width;
            trackBarZoomWindowSize.Size = ReletiveSize.controlLength(pnlZoomSizeContent, trackBarZoomWindowSize.Size.Height, zoomPercentage);
            pnlZWSPlus.Location = ReletiveSize.reletiveLocation(trackBarZoomWindowSize, pnlZWSPlus.Location.Y, 7, 'v');
            labZoomWindowSize.Location = ReletiveSize.labelPosition(pnlZoomSize, labZoomWindowSize);
            //Zoom amount panel
            pnlZoomAmount.Location = ReletiveSize.distribute(pnlZoomSettings, pnlZoomAmount.Location.X, 2, 2, "h", 0);
            pnlZoomAmount.Size = new Size(pnlZoomSettings.Size.Width, pnlZoomAmount.Size.Height);
            pnlZoomAmountContent.Location = new Point(pnlZoomSizeContent.Location.X, pnlZoomAmountContent.Location.Y);
            pnlZoomAmountContent.Size = pnlZoomSizeContent.Size;
            trackBarZoomAmount.Size = trackBarZoomWindowSize.Size;
            pnlZIAPlus.Location = new Point(pnlZWSPlus.Location.X, pnlZIAPlus.Location.Y);
            labZoomAmount.Location = ReletiveSize.labelPosition(pnlZoomAmount, labZoomAmount);
            //Rearrange panel
            pnlRearrange.Location = ReletiveSize.mainPanelLocation(pnlSwitchSetting.Location.Y, pnlSwitchSetting.Height);
            pnlRearrange.Size = ReletiveSize.panelRearrangeSize(panelSaveAndCancel.Location.Y, pnlRearrange.Location.Y);
            pnlRearrangeControls.Location = ReletiveSize.centerLocation(pnlRearrange, pnlRearrangeControls);
            //Crosshair Settings size and location
            //Main panel
            pnlCrosshairPage.Location = ReletiveSize.mainPanelLocation(pnlSwitchSetting.Location.Y, pnlSwitchSetting.Height);
            pnlCrosshairPage.Size = ReletiveSize.panelGeneralSize(panelSaveAndCancel.Location.Y, pnlCrosshairPage.Location.Y);
            //Crosshair selection panel
            panelCrosshairSelection.Location = ReletiveSize.distributeToBottom(pnlCrosshairPage, panelCrosshairSelection.Location.X, panelCrosshairSelection.Height, 1, 3, "h", 0);
            panelCrosshairSelection.Size = new Size(pnlCrosshairPage.Size.Width, panelCrosshairSelection.Size.Height);
            panelCrosshairHolder.Location = ReletiveSize.distribute(panelCrosshairSelection, panelCrosshairHolder.Location.Y, 1, 1, "w", 0.1);
            panelCrosshairHolder.Size = ReletiveSize.controlLength(pnlCrosshairPage, panelCrosshairHolder.Size.Height, 0.9);
            double crosshairPercentage = (double)(panelCrosshairHolder.Size.Width - percentageSize) / (double)panelCrosshairHolder.Size.Width;
            trackBarCrosshair.Size = ReletiveSize.controlLength(panelCrosshairHolder, trackBarCrosshair.Size.Height, crosshairPercentage);
            pnlCrosshairUpButton.Location = ReletiveSize.reletiveLocation(trackBarCrosshair, pnlCrosshairUpButton.Location.Y, 7, 'v');
            labCrosshairType.Location = ReletiveSize.labelPosition(panelCrosshairSelection, labCrosshairType);
            //Crosshair picture
            pictureBoxCrosshairPreview.Location = ReletiveSize.distribute(pnlCrosshairPage, pictureBoxCrosshairPreview.Location.X, 2, 3, "h", 0);
            pictureBoxCrosshairPreview.Location = ReletiveSize.distribute(pnlCrosshairPage, pictureBoxCrosshairPreview.Location.Y, 1, 1, "w", 0.5);
            pictureBoxCrosshairPreview.Left = (pictureBoxCrosshairPreview.Location.X - (pictureBoxCrosshairPreview.Width / 2));
            //Feedback panel
            pnlFeedback.Location = ReletiveSize.distributeToBottom(pnlCrosshairPage, pnlFeedback.Location.X, pnlFeedback.Height, 3, 3, "h", 0);
            pnlFeedback.Size = new Size(pnlCrosshairPage.Size.Width, pnlFeedback.Size.Height);
            pnlFeedbackContent.Location = new Point((pnlFeedback.Size.Width / 2) - (pnlFeedbackContent.Width / 2), pnlFeedbackContent.Location.Y);
            pnlFeedbackContent.Location = new Point((pnlFeedback.Size.Width / 2) - (pnlFeedbackContent.Width / 2), pnlFeedbackContent.Location.Y);
            labFeedback.Location = ReletiveSize.labelPosition(pnlFeedback, labFeedback);
        }

        //private void btnChangeSide_Click(object sender, EventArgs e)
        //{
        //    if (OnTheRight)
        //    {
        //        changeSide("On left", ApplicationDesktopToolbar.AppBarEdges.Left, false);
        //        ChangeButtonColor(btnChangeSide, true, false);
        //    }
        //    else
        //    {
        //        changeSide("On Right", ApplicationDesktopToolbar.AppBarEdges.Right, true);
        //        ChangeButtonColor(btnChangeSide, false, false);
        //    }
        //}

        //private void changeSide(string text, ApplicationDesktopToolbar.AppBarEdges edge, bool flag)
        //{
        //    lblIndicationLeftOrRight.Text = text;
        //    form1.Edge = edge;
        //    OnTheRight = flag;
        //}

        ///
        private void btnAutoStart_Click(object sender, EventArgs e)
        {

            if (Program.onStartUp)
            {
                AutoStart.SetOff();
                Program.onStartUp = !Program.onStartUp;
                ChangeButtonColor(btnAutoStart, false, false);
            }
            else
            {
                if (AutoStart.SetOn())
                {
                    Program.onStartUp = !Program.onStartUp;
                    ChangeButtonColor(btnAutoStart, true, false);
                }
            }

            form1.OnStartTextChange();

        }
        ///
        public void ChangeButtonColor(Button button, bool onOff, bool hasText)
        {

            button.BackColor = onOff ? Constants.SelectedColor : Constants.SettingButtonColor;
            if (hasText)
            {
                if (onOff)
                {
                    button.ForeColor = Color.Black;
                }
                else
                {
                    button.ForeColor = Color.White;
                }
            }
        }

        //private void btnGaze_Click(object sender, EventArgs e)
        //{

        //    gazeOrSwitch = GazeOrSwitch.GAZE;
        //    changeSitchGaze(gazeOrSwitch);

        //}
        //
        private void btnCancel_Click(object sender, EventArgs e)
        {
            Close();
        }

        //private void btnSwitch_Click(object sender, EventArgs e)
        //{

        //    gazeOrSwitch = GazeOrSwitch.SWITCH;
        //    changeSitchGaze(gazeOrSwitch);
        //}

        //private void changeSitchGaze(GazeOrSwitch gs)
        //{
        //    switch (gs)
        //    {
        //        case GazeOrSwitch.GAZE:
        //            ChangeButtonColor(btnGaze, !onOff[0], false);
        //            ChangeButtonColor(btnSwitch, onOff[0], false);
        //            break;
        //        case GazeOrSwitch.SWITCH:
        //            ChangeButtonColor(btnGaze, onOff[0], false);
        //            ChangeButtonColor(btnSwitch, !onOff[0], false);
        //            break;
        //    }
        //}



        //private void lblOnOff(Label l, bool b)
        //{
        //    if(b)
        //    {
        //        l.Text = "On";
        //    }
        //    else
        //    {
        //        l.Text = "Off";
        //    }

        //}


        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                SettingJSON setting = new SettingJSON();

                setting.fixationTimeLength = trackBarFixTimeLength.Value * Constants.GAP_TIME_LENGTH + Constants.MIN_TIME_LENGTH;
                setting.fixationTimeOut = trackBarFixTimeOut.Value * Constants.GAP_TIME_OUT + Constants.MIN_TIME_OUT;
                setting.leftClick = lbLeft.Text;
                setting.doubleClick = lbDouble.Text;
                setting.rightClick = lbRight.Text;
                setting.scroll = lbScroll.Text;
                setting.micInput = lbMicOn.Text;
                setting.micInputOff = lbMicOff.Text;
                setting.sidebar = selectedActions.ToArray<string>();
                setting.Crosshair = trackBarCrosshair.Value;
                setting.maxZoom = trackBarZoomAmount.Value;
                setting.zoomWindowSize = trackBarZoomWindowSize.Value;
                setting.stickyLeftClick = stickyLeft;
                setting.selectionFeedback = selectionFeedback;
                setting.dynamicZoom = dynamicZoom;

                Program.readSettings.sidebar = selectedActions.ToArray<string>();
                Program.readSettings.maxZoom = setting.maxZoom;
                form1.ArrangeSidebar(Program.readSettings.sidebar);
                string settings = JsonConvert.SerializeObject(setting);
                File.WriteAllText(Program.path, settings);

                Program.readSettings = setting;
                //form1.stateManager.RefreshZoom();

                form1.NotifyIcon.BalloonTipTitle = "Saving success";
                form1.NotifyIcon.BalloonTipText = "Your settings are successfuly saved";
                this.Close();
                form1.stateManager.ResetMagnifier();
                form1.NotifyIcon.ShowBalloonTip(2000);
            }
            catch (Exception exception)
            {
                form1.NotifyIcon.BalloonTipTitle = "Saving error";
                form1.NotifyIcon.BalloonTipText = "For some reason, your settings are not successfuly saved, click me to show error message";
                form1.NotifyIcon.Tag = exception.Message;
                this.Close();
                form1.NotifyIcon.BalloonTipClicked += NotifyIcon_BalloonTipClicked;
                form1.NotifyIcon.ShowBalloonTip(5000);
            }
        }

        private void resetSettings()
        {
            File.Delete(Program.path);
            Program.ReadWriteJson();
            Settings_Load(this, new System.EventArgs());
            AutoStart.SetOff();
            Program.onStartUp = !Program.onStartUp;
            ChangeButtonColor(btnAutoStart, false, false);
            resetSideBar();
            stickyLeft = Program.readSettings.stickyLeftClick;
            buttonStickyLeftClick.BackColor = Color.Black;
            selectionFeedback = Program.readSettings.selectionFeedback;
            btnFeedback.BackColor = Color.White;
            dynamicZoom = false;
        }

        void NotifyIcon_BalloonTipClicked(object sender, EventArgs e)
        {
            MessageBox.Show((String)((NotifyIcon)sender).Tag, "Exception", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private void Settings_Load(object sender, EventArgs e)
        {
            Program.ReadWriteJson();

            if (Program.onStartUp)
            {
                ChangeButtonColor(btnAutoStart, true, false);
            }
            else
            {
                ChangeButtonColor(btnAutoStart, false, false);
            }


            //TODO: Need to be replaced
            trackBarFixTimeLength.Value = (Program.readSettings.fixationTimeLength - Constants.MIN_TIME_LENGTH) / Constants.GAP_TIME_LENGTH;
            trackBarFixTimeOut.Value = (Program.readSettings.fixationTimeOut - Constants.MIN_TIME_OUT) / Constants.GAP_TIME_OUT;
            trackBarZoomAmount.Value = Program.readSettings.maxZoom;
            trackBarZoomWindowSize.Value = Program.readSettings.zoomWindowSize;
            lbLeft.Text = Program.readSettings.leftClick;
            lbDouble.Text = Program.readSettings.doubleClick;
            lbRight.Text = Program.readSettings.rightClick;
            lbScroll.Text = Program.readSettings.scroll;
            lbMicOn.Text = Program.readSettings.micInput;
            lbMicOff.Text = Program.readSettings.micInputOff;

            trackBarCrosshair.Value = Program.readSettings.Crosshair;
            UpdateCrosshair();
        }

        private void changePanel(Panel pnlToShow)
        {
            shownPanel.SendToBack();
            shownPanel.Hide();
            shownPanel = pnlToShow;
            shownPanel.BringToFront();
            shownPanel.Show();
        }

        private void btnGeneralSetting_Click(object sender, EventArgs e)
        {
            if (shownPanel != pnlGeneral)
            {
                changePanel(pnlGeneral);
                UseMap(SettingState.General);

                ChangeButtonColor(btnShortCutKeySetting, false, true);
                ChangeButtonColor(btnZoomSettings, false, true);
                ChangeButtonColor(btnRearrangeSetting, false, true);
                ChangeButtonColor(buttonCrosshairSetting, false, true);
                ChangeButtonColor(btnGeneralSetting, true, true);

                WaitForUserKeyPress = false;
            }
        }

        private void btnKeyBoardSetting_Click(object sender, EventArgs e)
        {
            if (shownPanel != pnlPageKeyboard)
            {
                changePanel(pnlPageKeyboard);
                UseMap(SettingState.Shortcut);

                ChangeButtonColor(btnGeneralSetting, false, true);
                ChangeButtonColor(btnZoomSettings, false, true);
                ChangeButtonColor(btnRearrangeSetting, false, true);
                ChangeButtonColor(buttonCrosshairSetting, false, true);
                ChangeButtonColor(btnShortCutKeySetting, true, true);

                lbFKeyFeedback.Text = "";
            }
        }

        private void btnZoomSettings_Click(object sender, EventArgs e)
        {
            if (shownPanel != pnlZoomSettings)
            {
                changePanel(pnlZoomSettings);
                UseMap(SettingState.Zoom);

                ChangeButtonColor(btnGeneralSetting, false, true);
                ChangeButtonColor(btnShortCutKeySetting, false, true);
                ChangeButtonColor(btnRearrangeSetting, false, true);
                ChangeButtonColor(buttonCrosshairSetting, false, true);
                ChangeButtonColor(btnZoomSettings, true, true);
            }
        }

        private void btnRearrangeSetting_Click(object sender, EventArgs e)
        {
            if (shownPanel != pnlRearrange)
            {
                changePanel(pnlRearrange);
                UseMap(SettingState.Rearrange);
                panelSaveAndCancel.BringToFront();

                ChangeButtonColor(btnGeneralSetting, false, true);
                ChangeButtonColor(btnShortCutKeySetting, false, true);
                ChangeButtonColor(btnZoomSettings, false, true);
                ChangeButtonColor(buttonCrosshairSetting, false, true);
                ChangeButtonColor(btnRearrangeSetting, true, true);
                RefreshActions();
            }
        }

        private void buttonCrosshairSetting_Click(object sender, EventArgs e)
        {
            changePanel(pnlCrosshairPage);
            UseMap(SettingState.Crosshair);

            ChangeButtonColor(btnGeneralSetting, false, true);
            ChangeButtonColor(btnShortCutKeySetting, false, true);
            ChangeButtonColor(btnZoomSettings, false, true);
            ChangeButtonColor(btnRearrangeSetting, false, true);
            ChangeButtonColor(buttonCrosshairSetting, true, true);

            WaitForUserKeyPress = false;
        }

        private void buttonCrosshairDown_Click(object sender, EventArgs e)
        {
            if (trackBarCrosshair.Value == 0)
            {
                trackBarCrosshair.Value = trackBarCrosshair.Maximum;
            }
            else
            {
                trackBarCrosshair.Value--;
            }
            UpdateCrosshair();
        }

        private void buttonCrosshairUp_Click(object sender, EventArgs e)
        {
            if (trackBarCrosshair.Value == trackBarCrosshair.Maximum)
            {
                trackBarCrosshair.Value = 0;
            }
            else
            {
                trackBarCrosshair.Value++;
            }

            UpdateCrosshair();
        }

        private void trackBarCrosshair_ValueChanged(object sender, EventArgs e)
        {
            UpdateCrosshair();
        }

        public void UpdateCrosshair()
        {
            pictureBoxCrosshairPreview.Image = DrawingForm.GetCrossHairImage((DrawingForm.CrossHair)trackBarCrosshair.Value);
        }

        private void changeTrackBarValue(TrackBar trackbar, String IncrementOrDecrement)
        {
            switch (IncrementOrDecrement)
            {
                case "I":
                    if (trackbar.Value != trackbar.Maximum) { trackbar.Value = ++trackbar.Value; }
                    break;
                case "D":
                    if (trackbar.Value != trackbar.Minimum) { trackbar.Value = --trackbar.Value; }
                    break;
            }
            trackbar.Update();
        }

        private void Settings_Shown(object sender, EventArgs e)
        {
            connectBehaveMap();
            form1.shortCutKeyWorker.StopKeyboardWorker();
        }

        //Method to assign key when for function short cut. Waits until WaitForUserKeyPress is set to true, the next key that is pressed
        //is assign to the function stored in actionToAssignKey.
        public void GetKeyPress(object o, HookedKeyboardEventArgs pressedKey)

        {
            KeyConverter converter = new KeyConverter();
            String keyPressed = pressedKey.KeyPressed.ToString();

            if (WaitForUserKeyPress)
            {
                /*
                if (actionToAssignKey == ActionToBePerformed.MicInput || actionToAssignKey == ActionToBePerformed.MicInputOff)
                {
                    keyPressed = converter.Convert(pressedKey.KeyPressed);
                }
                */
                if (checkIfKeyIsAssignedAlready(keyPressed, form1.shortCutKeyWorker.keyAssignments))
                {
                    lbFKeyFeedback.Text = keyPressed + " already assigned.";
                }
                else if (keyPressed == Constants.KEY_NOT_VALID_MESSAGE)
                {
                    lbFKeyFeedback.Text = keyPressed;
                }
                else
                {
                    form1.shortCutKeyWorker.keyAssignments[actionToAssignKey] = keyPressed;
                    updateLabel(keyPressed, actionToAssignKey);
                    WaitForUserKeyPress = false;
                    lbFKeyFeedback.Text = "";
                }
            }
        }


        private bool checkIfKeyIsAssignedAlready(String ValueToCheck, Dictionary<ActionToBePerformed, String> KeyAssignedDict)
        {

            foreach (KeyValuePair<ActionToBePerformed, String> currentKVP in KeyAssignedDict)
            {
                if (currentKVP.Value == ValueToCheck)
                {
                    return true;
                }
            }

            return false;
        }

        void updateLabel(String newKey, ActionToBePerformed functiontoAssign)
        {
            switch (functiontoAssign)
            {
                case ActionToBePerformed.LeftClick:
                    lbLeft.Text = newKey;
                    break;
                case ActionToBePerformed.RightClick:
                    lbRight.Text = newKey;
                    break;
                case ActionToBePerformed.Scroll:
                    lbScroll.Text = newKey;
                    break;
                case ActionToBePerformed.DoubleClick:
                    lbDouble.Text = newKey;
                    break;
                //case ActionToBePerformed.MicInput:
                //    lbMicOn.Text = newKey;
                //    break;
                //case ActionToBePerformed.MicInputOff:
                //    lbMicOff.Text = newKey;
                //    break;
            }
        }

        private void setFkeyPanelWidth(List<Panel> panelList)
        {
            int screenWidth = pnlPageKeyboard.Width;

            int amountOfPanels = panelList.Count;

            int panelWidth = panelList[0].Width;

            int screenSectionSize = screenWidth / amountOfPanels;

            int spacer = screenSectionSize - panelWidth;

            int spacerBuffer = spacer / 2;

            foreach (Panel currentPanel in panelList)
            {
                Point panelLocation = new Point(spacerBuffer, currentPanel.Location.Y);

                currentPanel.Location = panelLocation;

                spacerBuffer += screenSectionSize;
            }
        }

        private void Settings_FormClosed(object sender, FormClosedEventArgs e)
        {
            form1.shortCutKeyWorker.StartKeyBoardWorker();
            WaitForUserKeyPress = false;
        }
        private void btnFixTimeLengthMins_Click(object sender, EventArgs e)
        {
            changeTrackBarValue(trackBarFixTimeLength, "D");
        }

        private void btnFixTimeLengthPlus_Click(object sender, EventArgs e)
        {
            changeTrackBarValue(trackBarFixTimeLength, "I");
        }

        private void btnFixTimeOutMins_Click(object sender, EventArgs e)
        {
            changeTrackBarValue(trackBarFixTimeOut, "D");
        }

        private void btnFixTimeOutPlus_Click(object sender, EventArgs e)
        {
            changeTrackBarValue(trackBarFixTimeOut, "I");
        }
        private void btnZoomSizeMinus_Click(object sender, EventArgs e)
        {
            changeTrackBarValue(trackBarZoomWindowSize, "D");
        }

        private void btnZoomSizePlus_Click(object sender, EventArgs e)
        {
            changeTrackBarValue(trackBarZoomWindowSize, "I");
        }

        private void btnZoomAmountMinus_Click(object sender, EventArgs e)
        {
            changeTrackBarValue(trackBarZoomAmount, "D");
        }

        private void btnZoomAmountPlus_Click(object sender, EventArgs e)
        {
            changeTrackBarValue(trackBarZoomAmount, "I");
        }

        private void trackBarZoomWindowSize_ValueChanged(object sender, EventArgs e)
        {

        }

        
        private void trackBarZoomAmount_ValueChanged(object sender, EventArgs e)
        {
            //form1.stateManager.SetMagnifierMaxZoom(trackBarZoomAmount.Value * Constants.GAPTST + Constants.MINTST);
        }
        

        private void trackBarFixTimeLength_ValueChanged(object sender, EventArgs e)
        {
            //form1.stateManager.fixationWorker.FixationDetectionTimeLength = trackBarFixTimeLength.Value * Constants.GAP_TIME_LENGTH + Constants.MIN_TIME_LENGTH;
            //form1.stateManager.fixationWorker.fixationTimer.Interval = trackBarFixTimeLength.Value * Constants.GAP_TIME_LENGTH + Constants.MIN_TIME_LENGTH;
            form1.trackBarFixTimeLength(trackBarFixTimeLength.Value * Constants.GAP_TIME_LENGTH + Constants.MIN_TIME_LENGTH,
                trackBarFixTimeLength.Value * Constants.GAP_TIME_LENGTH + Constants.MIN_TIME_LENGTH);
        }

        private void trackBarFixTimeOut_ValueChanged(object sender, EventArgs e)
        {
            //form1.stateManager.fixationWorker.FixationTimeOutLength = trackBarFixTimeOut.Value * Constants.GAP_TIME_OUT + Constants.MIN_TIME_OUT;
            //form1.stateManager.fixationWorker.timeOutTimer.Interval = trackBarFixTimeOut.Value * Constants.GAP_TIME_OUT + Constants.MIN_TIME_OUT;
            form1.trackBarFixTimeOut(trackBarFixTimeOut.Value * Constants.GAP_TIME_OUT + Constants.MIN_TIME_OUT,
                trackBarFixTimeOut.Value * Constants.GAP_TIME_OUT + Constants.MIN_TIME_OUT);
        }

        //Methods to rearrange sidebar
        private void InitSidebarActions()
        {

            buttonMap.Add("left_click", btnActionLeftClick);
            buttonMap.Add("right_click", btnActionRightClick);
            buttonMap.Add("keyboard", btnActionKeyboard);
            buttonMap.Add("double_left_click", btnActionDoubleLeftClick);
            buttonMap.Add("scroll", btnActionScrollClick);
            buttonMap.Add("settings", btnActionSettings);
            buttonMap.Add("mic", btnActionMic);

            actionButtons.Add(btnActionDoubleLeftClick);
            actionButtons.Add(btnActionLeftClick);
            actionButtons.Add(btnActionRightClick);
            actionButtons.Add(btnActionScrollClick);
            actionButtons.Add(btnActionKeyboard);
            actionButtons.Add(btnActionSettings);
            actionButtons.Add(btnActionMic);

            actionPanels.Add(pnlDoubleLeftClickButton);
            actionPanels.Add(pnlLeftClickButton);
            actionPanels.Add(pnlRightClickButton);
            actionPanels.Add(pnlScrollClickButton);
            actionPanels.Add(pnlKeyboardButton);
            actionPanels.Add(pnlSettingsButton);
            actionPanels.Add(pnlMicButton);

            foreach (Panel pnl in actionPanels)
            {
                sidebarActionInitPositions.Add(new Point(pnl.Left, pnl.Top));
            }

            foreach (String s in Program.readSettings.sidebar)
            {
                AddAction(s);
            }
        }

        private void resetSideBar()
        {
            selectedActions.Clear();
            foreach (String s in Program.readSettings.sidebar)
            {
                AddAction(s);
            }
        }
        public void AddAction(String actionString)
        {
            selectedActions.Add(actionString);
            RefreshActions();
        }

        public void RemoveAction(String actionString)
        {
            selectedActions.Remove(actionString);
            RefreshActions();
        }

        public void RefreshActions()
        {
            int RIGHT_XPOS = pnlRearrange.Width - 400;
            int LEFT_XPOS = 400;
            int yPos = 0;
            const int YGAP = 10;
            const int XGAP = 10;

            int ind = 0;
            int outOfScreen = 0;
            int notUsed = 0;
            int notUsedY = 0;
            foreach (Button b in actionButtons)
            {
                if (ButtonInSidebar(b))
                {
                    int selIndex = selectedActions.IndexOf(GetStringForButton(b));
                    int y = yPos + ((b.Height + YGAP) * selIndex);

                    if (y + b.Height < pnlRearrange.Height)
                    {
                        actionPanels[ind].Left = RIGHT_XPOS;
                        actionPanels[ind].Top = y;
                    }
                    else
                    {
                        actionPanels[ind].Left = RIGHT_XPOS + b.Width + XGAP;
                        actionPanels[ind].Top = yPos + ((b.Height + YGAP) * outOfScreen);
                        outOfScreen++;
                    }

                }
                else
                {
                    if (notUsed >= 3)
                    {
                        actionPanels[ind].Left = LEFT_XPOS + b.Width + XGAP;
                        actionPanels[ind].Top = yPos + ((b.Height + YGAP) * notUsedY);
                        notUsedY++;
                    }
                    else
                    {
                        actionPanels[ind].Left = LEFT_XPOS;
                        actionPanels[ind].Top = yPos + ((b.Height + YGAP) * notUsed);
                    }
                    notUsed++;
                }
                ind++;
            }
        }

        private Button GetButtonForString(String buttonString)
        {
            return buttonMap[buttonString];
        }

        private String GetStringForButton(Button button)
        {
            String actionString = buttonMap.First(x => x.Value == button).Key;

            return actionString;
        }

        public bool ButtonInSidebar(Button b)
        {
            foreach (String s in selectedActions)
            {
                Button actionButton = GetButtonForString(s);
                if (actionButton == b)
                {
                    return true;
                }
            }
            return false;
        }

        public void ActionButtonClick(Button b)
        {
            String buttonString = GetStringForButton(b);
            if (ButtonInSidebar(b))
            {
                if (!selectionButton.Equals(""))
                {
                    Button selButton = GetButtonForString(selectionButton);
                    selButton.BackColor = Color.Black;
                }

                selectionButton = buttonString;
                b.BackColor = Color.Red;
            }
            else
            {
                AddAction(buttonString);
            }
        }

        private void btnRemove_Click(object sender, EventArgs e)
        {
            if (!selectionButton.Equals("") && !selectionButton.Equals("settings"))
            {
                Button b = GetButtonForString(selectionButton);
                b.BackColor = Color.Black;

                RemoveAction(selectionButton);
                selectionButton = "";

                RefreshActions();
            }
        }

        private void btnMoveUp_Click(object sender, EventArgs e)
        {
            if (!selectionButton.Equals(""))
            {
                Button b = GetButtonForString(selectionButton);

                int currentIndex = selectedActions.IndexOf(selectionButton);
                if (currentIndex > 0)
                {
                    String temp = selectedActions[currentIndex];
                    selectedActions[currentIndex] = selectedActions[currentIndex - 1];
                    selectedActions[currentIndex - 1] = temp;
                    RefreshActions();

                }

                b.BackColor = Color.Black;
                selectionButton = "";
            }
        }

        private void btnMoveDown_Click(object sender, EventArgs e)
        {
            if (!selectionButton.Equals(""))
            {
                Button b = GetButtonForString(selectionButton);

                int currentIndex = selectedActions.IndexOf(selectionButton);
                if (currentIndex < selectedActions.Count - 1)
                {
                    String temp = selectedActions[currentIndex];
                    selectedActions[currentIndex] = selectedActions[currentIndex + 1];
                    selectedActions[currentIndex + 1] = temp;
                    RefreshActions();
                }

                b.BackColor = Color.Black;
                selectionButton = "";
            }
        }

        //Called when an action button is clicked
        private void btnActionButtonClick_Click(object sender, EventArgs e)
        {
            ActionButtonClick((Button)sender);
        }

        private void btnFeedback_Click(object sender, EventArgs e)
        {
            selectionFeedback = !selectionFeedback;

            btnFeedback.BackColor = Color.Black;
            if (selectionFeedback)
                btnFeedback.BackColor = Color.White;
        }


        private void buttonStickyLeftClick_Click(object sender, EventArgs e)
        {
            stickyLeft = !stickyLeft;

            buttonStickyLeftClick.BackColor = Color.Black;
            if (stickyLeft)
                buttonStickyLeftClick.BackColor = Color.White;
        }

        private void btnDefaultConfirmYes_Click(object sender, EventArgs e)
        {
            try
            {
                resetSettings();
            }
            catch(Exception ex)
            {
                MessageBox.Show("Your settings were not reset, please restart the application as a administator.");
            }

            changePanel(pnlGeneral);
            //UseMap(SettingState.General);
            RemoveAndAddMainBhavMap("add");
            pnlDefaultConfirmYes.BackColor = Color.Black;
        }

        private void btnDefaultConfirmNo_Click(object sender, EventArgs e)
        {
            changePanel(pnlGeneral);
            //UseMap(SettingState.General);
            RemoveAndAddMainBhavMap("add");
            pnlDefaultConfirmNo.BackColor = Color.Black;
        }

        private void btnStaticZoomModeClick(object sender, EventArgs e) //DYNMIC ZOON CLICK BIND HERE
        {
            dynamicZoom = false;
            btnStaticZoomMode.BackColor = Color.White;
            btnStaticZoomMode.ForeColor = Color.Black;
            btnDynamicZoomMode.BackColor = Color.Black;
            btnDynamicZoomMode.ForeColor = Color.White;
        }

        private void pnlRightClick_Paint(object sender, PaintEventArgs e)
        {

        }

        private void pnlZoomSettings_Paint(object sender, PaintEventArgs e)
        {

        }

        //private void btnDynamicZoomMode_Click(object sender, EventArgs e)
        //{
        //    dynamicZoom = true;
        //    btnDynamicZoomMode.BackColor = Color.White;
        //    btnDynamicZoomMode.ForeColor = Color.Black;
        //    btnStaticZoomMode.BackColor = Color.Black;
        //    btnStaticZoomMode.ForeColor = Color.White;
        //}

        private void btnDefaults_Click(object sender, EventArgs e)
        {
            RemoveAndAddMainBhavMap("remove");
            //UseMap(SettingState.Confirm);
            pnlDefaultConfirm.Show();
            pnlDefaultConfirm.BringToFront();
            shownPanel = pnlDefaultConfirm;
            pnlDefaults.BackColor = Color.Black;
        }
    }
}
