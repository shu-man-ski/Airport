using System.Windows;

namespace Airport
{
    public partial class UsersWindow : Window
    {
        User user;
        public UsersWindow()
        {
            InitializeComponent();

            Database.Request("SELECT * FROM [User]", userGrid);

            user = new User();
        }

        private void UserAdd_Click(object sender, RoutedEventArgs e)
        {
            if (userAddLogin.Text != "" && userAddPassword.Text != "" && userAddFullName.Text != "")
            {
                user.Login = userAddLogin.Text;
                user.Password = userAddPassword.Text;
                user.FullName = userAddFullName.Text;

                Database.AddUser(user.Login, user.Password, user.FullName);
                Database.Request("SELECT * FROM [User]", userGrid);
            }
            else
                MessageBox.Show("Проверьте, заполнены ли все поля, и убедитесь в их корректности", "Предупреждение", MessageBoxButton.OK, MessageBoxImage.Warning);
        }
        private void UserUpdate_Click(object sender, RoutedEventArgs e)
        {
            if (userUpdateLogin.Text != "" && userUpdatePassword.Text != "" && userUpdateFullName.Text != "")
            {
                user.Login = userUpdateLogin.Text;
                user.Password = userUpdatePassword.Text;
                user.FullName = userUpdateFullName.Text;

                Database.UpdateUser(user.Login, user.Password, user.FullName);
                Database.Request("SELECT * FROM [User]", userGrid);
            }
            else
                MessageBox.Show("Проверьте, заполнены ли все поля, и убедитесь в их корректности", "Предупреждение", MessageBoxButton.OK, MessageBoxImage.Warning);
        }
        private void User_DeleteByLogin_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
