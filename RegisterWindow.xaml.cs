using System;
using System.Windows;

namespace TravelPal
{
    /// <summary>
    /// Interaction logic for RegisterWindow.xaml
    /// </summary>
    public partial class RegisterWindow : Window
    {
        public RegisterWindow()
        {
            InitializeComponent();
            AddCountriesToComboBox();
        }

        private void AddCountriesToComboBox()
        {
            foreach (Enum country in Enum.GetValues(typeof(EuropeanCountries)))
            {
                cbRegisterCountry.Items.Add(country);
            }
        }

        private void btnRegisterAccount_Click(object sender, RoutedEventArgs e)
        {
            // Close RegisterWindow and open MainWindow
            MessageBox.Show("Account succesfully created. Please login!");
            MainWindow mainWindow = new();
            mainWindow.Show();
            Close();
        }
    }
}
