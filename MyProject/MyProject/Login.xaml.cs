using System.Data.SqlClient;
using System.Windows;
using System.Configuration;

namespace MyProject
{
    public partial class Login : Window
    {
        private readonly string connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
        public Login()
        {
            InitializeComponent();
        }

        private void BtnLogin_Click(object sender, RoutedEventArgs e)
        {
            string username = userNameBox.Text;
            string password = passwordBox.Password;

            if (string.IsNullOrWhiteSpace(username) || string.IsNullOrWhiteSpace(password))
            {
                MessageBox.Show("Введите логин и пароль.", "Предупреждение", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                string query = "SELECT Role FROM Users WHERE Username = @username AND Password = @password";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@username", username);
                    command.Parameters.AddWithValue("@password", password);

                    object role = command.ExecuteScalar();
                    if (role != null)
                    {
                        Window nextWindow;
                        if (role.ToString() == "Admin")
                        {
                            nextWindow = new Manager_main();
                        }
                        else
                        {
                            nextWindow = new Customer_main();
                        }

                        nextWindow.Show();
                        Close();
                    }
                    else
                    {
                        MessageBox.Show("Неверный логин или пароль!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }
            }
        }
    }
}
