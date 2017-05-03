using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

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
            }
        }
    }
}
