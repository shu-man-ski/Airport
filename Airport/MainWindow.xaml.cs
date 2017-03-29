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
        Admin _admin;
        private bool Authorization { get; set; }
        public static bool AuthorizationWnd { get; set; }
        string connectionString;
        SqlDataAdapter adapter;
        DataTable flightTable;

        public MainWindow()
        {
            InitializeComponent();
            Authorization = false;
            AuthorizationWnd = true;
            _admin = new Admin();
            // получаем строку подключения из app.config
            connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
        }
        private void Window_Loaded(object sender, RoutedEventArgs e)
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

            string sql = "SELECT * FROM flight";
            flightTable = new DataTable();
            SqlConnection connection = null;
            try
            {
                connection = new SqlConnection(connectionString);
                SqlCommand command = new SqlCommand(sql, connection);
                adapter = new SqlDataAdapter(command);

                // установка команды на добавление для вызова хранимой процедуры
                adapter.InsertCommand = new SqlCommand("sp_InsertFlight", connection);
                adapter.InsertCommand.CommandType = CommandType.StoredProcedure;
                adapter.InsertCommand.Parameters.Add(new SqlParameter("@ID", SqlDbType.Int, 0, "ID"));
                adapter.InsertCommand.Parameters.Add(new SqlParameter("@Авиакомания", SqlDbType.NVarChar, 50, "Авиакомания"));
                adapter.InsertCommand.Parameters.Add(new SqlParameter("@Аэропорт_отправления", SqlDbType.NVarChar, 50, "Аэропорт_отправления"));
                adapter.InsertCommand.Parameters.Add(new SqlParameter("@Аэропорт_прибытия", SqlDbType.NVarChar, 50, "Аэропорт_прибытия"));
                adapter.InsertCommand.Parameters.Add(new SqlParameter("@Дата_отправления", SqlDbType.Date, 0, "Дата_отправления"));
                adapter.InsertCommand.Parameters.Add(new SqlParameter("@Дата_прибытия", SqlDbType.Date, 0, "Дата_прибытия"));
                adapter.InsertCommand.Parameters.Add(new SqlParameter("@Время_отправления", SqlDbType.Time, 7, "Время_отправления"));
                adapter.InsertCommand.Parameters.Add(new SqlParameter("@Время_прибытия", SqlDbType.Time, 7, "Время_прибытия"));
                adapter.InsertCommand.Parameters.Add(new SqlParameter("@Количество_мест", SqlDbType.Int, 0, "Количество_мест"));

                connection.Open();
                //adapter.Fill(flightTable);
                flightGrid.ItemsSource = flightTable.DefaultView;
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
        private void UpdateDB()
        {
            SqlCommandBuilder comandbuilder = new SqlCommandBuilder(adapter);
            adapter.Update(flightTable);
        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            UpdateDB();
        }
    }
}
