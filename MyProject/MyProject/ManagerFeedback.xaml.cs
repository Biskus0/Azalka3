using System.Data.SqlClient;
using System.Windows;
using System.Data;
using System.Configuration;
using System;

namespace MyProject
{
    public partial class ManagerFeedback : Window
    {
        public ManagerFeedback()
        {
            InitializeComponent();
            LoadFeedbackData();
        }

        private void LoadFeedbackData()
        {
            string connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;

            string query = "SELECT f.FeedbackID AS 'ID Отзыва', " +
                           "c.ClientName AS 'Имя Клиента', f.Rating AS 'Рейтинг', " +
                           "f.Comments AS 'Комментарии' " +
                           "FROM Feedback f " +
                           "JOIN Clients c ON f.ClientID = c.ClientID";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlDataAdapter dataAdapter = new SqlDataAdapter(query, connection);
                DataTable dataTable = new DataTable();
                dataAdapter.Fill(dataTable);

                FeedbackDataGrid.ItemsSource = dataTable.DefaultView;
            }
        }

        private void DeleteFeedback_Click(object sender, RoutedEventArgs e)
        {
            if (int.TryParse(FeedbackIDTextBox.Text, out int feedbackID))
            {
                string connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
                string deleteQuery = "DELETE FROM Feedback WHERE FeedbackID = @FeedbackID";

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    SqlCommand cmd = new SqlCommand(deleteQuery, connection);
                    cmd.Parameters.AddWithValue("@FeedbackID", feedbackID);

                    try
                    {
                        connection.Open();
                        int rowsAffected = cmd.ExecuteNonQuery();
                        if (rowsAffected > 0)
                        {
                            MessageBox.Show("Отзыв успешно удален.");
                            LoadFeedbackData(); 
                        }
                        else
                        {
                            MessageBox.Show("Отзыв с таким ID не найден.");
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Ошибка при удалении отзыва: " + ex.Message);
                    }
                }
            }
            else
            {
                MessageBox.Show("Пожалуйста, введите правильный ID отзыва.");
            }
        }

        private void Back_click(object sender, RoutedEventArgs e)
        {
            Manager_main Manager_mainWindow = new Manager_main();
            Manager_mainWindow.Show();
            Close();
        }
    }
}
