using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Data;
using System.Data.SqlClient;
using System.Security.Cryptography;
using System.Text;
using System.IO;

namespace Airport
{
    class Database : MainWindow
    {
        static private SqlConnection connection;
        static private SqlDataAdapter adapter;
        static private SqlCommand command;
        static private DataTable dataTable;


        static Database()
        {
            connection = new SqlConnection(@"Server=.\SQLEXPRESS;Integrated security=SSPI;database=master");
        }


        static public void CreateDatabase(string fileName)
        {
            string str = "CREATE DATABASE [Airport]" + 
                          " ON PRIMARY" + 
                            "(" +
                              " NAME = N'airportdb'," +
                              " FILENAME = N'" + fileName + @"\airportdb.mdf'," +
                              " SIZE = 15MB," +
                              " MAXSIZE = UNLIMITED," +
                              " FILEGROWTH = 1024KB" +
                            ")" +
                          " LOG ON " +
                            "(" +
                          " NAME = N'airportdb_log'," +
                          " FILENAME = N'" + fileName + @"\airportdb_log.ldf'," +
                          " SIZE = 1024KB," + 
                          " MAXSIZE = 2048GB," +
                          " FILEGROWTH = 10 %" +
                            ")" +
                         @" ALTER AUTHORIZATION ON DATABASE::[Airport] TO[NT AUTHORITY\СИСТЕМА]; ";

            SqlCommand myCommand = new SqlCommand(str, connection);
            try
            {
                connection.Open();
                myCommand.ExecuteNonQuery();
                MessageBox.Show("База данных 'Airport' создана успешно", "Создание базы данных", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "Создание базы данных", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            finally
            {
                if (connection.State == ConnectionState.Open)
                {
                    connection.Close();
                }
            }
        }
        static public void CreateTables()
        {
            string str;

            using (FileStream fstream = File.OpenRead(@"CREATE TABLES.sql"))
            {
                byte[] array = new byte[fstream.Length]; // Преобразуем строку в байты
                fstream.Read(array, 0, array.Length);    // Считываем данные 
                str = Encoding.Default.GetString(array); // Декодируем байты в строку
            }

            try
            {
                SqlCommand command = new SqlCommand(str, connection);

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


        static public List<string> GetListRows(string _select, string _row)
        {
            dataTable = new DataTable();
            List<string> list = new List<string>();

            try
            {
                SqlCommand command = new SqlCommand("USE [Airport] " + _select, connection);
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
                SqlCommand command = new SqlCommand("USE [Airport] " + sql, connection);

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
                SqlCommand command = new SqlCommand("USE [Airport] " + sql, connection);
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
                SqlCommand command = new SqlCommand("USE [Airport] " + sql, connection);
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
                SqlCommand command = new SqlCommand("USE [Airport] " + sql, connection);
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
                SqlCommand command = new SqlCommand("USE [Airport] " + sql, connection);
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
            string sql = "UPDATE [User] SET [Пароль] = '" + _Password + "', [ФИО] = '" + _FullName + "' WHERE [Логин] = '" + _Login + "'";
            try
            {
                SqlCommand command = new SqlCommand("USE [Airport] " + sql, connection);
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
                command = new SqlCommand("USE [Airport] " + _select, connection);
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
                if (ex.Number == 911)
                    return 911;
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


        public static string Encryption(string ishText, string pass,
               string sol = "doberman", string cryptographicAlgorithm = "SHA1",
               int passIter = 2, string initVec = "a8doSuDitOz1hZe#",
               int keySize = 256)
        {
            if (string.IsNullOrEmpty(ishText))
                return "";

            byte[] initVecB = Encoding.ASCII.GetBytes(initVec);
            byte[] solB = Encoding.ASCII.GetBytes(sol);
            byte[] ishTextB = Encoding.UTF8.GetBytes(ishText);

            PasswordDeriveBytes derivPass = new PasswordDeriveBytes(pass, solB, cryptographicAlgorithm, passIter);
            byte[] keyBytes = derivPass.GetBytes(keySize / 8);
            RijndaelManaged symmK = new RijndaelManaged();
            symmK.Mode = CipherMode.CBC;

            byte[] cipherTextBytes = null;

            using (ICryptoTransform encryptor = symmK.CreateEncryptor(keyBytes, initVecB))
            {
                using (MemoryStream memStream = new MemoryStream())
                {
                    using (CryptoStream cryptoStream = new CryptoStream(memStream, encryptor, CryptoStreamMode.Write))
                    {
                        cryptoStream.Write(ishTextB, 0, ishTextB.Length);
                        cryptoStream.FlushFinalBlock();
                        cipherTextBytes = memStream.ToArray();
                        memStream.Close();
                        cryptoStream.Close();
                    }
                }
            }

            symmK.Clear();
            return Convert.ToBase64String(cipherTextBytes);
        }
        public static string Decryption(string ciphText, string pass,
               string sol = "doberman", string cryptographicAlgorithm = "SHA1",
               int passIter = 2, string initVec = "a8doSuDitOz1hZe#",
               int keySize = 256)
        {
            if (string.IsNullOrEmpty(ciphText))
                return "";

            byte[] initVecB = Encoding.ASCII.GetBytes(initVec);
            byte[] solB = Encoding.ASCII.GetBytes(sol);
            byte[] cipherTextBytes = Convert.FromBase64String(ciphText);

            PasswordDeriveBytes derivPass = new PasswordDeriveBytes(pass, solB, cryptographicAlgorithm, passIter);
            byte[] keyBytes = derivPass.GetBytes(keySize / 8);

            RijndaelManaged symmK = new RijndaelManaged();
            symmK.Mode = CipherMode.CBC;

            byte[] plainTextBytes = new byte[cipherTextBytes.Length];
            int byteCount = 0;

            using (ICryptoTransform decryptor = symmK.CreateDecryptor(keyBytes, initVecB))
            {
                using (MemoryStream mSt = new MemoryStream(cipherTextBytes))
                {
                    using (CryptoStream cryptoStream = new CryptoStream(mSt, decryptor, CryptoStreamMode.Read))
                    {
                        byteCount = cryptoStream.Read(plainTextBytes, 0, plainTextBytes.Length);
                        mSt.Close();
                        cryptoStream.Close();
                    }
                }
            }

            symmK.Clear();
            return Encoding.UTF8.GetString(plainTextBytes, 0, byteCount);
        }
        static public int DecryptionRequest(string _select, DataGrid _dataGrid = null)
        {
            dataTable = new DataTable();
            try
            {
                command = new SqlCommand("USE [Airport] " + _select, connection);
                adapter = new SqlDataAdapter(command);

                connection.Open();
                adapter.Fill(dataTable);

                for (int i = 0; i < dataTable.Rows.Count; i++)
                    for (int j = 0; j < dataTable.Columns.Count; j++)
                        dataTable.Rows[i][j] = Decryption(dataTable.Rows[i][j].ToString(), "1320");

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
        static public List<string> GetDecryptListRows(string _select, string _row)
        {
            dataTable = new DataTable();
            List<string> list = new List<string>();

            try
            {
                SqlCommand command = new SqlCommand("USE [Airport] " + _select, connection);
                adapter = new SqlDataAdapter(command);

                connection.Open();
                adapter.Fill(dataTable);

                int i = 0;
                foreach (DataRow row in dataTable.Rows)
                {
                    if (dataTable.Rows.ToString() == Encryption(_row, "1320"))
                        i++;
                    list.Add(Decryption(row[i].ToString(), "1320"));
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
    }
}