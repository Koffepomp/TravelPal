using System;
using System.Linq;
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
            TravelManager = travelManager;
            InitializeComponent();
            cbTripVacation.Items.Add("Trip");
            cbTripVacation.Items.Add("Vacation");
            cbTripType.Items.Add("Work");
            cbTripType.Items.Add("Leisure");
            cbTripType.SelectedIndex = 0;
            AddCountriesToComboBox();
            tbFrom.Text = user.Country;
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
            try
            {
                if (cbTripVacation.SelectedIndex == 0 && cbTripType.SelectedItem == null)
                {
                    throw new Exception("Select a trip type.");
                }

                if (tbDestination.Text.Count() <= 0 ||
                    cbCountry.SelectedItem == null ||
                    tbTravelers.Text.Count() <= 0 ||
                    cbTripVacation.SelectedItem == null
                    )
                {
                    throw new Exception("Please fill out all fields.");
                }

                if (cldDates.SelectedDate == null)
                {
                    throw new Exception("Select travel dates in the calendar.");
                }

                int travelers = Convert.ToInt32(tbTravelers.Text);
                TravelManager.AddTravel(
                    tbDestination.Text,
                    (Countries)cbCountry.SelectedItem,
                    travelers,
                    cbTripVacation.SelectedItem.ToString(),
                    cbTripType.SelectedItem.ToString(),
                    (bool)chbxAllInclusive.IsChecked,
                    (DateTime)cldDates.SelectedDate
                    );

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
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
