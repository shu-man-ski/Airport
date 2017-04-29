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
        private Admin _admin;
        private Flight _flight;
        private bool Authorization { get; set; }
        public static bool AuthorizationWnd { get; set; }
        private string connectionString;
        private SqlDataAdapter adapter;
        private SqlConnection connection;
        private DataTable flightTable;


        public MainWindow()
        {
            InitializeComponent();

            Authorization = false;
            AuthorizationWnd = true;

            _admin = new Admin();
            _flight = new Flight();

            flightTable = new DataTable();
            connection = null;
            // получаем строку подключения из app.config
            connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
        }
        private void CheckAuthoriztion()
        {
            while (!Authorization && AuthorizationWnd)
            {
                AuthorizationWindow AWnd = new AuthorizationWindow();
                if (AWnd.ShowDialog() == true)
                {
                    if (AWnd.Password == _admin.Password && AWnd.Login == _admin.Login)
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

            UpdateDB();
        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            UpdateDB();
        }
        private void UpdateDB()
        {
            flightTable.Clear();

            string sql = "SELECT * FROM flight";
            try
            {
                connection = new SqlConnection(connectionString);
                SqlCommand command = new SqlCommand(sql, connection);
                adapter = new SqlDataAdapter(command);

                connection.Open();
                adapter.Fill(flightTable);
                flightGrid.ItemsSource = flightTable.DefaultView;
            }
            catch (SqlException ex)
            {
                MessageBox.Show(ex.Message);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                if (connection != null)
                    connection.Close();
            }
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            _flight.ID = int.Parse(ID_авиарейса.Text);
            _flight.Airline = Авиакомпания.Text.ToString();
            _flight.NumSeats = int.Parse(Количество_мест.Text);
            _flight.AirportFrom = Аэропорт_отправления.Text.ToString();
            _flight.AirportIn = Аэропорт_прибытия.Text.ToString();

            string sql = "insert into flight([ID Рейса], [Авиакомания], [Количество мест], [Аэропорт отправления], [Аэропорт прибытия]) VALUES (@ID_рейса, @Авиакомания, @Количество_мест, @Аэропорт_отправления, @Аэропорт_прибытия)";
            try
            {
                connection = new SqlConnection(connectionString);
                SqlCommand command = new SqlCommand(sql, connection);
                command.Parameters.AddWithValue("@ID_рейса", _flight.ID);
                command.Parameters.AddWithValue("@Авиакомания", _flight.Airline);
                command.Parameters.AddWithValue("@Количество_мест", _flight.NumSeats);
                command.Parameters.AddWithValue("@Аэропорт_отправления", _flight.AirportFrom);
                command.Parameters.AddWithValue("@Аэропорт_прибытия", _flight.AirportIn);

                connection.Open();
                command.ExecuteNonQuery();
            }
            catch (SqlException ex)
            {
                MessageBox.Show(ex.Message);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                if (connection != null)
                    connection.Close();
            }

            UpdateDB();
        }
    }
}
