using CookieClickerAdmin.WPF.Pages.MenuPages;
using System.Windows;
using System.Windows.Controls;

namespace CookieClickerAdmin.WPF.Pages
{
    /// <summary>
    /// Логика взаимодействия для MainMenu.xaml
    /// </summary>
    public partial class MainMenu : Page
    {
        public MainMenu()
        {
            InitializeComponent();
            GetItem.TitleTextBlock.Text = "Главное меню";
            GetItem.MenuFrame = frameMenu;
        }

        private void AccountPage_Click(object sender, RoutedEventArgs e)
        {
            frameMenu.Content = new AccountMenu();
        }

        private void CurrencyPage_Click(object sender, RoutedEventArgs e)
        {
            frameMenu.Content = new CurrencyMenu();
        }

        private void DonateStatusPage_Click(object sender, RoutedEventArgs e)
        {
            frameMenu.Content = new DonateStatusMenu();
        }

        private void DonationPage_Click(object sender, RoutedEventArgs e)
        {
            frameMenu.Content = new DonationMenu();
        }

        private void EnhancementPage_Click(object sender, RoutedEventArgs e)
        {
            frameMenu.Content = new EnhancementMenu();
        }

        private void EnhancementAccountPage_Click(object sender, RoutedEventArgs e)
        {
            frameMenu.Content = new EnhancementAccountMenu();
        }
        private void ImageIBPage_Click(object sender, RoutedEventArgs e)
        {
            frameMenu.Content = new ImageIBMenu();
        }

        private void TypeEnhancementPage_Click(object sender, RoutedEventArgs e)
        {
            frameMenu.Content = new TypeEnhancementMenu();
        }

        private void FriendPage_Click(object sender, RoutedEventArgs e)
        {
            frameMenu.Content = new FriendMenu();
        }

        private void Exit_Click(object sender, RoutedEventArgs e)
        {
            var answer = MessageBox.Show("Вы уверены что хотите выйти","Уведомление", MessageBoxButton.YesNo,MessageBoxImage.Information);
            if (answer == MessageBoxResult.Yes)
            {
                GetItem.MainFrame.Content = new Authorization();
            }
        }
    }
}
