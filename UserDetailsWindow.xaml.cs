using System;
using System.Windows;
using TravelPal.Accounts;
using TravelPal.Enums;

namespace TravelPal
{
    /// <summary>
    /// Interaction logic for UserDetailsWindow.xaml
    /// </summary>
    public partial class UserDetailsWindow : Window
    {
        UserManager UserManager;
        IUser User;
        public UserDetailsWindow(UserManager userManager, IUser user)
        {
            UserManager = userManager;
            User = user;
            InitializeComponent();
            tbUsername.Text = user.Username;
            cbCountry.Items.Add(user.Country);
            cbCountry.SelectedIndex = 0;
        }
        private void AddCountriesToComboBox()
        {
            foreach (Enum country in Enum.GetValues(typeof(Countries)))
            {
                cbCountry.Items.Add(country);
            }
        }

        private void btnCancelSettings_Click(object sender, RoutedEventArgs e)
        {
            // User cancelled making account changes
            foreach (Window window in Application.Current.Windows)
            {
                if (window.GetType().Name == "TravelsWindow")
                {
                    window.Show();
                }
            }
            Close();
        }

        private void btnSaveSettings_Click(object sender, RoutedEventArgs e)
        {
            // Saves new changes to user account and return to Travels Window
            MessageBox.Show("New settings saved!");
            foreach (Window window in Application.Current.Windows)
            {
                if (window.GetType().Name == "TravelsWindow")
                {
                    window.Show();
                }
            }
            btnChangeUsername.Visibility = Visibility.Visible;
            btnChangeCountry.Visibility = Visibility.Visible;
            tbUsername.IsEnabled = false;
            cbCountry.IsEnabled = false;
            Close();
        }

        private void btnChangeUsername_Click(object sender, RoutedEventArgs e)
        {
            tbUsername.IsEnabled = true;
            btnChangeUsername.Visibility = Visibility.Hidden;
        }

        private void btnChangeCountry_Click(object sender, RoutedEventArgs e)
        {
            cbCountry.Items.Clear();
            AddCountriesToComboBox();
            cbCountry.IsEnabled = true;
            btnChangeCountry.Visibility = Visibility.Hidden;
        }
    }
}
