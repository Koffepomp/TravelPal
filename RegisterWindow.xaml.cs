using System;
using System.Windows;
using TravelPal.Accounts;
using TravelPal.Enums;

namespace TravelPal
{
    /// <summary>
    /// Interaction logic for RegisterWindow.xaml
    /// </summary>
    public partial class RegisterWindow : Window
    {
        UserManager UserManager;
        public RegisterWindow(UserManager userManager)
        {
            InitializeComponent();
            AddCountriesToComboBox();
            UserManager = userManager;
        }

        // Fills country combobox with the countries enum
        private void AddCountriesToComboBox()
        {
            foreach (Enum country in Enum.GetValues(typeof(Countries)))
            {
                cbRegisterCountry.Items.Add(country);
            }
        }

        // Tries to create a new account if textboxes are filled out correctly
        // Wanted to tryout a different method instead of using try/catch but it's not as easily readible code
        private void btnRegisterAccount_Click(object sender, RoutedEventArgs e)
        {
            User user = new(tbRegisterUsername.Text, pbRegisterPassword.Password, (Countries)cbRegisterCountry.SelectedItem);

            // Makes sure that the password and confirmpassword are the same
            if (pbRegisterPassword.Password == pbRegisterConfirmPassword.Password)
            {
                // If username is 3 letters or more, it passes the if statement
                if (tbRegisterUsername.Text.Length >= 3)

                    // If password is 5 letters or more, it will continue down the if chain
                    if (pbRegisterPassword.Password.Length >= 5)

                        // Checks input username against existing users. If username is not taken returns true
                        if (UserManager.AddUser(user))
                        {
                            // All criterias met and account created. Closes the registerwindow and opens mainwindow again
                            foreach (Window window in Application.Current.Windows)
                            {
                                if (window.GetType().Name == "MainWindow")
                                {
                                    window.Show();
                                }
                            }
                            this.Close();
                        }
                        else
                        {
                            MessageBox.Show("Username already taken!");
                        }
                    else
                    {
                        MessageBox.Show("Password too short! (5-16 characters)");
                    }
                else
                {
                    MessageBox.Show("Username too short! (3-16 characters)");
                }
            }
            else
            {
                MessageBox.Show("Passwords mismatch! Please enter again.");
            }
        }

        // Updates everytime a letter is removed or added to the username textbox
        // If username is already taken, shows an error message in realtime
        private void tbRegisterUsername_TextChanged(object sender, System.Windows.Controls.TextChangedEventArgs e)
        {
            lblUsernameTaken.Visibility = Visibility.Hidden;
            foreach (IUser user in UserManager.Users)
            {
                string username = user.Username;
                if (username == tbRegisterUsername.Text)
                {
                    lblUsernameTaken.Visibility = Visibility.Visible;
                }
            }
        }

        // When country is selected register button becomes clickable
        private void cbRegisterCountry_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            btnRegisterAccount.IsEnabled = true;
        }
    }
}
