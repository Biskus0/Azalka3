using System;
using System.Data.SqlClient;
using System.Windows;

namespace MyProject
{
    public partial class EditingServices : Window
    {
        public EditingServices()
        {
            InitializeComponent();
        }

        private void AddService_Click(object sender, RoutedEventArgs e)
        {
            string serviceName = ServiceNameTextBox.Text;
            string description = DescriptionTextBox.Text;
            string priceText = PriceTextBox.Text;

            if (string.IsNullOrWhiteSpace(serviceName) || string.IsNullOrWhiteSpace(description) || string.IsNullOrWhiteSpace(priceText))
            {
                MessageBox.Show("Пожалуйста, заполните все поля.");
                return;
            }

            if (!decimal.TryParse(priceText, out decimal price))
            {
                MessageBox.Show("Некорректная цена.");
                return;
            }

            string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                string query = "INSERT INTO Services (ServiceName, Description, Price) VALUES (@serviceName, @description, @price)";
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@serviceName", serviceName);
                command.Parameters.AddWithValue("@description", description);
                command.Parameters.AddWithValue("@price", price);

                command.ExecuteNonQuery();
            }

            MessageBox.Show("Услуга добавлена!");
        }

        private void Back_Click(object sender, RoutedEventArgs e)
        {
            ManagerServicesList managerServicesListWindow = new ManagerServicesList();
            managerServicesListWindow.Show();
            Close();
        }
    }
}
