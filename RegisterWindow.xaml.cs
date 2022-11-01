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

            UserManager.CreateUser(tbRegisterUsername.Text, tbRegisterPassword.Text, cbRegisterCountry.SelectedItem.ToString());

            //MainWindow mainWindow = new();
            //mainWindow.Show();
            Close();
        }
    }
}
