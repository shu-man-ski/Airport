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
    /// <summary>
    /// Логика взаимодействия для AuthorizationWindow.xaml
    /// </summary>
    public partial class AuthorizationWindow : Window
    {
        public AuthorizationWindow AuthorizationWnd { get; set; }
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
        private void Accept_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = true;
        }
        public void Button_Click(object sender, RoutedEventArgs e)
        {
            MainWindow.AuthorizationWnd = false;
        }
    }
}
