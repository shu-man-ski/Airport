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
            userUpdateLogin.ItemsSource = Database.GetListForComboBox("SELECT [Логин] FROM [User]", "[Логин]");
            userDeleteByLogin.ItemsSource = Database.GetListForComboBox("SELECT [Логин] FROM [User]", "[Логин]");

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
                userUpdateLogin.ItemsSource = Database.GetListForComboBox("SELECT [Логин] FROM [User]", "[Логин]");
            }
            else
                MessageBox.Show("Проверьте, заполнены ли все поля", "Предупреждение", MessageBoxButton.OK, MessageBoxImage.Warning);
        }
        private void UserUpdate_Click(object sender, RoutedEventArgs e)
        {
            if ((userUpdateLogin.Items.IndexOf(userUpdateLogin.Text) >= 0) && userUpdatePassword.Text != "" && userUpdateFullName.Text != "")
            {
                user.Login = userUpdateLogin.Text;
                user.Password = userUpdatePassword.Text;
                user.FullName = userUpdateFullName.Text;

                Database.UpdateUser(user.Login, user.Password, user.FullName);
                userUpdateLogin.ItemsSource = Database.GetListForComboBox("SELECT [Логин] FROM [User]", "[Логин]");
                Database.Request("SELECT * FROM [User]", userGrid);
            }
            else
                MessageBox.Show("Проверьте, заполнены ли все поля", "Предупреждение", MessageBoxButton.OK, MessageBoxImage.Warning);
        }
        private void User_DeleteByLogin_Click(object sender, RoutedEventArgs e)
        {
            if ((userDeleteByLogin.Items.IndexOf(userDeleteByLogin.Text) >= 0))
            {
                user.Login = userDeleteByLogin.Text;

                Database.Request("DELETE FROM [User] WHERE [Логин] = '" + user.Login + "'");
                userUpdateLogin.ItemsSource = Database.GetListForComboBox("SELECT [Логин] FROM [User]", "[Логин]");
                Database.Request("SELECT * FROM [User]", userGrid);
            }
            else
                MessageBox.Show("Выберите логин пользователя из списка", "Предупреждение", MessageBoxButton.OK, MessageBoxImage.Warning);
        }
    }
}
