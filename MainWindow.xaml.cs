using System.Windows;

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
            string inputUsername = tbUsername.Text;
            string inputPassword = tbPassword.Text;
            if (userManager.SignInUser(inputUsername, inputPassword))
            {
                tbPassword.Clear();
                // Close Main window and open TravelsWindow
                TravelsWindow travelsWindow = new(userManager, userManager.SignedInUser, travelManager);
                travelsWindow.Show();
                this.Hide();
            }
            else
            {
                MessageBox.Show("Wrong username or password!");
                tbPassword.Clear();
            }
        }

        private void btnForgotPassword_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Unlucky.");
        }
    }
}
