using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Karna.Magnification;

namespace GazeToolBar
{
    class ZoomMagnifierDynamic : ZoomMagnifier
    {
        private int FORM_WIDTH { get; set; }
        private int FORM_HEIGHT { get; set; }

        Point startGazePoint;
        int leftPoint = 0;
        int topPoint = 0;


        public ZoomMagnifierDynamic(Form displayform, FixationDetection fixationWorker)
    : base(displayform, fixationWorker)
        {
            trial = true;
            startGazePoint = FixationPoint;
        }

        public override void PlaceZoomWindow(Point fixationPoint)
        {
            FixationPoint = fixationPoint;

            updateTimer.Enabled = true;
            form.Width = Program.readSettings.zoomWindowSize * 100;
            form.Height = Program.readSettings.zoomWindowSize * 80;
            //Centers the zoom form on the circle
            sourceRect.left = FixationPoint.X - (form.Width / 2);
            sourceRect.top = FixationPoint.Y - (form.Width / 2);

            screenBounds = Screen.FromControl(form).Bounds;

            form.Left = Clamp((FixationPoint.X - (form.Width / 2)), 0, screenBounds.Width - form.Width);
            form.Top = Clamp((FixationPoint.Y - (form.Width / 2)), 0, screenBounds.Height - form.Height);

            positionSmoother = new FixationSmootherAverage(SMOOTHER_BUFFER);

            ZOOM_MAX = Program.readSettings.maxZoom;
        }

        protected override void UpdateZoomPosition()
        {
            if ((!hasInitialized) || (hwndMag == IntPtr.Zero) || !updateTimer.Enabled)
            {
                return;
            }

            sourceRect.left = Convert.ToInt32(FixationPoint.X - ((form.Width / Magnification) / 2));
            sourceRect.top = Convert.ToInt32(FixationPoint.Y - ((form.Width / Magnification) / 2));

            NativeMethods.MagSetWindowSource(hwndMag, sourceRect); //Sets the source of the zoom


            NativeMethods.InvalidateRect(hwndMag, IntPtr.Zero, true); // Force redraw.
        }

        protected override void startZoom()
        {
            FixationPoint = fixationWorker.getXY();


            zoomPoint = new Point(
            (int)(fixationWorker.getXY().X - ((form.Width / Magnification) / 2)),
            (int)(fixationWorker.getXY().Y - ((form.Height / Magnification) / 2))
            );

            //Smoothed point
            Point zoomPointSmoothed = GetPointSmoothed(zoomPoint);



            sourceRect.left = FixationPoint.X - (form.Width / 2);
            sourceRect.top = FixationPoint.Y - (form.Height / 2);

            //ensuring zoom window is on screen
            sourceRect.left = Clamp(sourceRect.left, 0, screenBounds.Width - (int)(form.Width / Magnification));
            sourceRect.top = Clamp(sourceRect.top, 0, screenBounds.Height - (int)(form.Height / Magnification));

            //bottom and right of screen
            sourceRect.right = form.Width;
            sourceRect.bottom = form.Height;
            startGazePoint = FixationPoint;
        }

        protected override void timer_Tick(object sender, EventArgs e)
        {
            if (trial)
            {
                startZoom();
                trial = false;
            }
            Zoom();
            UpdateZoomPosition();
        }


    }
}
