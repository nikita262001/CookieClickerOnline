using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace CookieClickerAdmin.WPF.Pages
{
    /// <summary>
    /// Логика взаимодействия для Authorization.xaml
    /// </summary>
    public partial class Authorization : Page
    {
        public Authorization()
        {
            InitializeComponent();
            GetItem.TitleTextBlock.Text = "Меню авторизации";
        }

        private void ButtonAuthorization_Click(object sender, RoutedEventArgs e)
        {
            if (txtLog.Text == "Login" && txtPas.Text == "Password")
            {
                GetItem.MainFrame.Content = new MainMenu();
            }
        }
    }
}
