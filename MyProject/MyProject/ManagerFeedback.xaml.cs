using System.Data.SqlClient;
using System.Windows;
using System.Data;
using System.Configuration;

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

        private void Back_click(object sender, RoutedEventArgs e)
        {
            Manager_main Manager_mainWindow = new Manager_main();
            Manager_mainWindow.Show();
            Close();
        }
    }
}

