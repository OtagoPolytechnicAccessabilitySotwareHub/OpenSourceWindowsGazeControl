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

            this.BackColor = Color.Fuchsia;
            TransparencyKey = Color.Fuchsia;
            panel38.SendToBack();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            SendKeys.Send("z");
            this.Close();
            //this.Hide();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            SendKeys.Send("x");
        }

        private void button3_Click(object sender, EventArgs e)
        {
            SendKeys.Send("c");
        }

        private void button4_Click(object sender, EventArgs e)
        {
            SendKeys.Send("v");
        }

        private void button5_Click(object sender, EventArgs e)
        {
            SendKeys.Send("b");
        }

        private void button6_Click(object sender, EventArgs e)
        {
            SendKeys.Send("n");
        }

        private void button7_Click(object sender, EventArgs e)
        {
            SendKeys.Send("m");
        }

        private void button13_Click(object sender, EventArgs e)
        {
            SendKeys.Send("{ENTER}");
        }

        private void button21_Click(object sender, EventArgs e)
        {
            SendKeys.Send("a");
        }

        private void button24_Click(object sender, EventArgs e)
        {
            SendKeys.Send("s");
        }

        private void button23_Click(object sender, EventArgs e)
        {
            SendKeys.Send("d");
        }

        private void button22_Click(object sender, EventArgs e)
        {
            SendKeys.Send("f");
        }

        private void button19_Click(object sender, EventArgs e)
        {
            SendKeys.Send("g");
        }

        private void button18_Click(object sender, EventArgs e)
        {
            SendKeys.Send("h");
        }

        private void button17_Click(object sender, EventArgs e)
        {
            SendKeys.Send("j");
        }

        private void button16_Click(object sender, EventArgs e)
        {
            SendKeys.Send("k");
        }

        private void button15_Click(object sender, EventArgs e)
        {
            SendKeys.Send("l");
        }

        private void button33_Click(object sender, EventArgs e)
        {
            SendKeys.Send("q");
        }

        private void button36_Click(object sender, EventArgs e)
        {
            SendKeys.Send("w");
        }

        private void button35_Click(object sender, EventArgs e)
        {
            SendKeys.Send("e");
        }

        private void button34_Click(object sender, EventArgs e)
        {
            SendKeys.Send("r");
        }

        private void button31_Click(object sender, EventArgs e)
        {
            SendKeys.Send("t");
        }

        private void button30_Click(object sender, EventArgs e)
        {
            SendKeys.Send("y");
        }

        private void button29_Click(object sender, EventArgs e)
        {
            SendKeys.Send("u");
        }

        private void button28_Click(object sender, EventArgs e)
        {
            SendKeys.Send("i");
        }

        private void button27_Click(object sender, EventArgs e)
        {
            SendKeys.Send("o");
        }

        private void button26_Click(object sender, EventArgs e)
        {
            SendKeys.Send("p");
        }

        private void button8_Click(object sender, EventArgs e)
        {

        }

        private void button9_Click(object sender, EventArgs e)
        {

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
                panel38.Location = new Point(0, 650);
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

        private void button11_Click(object sender, EventArgs e)
        {

        }

        private void button12_Click(object sender, EventArgs e)
        {

        }

        private void button20_Click(object sender, EventArgs e)
        {

        }

        private void button32_Click(object sender, EventArgs e)
        {

        }

        private void button14_Click(object sender, EventArgs e)
        {

        }

        private void button25_Click(object sender, EventArgs e)
        {

        }

        private void panel38_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
