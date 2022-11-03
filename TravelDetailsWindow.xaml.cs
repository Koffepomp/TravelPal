using System;
using System.Windows;
using TravelPal.Accounts;
using TravelPal.Enums;
using TravelPal.PackingList;
using TravelPal.Travels;

namespace TravelPal
{
    /// <summary>
    /// Interaction logic for TravelDetailsWindow.xaml
    /// </summary>
    public partial class TravelDetailsWindow : Window
    {
        UserManager UserManager;
        IUser User;
        TravelManager TravelManager;
        Travel Travel;
        public TravelDetailsWindow(UserManager userManager, IUser user, TravelManager travelManager, Travel selectedTravel)
        {
            UserManager = userManager;
            User = user;
            TravelManager = travelManager;
            Travel = selectedTravel;
            InitializeComponent();
            LoadTravelDetails();
        }

        private void LoadTravelDetails()
        {
            foreach (IPackingListItem item in Travel.PackingList)
            {
                lvInventory.Items.Add(item.GetInfo());
            }

            tbDestination.Text = Travel.Destination;
            cbCountry.Items.Add(Travel.Country);
            cbCountry.SelectedIndex = 0;
            tbTravelers.Text = Travel.Travellers.ToString();
            cbTripVacation.Items.Add("Trip");
            cbTripVacation.Items.Add("Vacation");
            if (Travel.GetType().Name == "Trip")
            {
                cbTripVacation.SelectedIndex = 0;
                cbTripType.Visibility = Visibility.Visible;
                if (((Trip)Travel).Type.ToString() == "Work")
                {
                    cbTripType.SelectedIndex = 0;
                }
                else if (((Trip)Travel).Type.ToString() == "Leisure")
                {
                    cbTripType.SelectedIndex = 1;
                }
            }
            else if (Travel.GetType().Name == "Vacation")
            {
                chbxAllInclusive.IsChecked = ((Vacation)Travel).IsAllInclusive;
                cbTripVacation.SelectedIndex = 1;
                chbxAllInclusive.Visibility = Visibility.Visible;
                lblAllInclusive.Visibility = Visibility.Visible;
            }
        }

        private void btnEdit_Click(object sender, RoutedEventArgs e)
        {
            // Enables and disables appropiate features when edit is clicked
            btnEdit.IsEnabled = false;
            btnSaveTravel.IsEnabled = true;
            tbDestination.IsEnabled = true;
            cbCountry.IsEnabled = true;
            tbTravelers.IsEnabled = true;
            //cbTripVacation.IsEnabled = true;
            //chbxAllInclusive.IsEnabled = true;
            //cbTripType.IsEnabled = true;
            AddCountriesToComboBox();
            AddTripsToComboBox();
        }

        private void AddCountriesToComboBox()
        {
            cbCountry.Items.Clear();
            foreach (Enum country in Enum.GetValues(typeof(Countries)))
            {
                cbCountry.Items.Add(country);
            }
        }

        private void AddTripsToComboBox()
        {
            foreach (TripTypes tripType in Enum.GetValues(typeof(TripTypes)))
            {
                cbTripType.Items.Add(tripType);
                cbTripType.SelectedIndex = 0;
            }
        }

        private void btnSaveTravel_Click(object sender, RoutedEventArgs e)
        {
            Travel.Destination = tbDestination.Text;
            Travel.Country = (Countries)cbCountry.SelectedItem;
            int travelers = Convert.ToInt32(tbTravelers.Text);
            Travel.Travellers = travelers;

            ((TravelsWindow)this.Owner).UpdateTravelListView();
            foreach (Window window in Application.Current.Windows)
            {
                if (window.GetType().Name == "TravelsWindow")
                {
                    window.Show();
                }
            }
            Close();
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            foreach (Window window in Application.Current.Windows)
            {
                if (window.GetType().Name == "TravelsWindow")
                {
                    window.Show();
                }
            }
            Close();
        }
    }
}
