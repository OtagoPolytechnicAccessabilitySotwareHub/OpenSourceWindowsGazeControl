using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GazeToolBar
{
    public class AutoWord
    {
        private string word;
        private Double frequency;
        private Double currentDistance;
        public AutoWord(string word, string freq)
        {
            this.word = word;
            frequency = Convert.ToDouble(freq);
            currentDistance = 0;
        }

        public Double CurrentDistance { get => currentDistance; set => currentDistance = value; }
        public Double Frequency { get => frequency; set => frequency = value; }
        public string Word { get => word; set => word = value; }
    }
}
