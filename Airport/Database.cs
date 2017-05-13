using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;

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
            string sql = "INSERT INTO Plane([Тип], [Модель], [Количество мест], [Грузоподъемность], [Дата последнего ТО])" +
                                   "VALUES (@Type,  @Model,  @NumberOfSeats,    @Capacity,          @MaintenanceDate)";
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
            string sql = "INSERT INTO Flight([ID самолета], [Авиакомпания], [Аэропорт прибытия], [Дата отправления], [Дата прибытия])" +
                                    "VALUES (@IDPlane,      @Airline,       @AirportOfArrival,   @DateOfDeparture,   @DateOfArival)";
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
        static public void AddPassenger(string _NumberPassport, string _IdentificationNumberPassport, string _AuthorityThatIssuedPassport, string _DateIssue, string _FullName)
        {
            string sql = "INSERT INTO Passenger([Номер паспорта], [Идентификационный номер],     [Орган, выдавший паспорт],    [Дата выдачи], [ФИО])" +
                                       "VALUES (@NumberPassport,  @IdentificationNumberPassport, @AuthorityThatIssuedPassport, @DateIssue,    @FullName)";
            try
            {
                connection = new SqlConnection(connectionString);
                SqlCommand command = new SqlCommand(sql, connection);
                command.Parameters.AddWithValue("@NumberPassport", _NumberPassport);
                command.Parameters.AddWithValue("@IdentificationNumberPassport", _IdentificationNumberPassport);
                command.Parameters.AddWithValue("@AuthorityThatIssuedPassport", _AuthorityThatIssuedPassport);
                command.Parameters.AddWithValue("@DateIssue", _DateIssue);
                command.Parameters.AddWithValue("@FullName", _FullName);

                connection.Open();
                command.ExecuteNonQuery();
            }
            catch (SqlException ex)
            {
                if (ex.Number == 2627)
                    MessageBox.Show("Невозможно добавить новый объект, так как уже имеется подобный с таким же номером паспорта");
                else
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
        static public void AddTicket(int _IDFlight, string _NumberPassport)
        {
            string sql = "INSERT INTO Ticket([Авиарейс], [Пассажир])" +
                                    "VALUES (@Flight,    @NumberPassport)";
            try
            {
                connection = new SqlConnection(connectionString);
                SqlCommand command = new SqlCommand(sql, connection);
                command.Parameters.AddWithValue("@Flight", _IDFlight);
                command.Parameters.AddWithValue("@NumberPassport", _NumberPassport);

                connection.Open();
                command.ExecuteNonQuery();
            }
            catch (SqlException ex)
            {
                if (ex.Number == 2627)
                    MessageBox.Show("Невозможно добавить новый объект, так как уже имеется на данном авиарейсе такой пассажир");
                else
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
        static public void AddUser(string _Login, string _Password, string _FullName)
        {
            string sql = "INSERT INTO [User]([Логин], [Пароль], [ФИО])" +
                                  "VALUES (@Login, @Password, @FullName)";
            try
            {
                connection = new SqlConnection(connectionString);
                SqlCommand command = new SqlCommand(sql, connection);
                command.Parameters.AddWithValue("@Login", _Login);
                command.Parameters.AddWithValue("@Password", _Password);
                command.Parameters.AddWithValue("@FullName", _FullName);

                connection.Open();
                command.ExecuteNonQuery();
            }
            catch (SqlException ex)
            {
                if (ex.Number == 2627)
                    MessageBox.Show("Невозможно добавить новый объект, так как уже имеется пользователь с таким логином");
                else
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
        static public void UpdateUser(string _Login, string _Password, string _FullName)
        {
            string sql = "UPDATE [User] SET [Логин] = @Login, [Пароль] = @Password, [ФИО] = @FullName";
            try
            {
                connection = new SqlConnection(connectionString);
                SqlCommand command = new SqlCommand(sql, connection);
                command.Parameters.AddWithValue("@Login", _Login);
                command.Parameters.AddWithValue("@Password", _Password);
                command.Parameters.AddWithValue("@FullName", _FullName);

                connection.Open();
                command.ExecuteNonQuery();
            }
            catch (SqlException ex)
            {
                if (ex.Number == 2627)
                    MessageBox.Show("Невозможно добавить новый объект, так как уже имеется пользователь с таким логином");
                else
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
                if (_dataGrid != null)
                    _dataGrid.ItemsSource = dataTable.DefaultView;
            }
            catch (SqlException ex)
            {
                if (ex.Number == 547)
                    MessageBox.Show("Невозможно выполнить дествие, так как текущий объект имеет связь с данными из другой таблицы");
                return 0;
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
        static public void RequestAutoriztion(ref string _login, ref string _password)
        {
            dataTable = new DataTable();
            string select = "SELECT [Логин],[Пароль] FROM [User] WHERE [Логин] = " + _login + " AND [Пароль] = " + _password;
            try
            {
                connection = new SqlConnection(connectionString);
                command = new SqlCommand(select, connection);
                adapter = new SqlDataAdapter(command);

                connection.Open();
                adapter.Fill(dataTable);
                _login = dataTable.Columns[0].ToString();
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
