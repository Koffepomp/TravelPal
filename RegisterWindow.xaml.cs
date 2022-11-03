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

        private void AddCountriesToComboBox()
        {
            foreach (Enum country in Enum.GetValues(typeof(Countries)))
            {
                cbRegisterCountry.Items.Add(country);
            }
        }

        private void btnRegisterAccount_Click(object sender, RoutedEventArgs e)
        {
            User user = new(tbRegisterUsername.Text, pbRegisterPassword.Password, (Countries)cbRegisterCountry.SelectedItem);

            if (pbRegisterPassword.Password == pbRegisterConfirmPassword.Password)
            {
                if (UserManager.AddUser(user))
                {
                    //MessageBox.Show("Account creation complete!");
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
            }
            else
            {
                MessageBox.Show("Passwords mismatch! Please enter again.");
            }
        }
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

        private void cbRegisterCountry_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            btnRegisterAccount.IsEnabled = true;
        }
    }
}
