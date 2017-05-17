using System.Windows;

namespace Airport
{
    public partial class AuthorizationWindow : Window
    {
        public string Login
        {
            get { return loginBox.Text; }
        }
        public string Password
        {
            get { return passwordBox.Password; }
        }

        public AuthorizationWindow()
        {
            InitializeComponent();
        }

        private void Entrance_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = true;
        }
        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            MainWindow.AuthorizationWnd = false;
        }      
    }
}
