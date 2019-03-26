using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using EyeXFramework.Forms;
/*
 *  Class: Form2 aka Keyboard
 *  Name: 
 *  Date: 14/03/2019
 *  Description: 
 *  Purpose: 
 */
namespace GazeToolBar
{
     
    public partial class Keyboard : Form
    {
        int FlashDelay = 500; //delay on making button flash
        private bool bottom; //for location of keyboard. top or bottom
        private static FormsEyeXHost eyeXHost;

        private const int KeyboardAmount = 4; //How many keyboard.
        private bool cap; //If shift has been pressed
        private int KeyboardView; //Which keyboard is being displayed

        //Lists for each key on the keyboard. Enter, Tab, Space, ,, ., shift not included.
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
        private string[] button14x;
        private string[] button15x;
        private string[] button16x;
        private string[] button17x;
        private string[] button18x;
        private string[] button19x;
        private string[] button21x;
        private string[] button22x;
        private string[] button23x;
        private string[] button28x;
        private string[] button29x;
        private string[] button30x;
        private string[] button31x;
        private string[] button32x;
        private string[] button33x;
        private string[] button34x;



        public Keyboard(FormsEyeXHost EyeXHost)
        {
            eyeXHost = EyeXHost;
            InitializeComponent();
            connectBehaveMap();
            //Default to bottom of screen and shift off
            bottom = true;
            cap = false;
        }

        private void Form2_Load(object sender, EventArgs e)
        {
            //keyboard set to first set of keys
            KeyboardView = 0;
            timer1.Enabled = true;
            //Setting up keyboard design
            this.BackColor = Color.Fuchsia;
            TransparencyKey = Color.Fuchsia; //form is maximised and transparent
            panel38.SendToBack();
            //double panelWidth = Convert.ToDouble(Screen.PrimaryScreen.WorkingArea.Width);                    DELETE?????

            //Resizing keyboard by the size of the screen
            double panelWidth = Convert.ToDouble(System.Windows.SystemParameters.WorkArea.Width); 
            double newPanelWidth = panelWidth;
            int intNewPanelWidth = Convert.ToInt32(newPanelWidth);
            panel38.Width = (intNewPanelWidth);
            panel38.Left = Convert.ToInt32(System.Windows.SystemParameters.WorkArea.Left);//(intNewPanelWidth / 24);
            panel38.Height = panel38.Width / 4 + 4;
            panel38.Top = (ClientSize.Height - panel38.Height); //for changing position of keyboard, -50 is a rough equivilent of taskbar height

            //Moves keyboard location to allow for taskbar when visible
            if (IsTaskbarVisible())
            {
                int taskBHeight = Convert.ToInt32(Math.Abs(System.Windows.SystemParameters.PrimaryScreenHeight - System.Windows.SystemParameters.WorkArea.Height));
                panel38.Top = (ClientSize.Height - panel38.Height - taskBHeight);
            }


            //Dynamically resizing and ordering keys onto keyboard
            int count = 0;
            int count2 = 0;
            int countbutton = 0;
            foreach (Control control in panel38.Controls)
            {
                control.AutoSize = false;
                control.Size = new Size(panel38.Width / 12, panel38.Width / 12);
                control.Location = (new Point(count, count2 + 4));
                count = count + panel38.Width / 12;
                if (count > panel38.Width - (panel38.Width / 12))
                {
                    count2 = count2 + panel38.Width / 12;
                    count = 0;
                }
                foreach (Button button in control.Controls.OfType<Button>())
                {
                    button.FlatStyle = FlatStyle.Flat;
                    button.FlatAppearance.BorderSize = 0;
                    button.Size = new Size(panel38.Width / 12 - 4, panel38.Width / 12 - 4);
                    button.Location = new Point(2, 2);
                    //Console.WriteLine(button.Name + " " + button.Width + " " + button.Height + " " + button.Location);                   DELETE?????
                    button.TabStop = false;
                    countbutton++;
                }
            }
            //Console.WriteLine();                   DELETE?????

            //Lists for each key on keyboard.
            //When adding new keyboards, add two at a time to allow for shift key.
            button1x = new String[] { "123", "123", "abc", "abc" };//Entries repeated to allow for shift button
            //Row One
            button2x = new String[] {"q","Q", "1" , "{F1}" };
            button3x = new String[] {"w", "W", "2", "{F2}" };
            button4x = new String[] { "e", "E", "3", "{F3}" };
            button5x = new String[] { "r", "R", "4", "{F4}" };
            button6x = new String[] { "t", "T", "5", "{F5}" };
            button7x = new String[] { "y", "Y", "6", "{F6}" };
            button8x = new String[] { "u", "U", "7", "{F7}" };
            button9x = new String[] { "i", "I", "8", "{F8}" };
            button10x = new String[] { "o", "O", "9", "{F9}" };
            button11x = new String[] { "p", "P", "0", "{F10}" };
            //Row 2
            button14x = new String[] { "a", "A", "@", "{F11}" };
            button15x = new String[] { "s", "S", "#", "{F12}" };
            button16x = new String[] { "d", "D", "$", "{HOME}" };
            button17x = new String[] { "f", "F", "?", "{END}" };
            button18x = new String[] { "g", "G", "{(}", "{PRTSC}" };
            button19x = new String[] { "h", "H", "{)}", "{UP}" };
            button21x = new String[] { "j", "J", "'", "{%}" };
            button22x = new String[] { "k", "K", "\"", "{{}" };
            button23x = new String[] { "l", "L", "!", "{}}" };
            //Row 3
            button28x = new String[] { "z", "Z", "-", "/" };
            button29x = new String[] { "x", "X", "{+}", "[" };
            button30x = new String[] { "c", "C", "=", "]" };
            button31x = new String[] { "v", "V", "<", "{LEFT}" };
            button32x = new String[] { "b", "B", ">", "{DOWN}" };
            button33x = new String[] { "n", "N", ";", "{RIGHT}" };
            button34x = new String[] { "m", "M", ":", "&" };
            //button13.Text = button2x[KeyboardView];                   DELETE?????

            //puts correct text on keys
            rename_buttons();
            //Keys that are never renamed
            button28.Text = "⌫";
            button8.Text = "⇧";
            button36.Text = "⭾";    
            button34.Text = "↵";
            button6.Text = "Ctrl";
            button18.Text = "⎵";
            button3.Text = ".";
            button22.Text = ",";


        }

