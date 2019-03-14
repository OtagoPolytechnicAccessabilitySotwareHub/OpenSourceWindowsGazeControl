using EyeXFramework.Forms;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GazeToolBar
{

    //The program states
    public enum SystemState { Wait, ActionButtonSelected, Zooming, ZoomWait, ApplyAction, ScrollWait }

    //The actions that can be performed by the program
    public enum ActionToBePerformed { RightClick, LeftClick, DoubleClick, Scroll, none }

    /*
        * The State manager is the main control for the program
     */
    public class StateManager
    {
        //The timer that runs the program, this is run every ms
        private Timer ControlTimer;

        //This is the form that appears when clicking on an area of the screen
        private ZoomLens zoomForm;

        //This controls the magnification on the ZoomLens
        //private ZoomMagnifier magnifier;

        /*
         * Testing ZoomMagnifierCentered
         */
        private ZoomMagnifier magnifier;

        //Monitor Gaze fixation data and raise systems flag when this occurs.
        private FixationDetection fixationWorker;

        //Controls the scrolling functionality
        private ScrollControl scrollWorker;

        //Controls the shortcut keys
        private ShortcutKeyWorker shortcutKeyWorker;


        public StateManager(ShortcutKeyWorker shortcutKeyWorker, ScrollControl scrollWorker,
            FixationDetection fixationWorker)
        {
            /*
             * Set up the timer.
             *      - The timer will run every milisecond it can
             *      - The timer will call the RunCycle method every time it ticks
             */
            ControlTimer = new Timer();
            ControlTimer.Interval = 1;
            ControlTimer.Enabled = true;
            ControlTimer.Tick += RunCycle;

            //Setup the zoom form
            zoomForm = new ZoomLens();
            

            this.scrollWorker = scrollWorker;
            this.fixationWorker = fixationWorker;
            this.shortcutKeyWorker = shortcutKeyWorker;

            /*
             * for testing centered
            
            magnifier = CreateMagnifier();

            *
            */
            if (Program.readSettings.dynamicZoom)
            {
                magnifier = new ZoomMagnifier(zoomForm, fixationWorker);
            }
            else
            {
                magnifier = new ZoomMagnifierCentered(zoomForm, fixationWorker);
            }

        }


        /*
            * Runs evey timer tick, updates the state then applies the action
         */
        public void RunCycle(Object sender, EventArgs e)
        {
            UpdateState();
            DoAction();
        }

        /*
            * Creates the zoom magnifier 
         */
        private ZoomMagnifier CreateMagnifier()
        {
            return new ZoomMagnifier(zoomForm, fixationWorker); //TODO: remove the need for the point here
        }

        /*
             * Applies the current state's action, called at each timer tick
        */
        public void DoAction()
        {
            switch (SystemFlags.currentState)
            {
                case SystemState.Wait:
                    DoActionWait();
                    break;
                case SystemState.ActionButtonSelected:
                    DoActionButtonSelected();
                    break;
                case SystemState.Zooming:
                    DoActionZooming();
                    break;
                case SystemState.ZoomWait:
                    DoActionZoomWait();
                    break;
                case SystemState.ScrollWait:
                    DoActionScrollWait();
                    break;
                case SystemState.ApplyAction:
                    DoActionApply();
                    break;
            }
        }

        /*
            * Will switch the current state and see if it needs to change
        */
        public void UpdateState()
        {
            switch (SystemFlags.currentState)
            {
                case SystemState.Wait:
                    UpdateWaitState();
                    break;
                case SystemState.ActionButtonSelected:
                    UpdateActionButtonSelectedState();
                    break;
                case SystemState.Zooming:
                    UpdateZoomingState();
                    break;
                case SystemState.ZoomWait:
                    UpdateZoomWaitState();
                    break;
                case SystemState.ScrollWait:
                    UpdateScrollWaitState();
                    break;
                case SystemState.ApplyAction:
                    UpdateApplyActionState();
                    break;
            }
        }
        
/*---------Do action methods-----------*/

        public void DoActionWait()
        {
            resetColorButton();
        }

        public void DoActionButtonSelected()
        {
            if (!SystemFlags.fixationRunning)
            {
                fixationWorker.StartDetectingFixation();
                SystemFlags.fixationRunning = true;
            }

            //draws the crosshairs at point of gaze
            runZoomForm(fixationWorker.getXY());
        }

        /*
            *   DoActionZooming happens when the fixationTimer in fixationWorker reaches it's limit
            *   and SystemFlags.hasGaze is set to true Happens just once.
        */
        public void DoActionZooming()
        {
            
            if (SystemFlags.shortCutKeyPressed)//if a user defined click key is pressed
            {
                magnifier.PlaceZoomWindow(shortcutKeyWorker.GetXY());
            }
            else
            {
                magnifier.PlaceZoomWindow(fixationWorker.getXY());
            }

            SystemFlags.shortCutKeyPressed = false;
            SystemFlags.hasGaze = false;
            SystemFlags.fixationRunning = false;

            if(!Program.readSettings.dynamicZoom)
            {

                Point p1 = Utils.DividePoint(magnifier.Offset, magnifier.MagnifierDivAmount());
                Point p2 = Utils.DividePoint(magnifier.SecondaryOffset, magnifier.MagnifierDivAmount());

                Point o = Utils.SubtractPoints(p1, p2);

                zoomForm.Offset = o;                    // This initiate's the timer for drawing of the user feedback image
                zoomForm.Start();
                zoomForm.Show();
                zoomForm.CrossHairPos = magnifier.GetLookPosition();
            }

        }

        public void DoActionZoomWait()
        {
            if (!SystemFlags.fixationRunning)
            {
                fixationWorker.StartDetectingFixation();
                SystemFlags.fixationRunning = true;
            }
            //runZoomForm(fixationWorker.getXY());// magnifier.GetLookPosition());
            runZoomForm(magnifier.GetLookPosition());
        }

        public void DoActionScrollWait()
        {
            //nofu
        }

        /*
             * Calls PerformAction to do the action the user has selected
             * And resets the Zoomlens and ZoomMagnifier
        */ 
        public void DoActionApply()
        {

            Point lookPosition = magnifier.GetLookPosition();

            zoomForm.ResetZoomLens();
            magnifier.ResetZoomValue();
            magnifier.Stop();

            performAction(SystemFlags.actionToBePerformed, lookPosition); // fixationWorker.getXY());
        }

/*
 *------^^-Do action methods end-^^------------------
 *------vv-Update state methods--vv------------------
 */

        /*
             *  Called from UpdateState() when the system state is in the Wait phase
        */
        public void UpdateWaitState()
        {

            if (SystemFlags.actionButtonSelected) //If a button has been selected in the toolbar
            {
                SetState(SystemState.ActionButtonSelected);
                SystemFlags.actionButtonSelected = false;
            }
            else if (SystemFlags.shortCutKeyPressed)    //if a shortcut key was pressed
            {
                magnifier.ResetZoomValue();
                SetState(SystemState.Zooming);
            }
        }

        /*
             *  Called from UpdateState() when the system state is in the ActionButtonSelected phase
        */
        public void UpdateActionButtonSelectedState()
        {
            SystemFlags.hasSelectedButtonColourBeenReset = false;
            if (SystemFlags.hasGaze)
            {
                SetState(SystemState.Zooming);
            }
            else if (SystemFlags.timeOut)
            {
                EnterWaitState();
                SystemFlags.timeOut = false;
            }
        }

        /*
             *  Called from UpdateState() when the system state is in the Zooming phase
        */
        public void UpdateZoomingState()
        {
            if (SystemFlags.actionToBePerformed == ActionToBePerformed.Scroll)
            {
                SetState(SystemState.ApplyAction);
            }
            else
            {
                SetState(SystemState.ZoomWait);
            }
        }

        /*
             *  Called from UpdateState() when the system state is in the ZoomWait phase
        */
        public void UpdateZoomWaitState()
        {
            
            if (SystemFlags.hasGaze)   //if the second zoomGaze has happed an action needs to be performed
            {
                SetState(SystemState.ApplyAction);
            }
            else if (SystemFlags.timeOut)
            {
                EnterWaitState();
                zoomForm.ResetZoomLens();
            }
        }

        /*
            *  Called from UpdateState() when the system state is in the ApplyAction phase
        */
        public void UpdateApplyActionState()
        {
            resetColorButton();


            if (SystemFlags.scrolling)
            {
                SetState(SystemState.ScrollWait);
            }
            else
            {
                EnterWaitState();
            }
        }

        /*
             *  Called from UpdateState() when the system state is in the ScrollWait phase
        */
        public void UpdateScrollWaitState()
        {
            if (!SystemFlags.scrolling)
            {
                EnterWaitState();
            }
        }


        /*---------End of update state methods--------------*/


        /*
            * Method to do the selected action on DoActionApply
        */

        private void performAction(ActionToBePerformed action, Point fixationPoint)
        {
            switch(action)
            {
                case ActionToBePerformed.LeftClick:
                    VirtualMouse.LeftMouseClick(fixationPoint.X, fixationPoint.Y);
                    break;
                case ActionToBePerformed.RightClick:
                    VirtualMouse.RightMouseClick(fixationPoint.X, fixationPoint.Y);
                    break;
                case ActionToBePerformed.DoubleClick:
                    VirtualMouse.LeftDoubleClick(fixationPoint.X, fixationPoint.Y);
                    break;
                case ActionToBePerformed.Scroll:
                    SystemFlags.currentState = SystemState.ScrollWait;
                    SystemFlags.scrolling = true;
                    VirtualMouse.SetCursorPos(fixationPoint.X, fixationPoint.Y);
                    scrollWorker.StartScroll();
                    break;
            }
        }

        /*
             * Sets the current system state
             * Generally this is only called from within the UpdateState methods
         */
        public void SetState(SystemState newState)
        {
            SystemFlags.currentState = newState;
        }

        /*
             * The work that needs to be done when entering the wait state
        */
        public void EnterWaitState()
        {
            SystemFlags.fixationRunning = false;
            SystemFlags.actionButtonSelected = false;
            SystemFlags.fixationRunning = false;
            SystemFlags.hasGaze = false;
            SystemFlags.timeOut = false;
            fixationWorker.IsZoomerFixation(false);
            SetState(SystemState.Wait);
            magnifier.Stop();
        }

        private void runZoomForm(Point fixationPoint)
        {
            zoomForm.Start();
            zoomForm.Show();
            zoomForm.CrossHairPos = fixationPoint;
        }

        private void resetColorButton()
        {
            if (SystemFlags.hasSelectedButtonColourBeenReset == false)
            {
                SystemFlags.hasSelectedButtonColourBeenReset = true;
            }
        }

    public void ResetMagnifier()
        {
            if(Program.readSettings.dynamicZoom)
            {
                magnifier = new ZoomMagnifier(zoomForm, fixationWorker);
            }
            else
            {
                magnifier = new ZoomMagnifierCentered(zoomForm, fixationWorker);
            }
        }
        /*
            *Allows the settings to update fixationDetection fixationDetectionTimeOutLength
            * and timeOutTimer intterval
        */
        public void trackBarFixTimeOut(int FixationTimeOutLength, int timeOutTimerInterval)
        {
            fixationWorker.UpdateTimeOut(FixationTimeOutLength, timeOutTimerInterval);

        }

        /*
            *Allows the settings to update fixationDetection fixationDetectionTimeLength
            * and timer interval
        */

        public void trackBarFixTimeLength(int fixationDetectionTimeLength, int fixationTimerInterval)
        {
            fixationWorker.UpdateTimeLength(fixationDetectionTimeLength, fixationTimerInterval);
        }

    }
}
