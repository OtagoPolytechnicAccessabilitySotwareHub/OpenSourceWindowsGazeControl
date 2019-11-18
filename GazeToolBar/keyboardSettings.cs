using EyeXFramework.Forms;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GazeToolBar
{
    public partial class keyboardSettings : Form
    {
        private SettingsHome home;
        private static FormsEyeXHost eyeXHost;
        private Form1 form1;
        private bool eng;
        private bool k123;
        private bool kacc;
        private bool autocomplete;
        private bool spanish;
        public keyboardSettings(SettingsHome home, Form1 form1, FormsEyeXHost EyeXHost)
        {
            eyeXHost = EyeXHost;
            InitializeComponent();
            this.home = home;
            this.form1 = form1;
            connectBehaveMap();
            changeButtonColour(Program.readSettings.eng, btnAbcOn, btnAbcOff);
            changeButtonColour(Program.readSettings.k123, btn123On, btn123Off);
            changeButtonColour(Program.readSettings.kacc, btnAccOn, btnAccOff);
            changeButtonColour(Program.readSettings.spanish, btnespOn, btnespOff);
            eng = Program.readSettings.eng;
            k123 = Program.readSettings.k123;
            kacc = Program.readSettings.kacc;
            autocomplete = Program.readSettings.autocomplete;
            spanish = Program.readSettings.spanish;
            controlRelocateAndResize();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                Program.readSettings.eng = eng;
                Program.readSettings.k123 = k123;
                Program.readSettings.kacc = kacc;
                Program.readSettings.spanish = spanish;
                Program.readSettings.autocomplete = autocomplete;

                //form1.NotifyIcon.BalloonTipTitle = "Saving success";
                //form1.NotifyIcon.BalloonTipText = "Your settings are successfuly saved";
                Program.readSettings.createJSON(Program.readSettings.sidebar);
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

        private void changeButtonColour(Boolean switchbool, Button on, Button off)
        {
            if(switchbool)
            {
                on.BackColor = Program.readSettings.iconColour;
                off.BackColor = Program.readSettings.mainColour;
                off.ForeColor = Program.readSettings.iconColour;
                on.ForeColor = Program.readSettings.mainColour;
            }
            else
            {
                off.BackColor = Program.readSettings.iconColour;
                on.BackColor = Program.readSettings.mainColour;
                on.ForeColor = Program.readSettings.iconColour;
                off.ForeColor = Program.readSettings.mainColour;

            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void btnAbcOn_Click(object sender, EventArgs e)
        {
            eng=true;
            changeButtonColour(eng, btnAbcOn, btnAbcOff);
        }

        private void btnAbcOff_Click(object sender, EventArgs e)
        {
            eng = false;
            changeButtonColour(eng, btnAbcOn, btnAbcOff);
        }

        private void btn123On_Click(object sender, EventArgs e)
        {
            k123 = true;
            changeButtonColour(eng, btn123On, btn123Off);
        }

        private void btn123Off_Click(object sender, EventArgs e)
        {
            k123 = false;
            changeButtonColour(k123, btn123On, btn123Off);
        }

        private void btnAccOn_Click(object sender, EventArgs e)
        {
            kacc = true;
            changeButtonColour(kacc, btnAccOn, btnAccOff);
        }

        private void btnAccOff_Click(object sender, EventArgs e)
        {
            kacc = false;
            changeButtonColour(kacc, btnAccOn, btnAccOff);
        }

        private void btnespOn_Click(object sender, EventArgs e)
        {
            spanish = true;
            changeButtonColour(spanish, btnespOn, btnespOff);
        }

        private void btnespOff_Click(object sender, EventArgs e)
        {
            spanish = false;
            changeButtonColour(spanish, btnespOn, btnespOff);
        }




        private void controlRelocateAndResize()
        {
            int percentageSize = 400; //Higher number for smaller trackbars
            panelSaveAndCancel.Location = ReletiveSize.panelSaveAndCancel(panelSaveAndCancel.Width, panelSaveAndCancel.Height);
            ReletiveSize.sizeEvenly(panelSaveAndCancel, 0.4);
            pnlSave.Location = ReletiveSize.distribute(panelSaveAndCancel, pnlSave.Location.Y, 1, 2, "wn", 0.5);
            pnlCancel.Location = ReletiveSize.distribute(panelSaveAndCancel, pnlSave.Location.Y, 2, 2, "wn", 0.7);
            panelKeyboard.Size = ReletiveSize.panelGeneralSize(panelSaveAndCancel.Location.Y, panelKeyboard.Location.Y);
            ReletiveSize.resizeLabel(label8,20);
            panelOn.Location = ReletiveSize.distribute(panelKeyboard, panelOn.Location.X, 2, 3, "h", 0);
            panelOff.Location = ReletiveSize.distribute(panelKeyboard, panelOff.Location.X, 3, 3, "h", 0);
            panelTitle.Location = new Point(panelOn.Left, (panelOn.Top - (panelTitle.Height)-20));
            panelTitle.Width = panelKeyboard.Width;
            panelOn.Width = panelKeyboard.Width;
            panelOff.Width = panelKeyboard.Width;

            ReletiveSize.resizeLabel(label1, 50);
            ReletiveSize.resizeLabel(label2, 50);
            ReletiveSize.resizeLabel(label3, 50);
            ReletiveSize.resizeLabel(label5, 50);
            label1.Location = ReletiveSize.distribute(panelTitle, label1.Location.Y, 2, 5, "wn", 0.15);
            label2.Location = ReletiveSize.distribute(panelTitle, label1.Location.Y, 3, 5, "wn", 0.15);
            label3.Location = ReletiveSize.distribute(panelTitle, label1.Location.Y, 4, 5, "wn", 0.15);
            label5.Location = ReletiveSize.distribute(panelTitle, label1.Location.Y, 5, 5, "wn", 0.15);


            ReletiveSize.resizeLabel(label6, 50);
            ReletiveSize.sizeEvenly(panelOn, 0.7);
            label6.Location = ReletiveSize.distribute(panelOn, label6.Location.Y, 1, 5, "wn", 0.15);
            pnlAbcOn.Location = ReletiveSize.distribute(panelOn, pnlAbcOn.Location.Y, 2, 5, "wn", 0.15);
            pnl123On.Location = ReletiveSize.distribute(panelOn, pnl123On.Location.Y, 3, 5, "wn", 0.15);
            pnlAccOn.Location = ReletiveSize.distribute(panelOn, pnlAccOn.Location.Y, 4, 5, "wn", 0.15);
            pnlespOn.Location = ReletiveSize.distribute(panelOn, pnlespOn.Location.Y, 5, 5, "wn", 0.15);

            ReletiveSize.resizeLabel(label7, 50);
            ReletiveSize.sizeEvenly(panelOff, 0.7);
            label7.Location = ReletiveSize.distribute(panelOff, label7.Location.Y, 1, 5, "wn", 0.15);
            pnlAbcOff.Location = ReletiveSize.distribute(panelOff, pnlAbcOff.Location.Y, 2, 5, "wn", 0.15);
            pnl123Off.Location = ReletiveSize.distribute(panelOff, pnl123Off.Location.Y, 3, 5, "wn", 0.15);
            pnlAccOff.Location = ReletiveSize.distribute(panelOff, pnlAccOff.Location.Y, 4, 5, "wn", 0.15);
            pnlespOff.Location = ReletiveSize.distribute(panelOff, pnlespOff.Location.Y, 5, 5, "wn", 0.15);
            foreach (Panel panel in panelSaveAndCancel.Controls)
            {
                foreach (Button button in panel.Controls)
                {
                    button.ForeColor = Program.readSettings.iconColour;
                }
            }
            this.BackColor = Program.readSettings.mainColour;
            label6.ForeColor = Program.readSettings.iconColour;
            label7.ForeColor = Program.readSettings.iconColour;
            label8.ForeColor = Program.readSettings.iconColour;
            foreach (Label label in panelTitle.Controls)
            {
                label.ForeColor = Program.readSettings.iconColour;
            }


        }




        private Boolean returnTrue(Boolean False)
        {
            if (False)
            {
                return false;
            }
            else
            {
                return true;
            }
        }


    }
}
