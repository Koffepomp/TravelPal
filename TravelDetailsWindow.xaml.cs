using System.Windows;
using TravelPal.Accounts;
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
            tbDestination.Text = Travel.Destination;
            cbCountry.Items.Add(Travel.Country);
            cbCountry.SelectedIndex = 0;
            tbTravelers.Text = Travel.Travellers.ToString();
            cbTripVacation.Items.Add("Trip");
            cbTripVacation.Items.Add("Vacation");
            cbTripType.Items.Add("Work");
            cbTripType.Items.Add("Leisure");
            if (Travel.TripOrVacation == "Trip")
            {
                cbTripVacation.SelectedIndex = 0;
                cbTripType.Visibility = Visibility.Visible;
                if (Travel.TripType == "Work")
                {
                    cbTripType.SelectedIndex = 0;
                }
                else if (Travel.TripType == "Leisure")
                {
                    cbTripType.SelectedIndex = 1;
                }
            }
            else if (Travel.TripOrVacation == "Vacation")
            {
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
            cbTripVacation.IsEnabled = true;
            chbxAllInclusive.IsEnabled = true;
            cbTripType.IsEnabled = true;
        }

        private void btnSaveTravel_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("New travel details saved!");
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
