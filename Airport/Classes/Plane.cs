using System.Windows;

namespace Airport
{
    class Plane
    {
        public string ID { get; set; }
        public string Type { get; set; }
        public string Model { get; set; }
        private int numberOfSeats;
        public int NumberOfSeats
        {
            get { return numberOfSeats; }
            set
            {
                if (value > 5 && value < 750)
                    numberOfSeats = value;
                else
                {
                    MessageBox.Show("Недопустимое количество мест в самолете. Проверьте корректность ввода");
                    numberOfSeats = 0;
                }
            }
        }
        private int capacity;
        public int Capacity
        {
            get { return capacity; }
            set
            {
                if (value > 20 && value < 350)
                    capacity = value;
                else
                {
                    MessageBox.Show("Недопустимый размер грузоподъемности самолета. Проверьте корректность ввода");
                    capacity = 0;
                }
            }
        }
        public string MaintenanceDate { get; set; }
    }
}
