using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Airport
{
    class Flight
    {
        private int id;
        public int ID
        {
            get { return id; }
            set
            {
                if (value >= 0)
                    id = value;
                else
                    MessageBox.Show("Номер рейса не может быть отрицательным. Повторите ввод");
            }
        }
        public string Airline { get; set; }
        private int numseats;
        public int NumSeats
        {
            get { return numseats; }
            set
            {
                if (value >= 0)
                    numseats = value;
                else
                    MessageBox.Show("Количество мест не может быть отрицательным. Повторите ввод");
            }
        }
        public string AirportOfDeparture { get; set; }
        public string AirportOfArrival { get; set; }
        public DateTime DateOfDeparture { get; set; }
        public DateTime DateOfArrival { get; set; }
    }
}
