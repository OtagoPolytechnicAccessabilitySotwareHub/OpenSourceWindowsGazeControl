using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using EyeXFramework.Forms;

namespace GazeToolBar
{
    
    public partial class Form2 : Form
    {
        private bool bottom;
        private static FormsEyeXHost eyeXHost;
        public Form2(FormsEyeXHost EyeXHost)
        {
            eyeXHost = EyeXHost;
            InitializeComponent();
            connectBehaveMap();
            bottom = true;
        }

        private void Form2_Load(object sender, EventArgs e)
        {
            timer1.Enabled = true;
            this.BackColor = Color.Fuchsia;
            TransparencyKey = Color.Fuchsia;
            panel38.SendToBack();
            double panelWidth = Convert.ToDouble(Screen.PrimaryScreen.WorkingArea.Width);
            double newPanelWidth = panelWidth * 0.93;
            int intNewPanelWidth = Convert.ToInt32(newPanelWidth);
            panel38.Width = intNewPanelWidth;
            panel38.Left = 0;
            panel38.Top = (this.Height - panel38.Height - 50);
            /*foreach (Control control in panel38.Controls)
            {
                control.Size = new Size(Screen.PrimaryScreen.WorkingArea.Width / 13, Screen.PrimaryScreen.WorkingArea.Width / 13);
            }*/

        }


        private void button1_Click(object sender, EventArgs e)
        {
            SendKeys.Send("z");
            this.Close();
            //this.Hide();
        }

        private async void button2_ClickAsync(object sender, EventArgs e)
        {
            button2.BackColor = Color.Red;
            SendKeys.Send("x");

            await Task.Delay(500);
            button2.BackColor = Color.Black;

        }

        private async void button3_Click(object sender, EventArgs e)
        {
            button3.BackColor = Color.Red;
            SendKeys.Send("c");
            await Task.Delay(500);
            button3.BackColor = Color.Black;
        }

        private async void button4_Click(object sender, EventArgs e)
        {
            button4.BackColor = Color.Red;
            SendKeys.Send("v");
            await Task.Delay(500);
            button4.BackColor = Color.Black;
        }

        private async void button5_Click(object sender, EventArgs e)
        {
            button5.BackColor = Color.Red;
            SendKeys.Send("b");
            await Task.Delay(500);
            button5.BackColor = Color.Black;
        }

        private async void button6_Click(object sender, EventArgs e)
        {
            button6.BackColor = Color.Red;
            SendKeys.Send("n");
            await Task.Delay(500);
            button6.BackColor = Color.Black;
        }

        private async void button7_Click(object sender, EventArgs e)
        {
            button7.BackColor = Color.Red;
            SendKeys.Send("m");
            await Task.Delay(500);
            button7.BackColor = Color.Black;
        }

        private async void button13_Click(object sender, EventArgs e)
        {
            button13.BackColor = Color.Red;
            SendKeys.Send("{ENTER}");
            await Task.Delay(500);
            button13.BackColor = Color.Black;
        }

        private async void button21_Click(object sender, EventArgs e)
        {
            button21.BackColor = Color.Red;
            SendKeys.Send("a");
            await Task.Delay(500);
            button21.BackColor = Color.Black;
        }

        private async void button24_Click(object sender, EventArgs e)
        {
            button24.BackColor = Color.Red;
            SendKeys.Send("s");
            await Task.Delay(500);
            button24.BackColor = Color.Black;
        }

        private async void button23_Click(object sender, EventArgs e)
        {
            button23.BackColor = Color.Red;
            SendKeys.Send("d");
            await Task.Delay(500);
            button23.BackColor = Color.Black;
        }

        private async void button22_Click(object sender, EventArgs e)
        {
            button22.BackColor = Color.Red;
            SendKeys.Send("f");
            await Task.Delay(500);
            button22.BackColor = Color.Black;
        }

        private async void button19_Click(object sender, EventArgs e)
        {
            button19.BackColor = Color.Red;
            SendKeys.Send("g");
            await Task.Delay(500);
            button19.BackColor = Color.Black;
        }

        private async void button18_Click(object sender, EventArgs e)
        {
            button18.BackColor = Color.Red;
            SendKeys.Send("h");
            await Task.Delay(500);
            button18.BackColor = Color.Black;
        }

