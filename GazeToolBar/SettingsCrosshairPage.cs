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
    public partial class SettingsCrosshairPage : Form
    {
        private SettingsHome home;
        private static FormsEyeXHost eyeXHost;
        private Form1 form1;
        public SettingsCrosshairPage(SettingsHome home, Form1 form1, FormsEyeXHost EyeXHost)
        {
            eyeXHost = EyeXHost;
            InitializeComponent();
            connectBehaveMap();
            this.home = home;
            this.form1 = form1;
            controlRelocateAndResize();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {

                Program.readSettings.Crosshair = trackBarCrosshair.Value;

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

        private void btnCancel_Click(object sender, EventArgs e)
        {
            Close();
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




        private void controlRelocateAndResize()
        {
            int percentageSize = 400; //Higher number for smaller trackbars
            panelSaveAndCancel.Location = ReletiveSize.panelSaveAndCancel(panelSaveAndCancel.Width, panelSaveAndCancel.Height);
            ReletiveSize.sizeEvenly(panelSaveAndCancel, 0.4);
            ReletiveSize.resizeLabel(label1, 20);
            ReletiveSize.resizeLabel(labCrosshairType, 80);
            pnlSave.Location = ReletiveSize.distribute(panelSaveAndCancel, pnlSave.Location.Y, 1, 2, "wn", 0.5);
            pnlCancel.Location = ReletiveSize.distribute(panelSaveAndCancel, pnlSave.Location.Y, 2, 2, "wn", 0.7);
            //Set feed back label to the center of the screen.
            //lbFKeyFeedback.Location = new Point((pnlPageKeyboard.Width / 2) - (lbFKeyFeedback.Width / 2), lbFKeyFeedback.Location.Y);
            //pnlPageKeyboard.Location = ReletiveSize.mainPanelLocation(pnlSwitchSetting.Location.Y, pnlSwitchSetting.Height);

            //Zoom type panel
            //Main panel
            pnlCrosshairPage.Size = ReletiveSize.panelGeneralSize(panelSaveAndCancel.Location.Y, pnlCrosshairPage.Location.Y);
            //pnlCrosshairPage.Width = ClientSize.Width;
            //Crosshair selection panel
            panelCrosshairSelection.Location = ReletiveSize.distributeToBottom(pnlCrosshairPage, panelCrosshairSelection.Location.X, panelCrosshairSelection.Height, 1, 3, "h", 0);
            panelCrosshairSelection.Size = new Size(pnlCrosshairPage.Size.Width, panelCrosshairSelection.Size.Height);
            panelCrosshairHolder.Location = ReletiveSize.distribute(panelCrosshairSelection, panelCrosshairHolder.Location.Y, 1, 1, "w", 0.1);
            panelCrosshairHolder.Size = ReletiveSize.controlLength(pnlCrosshairPage, panelCrosshairHolder.Size.Height, 0.85);
            double crosshairPercentage = (double)(panelCrosshairHolder.Size.Width - percentageSize) / (double)panelCrosshairHolder.Size.Width;
            trackBarCrosshair.Size = ReletiveSize.controlLength(panelCrosshairHolder, trackBarCrosshair.Size.Height, crosshairPercentage);
            pnlCrosshairUpButton.Location = ReletiveSize.reletiveLocation(trackBarCrosshair, pnlCrosshairUpButton.Location.Y, 7, 'v');
            labCrosshairType.Location = ReletiveSize.labelPosition(panelCrosshairSelection, labCrosshairType);
            //Crosshair picture
            pictureBoxCrosshairPreview.Location = ReletiveSize.distribute(pnlCrosshairPage, pictureBoxCrosshairPreview.Location.X, 2, 3, "h", 0);
            pictureBoxCrosshairPreview.Location = ReletiveSize.distribute(pnlCrosshairPage, pictureBoxCrosshairPreview.Location.Y, 2, 2, "w", 0.5);
            pictureBoxCrosshairPreview.Left = (pictureBoxCrosshairPreview.Location.X - (pictureBoxCrosshairPreview.Width / 2));

            foreach (Panel panel in panelSaveAndCancel.Controls)
            {
                foreach (Button button in panel.Controls)
                {
                    button.ForeColor = Program.readSettings.secondColour;
                }
            }
            label1.ForeColor = Program.readSettings.secondColour;
            labCrosshairType.ForeColor = Program.readSettings.secondColour;
            this.BackColor = Program.readSettings.mainColour;
        }


    }
}
