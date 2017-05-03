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
            flightIDPlane.ItemsSource = Database.InitFlightIDPlaneComboBox(flightIDPlane);
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
            planeSearchByType.Items.Add("Гражданский");
            planeSearchByType.Items.Add("Военный");
            planeSearchByType.Items.Add("Специальный");
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

            planeSearchByModel.Items.Add("Aérospatiale (Франция)");
            planeSearchByModel.Items.Add("Airbus (ЕС)");
            planeSearchByModel.Items.Add("Boeing (США)");
            planeSearchByModel.Items.Add("British Aerospace (Великобритания)");
            planeSearchByModel.Items.Add("British Aircraft (Великобритания)");
            planeSearchByModel.Items.Add("Heinkel (Германия)");
            planeSearchByModel.Items.Add("Junkers (Германия)");
            planeSearchByModel.Items.Add("McDonnell Douglas (США)");
            planeSearchByModel.Items.Add("Messerschmitt (Германия)");
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

            flightSearchByAirline.Items.Add("Lufthansa");
            flightSearchByAirline.Items.Add("S7 Airline");
            flightSearchByAirline.Items.Add("Ber Air");
            flightSearchByAirline.Items.Add("RusAir");
            flightSearchByAirline.Items.Add("Minsk Air");
            flightSearchByAirline.Items.Add("AZUR Air");
            flightSearchByAirline.Items.Add("Air Astana");
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

            Database.Request("SELECT * FROM Plane", planesGrid);
            Database.Request("SELECT * FROM Flight", flightsGrid);
        }


        private void MenuItemExit_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }


        private void Plane_Add_Click(object sender, RoutedEventArgs e)
        {
            plane.Type = planeType.SelectedItem.ToString();
            plane.Model = planeModel.SelectedItem.ToString();
            plane.NumberOfSeats = int.Parse(planeNumberOfSeats.Text);
            plane.Capacity = int.Parse(planeCapacity.Text);
            plane.MaintenanceDate = planeMaintenanceDate.SelectedDate.Value.ToString("d");

            Database.AddPlane(plane.Type, plane.Model, plane.NumberOfSeats, plane.Capacity, plane.MaintenanceDate);
            Database.Request("SELECT * FROM Plane", planesGrid);
        }
        private void Plane_SearchByType_Click(object sender, RoutedEventArgs e)
        {
            ResultWindow resultWnd = new ResultWindow(planeSearchByType, "Plane", "Тип");
            resultWnd.Show();
        }
        private void Plane_SearchByModel_Click(object sender, RoutedEventArgs e)
        {
            ResultWindow resultWnd = new ResultWindow(planeSearchByModel, "Plane", "Модель");
            resultWnd.Show();
        }
        private void Plane_SearchByCapasity_Click(object sender, RoutedEventArgs e)
        {
            ResultWindow resultWnd = new ResultWindow(planeSearchByCapasity, "Plane", "Грузоподъемность");
            resultWnd.Show();
        }
        private void Plane_DeleteByID_Click(object sender, RoutedEventArgs e)
        {
            if (planeDeleteByID.Text == "")
            {
                MessageBox.Show("Поле для удаления пустое. Повторите ввод");
            }
            else
            {
                Database.Request("DELETE FROM Plane WHERE ID = " + planeDeleteByID.Text);
            }
            Database.Request("SELECT * FROM Plane", planesGrid);
        }


        private void Flight_Add_Click(object sender, RoutedEventArgs e)
        {
            flight.IDPlane = int.Parse(flightIDPlane.Text);
            flight.Airline = flightAirline.Text;
            flight.AirportOfArrival = flightAirportOfArrival.Text;
            flight.DateOfDeparture = flightDateOfDeparture.SelectedDate.Value.ToString("d");
            flight.DateOfArrival = flightDataOfArrival.SelectedDate.Value.ToString("d");

            Database.AddFlight(flight.IDPlane, flight.Airline, flight.AirportOfArrival, flight.DateOfDeparture, flight.DateOfArrival);
            Database.Request("SELECT * FROM Flight", flightsGrid);
        }
        private void Flight_SearchByAirline_Click(object sender, RoutedEventArgs e)
        {

        }
        private void Flight_SearchByAirportOfArrival_Click(object sender, RoutedEventArgs e)
        {

        }
        private void Flight_SearchByDateOfDeparture_Click(object sender, RoutedEventArgs e)
        {

        }
        private void Flight_DeleteByIDPlane_Click(object sender, RoutedEventArgs e)
        {

        }


        private void Passenger_Add_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
