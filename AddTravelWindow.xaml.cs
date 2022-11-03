using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using TravelPal.Accounts;
using TravelPal.Enums;
using TravelPal.PackingList;
using TravelPal.Travels;

namespace TravelPal
{
    /// <summary>
    /// Interaction logic for AddTravelWindow.xaml
    /// </summary>
    public partial class AddTravelWindow : Window
    {
        UserManager UserManager;
        IUser SignedInUser;
        TravelManager TravelManager;

        int travelDays = 0;
        DateTime startDate;
        DateTime endDate;

        public AddTravelWindow(UserManager userManager, IUser signedInUser, TravelManager travelManager)
        {
            UserManager = userManager;
            SignedInUser = signedInUser;
            TravelManager = travelManager;

            InitializeComponent();
            AddCountriesToComboBox();
            AddTripsToComboBox();

            cldDates.SelectionMode = CalendarSelectionMode.MultipleRange;
            cldDates.SelectedDate = DateTime.Today;
            cldDates.DisplayDate = DateTime.Today;
        }

        private void AddCountriesToComboBox()
        {
            tbFrom.Text = SignedInUser.Location.ToString();
            foreach (Enum country in Enum.GetValues(typeof(Countries)))
            {
                cbCountry.Items.Add(country);
            }
        }

        private void AddTripsToComboBox()
        {
            cbTripVacation.Items.Add("Trip");
            cbTripVacation.Items.Add("Vacation");
            foreach (TripTypes tripType in Enum.GetValues(typeof(TripTypes)))
            {
                cbTripType.Items.Add(tripType);
                cbTripType.SelectedIndex = 0;
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

            try
            {
                string itemName = tbItemName.Text;
                if ((bool)chbxDocument.IsChecked)
                {
                    // det är ett dokument
                    bool isRequired = (bool)chbxRequired.IsChecked;
                    TravelDocument travelDocument = new(itemName, isRequired);
                    AddToListView(travelDocument);
                }
                else
                {
                    // lägg till item
                    int itemAmount = Convert.ToInt32(tbItemAmount.Text);
                    OtherItem otherItem = new(itemName, itemAmount);
                    AddToListView(otherItem);
                }
                ResetInventoryFields();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void AddToListView(IPackingListItem newItem)
        {
            ListViewItem newListViewItem = new();
            newListViewItem.Content = newItem.GetInfo();
            newListViewItem.Tag = newItem;

            lvInventory.Items.Add(newListViewItem);
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

        private bool IsCountryInEU(Countries countries)
        {
            foreach (EuropeanCountries country in Enum.GetValues(typeof(EuropeanCountries)))
            {
                if (country.ToString() == countries.ToString())
                {
                    return true;
                }
            }
            return false;
        }

        private void btnAddTravel_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                List<IPackingListItem> packList = CreateList();

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

                if (cbTripVacation.SelectedIndex == 0)
                {
                    Trip trip = new((TripTypes)cbTripType.SelectedItem, tbDestination.Text, (Countries)cbCountry.SelectedItem, travelers, cldDates.SelectedDates[0], cldDates.SelectedDates[cldDates.SelectedDates.Count() - 1], cldDates.SelectedDates.Count(), packList, SignedInUser);
                    ((User)SignedInUser).GetAllTravels().Add(trip);
                    TravelManager.AddTravel(trip);

                }
                else
                {
                    // vacation
                    Vacation vacation = new((bool)chbxAllInclusive.IsChecked, tbDestination.Text, (Countries)cbCountry.SelectedItem, travelers, cldDates.SelectedDates[0], cldDates.SelectedDates[cldDates.SelectedDates.Count() - 1], cldDates.SelectedDates.Count(), packList, SignedInUser);
                    ((User)SignedInUser).GetAllTravels().Add(vacation);
                    TravelManager.AddTravel(vacation);
                }

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
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private List<IPackingListItem> CreateList()
        {
            List<IPackingListItem> packList = new();
            foreach (ListViewItem item in lvInventory.Items)
            {
                packList.Add((IPackingListItem)item.Tag);
            }

            return packList;
        }

        private void cldDates_SelectedDatesChanged(object sender, SelectionChangedEventArgs e)
        {
            Mouse.Capture(null);
        }

        private void cbCountry_LostMouseCapture(object sender, MouseEventArgs e)
        {
            Countries userCountry = SignedInUser.Location;
            //Countries inputCountry = (Countries)cbCountry.SelectedItem;


        }
    }
}
