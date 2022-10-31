using System.Windows;

namespace TravelPal
{
    /// <summary>
    /// Interaction logic for UserDetailsWindow.xaml
    /// </summary>
    public partial class UserDetailsWindow : Window
    {
        public UserDetailsWindow()
        {
            InitializeComponent();
        }

        private void btnCancelSettings_Click(object sender, RoutedEventArgs e)
        {
            // User cancelled making account changes
            MessageBox.Show("No changes made. Returning to Travels...");
            //TravelsWindow travelsWindow = new();
            //travelsWindow.Show();
            Close();
        }

        private void btnSaveSettings_Click(object sender, RoutedEventArgs e)
        {
            // Saves new changes to user account and return to Travels Window
            MessageBox.Show("New settings saved!");
            //TravelsWindow travelsWindow = new();
            //travelsWindow.Show();
            Close();
        }

        private void btnChangeUsername_Click(object sender, RoutedEventArgs e)
        {
            tbUsername.IsEnabled = true;
            btnChangeUsername.Visibility = Visibility.Hidden;
        }
    }
}
