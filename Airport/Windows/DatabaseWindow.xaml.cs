using System.Windows;

namespace Airport
{
    public partial class DatabaseWindow : Window
    {
        public DatabaseWindow()
        {
            InitializeComponent();
        }

        private void CraeteDB_Click(object sender, RoutedEventArgs e)
        {
            if (databaseWay.Text != "")
            {
                Database.CreateDatabase(databaseWay.Text);
                Database.CreateTables();
            }
            else
                MessageBox.Show("Вы не ввели путь. Повторите ввод", "Предупреждение", MessageBoxButton.OK, MessageBoxImage.Warning);
        }
    }
}