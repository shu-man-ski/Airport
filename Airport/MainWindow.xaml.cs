using System;
using System.Collections.Generic;
using System.Windows;

namespace Airport
{
    public partial class MainWindow : Window
    {
        private User user;
        private Plane plane;
        private Flight flight;
        private Passenger passenger;
        private Ticket ticket;
        private bool Authorization { get; set; }
        public static bool AuthorizationWnd { get; set; }
        const int validItem = 0;


        public MainWindow()
        {
            InitializeComponent();

            InitAllDatePicker();
            InitPlaneTypeComboBox();
            InitPlaneModelComboBox();
            UpdateAllCombobox();
            InitFlightAirline();

            Authorization = false;
            AuthorizationWnd = true;

            user = new User();
            plane = new Plane();
            flight = new Flight();
            passenger = new Passenger();
            ticket = new Ticket();

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
        private void UpdateAllCombobox()
        {
            planeSearchByType.ItemsSource = Database.GetListForComboBox("SELECT DISTINCT [Тип] FROM Plane", "[Тип]");
            planeSearchByModel.ItemsSource = Database.GetListForComboBox("SELECT DISTINCT [Модель] FROM Plane", "[Модель]");

            flightIDPlane.ItemsSource = Database.GetListForComboBox("SELECT [ID самолета] FROM Plane", "[ID самолета]");
            flightSearchByAirline.ItemsSource = Database.GetListForComboBox("SELECT DISTINCT [Авиакомпания] FROM Flight", "[Авиакомпания]");

            ticketFlight.ItemsSource = Database.GetListForComboBox("SELECT [ID авиарейса] FROM Flight", "[ID авиарейса]");
            ticketPassenger.ItemsSource = Database.GetListForComboBox("SELECT [Номер паспорта] FROM Passenger", "[Номер паспорта]");
            ticketSearchByFlight.ItemsSource = Database.GetListForComboBox("SELECT DISTINCT [Авиарейс] FROM Ticket", "[Авиарейс]");
        }


        private void CheckAuthoriztion()
        {
            while (!Authorization && AuthorizationWnd)
            {
                try
                {
                    AuthorizationWindow AWnd = new AuthorizationWindow();
                    if (AWnd.ShowDialog() == true)
                    {
                        List<string> login = Database.GetListForComboBox("SELECT [Логин] FROM [User] WHERE [Логин] = '" + AWnd.Password + "'", "[Логин]");
                        List<string> password = Database.GetListForComboBox("SELECT [Пароль] FROM [User] WHERE [Пароль] = '" + AWnd.Login + "'", "[Пароль]");

                        if (AWnd.Password == login[0] && AWnd.Login == password[0])
                            Authorization = true;
                    }
                }
                catch (Exception ex)
                {
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

            Database.Request("SELECT * FROM Plane", planeGrid);
            Database.Request("SELECT * FROM Flight", flightGrid);
            Database.Request("SELECT * FROM Passenger", passengerGrid);
            Database.Request("SELECT * FROM Ticket", ticketGrid);
        }


        private void MenuItemDB_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("База данных");
        }
        private void MenuItemUsers_Click(object sender, RoutedEventArgs e)
        {
            UsersWindow userWnd = new UsersWindow();
            userWnd.Show();
        }
        private void MenuItemAbout_Click(object sender, RoutedEventArgs e)
        {
            AboutProgramWindow aboutProgWnd = new AboutProgramWindow();
            aboutProgWnd.Show();
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

                if (plane.NumberOfSeats != 0 && plane.Capacity != 0)
                    Database.AddPlane(plane.Type, plane.Model, plane.NumberOfSeats, plane.Capacity, plane.MaintenanceDate);
                Database.Request("SELECT * FROM Plane", planeGrid);
            }
            else
                MessageBox.Show("Проверьте, заполнены ли все поля, и убедитесь в их корректности", "Предупреждение", MessageBoxButton.OK, MessageBoxImage.Warning);
            UpdateAllCombobox();
        }
        private void Plane_SearchByType_Click(object sender, RoutedEventArgs e)
        {
            ResultWindow resultWnd = new ResultWindow("Plane", "Тип", _searchCB: planeSearchByType, _dataGrid: planeGrid);
            UpdateAllCombobox();
        }
        private void Plane_SearchByModel_Click(object sender, RoutedEventArgs e)
        {
            ResultWindow resultWnd = new ResultWindow("Plane", "Модель", _searchCB: planeSearchByModel, _dataGrid: planeGrid);
            UpdateAllCombobox();
        }
        private void Plane_SearchByCapasity_Click(object sender, RoutedEventArgs e)
        {
            ResultWindow resultWnd = new ResultWindow("Plane", "Грузоподъемность", _searchTB: planeSearchByCapasity, _dataGrid: planeGrid);
            UpdateAllCombobox();
        }
        private void Plane_DeleteByID_Click(object sender, RoutedEventArgs e)
        {
            if (planeDeleteByID.Text == "")
            {
                MessageBox.Show("Пустое поле для удаления. Повторите ввод", "Предупреждение", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
            else
            {
                if (Database.Request("SELECT * FROM Plane WHERE [ID самолета] = " + planeDeleteByID.Text) != 1)
                    MessageBox.Show("Самолет с таким ID не найден", "Предупреждение", MessageBoxButton.OK, MessageBoxImage.Information);
                else
                {
                    if (Database.Request("DELETE FROM Plane WHERE [ID самолета] = " + planeDeleteByID.Text) != 0)
                        MessageBox.Show("Самолет с ID " + planeDeleteByID.Text + " был успешно удален", "Предупреждение", MessageBoxButton.OK, MessageBoxImage.Information);
                }
            }
            Database.Request("SELECT * FROM Plane", planeGrid);
            UpdateAllCombobox();
        }


        private void Flight_Add_Click(object sender, RoutedEventArgs e)
        {
            DateTime? dateOfDeparture = flightDateOfDeparture.SelectedDate;
            DateTime? dateOfArrival = flightDataOfArrival.SelectedDate;
            if (flightIDPlane.Items.IndexOf(flightIDPlane.Text) >= 0 && flightAirline.Items.IndexOf(flightAirline.Text) >= 0 &&
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
            UpdateAllCombobox();
        }
        private void Flight_SearchByAirline_Click(object sender, RoutedEventArgs e)
        {
            ResultWindow resultWnd = new ResultWindow("Flight", "Авиакомпания", _searchCB: flightSearchByAirline, _dataGrid: flightGrid);
            UpdateAllCombobox();
        }
        private void Flight_SearchByAirportOfArrival_Click(object sender, RoutedEventArgs e)
        {
            ResultWindow resultWnd = new ResultWindow("Flight", "[Аэропорт прибытия]", _searchTB: flightSearchByAirportOfArrival, _dataGrid: flightGrid);
            UpdateAllCombobox();
        }
        private void Flight_SearchByDateOfDeparture_Click(object sender, RoutedEventArgs e)
        {
            ResultWindow resultWnd = new ResultWindow("Flight", "[Дата отправления]", _searchDP: flightSearchByDateOfDeparture, _dataGrid: flightGrid);
            UpdateAllCombobox();
        }
        private void Flight_DeleteByIDPlane_Click(object sender, RoutedEventArgs e)
        {
            if (flightDeleteByIDPlane.Text == "")
            {
                MessageBox.Show("Пустое поле для удаления. Повторите ввод", "Предупреждение", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
            else
            {
                if (Database.Request("SELECT * FROM Flight WHERE [ID авиарейса] = " + flightDeleteByIDPlane.Text) != 1)
                    MessageBox.Show("Авиарейс с таким ID не найден", "Предупреждение", MessageBoxButton.OK, MessageBoxImage.Information);
                else
                {
                    if (Database.Request("DELETE FROM Flight WHERE [ID авиарейса] = " + flightDeleteByIDPlane.Text) != 0)
                        MessageBox.Show("Авиарейс с ID " + flightDeleteByIDPlane.Text + " был успешно удален", "Предупреждение", MessageBoxButton.OK, MessageBoxImage.Information);
                }
            }
            Database.Request("SELECT * FROM Flight", flightGrid);
            UpdateAllCombobox();
        }


        private void Passenger_Add_Click(object sender, RoutedEventArgs e)
        {
            DateTime? dateIssue = passengerDateIssue.SelectedDate;
            if (passengerNumberPassport.Text != "" && passengerIdentificationNumberPassport.Text != "" &&
                passengerAuthorityThatIssuedPassport.Text != "" && dateIssue != null && passengerFullName.Text != "")
            {
                passenger.NumberPassport = passengerNumberPassport.Text;
                passenger.IdentificationNumberPassport = passengerIdentificationNumberPassport.Text;
                passenger.AuthorityThatIssuedPassport = passengerAuthorityThatIssuedPassport.Text;
                passenger.DateIssue = passengerDateIssue.SelectedDate.Value.ToString("d");
                passenger.FullName = passengerFullName.Text;

                Database.AddPassenger(passenger.NumberPassport, passenger.IdentificationNumberPassport, passenger.AuthorityThatIssuedPassport, passenger.DateIssue, passenger.FullName);
                Database.Request("SELECT * FROM Passenger", passengerGrid);
            }
            else
                MessageBox.Show("Проверьте, заполнены ли все поля, и убедитесь в их корректности", "Предупреждение", MessageBoxButton.OK, MessageBoxImage.Warning);
            UpdateAllCombobox();
        }
        private void Passenger_SearchByNumberPassport_Click(object sender, RoutedEventArgs e)
        {
            ResultWindow resultWnd = new ResultWindow("Passenger", "[Номер паспорта]", _searchTB: passengerSearchByNumberPassport, _dataGrid: passengerGrid);
            UpdateAllCombobox();
        }
        private void Passenger_SearchByFullName_Click(object sender, RoutedEventArgs e)
        {
            ResultWindow resultWnd = new ResultWindow("Passenger", "[ФИО]", _searchTB: passengerSearchByFullName, _dataGrid: passengerGrid);
            UpdateAllCombobox();
        }
        private void Passenger_DeleteByNumberPassport_Click(object sender, RoutedEventArgs e)
        {
            if (passengerDeleteByNumberPassport.Text == "")
            {
                MessageBox.Show("Пустое поле для удаления. Повторите ввод", "Предупреждение", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
            else
            {
                if (Database.Request("SELECT * FROM Passenger WHERE [Номер паспорта] = " + "'" + passengerDeleteByNumberPassport.Text + "'") != 1)
                    MessageBox.Show("Пассажир с таким номером паспорта не найден", "Предупреждение", MessageBoxButton.OK, MessageBoxImage.Information);
                else
                {
                    if (Database.Request("DELETE FROM Passenger WHERE [Номер паспорта] = " + "'" + passengerDeleteByNumberPassport.Text + "'") != 0)
                        MessageBox.Show("Пассажир с номером паспорта " + passengerDeleteByNumberPassport.Text + " был успешно удален", "Предупреждение", MessageBoxButton.OK, MessageBoxImage.Information);
                }
            }
            Database.Request("SELECT * FROM Passenger", passengerGrid);
            UpdateAllCombobox();
        }


        private void Ticket_Add_Click(object sender, RoutedEventArgs e)
        {
            if (ticketFlight.Items.IndexOf(ticketFlight.Text) >= 0 && ticketPassenger.Items.IndexOf(ticketPassenger.Text) >= 0)
            {
                ticket.IDFlight = int.Parse(ticketFlight.Text);
                ticket.NumberPassport = ticketPassenger.Text;

                Database.AddTicket(ticket.IDFlight, ticket.NumberPassport);
                Database.Request("SELECT * FROM Ticket", ticketGrid);
            }
            else
                MessageBox.Show("Проверьте, заполнены ли все поля", "Предупреждение", MessageBoxButton.OK, MessageBoxImage.Warning);
            UpdateAllCombobox();
        }
        private void Ticket_SearchByFlighte_Click(object sender, RoutedEventArgs e)
        {
            ResultWindow resultWnd = new ResultWindow("Ticket", "[Авиарейс]", _searchCB: ticketSearchByFlight, _dataGrid: ticketGrid);
            UpdateAllCombobox();
        }
    }
}
