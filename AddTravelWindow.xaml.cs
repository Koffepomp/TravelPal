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

        // Fills country combobox with the countries enum
        private void AddCountriesToComboBox()
        {
            tbFrom.Text = SignedInUser.Location.ToString();
            AddPassport();
            foreach (Enum country in Enum.GetValues(typeof(Countries)))
            {
                cbCountry.Items.Add(country);
            }
        }

        // Adds trip/vacation and triptypes enum to the comboboxes
        private void AddTripsToComboBox()
        {
            cbTripVacation.Items.Add("Trip");
            cbTripVacation.Items.Add("Vacation");
            foreach (TripTypes tripType in Enum.GetValues(typeof(TripTypes)))
            {
                cbTripType.Items.Add(tripType);
            }
            cbTripType.SelectedIndex = 0;
        }

        // Shows or hides the triptype or allinclusive elements
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

        // Toggles visibility of the required and amount elements
        private void chbxDocument_Click(object sender, RoutedEventArgs e)
        {
            ToggleAmountAndRequiredVisibility();
        }

        // Adds a packinglist item and throws an exception if failing to do so
        private void btnAddItem_Click(object sender, RoutedEventArgs e)
        {

            try
            {
                string itemName = tbItemName.Text;
                if ((bool)chbxDocument.IsChecked)
                {
                    bool isRequired = (bool)chbxRequired.IsChecked;
                    TravelDocument travelDocument = new(itemName, isRequired);
                    AddToListView(travelDocument);
                }
                else
                {
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

        // Makes the packinglist item into a listviewitem, tags it and adds it to the packinglist
        private void AddToListView(IPackingListItem newItem)
        {
            ListViewItem newListViewItem = new();
            newListViewItem.Content = newItem.GetInfo();
            newListViewItem.Tag = newItem;

            lvInventory.Items.Add(newListViewItem);
        }

        // Toggles visibility of the required and amount elements
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

        // Resets the add packinglist fields and textboxes after adding an item
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

        // Exits AddTravelWindow and sends user back to TravelsWindow
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

        // Compares the countries enums to check if its in EU
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

        // Adds a passport to the listview with true or false depending on if country is in EU or not
        private void AddPassport()
        {
            if (IsCountryInEU(SignedInUser.Location))
            {
                TravelDocument Passport = new("Passport", false);
                AddToListView(Passport);
            }
            else
            {
                TravelDocument Passport = new("Passport", true);
                AddToListView(Passport);
            }
        }

        // Adds a travel or trip to the travelmanager and user
        // Some exception handling if not filled out correctly
        // Ends with closing the window and opening TravelsWindow again
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

        // Used when adding a travel. Creates the packinglist, adds items and returns it
        private List<IPackingListItem> CreateList()
        {
            List<IPackingListItem> packList = new();
            foreach (ListViewItem item in lvInventory.Items)
            {
                packList.Add((IPackingListItem)item.Tag);
            }

            return packList;
        }

        // A small fix so you dont have to double click buttons after clicking in the calendar
        private void cldDates_SelectedDatesChanged(object sender, SelectionChangedEventArgs e)
        {
            Mouse.Capture(null);
        }

        // Sets user country to the signed in user when mouse leaves the country combobox
        private void cbCountry_LostMouseCapture(object sender, MouseEventArgs e)
        {
            Countries userCountry = SignedInUser.Location;
        }

        // When country combobox selection is changed, checks if passport is required in the new selection
        // Could be optimized but it works for now
        private void cbCountry_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (IsCountryInEU((Countries)cbCountry.SelectedItem))
            {
                if (IsCountryInEU(SignedInUser.Location))
                {
                    foreach (ListViewItem item in lvInventory.Items)
                    {
                        if ((TravelDocument)item.Tag is TravelDocument)
                        {
                            TravelDocument traveldocument = (TravelDocument)item.Tag;
                            if (traveldocument.Name == "Passport")
                            {
                                traveldocument.IsRequired = false;
                                item.Content = traveldocument.GetInfo();
                            }
                        }
                    }
                }
                else
                {
                    foreach (ListViewItem item in lvInventory.Items)
                    {
                        if ((TravelDocument)item.Tag is TravelDocument)
                        {
                            TravelDocument traveldocument = (TravelDocument)item.Tag;
                            if (traveldocument.Name == "Passport")
                            {
                                traveldocument.IsRequired = true;
                                item.Content = traveldocument.GetInfo();
                            }
                        }
                    }
                }
            }
            else
            {
                foreach (ListViewItem item in lvInventory.Items)
                {
                    if ((TravelDocument)item.Tag is TravelDocument)
                    {
                        TravelDocument traveldocument = (TravelDocument)item.Tag;
                        if (traveldocument.Name == "Passport")
                        {
                            traveldocument.IsRequired = true;
                            item.Content = traveldocument.GetInfo();
                        }
                    }
                }
            }
        }
    }
}
