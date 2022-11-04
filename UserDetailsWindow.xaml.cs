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
        IUser SignedInUser;
        public UserDetailsWindow(UserManager userManager, IUser signedInUser)
        {
            UserManager = userManager;
            SignedInUser = signedInUser;
            InitializeComponent();

            // Sets username to current signedinuser username
            tbUsername.Text = signedInUser.Username;

            // Add country to current signedinuser country
            cbCountry.Items.Add(signedInUser.Location);

            // Sets the combobox to have selecteditem as signinuser country
            cbCountry.SelectedIndex = 0;
        }

        // Fills country combobox with the countries enum
        private void AddCountriesToComboBox()
        {
            foreach (Enum country in Enum.GetValues(typeof(Countries)))
            {
                cbCountry.Items.Add(country);
            }
        }

        // User cancelled making account changes. Closes UserDetailsWindow and opens Travelswindow again
        private void btnCancelSettings_Click(object sender, RoutedEventArgs e)
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

        // When user presses save, another way of exception handling goes through all input fields
        // Makes sure to set new username, location and password then closes the window and opens Travelswindow
        private void btnSaveSettings_Click(object sender, RoutedEventArgs e)
        {
            if (tbUsername.Text.Length < 3)
            {
                MessageBox.Show("Username too short. (3-16 characters)");
            }
            else if (pbNewPassword.Password != pbNewConfirmPassword.Password)
            {
                MessageBox.Show("Passwords mismatch! Please enter again.");
            }
            else if (pbNewPassword.Password.Length == 0)
            {
                MessageBox.Show("Please enter a new password.");
            }
            else if (pbNewPassword.Password.Length > 0 && pbNewPassword.Password.Length < 5)
            {
                MessageBox.Show("Password too short. (5-16 characters)");
            }
            else
            {
                if (btnChangeUsername.Visibility == Visibility.Hidden)
                {
                    UserManager.UpdateUsername(SignedInUser, tbUsername.Text);
                }
                SignedInUser.Location = (Countries)cbCountry.SelectedItem;
                SignedInUser.Password = pbNewConfirmPassword.Password;

                ((TravelsWindow)this.Owner).UpdateTravelWindow();
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
        }

        // If user pressed "CHANGE" next to username, it unlocks the username textbox and hides the "CHANGE" text
        private void btnChangeUsername_Click(object sender, RoutedEventArgs e)
        {
            tbUsername.IsEnabled = true;
            btnChangeUsername.Visibility = Visibility.Hidden;
        }

        // If user pressed "CHANGE" next to country, it unlocks the country combobox and populates it with the countries enum
        private void btnChangeCountry_Click(object sender, RoutedEventArgs e)
        {
            cbCountry.Items.Clear();
            AddCountriesToComboBox();
            cbCountry.IsEnabled = true;
            btnChangeCountry.Visibility = Visibility.Hidden;
        }
    }
}
