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
    public partial class ColourSettings : Form
    {
        private SettingsHome home;
        private static FormsEyeXHost eyeXHost;
        private Form1 form1;
        private List<Panel> panels;
        private List<Button> buttons;
        private Boolean colourbeingchanged;

        public ColourSettings(SettingsHome home, Form1 form1, FormsEyeXHost EyeXHost)
        {
            eyeXHost = EyeXHost;
            InitializeComponent();
            this.home = home;
            this.form1 = form1;
            pnlMain.BackColor = Program.readSettings.mainColour;
            pnlSec.BackColor = Program.readSettings.secondColour;
            this.BackColor = Program.readSettings.mainColour;
            int height = Convert.ToInt32(Math.Abs(System.Windows.SystemParameters.PrimaryScreenHeight));
            int width = Convert.ToInt32(Math.Abs(System.Windows.SystemParameters.PrimaryScreenWidth));
            brushColours.Height = Convert.ToInt32(height - (height * 0.2));
            brushColours.Top = Convert.ToInt32(height * 0.2);
            brushColours.Left = 0;
            brushColours.Width = width;
            int colourPanelwidth = Convert.ToInt32((brushColours.Width - (brushColours.Width * 0.3)) / 9);
            int colourPanelheight = Convert.ToInt32((brushColours.Height - (brushColours.Height * 0.3)) / 4);
            int down = Convert.ToInt32(brushColours.Height * 0.075);
            int left = Convert.ToInt32(brushColours.Width * 0.075);
            int countLeft = 0;
            foreach (Panel pane in brushColours.Controls.OfType<Panel>())
            {

                pane.Height = colourPanelheight;
                pane.Top = down;
                pane.Left = left;
                pane.Width = colourPanelwidth;
                countLeft++;
                left += (colourPanelwidth * 2);
                if (countLeft > 5)
                {
                    countLeft = 0;
                    left = Convert.ToInt32(brushColours.Width * 0.075);
                    down = down + (2 * colourPanelheight);
                }
                foreach (Button button in pane.Controls.OfType<Button>())
                {
                    button.Height = colourPanelheight - 6;
                    button.Width = colourPanelwidth - 6;
                }
            }
            
            brushColours.Left = ClientSize.Width + 400;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            Program.readSettings.secondColour = pnlSec.BackColor;
            Program.readSettings.mainColour = pnlMain.BackColor;
            Close();
        }

        private void btnMainColour_Click(object sender, EventArgs e)
        {
            btnPanel.Left = ClientSize.Width + 4000;
            brushColours.Left = 0;
            colourbeingchanged = false;
        }

        private void btnHighLight_Click(object sender, EventArgs e)
        {
            btnPanel.Left = ClientSize.Width + 4000;
            brushColours.Left = 0;
            colourbeingchanged = true;
        }

        private void btnIcon_Click(object sender, EventArgs e)
        {

        }


        private void changeColour(Button buttonClicked)
        {
            if(colourbeingchanged)//Highlight colour is being changed
            {
                
                pnlSec.BackColor = buttonClicked.BackColor;
            }
            else
            {

                pnlMain.BackColor = buttonClicked.BackColor;
            }
            btnPanel.Left = 0;
            brushColours.Left = ClientSize.Width + 4000;
        }


        private void colourOptionButton1_Click(object sender, EventArgs e)
        {
            changeColour(colourOptionButton1);
        }

        private void colourOptionButton2_Click(object sender, EventArgs e)
        {
            changeColour(colourOptionButton2);
        }

        private void colourOptionButton3_Click(object sender, EventArgs e)
        {
            changeColour(colourOptionButton3);
        }

        private void colourOptionButton4_Click(object sender, EventArgs e)
        {
            changeColour(colourOptionButton4);
        }

        private void colourOptionButton16_Click(object sender, EventArgs e)
        {
            changeColour(colourOptionButton16);
        }

        private void colourOptionButton5_Click(object sender, EventArgs e)
        {
            changeColour(colourOptionButton5);
        }

        private void colourOptionButton6_Click(object sender, EventArgs e)
        {
            changeColour(colourOptionButton6);
        }

        private void colourOptionButton7_Click(object sender, EventArgs e)
        {
            changeColour(colourOptionButton7);
        }

        private void colourOptionButton8_Click(object sender, EventArgs e)
        {
            changeColour(colourOptionButton8);
        }

        private void colourOptionButton9_Click(object sender, EventArgs e)
        {
            changeColour(colourOptionButton9);
        }

        private void colourOptionButton10_Click(object sender, EventArgs e)
        {
            changeColour(colourOptionButton10);
        }

        private void colourOptionButton11_Click(object sender, EventArgs e)
        {
            changeColour(colourOptionButton11);
        }

        private void colourOptionButton12_Click(object sender, EventArgs e)
        {
            changeColour(colourOptionButton12);
        }

        private void colourOptionButton13_Click(object sender, EventArgs e)
        {
            changeColour(colourOptionButton13);
        }

        private void colourOptionButton14_Click(object sender, EventArgs e)
        {
            changeColour(colourOptionButton14);
        }

        private void colourOptionButton15_Click(object sender, EventArgs e)
        {
            changeColour(colourOptionButton15);
        }

        private void colourOptionButton17_Click(object sender, EventArgs e)
        {
            changeColour(colourOptionButton17);
        }

        private void colourOptionButton18_Click(object sender, EventArgs e)
        {
            changeColour(colourOptionButton18);
        }

    }
}
