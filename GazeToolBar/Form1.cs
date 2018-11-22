using System;
using System.Drawing;
using System.Windows.Forms;
using EyeXFramework;
using Tobii;
using System.IO;
using System.Diagnostics;
using System.Collections.Generic;
using EyeXFramework.Forms;
using GazeToolBar;

namespace GazeToolBar
{
    /*
        Date: 17/05/2016
        Name: Derek Dai
        Description: Main toolbar form
    */
    public partial class Form1 : ShellLib.ApplicationDesktopToolbar
    {
        private Settings settings;
        private ContextMenu contextMenu;
        private MenuItem menuItemExit;
        private MenuItem menuItemStartOnOff;
        private MenuItem settingsItem;
        public StateManager stateManager;
        private static FormsEyeXHost eyeXHost;
        private Form2 form2;

        //Allocate memory location for KeyboardHook and worker.
        public KeyboardHook LowLevelKeyBoardHook;
        public ShortcutKeyWorker shortCutKeyWorker;



        public Dictionary<ActionToBePerformed, String> FKeyMapDictionary;

        List<Panel> highlightPannerList;

        public Form1()
        {
            
            InitializeComponent();
            contextMenu = new ContextMenu();
            menuItemExit = new MenuItem();
            menuItemStartOnOff = new MenuItem();
            settingsItem = new MenuItem();
            initMenuItem();
            //form2 = new Form2(eyeXHost);
            
            highlightPannerList = new List<Panel>();
            highlightPannerList.Add(pnlHiLteRightClick);
            highlightPannerList.Add(pnlHighLightSingleLeft);
            highlightPannerList.Add(pnlHighLightDoubleClick);
            highlightPannerList.Add(pnlHighLightScrol);
            highlightPannerList.Add(pnlHighLightKeyboard);
            highlightPannerList.Add(pnlHighLightSettings);
            highlightPannerList.Add(pnlHighLightMic);
            setButtonPanelHight(highlightPannerList);


            eyeXHost = new FormsEyeXHost();
            eyeXHost.Start();


            connectBehaveMap();

            String[] sidebarArrangement = Program.readSettings.sidebar;
            ArrangeSidebar(sidebarArrangement);
        }

        public void ArrangeSidebar(string[] sidebarArrangement)
        {
            foreach (Panel p in highlightPannerList)
            {
                p.Left = -100;
            }

            const int BUTTON_HEIGHT = 75;
            int gapSize = ((int)(Height / 1.5) / sidebarArrangement.Length) - (BUTTON_HEIGHT / 2);
            //   MessageBox.Show(gapSize + " " + sidebarArrangement.Length + " " + 740 + " " + BUTTON_HEIGHT);
            int yPos = gapSize;
            foreach (String s in sidebarArrangement)
            {
                Panel highlight = GetHighlightPanelForString(s);

                if (highlight != null)
                {
                    highlight.Top = yPos;
                    highlight.Left = 15;

                    yPos += BUTTON_HEIGHT + gapSize;
                }
            }
        }

        private Panel GetHighlightPanelForString(String buttonString)
        {
            switch (buttonString)
            {
                case "right_click":
                    return pnlHiLteRightClick;
                case "left_click":
                    return pnlHighLightSingleLeft;
                case "double_left_click":
                    return pnlHighLightDoubleClick;
                case "scroll":
                    return pnlHighLightScrol;
                case "keyboard":
                    return pnlHighLightKeyboard;
                case "settings":
                    return pnlHighLightSettings;
                case "mic":
                    return pnlHighLightMic;
                default:
                    return null;
            }
        }



        /// <summary>
        /// Setup the context menu for
        /// notify icon
        /// </summary>
        private void initMenuItem()
        {
            menuItemExit.Text = "Exit";
            menuItemStartOnOff.Text = Constants.AUTO_START_OFF;
            menuItemStartOnOff.Click += new EventHandler(menuItemAutostart_click);
            menuItemExit.Click += new EventHandler(menuItemExit_Click);
            settingsItem.Text = "Settings";
            contextMenu.MenuItems.Add(settingsItem);
            contextMenu.MenuItems.Add(menuItemStartOnOff);
            contextMenu.MenuItems.Add(menuItemExit);
            notifyIcon.ContextMenu = contextMenu;
            notifyIcon.Text = "Gaze Toolbar";
            notifyIcon.Visible = true;
            OnStartTextChange();
        }

