using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Windows;

namespace MyProject
{
    public partial class Staff : Window
    {
        public Staff()
        {
            InitializeComponent();
            LoadStaffData();
        }

        public class StaffMember
        {
            public string StaffName { get; set; }
            public string StaffRole { get; set; }
            public decimal StaffSalary { get; set; }
            public string StaffPhone { get; set; }
        }

        private void LoadStaffData()
        {
            List<StaffMember> staffList = new List<StaffMember>();
            string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                string query = "SELECT StaffName, StaffRole, StaffSalary, StaffPhone FROM Staff";

                SqlCommand command = new SqlCommand(query, connection);
                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    staffList.Add(new StaffMember
                    {
                        StaffName = reader["StaffName"].ToString(),
                        StaffRole = reader["StaffRole"].ToString(),
                        StaffSalary = Convert.ToDecimal(reader["StaffSalary"]),
                        StaffPhone = reader["StaffPhone"].ToString()
                    });
                }
            }

            StaffList.ItemsSource = staffList;
        }

        private void Back_click(object sender, RoutedEventArgs e)
        {
            Manager_main Manager_mainWindow = new Manager_main();
            Manager_mainWindow.Show();
            Close();
        }
    }
}
