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

        // Loads all the travel details to fill out the text fields
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
            lblStartDate.Content = Travel.StartDate;
            lblEndDate.Content = Travel.EndDate;

            foreach (TripTypes tripType in Enum.GetValues(typeof(TripTypes)))
            {
                cbTripType.Items.Add(tripType);
            }

            // If its a trip, sets it to trip in the combobox
            if (Travel.GetType().Name == "Trip")
            {
                cbTripVacation.SelectedIndex = 0;
                cbTripType.Visibility = Visibility.Visible;
                if (((Trip)Travel).Type.ToString() == "Leisure")
                {
                    cbTripType.SelectedIndex = 0;
                }
                else if (((Trip)Travel).Type.ToString() == "Work")
                {
                    cbTripType.SelectedIndex = 1;
                }
            }
            // If its a vacation, shows the allinclusive checkbox
            else if (Travel.GetType().Name == "Vacation")
            {
                chbxAllInclusive.IsChecked = ((Vacation)Travel).IsAllInclusive;
                cbTripVacation.SelectedIndex = 1;
                chbxAllInclusive.Visibility = Visibility.Visible;
                lblAllInclusive.Visibility = Visibility.Visible;
            }
        }

        // Enables save button, destination textbox, country combobox and travelers textbox when "EDIT" is pressed
        private void btnEdit_Click(object sender, RoutedEventArgs e)
        {
            btnEdit.IsEnabled = false;
            btnSaveTravel.IsEnabled = true;
            tbDestination.IsEnabled = true;
            cbCountry.IsEnabled = true;
            tbTravelers.IsEnabled = true;
            AddCountriesToComboBox();
            AddTripsToComboBox();
        }

        // Fills country combobox with the countries enum
        private void AddCountriesToComboBox()
        {
            foreach (Enum country in Enum.GetValues(typeof(Countries)))
            {
                cbCountry.Items.Add(country);
            }
        }

        // Adds triptype combobox with triptypes enum
        private void AddTripsToComboBox()
        {
            foreach (TripTypes tripType in Enum.GetValues(typeof(TripTypes)))
            {
                cbTripType.Items.Add(tripType);
                cbTripType.SelectedIndex = 0;
            }
        }

        // Overwrites the old elements with the new information before closing TravelDetailsWindow and opening TravelsWindow
        private void btnSaveTravel_Click(object sender, RoutedEventArgs e)
        {
            Travel.Destination = tbDestination.Text;
            Travel.Country = (Countries)cbCountry.SelectedItem;
            int travelers = Convert.ToInt32(tbTravelers.Text);
            Travel.Travellers = travelers;

            ((TravelsWindow)this.Owner).UpdateTravelWindow();
            foreach (Window window in Application.Current.Windows)
            {
                if (window.GetType().Name == "TravelsWindow")
                {
                    window.Show();
                }
            }
            Close();
        }

        // Discards any changes made, closes TravelDetailsWindow and opening TravelsWindow
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
