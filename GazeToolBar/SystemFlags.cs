using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GazeToolBar
{
    /*
     * Global flags used across the program
     */
    public static class SystemFlags
    {
        //If the keyboard is up
        public static bool isKeyBoardUp { get; set; }

        //If the keyboard has been hidden for a button click
        public static bool isKeyboardWaiting { get; set; }

        //If an action button has been selected
        public static bool actionButtonSelected { get; set; }

        //The action that needs to be performed
        public static ActionToBePerformed actionToBePerformed { get; set; }

        //The current system state
        public static SystemState currentState { get; set; }

        //If the fixation detection has found a fixation
        public static bool hasGaze { get; set; }

        //If the fixation detection has NOT found a gaze and it has timed out
        public static bool timeOut { get; set; }

        //If the fixation detection is currently running
        public static bool fixationRunning { get; set; }

        //TODO: remove for new sidebar
        //If the sidebar color has been set back to normal
        public static bool hasSelectedButtonColourBeenReset { get; set; }

        //If the program is scrolling
        public static bool scrolling { get; set; }

        //If a shortcut key has been pressed
        public static bool shortCutKeyPressed { get; set; }
    }
}
