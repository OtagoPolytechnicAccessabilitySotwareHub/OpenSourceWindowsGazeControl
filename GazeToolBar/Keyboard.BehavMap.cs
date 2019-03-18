using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using EyeXFramework;
using EyeXFramework.Forms;

namespace GazeToolBar
{
    public partial class Keyboard : Form
    {
        int buttonClickDelay = 500; //How long (ms) you need to look at button before it sends click event
        private void connectBehaveMap() //connecting the behaviour map with the form
        {
            eyeXHost.Connect(Form2Map);

            setupMap();
            setupForm2Map();
        }

        private void setupMap()
        {

        }

        private void setupForm2Map()
        {
            Form2Map.Add(button1, new GazeAwareBehavior(button1_Click) { DelayMilliseconds = buttonClickDelay }); //look at button to activate it
            Form2Map.Add(panel1, new GazeAwareBehavior(OnGazeChangeBTColour));                                    //look at the panel to light up the area behind the button so you know where you are looking on the keyboard

            Form2Map.Add(button2, new GazeAwareBehavior(button2_Click) { DelayMilliseconds = buttonClickDelay });
            Form2Map.Add(panel2, new GazeAwareBehavior(OnGazeChangeBTColour));

            Form2Map.Add(button3, new GazeAwareBehavior(button3_Click) { DelayMilliseconds = buttonClickDelay });
            Form2Map.Add(panel3, new GazeAwareBehavior(OnGazeChangeBTColour));

            Form2Map.Add(button4, new GazeAwareBehavior(button4_Click) { DelayMilliseconds = buttonClickDelay });
            Form2Map.Add(panel4, new GazeAwareBehavior(OnGazeChangeBTColour));

            Form2Map.Add(button5, new GazeAwareBehavior(button5_Click) { DelayMilliseconds = buttonClickDelay });
            Form2Map.Add(panel5, new GazeAwareBehavior(OnGazeChangeBTColour));

            Form2Map.Add(button6, new GazeAwareBehavior(button6_Click) { DelayMilliseconds = buttonClickDelay });
            Form2Map.Add(panel6, new GazeAwareBehavior(OnGazeChangeBTColour));

            Form2Map.Add(button7, new GazeAwareBehavior(button7_Click) { DelayMilliseconds = buttonClickDelay });
            Form2Map.Add(panel7, new GazeAwareBehavior(OnGazeChangeBTColour));

            Form2Map.Add(button8, new GazeAwareBehavior(button8_Click) { DelayMilliseconds = buttonClickDelay });
            Form2Map.Add(panel8, new GazeAwareBehavior(OnGazeChangeBTColour));

            Form2Map.Add(button9, new GazeAwareBehavior(button9_Click) { DelayMilliseconds = buttonClickDelay });
            Form2Map.Add(panel9, new GazeAwareBehavior(OnGazeChangeBTColour));

            Form2Map.Add(button10, new GazeAwareBehavior(button10_Click) { DelayMilliseconds = buttonClickDelay });
            Form2Map.Add(panel10, new GazeAwareBehavior(OnGazeChangeBTColour));

            Form2Map.Add(button11, new GazeAwareBehavior(button11_Click) { DelayMilliseconds = buttonClickDelay });
            Form2Map.Add(panel11, new GazeAwareBehavior(OnGazeChangeBTColour));

            Form2Map.Add(button12, new GazeAwareBehavior(button12_Click) { DelayMilliseconds = buttonClickDelay });
            Form2Map.Add(panel12, new GazeAwareBehavior(OnGazeChangeBTColour));

            Form2Map.Add(button13, new GazeAwareBehavior(button13_Click) { DelayMilliseconds = buttonClickDelay });
            Form2Map.Add(panel13, new GazeAwareBehavior(OnGazeChangeBTColour));

            Form2Map.Add(button14, new GazeAwareBehavior(button14_Click) { DelayMilliseconds = buttonClickDelay });
            Form2Map.Add(panel14, new GazeAwareBehavior(OnGazeChangeBTColour));

            Form2Map.Add(button15, new GazeAwareBehavior(button15_Click) { DelayMilliseconds = buttonClickDelay });
            Form2Map.Add(panel15, new GazeAwareBehavior(OnGazeChangeBTColour));

            Form2Map.Add(button16, new GazeAwareBehavior(button16_Click) { DelayMilliseconds = buttonClickDelay });
            Form2Map.Add(panel16, new GazeAwareBehavior(OnGazeChangeBTColour));

            Form2Map.Add(button17, new GazeAwareBehavior(button17_Click) { DelayMilliseconds = buttonClickDelay });
            Form2Map.Add(panel17, new GazeAwareBehavior(OnGazeChangeBTColour));

            Form2Map.Add(button18, new GazeAwareBehavior(button18_Click) { DelayMilliseconds = buttonClickDelay });
            Form2Map.Add(panel18, new GazeAwareBehavior(OnGazeChangeBTColour));

            Form2Map.Add(button19, new GazeAwareBehavior(button19_Click) { DelayMilliseconds = buttonClickDelay });
            Form2Map.Add(panel19, new GazeAwareBehavior(OnGazeChangeBTColour));

            Form2Map.Add(button20, new GazeAwareBehavior(button20_Click) { DelayMilliseconds = buttonClickDelay });
            Form2Map.Add(panel20, new GazeAwareBehavior(OnGazeChangeBTColour));

            Form2Map.Add(button21, new GazeAwareBehavior(button21_Click) { DelayMilliseconds = buttonClickDelay });
            Form2Map.Add(panel21, new GazeAwareBehavior(OnGazeChangeBTColour));

            Form2Map.Add(button22, new GazeAwareBehavior(button22_Click) { DelayMilliseconds = buttonClickDelay });
            Form2Map.Add(panel22, new GazeAwareBehavior(OnGazeChangeBTColour));

            Form2Map.Add(button23, new GazeAwareBehavior(button23_Click) { DelayMilliseconds = buttonClickDelay });
            Form2Map.Add(panel23, new GazeAwareBehavior(OnGazeChangeBTColour));

            Form2Map.Add(button24, new GazeAwareBehavior(button24_Click) { DelayMilliseconds = buttonClickDelay });
            Form2Map.Add(panel24, new GazeAwareBehavior(OnGazeChangeBTColour));

            Form2Map.Add(button25, new GazeAwareBehavior(button25_Click) { DelayMilliseconds = buttonClickDelay });
            Form2Map.Add(panel25, new GazeAwareBehavior(OnGazeChangeBTColour));

            Form2Map.Add(button26, new GazeAwareBehavior(button26_Click) { DelayMilliseconds = buttonClickDelay });
            Form2Map.Add(panel26, new GazeAwareBehavior(OnGazeChangeBTColour));

            Form2Map.Add(button27, new GazeAwareBehavior(button27_Click) { DelayMilliseconds = buttonClickDelay });
            Form2Map.Add(panel27, new GazeAwareBehavior(OnGazeChangeBTColour));

            Form2Map.Add(button28, new GazeAwareBehavior(button28_Click) { DelayMilliseconds = buttonClickDelay });
            Form2Map.Add(panel28, new GazeAwareBehavior(OnGazeChangeBTColour));

            Form2Map.Add(button29, new GazeAwareBehavior(button29_Click) { DelayMilliseconds = buttonClickDelay });
            Form2Map.Add(panel29, new GazeAwareBehavior(OnGazeChangeBTColour));

            Form2Map.Add(button30, new GazeAwareBehavior(button30_Click) { DelayMilliseconds = buttonClickDelay });
            Form2Map.Add(panel30, new GazeAwareBehavior(OnGazeChangeBTColour));

            Form2Map.Add(button31, new GazeAwareBehavior(button31_Click) { DelayMilliseconds = buttonClickDelay });
            Form2Map.Add(panel31, new GazeAwareBehavior(OnGazeChangeBTColour));

            //Form2Map.Add(button32, new GazeAwareBehavior(button32_Click) { DelayMilliseconds = buttonClickDelay });
            //Form2Map.Add(panel32, new GazeAwareBehavior(OnGazeChangeBTColour));

            Form2Map.Add(button33, new GazeAwareBehavior(button33_Click) { DelayMilliseconds = buttonClickDelay });
            Form2Map.Add(panel33, new GazeAwareBehavior(OnGazeChangeBTColour));

            Form2Map.Add(button34, new GazeAwareBehavior(button34_Click) { DelayMilliseconds = buttonClickDelay });
            Form2Map.Add(panel34, new GazeAwareBehavior(OnGazeChangeBTColour));

            Form2Map.Add(button35, new GazeAwareBehavior(button35_Click) { DelayMilliseconds = buttonClickDelay });
            Form2Map.Add(panel35, new GazeAwareBehavior(OnGazeChangeBTColour));

            Form2Map.Add(button36, new GazeAwareBehavior(button36_Click) { DelayMilliseconds = buttonClickDelay });
            Form2Map.Add(panel36, new GazeAwareBehavior(OnGazeChangeBTColour));

            Form2Map.Add(button37, new GazeAwareBehavior(button37_Click) { DelayMilliseconds = buttonClickDelay });
            Form2Map.Add(panel32, new GazeAwareBehavior(OnGazeChangeBTColour));


        }

