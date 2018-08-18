using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GazeToolBar
{
    //TODO: Remove "_new" from anything once done
    //This will probably not get used :( but it's so my

    //The program states
    public enum SystemState_new { Wait, ActionButtonSelected, Zooming, ZoomWait, ApplyAction, ScrollWait }

    //The actions that can be performed by the program
    public enum ActionToBePerformed_new { RightClick, LeftClick, DoubleClick, Scroll }

    /*
        * The State manager is the main control for the program
     */
    public class StateManager_new
    {
        //The timer that runs the program, this is run every ms
        private Timer ControlTimer;

        //This is the form that appears when clicking on an area of the screen
        private ZoomLens zoomForm;

        //This controls the magnification on the ZoomLens
        private ZoomMagnifier magnifier;

        //Monitor Gaze fixation data and raise systems flag when this occurs.
        private FixationDetection fixationWorker;

        public StateManager_new()
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
            magnifier = CreateMagnifier();
            fixationWorker = new FixationDetection();
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
            return new ZoomMagnifierCentered(zoomForm, new System.Drawing.Point()); //TODO: remove the need for the point here
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

        public void DoActionWait()
        {
            if (SystemFlags.hasSelectedButtonColourBeenReset == false)
            {
                SystemFlags.hasSelectedButtonColourBeenReset = true;
            }
        }

        public void DoActionButtonSelected()
        {
            if (!SystemFlags.fixationRunning)
            {
                fixationWorker.StartDetectingFixation();
                SystemFlags.fixationRunning = true;
            }
        }

        public void DoActionZooming()
        {
            //TODO
        }

        public void DoActionZoomWait()
        {
            if (!SystemFlags.fixationRunning)
            {
                fixationWorker.StartDetectingFixation();
                SystemFlags.fixationRunning = true;
            }
        }

        public void DoActionScrollWait()
        {
            //nofu
        }

        public void DoActionApply()
        {
            //TODO
        }


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
        }

        /*
            *Allows the settings to update the max zoom
        */
        public void SetMagnifierMaxZoom(int maxZoom)
        {
            magnifier.MaxZoom = maxZoom;
        }

        /*
            *Allows the settings to update fixationDetection fixationDetectionTimeOutLength
            * and timeOutTimer intterval
        */
        public void trackBarFixTimeOut(int FixationTimeOutLength, int timeOutTimerInterval)
        {
            //fixationWorker.FixationTimeOutLength = FixationTimeOutLength;
            //fixationWorker.timeOutTimer.Interval = timeOutTimerInterval;
            fixationWorker.UpdateTimeOut(FixationTimeOutLength, timeOutTimerInterval);

        }

        /*
            *Allows the settings to update fixationDetection fixationDetectionTimeLength
            * and timer interval
        */

        public void trackBarFixTimeLength(int fixationDetectionTimeLength, int fixationTimerInterval)
        {
            //fixationWorker.FixationDetectionTimeLength = fixationDetectionTimeLength;
            //fixationWorker.fixationTimer.Interval = fixationTimerInterval;
            fixationWorker.UpdateTimeLength(fixationDetectionTimeLength, fixationTimerInterval);
        }
    }
}
