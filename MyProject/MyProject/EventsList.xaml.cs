using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Configuration;
using System.Windows;

namespace MyProject
{
    public partial class EventsList : Window
    {
        public EventsList()
        {
            InitializeComponent();
            LoadEvents();
        }

        private void LoadEvents()
        {
            List<EventModel> events = new List<EventModel>();

            string connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
            string query = @"
    SELECT e.EventID, e.EventName, e.EventDate, l.LocationName, l.LocationAddress
    FROM Events e
    LEFT JOIN Locations l ON e.LocationID = l.LocationID";


            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand(query, conn);
                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    events.Add(new EventModel
                    {
                        EventID = reader.GetInt32(0),
                        EventName = reader.GetString(1),
                        EventDate = reader.GetDateTime(2),
                        LocationName = reader.IsDBNull(3) ? "Нет данных" : reader.GetString(3),
                        LocationAddress = reader.IsDBNull(4) ? "Нет данных" : reader.GetString(4)
                    });
                }
            }

            EventsDataGrid.ItemsSource = events;
        }
    }
    public class EventModel
    {
        public int EventID { get; set; }
        public string EventName { get; set; }
        public DateTime EventDate { get; set; }
        public string LocationName { get; set; }
        public string LocationAddress { get; set; }  
    }

}
