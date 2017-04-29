using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Airport
{
    class Flight
    {
        public int ID { get; set; }
        public string Airline { get; set; }
        public int NumSeats { get; set; }
        public string AirportFrom { get; set; }
        public string AirportIn { get; set; }
    }
}
