using System;
using System.Data.SqlClient;
using System.Windows;

namespace MyProject
{
    public partial class AssignStaff : Window
    {
        private string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;

        public AssignStaff()
        {
            InitializeComponent();
            LoadEvents();
            LoadStaff();
        }

        private void LoadEvents()
        {
            // Загрузить все мероприятия в ComboBox
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand("SELECT EventID, EventName FROM Events", connection);
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    EventComboBox.Items.Add(reader["EventName"].ToString());
                }
                reader.Close();
            }
        }

        private void LoadStaff()
        {
            // Загрузить весь персонал в ComboBox
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand("SELECT StaffID, StaffName FROM Staff", connection);
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    StaffComboBox.Items.Add(reader["StaffName"].ToString());
                }
                reader.Close();
            }
        }

        private void AssignButton_Click(object sender, RoutedEventArgs e)
        {
            string selectedEvent = EventComboBox.SelectedItem as string;
            string selectedStaff = StaffComboBox.SelectedItem as string;

            if (selectedEvent != null && selectedStaff != null)
            {
                int eventId = GetEventIdByName(selectedEvent);
                int staffId = GetStaffIdByName(selectedStaff);

                // Проверка, назначен ли уже персонал на мероприятие
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    SqlCommand checkCommand = new SqlCommand("SELECT COUNT(*) FROM EventStaff WHERE EventID = @EventID AND StaffID = @StaffID", connection);
                    checkCommand.Parameters.AddWithValue("@EventID", eventId);
                    checkCommand.Parameters.AddWithValue("@StaffID", staffId);

                    int count = (int)checkCommand.ExecuteScalar();
                    if (count == 0)
                    {
                        // Если персонал не назначен, то добавить
                        SqlCommand assignCommand = new SqlCommand("INSERT INTO EventStaff (EventID, StaffID) VALUES (@EventID, @StaffID)", connection);
                        assignCommand.Parameters.AddWithValue("@EventID", eventId);
                        assignCommand.Parameters.AddWithValue("@StaffID", staffId);
                        assignCommand.ExecuteNonQuery();

                        MessageBox.Show("Персонал успешно назначен!");
                    }
                    else
                    {
                        MessageBox.Show("Этот персонал уже назначен на мероприятие.");
                    }
                }
            }
            else
            {
                MessageBox.Show("Пожалуйста, выберите мероприятие и персонал.");
            }
        }

        private int GetEventIdByName(string eventName)
        {
            int eventId = -1;
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand("SELECT EventID FROM Events WHERE EventName = @EventName", connection);
                command.Parameters.AddWithValue("@EventName", eventName);
                eventId = (int)command.ExecuteScalar();
            }
            return eventId;
        }

        private int GetStaffIdByName(string staffName)
        {
            int staffId = -1;
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand("SELECT StaffID FROM Staff WHERE StaffName = @StaffName", connection);
                command.Parameters.AddWithValue("@StaffName", staffName);
                staffId = (int)command.ExecuteScalar();
            }
            return staffId;
        }

        private void Back_click(object sender, RoutedEventArgs e)
        {
            Manager_main Manager_mainWindow = new Manager_main();
            Manager_mainWindow.Show();
            Close();
        }
    }
}
