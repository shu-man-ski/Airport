using System;

namespace Airport
{
    class Valid
    {
        public int PlaneNumberOfSeats { get; set; }
        public int PlaneCapacity { get; set; }
        public int PlaneSearchCapacity { get; set; }
        public int PlaneDeleteByID { get; set; }
        public int FlightDeleteByIDPlane { get; set; }
        public int PassengerNumberPassport { get; set; }
        public int PassengerSearchNumberPassport { get; set; }
        public int PassengerDeleteNumberPassport { get; set; }

        static public bool IsNum(string s)
        {
            foreach (char c in s)
            {
                if (!Char.IsDigit(c))
                    return false;
            }
            return true;
        }
    }
}
