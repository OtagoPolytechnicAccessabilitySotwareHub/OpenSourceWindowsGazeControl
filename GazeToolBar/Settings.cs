﻿using System;
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
    public partial class Settings : Form
    {
        private Form1 form1;
        private bool[] onOff;
        private bool pnlKeyboardIsShow;
        private bool pnlZoomSettingsIsShow;
        private bool pnlGeneralIsShow;
        private bool pnlRearrangeSettingsIsShown;
        private bool WaitForUserKeyPress;
        private static FormsEyeXHost eyeXHost;
        private List<Point> sidebarActionInitPositions = new List<Point>();
        private List<String> selectedActions = new List<String>();
        private List<Button> actionButtons = new List<Button>();
        private String selectionButton = "";
        private Dictionary<String, Button> buttonMap = new Dictionary<string, Button>();

        public Form1 sideForm;


        private List<Panel> fKeyPannels;

        public Settings(Form1 form1, FormsEyeXHost EyeXHost)
        {
            eyeXHost = EyeXHost;
            InitializeComponent();
            InitSidebarActions();
            pnlPageKeyboard.Hide();
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
            pnlGeneralIsShow = true;
            pnlKeyboardIsShow = false;
            pnlZoomSettingsIsShow = false;
            pnlRearrangeSettingsIsShown = false;

            //Set Short cut key assignment panel to the viable width of the form
            pnlPageKeyboard.Width = Constants.SCREEN_SIZE.Width - 20;

            //Set feed back label to the center of the screen.
            lbFKeyFeedback.Location = new Point((pnlPageKeyboard.Width / 2) - (lbFKeyFeedback.Width / 2), lbFKeyFeedback.Location.Y);
            //Store reference to short cut assignment panels in a list so they can be iterated over and set their on screen positions relative form size.
            fKeyPannels = new List<Panel>() { pnlLeftClick, pnlRightClick, pnlDoubleClick, pnlScroll };// pnlDragAndDrop };
            //Set panel positions.
            setFkeyPanelWidth(fKeyPannels);

            //set initial values of mapped keys to on screen label.
            lbDouble.Text = form1.FKeyMapDictionary[ActionToBePerformed.DoubleClick];
            lbRight.Text = form1.FKeyMapDictionary[ActionToBePerformed.RightClick];
            lbLeft.Text = form1.FKeyMapDictionary[ActionToBePerformed.LeftClick];
            lbScroll.Text = form1.FKeyMapDictionary[ActionToBePerformed.Scroll];

            WaitForUserKeyPress = false;

            form1.LowLevelKeyBoardHook.OnKeyPressed += GetKeyPress;


           

        }

        private void controlRelocateAndResize()
        {
            pnlSwitchSetting.Location = ReletiveSize.panelSwitchSettingLocation(pnlSwitchSetting.Width, pnlSwitchSetting.Height);
            panelSaveAndCancel.Location = ReletiveSize.panelSaveAndCancel(panelSaveAndCancel.Width, panelSaveAndCancel.Height);
            pnlGeneral.Size = ReletiveSize.panelGeneralSize();

            pnlGeneral.Location = ReletiveSize.mainPanelLocation(pnlSwitchSetting.Location.Y, pnlSwitchSetting.Height);
            pnlPageKeyboard.Location = ReletiveSize.mainPanelLocation(pnlSwitchSetting.Location.Y, pnlSwitchSetting.Height);

            panelPrecision.Location = ReletiveSize.distribute(pnlGeneral, panelPrecision.Location.X, 1, 3, "h", 0);
            pnlFixationTimeOut.Location = ReletiveSize.distribute(pnlGeneral, pnlFixationTimeOut.Location.X, 2, 3, "h", 0);
            panelOther.Location = ReletiveSize.distribute(pnlGeneral, panelOther.Location.X, 3, 3, "h", 0);

            panelPrecision.Size = new Size(pnlGeneral.Size.Width, panelPrecision.Size.Height);
            pnlFixationTimeOut.Size = new Size(pnlGeneral.Size.Width, pnlFixationTimeOut.Size.Height);
            panelOther.Size = new Size(pnlGeneral.Size.Width, panelOther.Size.Height);

            lblFixationDetectionTimeLength.Location = ReletiveSize.labelPosition(panelPrecision, lblFixationDetectionTimeLength);
            lblSpeed.Location = ReletiveSize.labelPosition(pnlFixationTimeOut, lblSpeed);
            lblOther.Location = ReletiveSize.labelPosition(panelOther, lblOther);

            pnlOtherAuto.Location = new Point(panelOther.Size.Width / 2, pnlOtherAuto.Location.Y);

            //double p = ((double)pnlSelectionGaze.Location.X + (double)btnGaze.Location.X) / (double)pnlSelectionGaze.Parent.Size.Width;
            pnlFixTimeLengthContent.Location = ReletiveSize.distribute(panelPrecision, pnlFixTimeLengthContent.Location.Y, 1, 1, "w", 0.15);
            pnlFixTimeOutContent.Location = new Point(pnlFixTimeLengthContent.Location.X, pnlFixTimeOutContent.Location.Y);

            pnlFixTimeLengthContent.Size = ReletiveSize.controlLength(panelPrecision, pnlFixTimeLengthContent.Size.Height, 0.8);
            pnlFixTimeOutContent.Size = pnlFixTimeLengthContent.Size;

            double percentage = (double)(pnlFixTimeLengthContent.Size.Width - 110) / (double)pnlFixTimeLengthContent.Size.Width;
            trackBarFixTimeLength.Size = ReletiveSize.controlLength(pnlFixTimeLengthContent, trackBarFixTimeLength.Size.Height, percentage);
            trackBarFixTimeOut.Size = trackBarFixTimeLength.Size;

            pnlFTLPlus.Location = ReletiveSize.reletiveLocation(trackBarFixTimeLength, pnlFTLPlus.Location.Y, 7, 'v');
            pnlFTOPlus.Location = new Point(pnlFTLPlus.Location.X, pnlFTOPlus.Location.Y);
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

        public void ChangeButtonColor(Button button, bool onOff, bool hasText)
        {

            button.BackColor = onOff ? Constants.SelectedColor: Constants.SettingButtonColor;
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

                //TODO: Need to be replaced

                //setting.position = lblIndicationLeftOrRight.Text.Substring(3);
                //setting.precision = trackBarFixTimeLength.Value;
                //setting.selection = gazeOrSwitch.ToString();
                //setting.size = sizes.ToString();
                //setting.soundFeedback = onOff[3];
                //setting.speed = trackBarFixTimeOut.Value;
                //setting.wordPrediction = onOff[2];

                setting.fixationTimeLength = trackBarFixTimeLength.Value * Constants.GAP_TIME_LENGTH + Constants.MIN_TIME_LENGTH;
                setting.fixationTimeOut = trackBarFixTimeOut.Value * Constants.GAP_TIME_OUT + Constants.MIN_TIME_OUT;
                setting.leftClick = lbLeft.Text;
                setting.doubleClick = lbDouble.Text;
                setting.rightClick = lbRight.Text;
                setting.scoll = lbScroll.Text;
                setting.sidebar = Program.readSettings.sidebar;
                Program.readSettings.sidebar = selectedActions.ToArray<string>();

                string sidebarSettings = JsonConvert.SerializeObject(Program.readSettings);
                File.WriteAllText(Program.path, sidebarSettings);

                sideForm.ArrangeSidebar(Program.readSettings.sidebar);

                string settings = JsonConvert.SerializeObject(setting);
                File.WriteAllText(Program.path, settings);
                //MessageBox.Show("Save Success", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                form1.NotifyIcon.BalloonTipTitle = "Saving success";
                form1.NotifyIcon.BalloonTipText = "Your settings are successfuly saved";
                this.Close();
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
                //MessageBox.Show(exception.Message, "Exception", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        void NotifyIcon_BalloonTipClicked(object sender, EventArgs e)
        {
            MessageBox.Show((String)((NotifyIcon)sender).Tag, "Exception", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private void Settings_Load(object sender, EventArgs e)
        {
            Program.ReadWriteJson();
            //TODO: Need to be replaced
            //trackBarFixTimeLength.Value = Program.readSettings.precision;
            //trackBarFixTimeOut.Value = Program.readSettings.speed;
            //lblIndicationLeftOrRight.Text = lblIndicationLeftOrRight.Text.Remove(3) + Program.readSettings.position;
            
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
            lbLeft.Text = Program.readSettings.leftClick;
            lbDouble.Text = Program.readSettings.doubleClick;
            lbRight.Text = Program.readSettings.rightClick;
            lbScroll.Text = Program.readSettings.scoll;

            //if (Program.readSettings.selection == GazeOrSwitch.GAZE.ToString())
            //{
            //    gazeOrSwitch = GazeOrSwitch.GAZE;
            //    //changeSitchGaze(gazeOrSwitch);
            //}
            //else
            //{
            //    gazeOrSwitch = GazeOrSwitch.SWITCH;
            //    //changeSitchGaze(gazeOrSwitch);
            //}

            //if (Program.readSettings.position == "left")
            //{
            //    OnTheRight = false;
            //    //ChangeButtonColor(btnChangeSide, true, false);
            //}
            //else
            //{
            //    OnTheRight = true;
            //}
        }

        private void btnGeneralSetting_Click(object sender, EventArgs e)
        {
            if (!pnlGeneralIsShow)
            {
                pnlPageKeyboard.Hide();
                pnlZoomSettings.Hide();
                pnlRearrangeSettings.Hide();
                ChangeButtonColor(btnShortCutKeySetting, false, true);
                ChangeButtonColor(btnZoomSettings, false, true);
                ChangeButtonColor(btnRearrangeSetting, false, true);
                pnlGeneral.Show();
                ChangeButtonColor(btnGeneralSetting, true, true);
                pnlGeneralIsShow = true;
                pnlKeyboardIsShow = false;
                pnlZoomSettingsIsShow = false;
                pnlRearrangeSettingsIsShown = false;

                WaitForUserKeyPress = false;
            }
        }

        private void btnKeyBoardSetting_Click(object sender, EventArgs e)
        {
            if (!pnlKeyboardIsShow)
            {
                pnlGeneral.Hide();
                pnlZoomSettings.Hide();
                pnlRearrangeSettings.Hide();
                ChangeButtonColor(btnGeneralSetting, false, true);
                ChangeButtonColor(btnZoomSettings, false, true);
                ChangeButtonColor(btnRearrangeSetting, false, true);
                pnlPageKeyboard.Show();
                ChangeButtonColor(btnShortCutKeySetting, true, true);
                pnlKeyboardIsShow = true;
                pnlGeneralIsShow = false;
                pnlZoomSettingsIsShow = false;
                pnlRearrangeSettingsIsShown = false;

                lbFKeyFeedback.Text = "";
            }
        }

        private void btnZoomSettings_Click(object sender, EventArgs e)
        {
            if (!pnlZoomSettingsIsShow)
            {
                pnlGeneral.Hide();
                pnlPageKeyboard.Hide();
                pnlRearrangeSettings.Hide();
                ChangeButtonColor(btnGeneralSetting, false, true);
                ChangeButtonColor(btnShortCutKeySetting, false, true);
                ChangeButtonColor(btnRearrangeSetting, false, true);
                pnlZoomSettings.Show();
                ChangeButtonColor(btnZoomSettings, true, true);
                pnlZoomSettingsIsShow = true;
                pnlKeyboardIsShow = false;
                pnlGeneralIsShow = false;
                pnlRearrangeSettingsIsShown = false;
            }
        }

        private void btnRearrangeSetting_Click(object sender, EventArgs e)
        {
            if (!pnlRearrangeSettingsIsShown)
            {
                pnlGeneral.Hide();
                pnlPageKeyboard.Hide();
                pnlZoomSettings.Hide();
                ChangeButtonColor(btnGeneralSetting, false, true);
                ChangeButtonColor(btnShortCutKeySetting, false, true);
                ChangeButtonColor(btnZoomSettings, false, true);
                pnlRearrangeSettings.Show();
                ChangeButtonColor(btnRearrangeSetting, true, true);
                pnlRearrangeSettingsIsShown = true;
                pnlZoomSettingsIsShow = false;
                pnlKeyboardIsShow = false;
                pnlGeneralIsShow = false;
            }
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

            String keyPressed = pressedKey.KeyPressed.ToString();

             if(WaitForUserKeyPress)
            {

                if (checkIfKeyIsAssignedAlready(keyPressed, form1.shortCutKeyWorker.keyAssignments))
                {
                    lbFKeyFeedback.Text = keyPressed + " already assigned.";
                }
                else
                {
                    form1.shortCutKeyWorker.keyAssignments[actionToAssignKey] = keyPressed;
                    updateLabel(pressedKey.KeyPressed.ToString(), actionToAssignKey);
                    WaitForUserKeyPress = false;
                    lbFKeyFeedback.Text = "";
                }
            }
        }


        private bool checkIfKeyIsAssignedAlready(String ValueToCheck, Dictionary<ActionToBePerformed, String> KeyAssignedDict)
        {
            
            foreach (KeyValuePair<ActionToBePerformed, String> currentKVP in KeyAssignedDict)
            { 
                if(currentKVP.Value == ValueToCheck)
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

        private void trackBarFixTimeLength_ValueChanged(object sender, EventArgs e)
        {
            form1.stateManager.fixationWorker.FixationDetectionTimeLength = trackBarFixTimeLength.Value * Constants.GAP_TIME_LENGTH + Constants.MIN_TIME_LENGTH;
            form1.stateManager.fixationWorker.fixationTimer.Interval = trackBarFixTimeLength.Value * Constants.GAP_TIME_LENGTH + Constants.MIN_TIME_LENGTH;
        }

        private void trackBarFixTimeOut_ValueChanged(object sender, EventArgs e)
        {
            form1.stateManager.fixationWorker.FixationTimeOutLength = trackBarFixTimeOut.Value * Constants.GAP_TIME_OUT + Constants.MIN_TIME_OUT;
            form1.stateManager.fixationWorker.timeOutTimer.Interval = trackBarFixTimeOut.Value * Constants.GAP_TIME_OUT + Constants.MIN_TIME_OUT;
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

            actionButtons.Add(btnActionDoubleLeftClick);
            actionButtons.Add(btnActionLeftClick);
            actionButtons.Add(btnActionRightClick);
            actionButtons.Add(btnActionScrollClick);
            actionButtons.Add(btnActionKeyboard);
            actionButtons.Add(btnActionSettings);

            foreach (Button b in actionButtons)
            {
                sidebarActionInitPositions.Add(new Point(b.Left, b.Top));
            }

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
            const int XPOS = 850;
            int yPos = 80;
            const int YGAP = 10;

            int ind = 0;
            foreach (Button b in actionButtons)
            {
                if (ButtonInSidebar(b))
                {
                    int selIndex = selectedActions.IndexOf(GetStringForButton(b));
                    int y = yPos + ((b.Height + YGAP) * selIndex);

                    b.Left = XPOS;
                    b.Top = y;
                }
                else
                {
                    b.Left = sidebarActionInitPositions[ind].X;
                    b.Top = sidebarActionInitPositions[ind].Y;
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
    }
}
