﻿using Karna.Magnification;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

/*
 * I think it would be better to have a base class from which ZoomMagnifier and ZoomMagnifierCentered
 * Inherit, rather than having ZoomMagnifierCentered inherit from ZoomMagnifier.
 */

namespace GazeToolBar
{
    public class ZoomMagnifierCentered : ZoomMagnifier
    {
        private int FORM_WIDTH { get; set; }
        private int FORM_HEIGHT { get; set;}

        private Point SecondaryOffset;
        int leftPoint = 0;
        int topPoint = 0;

        public ZoomMagnifierCentered(Form displayform, FixationDetection fixationWorker)
            : base(displayform, fixationWorker)
        {
            ZOOM_MAX = Program.readSettings.maxZoom;
            FORM_WIDTH = Program.readSettings.zoomWindowSize * 100;
            FORM_HEIGHT = FORM_WIDTH / 2;
            SecondaryOffset = new Point(0, 0);
        }

        public override void PlaceZoomWindow(Point fixationPoint)
        {
            this.FixationPoint = fixationPoint;
            Rectangle screenBounds = Screen.FromControl(form).Bounds;
            int offsetX = 0;
            offsetX = screenBounds.Left;

            updateTimer.Enabled = true;
            form.Width = FORM_WIDTH;
            form.Height = FORM_HEIGHT;
            int halfTop = screenBounds.Height / 2;
            int halfWidth = screenBounds.Width / 2;

            if (fixationPoint.X >= halfWidth)
            {
                leftPoint = halfWidth;
            }
            else
            {
                leftPoint = 0;
            }
            if (fixationPoint.Y >= halfTop)
            {
                topPoint = halfTop;
            }
            else
            {
                topPoint = 0;
            }

            form.Left = leftPoint;
            form.Top = topPoint;

            FORM_WIDTH = screenBounds.Width / 2;
            FORM_HEIGHT = screenBounds.Height / 2;
            //form.Left = offsetX + (screenBounds.Right / 2) - (form.Width / 2);
            //form.Top = (screenBounds.Bottom / 2) - (form.Height / 2);

            int dX = fixationPoint.X - (form.Left + FORM_WIDTH / 2);
            int dY = fixationPoint.Y - (form.Top + FORM_HEIGHT / 2);

            Offset = new Point(dX, dY);
        }

        protected override void UpdateZoomPosition()
        {
            //If the magnifier is not setup correctly (will crash otherwise)
            if ((!hasInitialized) || (hwndMag == IntPtr.Zero))
            {
                return;
            }

            sourceRect = new RECT();
            Point zoomPosition = Utils.SubtractPoints(GetZoomPosition(FixationPoint), Offset);
            Rectangle screenBounds = Screen.FromControl(form).Bounds;

            //difference
            int offsetX = 0;
            offsetX = screenBounds.Left;
            form.Width = FORM_WIDTH;
            form.Height = FORM_HEIGHT;

            //form.Left = Math.Abs(offsetX) + (screenBounds.Right / 2) - (form.Width / 2);
            //form.Top =  (screenBounds.Bottom / 2) - (form.Height / 2);
            form.Left = leftPoint;
            form.Top = topPoint;

            int dX = FixationPoint.X - (form.Left + FORM_WIDTH / 2);
            int dY = FixationPoint.Y - (form.Top + FORM_HEIGHT / 2);

            Offset = new Point(dX, dY);

            //

            //Magnified width and height
            int width = (int)(form.Width / Magnification);
            int height = (int)(form.Height / Magnification);

            //Zoom rectangle position
            sourceRect.left = zoomPosition.X - (width / 2);
            sourceRect.top = zoomPosition.Y - (height / 2);

            int inLeft = sourceRect.left;
            int inTop = sourceRect.top;
            
            sourceRect.left = Clamp(sourceRect.left, 0, screenBounds.Width - width);
            sourceRect.top = Clamp(sourceRect.top, 0, screenBounds.Height - height);
            
            int fnLeft = sourceRect.left - inLeft;
            int fnTop = sourceRect.top - inTop;

            if (SecondaryOffset.X == 0 && SecondaryOffset.Y == 0)
            {
           //     MessageBox.Show(SecondaryOffset.X + " " + SecondaryOffset.Y + " " + inLeft + " " + fnLeft);

                SecondaryOffset = new Point(fnLeft, fnTop);
            }

            Utils.Print("SecOffset- " + SecondaryOffset);

            NativeMethods.MagSetWindowSource(hwndMag, sourceRect);  //Sets the source of the zoom
            NativeMethods.InvalidateRect(hwndMag, IntPtr.Zero, true); // Force redraw.
        }

        public override int MagnifierDivAmount()
        {
            return 1;
        }

        public override void ResetZoomValue()
        {
            //base.ResetZoomValue();

            SecondaryOffset = new Point(0, 0);
            Magnification = ZOOM_MAX;

        }
        public override Point GetLookPosition()
        {
            Point startPoint = new Point(sourceRect.left, sourceRect.top);
            Point actualLook = CurrentLook;
            Point formPos = new Point(form.Left, form.Top);
            Point adjustedPoint = Utils.SubtractPoints(actualLook, formPos);
            Point magAdjust = new Point((int)(adjustedPoint.X / ZOOM_MAX), (int)(adjustedPoint.Y / ZOOM_MAX));

            Point finalPoint = Utils.AddPoints(magAdjust, startPoint);

            //Console.WriteLine("X: " + finalPoint.X + "\nY: " + finalPoint.Y);
            //  Point finalPoint = adjustedPoint;//Utils.SubtractPoints(Utils.AddPoints(startPoint, adjustedPoint), 1);
            

            return finalPoint;
        }
    }
}
