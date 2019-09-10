using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using EyeXFramework.Forms;
/*
 *  Class: Keyboard
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

        private const int KeyboardAmount = 6; //How many keyboard.
        private bool cap; //If shift has been pressed
        private int KeyboardView; //Which keyboard is being displayed

        //Lists for each key on the keyboard. Enter, Tab, Space, ,, ., shift not included.

        private keyboardKeys[] keys;


        private Panel F1keyboardPanel;
        private Panel F1LeftClickPanel;
        private Panel F1RightClickPanel;
        private Panel F1DoubleClickPanel;
        private Panel F1ScrollPanel;
        private Form1 gazeSidePanel;



        public Keyboard(FormsEyeXHost EyeXHost, Form1 gazeSidePanel, Panel form1KB, Panel form1LCP, Panel form1RCP, Panel form1DCP, Panel form1ScrollP)
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
            F1ScrollPanel = form1ScrollP;
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
                    button.TabStop = false;
                    countbutton++;
                }
            }



            keys = new keyboardKeys[27];
            string[] keyboardsAvailable = new string[] { "KeyboardSetOne.txt", "KeyboardSetTwo.txt", "KeyboardSetThree.txt" };
            for (int i = 0; i < 27; i++)
            {
                keys[i] = new keyboardKeys();
            }
            Console.WriteLine("Keyboard Made");


            for (int i = 0; i < keyboardsAvailable.Length; i++)
            {
                Console.WriteLine("Keys added");
                using (StreamReader sr = new StreamReader(keyboardsAvailable[i], Encoding.GetEncoding("iso-8859-1")))
                    {
                        string line;
                        int lineNum = 0;
                        string firstEnt = sr.ReadLine();
                        keys[lineNum].addKey(firstEnt);
                        keys[lineNum].addKey(firstEnt);
                        while ((line = sr.ReadLine()) != null)
                        {
                            lineNum++;
                            string[] seperateKeys = line.Split(',');
                            keys[lineNum].addKey(seperateKeys[0]);
                            keys[lineNum].addKey(seperateKeys[1]);                            
                        }

                }
                
            }
            Console.WriteLine("Keys finished");

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

            panelLeftClick.Location = F1LeftClickPanel.Location;
            panelLeftClick.Left = Convert.ToInt32((F1LeftClickPanel.Left + System.Windows.SystemParameters.WorkArea.Width));
            panelLeftClick.Size = F1LeftClickPanel.Size;

            panelRightClick.Location = F1RightClickPanel.Location;
            panelRightClick.Left = Convert.ToInt32((F1RightClickPanel.Left + System.Windows.SystemParameters.WorkArea.Width));
            panelRightClick.Size = F1RightClickPanel.Size;

            panelScroll.Location = F1ScrollPanel.Location;
            panelScroll.Left = Convert.ToInt32((F1ScrollPanel.Left + System.Windows.SystemParameters.WorkArea.Width));
            panelScroll.Size = F1ScrollPanel.Size;

            panelDLeftClick.Location = F1DoubleClickPanel.Location;
            panelDLeftClick.Left = Convert.ToInt32((F1DoubleClickPanel.Left + System.Windows.SystemParameters.WorkArea.Width));
            panelDLeftClick.Size = F1DoubleClickPanel.Size;
        }

        //Changes text on keyboard when 'abc' and shift are pressed
        private void rename_buttons()
        {
            //Strips keys of {} before reseting text on each button
            Console.WriteLine("strip 0");
            button10.Text = strip_Keys(keys[0].getKey((KeyboardView+2)%KeyboardAmount));
            button13.Text = strip_Keys(keys[1].getKey(KeyboardView));
            button25.Text = strip_Keys(keys[2].getKey(KeyboardView));
            button37.Text = strip_Keys(keys[3].getKey(KeyboardView));
            button26.Text = strip_Keys(keys[4].getKey(KeyboardView));
            button14.Text = strip_Keys(keys[5].getKey(KeyboardView));
            button9.Text = strip_Keys(keys[6].getKey(KeyboardView));
            button20.Text = strip_Keys(keys[7].getKey(KeyboardView));
            button27.Text = strip_Keys(keys[8].getKey(KeyboardView));
            button12.Text = strip_Keys(keys[9].getKey(KeyboardView));
            
            button15.Text = strip_Keys(keys[10].getKey(KeyboardView));
            button11.Text = strip_Keys(keys[11].getKey(KeyboardView));
            button29.Text = strip_Keys(keys[12].getKey(KeyboardView));
            button16.Text = strip_Keys(keys[13].getKey(KeyboardView));
            button33.Text = strip_Keys(keys[14].getKey(KeyboardView));
            button30.Text = strip_Keys(keys[15].getKey(KeyboardView));
            button7.Text = strip_Keys(keys[16].getKey(KeyboardView));
            
            button21.Text = strip_Keys(keys[17].getKey(KeyboardView));
            button17.Text = strip_Keys(keys[18].getKey(KeyboardView));
            button31.Text = strip_Keys(keys[19].getKey(KeyboardView));
            button18.Text = strip_Keys(keys[20].getKey(KeyboardView));
            button24.Text = strip_Keys(keys[21].getKey(KeyboardView));
            button35.Text = strip_Keys(keys[22].getKey(KeyboardView));
            Console.WriteLine("strip 3");
            button1.Text = strip_Keys(keys[23].getKey(KeyboardView));
            button5.Text = strip_Keys(keys[24].getKey(KeyboardView));
            button23.Text = strip_Keys(keys[25].getKey(KeyboardView));
            button2.Text = strip_Keys(keys[26].getKey(KeyboardView));

            
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

        private async void buttonClicker(int key, Button button)
        {
            button.BackColor = Color.Cyan;
            SendKeys.Send(keys[key].getKey(KeyboardView)); //set background color when clicked
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
        //Buttons

        private async void button10_Click(object sender, EventArgs e)
        {
            //Increase by two to allow for shift key.
            KeyboardView = (KeyboardView + 2) % KeyboardAmount;
            rename_buttons();
            await Task.Delay(FlashDelay);
            button10.BackColor = Color.Black;
        }

        private void button13_Click(object sender, EventArgs e)
        {
            buttonClicker(1, button13);
        }

        private void button25_ClickAsync(object sender, EventArgs e)
        {
            buttonClicker(2, button25);
        }

        private void button37_ClickAsync(object sender, EventArgs e)
        {
            buttonClicker(3, button37);

        }

        private void button26_Click(object sender, EventArgs e)
        {
            buttonClicker(4, button26);
        }

        private void button14_ClickAsync(object sender, EventArgs e)
        {
            buttonClicker(5, button14);
        }

        private void button9_ClickAsync(object sender, EventArgs e)
        {
            buttonClicker(6, button9);
        }

        private void button20_ClickAsync(object sender, EventArgs e)
        {
            buttonClicker(7, button20);
        }

        private void button27_Click(object sender, EventArgs e)
        {
            buttonClicker(8, button27);
        }
        private void button12_ClickAsync(object sender, EventArgs e)
        {
            buttonClicker(9, button12);
        }

        private void button15_Click(object sender, EventArgs e)
        {
            buttonClicker(10, button15);
        }
        private void button11_ClickAsync(object sender, EventArgs e)
        {
            buttonClicker(11, button11);
        }

        private void button29_Click(object sender, EventArgs e)
        {
            buttonClicker(12, button29);
        }

        private void button16_Click(object sender, EventArgs e)
        {
            buttonClicker(13, button16);
        }


        private void button33_Click(object sender, EventArgs e)
        {
            buttonClicker(14, button33);
        }

        private void button30_Click(object sender, EventArgs e)
        {
            buttonClicker(15, button30);
        }
        private void button7_Click(object sender, EventArgs e)
        {
            buttonClicker(16, button7);
        }

        private void button21_Click(object sender, EventArgs e)
        {
            buttonClicker(17, button21);
        }
        private void button17_Click(object sender, EventArgs e)
        {
            buttonClicker(18, button17);
        }

        private void button31_Click(object sender, EventArgs e)
        {
            buttonClicker(19, button31);
        }

        private void button18_Click(object sender, EventArgs e)
        {
            //buttonClicker(" ", button18);
            buttonClicker(20, button18);
        }

        private void button24_Click(object sender, EventArgs e)
        {
            buttonClicker(21, button24);
        }

        private void button35_Click(object sender, EventArgs e)
        {
            buttonClicker(22, button35);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            buttonClicker(23, button1);
        }
        private void button5_Click(object sender, EventArgs e)
        {
            buttonClicker(24, button5);
        }

        private void button23_Click(object sender, EventArgs e)
        {
            buttonClicker(25, button23);
        }

        private void button2_ClickAsync(object sender, EventArgs e)
        {
            buttonClicker(26, button2);
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



        private void button6_Click(object sender, EventArgs e)
        {
            //this.Close();
            buttonClicker("^", button6);
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








        private void button36_Click(object sender, EventArgs e)
        {
            buttonClicker("{TAB}", button36);
        }



        private void button34_Click(object sender, EventArgs e)
        {
            buttonClicker("{ENTER}", button34);
        }







        private void button28_Click(object sender, EventArgs e)
        {
            buttonClicker("{BACKSPACE}", button28);
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



        //'abc' button.














        private void panel38_Paint(object sender, PaintEventArgs e)
        {

        }

        private void btnKeyboard_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnLeftClick_Click(object sender, EventArgs e)
        {
            gazeSidePanel.btnSingleLeftClick.PerformClick();
        }

        private void btnRightClick_Click(object sender, EventArgs e)
        {
            gazeSidePanel.btnRightClick.PerformClick();
        }

        private void btnScroll_Click(object sender, EventArgs e)
        {
            gazeSidePanel.btnScoll.PerformClick();
        }

        private void btnDoubleLeftClick_Click(object sender, EventArgs e)
        {
            gazeSidePanel.btnDoubleClick.PerformClick();
        }
    }
}
