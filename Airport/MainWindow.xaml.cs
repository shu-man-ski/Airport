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
        private Flight flight;
        private bool Authorization { get; set; }
        public static bool AuthorizationWnd { get; set; }
        


        public MainWindow()
        {
            InitializeComponent();

            Authorization = false;
            AuthorizationWnd = true;

            admin = new Admin();
            flight = new Flight();
            this.DataContext = flight;
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

            Database.Update(flightsGrid, "Flight");
        }
        

        private void Add_Click(object sender, RoutedEventArgs e)
        {
            flight.ID = int.Parse(ID_авиарейса.Text);
            flight.Airline = Авиакомпания.Text.ToString();
            flight.NumSeats = int.Parse(Количество_мест.Text);
            flight.AirportOfDeparture = Аэропорт_отправления.Text.ToString();
            flight.AirportOfArrival = Аэропорт_прибытия.Text.ToString();
            flight.DateOfDeparture = Дата_отправления.SelectedDate.Value;
            flight.DateOfArrival = Дата_прибытия.SelectedDate.Value;

            Database.AddFlight(flight.ID, flight.Airline, flight.NumSeats,
                flight.AirportOfDeparture, flight.AirportOfArrival, flight.DateOfDeparture, flight.DateOfArrival);

            Database.Update(flightsGrid, "Flight");
        }
        private void UpdateDB_Click(object sender, RoutedEventArgs e)
        {
            Database.Update(flightsGrid, "Flight");
        }
    }
}