        //Changes text on keyboard when 'abc' and shift are pressed
        private void rename_buttons()
        {
            //Strips keys of {} before reseting text on each button
            button10.Text = strip_Keys(button1x[KeyboardView]);
            button13.Text = strip_Keys(button2x[KeyboardView]);
            button25.Text = strip_Keys(button3x[KeyboardView]);
            button37.Text = strip_Keys(button4x[KeyboardView]);
            button26.Text = strip_Keys(button5x[KeyboardView]);
            button14.Text = strip_Keys(button6x[KeyboardView]);
            button9.Text = strip_Keys(button7x[KeyboardView]);
            button20.Text = strip_Keys(button8x[KeyboardView]);
            button27.Text = strip_Keys(button9x[KeyboardView]);
            button12.Text = strip_Keys(button10x[KeyboardView]);
            button15.Text = strip_Keys(button11x[KeyboardView]);
            button11.Text = strip_Keys(button14x[KeyboardView]);
            button29.Text = strip_Keys(button15x[KeyboardView]);
            button16.Text = strip_Keys(button16x[KeyboardView]);
            button33.Text = strip_Keys(button17x[KeyboardView]);
            button30.Text = strip_Keys(button18x[KeyboardView]);
            button7.Text = strip_Keys(button19x[KeyboardView]);
            button21.Text = strip_Keys(button21x[KeyboardView]);
            button17.Text = strip_Keys(button22x[KeyboardView]);
            button31.Text = strip_Keys(button23x[KeyboardView]);
            button24.Text = strip_Keys(button28x[KeyboardView]);
            button35.Text = strip_Keys(button29x[KeyboardView]);
            button1.Text = strip_Keys(button30x[KeyboardView]);
            button19.Text = strip_Keys(button31x[KeyboardView]);
            button5.Text = strip_Keys(button32x[KeyboardView]);
            button23.Text = strip_Keys(button33x[KeyboardView]);
            button2.Text = strip_Keys(button34x[KeyboardView]);

            //------------------------------------------------------//

            //Resizes text on keys
            int newSize = panel38.Width / 40;
            //Different size for keyboards that have more characters on each button
            if (KeyboardView==3)
            {
                newSize = panel38.Width / 70;
            }
            foreach (Control control in panel38.Controls)
            {
                foreach (Button button in control.Controls.OfType<Button>())
                {
                    button.Font = new Font(button.Font.FontFamily, newSize);
                }
            }
            //Setting Symbol keys to thier own font size so it stays consistent
            button34.Font = new Font(button34.Font.FontFamily, panel38.Width/20);
            button28.Font = new Font(button28.Font.FontFamily, panel38.Width / 40);
            button8.Font = new Font(button8.Font.FontFamily, panel38.Width / 20);
            button36.Font = new Font(button36.Font.FontFamily, panel38.Width / 30);
            button18.Font = new Font(button18.Font.FontFamily, panel38.Width / 25);
            button4.Font = new Font(button4.Font.FontFamily, panel38.Width / 30);
        }

