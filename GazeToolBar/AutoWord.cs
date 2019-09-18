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
        private int frequency;
        private int currentDistance;
        public AutoWord(string word, string freq)
        {
            this.word = word;
            frequency = 1;
            currentDistance = 0;
        }

        public int CurrentDistance { get => currentDistance; set => currentDistance = value; }
        public int Frequency { get => frequency; set => frequency = value; }
        public string Word { get => word; set => word = value; }
    }
}
