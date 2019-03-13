using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GazeToolBar
{
    /// <summary>
    /// This class is a full screen, transparent form.
    /// It's purpose is for drawing the user feedback when zoomed in.
    /// 
    /// </summary>
    public partial class DrawingForm : Form
    {
        public enum CrossHair { CROSSHAIR_1, CROSSHAIR_2, CROSSHAIR_3, CROSSHAIR_4, CROSSHAIR_5, CROSSHAIR_6, CROSSHAIR_7, NONE };

        Graphics graphics;
        //Size highlightSize;
        Point currentGaze;
        Image crosshairImage;
        private IFixationSmoother smoother;
        private List<Point> history;

        public DrawingForm()
        {
            InitializeComponent();
            ShowInTaskbar = false;
            smoother = CreateSmoother();
            // If wanting to draw a fillEllipse, or re-introduce gazeHighlight
            // then un-comment the below line to set size
            // highlightSize = new Size(40, 40);
            currentGaze = new Point();

            history = new List<Point>();
            // Load the user feedback image
            crosshairImage = GetCrossHairImage((CrossHair)Program.readSettings.Crosshair);
        }

        public static Image GetCrossHairImage(CrossHair c)
        {
            switch (c)
            {
                case CrossHair.CROSSHAIR_1:
                    return Properties.Resources.crosshair_1;
                case CrossHair.CROSSHAIR_2:
                    return Properties.Resources.crosshair_2;
                case CrossHair.CROSSHAIR_3:
                    return Properties.Resources.crosshair_3;
                case CrossHair.CROSSHAIR_4:
                    return Properties.Resources.crosshair_4;
                case CrossHair.CROSSHAIR_5:
                    return Properties.Resources.crosshair_5;
                case CrossHair.CROSSHAIR_6:
                    return Properties.Resources.crosshair_6;
                case CrossHair.CROSSHAIR_7:
                    return Properties.Resources.crosshair_7;
                case CrossHair.NONE:
                    return Properties.Resources.crosshair_None;
                default:
                    return Properties.Resources.crosshair_2;
            }
        }

        /// <summary>
        /// Make form full screen, remove border, and make transparent
        /// Also give a graphics instance and set to topmost (for yet to be finished win10 menu bug)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DrawingForm_Load(object sender, EventArgs e)
        {
            Top = 0;
            Left = 0;
            Rectangle screenBounds = Screen.FromControl(this).Bounds;


            Width =  screenBounds.Width;
            Height = screenBounds.Height + 300;
            FormBorderStyle = FormBorderStyle.None;
            TransparencyKey = Color.Thistle;
            BackColor = Color.Thistle;
            graphics = CreateGraphics();
            TopMost = true;
        }


        public IFixationSmoother CreateSmoother()
        {
            return new FixationSmootherExponential(10);
        }

        public void SetCrossHairPos(Point p)
        {
            currentGaze = p;
        }

        public void SetCrossHairPos(int x, int y)
        {
            SetCrossHairPos(new Point(x, y));
        }

        public void Draw()
        {
            Point formCoordinates = currentGaze;
            crosshairImage = GetCrossHairImage((CrossHair)Program.readSettings.Crosshair);

            Refresh();
            graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
            Bitmap image = (Bitmap)crosshairImage;
            int crossHairX = (formCoordinates.X - (image.Width / 2));
            int crossHairY = (formCoordinates.Y - (image.Height / 2));

            Rectangle Dimensions = new Rectangle(crossHairX, crossHairY, image.Width, image.Height);
            graphics.DrawImage(image, Dimensions);

            history.Add(currentGaze);

        }

        public void ClearForm()
        {
            Refresh();
            Hide();
            smoother = CreateSmoother();
        }
    }
}
