using System.Windows;
using System.Windows.Controls;

namespace Airport
{
    public partial class ResultWindow : Window
    {
        private string Table { get; set; }
        private string Obj { get; set;}
        private ComboBox SearchCB { get; set; }
        private TextBox SearchTB { get; set; }
        private DatePicker SearchDP { get; set; }
        private DataGrid DataGrid { get; set; }


        public ResultWindow(string table, string obj, ComboBox searchCB = null, TextBox searchTB = null, DatePicker searchDP = null, DataGrid dataGrid = null)
        {
            InitializeComponent();

            Table = table;
            Obj = obj;
            SearchCB = searchCB;
            SearchTB = searchTB;
            SearchDP = searchDP;
            DataGrid = dataGrid;
            if (SearchCB != null)
            {
                if (SearchCB.Items.IndexOf(SearchCB.Text) < 0)
                    MessageBox.Show("Поле для поиска пустое. Выберите из списка", "Предупреждение", MessageBoxButton.OK, MessageBoxImage.Information);
                    else
                {
                    if (Database.Query("SELECT * FROM " + Table + " WHERE " + obj + " = " + "'" + SearchCB.Text + "'", searchResult) <= 0)
                        MessageBox.Show("Объект не был найден. Убедитесь в корректности введенных данных", "Результат поиска", MessageBoxButton.OK, MessageBoxImage.Information);
                    else
                        Show();
                }
            }
            if (SearchTB != null)
            {
                if (SearchTB.Text == "")
                    MessageBox.Show("Поле для поиска пустое. Повторите ввод", "Предупреждение", MessageBoxButton.OK, MessageBoxImage.Information);
                else
                {
                    if (Database.Query("SELECT * FROM " + Table + " WHERE " + obj + " = " + "'" + SearchTB.Text + "'", searchResult) <= 0)
                        MessageBox.Show("Объект не был найден. Убедитесь в корректности введенных данных", "Результат поиска", MessageBoxButton.OK, MessageBoxImage.Information);
                    else
                        Show();
                }
            }
            if (SearchDP != null)
            {
                if (SearchDP.SelectedDate == null)
                    MessageBox.Show("Поле для поиска пустое. Повторите ввод", "Предупреждение", MessageBoxButton.OK, MessageBoxImage.Information);
                else
                {
                    if (Database.Query("SELECT * FROM " + Table + " WHERE " + obj + " = " + "'" + SearchDP.SelectedDate.Value.ToString("d") + "'", searchResult) <= 0)
                        MessageBox.Show("Объект не был найден. Убедитесь в корректности введенных данных", "Результат поиска", MessageBoxButton.OK, MessageBoxImage.Information);
                    else
                        Show();
                }
            }
        }

        private void Result_DeleteAll_Click(object sender, RoutedEventArgs e)
        {
            if (SearchCB != null)
                Database.Query("DELETE FROM " + Table + " WHERE " + Obj + " = " + "'" + SearchCB.Text + "'");
            if (SearchTB != null)
                Database.Query("DELETE FROM " + Table + " WHERE " + Obj + " = " + "'" + SearchTB.Text + "'");
            if (SearchDP != null)
                Database.Query("DELETE FROM " + Table + " WHERE " + Obj + " = " + "'" + SearchDP.SelectedDate.Value.ToString("d") + "'");
            Database.Query("SELECT * FROM " + Table, DataGrid);
            Close();
        }
    }
}