        private async void button17_Click(object sender, EventArgs e)
        {
            button17.BackColor = Color.Red;
            SendKeys.Send("j");
            await Task.Delay(500);
            button17.BackColor = Color.Black;
        }

        private async void button16_Click(object sender, EventArgs e)
        {
            button16.BackColor = Color.Red;
            SendKeys.Send("k");
            await Task.Delay(500);
            button16.BackColor = Color.Black;
        }

        private async void button15_Click(object sender, EventArgs e)
        {
            button15.BackColor = Color.Red;
            SendKeys.Send("l");
            await Task.Delay(500);
            button15.BackColor = Color.Black;
        }

        private async void button33_Click(object sender, EventArgs e)
        {
            button33.BackColor = Color.Red;
            SendKeys.Send("q");
            await Task.Delay(500);
            button33.BackColor = Color.Black;
        }

        private async void button36_Click(object sender, EventArgs e)
        {
            button36.BackColor = Color.Red;
            SendKeys.Send("w");
            await Task.Delay(500);
            button36.BackColor = Color.Black;
        }

        private async void button35_Click(object sender, EventArgs e)
        {
            button35.BackColor = Color.Red;
            SendKeys.Send("e");
            await Task.Delay(500);
            button35.BackColor = Color.Black;
        }

        private async void button34_Click(object sender, EventArgs e)
        {
            button34.BackColor = Color.Red;
            SendKeys.Send("r");
            await Task.Delay(500);
            button34.BackColor = Color.Black;
        }

        private async void button31_Click(object sender, EventArgs e)
        {
            button31.BackColor = Color.Red;
            SendKeys.Send("t");
            await Task.Delay(500);
            button31.BackColor = Color.Black;
        }

        private async void button30_Click(object sender, EventArgs e)
        {
            button30.BackColor = Color.Red;
            SendKeys.Send("y");
            await Task.Delay(500);
            button30.BackColor = Color.Black;
        }

        private async void button29_Click(object sender, EventArgs e)
        {
            button29.BackColor = Color.Red;
            SendKeys.Send("u");
            await Task.Delay(500);
            button29.BackColor = Color.Black;
        }

        private async void button28_Click(object sender, EventArgs e)
        {
            button28.BackColor = Color.Red;
            SendKeys.Send("i");
            await Task.Delay(500);
            button28.BackColor = Color.Black;
        }

        private async void button27_Click(object sender, EventArgs e)
        {
            button27.BackColor = Color.Red;
            SendKeys.Send("o");
            await Task.Delay(500);
            button27.BackColor = Color.Black;
        }

        private async void button26_Click(object sender, EventArgs e)
        {
            button26.BackColor = Color.Red;
            SendKeys.Send("p");
            await Task.Delay(500);
            button26.BackColor = Color.Black;
        }

        private async void button8_Click(object sender, EventArgs e)
        {
            button8.BackColor = Color.Red;
            SendKeys.Send(",");
            await Task.Delay(500);
            button8.BackColor = Color.Black;
        }

        private async void button9_ClickAsync(object sender, EventArgs e)
        {
            button9.BackColor = Color.Red;
            SendKeys.Send(".");
            await Task.Delay(500);
            button9.BackColor = Color.Black;
        }

