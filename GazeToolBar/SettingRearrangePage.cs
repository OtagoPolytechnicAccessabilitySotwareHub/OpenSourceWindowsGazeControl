using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using EyeXFramework.Forms;

namespace GazeToolBar
{
    public partial class SettingRearrangePage : Form
    {

        private List<Point> sidebarActionInitPositions = new List<Point>();
        private List<String> selectedActions = new List<String>();
        private List<Button> actionButtons = new List<Button>();
        private List<Panel> actionPanels = new List<Panel>();
        private String selectionButton = "";
        private SettingsHome home;
        private static FormsEyeXHost eyeXHost;
        private Form1 form1;
        private Dictionary<String, Button> buttonMap = new Dictionary<string, Button>();
        public SettingRearrangePage(SettingsHome home, Form1 form1, FormsEyeXHost EyeXHost)
        {

            eyeXHost = EyeXHost;
            InitializeComponent();
            connectBehaveMap();
            this.home = home;
            this.form1 = form1;
            controlRelocateAndResize();
            InitSidebarActions();
            RefreshActions();
            
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
            //buttonMap.Add("mic", btnActionMic);

            actionButtons.Add(btnActionDoubleLeftClick);
            actionButtons.Add(btnActionLeftClick);
            actionButtons.Add(btnActionRightClick);
            actionButtons.Add(btnActionScrollClick);
            actionButtons.Add(btnActionKeyboard);
            actionButtons.Add(btnActionSettings);
            //actionButtons.Add(btnActionMic);

            actionPanels.Add(pnlDoubleLeftClickButton);
            actionPanels.Add(pnlLeftClickButton);
            actionPanels.Add(pnlRightClickButton);
            actionPanels.Add(pnlScrollClickButton);
            actionPanels.Add(pnlKeyboardButton);
            actionPanels.Add(pnlSettingsButton);
            //actionPanels.Add(pnlMicButton);

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
                    selButton.BackColor = Program.readSettings.mainColour;
                }

                selectionButton = buttonString;
                b.BackColor = Program.readSettings.secondColour;
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
                b.BackColor = Program.readSettings.mainColour;

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

                b.BackColor = Program.readSettings.mainColour;
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

