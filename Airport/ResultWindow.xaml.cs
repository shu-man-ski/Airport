using System.Windows;
using System.Windows.Controls;

namespace Airport
{
    public partial class ResultWindow : Window
    {
        private string Table { get; set; }
        private string Obj { get; set;}
        private ComboBox searchCB = null;
        private TextBox searchTB = null;
        private DatePicker searchDP = null;
        private DataGrid dataGrid = null;


        public ResultWindow(string _table, string _obj, ComboBox _searchCB = null, TextBox _searchTB = null, DatePicker _searchDP = null, DataGrid _dataGrid = null)
        {
            InitializeComponent();

            Table = _table;
            Obj = _obj;
            searchCB = _searchCB;
            searchTB = _searchTB;
            searchDP = _searchDP;
            dataGrid = _dataGrid;
            if (_searchCB != null)
            {
                if (_searchCB.Items.IndexOf(_searchCB.Text) < 0)
                {
                    MessageBox.Show("Поле для поиска пустое. Выберите из списка");
                }
                else
                {
                    if (Database.Request("SELECT * FROM " + _table + " WHERE " + _obj + " = " + "'" + _searchCB.Text + "'", searchResult) <= 0)
                        MessageBox.Show("Такой объект не найден");
                    else
                        this.Show();
                }
            }
            if (_searchTB != null)
            {

                if (_searchTB.Text == "")
                {
                    MessageBox.Show("Поле для поиска пустое. Повторите ввод");
                }
                else
                {
                    if (Database.Request("SELECT * FROM " + _table + " WHERE " + _obj + " = " + "'" + _searchTB.Text + "'", searchResult) <= 0)
                        MessageBox.Show("Такой объект не найден");
                    else
                        this.Show();
                }
            }
            if (_searchDP != null)
            {
                if (_searchDP.SelectedDate == null)
                {
                    MessageBox.Show("Поле для поиска пустое. Повторите ввод");
                }
                else
                {
                    if (Database.Request("SELECT * FROM " + _table + " WHERE " + _obj + " = " + "'" + _searchDP.SelectedDate.Value.ToString("d") + "'", searchResult) <= 0)
                        MessageBox.Show("Такой объект не найден");
                    else
                        this.Show();
                }
            }
        }

        private void Result_DeleteAll_Click(object sender, RoutedEventArgs e)
        {
            if (searchCB != null)
            {
                Database.Request("DELETE FROM " + Table + " WHERE " + Obj + " = " + "'" + searchCB.Text + "'");
            }
            if (searchTB != null)
            {
                Database.Request("DELETE FROM " + Table + " WHERE " + Obj + " = " + "'" + searchTB.Text + "'");
            }
            if (searchDP != null)
            {
                Database.Request("DELETE FROM " + Table + " WHERE " + Obj + " = " + "'" + searchDP.SelectedDate.Value.ToString("d") + "'");
            }
            Database.Request("SELECT * FROM " + Table, dataGrid);
            this.Close();
        }
    }
}