        private void button10_Click(object sender, EventArgs e)
        {
            if (bottom == true)
            {
                bottom = false;
                //background panel
                panel38.Location = new Point(0, 0);
                /*top row
                panel32.Location = new Point(12, 12);
                panel33.Location = new Point(122, 12);
                panel36.Location = new Point(232, 12);
                panel35.Location = new Point(342, 12);
                panel34.Location = new Point(452, 12);
                panel31.Location = new Point(562, 12);
                panel30.Location = new Point(672, 12);
                panel29.Location = new Point(782, 12);
                panel28.Location = new Point(892, 12);
                panel27.Location = new Point(1002, 12);
                panel26.Location = new Point(1112, 12);
                panel25.Location = new Point(1222, 12);
                //middle row
                panel20.Location = new Point(12, 121);
                panel21.Location = new Point(122, 121);
                panel24.Location = new Point(232, 121);
                panel23.Location = new Point(342, 121);
                panel22.Location = new Point(452, 121);
                panel19.Location = new Point(562, 121);
                panel18.Location = new Point(672, 121);
                panel17.Location = new Point(782, 121);
                panel16.Location = new Point(892, 121);
                panel15.Location = new Point(1002, 121);
                panel12.Location = new Point(1112, 121);
                panel11.Location = new Point(1222, 121);
                //bottom row
                panel13.Location = new Point(12, 230);
                panel14.Location = new Point(122, 230);
                panel1.Location = new Point(232, 230);
                panel2.Location = new Point(342, 230);
                panel3.Location = new Point(452, 230);
                panel4.Location = new Point(562, 230);
                panel5.Location = new Point(672, 230);
                panel6.Location = new Point(782, 230);
                panel7.Location = new Point(892, 230);
                panel8.Location = new Point(1002, 230);
                panel9.Location = new Point(1112, 230);
                panel10.Location = new Point(1222, 230);
                */
            }
            else
            {
                bottom = true;
                //background panel
                panel38.Top = (this.Height - panel38.Height - 50);
                /*top row
                panel32.Location = new Point(12, 727);
                panel33.Location = new Point(122, 727);
                panel36.Location = new Point(232, 727);
                panel35.Location = new Point(342, 727);
                panel34.Location = new Point(452, 727);
                panel31.Location = new Point(562, 727);
                panel30.Location = new Point(672, 727);
                panel29.Location = new Point(782, 727);
                panel28.Location = new Point(892, 727);
                panel27.Location = new Point(1002, 727);
                panel26.Location = new Point(1112, 727);
                panel25.Location = new Point(1222, 727);
                //middle row
                panel20.Location = new Point(12, 836);
                panel21.Location = new Point(122, 836);
                panel24.Location = new Point(232, 836);
                panel23.Location = new Point(342, 836);
                panel22.Location = new Point(452, 836);
                panel19.Location = new Point(562, 836);
                panel18.Location = new Point(672, 836);
                panel17.Location = new Point(782, 836);
                panel16.Location = new Point(892, 836);
                panel15.Location = new Point(1002, 836);
                panel12.Location = new Point(1112, 836);
                panel11.Location = new Point(1222, 836);
                //bottom row
                panel13.Location = new Point(12, 945);
                panel14.Location = new Point(122, 945);
                panel1.Location = new Point(232, 945);
                panel2.Location = new Point(342, 945);
                panel3.Location = new Point(452, 945);
                panel4.Location = new Point(562, 945);
                panel5.Location = new Point(672, 945);
                panel6.Location = new Point(782, 945);
                panel7.Location = new Point(892, 945);
                panel8.Location = new Point(1002, 945);
                panel9.Location = new Point(1112, 945);
                panel10.Location = new Point(1222, 945);
                */
            }
        }

        private async void button11_ClickAsync(object sender, EventArgs e)
        {
            button11.BackColor = Color.Red;
            SendKeys.Send(" ");
            await Task.Delay(500);
            button11.BackColor = Color.Black;
        }

        private void button12_Click(object sender, EventArgs e)
        {

        }

        private async void button20_ClickAsync(object sender, EventArgs e)
        {
            button20.BackColor = Color.Red;
            SendKeys.Send("{CAPSLOCK}");
            await Task.Delay(500);
            button20.BackColor = Color.Black;
        }

        private void button32_Click(object sender, EventArgs e)
        {

        }

        private async void button14_ClickAsync(object sender, EventArgs e)
        {
            button14.BackColor = Color.Red;
            SendKeys.Send("{TAB}");
            await Task.Delay(500);
            button14.BackColor = Color.Black;
        }

        private async void button25_ClickAsync(object sender, EventArgs e)
        {
            button25.BackColor = Color.Red;
            SendKeys.Send("{BACKSPACE}");
            await Task.Delay(500);
            button25.BackColor = Color.Black;
        }

        private void panel38_Paint(object sender, PaintEventArgs e)
        {

        }

        private async void button37_ClickAsync(object sender, EventArgs e)
        {
            button37.BackColor = Color.Red;
            SendKeys.Send("q");
            await Task.Delay(500);
            button37.BackColor = Color.Black;
        }

        private void timer1_Tick(object sender, EventArgs e)
        {

        }
    }
}
