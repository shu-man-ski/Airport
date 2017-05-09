using System;
using System.Collections.Generic;
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

namespace Airport
{
    class Database : MainWindow
    {
        static private string connectionString;
        static private SqlDataAdapter adapter;
        static private SqlCommand command;
        static private SqlConnection connection;
        static private DataTable dataTable;

        static Database()
        {
            connection = null;
            // Получаем строку подключения из app.config
            connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
        }


        static public List<string> GetListForComboBox(string _select, string _row)
        {
            dataTable = new DataTable();
            List<string> list = new List<string>();

            try
            {
                connection = new SqlConnection(connectionString);
                SqlCommand command = new SqlCommand(_select, connection);
                adapter = new SqlDataAdapter(command);

                connection.Open();
                adapter.Fill(dataTable);

                int i = 0;
                foreach (DataRow row in dataTable.Rows)
                {
                    if (dataTable.Rows.ToString() == _row)
                        i++;
                    list.Add(row[i].ToString());
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
            string sql = "insert into Plane([Тип], [Модель], [Количество мест], [Грузоподъемность], [Дата последнего ТО])" +
                                   "values (@Type,  @Model,  @NumberOfSeats,    @Capacity,          @MaintenanceDate)";
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
            string sql = "insert into Flight([ID самолета], [Авиакомпания], [Аэропорт прибытия], [Дата отправления], [Дата прибытия])" +
                                    "values (@IDPlane,      @Airline,       @AirportOfArrival,   @DateOfDeparture,   @DateOfArival)";
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
        
        static public int Request(string _select, DataGrid _dataGrid = null)
        {
            dataTable = new DataTable();
            try
            {
                connection = new SqlConnection(connectionString);
                command = new SqlCommand(_select, connection);
                adapter = new SqlDataAdapter(command);

                connection.Open();
                adapter.Fill(dataTable);
                if(_dataGrid != null)
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
            return dataTable.Rows.Count;
        }
    }
}
