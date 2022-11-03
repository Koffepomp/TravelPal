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
            tbUsername.Text = signedInUser.Username;
            cbCountry.Items.Add(signedInUser.Location);
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
            // KOLLA OM ANVÄNDARNAMN FINNS REDAN. Samma som från registerwindow

            if (tbUsername.Text.Length < 3)
            {
                MessageBox.Show("Username too short. (3-16 characters)");
            }
            else if (tbNewPassword.Text != tbNewConfirmPassword.Text)
            {
                MessageBox.Show("Passwords mismatch! Please enter again.");
            }
            else if (tbNewPassword.Text.Length < 5)
            {
                MessageBox.Show("Password too short. (5-16 characters)");
            }
            else
            {
                tbUsername.Text = SignedInUser.Username;
                cbCountry.SelectedItem = SignedInUser.Location;
                tbNewConfirmPassword.Text = SignedInUser.Password;

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
