using System;
using System.Windows;

namespace MyProject
{
    public partial class Manager_main : Window
    {
        public Manager_main()
        {
            InitializeComponent();
        }

        

        private void Logout_click(object sender, RoutedEventArgs e)
        {
            Login LoginWindow = new Login();
            LoginWindow.Show();
            Close();
        }

        private void Staff_сlick(object sender, RoutedEventArgs e)
        {
            Staff StaffWindow = new Staff();
            StaffWindow.Show();
            Close();
        }

        private void EventsList_сlick(object sender, RoutedEventArgs e)
        {
            EventsList EventsListWindow = new EventsList();
            EventsListWindow.Show();
            Close();
        }

        private void ManagerFeedback_click(object sender, RoutedEventArgs e)
        {
            ManagerFeedback ManagerFeedbackWindow = new ManagerFeedback();
            ManagerFeedbackWindow.Show();
            Close();
        }

        private void AssignStaff_click(object sender, RoutedEventArgs e)
        {
            AssignStaff AssignStaffWindow = new AssignStaff();
            AssignStaffWindow.Show();
            Close();
        }

        private void Services_click(object sender, RoutedEventArgs e)
        {
            ManagerServicesList ManagerServicesListWindow = new ManagerServicesList();
            ManagerServicesListWindow.Show();
            Close();
        }
    }
}
