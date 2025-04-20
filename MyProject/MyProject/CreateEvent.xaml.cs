using System;
using System.Collections.Generic;
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
            LoadServices(); 
        }

        private void EventDatePicker_SelectedDateChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            if (EventDatePicker.SelectedDate != null)
            {
                LocationComboBox.IsEnabled = true;
                ServiceListBox.IsEnabled = true;

                LoadAvailableLocations(EventDatePicker.SelectedDate.Value);
            }
            else
            {
                LocationComboBox.IsEnabled = false;
                ServiceListBox.IsEnabled = false;

                LocationComboBox.ItemsSource = null;
                ServiceListBox.ItemsSource = null;
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
        private void LoadServices()
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    string query = "SELECT ServiceID, ServiceName FROM Services";
                    SqlCommand cmd = new SqlCommand(query, conn);
                    SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    adapter.Fill(dt);

                    ServiceListBox.ItemsSource = dt.AsEnumerable().Select(row => new
                    {
                        ServiceID = row.Field<int>("ServiceID"),
                        ServiceName = row.Field<string>("ServiceName")
                    }).ToList();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка при загрузке услуг: " + ex.Message);
            }
        }

        private void ResetForm()
        {
            EventNameTextBox.Text = string.Empty;
            EventDatePicker.SelectedDate = null;
            LocationComboBox.SelectedIndex = -1;
            LocationComboBox.ItemsSource = null;
            ServiceListBox.SelectedItems.Clear();
        }

        private void CreateEventButton_Click(object sender, RoutedEventArgs e)
        {
            if (EventNameTextBox.Text == "" || EventDatePicker.SelectedDate == null || LocationComboBox.SelectedValue == null)
            {
                MessageBox.Show("Пожалуйста, заполните все поля.");
                return;
            }

            string name = EventNameTextBox.Text;
            DateTime date = EventDatePicker.SelectedDate.Value;
            int locationId = (int)LocationComboBox.SelectedValue;

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    string insertEvent = "INSERT INTO Events (EventName, EventDate, LocationID) OUTPUT INSERTED.EventID VALUES (@Name, @Date, @LocationID)";
                    SqlCommand cmd = new SqlCommand(insertEvent, conn);
                    cmd.Parameters.AddWithValue("@Name", name);
                    cmd.Parameters.AddWithValue("@Date", date);
                    cmd.Parameters.AddWithValue("@LocationID", locationId);

                    int newEventId = (int)cmd.ExecuteScalar();

                    foreach (var selectedService in ServiceListBox.SelectedItems)
                    {
                        int serviceId = (int)((dynamic)selectedService).ServiceID;
                        string insertService = "INSERT INTO EventServices (EventID, ServiceID) VALUES (@EventID, @ServiceID)";
                        SqlCommand serviceCmd = new SqlCommand(insertService, conn);
                        serviceCmd.Parameters.AddWithValue("@EventID", newEventId);
                        serviceCmd.Parameters.AddWithValue("@ServiceID", serviceId);
                        serviceCmd.ExecuteNonQuery();
                    }

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
