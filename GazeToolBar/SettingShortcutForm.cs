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
    public partial class SettingShortcutForm : Form
    {
        private SettingsHome home;
        private static FormsEyeXHost eyeXHost;
        private Form1 form1;
        private bool WaitForUserKeyPress;
        public SettingShortcutForm(SettingsHome home, Form1 form1, FormsEyeXHost EyeXHost)
        {
            eyeXHost = EyeXHost;
            InitializeComponent();
            connectBehaveMap();
            this.home = home;
            this.form1 = form1;
            controlRelocateAndResize();
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
            ReletiveSize.evenlyDistrubute(panelSaveAndCancel);
            ReletiveSize.resizeLabel(label1,20);
            //Panel confirm defaults

            //Shortcut settings panel
            pnlPageKeyboard.Width = Constants.SCREEN_SIZE.Width;// - 20;
            pnlPageKeyboard.Size = ReletiveSize.panelGeneralSize(panelSaveAndCancel.Location.Y, pnlPageKeyboard.Location.Y);
            pnlLeftClick.Location = ReletiveSize.distribute(pnlPageKeyboard, pnlLeftClick.Location.Y, 1, 4, "wn", 0.25);
            pnlRightClick.Location = ReletiveSize.distribute(pnlPageKeyboard, pnlRightClick.Location.Y, 2, 4, "wn", 0.25);
            pnlDoubleClick.Location = ReletiveSize.distribute(pnlPageKeyboard, pnlDoubleClick.Location.Y, 3, 4, "wn", 0.25);
            pnlScroll.Location = ReletiveSize.distribute(pnlPageKeyboard, pnlScroll.Location.Y, 4, 4, "wn", 0.25);
            foreach (Panel panel in panelSaveAndCancel.Controls)
            {
                foreach (Button button in panel.Controls)
                {
                    button.ForeColor = Program.readSettings.secondColour;
                }
            }
            this.BackColor = Program.readSettings.mainColour;
            label1.ForeColor = Program.readSettings.secondColour;
            btClearFKeyDoubleClick.ForeColor = Program.readSettings.secondColour;
            btClearFKeyLeftClick.ForeColor = Program.readSettings.secondColour;
            btClearFKeyRightClick.ForeColor = Program.readSettings.secondColour;
            btClearFKeyScroll.ForeColor = Program.readSettings.secondColour;
            btFKeyDoubleClick.ForeColor = Program.readSettings.secondColour;
            btFKeyLeftClick.ForeColor = Program.readSettings.secondColour;
            btFKeyRightClick.ForeColor = Program.readSettings.secondColour;
            btFKeyScroll.ForeColor = Program.readSettings.secondColour;
            btnCancel.ForeColor = Program.readSettings.secondColour;
            
        }
    }
}
