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
        static private DataTable dataTable;

        static Database()
        {
            connection = null;
            // Получаем строку подключения из app.config
            connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
        }


        static public List<int> InitFlightIDPlaneComboBox(ComboBox _flightIdPlane)
        {
            dataTable = new DataTable();
            List<int> list = new List<int>();

            string sql = "SELECT [ID] FROM Plane";
            try
            {
                connection = new SqlConnection(connectionString);
                SqlCommand command = new SqlCommand(sql, connection);
                adapter = new SqlDataAdapter(command);

                connection.Open();
                adapter.Fill(dataTable);

                foreach (DataRow row in dataTable.Rows)
                {
                    list.Add(int.Parse(row["ID"].ToString()));
                }
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
            return list;
        }


        static public void AddPlane(string _Type, string _Model, int _NumberOfSeats, int _Capacity, string _MaintenanceDate)
        {
            string sql = "insert into Plane([Type], [Model], [NumberOfSeats], [Capacity], [MaintenanceDate])" +
                                   "values (@Type,  @Model,  @NumberOfSeats,  @Capacity,  @MaintenanceDate)";
            try
            {
                connection = new SqlConnection(connectionString);
                SqlCommand command = new SqlCommand(sql, connection);

                command.Parameters.AddWithValue("@Type", _Type);
                command.Parameters.AddWithValue("@Model", _Model);
                command.Parameters.AddWithValue("@NumberOfSeats", _NumberOfSeats);
                command.Parameters.AddWithValue("@Capacity", _Capacity);
                command.Parameters.AddWithValue("@MaintenanceDate", _MaintenanceDate);

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
        static public void AddFlight(int _IDPlane, string _Airline, string _AirportOfArrival, string _DateOfDeparture, string _DateOfArrival)
        {
            string sql = "insert into Flight([IDPlane], [Airline], [AirportOfArrival], [DateOfDeparture], [DateOfArival])" +
                                    "values (@IDPlane,  @Airline,  @AirportOfArrival,  @DateOfDeparture,  @DateOfArival)";
            try
            {
                connection = new SqlConnection(connectionString);
                SqlCommand command = new SqlCommand(sql, connection);
                command.Parameters.AddWithValue("@IDPlane", _IDPlane);
                command.Parameters.AddWithValue("@Airline", _Airline);
                command.Parameters.AddWithValue("@AirportOfArrival", _AirportOfArrival);
                command.Parameters.AddWithValue("@DateOfDeparture", _DateOfDeparture);
                command.Parameters.AddWithValue("@DateOfArival", _DateOfArrival);

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
        static public void Update(DataGrid _dataGrid, string _table)
        {
            dataTable = new DataTable();

            string sql = "SELECT * FROM " + _table;
            try
            {
                connection = new SqlConnection(connectionString);
                SqlCommand command = new SqlCommand(sql, connection);
                adapter = new SqlDataAdapter(command);

                connection.Open();
                adapter.Fill(dataTable);
                _dataGrid.ItemsSource = dataTable.DefaultView;
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
