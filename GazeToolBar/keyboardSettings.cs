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
            changeButtonColour(Program.readSettings.eng, btnAbcOn, btnAbcOff);
            changeButtonColour(Program.readSettings.k123, btn123On, btn123Off);
            changeButtonColour(Program.readSettings.kacc, btnAccOn, btnAccOff);
            changeButtonColour(Program.readSettings.spanish, btnespOn, btnespOff);
            changeButtonColour(Program.readSettings.autocomplete, btnAutoOn, btnAutoOff);
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
                on.BackColor = Color.White;
                off.BackColor = Color.Black;
            }
            else
            {
                off.BackColor = Color.White;
                on.BackColor = Color.Black;
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void btnAbcOn_Click(object sender, EventArgs e)
        {
            eng=returnTrue(eng);
            changeButtonColour(eng, btnAbcOn, btnAbcOff);
        }

        private void btnAbcOff_Click(object sender, EventArgs e)
        {
            eng = returnTrue(eng);
            changeButtonColour(eng, btnAbcOn, btnAbcOff);
        }

        private void btn123On_Click(object sender, EventArgs e)
        {
            k123 = returnTrue(k123);
            changeButtonColour(eng, btn123On, btn123Off);
        }

        private void btn123Off_Click(object sender, EventArgs e)
        {
            k123 = returnTrue(k123);
            changeButtonColour(k123, btn123On, btn123Off);
        }

        private void btnAccOn_Click(object sender, EventArgs e)
        {
            kacc = returnTrue(kacc);
            changeButtonColour(kacc, btnAccOn, btnAccOff);
        }

        private void btnAccOff_Click(object sender, EventArgs e)
        {
            kacc = returnTrue(kacc);
            changeButtonColour(kacc, btnAccOn, btnAccOff);
        }

        private void btnespOn_Click(object sender, EventArgs e)
        {
            spanish = returnTrue(spanish);
            changeButtonColour(spanish, btnespOn, btnespOff);
        }

        private void btnespOff_Click(object sender, EventArgs e)
        {
            spanish = returnTrue(spanish);
            changeButtonColour(spanish, btnespOn, btnespOff);
        }

        private void btnAutoOn_Click(object sender, EventArgs e)
        {
            autocomplete = returnTrue(autocomplete);
            changeButtonColour(autocomplete, btnAutoOn, btnAutoOff);
        }

        private void btnAutoOff_Click(object sender, EventArgs e)
        {
            autocomplete = returnTrue(autocomplete);
            changeButtonColour(autocomplete, btnAutoOn, btnAutoOff);
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
