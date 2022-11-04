using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Documents;
using TravelPal.Accounts;
using TravelPal.Enums;
using TravelPal.PackingList;
using TravelPal.Travels;

namespace TravelPal
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        UserManager userManager = new();
        TravelManager travelManager = new();
        public MainWindow()
        {
            InitializeComponent();
            LoadDefaultAccounts();
        }

        private void LoadDefaultAccounts()
        {
            // Admin default account
            Admin admin = new("admin", "password", Countries.Sweden);
            userManager.AddUser(admin);
            // Admin default trip


            // Gandalf default account
            User user = new("Gandalf", "password", Countries.Australia);
            userManager.AddUser(user);
            // Gandalf default trip


            List<IPackingListItem> tempList = new();
            OtherItem newItem = new("Gandalfs hatt", 1);
            tempList.Add(newItem);
            Trip trip = new(TripTypes.Leisure, "Ullared", Countries.Sweden, 3, new DateTime(2022, 11 ,1), new DateTime(2022, 11, 1), 1, tempList, user);
            user.Travels.Add(trip);
        }

        private void btnRegister_Click(object sender, RoutedEventArgs e)
        {
            RegisterWindow registerWindow = new(userManager);
            registerWindow.Show();
            this.Hide();
        }

        private void btnLogin_Click(object sender, RoutedEventArgs e)
        {
            string inputUsername = tbUsername.Text;
            string inputPassword = pbPassword.Password;
            if (userManager.SignInUser(inputUsername, inputPassword))
            {
                pbPassword.Clear();
                // Close Main window and open TravelsWindow
                TravelsWindow travelsWindow = new(userManager, userManager.SignedInUser, travelManager);
                travelsWindow.Show();
                this.Hide();
            }
            else
            {
                MessageBox.Show("Wrong username or password!");
                pbPassword.Clear();
            }
        }

        private void btnForgotPassword_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Unlucky.");
        }
    }
}
