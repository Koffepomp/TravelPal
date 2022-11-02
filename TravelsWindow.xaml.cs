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
            // Shows username as logged in
            lblUsername.Content = user.Username.ToUpper();

            // IF ADMIN HERE

            UpdateTravelListView();
        }

        public void UpdateTravelListView()
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
            UserDetailsWindow userDetailsWindow = new(UserManager, User);
            userDetailsWindow.Show();
            this.Hide();
        }

        private void btnSignOut_Click(object sender, RoutedEventArgs e)
        {
            // Close RegisterWindow and open MainWindow
            foreach (Window window in Application.Current.Windows)
            {
                if (window.GetType().Name == "MainWindow")
                {
                    window.Show();
                }
            }
            Close();
        }

        private void btnAddTravel_Click(object sender, RoutedEventArgs e)
        {
            // Close Travelswindow and open addtravelwindow
            AddTravelWindow addTravelWindow = new(UserManager, User, TravelManager);
            addTravelWindow.Owner = this;
            addTravelWindow.Show();
            this.Hide();
        }

        private void btnDetails_Click(object sender, RoutedEventArgs e)
        {
            // Close Travelswindow and open traveldetailswindow
            ListViewItem currentSelectedTravelDetails = new();
            currentSelectedTravelDetails = (ListViewItem)lvTravels.SelectedItem;
            Travel selectedTravel = (Travel)currentSelectedTravelDetails.Tag;

            // TravelDetailsWindow
        }

        private void btnRemoveTravel_Click(object sender, RoutedEventArgs e)
        {
            // Varför funkar inte detta????????????????????????
            try
            {
                ListViewItem currentSelectedTravelToRemove = new();
                currentSelectedTravelToRemove = (ListViewItem)lvTravels.SelectedItem;
                Travel selectedTravel = (Travel)currentSelectedTravelToRemove.Tag;
                TravelManager.RemoveTravel(selectedTravel);
                MessageBox.Show("Successfully removed travel!");

            }
            catch
            {
                MessageBox.Show("Please select a destination.");
            }
        }

        private void btnHelp_Click(object sender, RoutedEventArgs e)
        {
            // Help user with some information
        }
    }
}
