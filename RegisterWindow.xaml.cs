using System;
using System.Windows;
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
            // Close RegisterWindow and open MainWindow
            UserManager.CreateUser(tbRegisterUsername.Text, tbRegisterPassword.Text, (Countries)cbRegisterCountry.SelectedItem);
            foreach (Window window in Application.Current.Windows)
            {
                if (window.GetType().Name == "MainWindow")
                {
                    window.Show();
                }
            }
            this.Close();
            Close();
        }
    }
}
