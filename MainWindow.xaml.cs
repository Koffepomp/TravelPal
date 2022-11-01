using System.Collections.Generic;
using System.Windows;
using TravelPal.Accounts;

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
        }

        private void btnRegister_Click(object sender, RoutedEventArgs e)
        {
            // Close Main window and open register window
            //MessageBox.Show("Opening register window...");
            RegisterWindow registerWindow = new(userManager);
            registerWindow.Show();
            this.Hide();
        }

        private void btnLogin_Click(object sender, RoutedEventArgs e)
        {
            List<IUser> Accounts = userManager.FetchAccounts();
            string inputUsername = tbUsername.Text;
            string inputPassword = tbPassword.Text;
            bool loginAuthenticated = false;

            foreach (IUser user in Accounts)
            {
                if (user.Username == inputUsername && user.Password == inputPassword)
                {
                    loginAuthenticated = true;
                    // Close Main window and open TravelsWindow
                    TravelsWindow travelsWindow = new(userManager, user, travelManager);
                    travelsWindow.Show();
                    this.Hide();
                }
            }
            if (!loginAuthenticated)
            {
                MessageBox.Show("Wrong username or password!");
                tbPassword.Clear();
            }
        }
    }
}
