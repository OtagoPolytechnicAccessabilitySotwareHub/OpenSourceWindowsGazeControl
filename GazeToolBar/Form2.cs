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
        private static FormsEyeXHost eyeXHost;
        public Form2(FormsEyeXHost EyeXHost)
        {
            eyeXHost = EyeXHost;
            InitializeComponent();
            connectBehaveMap();
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

    }
}