        public void menuItemAutostart_click(object o, EventArgs e)
        {
            if(AutoStart.IsOn())
            {
                AutoStart.SetOff();
            }
            else
            {
                AutoStart.SetOn();
            }
            OnStartTextChange();
        }

        private void menuItemExit_Click(object sender, EventArgs e)
        {
            Close();
        }

        public MenuItem MenuItemStartOnOff { get { return menuItemStartOnOff; } }

        private void Form1_Load(object sender, EventArgs e)
        {


            FKeyMapDictionary = new Dictionary<ActionToBePerformed, string>();
            FKeyMapDictionary.Add(ActionToBePerformed.DoubleClick, Constants.KEY_FUNCTION_UNASSIGNED_MESSAGE);
            FKeyMapDictionary.Add(ActionToBePerformed.LeftClick, Constants.KEY_FUNCTION_UNASSIGNED_MESSAGE);
            FKeyMapDictionary.Add(ActionToBePerformed.Scroll, Constants.KEY_FUNCTION_UNASSIGNED_MESSAGE);
            FKeyMapDictionary.Add(ActionToBePerformed.RightClick, Constants.KEY_FUNCTION_UNASSIGNED_MESSAGE);
            FKeyMapDictionary.Add(ActionToBePerformed.MicInput, Constants.KEY_FUNCTION_UNASSIGNED_MESSAGE);
            FKeyMapDictionary.Add(ActionToBePerformed.MicInputOff, Constants.KEY_FUNCTION_UNASSIGNED_MESSAGE);


            //Instantiate keyboard hook and pass into worker class.
            LowLevelKeyBoardHook = new KeyboardHook();

            shortCutKeyWorker = new ShortcutKeyWorker(LowLevelKeyBoardHook, FKeyMapDictionary, eyeXHost);

            //Start monitoring key presses.
            LowLevelKeyBoardHook.HookKeyboard();
            Edge = AppBarEdges.Right;

            stateManager = new StateManager(this, shortCutKeyWorker, eyeXHost);
            stateManager.fixationWorker.FixationDetectionTimeLength = Program.readSettings.fixationTimeLength;
            stateManager.fixationWorker.FixationTimeOutLength = Program.readSettings.fixationTimeOut;
            stateManager.fixationWorker.fixationTimer.Interval = Program.readSettings.fixationTimeLength;
            stateManager.fixationWorker.timeOutTimer.Interval = Program.readSettings.fixationTimeOut;
            stateManager.magnifier.MaxZoom = Program.readSettings.maxZoom;
            shortCutKeyWorker.keyAssignments[ActionToBePerformed.LeftClick] = Program.readSettings.leftClick;
            shortCutKeyWorker.keyAssignments[ActionToBePerformed.DoubleClick] = Program.readSettings.doubleClick;
            shortCutKeyWorker.keyAssignments[ActionToBePerformed.RightClick] = Program.readSettings.rightClick;
            shortCutKeyWorker.keyAssignments[ActionToBePerformed.Scroll] = Program.readSettings.scoll;
            shortCutKeyWorker.keyAssignments[ActionToBePerformed.MicInput] = Program.readSettings.micInput;
            timer2.Enabled = true;

            Height = (int)System.Windows.SystemParameters.PrimaryScreenHeight;

            String[] sidebarArrangement = Program.readSettings.sidebar;
            ArrangeSidebar(sidebarArrangement);
        }

        private bool checkOpenForm(Type formToCheck)
        {
            foreach (Form form in Application.OpenForms)
            {
                if (form.GetType().Name == formToCheck.Name)
                {
                    return true;
                }
            }
            return false;
        }

        private void btnSettings_Click(object sender, EventArgs e)
        {
            
            if (!checkOpenForm(typeof(Settings)))
            {
                
                settings = new Settings(this, eyeXHost);
                settings.Show();
                AttemptToggle(SystemFlags.actionToBePerformed);
            }
        }

