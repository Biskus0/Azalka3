using System.Windows;

namespace MyProject
{
    public partial class Customer_main : Window
    {
        public Customer_main()
        {
            InitializeComponent();
        }
        private void Logout_click(object sender, RoutedEventArgs e)
        {
            Login loginWindow = new Login();
            loginWindow.Show();
            Close();
        }

        private void EventsList_click(object sender, RoutedEventArgs e)
        {
            EventsList EventsListWindow = new EventsList();
            EventsListWindow.Show();
            Close();
        }
    }
}
