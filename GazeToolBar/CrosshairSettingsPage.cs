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
    public partial class CrosshairSettingsPage : Form
    {
        private HomeSettings home;
        private static FormsEyeXHost eyeXHost;
        private Form1 form1;
        public CrosshairSettingsPage(HomeSettings home, Form1 form1, FormsEyeXHost EyeXHost)
        {
            eyeXHost = EyeXHost;
            InitializeComponent();
            this.home = home;
            this.form1 = form1;
        }
            private void btnSave_Click(object sender, EventArgs e)
        {

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
    }
}