                b.BackColor = Program.readSettings.mainColour;
                selectionButton = "";
            }
        }

        //Called when an action button is clicked
        private void btnActionButtonClick_Click(object sender, EventArgs e)
        {
            ActionButtonClick((Button)sender);
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                Program.readSettings.sidebar = selectedActions.ToArray<string>();
                Program.readSettings.createJSON(selectedActions.ToArray<string>());
                form1.ArrangeSidebar(Program.readSettings.sidebar);
                
                //form1.NotifyIcon.BalloonTipTitle = "Saving success";
                //form1.NotifyIcon.BalloonTipText = "Your settings are successfuly saved";
                this.Close();
                form1.stateManager.ResetMagnifier();
                //form1.NotifyIcon.ShowBalloonTip(2000);
            }
            catch (Exception exception)
            {
                //form1.NotifyIcon.BalloonTipTitle = "Saving error";
                //form1.NotifyIcon.BalloonTipText = "For some reason, your settings are not successfuly saved, click me to show error message";
                //form1.NotifyIcon.Tag = exception.Message;
                this.Close();
                //form1.NotifyIcon.BalloonTipClicked += NotifyIcon_BalloonTipClicked;
                //form1.NotifyIcon.ShowBalloonTip(5000);
            }
            
        }







        private void controlRelocateAndResize()
        {
            int percentageSize = 400; //Higher number for smaller trackbars
            panelSaveAndCancel.Location = ReletiveSize.panelSaveAndCancel(panelSaveAndCancel.Width, panelSaveAndCancel.Height);
            ReletiveSize.sizeEvenly(panelSaveAndCancel, 0.4);
            pnlSave.Location = ReletiveSize.distribute(panelSaveAndCancel, pnlSave.Location.Y, 1, 2, "wn", 0.5);
            pnlCancel.Location = ReletiveSize.distribute(panelSaveAndCancel, pnlSave.Location.Y, 2, 2, "wn", 0.7);
            ReletiveSize.resizeLabel(label1, 20);
            //Set feed back label to the center of the screen.
            //lbFKeyFeedback.Location = new Point((pnlPageKeyboard.Width / 2) - (lbFKeyFeedback.Width / 2), lbFKeyFeedback.Location.Y);
            //pnlPageKeyboard.Location = ReletiveSize.mainPanelLocation(pnlSwitchSetting.Location.Y, pnlSwitchSetting.Height);

            //Zoom Settings size and location
            //Main Panel
            //Rearrange panel
            pnlRearrange.Size = ReletiveSize.panelRearrangeSize(panelSaveAndCancel.Location.Y, (label1.Location.Y+label1.Height));
            pnlRearrangeControls.Location = ReletiveSize.centerLocation(pnlRearrange, pnlRearrangeControls);
            pnlRearrange.Top = (label1.Location.Y + label1.Height);
            this.BackColor = Program.readSettings.mainColour;
            label1.ForeColor = Program.readSettings.iconColour;
            foreach (Panel panel in panelSaveAndCancel.Controls)
            {
                foreach (Button button in panel.Controls)
                {
                    button.ForeColor = Program.readSettings.iconColour;
                }
            }

            switch (Program.readSettings.iconNumber)
            {
                case 0:
                    btnActionRightClick.Image = GazeToolBar.Properties.Resources.Right_Click_iconbl;
                    btnActionLeftClick.Image = GazeToolBar.Properties.Resources.Left_Click_iconbl;
                    btnActionDoubleLeftClick.Image = GazeToolBar.Properties.Resources.Double_Click_iconbl;
                    btnActionScrollClick.Image = GazeToolBar.Properties.Resources.Scroll_iconbl;
                    btnActionKeyboard.Image = GazeToolBar.Properties.Resources.Keyboard_iconbl;
                    btnActionSettings.Image = GazeToolBar.Properties.Resources.settings_iconbl;
                    btnMoveUp.Image = GazeToolBar.Properties.Resources.button_upbl;
                    btnMoveDown.Image = GazeToolBar.Properties.Resources.button_downbl;
                    btnRemove.Image = GazeToolBar.Properties.Resources.button_leftbl;
                    break;
                case 1:
                    btnActionRightClick.Image = GazeToolBar.Properties.Resources.Right_Click_iconwt;
                    btnActionLeftClick.Image = GazeToolBar.Properties.Resources.Left_Click_iconwt;
                    btnActionDoubleLeftClick.Image = GazeToolBar.Properties.Resources.Double_Click_iconwt;
                    btnActionScrollClick.Image = GazeToolBar.Properties.Resources.Scroll_iconwt;
                    btnActionKeyboard.Image = GazeToolBar.Properties.Resources.Keyboard_iconwt;
                    btnActionSettings.Image = GazeToolBar.Properties.Resources.settings_iconwt;
                    btnMoveUp.Image = GazeToolBar.Properties.Resources.button_upwt;
                    btnMoveDown.Image = GazeToolBar.Properties.Resources.button_downwt;
                    btnRemove.Image = GazeToolBar.Properties.Resources.button_leftwt;
                    break;
                case 2:
                    btnActionRightClick.Image = GazeToolBar.Properties.Resources.Right_Click_icon;
                    btnActionLeftClick.Image = GazeToolBar.Properties.Resources.Left_Click_icon;
                    btnActionDoubleLeftClick.Image = GazeToolBar.Properties.Resources.Double_Click_icon;
                    btnActionScrollClick.Image = GazeToolBar.Properties.Resources.Scroll_icon;
                    btnActionKeyboard.Image = GazeToolBar.Properties.Resources.Keyboard_icon;
                    btnActionSettings.Image = GazeToolBar.Properties.Resources.settings_icon;
                    btnMoveUp.Image = GazeToolBar.Properties.Resources.button_up;
                    btnMoveDown.Image = GazeToolBar.Properties.Resources.button_down;
                    btnRemove.Image = GazeToolBar.Properties.Resources.button_left;
                    break;
                case 3:
                    btnActionRightClick.Image = GazeToolBar.Properties.Resources.Right_Click_iconrd;
                    btnActionLeftClick.Image = GazeToolBar.Properties.Resources.Left_Click_iconrd;
                    btnActionDoubleLeftClick.Image = GazeToolBar.Properties.Resources.Double_Click_iconrd;
                    btnActionScrollClick.Image = GazeToolBar.Properties.Resources.Scroll_iconrd;
                    btnActionKeyboard.Image = GazeToolBar.Properties.Resources.Keyboard_iconrd;
                    btnActionSettings.Image = GazeToolBar.Properties.Resources.settings_iconrd;
                    btnMoveUp.Image = GazeToolBar.Properties.Resources.button_uprd;
                    btnMoveDown.Image = GazeToolBar.Properties.Resources.button_downrd;
                    btnRemove.Image = GazeToolBar.Properties.Resources.button_leftrd;
                    break;
                case 4:
                    btnActionRightClick.Image = GazeToolBar.Properties.Resources.Right_Click_iconyl;
                    btnActionLeftClick.Image = GazeToolBar.Properties.Resources.Left_Click_iconyl;
                    btnActionDoubleLeftClick.Image = GazeToolBar.Properties.Resources.Double_Click_iconyl;
                    btnActionScrollClick.Image = GazeToolBar.Properties.Resources.Scroll_iconyl;
                    btnActionKeyboard.Image = GazeToolBar.Properties.Resources.Keyboard_iconyl;
                    btnActionSettings.Image = GazeToolBar.Properties.Resources.settings_iconyl;
                    btnMoveUp.Image = GazeToolBar.Properties.Resources.button_upyl;
                    btnMoveDown.Image = GazeToolBar.Properties.Resources.button_downyl;
                    btnRemove.Image = GazeToolBar.Properties.Resources.button_leftyl;
                    break;
                case 5:
                    btnActionRightClick.Image = GazeToolBar.Properties.Resources.Right_Click_icongr;
                    btnActionLeftClick.Image = GazeToolBar.Properties.Resources.Left_Click_icongr;
                    btnActionDoubleLeftClick.Image = GazeToolBar.Properties.Resources.Double_Click_icongr;
                    btnActionScrollClick.Image = GazeToolBar.Properties.Resources.Scroll_icongr;
                    btnActionKeyboard.Image = GazeToolBar.Properties.Resources.Keyboard_icongr;
                    btnActionSettings.Image = GazeToolBar.Properties.Resources.settings_iconbl;
                    btnMoveUp.Image = GazeToolBar.Properties.Resources.button_upgr;
                    btnMoveDown.Image = GazeToolBar.Properties.Resources.button_downgr;
                    btnRemove.Image = GazeToolBar.Properties.Resources.button_leftgr;
                    break;
            }



        }







    }
}
