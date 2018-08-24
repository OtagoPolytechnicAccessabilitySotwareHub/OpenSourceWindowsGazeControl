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
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}
