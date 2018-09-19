//-----------------------------------------------------------------------
// Copyright 2014 Tobii Technology AB. All rights reserved.
//-----------------------------------------------------------------------

namespace EyeXFramework
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics.CodeAnalysis;
    using System.IO;
    using System.Linq;
    using System.Net.Sockets;
    using Tobii.EyeX.Client;
    using Tobii.EyeX.Framework;

    /// <summary>
    /// Provides a stream of fixation data.
    /// See <see cref="FixationEventArgs"/>.
    /// </summary>
    [SuppressMessage("Microsoft.Naming", "CA1711:IdentifiersShouldNotHaveIncorrectSuffix", Justification = "Suffix is DataStream, not just Stream. Low risk for confusion.")]
    public sealed class FixationDataStream : DataStreamBase<FixationEventArgs>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="FixationDataStream"/> class.
        /// </summary>
        /// <param name="mode">Specifies the kind of fixation detection to be used.</param>
        /// 
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
        int fpog_valid;
        public FixationDataStream(FixationDataMode mode)
        {
            Mode = mode;

            //Gazepoint Initialization Start==============================
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

        /// <summary>
        /// Gets the kind of fixation detection used.
        /// </summary>
        public FixationDataMode Mode { get; private set; }

        /// <summary>
        /// Assigns the behavior corresponding to the data stream type to an interactor.
        /// </summary>
        /// <param name="interactor">The global interactor to add the data stream behavior to.</param>
        protected override void AssignBehavior(Interactor interactor)
        {
            var parameters = new FixationDataParams { FixationDataMode = Mode };
            interactor.CreateFixationDataBehavior(ref parameters);
        }

        /// <summary>
        /// Extracts data points from an event from the EyeX Engine.
        /// </summary>
        /// <param name="behaviors">The <see cref="Behavior"/> instances containing the event data.</param>
        /// <returns>The collection of data points.</returns>
        protected override IEnumerable<FixationEventArgs> ExtractDataPoints(IEnumerable<Behavior> behaviors)
        {
            fpogx = 0;
            do
            {
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
            } while (fpogx == 0);
            double resX = fpogx * 1920;
            double resY = fpogy * 1080;
            double perX = fpogx * 100;
            double perY = fpogy * 100;

            foreach (var behavior in behaviors
                .Where(behavior => behavior.BehaviorType == BehaviorType.FixationData))
            {
                FixationDataEventParams parameters;
                if (behavior.TryGetFixationDataEventParams(out parameters) &&
                    parameters.FixationDataMode == Mode)
                {
                    yield return new FixationEventArgs(parameters.EventType, resX, resY, time_val);
                }
            }
        }
    }

    /// <summary>
    /// Provides event data for the fixation data stream.
    /// </summary>
    [SuppressMessage("StyleCop.CSharp.MaintainabilityRules", "SA1402:FileMayOnlyContainASingleClass", Justification = "Belongs here.")]
    public sealed class FixationEventArgs : EventArgs
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="FixationEventArgs"/> class.
        /// </summary>
        /// <param name="eventType">Specifies the kind of fixation event that occurred.</param>
        /// <param name="x">X coordinate of the gaze point in physical pixels.</param>
        /// <param name="y">Y coordinate of the gaze point in physical pixels.</param>
        /// <param name="timestamp">Timestamp in milliseconds.</param>
        public FixationEventArgs(FixationDataEventType eventType, double x, double y, double timestamp)
        {
            EventType = eventType;
            X = x;
            Y = y;
            Timestamp = timestamp;
        }

        /// <summary>
        /// Gets the kind of fixation event that occurred.
        /// The sequence is: Begin, [Data, ...], End.
        /// </summary>
        public FixationDataEventType EventType { get; private set; }

        /// <summary>
        /// Gets the X coordinate of the gaze point in physical pixels.
        /// </summary>
        public double X { get; private set; }

        /// <summary>
        /// Gets the Y coordinate of the gaze point in physical pixels.
        /// </summary>
        public double Y { get; private set; }

        /// <summary>
        /// Gets the timestamp in milliseconds.
        /// </summary>
        public double Timestamp { get; private set; }
    }
}
