using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GazeToolBar
{
    public class FixationSmootherAverage : FixationSmootherBase
    {
        int bufferCurrentIndex;
        int bufferFullIndex;

        public FixationSmootherAverage(int BufferSize) : base(BufferSize)
        {
            bufferCurrentIndex = 0;
            bufferFullIndex = 0;
        }

        public override void AddCoordinateToBuffer(double x, double y)
        {
            //double smoothingFactor = 0.6;
            //if (bufferCurrentIndex == bufferSize)
            //{
            //    bufferCurrentIndex = 0;
            //}

            //if (bufferFullIndex != bufferSize)
            //{
            //    bufferFullIndex++;
            //}

            //xBuffer[bufferCurrentIndex] = smoothingFactor * x + (1 - smoothingFactor) * xBuffer[bufferCurrentIndex - 1];
            //yBuffer[bufferCurrentIndex] = smoothingFactor * y + (1 - smoothingFactor) * yBuffer[bufferCurrentIndex - 1];


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

        public override GazePoint SmoothPointsFromBuffer()
        {
            GazePoint returnSmoothPoint = new GazePoint(0, 0);
            double xTotal = 0;
            double yTotal = 0;

            for (int arrayIndex = 0; arrayIndex < bufferFullIndex; arrayIndex++)
            {
                xTotal += xBuffer[arrayIndex];
                yTotal += yBuffer[arrayIndex];
            }

            returnSmoothPoint.X = xTotal / bufferFullIndex;
            returnSmoothPoint.Y = yTotal / bufferFullIndex;

            return returnSmoothPoint;

        }
    }
}
