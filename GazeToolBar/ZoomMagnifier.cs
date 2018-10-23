﻿using System;
using System.Windows.Forms;
using Karna.Magnification;
using System.Drawing;
using EyeXFramework.Forms;
using EyeXFramework;
using Tobii.EyeX.Framework;
using System.Threading;

namespace GazeToolBar
{
    public class ZoomMagnifier
    {
        protected const int UPDATE_SPEED = 1; //how fast the lens will update

        //TODO: Move these to settings json
        public static bool DO_ZOOM = true;         //Zoom enabled

        public static float ZOOM_SPEED = 0.02F;//005F;    //Amount zoom will increment

        public static float ZOOM_MAX = Program.readSettings.maxZoom;          //Max zoom amount
        public static int SMOOTHER_BUFFER = 5;

        public Point FixationPoint { get; set; }
        public Point Offset { get; set; }  //Offset is the amount of pixels moved when repositioning the form if it is offscreen. It's used to reposition the Fixation point.
        //public Point SecondaryOffset { get; set; }  //Used for the Centered zoom offset from the sides..
        protected Form form;
        protected System.Windows.Forms.Timer updateTimer;
        protected RECT magWindowRect = new RECT();
        protected IntPtr hwndMag;
        public RECT sourceRect;
        FormsEyeXHost eyeXHost;
        GazePointDataStream gazeStream;

        protected bool zooming;

        //protected FixationDetection fixationWorker;
        protected FixationSmootherExponential fixationSmoother;
        protected FixationSmootherAverage positionSmoother;

        public Point CurrentLook { get; set; }
        public float MaxZoom { get; set; } //Max zoom amount

        //public Timer Timer { get { return updateTimer; } }

        protected bool hasInitialized;
        protected float magnification;

        protected FixationDetection fixationWorker;

        protected Rectangle screenBounds;

        public ZoomMagnifier(Form displayform, FixationDetection fixationWorker)//, Point fixationPoint)
        {
            ZOOM_MAX = Program.readSettings.maxZoom;          //Max zoom amount
            Magnification = DO_ZOOM ? 1 : ZOOM_MAX;// Program.readSettings.maxZoom; //Set magnification to the max if not zooming
            form = displayform;
            form.TopMost = true;
            updateTimer = new System.Windows.Forms.Timer();
            //fixationWorker = new FixationDetection();
            //fixationSmoother = (FixationSmootherExponential)fixationWorker.CreateSmoother(SMOOTHER_BUFFER);//new FixationSmootherExponential(SMOOTHER_BUFFER);
            //positionSmoother = new FixationSmootherExponential(SMOOTHER_BUFFER);

            this.fixationWorker = fixationWorker;

            FixationPoint = new System.Drawing.Point();//fixationPoint;
            InitLens();
            sourceRect = new RECT();

            //Event handlers
            form.Resize += new EventHandler(form_Resize);
            form.FormClosing += new FormClosingEventHandler(form_FormClosing);
            updateTimer.Tick += new EventHandler(timer_Tick);

            updateTimer.Interval = UPDATE_SPEED;
            updateTimer.Enabled = false;
            Offset = new Point(0, 0);
            //SecondaryOffset = new Point(0, 0);

            eyeXHost = new FormsEyeXHost();
            eyeXHost.Start();
            gazeStream = eyeXHost.CreateGazePointDataStream(GazePointDataMode.LightlyFiltered);
            gazeStream.Next += (s, e) => SetLook(e.X, e.Y);

            form.Left = -4000;
            form.Top = -5000;
            form.Width = 1;
            form.Height = 1;
            zooming = true;

        }

        private void SetLook(double x, double y)
        {
            CurrentLook = new Point((int)x, (int)y);
        }

        public void InitLens()
        {
            hasInitialized = NativeMethods.MagInitialize();
            if (hasInitialized)
            {
                IntPtr hInst = NativeMethods.GetModuleHandle(null);

                // Create a magnifier control that fills the client area.
                NativeMethods.GetClientRect(form.Handle, ref magWindowRect);
                hwndMag = NativeMethods.CreateWindow((int)ExtendedWindowStyles.WS_EX_CLIENTEDGE,
                    NativeMethods.WC_MAGNIFIER, "Zoom Lens",
                    (int)WindowStyles.WS_CHILD | (int)WindowStyles.WS_VISIBLE,
                    magWindowRect.left, magWindowRect.top, magWindowRect.right, magWindowRect.bottom,
                    form.Handle, IntPtr.Zero, hInst, IntPtr.Zero);

                // Set the magnification factor.
                Transformation matrix = new Transformation(Magnification);
                NativeMethods.MagSetWindowTransform(hwndMag, ref matrix);
            }

        }
        //initialises the zoom window. called from statemanager
        public void PlaceZoomWindow(Point fixationPoint)
        {

            sourceRect.left = fixationPoint.X;
            sourceRect.top =  fixationPoint.Y;

            FixationPoint = fixationPoint;

            updateTimer.Enabled = true;
            form.Width = 400;
            form.Height = 400;

            screenBounds = Screen.FromControl(form).Bounds;

            form.Left = Clamp((FixationPoint.X - (form.Width / 2)), 0, screenBounds.Width - form.Width);
            form.Top = Clamp((FixationPoint.Y - (form.Width / 2)), 0, screenBounds.Height - form.Height);

            positionSmoother = new FixationSmootherAverage(SMOOTHER_BUFFER);
        }

