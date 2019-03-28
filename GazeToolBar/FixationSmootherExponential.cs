using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GazeToolBar
{
    public class FixationSmootherExponential : FixationSmootherBase
    {
        private int lowest;

        public FixationSmootherExponential(int BufferSize) : base(BufferSize)
        {
            lowest = BufferSize;
        }

        public override void AddCoordinateToBuffer(double x, double y)
        {
            double smoothingFactor = 0.9;

            if (xBuffer[bufferSize - 1] == -1)
            {
                Utils.ArrayFill(xBuffer, x);
                Utils.ArrayFill(yBuffer, y);
            }

            lowest = Math.Max(0, lowest - 1);

            double[] newXBuffer = new double[bufferSize];
            double[] newYBuffer = new double[bufferSize];

            Array.Copy(xBuffer, 1, newXBuffer, 0, xBuffer.Length - 1);
            Array.Copy(yBuffer, 1, newYBuffer, 0, yBuffer.Length - 1);

            newXBuffer[bufferSize - 1] = smoothingFactor * x + (1 - smoothingFactor) * xBuffer[bufferSize - 1];
            newYBuffer[bufferSize - 1] = smoothingFactor * y + (1 - smoothingFactor) * yBuffer[bufferSize - 1];


            xBuffer = newXBuffer;
            yBuffer = newYBuffer;
        }

        public override GazePoint SmoothPointsFromBuffer()
        {
            GazePoint returnSmoothPoint = new GazePoint(0, 0);

            double xE = 0;
            double yE = 0;
            int t = 0;

            for (int i = lowest; i < bufferSize; i++)
            {
                xE += (i + 1) * xBuffer[i];
                yE += (i + 1) * yBuffer[i];

                t += (i + 1);
            }

            double xF = xE / t;
            double yF = yE / t;


            returnSmoothPoint.X = xF;
            returnSmoothPoint.Y = yF;

            return returnSmoothPoint;
        }
    }
}
