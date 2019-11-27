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
        private int saveLeft;
        private int colourbeingchanged;
        private int tempIconColour;
        private Button currentMain;
        private Button currentSecond;
        private Button currentIcon;

        public ColourSettings(SettingsHome home, Form1 form1, FormsEyeXHost EyeXHost)
        {
            eyeXHost = EyeXHost;
            InitializeComponent();
            connectBehaveMap();
            this.home = home;
            this.form1 = form1;
            pnlMain.BackColor = Program.readSettings.mainColour;
            pnlSec.BackColor = Program.readSettings.secondColour;
            pnlIcon.BackColor = Program.readSettings.iconColour;
            tempIconColour = Program.readSettings.iconNumber;
            this.BackColor = Program.readSettings.mainColour;
            controlRelocateAndResize();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            
            Close();
            home.Show();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                Program.readSettings.secondColour = pnlSec.BackColor;
                Program.readSettings.mainColour = pnlMain.BackColor;
                Program.readSettings.iconNumber = tempIconColour;
                Program.readSettings.iconColour = pnlIcon.BackColor;
                Program.readSettings.createJSON(Program.readSettings.sidebar);
                home.Show();
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

        private void blankUsedColour()
        {
            switch (colourbeingchanged)
            {
                case 0:
                    currentSecond.Enabled = false;
                    break;
                case 1:
                    currentMain.Enabled = false;
                    break;
                case 2:
                    eliminateIconColourOption();
                    if (currentIcon!=null)
                    {
                        currentIcon.Enabled = false;
                        currentIcon.ForeColor = Program.readSettings.iconColour;
                        currentIcon.Text = "Main Colour";
                    }
                    break;
            }
            currentSecond.ForeColor = currentMain.BackColor;
            currentMain.ForeColor = currentSecond.BackColor;
            currentSecond.Text = "Highlight colour";
            currentMain.Text = "Main colour";
        }



        private void btnMainColour_Click(object sender, EventArgs e)
        {
            btnPanel.Left = ClientSize.Width + 4000;
            panelSaveAndCancel.Left = ClientSize.Width + 4000;
            brushColours.Left = 0;
            colourbeingchanged = 0;
            label8.Text = "Background Colour";
            ReletiveSize.resizeLabel(label8, 20);
            blankUsedColour();
        }

        private void btnHighLight_Click(object sender, EventArgs e)
        {
            btnPanel.Left = ClientSize.Width + 4000;
            panelSaveAndCancel.Left = ClientSize.Width + 4000;
            brushColours.Left = 0;
            colourbeingchanged = 1;
            label8.Text = "Highlight Colour";
            ReletiveSize.resizeLabel(label8, 20);
            blankUsedColour();
        }

        private void btnIcon_Click(object sender, EventArgs e)
        {
            btnPanel.Left = ClientSize.Width + 4000;
            panelSaveAndCancel.Left = ClientSize.Width + 4000;
            iconPanel.Left = 0;
            colourbeingchanged = 2;
            label8.Text = "Icon Colour";
            ReletiveSize.resizeLabel(label8, 20);
            blankUsedColour();
        }


        private void changeColour(Button buttonClicked)
        {
            switch(colourbeingchanged)
            {
                case 0:
                    pnlMain.BackColor = buttonClicked.BackColor;
                    currentMain.Text = "";
                    currentMain = buttonClicked;
                    break;
                case 1:
                    pnlSec.BackColor = buttonClicked.BackColor;
                    currentSecond.Text = "";
                    currentSecond = buttonClicked;
                    break;
                case 2:
                    if(currentIcon!=null)
                    {
                        currentIcon.Text = "";
                        currentIcon.Enabled = true;
                    }
                    pnlIcon.BackColor = buttonClicked.BackColor;
                    break;
            }
            btnPanel.Left = 0;
            iconPanel.Left = ClientSize.Width + 4000;
            panelSaveAndCancel.Left = saveLeft;
            brushColours.Left = ClientSize.Width + 4000;
            label8.Text = "Color Settings";
            ReletiveSize.resizeLabel(label8, 20);
            currentMain.Enabled = true;
            currentSecond.Enabled = true;
            
        }




        private void eliminateIconColourOption()
        {
            currentIcon = null;
            foreach (Panel pane in iconPanel.Controls.OfType<Panel>())
            {
                foreach (Button button in pane.Controls.OfType<Button>())
                {
                    if (button.BackColor == pnlMain.BackColor)
                    {
                        currentIcon = button;
                    }
                }
            }
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


        private void controlRelocateAndResize()
        {
            int percentageSize = 400; //Higher number for smaller trackbars
            panelSaveAndCancel.Location = ReletiveSize.panelSaveAndCancel(panelSaveAndCancel.Width, panelSaveAndCancel.Height);
            ReletiveSize.sizeEvenly(panelSaveAndCancel, 0.4);
            pnlSave.Location = ReletiveSize.distribute(panelSaveAndCancel, pnlSave.Location.Y, 1, 2, "wn", 0.5);
            pnlCancel.Location = ReletiveSize.distribute(panelSaveAndCancel, pnlSave.Location.Y, 2, 2, "wn", 0.7);
            btnPanel.Size = ReletiveSize.panelGeneralSize(panelSaveAndCancel.Top, btnPanel.Top);
            ReletiveSize.resizeLabel(label8, 20);
            saveLeft = panelSaveAndCancel.Left;
            btnPanel.Top = label8.Top + label8.Height+20;
            panelTop.Height = panelBottom.Height = btnPanel.Height / 2;
            panelTop.Location = ReletiveSize.distribute(btnPanel, panelTop.Location.X, 1, 2, "h", 0.5);
            panelBottom.Location = ReletiveSize.distribute(btnPanel, panelTop.Location.X, 2, 2, "h", 0.5);
            panelTop.Width = panelBottom.Width = btnPanel.Width-200;
            panelTop.Left = panelBottom.Left = 100;
            ReletiveSize.middleSquare(panelTop, 3);
            //ReletiveSize.sizeEvenly(panelTop, 0.4);
            pnlMain.Location = ReletiveSize.distribute(panelTop, 5, 1, 3, "wn", 0.4);
            pnlSec.Location = ReletiveSize.distribute(panelTop, 5, 2, 3, "wn", 0.4);
            pnlIcon.Location = ReletiveSize.distribute(panelTop, 5, 3, 3, "wn", 0.4);
            ReletiveSize.middleSquare(panelBottom, 3);
            //ReletiveSize.sizeEvenly(panelBottom, 0.4);
            pnlBackColour.Location = ReletiveSize.distribute(panelBottom, 10, 1, 3, "wn", 0.4);
            pnlHighlightColour.Location = ReletiveSize.distribute(panelBottom, 10, 2, 3, "wn", 0.4);
            pnlIconColour.Location = ReletiveSize.distribute(panelBottom, 10, 3, 3, "wn", 0.4);


            //int height = Convert.ToInt32(Math.Abs(System.Windows.SystemParameters.PrimaryScreenHeight));
            //int width = Convert.ToInt32(Math.Abs(System.Windows.SystemParameters.PrimaryScreenWidth));
            brushColours.Height = Convert.ToInt32(Constants.SCREEN_SIZE.Height - (Constants.SCREEN_SIZE.Height * 0.2));
            brushColours.Top = Convert.ToInt32(Constants.SCREEN_SIZE.Height * 0.2);
            brushColours.Left = 0;
            brushColours.Width = Constants.SCREEN_SIZE.Width;
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
                    button.Font = new Font(button.Font.FontFamily, Constants.SCREEN_SIZE.Width / 90);
                    if (button.BackColor==Program.readSettings.mainColour)
                    {
                        currentMain = button;
                    }
                    if (button.BackColor == Program.readSettings.secondColour)
                    {
                        currentSecond = button;
                    }
                }
            }
            iconPanel.Height = Convert.ToInt32(Constants.SCREEN_SIZE.Height - (Constants.SCREEN_SIZE.Height * 0.2));
            iconPanel.Top = Convert.ToInt32(Constants.SCREEN_SIZE.Height * 0.2);
            iconPanel.Left = 0;
            iconPanel.Width = Constants.SCREEN_SIZE.Width;
            colourPanelwidth = Convert.ToInt32((iconPanel.Width - (iconPanel.Width * 0.3)) / 4);
            colourPanelheight = Convert.ToInt32((iconPanel.Height - (iconPanel.Height * 0.3)) / 4);
            down = Convert.ToInt32(iconPanel.Height * 0.075);
            left = Convert.ToInt32(iconPanel.Width * 0.075);
            countLeft = 0;
            foreach (Panel pane in iconPanel.Controls.OfType<Panel>())
            {

                pane.Height = colourPanelheight;
                pane.Top = down;
                pane.Left = left;
                pane.Width = colourPanelwidth;
                countLeft++;
                left += (colourPanelwidth * 2);
                if (countLeft > 2)
                {
                    countLeft = 0;
                    left = Convert.ToInt32(iconPanel.Width * 0.075);
                    down = down + (2 * colourPanelheight);
                }
                foreach (Button button in pane.Controls.OfType<Button>())
                {
                    button.Height = colourPanelheight - 6;
                    button.Width = colourPanelwidth - 6;
                    button.Font = new Font(button.Font.FontFamily, Constants.SCREEN_SIZE.Width / 90);
                    if (button.BackColor== pnlMain.BackColor)
                    {
                        currentIcon = button;
                    }
                }
            }
            brushColours.Left = ClientSize.Width + 400;
            iconPanel.Left = ClientSize.Width + 400;
            foreach (Panel pane in panelTop.Controls)
            {
                pane.ForeColor = Program.readSettings.iconColour;
            }
            foreach (Panel pane in panelBottom.Controls)
            {
                foreach (Button button in pane.Controls)
                {
                    button.ForeColor = Program.readSettings.iconColour;
                }
                    
            }
            foreach (Control control in this.Controls)
            {
                control.BackColor = Program.readSettings.mainColour;
                control.ForeColor = Program.readSettings.iconColour;
            }
            foreach (Panel panel in panelSaveAndCancel.Controls)
            {
                foreach (Button button in panel.Controls)
                {
                    button.ForeColor = Program.readSettings.iconColour;
                }
            }
        }

        private void iconbtnBlack_Click(object sender, EventArgs e)
        {
            tempIconColour = 0;
            changeColour(iconbtnBlack);
        }

        private void iconbtnWhite_Click(object sender, EventArgs e)
        {
            tempIconColour = 1;
            changeColour(iconbtnWhite);
        }

        private void iconbtnBlue_Click(object sender, EventArgs e)
        {
            tempIconColour = 2;
            changeColour(iconbtnBlue);
        }

        private void iconbtnRed_Click(object sender, EventArgs e)
        {
            tempIconColour = 3;
            changeColour(iconbtnRed);
        }

        private void iconbtnYellow_Click(object sender, EventArgs e)
        {
            tempIconColour = 4;
            changeColour(iconbtnYellow);
        }

        private void iconbtnGreen_Click(object sender, EventArgs e)
        {
            tempIconColour = 5;
            changeColour(iconbtnGreen);
        }
    }
    }
