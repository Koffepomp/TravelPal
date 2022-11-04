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

            // Hides the add travel button if user is admin, since admins should never add travels
            if (signedInUser.GetType().Name == "Admin")
            {
                btnAddTravel.Visibility = Visibility.Hidden;
            }

            UpdateTravelWindow();
        }

        // This method is called on everytime the listview of travels needs to be updated
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
            // Shows username as logged in at the top, and updates it if necessary
            lblUsername.Content = SignedInUser.Username.ToUpper();

        }

        // Closes TravelsWindow and opens UserDetailWindow
        private void btnUserSettings_Click(object sender, RoutedEventArgs e)
        {
            UserDetailsWindow userDetailsWindow = new(UserManager, SignedInUser);
            userDetailsWindow.Owner = this;
            userDetailsWindow.Show();
            this.Hide();
        }

        // Closes RegisterWindow and opens MainWindow
        private void btnSignOut_Click(object sender, RoutedEventArgs e)
        {
            foreach (Window window in Application.Current.Windows)
            {
                if (window.GetType().Name == "MainWindow")
                {
                    window.Show();
                }
            }
            Close();
        }

        // Closes Travelswindow and opens AddTravelWindow
        private void btnAddTravel_Click(object sender, RoutedEventArgs e)
        {
            AddTravelWindow addTravelWindow = new(UserManager, SignedInUser, TravelManager);
            addTravelWindow.Owner = this;
            addTravelWindow.Show();
            this.Hide();
        }

        // Tries to open TravelDetailsWindow and throws an exception if a travel isnt selected in the listview
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

        // Tries to remove a travel selected in the listview
        private void btnRemoveTravel_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Travel selectedTravel = GetSelectedItem();

                // If its an admin removing the travel it needs to find the corresponding user travel as well
                if (SignedInUser.GetType().Name == "Admin")
                {
                    Admin admin = (Admin)SignedInUser;
                    foreach (IUser user in UserManager.Users)
                    {
                        if (user.GetType().Name == "User")
                        {
                            User castUser = (User)user;
                            bool found = false;
                            foreach (Travel travel in castUser.Travels)
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

        // Helps user with some information if clicked
        private void btnHelp_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Going on a work trip? Taking a vacation?\r\n" +
                "Allow us to simplify your travels.\r\n" +
                "Enjoy endless possibilities while managing your travel on the go.\r\n" +
                "Never forget what to bring with our customizable packinglist system!\r\n\r\n" +
                "Thank you for picking TravelPal as your vacation companion.\r\n" +
                "We hope you enjoy your trip as much as we enjoy taking your money.\r\n\r\n" +
                "- Bernt Pompsson, Chief Executive Officer"
                );
        }

        // Helps the details button and remove button to tag the right listviewitem to show or remove
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
