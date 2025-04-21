using System;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Windows;

namespace MyProject
{
    public partial class CreateEvent : Window
    {
        private readonly string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;

        public CreateEvent()
        {
            InitializeComponent();
        }

        private void EventDatePicker_SelectedDateChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            if (EventDatePicker.SelectedDate != null)
            {
                LocationComboBox.IsEnabled = true;
                LoadAvailableLocations(EventDatePicker.SelectedDate.Value);
            }
            else
            {
                LocationComboBox.IsEnabled = false;
                LocationComboBox.ItemsSource = null;
            }
        }

        private void LoadAvailableLocations(DateTime selectedDate)
        {
            try
            {
                LocationComboBox.ItemsSource = null;

                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    string query = @"
                        SELECT * FROM Locations
                        WHERE LocationID NOT IN (
                            SELECT LocationID FROM Events WHERE EventDate = @SelectedDate
                        )";

                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@SelectedDate", selectedDate.Date);

                    SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    adapter.Fill(dt);

                    LocationComboBox.ItemsSource = dt.AsEnumerable().Select(row => new
                    {
                        LocationID = row.Field<int>("LocationID"),
                        LocationName = row.Field<string>("LocationName")
                    }).ToList();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка при загрузке залов: " + ex.Message);
            }
        }

        private void ResetForm()
        {
            EventNameTextBox.Text = string.Empty;
            EventDatePicker.SelectedDate = null;
            LocationComboBox.SelectedIndex = -1;
            LocationComboBox.ItemsSource = null;
            ClientNameTextBox.Text = string.Empty;
            ClientPhoneTextBox.Text = string.Empty;
            ClientEmailTextBox.Text = string.Empty;
        }

        private void CreateEventButton_Click(object sender, RoutedEventArgs e)
        {
            if (EventNameTextBox.Text == "" || EventDatePicker.SelectedDate == null || LocationComboBox.SelectedValue == null ||
                string.IsNullOrWhiteSpace(ClientNameTextBox.Text) || string.IsNullOrWhiteSpace(ClientPhoneTextBox.Text) || string.IsNullOrWhiteSpace(ClientEmailTextBox.Text))
            {
                MessageBox.Show("Пожалуйста, заполните все поля.");
                return;
            }

            string name = EventNameTextBox.Text;
            DateTime date = EventDatePicker.SelectedDate.Value;
            int locationId = (int)LocationComboBox.SelectedValue;
            string clientName = ClientNameTextBox.Text;
            string clientPhone = ClientPhoneTextBox.Text;
            string clientEmail = ClientEmailTextBox.Text;

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    string insertClient = "INSERT INTO Clients (ClientName, ClientPhone, Email) OUTPUT INSERTED.ClientID VALUES (@ClientName, @ClientPhone, @Email)";
                    SqlCommand clientCmd = new SqlCommand(insertClient, conn);
                    clientCmd.Parameters.AddWithValue("@ClientName", clientName);
                    clientCmd.Parameters.AddWithValue("@ClientPhone", clientPhone);
                    clientCmd.Parameters.AddWithValue("@Email", clientEmail);

                    int clientId = (int)clientCmd.ExecuteScalar();

                    string insertEvent = "INSERT INTO Events (EventName, EventDate, LocationID) OUTPUT INSERTED.EventID VALUES (@Name, @Date, @LocationID)";
                    SqlCommand eventCmd = new SqlCommand(insertEvent, conn);
                    eventCmd.Parameters.AddWithValue("@Name", name);
                    eventCmd.Parameters.AddWithValue("@Date", date);
                    eventCmd.Parameters.AddWithValue("@LocationID", locationId);

                    int newEventId = (int)eventCmd.ExecuteScalar();

                    string insertInvoice = "INSERT INTO Invoices (ClientID, EventID, InvoiceDate, TotalAmount) VALUES (@ClientID, @EventID, @InvoiceDate, @TotalAmount)";
                    SqlCommand invoiceCmd = new SqlCommand(insertInvoice, conn);
                    invoiceCmd.Parameters.AddWithValue("@ClientID", clientId);
                    invoiceCmd.Parameters.AddWithValue("@EventID", newEventId);
                    invoiceCmd.Parameters.AddWithValue("@InvoiceDate", date);
                    invoiceCmd.Parameters.AddWithValue("@TotalAmount", 0); 

                    invoiceCmd.ExecuteNonQuery();

                    MessageBox.Show("Мероприятие успешно добавлено!");
                    ResetForm();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка при добавлении мероприятия: " + ex.Message);
            }
        }

        private void Back_click(object sender, RoutedEventArgs e)
        {
            Customer_main Customer_mainWindow = new Customer_main();
            Customer_mainWindow.Show();
            Close();
        }
    }
}
