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
        private Panel F1keyboardPanel;
        private Panel F1LeftClickPanel;
        private Panel F1RightClickPanel;
        private Panel F1DoubleClickPanel;
        private Form1 gazeSidePanel;



        public Keyboard(FormsEyeXHost EyeXHost, Form1 gazeSidePanel, Panel form1KB, Panel form1LCP, Panel form1RCP, Panel form1DCP)
        {
            eyeXHost = EyeXHost;
            InitializeComponent();
            connectBehaveMap();
            //Default to bottom of screen and shift off
            bottom = true;
            cap = false;
            F1keyboardPanel = form1KB;
            F1LeftClickPanel = form1LCP;
            F1RightClickPanel = form1RCP;
            F1DoubleClickPanel = form1DCP;
            this.gazeSidePanel = gazeSidePanel;
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
            button28x = new String[] { "z", "Z", "-", "/" }; //
            button29x = new String[] { "x", "X", "{+}", "[" }; //
            button30x = new String[] { "c", "C", "=", "]" }; //
            button31x = new String[] { "v", "V", "<", "{LEFT}" }; //
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
            button19.Text = "⎵";
            button3.Text = ".";
            button22.Text = ",";

            //close button

            pnlHighLightKeyboard.Location = F1keyboardPanel.Location;
            pnlHighLightKeyboard.Left = Convert.ToInt32((pnlHighLightKeyboard.Left + System.Windows.SystemParameters.WorkArea.Width));
            pnlHighLightKeyboard.Size = F1keyboardPanel.Size;

            pnlHighLightKeyboard.Location = F1keyboardPanel.Location;
            pnlHighLightKeyboard.Left = Convert.ToInt32((pnlHighLightKeyboard.Left + System.Windows.SystemParameters.WorkArea.Width));
            pnlHighLightKeyboard.Size = F1keyboardPanel.Size;

            pnlHighLightKeyboard.Location = F1keyboardPanel.Location;
            pnlHighLightKeyboard.Left = Convert.ToInt32((pnlHighLightKeyboard.Left + System.Windows.SystemParameters.WorkArea.Width));
            pnlHighLightKeyboard.Size = F1keyboardPanel.Size;

            pnlHighLightKeyboard.Location = F1keyboardPanel.Location;
            pnlHighLightKeyboard.Left = Convert.ToInt32((pnlHighLightKeyboard.Left + System.Windows.SystemParameters.WorkArea.Width));
            pnlHighLightKeyboard.Size = F1keyboardPanel.Size;

            pnlHighLightKeyboard.Location = F1keyboardPanel.Location;
            pnlHighLightKeyboard.Left = Convert.ToInt32((pnlHighLightKeyboard.Left + System.Windows.SystemParameters.WorkArea.Width));
            pnlHighLightKeyboard.Size = F1keyboardPanel.Size;




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
            button18.Text = strip_Keys(button28x[KeyboardView]); //z
            button24.Text = strip_Keys(button29x[KeyboardView]); //x
            button35.Text = strip_Keys(button30x[KeyboardView]);  //c
            button1.Text = strip_Keys(button31x[KeyboardView]); //v
            button5.Text = strip_Keys(button32x[KeyboardView]);
            button23.Text = strip_Keys(button33x[KeyboardView]);
            button2.Text = strip_Keys(button34x[KeyboardView]);

            /*button30.Text = strip_Keys(button18x[KeyboardView]);
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
            button2.Text = strip_Keys(button34x[KeyboardView]);*/

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
            button19.Font = new Font(button19.Font.FontFamily, panel38.Width / 25);
            button4.Font = new Font(button4.Font.FontFamily, panel38.Width / 30);
            button6.Font = new Font(button6.Font.FontFamily, panel38.Width / 50);
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

        private async void buttonClicker(String[] key, Button button)
        {
            button.BackColor = Color.Cyan;
            SendKeys.Send(key[KeyboardView]); //set background color when clicked
            await Task.Delay(FlashDelay);     //delay before deactivting button flash
            button.BackColor = Color.Black;   //revert back to original color
        }

        private async void buttonClicker(String key, Button button)  //for buttons that dont use keyboardview
        {
            button.BackColor = Color.Cyan;
            SendKeys.Send(key);               //set background color when clicked
            await Task.Delay(FlashDelay);     //delay before deactivting button flash
            button.BackColor = Color.Black;   //revert back to original color
        }


        private void button1_Click(object sender, EventArgs e) 
        {
            buttonClicker(button31x, button1);
        }

        private void button2_ClickAsync(object sender, EventArgs e)
        {
            buttonClicker(button34x, button2);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            buttonClicker(".", button3);
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

        private void button5_Click(object sender, EventArgs e)
        {
            buttonClicker(button32x, button5);
        }

        private void button6_Click(object sender, EventArgs e)
        {
            //this.Close();
            buttonClicker("^", button6);
        }

        private void button7_Click(object sender, EventArgs e)
        {
            buttonClicker(button19x, button7);
        }

        private void button13_Click(object sender, EventArgs e)
        {
            buttonClicker(button2x, button13);
        }

        private void button21_Click(object sender, EventArgs e)
        {
            buttonClicker(button21x, button21);
        }

        private void button24_Click(object sender, EventArgs e)
        {
            buttonClicker(button29x, button24);
        }

        private void button23_Click(object sender, EventArgs e)
        {
            buttonClicker(button33x, button23);
        }

        private void button22_Click(object sender, EventArgs e)
        {
            buttonClicker(",", button22);
        }

        private void button19_Click(object sender, EventArgs e)
        {
            buttonClicker(" ", button19);
            //buttonClicker(button31x, button19);
        }

        private void button18_Click(object sender, EventArgs e)
        {
            //buttonClicker(" ", button18);
            buttonClicker(button28x, button18);
        }

        private void button17_Click(object sender, EventArgs e)
        {
            buttonClicker(button22x, button17);
        }

        private void button16_Click(object sender, EventArgs e)
        {
            buttonClicker(button16x, button16);
        }

        private void button15_Click(object sender, EventArgs e)
        {
            buttonClicker(button11x, button15);
        }

        private void button33_Click(object sender, EventArgs e)
        {
            buttonClicker(button17x, button33);
        }

        private void button36_Click(object sender, EventArgs e)
        {
            buttonClicker("{TAB}", button36);
        }

        private void button35_Click(object sender, EventArgs e)
        {
            buttonClicker(button30x, button35);
        }

        private void button34_Click(object sender, EventArgs e)
        {
            buttonClicker("{ENTER}", button34);
        }

        private void button31_Click(object sender, EventArgs e)
        {
            buttonClicker(button23x, button31);
        }

        private void button30_Click(object sender, EventArgs e)
        {
            buttonClicker(button18x, button30);
        }

        private void button29_Click(object sender, EventArgs e)
        {
            buttonClicker(button15x, button29);
        }

        private void button28_Click(object sender, EventArgs e)
        {
            buttonClicker("{BACKSPACE}", button28);
        }

        private void button27_Click(object sender, EventArgs e)
        {
            buttonClicker(button9x, button27);
        }

        private void button26_Click(object sender, EventArgs e)
        {
            buttonClicker(button5x, button26);
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

        private void button9_ClickAsync(object sender, EventArgs e)
        {
            buttonClicker(button7x, button9);
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

        private void button11_ClickAsync(object sender, EventArgs e)
        {
            buttonClicker(button14x, button11);
        }

        private void button12_ClickAsync(object sender, EventArgs e)
        {
            buttonClicker(button10x, button12);
        }

        private void button20_ClickAsync(object sender, EventArgs e)
        {
            buttonClicker(button8x, button20);
        }

        private void button14_ClickAsync(object sender, EventArgs e)
        {
            buttonClicker(button6x, button14);
        }

        private void button25_ClickAsync(object sender, EventArgs e)
        {
            buttonClicker(button3x, button25);
        }

        private void button37_ClickAsync(object sender, EventArgs e)
        {
            buttonClicker(button4x, button37);

        }

        private void panel38_Paint(object sender, PaintEventArgs e)
        {

        }

        private void btnKeyboard_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
