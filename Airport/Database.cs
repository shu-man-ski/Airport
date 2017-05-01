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
    class Database : MainWindow
    {
        static private string connectionString;
        static private SqlDataAdapter adapter;
        static private SqlConnection connection;
        static private DataTable flightTable;

        static Database()
        {
            flightTable = new DataTable();
            connection = null;
            // Получаем строку подключения из app.config
            connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
        }

        static public void Update(DataGrid _dataGrid, string _table)
        {
            flightTable.Clear();

            string sql = "SELECT * FROM " + _table;
            try
            {
                connection = new SqlConnection(connectionString);
                SqlCommand command = new SqlCommand(sql, connection);
                adapter = new SqlDataAdapter(command);

                connection.Open();
                adapter.Fill(flightTable);
                _dataGrid.ItemsSource = flightTable.DefaultView;
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
        static public void AddFlight(int _ID, string _Airline, int _NumSeats,
            string AirportOfDeparture, string AirportOfArrival, DateTime _DateOfDeparture, DateTime _DateOfArrival)
        {
            string sql = "INSERT INTO Flight([ID Рейса], [Авиакомания], [Количество мест], [Аэропорт отправления], [Аэропорт прибытия], [Дата отправления], [Дата прибытия])" +
                                    "VALUES (@ID_рейса,  @Авиакомания,  @Количество_мест,  @Аэропорт_отправления,  @Аэропорт_прибытия,  @Дата_отправления,  @Дата_прибытия)";
            try
            {
                connection = new SqlConnection(connectionString);
                SqlCommand command = new SqlCommand(sql, connection);
                command.Parameters.AddWithValue("@ID_рейса", _ID);
                command.Parameters.AddWithValue("@Авиакомания", _Airline);
                command.Parameters.AddWithValue("@Количество_мест", _NumSeats);
                command.Parameters.AddWithValue("@Аэропорт_отправления", AirportOfDeparture);
                command.Parameters.AddWithValue("@Аэропорт_прибытия", AirportOfArrival);

                command.Parameters.AddWithValue("@Дата_отправления", _DateOfDeparture.ToString("d"));
                command.Parameters.AddWithValue("@Дата_прибытия", _DateOfArrival.ToString("d"));

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
        }
    }
}