        //Strips {} off sendkeys to be displayed on buttons.
        private String strip_Keys(String key)
        {
            //Regex used so { and } can be used on keyboard
            Regex regex = new Regex(Regex.Escape("{"));
            string k = regex.Replace(key, "", 1);
            regex = new Regex(Regex.Escape("}"));
            k = regex.Replace(k, "", 1);
            k = k.Replace("&", "&&"); //Single & does not display as it is a hotkey
            return k;
        }


        //Returns if taskbar is visible on screen.
        public static bool IsTaskbarVisible()
        {
            return (Math.Abs(System.Windows.SystemParameters.PrimaryScreenHeight - System.Windows.SystemParameters.WorkArea.Height) > 0);
        }


        private async void button1_Click(object sender, EventArgs e) 
        {
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

        //Button for switching keyboard location from bottom and top of screen.
        private async void button4_Click(object sender, EventArgs e)
        {
            button4.BackColor = Color.Cyan;
            if (bottom)
            {
                button4.Text = "⤓";
                panel38.Location = new Point(Convert.ToInt32(System.Windows.SystemParameters.WorkArea.Left), Convert.ToInt32(System.Windows.SystemParameters.WorkArea.Top));
            }
            else
            {
                button4.Text = "⤒";
                double panelWidth = Convert.ToDouble(System.Windows.SystemParameters.WorkArea.Width);
                double newPanelWidth = panelWidth;
                int intNewPanelWidth = Convert.ToInt32(newPanelWidth);
                panel38.Width = (intNewPanelWidth);
                panel38.Left = Convert.ToInt32(System.Windows.SystemParameters.WorkArea.Left);

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
            SendKeys.Send("^");
            await Task.Delay(FlashDelay);
            button6.BackColor = Color.Black;
        }

        private async void button7_Click(object sender, EventArgs e)
        {
            button7.BackColor = Color.Cyan;
            SendKeys.Send(button19x[KeyboardView]);
            await Task.Delay(FlashDelay);
            button7.BackColor = Color.Black;
        }

        private async void button13_Click(object sender, EventArgs e)
        {
            button13.BackColor = Color.Cyan;
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
            SendKeys.Send(button16x[KeyboardView]);
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
            SendKeys.Send(button17x[KeyboardView]);
            await Task.Delay(FlashDelay);
            button33.BackColor = Color.Black;
        }

        private async void button36_Click(object sender, EventArgs e)
        {
            button36.BackColor = Color.Cyan;
            SendKeys.Send("{TAB}");
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
            SendKeys.Send(button18x[KeyboardView]);
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

        //Shift key.
        private async void button8_Click(object sender, EventArgs e)
        {
            button8.BackColor = Color.Cyan;
            //If shift key is currently active
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

        //'abc' button.
        private async void button10_Click(object sender, EventArgs e)
        {
            //Increase by two to allow for shift key.
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

        //To Delete
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

        //To Delete
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

        //To Delete
        private void timer1_Tick(object sender, EventArgs e)
        {
            
        }
    }
}
