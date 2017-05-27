using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using System.IO;

namespace Airport
{
    class Database : MainWindow
    {
        static private SqlCommand command;       /* Инструкция T-SQL (или хранимая процедура) */
        static private SqlConnection connection; /* Подключение к базе данных SQL Server */
        static private SqlDataAdapter adapter;   /* Набор команд данных и подключение к БД */
        static private DataTable dataTable;      /* Таблица данных в памяти */


        static Database()
        {
            connection = new SqlConnection(@"Server=.\SQLEXPRESS;Integrated security=SSPI;database=master");
            adapter = null;
            command = null;
            dataTable = null;
        }


        static public void CreateDatabase(string path)
        {
            string query = "CREATE DATABASE [Airport]" +
                           "ON PRIMARY" +
                           "(" +
                              " NAME = N'airportdb'," +
                              " FILENAME = N'" + path + @"\airportdb.mdf'," +
                              " SIZE = 15MB," +
                              " MAXSIZE = UNLIMITED," +
                              " FILEGROWTH = 1024KB" +
                           ")" +
                           "LOG ON " +
                            "(" +
                           "NAME = N'airportdb_log'," +
                           "FILENAME = N'" + path + @"\airportdb_log.ldf'," +
                           "SIZE = 1024KB," +
                           "MAXSIZE = 2048GB," +
                           "FILEGROWTH = 10 %" +
                           ")" +
                          @" ALTER AUTHORIZATION ON DATABASE::[Airport] TO[NT AUTHORITY\СИСТЕМА]; ";
            try
            {
                command = new SqlCommand(query, connection);
                connection.Open();
                command.ExecuteNonQuery();
                MessageBox.Show("База данных 'Airport' создана успешно", "Создание базы данных", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Исключение создания базы данных (Exception)", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
            finally
            {
                if (connection.State == ConnectionState.Open)
                    connection.Close();
            }
        }
        static public void CreateTables()
        {
            string str;
            using (FileStream fstream = File.OpenRead(@"create_tables.sql"))
            {
                byte[] array = new byte[fstream.Length]; // Преобразуем строку в байты
                fstream.Read(array, 0, array.Length);    // Считываем данные 
                str = Encoding.Default.GetString(array); // Декодируем байты в строку
            }

            try
            {
                command = new SqlCommand(str, connection);
                connection.Open();
                command.ExecuteNonQuery();
            }
            catch (SqlException ex)
            {
                MessageBox.Show(ex.Message, "Исключение (SqlException)", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Исключение (Exception)", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
            finally
            {
                if (connection != null)
                    connection.Close();
            }
        }
        static public void DropDatabase()
        {
            try
            {
                command = new SqlCommand("DROP DATABASE [Airport]", connection);
                connection.Open();
                command.ExecuteNonQuery();
                MessageBox.Show("База данных 'Airport' успешно удалена", "Удаление базы данных", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            catch (SqlException ex)
            {
                MessageBox.Show(ex.Message, "Исключение (SqlException)", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Исключение (Exception)", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
            finally
            {
                if (connection != null)
                    connection.Close();
            }
        }


        static public int Query(string query                  /* Строка запроса */,
                                string[] sqlVariables         /* Переменные в T-SQL */,
                                object[] obj                  /* Переменные, значения которых необходимо передать */)
        {
            try
            {
                command = new SqlCommand("use [Airport] " + query, connection);
                adapter = new SqlDataAdapter(command);
                if (sqlVariables != null && obj != null)
                    if (sqlVariables.Length == obj.Length)
                        for (int i = 0; i < obj.Length; i++)
                        {
                            command.Parameters.AddWithValue('@' + sqlVariables[i], obj[i]);
                        }
                    else
                        throw new Exception();
                connection.Open();
                command.ExecuteNonQuery();
            }
            catch (SqlException ex)
            {
                if (ex.Number == 2627) /* Ошибка первичного ключа */
                    return 2627;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Исключение (Exception)", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
            finally
            {
                if (connection.State == ConnectionState.Open)
                    connection.Close();
            }
            return 0;
        }
        static public int Query(string query                  /* Строка запроса*/,
                                DataGrid dataGrid = null      /* Вывод данных в DataGrid */,
                                bool necessaryDecrypt = false /* Необходима ли дешифровка */)
        {
            dataTable = new DataTable();
            try
            {
                command = new SqlCommand("USE [Airport] " + query, connection);
                adapter = new SqlDataAdapter(command);
                connection.Open();

                adapter.Fill(dataTable);

                if (necessaryDecrypt)
                    for (int i = 0; i < dataTable.Rows.Count; i++)
                        for (int j = 0; j < dataTable.Columns.Count; j++)
                            dataTable.Rows[i][j] = Cryptographer.Decryption(dataTable.Rows[i][j].ToString());

                if (dataGrid != null)
                    dataGrid.ItemsSource = dataTable.DefaultView;
            }
            catch (SqlException ex)
            {
                if (ex.Number == 547) /* Ошибка внешнего ключа */
                    MessageBox.Show("Невозможно выполнить дествие, так как текущий объект имеет связь с данными из другой таблицы", "Исключение (SqlException)", MessageBoxButton.OK, MessageBoxImage.Warning);
                if (ex.Number == 911) /* Ошибка базы данных (не существует) */
                    return 911;
                return 0;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Исключение (Exception)", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
            finally
            {
                if (connection.State == ConnectionState.Open)
                    connection.Close();
            }
            return dataTable.Rows.Count;
        }
        static public List<string> GetListOfRowsToColumn(string query                    /* Строка запроса*/,
                                                         string column                   /* Название столбца */, 
                                                         bool   necessaryDecrypt = false /* Необходима ли дешифровка */)
        {
            dataTable = new DataTable();
            List<string> listOfRows = new List<string>();
            try
            {
                command = new SqlCommand("USE [Airport] " + query, connection);
                adapter = new SqlDataAdapter(command);
                connection.Open();

                adapter.Fill(dataTable);
                int i = 0;
                foreach (DataRow row in dataTable.Rows)
                {
                    if (necessaryDecrypt)
                    {
                        if (dataTable.Rows.ToString() == Cryptographer.Encryption(column)) i++;
                        listOfRows.Add(Cryptographer.Decryption(row[i].ToString()));
                    }
                    else
                    {
                        if (dataTable.Columns.ToString() == column) i++;
                        listOfRows.Add(row[i].ToString());
                    }
                }
            }
            catch (SqlException ex)
            {
                MessageBox.Show(ex.Message, "Исключение (SqlException)", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Исключение (Exception)", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
            finally
            {
                if (connection.State == ConnectionState.Open)
                    connection.Close();
            }
            return listOfRows;
        }
    }
}