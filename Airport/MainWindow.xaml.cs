using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Configuration;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;


namespace Airport
{
    public partial class MainWindow : Window
    {
        private Admin admin;
        private Plane plane;
        private Flight flight;
        private bool Authorization { get; set; }
        public static bool AuthorizationWnd { get; set; }
        const int validItem = 0;


        public MainWindow()
        {
            InitializeComponent();

            InitAllDatePicker();
            InitPlaneTypeComboBox();
            InitPlaneModelComboBox();
            planeSearchByType.ItemsSource = Database.GetListForComboBox("SELECT DISTINCT [Тип] FROM Plane", "[Тип]");
            planeSearchByModel.ItemsSource = Database.GetListForComboBox("SELECT DISTINCT [Модель] FROM Plane", "[Модель]");

            flightIDPlane.ItemsSource = Database.GetListForComboBox("SELECT [ID] FROM Plane", "ID");
            flightSearchByAirline.ItemsSource = Database.GetListForComboBox("SELECT DISTINCT [Авиакомпания] FROM Flight", "[Авиакомпания]");
            InitFlightAirline();

            Authorization = false;
            AuthorizationWnd = true;

            admin = new Admin();
            plane = new Plane();
            flight = new Flight();

            this.DataContext = new Valid();
        }

        private void InitPlaneTypeComboBox()
        {
            planeType.Items.Add("Военный");
            planeType.Items.Add("Гражданский");
            planeType.Items.Add("Специальный");
        }
        private void InitPlaneModelComboBox()
        {
            planeModel.Items.Add("Aerospatiale");
            planeModel.Items.Add("Airbus");
            planeModel.Items.Add("Boeing");
            planeModel.Items.Add("British Aerospace");
            planeModel.Items.Add("British Aircraft");
            planeModel.Items.Add("Heinkel");
            planeModel.Items.Add("Junkers");
            planeModel.Items.Add("McDonnell Douglas");
            planeModel.Items.Add("Messerschmitt");
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
        private void InitAllDatePicker()
        {
            planeMaintenanceDate.DisplayDateEnd = DateTime.Now;
            flightDateOfDeparture.DisplayDateEnd = DateTime.Now;
            flightDataOfArrival.DisplayDateEnd = DateTime.Now;
            flightSearchByDateOfDeparture.DisplayDateEnd = DateTime.Now;
            passengerDateIssue.DisplayDateEnd = DateTime.Now;
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
            Database.Request("SELECT * FROM Flight", flightGrid);
        }


        private void MenuItemUsers_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Пользователи");
        }
        private void MenuItemAbout_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("О программе");
        }
        private void MenuItemExit_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }


        private void Plane_Add_Click(object sender, RoutedEventArgs e)
        {
            DateTime? date = planeMaintenanceDate.SelectedDate;
            if ((planeType.Items.IndexOf(planeType.Text) >= 0 && planeModel.Items.IndexOf(planeModel.Text) >= 0 &&
                planeNumberOfSeats.Text != "" && planeCapacity.Text != "" && date != null) &&
                (Valid.IsNum(planeNumberOfSeats.Text) && Valid.IsNum(planeCapacity.Text)))
            {
                plane.Type = planeType.SelectedItem.ToString();
                plane.Model = planeModel.SelectedItem.ToString();
                plane.NumberOfSeats = int.Parse(planeNumberOfSeats.Text);
                plane.Capacity = int.Parse(planeCapacity.Text);
                plane.MaintenanceDate = planeMaintenanceDate.SelectedDate.Value.ToString("d");

                Database.AddPlane(plane.Type, plane.Model, plane.NumberOfSeats, plane.Capacity, plane.MaintenanceDate);
                Database.Request("SELECT * FROM Plane", planesGrid);
            }
            else
                MessageBox.Show("Проверьте, заполнены ли все поля, и убедитесь в их корректности", "Предупреждение", MessageBoxButton.OK, MessageBoxImage.Warning);
        }
        private void Plane_SearchByType_Click(object sender, RoutedEventArgs e)
        {
            ResultWindow resultWnd = new ResultWindow(planeSearchByType, "Plane", "Тип");
        }
        private void Plane_SearchByModel_Click(object sender, RoutedEventArgs e)
        {
            ResultWindow resultWnd = new ResultWindow(planeSearchByModel, "Plane", "Модель");
        }
        private void Plane_SearchByCapasity_Click(object sender, RoutedEventArgs e)
        {
            ResultWindow resultWnd = new ResultWindow(planeSearchByCapasity, "Plane", "Грузоподъемность");
        }
        private void Plane_DeleteByID_Click(object sender, RoutedEventArgs e)
        {
            if (planeDeleteByID.Text == "")
            {
                MessageBox.Show("Пустое поле для удаления. Повторите ввод", "Предупреждение", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
            else
            {
                if (Database.Request("SELECT * FROM Plane WHERE ID = " + planeDeleteByID.Text) != 1)
                    MessageBox.Show("Самолет с таким ID не найден", "Предупреждение", MessageBoxButton.OK, MessageBoxImage.Information);
                else
                {
                    if (Database.Request("DELETE FROM Plane WHERE ID = " + planeDeleteByID.Text) != 0)
                        MessageBox.Show("Самолет с ID " + planeDeleteByID.Text + " был успешно удален", "Предупреждение", MessageBoxButton.OK, MessageBoxImage.Information);
                }
            }
            Database.Request("SELECT * FROM Plane", planesGrid);
        }


        private void Flight_Add_Click(object sender, RoutedEventArgs e)
        {
            DateTime? dateOfDeparture = flightDateOfDeparture.SelectedDate;
            DateTime? dateOfArrival = flightDataOfArrival.SelectedDate;
            if (flightIDPlane.Items.IndexOf(flightIDPlane.Text) > 0 && flightAirline.Items.IndexOf(flightAirline.Text) > 0 &&
                flightAirportOfArrival.Text != "" && dateOfDeparture != null && dateOfArrival != null)
            {
                flight.IDPlane = int.Parse(flightIDPlane.Text);
                flight.Airline = flightAirline.Text;
                flight.AirportOfArrival = flightAirportOfArrival.Text;
                flight.DateOfDeparture = flightDateOfDeparture.SelectedDate.Value.ToString("d");
                flight.DateOfArrival = flightDataOfArrival.SelectedDate.Value.ToString("d");

                Database.AddFlight(flight.IDPlane, flight.Airline, flight.AirportOfArrival, flight.DateOfDeparture, flight.DateOfArrival);
                Database.Request("SELECT * FROM Flight", flightGrid);
            }
            else
                MessageBox.Show("Проверьте, заполнены ли все поля, и убедитесь в их корректности", "Предупреждение", MessageBoxButton.OK, MessageBoxImage.Warning);
        }
        private void Flight_SearchByAirline_Click(object sender, RoutedEventArgs e)
        {
            ResultWindow resultWnd = new ResultWindow(flightSearchByAirline, "Flight", "Авиакомпания");
        }
        private void Flight_SearchByAirportOfArrival_Click(object sender, RoutedEventArgs e)
        {
            ResultWindow resultWnd = new ResultWindow(flightSearchByAirportOfArrival, "Flight", "[Аэропорт прибытия]");
        }
        private void Flight_SearchByDateOfDeparture_Click(object sender, RoutedEventArgs e)
        {
            ResultWindow resultWnd = new ResultWindow(flightSearchByDateOfDeparture, "Flight", "[Дата отправления]");
        }
        private void Flight_DeleteByIDPlane_Click(object sender, RoutedEventArgs e)
        {
            if (flightDeleteByIDPlane.Text == "")
            {
                MessageBox.Show("Пустое поле для удаления. Повторите ввод", "Предупреждение", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
            else
            {
                if (Database.Request("SELECT * FROM Flight WHERE ID = " + flightDeleteByIDPlane.Text) != 1)
                    MessageBox.Show("Авиарейс с таким ID не найден", "Предупреждение", MessageBoxButton.OK, MessageBoxImage.Information);
                else
                {
                    if (Database.Request("DELETE FROM Flight WHERE ID = " + flightDeleteByIDPlane.Text) != 0)
                        MessageBox.Show("Авиарейс с ID " + flightDeleteByIDPlane.Text + " был успешно удален", "Предупреждение", MessageBoxButton.OK, MessageBoxImage.Information);
                }
            }
            Database.Request("SELECT * FROM Flight", flightGrid);
        }


        private void Passenger_Add_Click(object sender, RoutedEventArgs e)
        {

        }


        private void Ticket_Add_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
