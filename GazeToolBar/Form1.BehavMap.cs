using EyeXFramework;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace GazeToolBar
{
    /*
        Date: 17/05/2016
        Name: Derek Dai
        Description: Partial class of form1, so just make it more clear
    */
    partial class Form1
    {
        /// <summary>
        /// Connect behave map with buttons
        /// and add event to the behave map
        /// </summary>
        /// 

        private int delayBeforeButtonSelected = 1000;
        private void connectBehaveMap()
        {
            eyeXHost.Connect(bhavMap);
            eyeXHost.Connect(bhavMapHLCurrentGazeOnBT);

            
            //Will change later
            bhavMap.Add(btnDoubleClick, new GazeAwareBehavior(OnBtnDoubleClick) { DelayMilliseconds = delayBeforeButtonSelected });
            bhavMap.Add(btnRightClick, new GazeAwareBehavior(OnBtnRightClick) { DelayMilliseconds = delayBeforeButtonSelected });
            bhavMap.Add(btnSingleLeftClick, new GazeAwareBehavior(OnBtnSingleClick) { DelayMilliseconds = delayBeforeButtonSelected });
            bhavMap.Add(btnSettings, new GazeAwareBehavior(OnBtnSettings) { DelayMilliseconds = delayBeforeButtonSelected });
            bhavMap.Add(btnScoll, new GazeAwareBehavior(OnBtnScroll) { DelayMilliseconds = delayBeforeButtonSelected });
            bhavMap.Add(btnKeyboard, new GazeAwareBehavior(OnBtnKeyboard) { DelayMilliseconds = delayBeforeButtonSelected });
            bhavMap.Add(btnMic, new GazeAwareBehavior(OnBtnMicClick) { DelayMilliseconds = delayBeforeButtonSelected });
            //bhavMap.Add(btnDragAndDrop, new GazeAwareBehavior(OnBtnDragAndDrop) { DelayMilliseconds = delayBeforeButtonSelected });

            bhavMap.Add(pnlHiLteRightClick, new GazeAwareBehavior(OnGazeChangeBTColour));
            bhavMap.Add(pnlHighLightDoubleClick, new GazeAwareBehavior(OnGazeChangeBTColour));
            //bhavMap.Add(pnlHighLightDragAndDrop, new GazeAwareBehavior(OnGazeChangeBTColour));
            bhavMap.Add(pnlHighLightScrol, new GazeAwareBehavior(OnGazeChangeBTColour));
            bhavMap.Add(pnlHighLightSettings, new GazeAwareBehavior(OnGazeChangeBTColour));
            bhavMap.Add(pnlHighLightSingleLeft, new GazeAwareBehavior(OnGazeChangeBTColour));
            bhavMap.Add(pnlHighLightKeyboard, new GazeAwareBehavior(OnGazeChangeBTColour));
            bhavMap.Add(pnlHighLightMic, new GazeAwareBehavior(OnGazeChangeBTColour));

            //TESTING START===============================================================
            btnDoubleClick.MouseEnter += new EventHandler(common_MouseEnter);
            btnKeyboard.MouseEnter += new EventHandler(common_MouseEnter);
            btnMic.MouseEnter += new EventHandler(common_MouseEnter);
            btnRightClick.MouseEnter += new EventHandler(common_MouseEnter);
            btnScoll.MouseEnter += new EventHandler(common_MouseEnter);
            btnSingleLeftClick.MouseEnter += new EventHandler(common_MouseEnter);
            btnSettings.MouseEnter += new EventHandler(common_MouseEnter);

            btnDoubleClick.MouseLeave += new EventHandler(common_MouseLeave);
            btnKeyboard.MouseLeave += new EventHandler(common_MouseLeave);
            btnMic.MouseLeave += new EventHandler(common_MouseLeave);
            btnRightClick.MouseLeave += new EventHandler(common_MouseLeave);
            btnScoll.MouseLeave += new EventHandler(common_MouseLeave);
            btnSingleLeftClick.MouseLeave += new EventHandler(common_MouseLeave);
            btnSettings.MouseLeave += new EventHandler(common_MouseLeave);

            btnDoubleClick.MouseHover += new EventHandler(common_MouseHover);
            btnKeyboard.MouseHover += new EventHandler(common_MouseHover);
            btnMic.MouseHover += new EventHandler(common_MouseHover);
            btnRightClick.MouseHover += new EventHandler(common_MouseHover);
            btnScoll.MouseHover += new EventHandler(common_MouseHover);
            btnSingleLeftClick.MouseHover += new EventHandler(common_MouseHover);
            btnSettings.MouseHover += new EventHandler(common_MouseHover);
            //TESTING END=================================================================
        }


        //toggle border on and off on gaze to gaze to give feed back.
        private void OnGazeChangeBTColour(object s, GazeAwareEventArgs e)
        {
            var sentButton = s as Panel;
            if(sentButton != null)
            {
                sentButton.BackColor = (e.HasGaze) ? Color.Red : Color.Black;
            }
        }


        private void OnBtnDoubleClick(object sender, GazeAwareEventArgs e)
        {
            if (e.HasGaze)
            {
                //Reset the button color to its origin color
                resetButtonsColor();
                //Set this button to other color, so people know this button has selected
                btnDoubleClick.BackColor = Constants.SelectedColor;
                //Click this button
                btnDoubleClick.PerformClick();
            }
        }

        private void OnBtnRightClick(object sender, GazeAwareEventArgs e)
        {
            if (e.HasGaze)
            {
                resetButtonsColor();
                btnRightClick.BackColor = Constants.SelectedColor;
                btnRightClick.PerformClick();
            }
        }

        private void OnBtnSingleClick(object sender, GazeAwareEventArgs e)
        {
            if (e.HasGaze)
            {
                resetButtonsColor();
                btnSingleLeftClick.BackColor = Constants.SelectedColor;
                btnSingleLeftClick.PerformClick();
            }
        }

        private void OnBtnSettings(object sender, GazeAwareEventArgs e)
        {
            if (e.HasGaze)
            {
                resetButtonsColor();
                btnSettings.PerformClick();
            }
        }

        private void OnBtnScroll(object sender, GazeAwareEventArgs e)
        {
            if (e.HasGaze)
            {
                resetButtonsColor();
                btnScoll.BackColor = Constants.SelectedColor;
                btnScoll.PerformClick();
            }
        }

        private void OnBtnKeyboard(object sender, GazeAwareEventArgs e)
        {
            if (e.HasGaze)
            {
                resetButtonsColor();
                btnKeyboard.PerformClick();
            }
        }

        private void OnBtnMicClick(object sender, GazeAwareEventArgs e)
        {
            if (e.HasGaze)
            {
                resetButtonsColor();
                btnMic.BackColor = Constants.SelectedColor;
                btnMic.PerformClick();
            }
        }

        //private void OnBtnDragAndDrop(object sender, EventArgs e)
        //{
        //    resetButtonsColor();
        //    btnDragAndDrop.BackColor = ValueNeverChange.SelectedColor;
        //    btnDragAndDrop.PerformClick();
        //}

        public void resetButtonsColor()
        {
            ResetBtnBackcolor(btnSingleLeftClick, btnDoubleClick, btnRightClick, btnSettings, btnScoll, btnKeyboard, btnMic);
        }

        /// <summary>
        /// By calling this method all the buttons that passed in will be reset its color
        /// </summary>
        /// <param name="button">Buttons that will be reset on</param>
        void ResetBtnBackcolor(params Button[] button)
        {
            foreach (Button b in button)
            {
                b.BackColor = Color.Black;
            }
        }

        void common_MouseEnter(object sender, EventArgs e)
        {
            Button button = sender as Button;
            button.BackColor = Color.Red;
        }

        void common_MouseLeave(object sender, EventArgs e)
        {
            Button button = sender as Button;
            button.BackColor = Color.Transparent;
        }

        void common_MouseHover(object sender, EventArgs e)
        {
            Button button = sender as Button;
            button.PerformClick();
        }
    }
}
