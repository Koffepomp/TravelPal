using System.Windows;
using System.Windows.Controls;
using TravelPal.Accounts;
using TravelPal.Travels;

namespace TravelPal
{
    /// <summary>
    /// Interaction logic for TravelsWindow.xaml
    /// </summary>
    public partial class TravelsWindow : Window
    {
        UserManager UserManager;
        IUser User;
        TravelManager TravelManager;

        public TravelsWindow(UserManager userManager, IUser user, TravelManager travelManager)
        {
            InitializeComponent();
            UserManager = userManager;
            User = user;
            TravelManager = travelManager;
            lblUsername.Content = user.Username.ToUpper();

            // IF ADMIN HERE

            UpdateTravelListView();
        }

        private void UpdateTravelListView()
        {
            lvTravels.Items.Clear();

            foreach (Travel travel in TravelManager.GetAllTravels())
            {
                ListViewItem newTravelItem = new();
                newTravelItem.Content = travel.GetInfo();
                newTravelItem.Tag = travel;
                lvTravels.Items.Add(newTravelItem);
            }
        }

        private void btnUserSettings_Click(object sender, RoutedEventArgs e)
        {
            // Close Travelswindow and open usersettings
            UserDetailsWindow userDetailsWindow = new();
            userDetailsWindow.Show();
            //Close();
        }

        private void btnSignOut_Click(object sender, RoutedEventArgs e)
        {
            // Close RegisterWindow and open MainWindow
            MainWindow mainWindow = new();
            mainWindow.Show();
            Close();
        }

        private void btnAddTravel_Click(object sender, RoutedEventArgs e)
        {
            // Close Travelswindow and open addtravelwindow
            AddTravelWindow addTravelWindow = new(UserManager, User, TravelManager);
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
            UpdateTravelListView();
        }
    }
}
