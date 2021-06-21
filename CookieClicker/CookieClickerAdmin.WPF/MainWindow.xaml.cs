using CookieClickerAdmin.WPF.Pages;
using System.Windows;

namespace CookieClickerAdmin.WPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            GetItem.MainFrame = frame;
            GetItem.TitleTextBlock = txtTitle;
            frame.Content = new Authorization();
        }
    }
}
