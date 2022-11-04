using System;
using System.Collections.Generic;
using System.Windows;
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

            // Gandalf default account
            User user = new("Gandalf", "password", Countries.Australia);
            userManager.AddUser(user);

            // Gandalf default trip
            List<IPackingListItem> tempList = new();
            OtherItem newItem = new("Lightning Staff", 1);
            tempList.Add(newItem);
            Trip trip = new(TripTypes.Leisure, "Mordor", Countries.Australia, 3, new DateTime(2022, 11, 8), new DateTime(2022, 11, 12), 5, tempList, user);
            user.Travels.Add(trip);
            travelManager.AddTravel(trip);

            // Gandalf default vacation
            List<IPackingListItem> tempList2 = new();
            OtherItem crowbar = new("Crowbar", 1);
            OtherItem lockpicks = new("Lockpicks", 1);
            tempList2.Add(crowbar);
            tempList2.Add(lockpicks);
            Vacation vacation = new(true, "ICA Mexi", Countries.Mexico, 3, new DateTime(2022, 11, 8), new DateTime(2022, 11, 15), 8, tempList2, user);
            user.Travels.Add(vacation);
            travelManager.AddTravel(vacation);
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