        //toggle border on and off on gaze to gaze to give feed back.
        private void OnGazeChangeBTColour(object s, GazeAwareEventArgs e) 
        {
            var sentButton = s as Panel;
            if (sentButton != null)
            {
                sentButton.BackColor = (e.HasGaze) ? Color.FromArgb(115,220,255) : Color.Black;
            }
        }



        private void button1_Click(object sender, GazeAwareEventArgs e) //preform button click if button is looked at for long enough 
        {
            if (e.HasGaze) button1.PerformClick();
        }

        private void button2_Click(object sender, GazeAwareEventArgs e)
        {
            if (e.HasGaze) button2.PerformClick();
        }

        private void button3_Click(object sender, GazeAwareEventArgs e)
        {
            if (e.HasGaze) button3.PerformClick();
        }

        private void button4_Click(object sender, GazeAwareEventArgs e)
        {
            if (e.HasGaze) button4.PerformClick();
        }

        private void button5_Click(object sender, GazeAwareEventArgs e)
        {
            if (e.HasGaze) button5.PerformClick();
        }

        private void button6_Click(object sender, GazeAwareEventArgs e)
        {
            if (e.HasGaze) button6.PerformClick();
        }

        private void button7_Click(object sender, GazeAwareEventArgs e)
        {
            if (e.HasGaze) button7.PerformClick();
        }

