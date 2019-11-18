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
    public partial class GeneralSettingsForm : Form
    {
        private SettingsHome home;
        private static FormsEyeXHost eyeXHost;
        private Form1 form1;
        public GeneralSettingsForm(SettingsHome home, Form1 form1, FormsEyeXHost EyeXHost)
        {
            eyeXHost = EyeXHost;
            InitializeComponent();
            connectBehaveMap();
            this.home = home;
            this.form1 = form1;
            this.BackColor = Program.readSettings.mainColour;
            controlRelocateAndResize();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            Close();
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

        private void btnAutoStart_Click(object sender, EventArgs e)
        {
            if (Program.onStartUp)
            {
                ChangeButtonColor(btnAutoStart, false, false);
            }
            else
            {
                ChangeButtonColor(btnAutoStart, true, false);
            }

            form1.OnStartTextChange();
        }

        public void ChangeButtonColor(Button button, bool onOff, bool hasText)
        {

            button.BackColor = onOff ? Program.readSettings.secondColour : Program.readSettings.mainColour;
            if (hasText)
            {
                if (onOff)
                {
                    button.ForeColor = Program.readSettings.mainColour;
                }
                else
                {
                    button.ForeColor = Program.readSettings.secondColour;
                }
            }
            Program.onStartUp = onOff;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {

            try
            {
                Program.readSettings.fixationTimeLength = trackBarFixTimeLength.Value * Constants.GAP_TIME_LENGTH + Constants.MIN_TIME_LENGTH;
                Program.readSettings.fixationTimeOut = trackBarFixTimeOut.Value * Constants.GAP_TIME_OUT + Constants.MIN_TIME_OUT;
                if (Program.onStartUp)
                {
                    AutoStart.SetOff();
                    Program.onStartUp = !Program.onStartUp;
                }
                else
                {
                    if (AutoStart.SetOn())
                    {
                        Program.onStartUp = !Program.onStartUp;
                    }
                }

                form1.OnStartTextChange();
                Program.readSettings.createJSON(Program.readSettings.sidebar);
                Close();
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

        private void GeneralSettingsForm_Load(object sender, EventArgs e)
        {
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
        }

        private void controlRelocateAndResize()
        {
            int percentageSize = 400; //Higher number for smaller trackbars
            panelSaveAndCancel.Location = ReletiveSize.panelSaveAndCancel(panelSaveAndCancel.Width, panelSaveAndCancel.Height);
            ReletiveSize.sizeEvenly(panelSaveAndCancel, 0.4);
            ReletiveSize.resizeLabel(label1, 20);
            ReletiveSize.resizeLabel(lblOther,50);

            ReletiveSize.resizeLabel(lblSpeed, 80);
            ReletiveSize.resizeLabel(lblFixationDetectionTimeLength, 80);
            pnlSave.Location = ReletiveSize.distribute(panelSaveAndCancel, pnlSave.Location.Y, 1, 2, "wn", 0.5);
            pnlCancel.Location = ReletiveSize.distribute(panelSaveAndCancel, pnlSave.Location.Y, 2, 2, "wn", 0.7);
            
            //General Settings size and location
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
            pnlOtherAuto.Location = ReletiveSize.distribute(panelOther, pnlOtherAuto.Location.Y, 2, 3, "w", 0.25);
            lblOther.Location = ReletiveSize.labelPosition(panelOther, lblOther);
            pnlOtherAuto.Location = ReletiveSize.centerLocation(panelOther, pnlOtherAuto);

            this.BackColor = Program.readSettings.mainColour;
            foreach (Control control in this.Controls)
            {
                control.BackColor = Program.readSettings.mainColour;
                control.ForeColor = Program.readSettings.secondColour;
            }
            panelPrecision.BackColor = Program.readSettings.mainColour;
            pnlFixationTimeOut.BackColor = Program.readSettings.mainColour;
            btnAutoStart.ForeColor = Program.readSettings.iconColour;
            lblOther.ForeColor = Program.readSettings.iconColour;
            panelOther.BackColor = Program.readSettings.mainColour;
            pnlOtherAuto.BackColor = Program.readSettings.mainColour;
            foreach (Panel panel in panelSaveAndCancel.Controls)
            {
                foreach (Button button in panel.Controls)
                {
                    button.ForeColor = Program.readSettings.iconColour;
                }
            }
            lblFixationDetectionTimeLength.ForeColor = Program.readSettings.iconColour;
            lblSpeed.ForeColor = Program.readSettings.iconColour;
        }


    }
}
