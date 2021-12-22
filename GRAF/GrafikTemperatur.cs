using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GRAF
{
    public class GrafikTemperatur
    {
        public string Month { get; set; }
        public int Temperature { get; set; }

        public GrafikTemperatur()
        {

        }
        public GrafikTemperatur(string Month, int Temperature)
        {
            this.Month = Month;
            this.Temperature = Temperature;
        }
    }    
}
