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
    public partial class SettingsForm : Form
    {
        private Form1 form1;
        private SettingsHome home;
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
        private bool stickyLeft, selectionFeedback, dynamicZoom, corners;

        private List<Panel> fKeyPannels;

        public SettingsForm(SettingsHome home, Form1 form1, FormsEyeXHost EyeXHost)
        {
            eyeXHost = EyeXHost;
            InitializeComponent();
            pnlPageKeyboard.Hide();
            this.form1 = form1;
            this.home = home;
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

            //Store reference to short cut assignment panels in a list so they can be iterated over and set their on screen positions relative form size.
            fKeyPannels = new List<Panel>() { pnlLeftClick, pnlRightClick, pnlDoubleClick, pnlScroll /*pnlMic*/ };// pnlDragAndDrop };
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

            //stickyLeft = Program.readSettings.stickyLeftClick;
            //if (stickyLeft)
            //    buttonStickyLeftClick.BackColor = Color.White;

            //selectionFeedback = Program.readSettings.selectionFeedback;
            //if (selectionFeedback)
            //    btnFeedback.BackColor = Color.White;

            dynamicZoom = Program.readSettings.dynamicZoom;
            corners = Program.readSettings.centerZoom;
            

            form1.LowLevelKeyBoardHook.OnKeyPressed += GetKeyPress;

        }

        

        ///
       
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


        private void btnCancel_Click(object sender, EventArgs e)
        {
            Close();
        }



        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {

                //string settings = JsonConvert.SerializeObject(setting);
                //File.WriteAllText(Program.path, settings);

                //Program.readSettings = setting;
                //form1.stateManager.RefreshZoom();
                Program.readSettings.leftClick = lbLeft.Text;
                Program.readSettings.doubleClick = lbDouble.Text;
                Program.readSettings.rightClick = lbRight.Text;
                Program.readSettings.scroll = lbScroll.Text;

                Program.readSettings.createJSON(Program.readSettings.sidebar);
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


        void NotifyIcon_BalloonTipClicked(object sender, EventArgs e)
        {
            MessageBox.Show((String)((NotifyIcon)sender).Tag, "Exception", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private void Settings_Load(object sender, EventArgs e)
        {
            //Program.ReadWriteJson();

            

            //TODO: Need to be replaced
            lbLeft.Text = Program.readSettings.leftClick;
            lbDouble.Text = Program.readSettings.doubleClick;
            lbRight.Text = Program.readSettings.rightClick;
            lbScroll.Text = Program.readSettings.scroll;

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

        

        




        











        private void controlRelocateAndResize()
        {
            int percentageSize = 400; //Higher number for smaller trackbars
            panelSaveAndCancel.Location = ReletiveSize.panelSaveAndCancel(panelSaveAndCancel.Width, panelSaveAndCancel.Height);
            ReletiveSize.sizeEvenly(panelSaveAndCancel, 0.4);
            pnlSave.Location = ReletiveSize.distribute(panelSaveAndCancel, pnlSave.Location.Y, 1, 2, "wn", 0.5);
            pnlCancel.Location = ReletiveSize.distribute(panelSaveAndCancel, pnlSave.Location.Y, 2, 2, "wn", 0.7);
            ReletiveSize.resizeLabel(label1, 20);
            lbFKeyFeedback.Text = "";
            pnlPageKeyboard.Size = ReletiveSize.panelGeneralSize(panelSaveAndCancel.Location.Y, 200);
            pnlPageKeyboard.Location = new Point(0, 200);
            pnlPageKeyboard.Visible = true;
            lbFKeyFeedback.Location = new Point((pnlPageKeyboard.Width / 2) - (lbFKeyFeedback.Width / 2), lbFKeyFeedback.Location.Y);

            //Set feed back label to the center of the screen.
            lbFKeyFeedback.Location = new Point((pnlPageKeyboard.Width / 2) - (lbFKeyFeedback.Width / 2), lbFKeyFeedback.Location.Y);
            //pnlPageKeyboard.Location = ReletiveSize.mainPanelLocation(pnlSwitchSetting.Location.Y, pnlSwitchSetting.Height);
            label1.ForeColor = Program.readSettings.secondColour;
            //Zoom Settings size and location
            //Main Panel
            lbDouble.Text = form1.FKeyMapDictionary[ActionToBePerformed.DoubleClick];
            lbRight.Text = form1.FKeyMapDictionary[ActionToBePerformed.RightClick];
            lbLeft.Text = form1.FKeyMapDictionary[ActionToBePerformed.LeftClick];
            lbScroll.Text = form1.FKeyMapDictionary[ActionToBePerformed.Scroll];
        }









    }
}
