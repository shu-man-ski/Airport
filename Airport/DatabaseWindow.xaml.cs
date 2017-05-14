using System.Windows;

namespace Airport
{
    public partial class DatabaseWindow : Window
    {
        public DatabaseWindow()
        {
            InitializeComponent();
        }

        private void craeteDB_Click(object sender, RoutedEventArgs e)
        {
            Database.CreateDatabase();
            Database.CreateTables();
        }
    }
}