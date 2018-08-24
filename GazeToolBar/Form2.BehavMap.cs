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
    public partial class Form2 : Form
    {
        int buttonClickDelay = 500;
        private void connectBehaveMap()
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
            Form2Map.Add(button1, new GazeAwareBehavior(button1_Click) { DelayMilliseconds = buttonClickDelay });

            Form2Map.Add(panel1, new GazeAwareBehavior(OnGazeChangeBTColour));
        }

        //toggle border on and off on gaze to gaze to give feed back.
        private void OnGazeChangeBTColour(object s, GazeAwareEventArgs e)
        {
            var sentButton = s as Panel;
            if (sentButton != null)
            {
                sentButton.BackColor = (e.HasGaze) ? Color.Red : Color.Black;
            }
        }

        private void button1_Click(object sender, GazeAwareEventArgs e)
        {
            if (e.HasGaze) button1.PerformClick();
        }
    }
}
