using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Windows;
using System.Windows.Controls;

namespace MyProject
{
    public partial class Feedback : Window
    {
        private string connectionString;

        public Feedback()
        {
            InitializeComponent();
            connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
            LoadFeedback();
        }

        private void LoadFeedback()
        {
            var feedbackList = new List<FeedbackItem>();

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string query = @"
            SELECT F.Rating, F.Comments, C.ClientName
            FROM Feedback F
            INNER JOIN Clients C ON F.ClientID = C.ClientID";

                SqlCommand cmd = new SqlCommand(query, conn);
                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    feedbackList.Add(new FeedbackItem
                    {
                        ClientName = reader["ClientName"].ToString(),
                        Rating = Convert.ToInt32(reader["Rating"]),
                        Comments = reader["Comments"].ToString()
                    });
                }
            }

            FeedbackList.ItemsSource = feedbackList;
        }

        private void SubmitFeedback_Click(object sender, RoutedEventArgs e)
        {
            string clientName = ClientNameTextBox.Text.Trim();
            string comment = CommentTextBox.Text.Trim();

            if (string.IsNullOrWhiteSpace(clientName))
            {
                MessageBox.Show("Пожалуйста, введите имя клиента.");
                return;
            }

            if (RatingComboBox.SelectedItem is ComboBoxItem selectedItem &&
                int.TryParse(selectedItem.Content.ToString(), out int rating) &&
                !string.IsNullOrWhiteSpace(comment))
            {
                int clientId;

                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    SqlCommand getClientCmd = new SqlCommand("SELECT ClientID FROM Clients WHERE ClientName = @ClientName", conn);
                    getClientCmd.Parameters.AddWithValue("@ClientName", clientName);
                    var result = getClientCmd.ExecuteScalar();

                    if (result == null)
                    {
                        SqlCommand insertClientCmd = new SqlCommand("INSERT INTO Clients (ClientName) VALUES (@ClientName); SELECT SCOPE_IDENTITY();", conn);
                        insertClientCmd.Parameters.AddWithValue("@ClientName", clientName);
                        clientId = Convert.ToInt32(insertClientCmd.ExecuteScalar());
                    }
                    else
                    {
                        clientId = Convert.ToInt32(result);
                    }

                    SqlCommand insertFeedbackCmd = new SqlCommand("INSERT INTO Feedback (ClientID, Rating, Comments) VALUES (@ClientID, @Rating, @Comments)", conn);
                    insertFeedbackCmd.Parameters.AddWithValue("@ClientID", clientId);
                    insertFeedbackCmd.Parameters.AddWithValue("@Rating", rating);
                    insertFeedbackCmd.Parameters.AddWithValue("@Comments", comment);
                    insertFeedbackCmd.ExecuteNonQuery();
                }

                ClientNameTextBox.Clear();
                CommentTextBox.Clear();
                RatingComboBox.SelectedIndex = -1;
                LoadFeedback();
                MessageBox.Show("Спасибо за отзыв!");
            }
            else
            {
                MessageBox.Show("Пожалуйста, выберите оценку и введите комментарий.");
            }
        }

        private void Back_click(object sender, RoutedEventArgs e)
        {
            Customer_main Customer_mainWindow = new Customer_main();
            Customer_mainWindow.Show();
            Close();
        }
    }
    public class FeedbackItem
    {
        public string ClientName { get; set; }
        public int Rating { get; set; }
        public string Comments { get; set; }
    }
}
