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
    public partial class ZoomSettingsForm : Form
    {
        private SettingsHome home;
        private static FormsEyeXHost eyeXHost;
        private Form1 form1;
        private bool dynamicZoom, corners;
        public ZoomSettingsForm(SettingsHome home, Form1 form1, FormsEyeXHost EyeXHost)
        {
            eyeXHost = EyeXHost;
            InitializeComponent();
            this.home = home;
            this.form1 = form1;
            connectBehaveMap();
            controlRelocateAndResize();
        }


        //Method to update a trackbar up or down in value. Used by both trackpads.
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

        public void ChangeButtonColor(Button button, bool onOff, bool hasText)
        {

            button.BackColor = onOff ? Program.readSettings.iconColour : Program.readSettings.mainColour;
            if (hasText)
            {
                if (onOff)
                {
                    button.ForeColor = Program.readSettings.mainColour;
                }
                else
                {
                    button.ForeColor = Program.readSettings.iconColour;
                }
            }
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

        private void btnStaticZoomMode_Click(object sender, EventArgs e)
        {
            dynamicZoom = false;
            corners = false;
            //clicked button
            ChangeButtonColor(btnStaticZoomMode, true, true);
            ChangeButtonColor(btnDynamicZoomMode, false, true);
            ChangeButtonColor(btnCornerZoomMode, false, true);

        }

        private void btnCornerZoomMode_Click(object sender, EventArgs e)
        {
            dynamicZoom = false;
            corners = true;
            //clicked button
            ChangeButtonColor(btnStaticZoomMode, false, true);
            ChangeButtonColor(btnDynamicZoomMode, false, true);
            ChangeButtonColor(btnCornerZoomMode, true, true);
            //btnCornerZoomMode.BackColor = Color.White;
            //btnCornerZoomMode.ForeColor = Color.Black;
            ////unselected
            //btnDynamicZoomMode.BackColor = Color.Black;
            //btnDynamicZoomMode.ForeColor = Color.White;
            //btnStaticZoomMode.BackColor = Color.Black;
            //btnStaticZoomMode.ForeColor = Color.White;
        }

        private void btnDynamicZoomMode_Click(object sender, EventArgs e)
        {
            dynamicZoom = true;
            corners = false;
            //clicked button
            ChangeButtonColor(btnStaticZoomMode, false, true);
            ChangeButtonColor(btnDynamicZoomMode, true, true);
            ChangeButtonColor(btnCornerZoomMode, false, true);
            //btnDynamicZoomMode.BackColor = Color.White;
            //btnDynamicZoomMode.ForeColor = Color.Black;
            ////unselected
            //btnStaticZoomMode.BackColor = Color.Black;
            //btnStaticZoomMode.ForeColor = Color.White;
            //btnCornerZoomMode.BackColor = Color.Black;
            //btnCornerZoomMode.ForeColor = Color.White;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try{
                Program.readSettings.dynamicZoom = dynamicZoom;
                Program.readSettings.centerZoom = corners;
                Program.readSettings.maxZoom = trackBarZoomAmount.Value;
                Program.readSettings.zoomWindowSize = trackBarZoomWindowSize.Value;

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



        private void ZoomSettingsForm_Load(object sender, EventArgs e)
        {
            dynamicZoom = Program.readSettings.dynamicZoom;
            corners = Program.readSettings.centerZoom;
            ChangeButtonColor(btnDynamicZoomMode, false, true);
            ChangeButtonColor(btnCornerZoomMode, false, true);
            ChangeButtonColor(btnStaticZoomMode, false, true);
            if (dynamicZoom)
            {

                ChangeButtonColor(btnStaticZoomMode, false, true);
                ChangeButtonColor(btnDynamicZoomMode, true, true);
                ChangeButtonColor(btnCornerZoomMode, false, true);
            }
            else
            {
                if (corners)
                {
                    ChangeButtonColor(btnDynamicZoomMode, false, true);
                    ChangeButtonColor(btnCornerZoomMode, true, true);
                    ChangeButtonColor(btnStaticZoomMode, false, true);
                }
                else
                {
                    ChangeButtonColor(btnDynamicZoomMode, false, true);
                    ChangeButtonColor(btnCornerZoomMode, false, true);
                    ChangeButtonColor(btnStaticZoomMode, true, true);
                }
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            Close();
        }


        private void controlRelocateAndResize()
        {
            int percentageSize = 400; //Higher number for smaller trackbars
            panelSaveAndCancel.Location = ReletiveSize.panelSaveAndCancel(panelSaveAndCancel.Width, panelSaveAndCancel.Height);
            ReletiveSize.sizeEvenly(panelSaveAndCancel, 0.4);
            pnlSave.Location = ReletiveSize.distribute(panelSaveAndCancel, pnlSave.Location.Y, 1, 2, "wn", 0.5);
            pnlCancel.Location = ReletiveSize.distribute(panelSaveAndCancel, pnlSave.Location.Y, 2, 2, "wn", 0.7);
            ReletiveSize.resizeLabel(label1, 20);
            //General Settings size and location


            //Panel other

            //Set feed back label to the center of the screen.
            //lbFKeyFeedback.Location = new Point((pnlPageKeyboard.Width / 2) - (lbFKeyFeedback.Width / 2), lbFKeyFeedback.Location.Y);
            //pnlPageKeyboard.Location = ReletiveSize.mainPanelLocation(pnlSwitchSetting.Location.Y, pnlSwitchSetting.Height);

            //Zoom Settings size and location
            //Main Panel
            pnlZoomSettings.Size = ReletiveSize.panelGeneralSize(panelSaveAndCancel.Location.Y, pnlZoomSettings.Location.Y);
            //Zoom size panel
            pnlZoomSize.Location = ReletiveSize.distribute(pnlZoomSettings, pnlZoomSize.Location.X, 1, 3, "h", 0);
            pnlZoomSize.Size = new Size(pnlZoomSettings.Size.Width, pnlZoomSize.Size.Height);
            pnlZoomSizeContent.Location = ReletiveSize.distribute(pnlZoomSize, pnlZoomSizeContent.Location.Y, 1, 1, "w", 0.1);
            pnlZoomSizeContent.Size = ReletiveSize.controlLength(pnlZoomSettings, pnlZoomSizeContent.Size.Height, 0.85);
            double zoomPercentage = (double)(pnlZoomSizeContent.Size.Width - percentageSize) / (double)pnlZoomSizeContent.Size.Width;
            trackBarZoomWindowSize.Size = ReletiveSize.controlLength(pnlZoomSizeContent, trackBarZoomWindowSize.Size.Height, zoomPercentage);
            pnlZWSPlus.Location = ReletiveSize.reletiveLocation(trackBarZoomWindowSize, pnlZWSPlus.Location.Y, 7, 'v');
            labZoomWindowSize.Location = ReletiveSize.labelPosition(pnlZoomSize, labZoomWindowSize);
            //Zoom amount panel
            pnlZoomAmount.Location = ReletiveSize.distribute(pnlZoomSettings, pnlZoomAmount.Location.X, 2, 3, "h", 0);
            pnlZoomAmount.Size = new Size(pnlZoomSettings.Size.Width, pnlZoomAmount.Size.Height);
            pnlZoomAmountContent.Location = new Point(pnlZoomSizeContent.Location.X, pnlZoomAmountContent.Location.Y);
            pnlZoomAmountContent.Size = pnlZoomSizeContent.Size;
            trackBarZoomAmount.Size = trackBarZoomWindowSize.Size;
            pnlZIAPlus.Location = new Point(pnlZWSPlus.Location.X, pnlZIAPlus.Location.Y);
            labZoomAmount.Location = ReletiveSize.labelPosition(pnlZoomAmount, labZoomAmount);
            //Rearrange panel
            pnlZoomMode.Location = ReletiveSize.distribute(pnlZoomSettings, pnlZoomMode.Location.X, 3, 3, "h", 0);
            pnlZoomMode.Size = new Size(pnlZoomSettings.Size.Width, pnlZoomMode.Size.Height);
            ReletiveSize.sizeEvenly(pnlZoomMode, 0.6);
            pnlStaticZoomMode.Location = ReletiveSize.distribute(pnlZoomMode, pnlStaticZoomMode.Location.Y, 1, 3, "wn", 0.3);
            pnlCornerZoomMode.Location = ReletiveSize.distribute(pnlZoomMode, pnlCornerZoomMode.Location.Y, 2, 3, "wn", 0.3);
            pnlDynamicZoomMode.Location = ReletiveSize.distribute(pnlZoomMode, pnlDynamicZoomMode.Location.Y, 3, 3, "wn", 0.3);
            foreach (Panel panel in panelSaveAndCancel.Controls)
            {
                foreach (Button button in panel.Controls)
                {
                    button.ForeColor = Program.readSettings.iconColour;
                }
            }
            label1.ForeColor = Program.readSettings.iconColour;
            pnlZoomMode.BackColor = Program.readSettings.mainColour;
            pnlZoomSize.BackColor = Program.readSettings.mainColour;
            pnlZoomAmount.BackColor = Program.readSettings.mainColour;
            labZoomWindowSize.ForeColor = Program.readSettings.iconColour;
            labZoomAmount.ForeColor = Program.readSettings.iconColour;
            this.BackColor = Program.readSettings.mainColour;
            this.ForeColor = Program.readSettings.iconColour;


        }
        









    }
}
