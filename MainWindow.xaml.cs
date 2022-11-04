using System.Windows;
using TravelPal.Accounts;
using TravelPal.Enums;

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
            Admin admin = new("admin", "password", Countries.Sweden);
            userManager.AddUser(admin);

            User user = new("Gandalf", "password", Countries.Australia);
            userManager.AddUser(user);
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
