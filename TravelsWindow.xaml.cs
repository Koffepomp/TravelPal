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

            // IF ADMIN HERE
            if (signedInUser.GetType().Name == "Admin")
            {
                btnAddTravel.Visibility = Visibility.Hidden;
            }
            UpdateTravelWindow();
        }

        public void UpdateTravelWindow()
        {
            lvTravels.Items.Clear();

            if (SignedInUser.GetType().Name == "Admin")
            {
                foreach (Travel travel in TravelManager.GetAllTravels())
                {
                    ListViewItem newTravelItem = new();
                    newTravelItem.Content = travel.GetInfo();
                    newTravelItem.Tag = travel;
                    lvTravels.Items.Add(newTravelItem);
                }
            }
            else
            {
                foreach (Travel travel in ((User)SignedInUser).GetAllTravels())
                {
                    ListViewItem newTravelItem = new();
                    newTravelItem.Content = travel.GetInfo();
                    newTravelItem.Tag = travel;
                    lvTravels.Items.Add(newTravelItem);
                }
            }
            // Shows username as logged in
            lblUsername.Content = SignedInUser.Username.ToUpper();

        }

        private void btnUserSettings_Click(object sender, RoutedEventArgs e)
        {
            // Close Travelswindow and open usersettings
            UserDetailsWindow userDetailsWindow = new(UserManager, SignedInUser);
            userDetailsWindow.Owner = this;
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
                UpdateTravelWindow();
            }
        }

        private void btnRemoveTravel_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                // remove travel
                Travel selectedTravel = GetSelectedItem();

                if (SignedInUser.GetType().Name == "Admin")
                {
                    Admin admin = (Admin)SignedInUser;
                    foreach(IUser user in UserManager.Users)
                    {
                        if (user.GetType().Name == "User")
                        {
                            User castUser = (User)user;
                            bool found = false;
                            foreach(Travel travel in castUser.Travels)
                            {
                                if (travel == selectedTravel)
                                    found = true;
                            }
                            if (found)
                            {
                                castUser.GetAllTravels().Remove(selectedTravel);
                                break;
                            }
                        }
                    }
                }
                else
                {
                    User user = (User)SignedInUser;
                    user.GetAllTravels().Remove(selectedTravel);
                }

                TravelManager.GetAllTravels().Remove(selectedTravel);


                // FLOPP FÖRSÖK
                //TravelManager.RemoveTravel(selectedTravel);
                //Travel selectedTravel = GetSelectedItem();
                //((TravelManager)SignedInUser).RemoveTravel(selectedTravel);



                // Gammal jävel
                //TravelManager.RemoveTravel(selectedTravel);

                MessageBox.Show("Successfully removed travel!");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                UpdateTravelWindow();
            }
        }

        private void btnHelp_Click(object sender, RoutedEventArgs e)
        {
            // Help user with some information
            MessageBox.Show("Going on a work trip? Taking a vacation?\r\n" +
                "Allow us to simplify your travels.\r\n" +
                "Enjoy endless possibilities while managing your travel on the go.\r\n" +
                "Never forget what to bring with our customizable packinglist system!\r\n\r\n" +
                "Thank you for picking TravelPal as your vacation companion.\r\n" +
                "We hope you enjoy your trip as much as we enjoy taking your money.\r\n\r\n" +
                "- Bernt Pompsson, Chief Executive Officer"
                );
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
