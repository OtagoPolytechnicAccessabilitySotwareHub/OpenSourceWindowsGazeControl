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
        private Form Form2;

        //Allocate memory location for KeyboardHook and worker.
        public KeyboardHook LowLevelKeyBoardHook;
        public ShortcutKeyWorker shortCutKeyWorker;
        private ScrollControl scrollWorker;
        private FixationDetection fixationWorker;


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
                //case "mic":
                //    return pnlHighLightMic;
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
            //FKeyMapDictionary.Add(ActionToBePerformed.MicInput, Constants.KEY_FUNCTION_UNASSIGNED_MESSAGE);
            //FKeyMapDictionary.Add(ActionToBePerformed.MicInputOff, Constants.KEY_FUNCTION_UNASSIGNED_MESSAGE);


            //Instantiate keyboard hook and pass into worker class.
            LowLevelKeyBoardHook = new KeyboardHook();

            shortCutKeyWorker = new ShortcutKeyWorker(LowLevelKeyBoardHook, FKeyMapDictionary, eyeXHost);
            scrollWorker = new ScrollControl(eyeXHost);
            fixationWorker = new FixationDetection(eyeXHost);

            //Start monitoring key presses.
            LowLevelKeyBoardHook.HookKeyboard();
            Edge = AppBarEdges.Right;

            stateManager = new StateManager(shortCutKeyWorker, scrollWorker, fixationWorker);


            trackBarFixTimeLength(Program.readSettings.fixationTimeLength, Program.readSettings.fixationTimeOut);
            trackBarFixTimeOut(Program.readSettings.fixationTimeLength, Program.readSettings.fixationTimeOut);

            shortCutKeyWorker.keyAssignments[ActionToBePerformed.LeftClick] = Program.readSettings.leftClick;
            shortCutKeyWorker.keyAssignments[ActionToBePerformed.DoubleClick] = Program.readSettings.doubleClick;
            shortCutKeyWorker.keyAssignments[ActionToBePerformed.RightClick] = Program.readSettings.rightClick;
            shortCutKeyWorker.keyAssignments[ActionToBePerformed.Scroll] = Program.readSettings.scroll;
            //shortCutKeyWorker.keyAssignments[ActionToBePerformed.MicInput] = Program.readSettings.micInput;
            timer2.Enabled = true;

            Height = (int)System.Windows.SystemParameters.PrimaryScreenHeight;

            String[] sidebarArrangement = Program.readSettings.sidebar;
            ArrangeSidebar(sidebarArrangement);
        }

        public void trackBarFixTimeOut(int FixationTimeOutLength, int timeOutTimerInterval)
        {
            stateManager.trackBarFixTimeOut(FixationTimeOutLength, timeOutTimerInterval);
        }
        public void trackBarFixTimeLength(int fixationDetectionTimeLength, int fixationTimerInterval)
        {
            stateManager.trackBarFixTimeLength(fixationDetectionTimeLength, fixationTimerInterval);
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
            }
        }

        //stop ScrollControll from scrolling when another button is selected
        private void stopScroll()
        {
            scrollWorker.stopScroll();
        }

        private void btnRightClick_Click(object sender, EventArgs e)
        {

            stopScroll();
            SystemFlags.actionButtonSelected = true;//raise action button flag
            SystemFlags.actionToBePerformed = ActionToBePerformed.RightClick;   
        }

        private void btnSingleLeftClick_Click(object sender, EventArgs e)
        {
            stopScroll();
            SystemFlags.actionButtonSelected = true;//raise action button flag
            SystemFlags.actionToBePerformed = ActionToBePerformed.LeftClick;
        }

        private void btnDoubleClick_Click(object sender, EventArgs e)
        {
            stopScroll();
            SystemFlags.actionButtonSelected = true;//raise action button flag
            SystemFlags.actionToBePerformed = ActionToBePerformed.DoubleClick;
        }

        private void btnKeyboard_Click(object sender, EventArgs e)
        {
            if (!checkOpenForm(typeof(Keyboard))) //Checks if keyboard is onscreen
            {
                Form2 = new Keyboard(eyeXHost); //if not, create keyboard and show it
                //AttemptToggle(SystemFlags.actionToBePerformed);
                stopScroll();
                if (Form2.Visible)
                {
                    Form2.Close();
                }
                else
                {
                    Form2.Show();
                }
            }
            else
            {
                Form2.Close();
            }
        }

        private void btnScroll_Click(object sender, EventArgs e)
        {
            if (SystemFlags.actionToBePerformed == ActionToBePerformed.Scroll)
            {
                stopScroll();
                SystemFlags.actionToBePerformed = ActionToBePerformed.none;
            }
            else
            {
                SystemFlags.actionButtonSelected = true;
                SystemFlags.actionToBePerformed = ActionToBePerformed.Scroll;
            }


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
            stateManager.RunCycle(sender, e);
            if(SystemFlags.actionToBePerformed == ActionToBePerformed.none)
            {
                resetButtonsColor();
            }
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
