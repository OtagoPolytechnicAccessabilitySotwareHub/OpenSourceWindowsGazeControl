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
    public partial class RearrangeSettingPage : Form
    {

        private List<Point> sidebarActionInitPositions = new List<Point>();
        private List<String> selectedActions = new List<String>();
        private List<Button> actionButtons = new List<Button>();
        private List<Panel> actionPanels = new List<Panel>();
        private String selectionButton = "";
        private HomeSettings home;
        private static FormsEyeXHost eyeXHost;
        private Form1 form1;
        private Dictionary<String, Button> buttonMap = new Dictionary<string, Button>();
        public RearrangeSettingPage(HomeSettings home, Form1 form1, FormsEyeXHost EyeXHost)
        {

            eyeXHost = EyeXHost;
            InitializeComponent();
            this.home = home;
            this.form1 = form1;
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

        private void btnCancel_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                //SettingJSON setting = new SettingJSON();

                
                Program.readSettings.sidebar = selectedActions.ToArray<string>();
                Program.readSettings.createJSON(selectedActions.ToArray<string>());
                form1.ArrangeSidebar(Program.readSettings.sidebar);
                this.Close();
            }
            catch (Exception exception)
            {
                form1.NotifyIcon.BalloonTipTitle = "Saving error";

            }
        }
    }
}
