using System;
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
        protected const int UPDATE_SPEED = 150; //how fast the lens will update

        //TODO: Move these to settings json
        public bool DO_ZOOM = Program.readSettings.dynamicZoom;

        public static float ZOOM_SPEED = 0.5F;//0.06F;    //Amount zoom will increment

        public static float ZOOM_MAX;           //Max zoom amount
        public static int SMOOTHER_BUFFER = 10;

        public Point FixationPoint { get; set; }
        public Point Offset { get; set; }  //Offset is the amount of pixels moved when repositioning the form if it is offscreen. It's used to reposition the Fixation point.
        public Point SecondaryOffset { get; set; }  //Used for the Centered zoom offset from the sides..
        protected Form form;
        protected System.Windows.Forms.Timer updateTimer;
        protected RECT magWindowRect = new RECT();
        protected IntPtr hwndMag;
        public RECT sourceRect;
        FormsEyeXHost eyeXHost;
        GazePointDataStream gazeStream;
        protected Point zoomPoint;

        protected FixationSmootherExponential fixationSmoother;
        protected FixationSmootherAverage positionSmoother;

        public Point CurrentLook { get; set; }

        protected bool hasInitialized;
        protected float magnification;

        protected FixationDetection fixationWorker;

        protected Rectangle screenBounds;

        public bool trial = false;

        public ZoomMagnifier(Form displayform, FixationDetection fixationWorker)
        {
            ZOOM_MAX = Program.readSettings.maxZoom;          //Max zoom amount
            Magnification = DO_ZOOM ? 1 : ZOOM_MAX; //Set magnification to the max if not zooming
            form = displayform;
            form.TopMost = true;
            updateTimer = new System.Windows.Forms.Timer();

            this.fixationWorker = fixationWorker;

            FixationPoint = fixationWorker.getXY();
            InitLens();
            sourceRect = new RECT();
            //Event handlers
            form.Resize += new EventHandler(form_Resize);
            form.FormClosing += new FormClosingEventHandler(form_FormClosing);
            updateTimer.Tick += new EventHandler(timer_Tick);

            updateTimer.Interval = UPDATE_SPEED;
            updateTimer.Enabled = false;
            Offset = new Point(0, 0);
            SecondaryOffset = new Point(0, 0);

            eyeXHost = new FormsEyeXHost();
            eyeXHost.Start();
            gazeStream = eyeXHost.CreateGazePointDataStream(GazePointDataMode.LightlyFiltered);
            gazeStream.Next += (s, e) => SetLook(e.X, e.Y);

            form.Left = -4000;
            form.Top = -5000;
            form.Width = 1;
            form.Height = 1;

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

        public virtual void PlaceZoomWindow(Point fixationPoint)
        {
        }

        //updates the portion of the screen the zoom window is looking at.
        protected virtual void UpdateZoomPosition()
        {          
        }

        protected virtual void startZoom()
        {
            ////center of zoom screen
            zoomPoint = new Point(
            (int)(fixationWorker.getXY().X - ((form.Width / Magnification) / 2)),
            (int)(fixationWorker.getXY().Y - ((form.Height / Magnification) / 2))
                    );

            //Smoothed point
            Point zoomPointSmoothed = GetPointSmoothed(zoomPoint);

            //What is displayed in the zoom window
            sourceRect.left = zoomPointSmoothed.X;
            sourceRect.top = zoomPointSmoothed.Y;

            UpdateZoomPosition();
            //ensuring zoom window is on screen
            sourceRect.left = Clamp(sourceRect.left, 0, screenBounds.Width - (int)(form.Width / Magnification));
            sourceRect.top = Clamp(sourceRect.top, 0, screenBounds.Height - (int)(form.Height / Magnification));

            //bottom and right of screen
            sourceRect.right = form.Width;
            sourceRect.bottom = form.Height;
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

        
        public virtual void ResetZoomValue()
        {
            Offset = new Point(0, 0);

            Magnification = 1;
            trial = true;
            updateTimer.Enabled = false;
        }
        

        protected virtual void timer_Tick(object sender, EventArgs e)
        {
            Zoom();
            UpdateZoomPosition();
        }

        private void form_FormClosing(object sender, FormClosingEventArgs e)
        {
            //updateTimer.Enabled = false;
            Stop();
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
                magnification = value;

                // Set the magnification factor.
                Transformation matrix = new Transformation(magnification);
                NativeMethods.MagSetWindowTransform(hwndMag, ref matrix);
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
            trial = true;
            sourceRect = new RECT();
            form.Refresh();
            form.Hide();
        }

        public virtual int MagnifierDivAmount()
        {
            return (int)ZOOM_MAX;
        }


        public virtual Point GetLookPosition()
        {

            Point startPoint = new Point(sourceRect.left, sourceRect.top);
            Point actualLook = CurrentLook;
            Point formPos = new Point(form.Left, form.Top);
            Point adjustedPoint = Utils.SubtractPoints(actualLook, formPos);

            float magAdjustX = adjustedPoint.X / Magnification;
            float magAdjustY = adjustedPoint.Y / Magnification;

            Point finalPoint = new Point((int)magAdjustX + startPoint.X, (int)magAdjustY + startPoint.Y);

            return finalPoint;
        }
    }
}

