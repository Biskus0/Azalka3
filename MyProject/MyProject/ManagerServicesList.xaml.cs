using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Windows;
using System.Windows.Controls;

namespace MyProject
{
    public partial class ManagerServicesList : Window
    {
        public ManagerServicesList()
        {
            InitializeComponent();
            LoadServicesData();
        }

        public class Service
        {
            public string ServiceName { get; set; }
            public string Description { get; set; }
            public decimal Price { get; set; }
            public int ServiceID { get; set; }
        }

        private void LoadServicesData()
        {
            List<Service> serviceList = new List<Service>();
            string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                string query = "SELECT ServiceID, ServiceName, Description, Price FROM Services";

                SqlCommand command = new SqlCommand(query, connection);
                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    serviceList.Add(new Service
                    {
                        ServiceID = (int)reader["ServiceID"],
                        ServiceName = reader["ServiceName"].ToString(),
                        Description = reader["Description"].ToString(),
                        Price = Convert.ToDecimal(reader["Price"])
                    });
                }
            }

            ServicesDataGrid.ItemsSource = serviceList;
        }

        private void DeleteService_Click(object sender, RoutedEventArgs e)
        {
            Button deleteButton = sender as Button;
            Service serviceToDelete = deleteButton.DataContext as Service;

            if (serviceToDelete != null)
            {
                string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    string query = "DELETE FROM Services WHERE ServiceID = @serviceID";
                    SqlCommand command = new SqlCommand(query, connection);
                    command.Parameters.AddWithValue("@serviceID", serviceToDelete.ServiceID);

                    int rowsAffected = command.ExecuteNonQuery();
                    if (rowsAffected > 0)
                        MessageBox.Show("Услуга удалена!");
                    else
                        MessageBox.Show("Не удалось удалить услугу.");
                }

                LoadServicesData();
            }
        }

        private void EditService_Click(object sender, RoutedEventArgs e)
        {
            EditingServices editingServicesWindow = new EditingServices();
            editingServicesWindow.Show();
            Close();
        }

        private void Back_Click(object sender, RoutedEventArgs e)
        {
            Manager_main managerMainWindow = new Manager_main();
            managerMainWindow.Show();
            Close();
        }
    }
}
