using System.Windows;

namespace Airport
{
    public partial class UsersWindow : Window
    {
        User user;
        public UsersWindow()
        {
            InitializeComponent();

            Database.DecryptionRequest("SELECT * FROM [User]", userGrid);
            UpdateAllCombobx();

            user = new User();
        }

        private void UpdateAllCombobx()
        {
            userUpdateLogin.ItemsSource = Database.GetDecryptListRows("SELECT [Логин] FROM [User]", "[Логин]");
            userDeleteByLogin.ItemsSource = Database.GetDecryptListRows("SELECT [Логин] FROM [User] WHERE [Логин] != '" + Database.Encryption("Admin", "1320") + "'", "[Логин]");
        }
        private void UserAdd_Click(object sender, RoutedEventArgs e)
        {
            if (userAddLogin.Text != "" && userAddPassword.Text != "" && userAddFullName.Text != "")
            {
                user.Login = userAddLogin.Text;
                user.Password = userAddPassword.Text;
                user.FullName = userAddFullName.Text;

                Database.AddUser(Database.Encryption(user.Login, "1320"), Database.Encryption(user.Password, "1320"), Database.Encryption(user.FullName, "1320"));
                Database.DecryptionRequest("SELECT * FROM [User]", userGrid);
                UpdateAllCombobx();
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

                Database.UpdateUser(Database.Encryption(user.Login, "1320"), Database.Encryption(user.Password, "1320"), Database.Encryption(user.FullName, "1320"));
                Database.DecryptionRequest("SELECT * FROM [User]", userGrid);
                UpdateAllCombobx();
            }
            else
                MessageBox.Show("Проверьте, заполнены ли все поля", "Предупреждение", MessageBoxButton.OK, MessageBoxImage.Warning);
        }
        private void User_DeleteByLogin_Click(object sender, RoutedEventArgs e)
        {
            if ((userDeleteByLogin.Items.IndexOf(userDeleteByLogin.Text) >= 0))
            {
                user.Login = userDeleteByLogin.Text;

                Database.Request("DELETE FROM [User] WHERE [Логин] = '" + Database.Encryption(user.Login, "1320") + "'");
                Database.DecryptionRequest("SELECT * FROM [User]", userGrid);
                UpdateAllCombobx();
            }
            else
                MessageBox.Show("Выберите логин пользователя из списка", "Предупреждение", MessageBoxButton.OK, MessageBoxImage.Warning);
        }
    }
}
