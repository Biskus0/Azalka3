using System.Data.SqlClient;
using System.Windows;

namespace MyProject
{
    public partial class EditingStaff : Window
    {
        public EditingStaff()
        {
            InitializeComponent();
        }

        private void AddStaff_Click(object sender, RoutedEventArgs e)
        {
            string name = NameTextBox.Text;
            string role = RoleTextBox.Text;
            string salaryText = SalaryTextBox.Text;
            string phone = PhoneTextBox.Text;

            if (string.IsNullOrWhiteSpace(name) || string.IsNullOrWhiteSpace(role) || string.IsNullOrWhiteSpace(salaryText) || string.IsNullOrWhiteSpace(phone))
            {
                MessageBox.Show("Пожалуйста, заполните все поля.");
                return;
            }

            if (!decimal.TryParse(salaryText, out decimal salary))
            {
                MessageBox.Show("Некорректная зарплата.");
                return;
            }

            if (phone.Length != 11 || !long.TryParse(phone, out _))
            {
                MessageBox.Show("Номер телефона должен состоять из 11 цифр.");
                return;
            }

            string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                string query = "INSERT INTO Staff (StaffName, StaffRole, StaffSalary, StaffPhone) VALUES (@name, @role, @salary, @phone)";
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@name", name);
                command.Parameters.AddWithValue("@role", role);
                command.Parameters.AddWithValue("@salary", salary);
                command.Parameters.AddWithValue("@phone", phone);

                command.ExecuteNonQuery();
            }

            MessageBox.Show("Сотрудник добавлен!");
        }

        private void DeleteStaff_Click(object sender, RoutedEventArgs e)
        {
            string nameToDelete = DeleteNameTextBox.Text;

            if (string.IsNullOrWhiteSpace(nameToDelete))
            {
                MessageBox.Show("Введите имя для удаления.");
                return;
            }

            string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                string query = "DELETE FROM Staff WHERE StaffName = @name";
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@name", nameToDelete);

                int rowsAffected = command.ExecuteNonQuery();

                if (rowsAffected > 0)
                    MessageBox.Show("Сотрудник удалён.");
                else
                    MessageBox.Show("Сотрудник не найден.");
            }
        }

        private void Back_Click(object sender, RoutedEventArgs e)
        {
            Staff staffWindow = new Staff();
            staffWindow.Show();
            Close();
        }
    }
}
