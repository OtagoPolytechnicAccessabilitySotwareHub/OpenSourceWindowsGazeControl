using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EyeXFramework;
using Tobii.EyeX.Framework;
using Tobii.EyeX.Client;
using System.Threading;
using EyeXFramework.Forms;
using System.Net.Sockets;
using System.IO;

/*
 *  Class: CustomFixationDataStream
 *  Name: Richard Horne
 *  Date: 11/11/2016
 *  Description: Custom fixation datastream, Monitors stream of XY coordinates of a users gaze, and from this data calculates the standard deviation variance from the gaze average. 
 *  It then raises appropriate events when the users gaze is moving less than a specified threshold.
 */


namespace GazeToolBar
{
    /// <summary>
    /// 
    /// </summary>
    public class CustomFixationDataStream
    {
        //Gaze Data stream to subscribe to.
        GazePointDataStream gazeStream;

        //Ring buffer size
        int bufferSize = 50;
        int bufferCurrentIndex = 0;
        int bufferFullIndex = 0;

        //Fixation variance threshold
        double xFixationThreashold = .9;
        double yFixationThreashold = .3;
        
        //Adjustments to fixation detection, where EyeX Y-Axis data stream becomes unstable when a user is gazing at the top edge of the screen.
        //these settings work well for 21 inch 1680x1050 res screen
        double yFixationCutOffThreasholdWhenGazeAtTopOfScreen = 100;
        double yFixationScreenBoundary;
        double screenBoudaryCutOffPercent = 15;

        public bool ZoomerFixation {get; set;}

        //ring buffer arrays.
        double[] xBuffer;
        double[] yBuffer;



        EFixationStreamEventType fixationState;

        //Settings for local gazpoint server
        const int ServerPort = 4242;
        const string ServerAddr = "127.0.0.1";

        //Gazepoint variables
        int startindex, endindex;
        TcpClient gp3_client;
        NetworkStream data_feed;
        StreamWriter data_write;
        String incoming_data = "";

        //Global variable containing the current gaze average location.
        GazePoint gPAverage;

        //Deceleration of event that is raised when fixation occurs.
        public delegate void CustomFixationEventHandler(object o, CustomFixationEventArgs e);
        public event CustomFixationEventHandler next;

        //Constructor
        public CustomFixationDataStream(FormsEyeXHost EyeXHost)
        {
            //Calculate the amount of pixels away from the top of the screen to set cut of for top of screen threshold adjustment.
            yFixationScreenBoundary = Constants.PRIMARY_SCREEN.Height * (screenBoudaryCutOffPercent / 100);

            gazeStream = EyeXHost.CreateGazePointDataStream(GazePointDataMode.Unfiltered);
            //Create gate points event handler delegate
            EventHandler<GazePointEventArgs> gazeDel = new EventHandler<GazePointEventArgs>(updateGazeCoodinates);
            //register delegate with gaze data stream next event.
            gazeStream.Next += gazeDel;

            gPAverage = new GazePoint();

            xBuffer = new double[bufferSize];
            yBuffer = new double[bufferSize];

            fixationState = EFixationStreamEventType.Waiting;
            ZoomerFixation = false;

            gp3_client = new TcpClient(ServerAddr, ServerPort);

            //GazePoint Initialization
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
        }


        /// <summary>
        /// Method get subscribed to eye tracker gaze event data stream, then runs methods that convert users current gaze into fixation events.
        /// </summary>
        /// <param name="o"></param>
        /// <param name="currentGaze"></param>
        private void updateGazeCoodinates(object o, GazePointEventArgs currentGaze)
        {
            addCoordinateToBuffer(currentGaze.X, currentGaze.Y);

            gPAverage = average();

            generateFixationState(calculateVariance(), currentGaze.Timestamp);
        }


