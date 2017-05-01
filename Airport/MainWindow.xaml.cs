using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Airport
{
    public partial class MainWindow : Window
    {
        private Admin admin;
        private Plane plane;
        private Flight flight;
        private bool Authorization { get; set; }
        public static bool AuthorizationWnd { get; set; }
        

        public MainWindow()
        {
            InitializeComponent();

            InitPlaneTypeComboBox();
            InitPlaneModelComboBox();
            flightIdPlane.ItemsSource = Database.InitFlightIDPlaneComboBox(flightIdPlane);
            InitFlightAirline();

            Authorization = false;
            AuthorizationWnd = true;

            admin = new Admin();
            plane = new Plane();
            flight = new Flight();
        }

        private void InitPlaneTypeComboBox()
        {
            planeType.Items.Add("Гражданский");
            planeType.Items.Add("Военный");
            planeType.Items.Add("Специальный");
        }
        private void InitPlaneModelComboBox()
        {
            planeModel.Items.Add("Aérospatiale (Франция)");
            planeModel.Items.Add("Airbus (ЕС)");
            planeModel.Items.Add("Boeing (США)");
            planeModel.Items.Add("British Aerospace (Великобритания)");
            planeModel.Items.Add("British Aircraft (Великобритания)");
            planeModel.Items.Add("Heinkel (Германия)");
            planeModel.Items.Add("Junkers (Германия)");
            planeModel.Items.Add("McDonnell Douglas (США)");
            planeModel.Items.Add("Messerschmitt (Германия)");
        }
        private void InitFlightAirline()
        {
            flightAirline.Items.Add("Lufthansa");
            flightAirline.Items.Add("S7 Airline");
            flightAirline.Items.Add("Ber Air");
            flightAirline.Items.Add("RusAir");
            flightAirline.Items.Add("Minsk Air");
            flightAirline.Items.Add("AZUR Air");
            flightAirline.Items.Add("Air Astana");
        }


        private void CheckAuthoriztion()
        {
            while (!Authorization && AuthorizationWnd)
            {
                AuthorizationWindow AWnd = new AuthorizationWindow();
                if (AWnd.ShowDialog() == true)
                {
                    if (AWnd.Password == admin.Password && AWnd.Login == admin.Login)
                        Authorization = true;
                    else
                        MessageBox.Show(this, "Проверьте правильность ввода логина и пароля",
                                              "Ошибка авторизации", MessageBoxButton.OK, MessageBoxImage.Warning);
                }
            }
            if (!AuthorizationWnd)
                this.Close();
        }
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            CheckAuthoriztion();

            Database.Update(planesGrid, "Plane");
            Database.Update(flightsGrid, "Flight");
        }


        private void Plane_Add_Click(object sender, RoutedEventArgs e)
        {
            plane.Type = planeType.SelectedItem.ToString();
            plane.Model = planeModel.SelectedItem.ToString();
            plane.NumberOfSeats = int.Parse(planeNumberOfSeats.Text);
            plane.Capacity = int.Parse(planeCapacity.Text);
            plane.MaintenanceDate = planeMaintenanceDate.SelectedDate.Value.ToString("d");

            Database.AddPlane(plane.Type, plane.Model, plane.NumberOfSeats, plane.Capacity, plane.MaintenanceDate);
            Database.Update(planesGrid, "Plane");
        }
        private void Flight_Add_Click(object sender, RoutedEventArgs e)
        {
            flight.IDPlane = int.Parse(flightIdPlane.Text);
            flight.Airline = flightAirline.Text;
            flight.AirportOfArrival = flightAirportOfArrival.Text;
            flight.DateOfDeparture = flightDateOfDeparture.SelectedDate.Value.ToString("d");
            flight.DateOfArrival = flightDataOfArrival.SelectedDate.Value.ToString("d");

            Database.AddFlight(flight.IDPlane, flight.Airline, flight.AirportOfArrival, flight.DateOfDeparture, flight.DateOfArrival);
            Database.Update(flightsGrid, "Flight");
        }
        private void UpdateDB_Click(object sender, RoutedEventArgs e)
        {
            Database.Update(flightsGrid, "Flight");
        }
    }
}
