using System.Windows;
using System.Windows.Controls;

namespace TravelPal
{
    /// <summary>
    /// Interaction logic for AddTravelWindow.xaml
    /// </summary>
    public partial class AddTravelWindow : Window
    {
        public AddTravelWindow()
        {
            InitializeComponent();
            cbTripVacation.Items.Add("Trip");
            cbTripVacation.Items.Add("Vacation");
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
    }
}
