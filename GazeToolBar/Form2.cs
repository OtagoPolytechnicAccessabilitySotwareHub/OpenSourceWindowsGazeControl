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

        private const int KeyboardAmount = 4;
        private bool cap;
        private int KeyboardView;
        private string[] button1x;
        private string[] button2x;
        private string[] button3x;
        private string[] button4x;
        private string[] button5x;
        private string[] button6x;
        private string[] button7x;
        private string[] button8x;
        private string[] button9x;
        private string[] button10x;
        private string[] button11x;
        private string[] button12x;
        private string[] button13x;
        private string[] button14x;
        private string[] button15x;
        private string[] button16x;
        private string[] button17x;
        private string[] button18x;
        private string[] button19x;
        private string[] button20x;
        private string[] button21x;
        private string[] button22x;
        private string[] button23x;
        private string[] button24x;
        private string[] button25x;
        private string[] button26x;
        private string[] button27x;
        private string[] button28x;
        private string[] button29x;
        private string[] button30x;
        private string[] button31x;
        private string[] button32x;
        private string[] button33x;
        private string[] button34x;
        private string[] button35x;
        private string[] button36x;
        private string[] button37x;


        public Form2(FormsEyeXHost EyeXHost)
        {
            eyeXHost = EyeXHost;
            InitializeComponent();
            connectBehaveMap();
            bottom = true;
            cap = false;
        }

        private void Form2_Load(object sender, EventArgs e)
        {
            KeyboardView = 0;
            timer1.Enabled = true;
            this.BackColor = Color.Fuchsia;
            TransparencyKey = Color.Fuchsia; //form is maximised and transparent
            panel38.SendToBack();
            //double panelWidth = Convert.ToDouble(Screen.PrimaryScreen.WorkingArea.Width); 
            double panelWidth = Convert.ToDouble(System.Windows.SystemParameters.WorkArea.Width); 
            double newPanelWidth = panelWidth;
            int intNewPanelWidth = Convert.ToInt32(newPanelWidth);
            panel38.Width = (intNewPanelWidth);
            panel38.Left = Convert.ToInt32(System.Windows.SystemParameters.WorkArea.Left);//(intNewPanelWidth / 24);

            panel38.Height = panel38.Width / 4 + 4;
            panel38.Top = (ClientSize.Height - panel38.Height); //for changing position of keyboard, -50 is a rough equivilent of taskbar height


            if (IsTaskbarVisible())
            {
                int taskBHeight = Convert.ToInt32(Math.Abs(System.Windows.SystemParameters.PrimaryScreenHeight - System.Windows.SystemParameters.WorkArea.Height));
                panel38.Top = (ClientSize.Height - panel38.Height - taskBHeight);
            }

            int count = 0;
            int count2 = 0;

            Console.WriteLine();
            

            button1x = new String[] { "123", "123", "abc", "abc" }; // ~~~~Variable changer
            button2x = new String[] {"q","Q", "1" ,"C"}; //Panel 13
            button3x = new String[] {"w", "W", "2", "l" };
            button4x = new String[] { "e", "E", "3", "a" };
            button5x = new String[] { "r", "R", "4", "i" };
            button6x = new String[] { "t", "T", "5", "r" };
            button7x = new String[] { "y", "Y", "6", "e" };
            button8x = new String[] { "u", "U", "7", "C" };
            button9x = new String[] { "i", "I", "8", "l" };
            button10x = new String[] { "o", "O", "9", "a" };
            button11x = new String[] { "p", "P", "0", "i" };
            //button12x = new String[] { "{BACKSPACE}", "{BACKSPACE}", "{BACKSPACE}", "{BACKSPACE}" };//Backspace ~~~~~~~Different Maybe
            //button13x = new String[] { "{SHIFT}", "{SHIFT}", "{SHIFT}", "{SHIFT}" };// button SHIFT ATM leave
            button14x = new String[] { "a", "A", "@", "r" };
            button15x = new String[] { "s", "S", "#", "e" };
            button16x = new String[] { "d", "D", "$", "C" };
            button17x = new String[] { "f", "F", "%", "l" };
            button18x = new String[] { "g", "G", "^", "a" };
            button19x = new String[] { "h", "H", "&", "i" };
            //button20x = new String[] { "i", "J", "*", "r" };
            button21x = new String[] { "j", "J", "{(}", "e" };
            button22x = new String[] { "k", "K", "{)}", "C" };
            button23x = new String[] { "l", "L", "<", "l" };
            //button24x = new String[] { "{TAB}", "{TAB}", "{TAB}", "{TAB}" };//Tab
            //button25x = new String[] { "{ENTER}", "{ENTER}", "{ENTER}", "{ENTER}" };//enter
            //button26x = new String[] { "{CTRL}", "{CTRL}", "{CTRL}", "{CTRL}" };//ctrl
            //button27x = new String[] { " ", " ", " ", " " };
            button28x = new String[] { "z", "Z", "-", "a" };
            button29x = new String[] { "x", "X", "+", "i" };
            button30x = new String[] { "c", "C", "=", "r" };
            button31x = new String[] { "v", "V", "<", "e" };
            button32x = new String[] { "b", "B", ">", "C" };
            button33x = new String[] { "n", "N", ";", "l" };
            button34x = new String[] { "m", "M", ":", "a" };
            //button35x = new String[] { ",", ",", ",", "," };//comma
            //button36x = new String[] { ".", ".", ".", "." };//dot
            //button37x = new String[] { "up", "down" };//movekeyboard


            
            button13.Text = button2x[KeyboardView];

            int countbutton = 0;

            foreach (Control control in panel38.Controls)
                        {
                            control.AutoSize = false;
                            control.Size = new Size(panel38.Width / 12, panel38.Width / 12);
                            //control.Location = (new Point(1, 1));
                            //Console.WriteLine(control.Name + " " + control.Width + " " + control.Height +" "+ control.Location);
                            control.Location = (new Point(count, count2 + 4));
                            count= count + panel38.Width / 12;
                            if(count > panel38.Width - (panel38.Width / 12))
                                {
                                count2= count2+ panel38.Width / 12;
                                count = 0;
                            }
                


                            foreach (Button button in control.Controls.OfType<Button>())
                            {
                                button.FlatStyle = FlatStyle.Flat;
                                button.FlatAppearance.BorderSize = 0;
                                button.Size = new Size(panel38.Width / 12 - 4, panel38.Width / 12 - 4);
                                button.Location = new Point(2,2);
                                Console.WriteLine(button.Name + " " + button.Width + " " + button.Height + " " + button.Location);
                                countbutton++;
                            }
             

                        }

            rename_buttons();
            button28.Text = "⌫";
            button8.Text = "⇧";
            button36.Text = "Tab";
            button34.Text = "Enter";
            button6.Text = "Ctrl";
            button18.Text = "⎵";
            button22.Text = ".";
            button3.Text = ",";


        }

        private void rename_buttons()
        {
            string k = button21x[KeyboardView].Replace("{","");
             k = k.Replace("}", "");
            string j =  button22x[KeyboardView].Replace("{", "");
             j =j.Replace("}", "");

            button10.Text = button1x[KeyboardView];
            button13.Text = button2x[KeyboardView];
            button25.Text = button3x[KeyboardView];
            button37.Text = button4x[KeyboardView];
            button26.Text = button5x[KeyboardView];
            button14.Text = button6x[KeyboardView];
            button9.Text = button7x[KeyboardView];
            button20.Text = button8x[KeyboardView];
            button27.Text = button9x[KeyboardView];
            button12.Text = button10x[KeyboardView];
            button15.Text = button11x[KeyboardView];
            //button28.Text = "⌫";
            //button8.Text = "⇧";
            button11.Text = button14x[KeyboardView];
            button29.Text = button15x[KeyboardView];
            button16.Text = button16x[KeyboardView];
            button33.Text = button17x[KeyboardView];
            button30.Text = button18x[KeyboardView];
            button7.Text = button19x[KeyboardView];
            //button7.Text = button20x[KeyboardView];
            button21.Text = k;
            button17.Text = j;
            button31.Text = button23x[KeyboardView];
            //button36.Text = "Tab";      
            //button34.Text = "Enter";               
            //button6.Text = "Ctrl";                  
            //button18.Text = "⎵";  
            button24.Text = button28x[KeyboardView];
            button35.Text = button29x[KeyboardView];
            button1.Text = button30x[KeyboardView];
            button19.Text = button31x[KeyboardView];
            button5.Text = button32x[KeyboardView];
            button23.Text = button33x[KeyboardView];
            button2.Text = button34x[KeyboardView];
            //button22.Text = ".";
            //button3.Text = ",";
            //button4.Text = button37x[KeyboardView];
        }

        public static bool IsTaskbarVisible()
        {
            return (Math.Abs(System.Windows.SystemParameters.PrimaryScreenHeight - System.Windows.SystemParameters.WorkArea.Height) > 0);
        }


        private async void button1_Click(object sender, EventArgs e) 
        {

            //if(alpha == true) //sends key based on what keyboard is currently active
            //{
            //    SendKeys.Send("z");
            //}
            //else
            //{
            //    SendKeys.Send(":");
            //}
            SendKeys.Send(button30x[KeyboardView]);
            button1.BackColor = Color.Cyan; //set background color when clicked
            await Task.Delay(FlashDelay); //delay before deactivting button flash
            button1.BackColor = Color.Black; //revert back to original color
        }

        private async void button2_ClickAsync(object sender, EventArgs e)
        {
            button2.BackColor = Color.Cyan;
            SendKeys.Send(button34x[KeyboardView]);
            await Task.Delay(FlashDelay);
            button2.BackColor = Color.Black;

        }

        private async void button3_Click(object sender, EventArgs e)
        {
            button3.BackColor = Color.Cyan;
            SendKeys.Send(".");
            await Task.Delay(FlashDelay);
            button3.BackColor = Color.Black;
        }

        private async void button4_Click(object sender, EventArgs e)
        {
            button4.BackColor = Color.Cyan;
            if (bottom == true)
            {
                //background panel
                button4.Text = "Down";
                panel38.Location = new Point(Convert.ToInt32(System.Windows.SystemParameters.WorkArea.Left), Convert.ToInt32(System.Windows.SystemParameters.WorkArea.Top));

            }
            else
            {
                button4.Text = "Up";
                //background panel
                double panelWidth = Convert.ToDouble(System.Windows.SystemParameters.WorkArea.Width);
                double newPanelWidth = panelWidth;
                int intNewPanelWidth = Convert.ToInt32(newPanelWidth);
                panel38.Width = (intNewPanelWidth);
                panel38.Left = Convert.ToInt32(System.Windows.SystemParameters.WorkArea.Left);//(intNewPanelWidth / 24);

                panel38.Height = panel38.Width / 4 + 4;
                panel38.Top = (ClientSize.Height - panel38.Height);

                if (IsTaskbarVisible())
                {
                    int taskBHeight = Convert.ToInt32(Math.Abs(System.Windows.SystemParameters.PrimaryScreenHeight - System.Windows.SystemParameters.WorkArea.Height));
                    panel38.Top = (ClientSize.Height - panel38.Height - taskBHeight);
                }

            }
            bottom =! bottom;
            await Task.Delay(FlashDelay);
            button4.BackColor = Color.Black;
        }

        private async void button5_Click(object sender, EventArgs e)
        {
            button5.BackColor = Color.Cyan;
            SendKeys.Send(button32x[KeyboardView]);
            await Task.Delay(FlashDelay);
            button5.BackColor = Color.Black;
        }

        private async void button6_Click(object sender, EventArgs e)
        {
            button6.BackColor = Color.Cyan;
            //SendKeys.Send("{CTRL}"); //ctrl still crashing application
            await Task.Delay(FlashDelay);
            button6.BackColor = Color.Black;
        }

        private async void button7_Click(object sender, EventArgs e)
        {
            button7.BackColor = Color.Cyan;
            SendKeys.Send(button20x[KeyboardView]);
            await Task.Delay(FlashDelay);
            button7.BackColor = Color.Black;
        }

        private async void button13_Click(object sender, EventArgs e)
        {
            button13.BackColor = Color.Cyan;
            //if (alpha == true)
            //{
            //    SendKeys.Send("{ENTER}");
            //}
            //else
            //{
            //    SendKeys.Send("+");
            //}
            SendKeys.Send(button2x[KeyboardView]);
            await Task.Delay(FlashDelay);
            button13.BackColor = Color.Black;
        }

        private async void button21_Click(object sender, EventArgs e)
        {
            button21.BackColor = Color.Cyan;
            SendKeys.Send(button21x[KeyboardView]);
            await Task.Delay(FlashDelay);
            button21.BackColor = Color.Black;
        }

        private async void button24_Click(object sender, EventArgs e)
        {
            button24.BackColor = Color.Cyan;
            SendKeys.Send(button28x[KeyboardView]);
            await Task.Delay(FlashDelay);
            button24.BackColor = Color.Black;
        }

        private async void button23_Click(object sender, EventArgs e)
        {
            button23.BackColor = Color.Cyan;
            SendKeys.Send(button33x[KeyboardView]);
            await Task.Delay(FlashDelay);
            button23.BackColor = Color.Black;
        }

        private async void button22_Click(object sender, EventArgs e)
        {
            button22.BackColor = Color.Cyan;
            SendKeys.Send(",");
            await Task.Delay(FlashDelay);
            button22.BackColor = Color.Black;
        }

        private async void button19_Click(object sender, EventArgs e)
        {
            button19.BackColor = Color.Cyan;
            SendKeys.Send(button31x[KeyboardView]);
            await Task.Delay(FlashDelay);
            button19.BackColor = Color.Black;
        }

        private async void button18_Click(object sender, EventArgs e)
        {
            button18.BackColor = Color.Cyan;
            SendKeys.Send(" ");
            await Task.Delay(FlashDelay);
            button18.BackColor = Color.Black;
        }

        private async void button17_Click(object sender, EventArgs e)
        {
            button17.BackColor = Color.Cyan;
            SendKeys.Send(button22x[KeyboardView]);
            await Task.Delay(FlashDelay);
            button17.BackColor = Color.Black;
        }

        private async void button16_Click(object sender, EventArgs e)
        {
            button16.BackColor = Color.Cyan;
            SendKeys.Send(button17x[KeyboardView]);
            await Task.Delay(FlashDelay);
            button16.BackColor = Color.Black;
        }

        private async void button15_Click(object sender, EventArgs e)
        {
            button15.BackColor = Color.Cyan;
            SendKeys.Send(button11x[KeyboardView]);
            await Task.Delay(FlashDelay);
            button15.BackColor = Color.Black;
        }

        private async void button33_Click(object sender, EventArgs e)
        {
            button33.BackColor = Color.Cyan;
            SendKeys.Send(button18x[KeyboardView]);
            await Task.Delay(FlashDelay);
            button33.BackColor = Color.Black;
        }

        private async void button36_Click(object sender, EventArgs e)
        {
            button36.BackColor = Color.Cyan;
            SendKeys.Send(button24x[KeyboardView]);
            await Task.Delay(FlashDelay);
            button36.BackColor = Color.Black;
        }

        private async void button35_Click(object sender, EventArgs e)
        {
            button35.BackColor = Color.Cyan;
            SendKeys.Send(button29x[KeyboardView]);
            await Task.Delay(FlashDelay);
            button35.BackColor = Color.Black;
        }

        private async void button34_Click(object sender, EventArgs e)
        {
            button34.BackColor = Color.Cyan;
            SendKeys.Send("{ENTER}");
            await Task.Delay(FlashDelay);
            button34.BackColor = Color.Black;
        }

        private async void button31_Click(object sender, EventArgs e)
        {
            button31.BackColor = Color.Cyan;
            SendKeys.Send(button23x[KeyboardView]);
            await Task.Delay(FlashDelay);
            button31.BackColor = Color.Black;
        }

        private async void button30_Click(object sender, EventArgs e)
        {
            button30.BackColor = Color.Cyan;
            SendKeys.Send(button19x[KeyboardView]);
            await Task.Delay(FlashDelay);
            button30.BackColor = Color.Black;
        }

        private async void button29_Click(object sender, EventArgs e)
        {
            button29.BackColor = Color.Cyan;
            SendKeys.Send(button15x[KeyboardView]);
            await Task.Delay(FlashDelay);
            button29.BackColor = Color.Black;
        }

        private async void button28_Click(object sender, EventArgs e)
        {
            button28.BackColor = Color.Cyan;
            SendKeys.Send("{BACKSPACE}");
            await Task.Delay(FlashDelay);
            button28.BackColor = Color.Black;
        }

        private async void button27_Click(object sender, EventArgs e)
        {
            button27.BackColor = Color.Cyan;
            SendKeys.Send(button9x[KeyboardView]);
            await Task.Delay(FlashDelay);
            button27.BackColor = Color.Black;
        }

        private async void button26_Click(object sender, EventArgs e)
        {
            button26.BackColor = Color.Cyan;
            SendKeys.Send(button5x[KeyboardView]);
            await Task.Delay(FlashDelay);
            button26.BackColor = Color.Black;
        }

        private async void button8_Click(object sender, EventArgs e)
        {
            button8.BackColor = Color.Cyan;
            //SendKeys.Send(button13x[KeyboardView]);
            

            if (cap)
            {
                KeyboardView -= 1;
                await Task.Delay(FlashDelay);
                button8.BackColor = Color.Black;
            }
            else
            {
                KeyboardView += 1;
            }
            cap =! cap;
            rename_buttons();
        }

        private async void button9_ClickAsync(object sender, EventArgs e)
        {
            button9.BackColor = Color.Cyan;
            SendKeys.Send(button7x[KeyboardView]);
            await Task.Delay(FlashDelay);
            button9.BackColor = Color.Black;
        }

        private async void button10_Click(object sender, EventArgs e)
        {
            

            KeyboardView = (KeyboardView + 2) % KeyboardAmount;

            rename_buttons();

            await Task.Delay(FlashDelay);
            button10.BackColor = Color.Black;


        }

        private async void button11_ClickAsync(object sender, EventArgs e)
        {
            button11.BackColor = Color.Cyan;
            SendKeys.Send(button14x[KeyboardView]);
            await Task.Delay(FlashDelay);
            button11.BackColor = Color.Black;
        }

        private async void button12_ClickAsync(object sender, EventArgs e)
        {
            button12.BackColor = Color.Cyan;
            SendKeys.Send(button10x[KeyboardView]);
            await Task.Delay(FlashDelay);
            button12.BackColor = Color.Black;
        }

        private async void button20_ClickAsync(object sender, EventArgs e)
        {
            button20.BackColor = Color.Cyan;
            SendKeys.Send(button8x[KeyboardView]);
            await Task.Delay(FlashDelay);
            button20.BackColor = Color.Black;
        }

        private void button32_Click(object sender, EventArgs e)
        {

        }

        private async void button14_ClickAsync(object sender, EventArgs e)
        {
            button14.BackColor = Color.Cyan;
            SendKeys.Send(button6x[KeyboardView]);
            await Task.Delay(FlashDelay);
            button14.BackColor = Color.Black;
        }

        private async void button25_ClickAsync(object sender, EventArgs e)
        {
            button25.BackColor = Color.Cyan;
            SendKeys.Send(button3x[KeyboardView]);
            await Task.Delay(FlashDelay);
            button25.BackColor = Color.Black;
        }

        private void panel38_Paint(object sender, PaintEventArgs e)
        {

        }

        private async void button37_ClickAsync(object sender, EventArgs e)
        {
            button37.BackColor = Color.Cyan;
            SendKeys.Send(button4x[KeyboardView]);
            await Task.Delay(FlashDelay);
            button37.BackColor = Color.Black;

        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            
        }
    }
}
