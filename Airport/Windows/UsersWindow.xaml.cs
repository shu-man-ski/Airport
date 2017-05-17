using System.Windows;

namespace Airport
{
    public partial class UsersWindow : Window
    {
        User user;
        public UsersWindow()
        {
            InitializeComponent();

            Database.Query("SELECT * FROM [User]", userGrid, necessaryDecrypt: true);
            UpdateAllCombobx();

            user = new User();
        }

        private void UpdateAllCombobx()
        {
            userUpdateLogin.ItemsSource = Database.GetListOfRowsToColumn("SELECT [Логин] FROM [User]", "[Логин]", necessaryDecrypt: true);
            userDeleteByLogin.ItemsSource = Database.GetListOfRowsToColumn("SELECT [Логин] FROM [User] WHERE [Логин] != '" + Cryptographer.Encryption("Admin") + "'", "[Логин]", necessaryDecrypt: true);
        }
        private void User_Add_Click(object sender, RoutedEventArgs e)
        {
            if (userAddLogin.Text != "" && userAddPassword.Text != "" && userAddFullName.Text != "")
            {
                user.Login = userAddLogin.Text;
                user.Password = userAddPassword.Text;
                user.FullName = userAddFullName.Text;

                string query = "INSERT INTO [User]([Логин], [Пароль],  [ФИО])" +
                                          "VALUES (@Login,  @Password, @FullName)";
                string[] sqlVariables = { "Login", "Password", "FullName" };
                object[] obj = { Cryptographer.Encryption(user.Login), Cryptographer.Encryption(user.Password), Cryptographer.Encryption(user.FullName) };
                if (Database.Query(query, sqlVariables, obj) == 2627)
                    MessageBox.Show("Невозможно добавить новый объект, так как уже имеется пользователь с таким логином", "Предупреждение", MessageBoxButton.OK, MessageBoxImage.Warning);
                Database.Query("SELECT * FROM [User]", userGrid, necessaryDecrypt: true);
                UpdateAllCombobx();
            }
            else
                MessageBox.Show("Проверьте, заполнены ли все поля", "Предупреждение", MessageBoxButton.OK, MessageBoxImage.Warning);
        }
        private void User_Update_Click(object sender, RoutedEventArgs e)
        {
            if ((userUpdateLogin.Items.IndexOf(userUpdateLogin.Text) >= 0) && userUpdatePassword.Text != "" && userUpdateFullName.Text != "")
            {
                user.Login = userUpdateLogin.Text;
                user.Password = userUpdatePassword.Text;
                user.FullName = userUpdateFullName.Text;

                string query = "UPDATE [User] SET [Пароль] = '" + Cryptographer.Encryption(user.Password) + "', [ФИО] = '" + Cryptographer.Encryption(user.FullName) + "' WHERE [Логин] = '" + Cryptographer.Encryption(user.Login) + "'";
                string[] sqlVariables = { "Login", "Password", "FullName" };
                object[] obj = { Cryptographer.Encryption(user.Login), Cryptographer.Encryption(user.Password), Cryptographer.Encryption(user.FullName) };
                Database.Query(query, sqlVariables, obj);
                Database.Query("SELECT * FROM [User]", userGrid, necessaryDecrypt: true);
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

                Database.Query("DELETE FROM [User] WHERE [Логин] = '" + Cryptographer.Encryption(user.Login) + "'");
                Database.Query("SELECT * FROM [User]", userGrid, necessaryDecrypt: true);
                UpdateAllCombobx();
            }
            else
                MessageBox.Show("Выберите логин пользователя из списка", "Предупреждение", MessageBoxButton.OK, MessageBoxImage.Warning);
        }
    }
}
