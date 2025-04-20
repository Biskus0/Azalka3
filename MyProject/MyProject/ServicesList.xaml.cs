using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Windows;

namespace MyProject
{
    public partial class ServicesList : Window
    {
        public ServicesList()
        {
            InitializeComponent();
            LoadServices();
        }

        private void LoadServices()
        {
            List<ServiceModel> services = new List<ServiceModel>();
            string connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;

            string query = "SELECT ServiceID, ServiceName, Description, Price FROM Services";

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand(query, conn);
                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    services.Add(new ServiceModel
                    {
                        ServiceID = reader.GetInt32(0),
                        ServiceName = reader.GetString(1),
                        Description = reader.IsDBNull(2) ? "" : reader.GetString(2),
                        Price = reader.GetDecimal(3)
                    });
                }
            }

            ServicesDataGrid.ItemsSource = services;
        }

        private void Back_click(object sender, RoutedEventArgs e)
        {
            Customer_main Customer_mainWindow = new Customer_main();
            Customer_mainWindow.Show();
            Close();
        }
    }
    public class ServiceModel
    {
        public int ServiceID { get; set; }
        public string ServiceName { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
    }
}
