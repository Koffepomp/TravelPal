using System;
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
        IUser SignedInUser;
        TravelManager TravelManager;

        public TravelsWindow(UserManager userManager, IUser signedInUser, TravelManager travelManager)
        {
            InitializeComponent();
            UserManager = userManager;
            SignedInUser = signedInUser;
            TravelManager = travelManager;
            // Shows username as logged in
            lblUsername.Content = signedInUser.Username.ToUpper();

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
            UserDetailsWindow userDetailsWindow = new(UserManager, SignedInUser);
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
            AddTravelWindow addTravelWindow = new(UserManager, SignedInUser, TravelManager);
            addTravelWindow.Owner = this;
            addTravelWindow.Show();
            this.Hide();
        }

        private void btnDetails_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                TravelDetailsWindow travelDetailsWindow = new(UserManager, SignedInUser, TravelManager, GetSelectedItem());
                travelDetailsWindow.Owner = this;
                travelDetailsWindow.Show();
                this.Hide();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                UpdateTravelListView();
            }
        }

        private void btnRemoveTravel_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                TravelManager.RemoveTravel(GetSelectedItem());
                MessageBox.Show("Successfully removed travel!");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                UpdateTravelListView();
            }
        }

        private void btnHelp_Click(object sender, RoutedEventArgs e)
        {
            // Help user with some information
        }

        private Travel GetSelectedItem()
        {
            if (lvTravels.SelectedItem == null)
            {
                throw new Exception("Please select a travel.");
            }
            ListViewItem currentSelectedTravel = new();
            currentSelectedTravel = (ListViewItem)lvTravels.SelectedItem;
            Travel selectedTravel = (Travel)currentSelectedTravel.Tag;
            return selectedTravel;
        }
    }
}
