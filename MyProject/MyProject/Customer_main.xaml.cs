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

        private void ServicesList_click(object sender, RoutedEventArgs e)
        {
            ServicesList ServicesListWindow = new ServicesList();
            ServicesListWindow.Show();
            Close();
        }
        
        private void CreateEvent_click(object sender, RoutedEventArgs e)
        {
            CreateEvent CreateEventWindow = new CreateEvent();
            CreateEventWindow.Show();
            Close();
        }

        private void Feedback_click(object sender, RoutedEventArgs e)
        {
            Feedback FeedbackWindow = new Feedback();
            FeedbackWindow.Show();
            Close();
        }
    }
}
