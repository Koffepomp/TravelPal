using System;
using System.Windows;
using System.Windows.Controls;
using TravelPal.Accounts;
using TravelPal.Enums;

namespace TravelPal
{
    /// <summary>
    /// Interaction logic for AddTravelWindow.xaml
    /// </summary>
    public partial class AddTravelWindow : Window
    {
        UserManager UserManager;
        IUser User;
        TravelManager TravelManager;
        public AddTravelWindow(UserManager userManager, IUser user, TravelManager travelManager)
        {
            UserManager = userManager;
            User = user;
            InitializeComponent();
            cbTripVacation.Items.Add("Trip");
            cbTripVacation.Items.Add("Vacation");
            AddCountriesToComboBox();
            TravelManager = travelManager;
        }

        private void AddCountriesToComboBox()
        {
            foreach (Enum country in Enum.GetValues(typeof(Countries)))
            {
                cbCountry.Items.Add(country);
            }
        }

        private void cbTripVacation_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            // Trip type / all inclusive show and hide reaction
            if (cbTripVacation.SelectedItem == "Trip")
            {
                lblTripType.Visibility = Visibility.Visible;
                cbTripType.Visibility = Visibility.Visible;

                lblAllInclusive.Visibility = Visibility.Collapsed;
                chbxAllInclusive.Visibility = Visibility.Collapsed;
            }
            else if (cbTripVacation.SelectedItem == "Vacation")
            {
                lblAllInclusive.Visibility = Visibility.Visible;
                chbxAllInclusive.Visibility = Visibility.Visible;

                lblTripType.Visibility = Visibility.Collapsed;
                cbTripType.Visibility = Visibility.Collapsed;
            }
            else
            {
                lblAllInclusive.Visibility = Visibility.Collapsed;
                chbxAllInclusive.Visibility = Visibility.Collapsed;
                lblTripType.Visibility = Visibility.Collapsed;
                cbTripType.Visibility = Visibility.Collapsed;
            }
        }

        private void chbxDocument_Click(object sender, RoutedEventArgs e)
        {
            ToggleAmountAndRequiredVisibility();
        }

        private void btnAddItem_Click(object sender, RoutedEventArgs e)
        {
            ResetInventoryFields();
        }

        private void ToggleAmountAndRequiredVisibility()
        {
            if (lblAmount.Visibility == Visibility.Visible)
            {
                lblAmount.Visibility = Visibility.Collapsed;
                tbItemAmount.Visibility = Visibility.Collapsed;
                lblRequired.Visibility = Visibility.Visible;
                chbxRequired.Visibility = Visibility.Visible;
            }
            else
            {
                lblAmount.Visibility = Visibility.Visible;
                tbItemAmount.Visibility = Visibility.Visible;
                lblRequired.Visibility = Visibility.Collapsed;
                chbxRequired.Visibility = Visibility.Collapsed;
            }
        }

        private void ResetInventoryFields()
        {
            // Shows the amount label and textbox
            lblAmount.Visibility = Visibility.Visible;
            tbItemAmount.Visibility = Visibility.Visible;

            // Hides required label and checkbox
            lblRequired.Visibility = Visibility.Collapsed;
            chbxRequired.Visibility = Visibility.Collapsed;

            // Reset checkboxes
            chbxRequired.IsChecked = false;
            chbxDocument.IsChecked = false;

            // Clear item name and amount textboxes
            tbItemName.Clear();
            tbItemAmount.Clear();
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

        private void btnAddTravel_Click(object sender, RoutedEventArgs e)
        {
            TravelManager.AddTravel(tbDestination.Text, (Countries)cbCountry.SelectedItem, Convert.ToInt32(tbTravelers.Text));
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
    }
}
