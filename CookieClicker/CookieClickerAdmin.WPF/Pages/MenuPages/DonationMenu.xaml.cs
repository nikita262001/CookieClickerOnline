using CookieClicker.ClassLibrary.ApiRequest;
using CookieClicker.ClassLibrary.Models;
using CookieClickerAdmin.WPF.Pages.EditPages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace CookieClickerAdmin.WPF.Pages.MenuPages
{
    /// <summary>
    /// Логика взаимодействия для DonationMenu.xaml
    /// </summary>
    public partial class DonationMenu : Page
    {
        private decimal nowPage = 1;
        private decimal maxPage;
        private List<Donation> donations = new List<Donation>();
        public DonationMenu()
        {
            InitializeComponent();
            GetItem.TitleTextBlock.Text = "Меню донатов";
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            UpdatePage();
        }

        private async void UpdatePage()
        {
            listDonation.ItemsSource = null;
            while (listDonation.ItemsSource == null)
            {
                donations = await DonationMethod.GetDonations();
                listDonation.ItemsSource = donations;

                if (GetItem.MenuFrame.Content != this)
                    return;

                if (listDonation.ItemsSource == null)
                {
                    MessageBox.Show("В данный момент не возможно связаться с сервером", "Уведомление", MessageBoxButton.OK, MessageBoxImage.Information);
                    await Task.Run(() => Thread.Sleep(10000));
                }
                else
                {
                    if (donations.Count != 0)
                    {
                        stackPage.Visibility = Visibility.Visible;
                        maxPage = Math.Ceiling(donations.Count / 10m);
                        UpdateNumberPage();
                    }
                }
            }
        }

        private void UpdateNumberPage()
        {
            runNowPage.Text = $"{nowPage}";
            runMaxPage.Text = $"{maxPage}";
            listDonation.ItemsSource = donations.Skip(((int)nowPage - 1) * 10).Take(10);
        }

        private void LeftPage_Click(object sender, RoutedEventArgs e)
        {
            if (nowPage >= 2)
            {
                nowPage--;
                UpdateNumberPage();
            }
        }

        private void RightPage_Click(object sender, RoutedEventArgs e)
        {
            if (nowPage < maxPage)
            {
                nowPage++;
                UpdateNumberPage();
            }
        }

        private void listDonation_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            var donation = listDonation.SelectedItem as Donation;
            var answer = MessageBox.Show("Вы уверены что хотите редактировать выбранный донат.", "Уведомление", MessageBoxButton.YesNo, MessageBoxImage.Information);
            if (answer == MessageBoxResult.Yes)
            {
                GetItem.MenuFrame.Content = new DonationMenuEdit(donation);
            }
        }
    }
}
