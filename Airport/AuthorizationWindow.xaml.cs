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
