using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GazeToolBar
{
    class ZoomMagnifierDynamic : ZoomMagnifier
    {
        private int FORM_WIDTH { get; set; }
        private int FORM_HEIGHT { get; set; }

        private Point SecondaryOffset;
        int leftPoint = 0;
        int topPoint = 0;

        public ZoomMagnifierDynamic(Form displayform, FixationDetection fixationWorker)
    : base(displayform, fixationWorker)
        {
            ZOOM_MAX = Program.readSettings.maxZoom;
            FORM_WIDTH = Program.readSettings.zoomWindowSize * 100;
            FORM_HEIGHT = FORM_WIDTH / 2;
            SecondaryOffset = new Point(0, 0);
        }

        public override void PlaceZoomWindow(Point fixationPoint)
        {
            //get 
            sourceRect.left = fixationPoint.X;
            sourceRect.top = fixationPoint.Y;

            //Where Person is looking
            FixationPoint = fixationPoint;

            //Start timer to start zooming
            updateTimer.Enabled = true;

            //Create zoom window for zooming.
            form.Width = Program.readSettings.zoomWindowSize * 100;
            form.Height = Program.readSettings.zoomWindowSize * 80;

            //Bounds of the screen
            screenBounds = Screen.FromControl(form).Bounds;

            //Making sure the left and top is within the bounds of the screen
            form.Left = Clamp((FixationPoint.X - (form.Width / 2)), 0, screenBounds.Width - form.Width);
            form.Top = Clamp((FixationPoint.Y - (form.Width / 2)), 0, screenBounds.Height - form.Height);

            //Start smoother to keep smooth with eye
            positionSmoother = new FixationSmootherAverage(SMOOTHER_BUFFER);

            //Get maximum zoom amount
            ZOOM_MAX = Program.readSettings.maxZoom;
        }






    }
}
