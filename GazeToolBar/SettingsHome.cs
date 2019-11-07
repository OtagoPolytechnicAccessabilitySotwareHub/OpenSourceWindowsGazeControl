using System;
using System.Windows.Forms;
using ShellLib;
using System.Drawing;
using Newtonsoft.Json;
using System.IO;
using System.Collections.Generic;
using EyeXFramework.Forms;
using System.Linq;

namespace GazeToolBar
{
 /*
 *  Class: HomeSettings
 *  Name: 
 *  Date: 5/10/2019
 *  Description: This is the home page for the new settings menu. It contains buttons so users can pick what aspects of the programs settings they want to change.
 *  Each button opens a new form that will close back to this page.
 *  Purpose: To create a landing page for the settings to keep it from being too cluttered and hard to understand.
 */
    public partial class SettingsHome : Form
    {
        private static FormsEyeXHost eyeXHost;
        private Form1 form1;
        private GeneralSettingsForm genSettings;
        private ZoomSettingsForm zoomSettings;
        private SettingsCrosshairPage crossSettings;
        private SettingRearrangePage buttonArrangement;
        private SettingShortcutForm shortcutSettings;
        private keyboardSettings keyboardSettings;
        private ColourSettings colourSets;


        public SettingsHome(Form1 form1, FormsEyeXHost EyeXHost)
        {
            eyeXHost = EyeXHost;
            InitializeComponent();
            connectBehaveMap();
            this.form1 = form1;
            controlRelocateAndResize();
        }




        //Close Setting Window
        private void backButton_Click(object sender, EventArgs e)
        {
            List<String> selectedActions = new List<String>();
            Program.readSettings.createJSON(selectedActions.ToArray<string>());
            Close();
        }


        //Opens general Settings page
        private void generalButton_Click(object sender, EventArgs e)
        {
            genSettings = new GeneralSettingsForm(this,form1, eyeXHost);
            genSettings.Show();
        }

        private void zoomButton_Click(object sender, EventArgs e)
        {
            zoomSettings = new ZoomSettingsForm(this, form1, eyeXHost);
            zoomSettings.Show();
        }

        private void crossButton_Click(object sender, EventArgs e)
        {
            crossSettings = new SettingsCrosshairPage(this, form1, eyeXHost);
            crossSettings.Show();
        }

        private void arrangeButton_Click(object sender, EventArgs e)
        {

            buttonArrangement = new SettingRearrangePage(this, form1, eyeXHost);
            buttonArrangement.Show();

        }

        //Very important method. Do NOT delete!!!!!
        private Boolean returnTrue()
        {
            if(false)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        private void shortcutButton_Click(object sender, EventArgs e)
        {
            shortcutSettings = new SettingShortcutForm(this, form1, eyeXHost);
            shortcutSettings.Show();
        }



        private void KeyboardSettings_Click(object sender, EventArgs e)
        {
            keyboardSettings = new keyboardSettings(this, form1, eyeXHost);
            keyboardSettings.Show();
        }

        private void colourSettings_Click(object sender, EventArgs e)
        {
            colourSets = new ColourSettings(this, form1, eyeXHost);
            this.Hide();
            colourSets.Show();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Program.readSettings.defaultSettings();
        }

        private void controlRelocateAndResize()
        {
            int percentageSize = 400; //Higher number for smaller trackbars
            panelSaveAndCancel.Location = ReletiveSize.panelSaveAndCancel(panelSaveAndCancel.Width, panelSaveAndCancel.Height);
            ReletiveSize.sizeEvenly(panelSaveAndCancel,0.4);
            button1.Location = ReletiveSize.distribute(panelSaveAndCancel, button1.Location.Y, 1, 2, "wn", 0);
            backButton.Location = ReletiveSize.distribute(panelSaveAndCancel, button1.Location.Y, 2, 2, "wn", 0);
            ReletiveSize.resizeLabel(label1,20);
            homeButtons.Height = panelSaveAndCancel.Location.Y - 200;
            homeButtons.Width = Constants.SCREEN_SIZE.Width;
            homeButtons.Location = new Point(0, 200);
            //Console.WriteLine(homeButtons.Location.Y);
            topRow.Location = ReletiveSize.distribute(homeButtons, 100, 1, 2, "h", 0.3);
            topRow.Width = homeButtons.Width-200;
            bottomRow.Location = ReletiveSize.distribute(homeButtons, 100, 2, 2, "h", 0.3);
            bottomRow.Width = homeButtons.Width - 200;
            ReletiveSize.sizeEvenly(topRow, 0.5);
            ReletiveSize.sizeEvenly(bottomRow, 0.4);
            zoomPanel.Location = ReletiveSize.distribute(topRow, zoomPanel.Location.Y, 1, 3, "wn", 0.4);
            generalPanel.Location = ReletiveSize.distribute(topRow, generalPanel.Location.Y, 2, 3, "wn", 0.4);
            crossPanel.Location = ReletiveSize.distribute(topRow, crossPanel.Location.Y, 3, 3, "wn", 0.4);
            keyboardPanel.Location = ReletiveSize.distribute(bottomRow, keyboardPanel.Location.Y, 1, 4, "wn", 0.28);
            arrangePanel.Location = ReletiveSize.distribute(bottomRow, arrangePanel.Location.Y, 2, 4, "wn", 0.28);
            colourPanel.Location = ReletiveSize.distribute(bottomRow, colourPanel.Location.Y, 3, 4, "wn", 0.28);
            shortcutPanel.Location = ReletiveSize.distribute(bottomRow, shortcutPanel.Location.Y, 4, 4, "wn", 0.28);
            this.BackColor = Program.readSettings.mainColour;
            foreach (Control control in this.Controls)
            {
                control.BackColor = Program.readSettings.mainColour;
                control.ForeColor = Program.readSettings.secondColour;
            }

        }




        private void SettingsHome_Load(object sender, EventArgs e)
        {

        }

        private void SettingsHome_Paint(object sender, PaintEventArgs e)
        {
            this.BackColor = Program.readSettings.mainColour;
            foreach (Control control in this.Controls)
            {
                control.BackColor = Program.readSettings.mainColour;
                control.ForeColor = Program.readSettings.secondColour;
            }
            foreach (Panel panel in topRow.Controls)
            {
                foreach (Button button in panel.Controls)
                {
                    button.ForeColor = Program.readSettings.secondColour;
                }
            }
            foreach (Panel panel in bottomRow.Controls)
            {
                foreach (Button button in panel.Controls)
                {
                    button.ForeColor = Program.readSettings.secondColour;
                }
            }
            foreach (Panel panel in panelSaveAndCancel.Controls)
            {
                foreach (Button button in panel.Controls)
                {
                    button.ForeColor = Program.readSettings.secondColour;
                }
            }
        }

    }
}