        private void button8_Click(object sender, GazeAwareEventArgs e)
        {
            if (e.HasGaze) button8.PerformClick();
        }

        private void button9_Click(object sender, GazeAwareEventArgs e)
        {
            if (e.HasGaze) button9.PerformClick();
        }

        private void button10_Click(object sender, GazeAwareEventArgs e)
        {
            if (e.HasGaze) button10.PerformClick();
        }

        private void button11_Click(object sender, GazeAwareEventArgs e)
        {
            if (e.HasGaze) button11.PerformClick();
        }

        private void button12_Click(object sender, GazeAwareEventArgs e)
        {
            if (e.HasGaze) button12.PerformClick();
        }

        private void button13_Click(object sender, GazeAwareEventArgs e)
        {
            if (e.HasGaze) button13.PerformClick();
        }
        private void button14_Click(object sender, GazeAwareEventArgs e)
        {
            if (e.HasGaze) button14.PerformClick();
        }
        private void button15_Click(object sender, GazeAwareEventArgs e)
        {
            if (e.HasGaze) button15.PerformClick();
        }
        private void button16_Click(object sender, GazeAwareEventArgs e)
        {
            if (e.HasGaze) button16.PerformClick();
        }
        private void button17_Click(object sender, GazeAwareEventArgs e)
        {
            if (e.HasGaze) button17.PerformClick();
        }
        private void button18_Click(object sender, GazeAwareEventArgs e)
        {
            if (e.HasGaze) button18.PerformClick();
        }
        private void button19_Click(object sender, GazeAwareEventArgs e)
        {
            if (e.HasGaze) button19.PerformClick();
        }
        private void button20_Click(object sender, GazeAwareEventArgs e)
        {
            if (e.HasGaze) button20.PerformClick();
        }
        private void button21_Click(object sender, GazeAwareEventArgs e)
        {
            if (e.HasGaze) button21.PerformClick();
        }
        private void button22_Click(object sender, GazeAwareEventArgs e)
        {
            if (e.HasGaze) button22.PerformClick();
        }
        private void button23_Click(object sender, GazeAwareEventArgs e)
        {
            if (e.HasGaze) button23.PerformClick();
        }
        private void button24_Click(object sender, GazeAwareEventArgs e)
        {
            if (e.HasGaze) button24.PerformClick();
        }
        private void button25_Click(object sender, GazeAwareEventArgs e)
        {
            if (e.HasGaze) button25.PerformClick();
        }
        private void button26_Click(object sender, GazeAwareEventArgs e)
        {
            if (e.HasGaze) button26.PerformClick();
        }
        private void button27_Click(object sender, GazeAwareEventArgs e)
        {
            if (e.HasGaze) button27.PerformClick();
        }
        private void button28_Click(object sender, GazeAwareEventArgs e)
        {
            if (e.HasGaze) button28.PerformClick();
        }
        private void button29_Click(object sender, GazeAwareEventArgs e)
        {
            if (e.HasGaze) button29.PerformClick();
        }
        private void button30_Click(object sender, GazeAwareEventArgs e)
        {
            if (e.HasGaze) button30.PerformClick();
        }
        private void button31_Click(object sender, GazeAwareEventArgs e)
        {
            if (e.HasGaze) button31.PerformClick();
        }
        private void button33_Click(object sender, GazeAwareEventArgs e)
        {
            if (e.HasGaze) button33.PerformClick();
        }
        private void button34_Click(object sender, GazeAwareEventArgs e)
        {
            if (e.HasGaze) button34.PerformClick();
        }
        private void button35_Click(object sender, GazeAwareEventArgs e)
        {
            if (e.HasGaze) button35.PerformClick();
        }
        private void button36_Click(object sender, GazeAwareEventArgs e)
        {
            if (e.HasGaze) button36.PerformClick();
        }
        private void button37_Click(object sender, GazeAwareEventArgs e)
        {
            if (e.HasGaze) button37.PerformClick();
        }
    }
}
