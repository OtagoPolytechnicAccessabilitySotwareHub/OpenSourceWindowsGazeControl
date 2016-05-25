﻿using System;
using System.Drawing;
using System.Windows.Forms;
using EyeXFramework;
using Tobii;
using System.IO;
using System.Diagnostics;


namespace GazeToolBar
{
    /*
        Date: 17/05/2016
        Name: Derek Dai
        Description: Main toolbar form
    */
    public partial class Form1 : ShellLib.ApplicationDesktopToolbar
    {
        EyeXHost eyeXhost;
        FixationDetection fixationWorker;
        private Settings settings;
        private ContextMenu contextMenu;
        private MenuItem menuItemExit;
        private MenuItem menuItemStartOnOff;
        private Bitmap leftSingleClick;
        private Bitmap rightClick;
        private Bitmap settingIcon;
        private Bitmap doubleClick;
        private Bitmap scrollImage;
        private Bitmap keyboardImage;
        private Bitmap dragAndDropImage;

        public Form1()
        {
            //Initial a image to each button
            leftSingleClick = new Bitmap(new Bitmap("Left-Click-icon.png"), ReletiveSize.btnSize);
            rightClick = new Bitmap(new Bitmap("Right-Click-icon.png"), ReletiveSize.btnSize);
            settingIcon = new Bitmap(new Bitmap("Settings-icon.png"), ReletiveSize.btnSize);
            doubleClick = new Bitmap(new Bitmap("Double-Click-icon.png"), ReletiveSize.btnSize);
            scrollImage = new Bitmap(new Bitmap("Scroll-icon.png"), ReletiveSize.btnSize);
            keyboardImage = new Bitmap(new Bitmap("Keyboard-icon.png"), ReletiveSize.btnSize);
            dragAndDropImage = new Bitmap(new Bitmap("Drag-and-drop-icon.png"), ReletiveSize.btnSize);

            //Change resolution to 800 * 600
            ChangeResolution.ChangeScreenResolution();            
            InitializeComponent();
            Size = ReletiveSize.formSize;
            AutoStart.OpenKey();
            contextMenu = new ContextMenu();
            menuItemExit = new MenuItem();
            menuItemStartOnOff = new MenuItem();
            initMenuItem();
            setBtnSize();
            
            Edge = AppBarEdges.Right;
            AutoStart.IsAutoStart(settings, menuItemStartOnOff);

            //Set all the image to its button
            btnSingleLeftClick.Image = leftSingleClick;
            btnRightClick.Image = rightClick;
            btnSettings.Image = settingIcon;
            btnDoubleClick.Image = doubleClick;
            btnScoll.Image = scrollImage;
            btnKeyboard.Image = keyboardImage;
            btnDragAndDrop.Image = dragAndDropImage;

            connectBehaveMap();
        }

        /// <summary>
        /// Setup the context menu for
        /// notify icon
        /// </summary>
        private void initMenuItem()
        {
            menuItemExit.Text = "Exit";
            menuItemStartOnOff.Text = ValueNeverChange.AUTO_START_OFF;
            menuItemExit.Click += new EventHandler(menuItemExit_Click);
            menuItemStartOnOff.Click += new EventHandler(menuItemStartOnOff_Click);
            contextMenu.MenuItems.Add(menuItemStartOnOff);
            contextMenu.MenuItems.Add(menuItemExit);
           // ntficGaze.ContextMenu = contextMenu;
        }

        /// <summary>
        /// Set all the size of buttons, panel
        /// and location of the buttons, panel.
        /// This will make tool bar adjust itself correspond to screen resolution
        /// </summary>
        private void setBtnSize()
        {
            btnSingleLeftClick.Size = ReletiveSize.btnSize;
            btnDoubleClick.Size = ReletiveSize.btnSize;
            btnRightClick.Size = ReletiveSize.btnSize;
            btnSettings.Size = ReletiveSize.btnSize;
            btnScoll.Size = ReletiveSize.btnSize;
            btnKeyboard.Size = ReletiveSize.btnSize;
            btnDragAndDrop.Size = ReletiveSize.btnSize;

            btnSingleLeftClick.Location = new Point(ReletiveSize.btnPositionX, ReletiveSize.btnPostionY(2));
            btnDoubleClick.Location = new Point(ReletiveSize.btnPositionX, ReletiveSize.btnPostionY(3));
            btnRightClick.Location = new Point(ReletiveSize.btnPositionX, ReletiveSize.btnPostionY(1));
            btnSettings.Location = new Point(ReletiveSize.btnPositionX, ReletiveSize.btnPostionY(4));
            btnScoll.Location = new Point(ReletiveSize.btnPositionX, ReletiveSize.btnPostionY(5));
            btnKeyboard.Location = new Point(ReletiveSize.btnPositionX, ReletiveSize.btnPostionY(6));
            btnDragAndDrop.Location = new Point(ReletiveSize.btnPositionX, ReletiveSize.btnPostionY(7));

            panel.Location = new Point(panel.Location.X, ReletiveSize.panelPositionY);
            panel.Size = ReletiveSize.panelSize;
        }

        private void menuItemExit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void menuItemStartOnOff_Click(object sender, EventArgs e)
        {
            AutoStart.setAutoStartOnOff(settings, menuItemStartOnOff);
        }

        /// <summary>
        /// Change resolution back to its original resolution.
        /// This will solve the problem that desktop won't show the task bar properly.
        /// </summary>
        private void Form1_Shown(object sender, System.EventArgs e)
        {
            ChangeResolution.ChangeResolutionBack();
        }

        public MenuItem MenuItemStartOnOff { get { return menuItemStartOnOff; } }

        public Settings Settings { get { return settings; }
            
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            eyeXhost = new EyeXHost();
            eyeXhost.Start();
            fixationWorker = new FixationDetection(eyeXhost);
        }

        private void btnSettings_Click(object sender, EventArgs e)
        {
            settings = new Settings(this);
            settings.Show();
        }

        private void btnRightClick_Click(object sender, EventArgs e)
        {
            fixationWorker.SetupSelectedFixationAction(VirtualMouse.RightMouseClick);
        }

        private void btnSingleLeftClick_Click(object sender, EventArgs e)
        {
            ZoomLens zoom = new ZoomLens();
            fixationWorker.SetupSelectedFixationAction(zoom.CreateZoomLens);
        }

        private void btnDoubleClick_Click(object sender, EventArgs e)
        {
            fixationWorker.SetupSelectedFixationAction(VirtualMouse.LeftDoubleClick);
        }

        private void btnKeyboard_Click(object sender, EventArgs e)
        {
            // this will open the exe for optikey. is tried to both the location of optikeys exe and the binary file for GazeToolBar. so will likely break if file/folders are moved
            //will need some logic to decide if it needs to open or close optikey
            Process process = System.Diagnostics.Process.Start(Path.GetFullPath("../../../OptiKey/src/JuliusSweetland.OptiKey/bin/Debug/OptiKey.exe"));
            //MessageBox.Show(Environment.CurrentDirectory);

            //if optikey is already open
            //process.Kill();
        }

        private void btnScoll_Click(object sender, EventArgs e)
        {
            fixationWorker.SetupSelectedFixationAction(VirtualMouse.MiddleMouseButton);
            //Add logic to scroll/pan with eyes after middle click
        }

        private void btnDragAndDrop_Click(object sender, EventArgs e)
        {
            //Create logic to run left mouse down, update xy then left mouse up to simulate drag and drop
        }


    }
}