        public bool AttemptToggle(ActionToBePerformed action)
        {
            bool isScroll = (SystemFlags.currentState == SystemState.ApplyAction || SystemFlags.currentState == SystemState.ScrollWait) && (action == ActionToBePerformed.Scroll);

            if(SystemFlags.currentState == SystemState.ActionButtonSelected || SystemFlags.currentState == SystemState.ZoomWait || isScroll)
            {
                if (SystemFlags.actionToBePerformed == action)
                {
                    resetButtonsColor();
                    stateManager.EnterWaitState();

                    //special scrolling case
                    if(isScroll)
                    {
                        stopScroll();
                    }
                    return true;
                }
            }
            return false;
        }

        //stop ScrollControll from scrolling when another button is selected
        private void stopScroll()
        {
            SystemFlags.scrolling = false;
            stateManager.scrollWorker.stopScroll();
        }

        private void btnRightClick_Click(object sender, EventArgs e)
        {
            if (AttemptToggle(ActionToBePerformed.RightClick))
                return;
            stopScroll();
            SystemFlags.actionButtonSelected = true;//raise action button flag
            SystemFlags.actionToBePerformed = ActionToBePerformed.RightClick;   
        }

        private void btnSingleLeftClick_Click(object sender, EventArgs e)
        {
            if (AttemptToggle(ActionToBePerformed.LeftClick))
                return;
            stopScroll();
            SystemFlags.actionButtonSelected = true;//raise action button flag
            SystemFlags.actionToBePerformed = ActionToBePerformed.LeftClick;
        }

        private void btnDoubleClick_Click(object sender, EventArgs e)
        {
            if (AttemptToggle(ActionToBePerformed.DoubleClick))
                return;
            stopScroll();
            SystemFlags.actionButtonSelected = true;//raise action button flag
            SystemFlags.actionToBePerformed = ActionToBePerformed.DoubleClick;
        }

        private void btnKeyboard_Click(object sender, EventArgs e)
        {

            if (!checkOpenForm(typeof(Form2))) //Checks if keyboard is onscreen
            {
                form2 = new Form2(eyeXHost); //if not, create keyboard and show it
                AttemptToggle(SystemFlags.actionToBePerformed);
                stopScroll();
                if (form2.Visible)
                {
                    form2.Close();
                }
                else
                {
                    form2.Show();
                }
            }
            else
            {
                form2.Close();
            }
        }

        private void btnScoll_Click(object sender, EventArgs e)
        {
            if (AttemptToggle(ActionToBePerformed.Scroll))
                return;

            SystemFlags.actionButtonSelected = true;
            SystemFlags.actionToBePerformed = ActionToBePerformed.Scroll;

        }

        private void btnMic_Click(object sender, EventArgs e)
        {
            if (AttemptToggle(ActionToBePerformed.MicInput))
                return;

            SystemFlags.actionButtonSelected = true;//raise action button flag
            SystemFlags.actionToBePerformed = ActionToBePerformed.MicInput;
        }

        public void OnStartTextChange()
        {
            if (AutoStart.IsOn())
            {
                menuItemStartOnOff.Text = Constants.AUTO_START_ON;
                MenuItemStartOnOff.Checked = true;
            }
            else
            {
                menuItemStartOnOff.Text = Constants.AUTO_START_OFF;
                MenuItemStartOnOff.Checked = false;
            }
        }

        private void timer2_Tick(object sender, EventArgs e)
        {
            stateManager.Run();
        }


        private void setButtonPanelHight(List<Panel> panelList)
        {
            int screenHeight = Constants.SCREEN_SIZE.Height;
           
            int amountOfPanels = panelList.Count;
           
            int panelHight = panelList[0].Height;
            
            int screenSectionSize = screenHeight / amountOfPanels;
           
            int spacer = screenSectionSize - panelHight;
            
            int spacerBuffer = spacer / 2;

            foreach(Panel currentPanel in panelList)
            {
                Point panelLocation = new Point(currentPanel.Location.X, spacerBuffer);

                currentPanel.Location = panelLocation;

                spacerBuffer += screenSectionSize;
            }
        }
        public System.Windows.Forms.NotifyIcon NotifyIcon
        {
            get { return notifyIcon; }
            set { notifyIcon = value; }
        }


        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            //Remove KeyboardHook on closing form.
            LowLevelKeyBoardHook.UnHookKeyboard();
            eyeXHost.Dispose();
        }

    }
}