        /// <summary>
        /// Checks input data from variance calculation and raises appropriate event depending on this data and the CustomfixationDetectionStreams current state
        /// </summary>
        /// <param name="gazeVariation"></param>
        /// <param name="timestamp"></param>
          private void generateFixationState(GazePoint gazeVariation, double timestamp)
        {
           //Set pointer to next fixation data bucket.
           CustomFixationEventArgs cpe = null;

           //check where users gaze is, if it is less than yFixationScreenBoundary set yAdjustedThreashold to yFixationCutOffThreasholdWhenGazeAtTopOfScreen
           //To compensate for EyeX's poor accuracy when gazing near top edge of screen.
           double yAdjustedThreashold = gPAverage.Y < yFixationScreenBoundary && !ZoomerFixation ? yFixationCutOffThreasholdWhenGazeAtTopOfScreen : yFixationThreashold;
            

              //Check gaze data variation, current state and create appropriate event. Then set the CustomfixationDetectionStreams state.
            if (fixationState == EFixationStreamEventType.Waiting && gazeVariation.X < xFixationThreashold && gazeVariation.Y < yAdjustedThreashold)
            {
                cpe = new CustomFixationEventArgs(EFixationStreamEventType.Start, timestamp, gPAverage.X, gPAverage.Y);
                fixationState = EFixationStreamEventType.Middle;
            }
            else if (fixationState == EFixationStreamEventType.Middle && gazeVariation.X > xFixationThreashold && gazeVariation.Y > yAdjustedThreashold)
            {
                cpe = new CustomFixationEventArgs(EFixationStreamEventType.End, timestamp, gPAverage.X, gPAverage.Y);
                fixationState = EFixationStreamEventType.Waiting;
            }
            else if (fixationState == EFixationStreamEventType.Middle)
            {
                cpe = new CustomFixationEventArgs(EFixationStreamEventType.Middle, timestamp, gPAverage.X, gPAverage.Y);
            }


              //raise the event.
            if( cpe != null)
            {
                onFixationStateChange(cpe);
            }


        }

        //Method that raises fixation event.
        private void onFixationStateChange(CustomFixationEventArgs newFixation)
        {
            if(next != null)
            {
                next(this, newFixation);
            }
        }



        //add coordinates to ring buffer, check and reset array index when at end of array, increment bufferfullindex to indicate when buffer has been full for the first time, then overwrite previous data.
        private void addCoordinateToBuffer(double x, double y)
        {

            if (bufferCurrentIndex == bufferSize)
            {
                bufferCurrentIndex = 0;
            }

            if (bufferFullIndex != bufferSize)
            {
                bufferFullIndex++;
            }

            xBuffer[bufferCurrentIndex] = x;
            yBuffer[bufferCurrentIndex] = y;

            bufferCurrentIndex++;
        }

        private GazePoint calculateVariance()
        {
            double xTotal = 0;
            double yTotal = 0;

           

            for (int arrayIndex = 0; arrayIndex < bufferFullIndex; arrayIndex++)
            {
                xTotal += Math.Pow(xBuffer[arrayIndex], 2);
                yTotal += Math.Pow(yBuffer[arrayIndex], 2);
            }

            xTotal = xTotal / bufferFullIndex;
            yTotal = yTotal / bufferFullIndex;

            xTotal = Math.Sqrt(xTotal);
            yTotal = Math.Sqrt(yTotal);

            xTotal = xTotal - gPAverage.X;
            yTotal = yTotal - gPAverage.Y;



            return new GazePoint(xTotal, yTotal);

        }


        /// <summary>
        /// Reset fixation data stream to its waiting state, this solves and issue when fixations are in close proximity, by stopping the stream getting stuck in the middle stae of a fixation. 
        /// </summary>
        public void ResetFixationDetectionState()
        {
            fixationState = EFixationStreamEventType.Waiting;
            bufferCurrentIndex = 0;
            bufferFullIndex = 0;
            xBuffer = new double[bufferSize];
            yBuffer = new double[bufferSize];
            Thread.Sleep(100);
        }

        /// <summary>
        /// Calculates the average location of the users gaze, could be combined into calculateVariance() method.
        /// </summary>
        /// <returns>Average of buffers current set of data</returns>
        private GazePoint average()
        {
            double xTotal = 0;
            double yTotal = 0;
            double fpogx = 0;
            double fpogy = 0;

            GazePoint returnSmoothPoint = new GazePoint();

            for (int arrayIndex = 0; arrayIndex < bufferFullIndex; arrayIndex++)
            {
                xTotal += xBuffer[arrayIndex];
                yTotal += yBuffer[arrayIndex];
            }

            //=====================================
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
                        double time_val;                        
                        int fpog_valid;

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
            //=====================================

            double resX = fpogx * 1920;
            double resY = fpogy * 1080;
            double perX = fpogx * 100;
            double perY = fpogy * 100;

            returnSmoothPoint.X = perX / bufferFullIndex;
            returnSmoothPoint.Y = perY / bufferFullIndex;           

            return returnSmoothPoint;
        }

    }
}
