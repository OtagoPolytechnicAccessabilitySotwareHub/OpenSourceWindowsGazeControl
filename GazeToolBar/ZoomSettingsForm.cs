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
            btnStaticZoomMode.BackColor = Color.White;
            btnStaticZoomMode.ForeColor = Color.Black;
            //unselected
            btnDynamicZoomMode.BackColor = Color.Black;
            btnDynamicZoomMode.ForeColor = Color.White;
            btnCornerZoomMode.BackColor = Color.Black;
            btnCornerZoomMode.ForeColor = Color.White;
        }

        private void btnCornerZoomMode_Click(object sender, EventArgs e)
        {
            dynamicZoom = false;
            corners = true;
            //clicked button
            btnCornerZoomMode.BackColor = Color.White;
            btnCornerZoomMode.ForeColor = Color.Black;
            //unselected
            btnDynamicZoomMode.BackColor = Color.Black;
            btnDynamicZoomMode.ForeColor = Color.White;
            btnStaticZoomMode.BackColor = Color.Black;
            btnStaticZoomMode.ForeColor = Color.White;
        }

        private void btnDynamicZoomMode_Click(object sender, EventArgs e)
        {
            dynamicZoom = true;
            corners = false;
            //clicked button
            btnDynamicZoomMode.BackColor = Color.White;
            btnDynamicZoomMode.ForeColor = Color.Black;
            //unselected
            btnStaticZoomMode.BackColor = Color.Black;
            btnStaticZoomMode.ForeColor = Color.White;
            btnCornerZoomMode.BackColor = Color.Black;
            btnCornerZoomMode.ForeColor = Color.White;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try{
                Program.readSettings.dynamicZoom = dynamicZoom;
                Program.readSettings.centerZoom = corners;
                Program.readSettings.maxZoom = trackBarZoomAmount.Value;
                Program.readSettings.zoomWindowSize = trackBarZoomWindowSize.Value;


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

        void NotifyIcon_BalloonTipClicked(object sender, EventArgs e)
        {
            MessageBox.Show((String)((NotifyIcon)sender).Tag, "Exception", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private void ZoomSettingsForm_Load(object sender, EventArgs e)
        {
            dynamicZoom = Program.readSettings.dynamicZoom;
            corners = Program.readSettings.centerZoom;
            if (dynamicZoom)
            {
                btnDynamicZoomMode.BackColor = Color.White;
                btnDynamicZoomMode.ForeColor = Color.Black;
            }
            else
            {
                if (corners)
                {
                    btnCornerZoomMode.BackColor = Color.White;
                    btnCornerZoomMode.ForeColor = Color.Black;
                }
                else
                {
                    btnStaticZoomMode.BackColor = Color.White;
                    btnStaticZoomMode.ForeColor = Color.Black;
                }
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
