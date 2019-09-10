using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GazeToolBar
{
    //Shift Options Name
    //26 Keys - options
    //FileName
    class keyboardKeys
    {
        private List<string> keyList;
        public keyboardKeys()
        {
            keyList = new List<string>();
        }


        public void addKey(string newKey)
        {
            keyList.Add(newKey);
        }

        public string getKey(int slot)
        {
            return keyList[slot];
        }


    }
}