        //updates the portion of the screen the zoom window is looking at.
        private void UpdateZoomPosition()
        {
            if ((!hasInitialized) || (hwndMag == IntPtr.Zero) || !updateTimer.Enabled)
            {
                return;
            }

            Point zoomPoint = new Point(
                (int)(fixationWorker.getXY().X - ((form.Width / Magnification) / 2)),
                (int)(fixationWorker.getXY().Y - ((form.Height / Magnification) / 2))
                );

            Point zoomPointSmoothed = GetPointSmoothed(zoomPoint);

            sourceRect.left = zoomPointSmoothed.X;
            sourceRect.top = zoomPointSmoothed.Y;

            sourceRect.left = Clamp(sourceRect.left, 0, screenBounds.Width - (int)(form.Width / Magnification));
            sourceRect.top = Clamp(sourceRect.top, 0, screenBounds.Height - (int)(form.Height / Magnification));

            sourceRect.right = form.Width;
            sourceRect.bottom = form.Height;
            Console.WriteLine("sourceRect: left: " + sourceRect.left + " top: " + sourceRect.top + " right: " + sourceRect.right);

            //Thread.Sleep(100);

            NativeMethods.InvalidateRect(hwndMag, IntPtr.Zero, true); // Force redraw.
            NativeMethods.MagSetWindowSource(hwndMag, sourceRect);  //Sets the source of the zoom
            
        }
        
        public void Zoom()
        {
            if (DO_ZOOM)
            {
                if (!(Magnification > ZOOM_MAX))
                {
                    Magnification += ZOOM_SPEED;
                }
            }

        }

        //Gets the position that the zoom will be centered on
        public Point GetZoomPosition(Point fixationPoint)
        {
            //GazePoint smoothePosition = Smoother(Utils.AddPoints(FixationPoint, Offset));
            return Utils.AddPoints(fixationPoint, Offset);
        }

        //attempt to smooth the postion zoom is centered on
        //not working yet
        public Point getZoomPositionSmoothed()
        {
            Point position = Utils.AddPoints(FixationPoint, Offset);
            GazePoint smoothPosition = positionSmoother.UpdateAndGetSmoothPoint(position.X, position.Y);
            return new Point((int) smoothPosition.X, (int) smoothPosition.Y);
        }

        public Point GetPointSmoothed(Point fixPoint)
        {
            Point position = Utils.AddPoints(fixPoint, Offset);
            GazePoint smoothPosition = positionSmoother.UpdateAndGetSmoothPoint(position.X, position.Y);
            return new Point((int)smoothPosition.X, (int)smoothPosition.Y);
        }

        //TODO: move to utility class
        //Forces an int to be between two integers
        public int Clamp(int current, int min, int max)
        {
            return (current < min) ? min : (current > max) ? max : current;
        }

        public void ResetZoomValue()
        {
            Offset = new Point(0, 0);

            //SecondaryOffset = new Point(0, 0);
            Magnification = 1;// Program.readSettings.maxZoom;
            MaxZoom = magnification;

            MaxZoom = Program.readSettings.maxZoom; //magnification;

            updateTimer.Enabled = false;
        }

        private void timer_Tick(object sender, EventArgs e)
        {
            Zoom();
            UpdateZoomPosition();
            //UpdatePosition(FixationPoint);
        }

        private void form_FormClosing(object sender, FormClosingEventArgs e)
        {
            updateTimer.Enabled = false;
        }

        private void form_Resize(object sender, EventArgs e)
        {
            if (hasInitialized && (hwndMag != IntPtr.Zero))
            {
                NativeMethods.GetClientRect(form.Handle, ref magWindowRect);
                // Resize the control to fill the window.
                NativeMethods.SetWindowPos(hwndMag, IntPtr.Zero, magWindowRect.left,
                    magWindowRect.top, magWindowRect.right, magWindowRect.bottom, 0);
            }
        }

        //the magnification factor
        public float Magnification
        {
            set
            {
                //if (magnification != value)
                //{
                    magnification = value;
                    //if (magnification > ZOOM_MAX)
                    //{
                    //    magnification = ZOOM_MAX;
                    //}
                    // Set the magnification factor.
                    Transformation matrix = new Transformation(magnification);
                    NativeMethods.MagSetWindowTransform(hwndMag, ref matrix);
                //}
            }
            get { return magnification; }
        }

        public void Stop()
        {
            updateTimer.Enabled = false;
            form.Left = -4000;
            form.Top = -5000;
            form.Width = 1;
            form.Height = 1;
            form.Refresh();
            form.Hide();
        }

        public virtual int MagnifierDivAmount()
        {
            return (int)ZOOM_MAX;
        }


        public Point GetLookPosition()
        {

            Point startPoint = new Point(sourceRect.left, sourceRect.top);
            Point actualLook = CurrentLook;
            Point formPos = new Point(form.Left, form.Top);
            Point adjustedPoint = Utils.SubtractPoints(actualLook, formPos);
            //Point magAdjust = new Point((int)(adjustedPoint.X / Magnification), (int)(adjustedPoint.Y / Magnification));
            float magAdjustX = adjustedPoint.X / Magnification;
            float magAdjustY = adjustedPoint.Y / Magnification;

            //Point finalPoint = Utils.AddPoints(magAdjust, startPoint);
            Point finalPoint = new Point((int)magAdjustX + startPoint.X, (int)magAdjustY + startPoint.Y);

            //Point finalPoint = adjustedPoint;//Utils.SubtractPoints(Utils.AddPoints(startPoint, adjustedPoint), 1);
            //  MessageBox.Show(adjustedPoint.X + " " + adjustedPoint.Y + " " + finalPoint.X + " " + finalPoint.Y);

            //Point finalPoint = actualLook;

            //MessageBox.Show(startPoint.ToString());
            //MessageBox.Show(finalPoint.ToString());
            Console.WriteLine("GetLookPosition: X: " +finalPoint.X + " Y: " + finalPoint.Y);
            //return adjustedPoint;
            return finalPoint;
        }
    }
}

