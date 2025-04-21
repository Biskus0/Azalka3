using System.Collections.Generic;
using System.Data.SqlClient;
using System.Windows;

namespace MyProject
{
    public partial class Clients : Window
    {
        private int eventID;

        public Clients(int eventID)
        {
            InitializeComponent();
            this.eventID = eventID;
            LoadClients();
        }

        private void LoadClients()
        {
            List<ClientModel> clients = new List<ClientModel>();

            string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;

            string query = @"
                SELECT c.ClientName, c.ClientPhone, c.Email
                FROM Clients c
                INNER JOIN Invoices i ON c.ClientID = i.ClientID
                WHERE i.EventID = @eventID
            ";

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@eventID", eventID);
                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    clients.Add(new ClientModel
                    {
                        ClientName = reader.GetString(0),
                        ClientPhone = reader.GetString(1),
                        Email = reader.GetString(2)
                    });
                }
            }

            ClientsDataGrid.ItemsSource = clients;
        }

        private void Back_click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }

    public class ClientModel
    {
        public string ClientName { get; set; }
        public string ClientPhone { get; set; }
        public string Email { get; set; }
    }
}
