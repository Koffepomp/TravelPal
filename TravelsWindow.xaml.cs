using System.Windows;
using TravelPal.Accounts;

namespace TravelPal
{
    /// <summary>
    /// Interaction logic for TravelsWindow.xaml
    /// </summary>
    public partial class TravelsWindow : Window
    {
        UserManager UserManager;
        IUser User;
        public TravelsWindow(UserManager userManager, IUser user)
        {
            InitializeComponent();
            UserManager = userManager;
            User = user;
            lblUsername.Content = user.Username.ToUpper();

            // IF ADMIN HERE

            UpdateTravelListView();
        }

        private void UpdateTravelListView()
        {
            lvTravels.Items.Clear();
        }

        private void btnUserSettings_Click(object sender, RoutedEventArgs e)
        {
            // Close Travelswindow and open usersettings
            MessageBox.Show("Opening user settings!");
            UserDetailsWindow userDetailsWindow = new();
            userDetailsWindow.Show();
            //Close();
        }

        private void btnSignOut_Click(object sender, RoutedEventArgs e)
        {
            // Close RegisterWindow and open MainWindow
            MessageBox.Show("Logging out...");
            MainWindow mainWindow = new();
            mainWindow.Show();
            Close();
        }

        private void btnAddTravel_Click(object sender, RoutedEventArgs e)
        {
            // Close Travelswindow and open addtravelwindow
            MessageBox.Show("Opening add travel window!");
            AddTravelWindow addTravelWindow = new();
            addTravelWindow.Show();
        }

        private void btnDetails_Click(object sender, RoutedEventArgs e)
        {
            // Close Travelswindow and open traveldetailswindow
            MessageBox.Show("Opening detailed travel window!");
        }

        private void btnRemoveTravel_Click(object sender, RoutedEventArgs e)
        {
            // Remove selected item from the listview
            MessageBox.Show("Successfully removed travel!");
        }

        private void btnHelp_Click(object sender, RoutedEventArgs e)
        {
            // Help user with some information
            MessageBox.Show("Help was clicked!");
        }
    }
}
