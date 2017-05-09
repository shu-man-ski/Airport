using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace Airport
{
    public partial class ResultWindow : Window
    {
        public ResultWindow(ComboBox _search, string _table, string _obj)
        {
            InitializeComponent();

            if (_search.Items.IndexOf(_search.Text) < 0)
            {
                MessageBox.Show("Поле для поиска пустое. Выберите из списка");
            }
            else 
            {
                Database.Request("SELECT * FROM " + _table + " WHERE " + _obj + " = " + "'" + _search.Text + "'", searchResult);
                this.Show();
            }
        }
        public ResultWindow(TextBox _search, string _table, string _obj)
        {
            InitializeComponent();

            if (_search.Text == "")
            {
                MessageBox.Show("Поле для поиска пустое. Повторите ввод");
            }
            else
            {
                Database.Request("SELECT * FROM " + _table + " WHERE " + _obj + " = " + "'" + _search.Text + "'", searchResult);
                this.Show();
            }
        }
        public ResultWindow(DatePicker _search, string _table, string _obj)
        {
            InitializeComponent();

            if (_search.SelectedDate == null)
            {
                MessageBox.Show("Поле для поиска пустое. Повторите ввод");
            }
            else
            {
                Database.Request("SELECT * FROM " + _table + " WHERE " + _obj + " = " + "'" + _search.SelectedDate.Value.ToString("d") + "'", searchResult);
                this.Show();
            }
        }
    }
}
