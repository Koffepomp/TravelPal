using System.Windows;

namespace TravelPal
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void btnRegister_Click(object sender, RoutedEventArgs e)
        {
            // Close Main window and open register window
            MessageBox.Show("Opening register window...");
            RegisterWindow registerWindow = new();
            registerWindow.Show();
            Close();
        }

        private void btnLogin_Click(object sender, RoutedEventArgs e)
        {
            // Close Main window and open TravelsWindow
            MessageBox.Show("Logging in... Please wait...");
        }
    }
}
