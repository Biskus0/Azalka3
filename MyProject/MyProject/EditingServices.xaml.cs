using System;
using System.Data.SqlClient;
using System.Windows;
using System.Configuration;
using System.Collections.ObjectModel;

namespace MyProject
{
    public partial class EditingServices : Window
    {
        private readonly string connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;

        public ObservableCollection<Service> Services { get; set; }

        public EditingServices()
        {
            InitializeComponent();
            Services = new ObservableCollection<Service>();
            LoadServices();
            ServicesList.ItemsSource = Services;
        }

        // Загрузка списка услуг
        private void LoadServices()
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    string query = "SELECT ServiceID, ServiceName, Description, Price FROM Services";
                    SqlCommand command = new SqlCommand(query, connection);
                    SqlDataReader reader = command.ExecuteReader();

                    Services.Clear();
                    while (reader.Read())
                    {
                        Services.Add(new Service
                        {
                            ServiceID = (int)reader["ServiceID"],
                            ServiceName = reader["ServiceName"].ToString(),
                            Description = reader["Description"].ToString(),
                            Price = (decimal)reader["Price"]
                        });
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка при загрузке данных: " + ex.Message, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        // Добавление новой услуги
        private void AddService_Click(object sender, RoutedEventArgs e)
        {
            string serviceName = ServiceNameTextBox.Text.Trim();
            string description = DescriptionTextBox.Text.Trim();
            decimal price;

            if (string.IsNullOrWhiteSpace(serviceName) || string.IsNullOrWhiteSpace(description) || !decimal.TryParse(PriceTextBox.Text, out price))
            {
                MessageBox.Show("Пожалуйста, заполните все поля корректно.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    string query = "INSERT INTO Services (ServiceName, Description, Price) VALUES (@ServiceName, @Description, @Price)";
                    SqlCommand command = new SqlCommand(query, connection);
                    command.Parameters.AddWithValue("@ServiceName", serviceName);
                    command.Parameters.AddWithValue("@Description", description);
                    command.Parameters.AddWithValue("@Price", price);

                    int rowsAffected = command.ExecuteNonQuery();
                    if (rowsAffected > 0)
                    {
                        MessageBox.Show("Услуга успешно добавлена.", "Успех", MessageBoxButton.OK, MessageBoxImage.Information);
                        LoadServices(); // Перезагрузить список услуг
                    }
                    else
                    {
                        MessageBox.Show("Не удалось добавить услугу.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка при добавлении услуги: " + ex.Message, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        // Удаление услуги
        private void DeleteService_Click(object sender, RoutedEventArgs e)
        {
            var selectedService = ServicesList.SelectedItem as Service;
            if (selectedService == null)
            {
                MessageBox.Show("Выберите услугу для удаления.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    string query = "DELETE FROM Services WHERE ServiceID = @ServiceID";
                    SqlCommand command = new SqlCommand(query, connection);
                    command.Parameters.AddWithValue("@ServiceID", selectedService.ServiceID);

                    int rowsAffected = command.ExecuteNonQuery();
                    if (rowsAffected > 0)
                    {
                        MessageBox.Show("Услуга успешно удалена.", "Успех", MessageBoxButton.OK, MessageBoxImage.Information);
                        LoadServices(); // Перезагрузить список услуг
                    }
                    else
                    {
                        MessageBox.Show("Не удалось удалить услугу.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка при удалении услуги: " + ex.Message, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }

    public class Service
    {
        public int ServiceID { get; set; }
        public string ServiceName { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
    }
}
