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
        SELECT 
            e.EventID, 
            e.EventName, 
            e.EventDate, 
            l.LocationName, 
            l.LocationAddress,
            STRING_AGG(st.StaffName, ', ') AS Staff,
            STRING_AGG(s.ServiceName, ', ') AS Services
        FROM Events e
        LEFT JOIN Locations l ON e.LocationID = l.LocationID
        LEFT JOIN EventStaff es ON e.EventID = es.EventID
        LEFT JOIN Staff st ON es.StaffID = st.StaffID
        LEFT JOIN EventServices es2 ON e.EventID = es2.EventID
        LEFT JOIN Services s ON es2.ServiceID = s.ServiceID
        GROUP BY e.EventID, e.EventName, e.EventDate, l.LocationName, l.LocationAddress
    ";

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
                        LocationAddress = reader.IsDBNull(4) ? "Нет данных" : reader.GetString(4),
                        Staff = reader.IsDBNull(5) ? "Нет персонала" : reader.GetString(5),
                        Services = reader.IsDBNull(6) ? "Нет услуг" : reader.GetString(6)
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
        public string Services { get; set; }
        public string Staff { get; set; }
    }

}
