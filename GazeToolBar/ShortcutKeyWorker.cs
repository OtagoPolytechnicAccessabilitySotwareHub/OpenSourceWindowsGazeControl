using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EyeXFramework;
using Tobii.EyeX.Framework;
using Tobii.EyeX.Client;
using System.Windows.Input;
using System.Drawing;
using EyeXFramework.Forms;
using System.Net.Sockets;
using System.IO;

namespace GazeToolBar
{/*
 *  Class: ShortcutKeyWorker
 *  Name: Richard Horne
 *  Date: 11/11/2016
 *  Description: Check for keyboard key press events, and on a key press event if mapped to a GazeToolbarFunction signal state machine to proceed with requested function.
 */

    public class ShortcutKeyWorker
    {
        //Gazepoint variables
        const int ServerPort = 4242;
        const string ServerAddr = "127.0.0.1";

        int startindex, endindex;
        TcpClient gp3_client;
        NetworkStream data_feed;
        StreamWriter data_write;
        String incoming_data = "";
        double time_val = 0;
        double fpogx = 0;
        double fpogy = 0;
        double resX = 0;
        double resY = 0;
        int fpog_valid;

        //Fields
        GazePointDataStream gazeStream;
        EventHandler<GazePointEventArgs> gazeDel;

        double currentGazeLocationX;
        double currentGazeLocationY;

       public  Dictionary<ActionToBePerformed, String> keyAssignments { get; set; }

        KeyboardHook keyBoardHook;
        public ShortcutKeyWorker(KeyboardHook KeyboardObserver, Dictionary<ActionToBePerformed, String> KeyAssignments, FormsEyeXHost EyeXHost)//, Dictionary<EToolBarFunction, String> KeyAssignments)
        {
            keyBoardHook = KeyboardObserver;
            keyBoardHook.OnKeyPressed += RunKeyFunction;

            keyAssignments = KeyAssignments;

            //Connect to eyeX engine gaze stream. 
            gazeStream = EyeXHost.CreateGazePointDataStream(GazePointDataMode.LightlyFiltered);
            //Create gate points event handler delegate
            gazeDel = new EventHandler<GazePointEventArgs>(updateGazeCoodinates);
            //register delegate with gaze data stream next event.
            gazeStream.Next += gazeDel;

            //Gazepoint Initialization Start==============================

            // Try to create client object, return if no server found
            try
            {
                gp3_client = new TcpClient(ServerAddr, ServerPort);
            }
            catch (Exception e)
            {
                Console.WriteLine("Failed to connect with error: {0}", e);
                return;
            }

            // Load the read and write streams
            data_feed = gp3_client.GetStream();
            data_write = new StreamWriter(data_feed);

            // Setup the data records
            data_write.Write("<SET ID=\"ENABLE_SEND_TIME\" STATE=\"1\" />\r\n");
            data_write.Write("<SET ID=\"ENABLE_SEND_POG_FIX\" STATE=\"1\" />\r\n");
            data_write.Write("<SET ID=\"ENABLE_SEND_CURSOR\" STATE=\"1\" />\r\n");
            data_write.Write("<SET ID=\"ENABLE_SEND_DATA\" STATE=\"1\" />\r\n");

            // Flush the buffer out the socket
            data_write.Flush();
            //Gazepoint Initialization End================================

        }

        public void StopKeyboardWorker()
        {
            keyBoardHook.OnKeyPressed -= RunKeyFunction;
        }
        public void StartKeyBoardWorker(){
            keyBoardHook.OnKeyPressed += RunKeyFunction;
        }

        //Convert pressed key into a string representation, then check if that key has been assigned to any of the functions stored 
        //in the keyAssignments dictionary, if found signal state manager to proceed with function
        public void RunKeyFunction(object o, HookedKeyboardEventArgs pressedKey)
        {

            String keyString = pressedKey.KeyPressed.ToString();

            if (keyString == keyAssignments[ActionToBePerformed.LeftClick])
            {
                SystemFlags.shortCutKeyPressed = true;
                
                SystemFlags.actionToBePerformed = ActionToBePerformed.LeftClick;
            }
            else if (keyString == keyAssignments[ActionToBePerformed.RightClick])
            {
                SystemFlags.shortCutKeyPressed = true;
                
                SystemFlags.actionToBePerformed = ActionToBePerformed.RightClick;
            }
            else if (keyString == keyAssignments[ActionToBePerformed.DoubleClick])
            {
                SystemFlags.shortCutKeyPressed = true;
       
                SystemFlags.actionToBePerformed = ActionToBePerformed.DoubleClick;
            }
            else if (keyString == keyAssignments[ActionToBePerformed.Scroll])
            {
                
                SystemFlags.shortCutKeyPressed = true;
                SystemFlags.actionToBePerformed = ActionToBePerformed.Scroll;
            }

        }


        private void updateGazeCoodinates(object o, GazePointEventArgs currentGaze)
        {           
            //#####currently being tested with gazepoint
            //Save the users current gaze location.
            currentGazeLocationX = currentGaze.X;
            currentGazeLocationY = currentGaze.Y;
        }
        
        //method to use for thread that will contiously transmit gazepoint data stream
        private void tester()
        {
            do
            {
                fpogx = 0;
                int ch = data_feed.ReadByte();
                if (ch != -1)
                {
                    incoming_data += (char)ch;

                    // find string terminator ("\r\n") 
                    if (incoming_data.IndexOf("\r\n") != -1)
                    {
                        // only process DATA RECORDS, ie <REC .... />
                        if (incoming_data.IndexOf("<REC") != -1)
                        {

                            // Process incoming_data string to extract FPOGX, FPOGY, etc...
                            startindex = incoming_data.IndexOf("TIME=\"") + "TIME=\"".Length;
                            endindex = incoming_data.IndexOf("\"", startindex);
                            time_val = Double.Parse(incoming_data.Substring(startindex, endindex - startindex));

                            startindex = incoming_data.IndexOf("FPOGX=\"") + "FPOGX=\"".Length;
                            endindex = incoming_data.IndexOf("\"", startindex);
                            fpogx = Double.Parse(incoming_data.Substring(startindex, endindex - startindex));

                            startindex = incoming_data.IndexOf("FPOGY=\"") + "FPOGY=\"".Length;
                            endindex = incoming_data.IndexOf("\"", startindex);
                            fpogy = Double.Parse(incoming_data.Substring(startindex, endindex - startindex));

                            startindex = incoming_data.IndexOf("FPOGV=\"") + "FPOGV=\"".Length;
                            endindex = incoming_data.IndexOf("\"", startindex);
                            fpog_valid = Int32.Parse(incoming_data.Substring(startindex, endindex - startindex));
                        }
                        incoming_data = "";
                    }
                }
                resX = fpogx * 1920;
                resY = fpogy * 1080;
                double perX = fpogx * 100;
                double perY = fpogy * 100;
            } while (0 == 0);

        }

        //returns the users current gaze as a point.
        public Point GetXY()
        {
            return new Point((int)currentGazeLocationX, (int)currentGazeLocationY);
           
        }

    }
}
