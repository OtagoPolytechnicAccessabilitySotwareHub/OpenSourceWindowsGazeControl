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
        int FlashDelay = 500; //delay on making button flash
        bool alpha = true; //is onscreen keyboard letters or numbers
        private bool bottom; //for location of keyboard. top or bottom
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
            TransparencyKey = Color.Fuchsia; //form is maximised and transparent
            panel38.SendToBack();
            //double panelWidth = Convert.ToDouble(Screen.PrimaryScreen.WorkingArea.Width); 
            double panelWidth = Convert.ToDouble(ClientSize.Width); 
            double newPanelWidth = panelWidth * 0.93;
            int intNewPanelWidth = Convert.ToInt32(newPanelWidth);
            panel38.Width = intNewPanelWidth;
            panel38.Left = 0;
            panel38.Height = Convert.ToInt32(ClientSize.Height * 0.3147) + 75;
            panel38.Top = (this.Height - panel38.Height - 50); //for changing position of keyboard, -50 is a rough equivilent of taskbar height

            int count = 0;
            int count2 = 0;

            foreach (Control control in panel38.Controls)
            {
                control.AutoSize = false;
                control.Size = new Size(intNewPanelWidth / 12, intNewPanelWidth / 12);
                //control.Location = (new Point(1, 1));
                Console.WriteLine(control.Name + " " + control.Width + " " + control.Height +" "+ control.Location);
                control.Location = (new Point(count, count2));
                count= count + intNewPanelWidth / 12;
                if(count > intNewPanelWidth-(intNewPanelWidth / 12))
                    {
                    count2= count2+ intNewPanelWidth / 12;
                    count = 0;
                }
                


                foreach (Button button  in control.Controls.OfType<Button>())
                {
                    button.Size = new Size(intNewPanelWidth / 12, intNewPanelWidth / 12);
                    Console.WriteLine(button.Name + " " + button.Width + " " + button.Height + " " + button.Location);
                }
             

            }

        }


        private async void button1_Click(object sender, EventArgs e) 
        {
            button1.BackColor = Color.Red; //set background color when clicked
            if(alpha == true) //sends key based on what keyboard is currently active
            {
                SendKeys.Send("z");
            }
            else
            {
                SendKeys.Send(":");
            }

            await Task.Delay(FlashDelay); //delay before deactivting button flash
            button1.BackColor = Color.Black; //revert back to original color
        }

        private async void button2_ClickAsync(object sender, EventArgs e)
        {
            button2.BackColor = Color.Red;
            if (alpha == true)
            {
                SendKeys.Send("x");
            }
            else
            {
                SendKeys.Send(";");
            }

            await Task.Delay(FlashDelay);
            button2.BackColor = Color.Black;

        }

        private async void button3_Click(object sender, EventArgs e)
        {
            button3.BackColor = Color.Red;
            if (alpha == true)
            {
                SendKeys.Send("c");
            }
            else
            {
                SendKeys.Send("\"");
            }
            await Task.Delay(FlashDelay);
            button3.BackColor = Color.Black;
        }

        private async void button4_Click(object sender, EventArgs e)
        {
            button4.BackColor = Color.Red;
            if (alpha == true)
            {
                SendKeys.Send("v");
            }
            else
            {
                SendKeys.Send("\\");
            }
            await Task.Delay(FlashDelay);
            button4.BackColor = Color.Black;
        }

        private async void button5_Click(object sender, EventArgs e)
        {
            button5.BackColor = Color.Red;
            if (alpha == true)
            {
                SendKeys.Send("b");
            }
            else
            {
                SendKeys.Send("/");
            }
            await Task.Delay(FlashDelay);
            button5.BackColor = Color.Black;
        }

        private async void button6_Click(object sender, EventArgs e)
        {
            button6.BackColor = Color.Red;
            if (alpha == true)
            {
                SendKeys.Send("n");
            }
            else
            {
                SendKeys.Send(">");
            }
            await Task.Delay(FlashDelay);
            button6.BackColor = Color.Black;
        }

        private async void button7_Click(object sender, EventArgs e)
        {
            button7.BackColor = Color.Red;
            if (alpha == true)
            {
                SendKeys.Send("m");
            }
            else
            {
                SendKeys.Send("<");
            }
            await Task.Delay(FlashDelay);
            button7.BackColor = Color.Black;
        }

        private async void button13_Click(object sender, EventArgs e)
        {
            button13.BackColor = Color.Red;
            if (alpha == true)
            {
                SendKeys.Send("{ENTER}");
            }
            else
            {
                SendKeys.Send("+");
            }
            await Task.Delay(FlashDelay);
            button13.BackColor = Color.Black;
        }

        private async void button21_Click(object sender, EventArgs e)
        {
            button21.BackColor = Color.Red;
            if (alpha == true)
            {
                SendKeys.Send("a");
            }
            else
            {
                SendKeys.Send("@");
            }
            await Task.Delay(FlashDelay);
            button21.BackColor = Color.Black;
        }

        private async void button24_Click(object sender, EventArgs e)
        {
            button24.BackColor = Color.Red;
            if (alpha == true)
            {
                SendKeys.Send("s");
            }
            else
            {
                SendKeys.Send("#");
            }
            await Task.Delay(FlashDelay);
            button24.BackColor = Color.Black;
        }

        private async void button23_Click(object sender, EventArgs e)
        {
            button23.BackColor = Color.Red;
            if (alpha == true)
            {
                SendKeys.Send("d");
            }
            else
            {
                SendKeys.Send("$");
            }
            await Task.Delay(FlashDelay);
            button23.BackColor = Color.Black;
        }

        private async void button22_Click(object sender, EventArgs e)
        {
            button22.BackColor = Color.Red;
            if (alpha == true)
            {
                SendKeys.Send("f");
            }
            else
            {
                SendKeys.Send("%");
            }
            await Task.Delay(FlashDelay);
            button22.BackColor = Color.Black;
        }

        private async void button19_Click(object sender, EventArgs e)
        {
            button19.BackColor = Color.Red;
            if (alpha == true)
            {
                SendKeys.Send("g");
            }
            else
            {
                SendKeys.Send("^");
            }
            await Task.Delay(FlashDelay);
            button19.BackColor = Color.Black;
        }

        private async void button18_Click(object sender, EventArgs e)
        {
            button18.BackColor = Color.Red;
            if (alpha == true)
            {
                SendKeys.Send("h");
            }
            else
            {
                SendKeys.Send("&");
            }
            await Task.Delay(FlashDelay);
            button18.BackColor = Color.Black;
        }

        private async void button17_Click(object sender, EventArgs e)
        {
            button17.BackColor = Color.Red;
            if (alpha == true)
            {
                SendKeys.Send("j");
            }
            else
            {
                SendKeys.Send("*");
            }
            await Task.Delay(FlashDelay);
            button17.BackColor = Color.Black;
        }

        private async void button16_Click(object sender, EventArgs e)
        {
            button16.BackColor = Color.Red;
            if (alpha == true)
            {
                SendKeys.Send("k");
            }
            else
            {
                SendKeys.Send("{(}");
            }
            await Task.Delay(FlashDelay);
            button16.BackColor = Color.Black;
        }

        private async void button15_Click(object sender, EventArgs e)
        {
            button15.BackColor = Color.Red;
            if (alpha == true)
            {
                SendKeys.Send("l");
            }
            else
            {
                SendKeys.Send("{)}");
            }
            await Task.Delay(FlashDelay);
            button15.BackColor = Color.Black;
        }

        private async void button33_Click(object sender, EventArgs e)
        {
            button33.BackColor = Color.Red;
            if (alpha == true)
            {
                SendKeys.Send("q");
            }
            else
            {
                SendKeys.Send("1");
            }
            await Task.Delay(FlashDelay);
            button33.BackColor = Color.Black;
        }

        private async void button36_Click(object sender, EventArgs e)
        {
            button36.BackColor = Color.Red;
            if (alpha == true)
            {
                SendKeys.Send("w");
            }
            else
            {
                SendKeys.Send("2");
            }
            await Task.Delay(FlashDelay);
            button36.BackColor = Color.Black;
        }

        private async void button35_Click(object sender, EventArgs e)
        {
            button35.BackColor = Color.Red;
            if (alpha == true)
            {
                SendKeys.Send("e");
            }
            else
            {
                SendKeys.Send("3");
            }
            await Task.Delay(FlashDelay);
            button35.BackColor = Color.Black;
        }

        private async void button34_Click(object sender, EventArgs e)
        {
            button34.BackColor = Color.Red;
            if (alpha == true)
            {
                SendKeys.Send("r");
            }
            else
            {
                SendKeys.Send("4");
            }
            await Task.Delay(FlashDelay);
            button34.BackColor = Color.Black;
        }

        private async void button31_Click(object sender, EventArgs e)
        {
            button31.BackColor = Color.Red;
            if (alpha == true)
            {
                SendKeys.Send("t");
            }
            else
            {
                SendKeys.Send("5");
            }
            await Task.Delay(FlashDelay);
            button31.BackColor = Color.Black;
        }

        private async void button30_Click(object sender, EventArgs e)
        {
            button30.BackColor = Color.Red;
            if (alpha == true)
            {
                SendKeys.Send("y");
            }
            else
            {
                SendKeys.Send("6");
            }
            await Task.Delay(FlashDelay);
            button30.BackColor = Color.Black;
        }

        private async void button29_Click(object sender, EventArgs e)
        {
            button29.BackColor = Color.Red;
            if (alpha == true)
            {
                SendKeys.Send("u");
            }
            else
            {
                SendKeys.Send("7");
            }
            await Task.Delay(FlashDelay);
            button29.BackColor = Color.Black;
        }

        private async void button28_Click(object sender, EventArgs e)
        {
            button28.BackColor = Color.Red;
            if (alpha == true)
            {
                SendKeys.Send("i");
            }
            else
            {
                SendKeys.Send("8");
            }
            await Task.Delay(FlashDelay);
            button28.BackColor = Color.Black;
        }

        private async void button27_Click(object sender, EventArgs e)
        {
            button27.BackColor = Color.Red;
            if (alpha == true)
            {
                SendKeys.Send("o");
            }
            else
            {
                SendKeys.Send("9");
            }
            await Task.Delay(FlashDelay);
            button27.BackColor = Color.Black;
        }

        private async void button26_Click(object sender, EventArgs e)
        {
            button26.BackColor = Color.Red;
            if (alpha == true)
            {
                SendKeys.Send("p");
            }
            else
            {
                SendKeys.Send("?");
            }
            await Task.Delay(FlashDelay);
            button26.BackColor = Color.Black;
        }

        private async void button8_Click(object sender, EventArgs e)
        {
            button8.BackColor = Color.Red;
            if (alpha == true)
            {
                SendKeys.Send(",");
            }
            else
            {
                SendKeys.Send("~");
            }
            await Task.Delay(FlashDelay);
            button8.BackColor = Color.Black;
        }

        private async void button9_ClickAsync(object sender, EventArgs e)
        {
            button9.BackColor = Color.Red;
            if (alpha == true)
            {
                SendKeys.Send(".");
            }
            else
            {
                SendKeys.Send("|");
            }
            await Task.Delay(FlashDelay);
            button9.BackColor = Color.Black;
        }

        private void button10_Click(object sender, EventArgs e)
        {
            if (bottom == true)
            {
                bottom = false;
                //background panel
                panel38.Location = new Point(0, 0);
                
            }
            else
            {
                bottom = true;
                //background panel
                panel38.Top = (this.Height - panel38.Height - 50);
                
            }
        }

        private async void button11_ClickAsync(object sender, EventArgs e)
        {
            button11.BackColor = Color.Red;
            if (alpha == true)
            {
                SendKeys.Send(" ");
            }
            else
            {
                SendKeys.Send("_");
            }
            await Task.Delay(FlashDelay);
            button11.BackColor = Color.Black;
        }

        private async void button12_ClickAsync(object sender, EventArgs e)
        {
            button12.BackColor = Color.Red;
            if (alpha == true)
            {
                SendKeys.Send("{CTRL}");
            }
            else
            {
                SendKeys.Send("_");
            }
            await Task.Delay(FlashDelay);
            button12.BackColor = Color.Black;
        }

        private async void button20_ClickAsync(object sender, EventArgs e)
        {
            button20.BackColor = Color.Red;
            if (alpha == true)
            {
                SendKeys.Send("{CAPSLOCK}");
            }
            else
            {
                SendKeys.Send("!");
            }
            await Task.Delay(FlashDelay);
            button20.BackColor = Color.Black;
        }

        private void button32_Click(object sender, EventArgs e)
        {

        }

        private async void button14_ClickAsync(object sender, EventArgs e)
        {
            button14.BackColor = Color.Red;
            if (alpha == true)
            {
                SendKeys.Send("{TAB}");
            }
            else
            {
                SendKeys.Send("-");
            }
            await Task.Delay(FlashDelay);
            button14.BackColor = Color.Black;
        }

        private async void button25_ClickAsync(object sender, EventArgs e)
        {
            button25.BackColor = Color.Red;
            if (alpha == true)
            {
                SendKeys.Send("{BACKSPACE}");
            }
            else
            {
                SendKeys.Send("{BACKSPACE}");
            }
            await Task.Delay(FlashDelay);
            button25.BackColor = Color.Black;
        }

        private void panel38_Paint(object sender, PaintEventArgs e)
        {

        }

        private async void button37_ClickAsync(object sender, EventArgs e)
        {

            if(alpha == false) //if letter keyboard is not onscreen
            {
                button37.Text = "123";
                button33.Text = "q";
                button36.Text = "w";
                button35.Text = "e";
                button34.Text = "r";
                button31.Text = "t";
                button30.Text = "y";
                button29.Text = "u";
                button28.Text = "i";
                button27.Text = "o";
                button26.Text = "p";
                button25.Text = "⌫";
                //line 2
                button20.Text = "⇧";
                button21.Text = "a";
                button24.Text = "s";
                button23.Text = "d";
                button22.Text = "f";
                button19.Text = "g";
                button18.Text = "h";
                button17.Text = "j";
                button16.Text = "k";
                button15.Text = "l";
                button14.Text = "Tab";
                button13.Text = "Enter";
                //line3
                button12.Text = "Ctrl";
                button11.Text = "⎵";
                button1.Text = "z";
                button2.Text = "x";
                button3.Text = "c";
                button4.Text = "v";
                button5.Text = "b";
                button6.Text = "n";
                button7.Text = "m";
                button8.Text = ",";
                button9.Text = ".";
                button10.Text = "DOWN";

                alpha = true;

            }
            else 
            {
                button37.Text = "abc";
                button33.Text = "1";
                button36.Text = "2";
                button35.Text = "3";
                button34.Text = "4";
                button31.Text = "5";
                button30.Text = "6";
                button29.Text = "7";
                button28.Text = "8";
                button27.Text = "9";
                button26.Text = "?";
                button25.Text = "⌫";
                //line 2
                button20.Text = "!";
                button21.Text = "@";
                button24.Text = "#";
                button23.Text = "$";
                button22.Text = "%";
                button19.Text = "^";
                button18.Text = "&&";
                button17.Text = "*";
                button16.Text = "(";
                button15.Text = ")";
                button14.Text = "-";
                button13.Text = "+";
                //line3
                button12.Text = "=";
                button11.Text = "_";
                button1.Text = ":";
                button2.Text = ";";
                button3.Text = "\"";
                button4.Text = "\\";
                button5.Text = "/";
                button6.Text = ">";
                button7.Text = "<";
                button8.Text = "~";
                button9.Text = "|";
                button10.Text = "DOWN";

                alpha = false;
            }

            await Task.Delay(FlashDelay);
            button37.BackColor = Color.Black;

        }

        private void timer1_Tick(object sender, EventArgs e)
        {

        }
    }
}
