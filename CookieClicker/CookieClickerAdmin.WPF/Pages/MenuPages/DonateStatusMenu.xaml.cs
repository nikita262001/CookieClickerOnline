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
    /// Логика взаимодействия для DonateStatusMenu.xaml
    /// </summary>
    public partial class DonateStatusMenu : Page
    {
        private decimal nowPage = 1;
        private decimal maxPage;
        private List<DonateStatus> donateStatuses = new List<DonateStatus>();
        public DonateStatusMenu()
        {
            InitializeComponent();
            GetItem.TitleTextBlock.Text = "Меню статусов доната";
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            UpdatePage();
        }
        
        private async void UpdatePage()
        {
            listDonateStatus.ItemsSource = null; 
            while (listDonateStatus.ItemsSource == null)
            {
                donateStatuses = await DonateStatusMethod.GetDonateStatus();
                listDonateStatus.ItemsSource = donateStatuses;

                if (GetItem.MenuFrame.Content != this)
                    return;

                if (listDonateStatus.ItemsSource == null)
                {
                    MessageBox.Show("В данный момент не возможно связаться с сервером", "Уведомление", MessageBoxButton.OK, MessageBoxImage.Information);
                    await Task.Run(() => Thread.Sleep(10000));
                }
                else
                {
                    if (donateStatuses.Count != 0)
                    {
                        stackPage.Visibility = Visibility.Visible;
                        maxPage = Math.Ceiling(donateStatuses.Count / 10m);
                        UpdateNumberPage();
                    }
                }
            }
        }

        private void UpdateNumberPage()
        {
            runNowPage.Text = $"{nowPage}";
            runMaxPage.Text = $"{maxPage}";
            listDonateStatus.ItemsSource = donateStatuses.Skip(((int)nowPage - 1) * 10).Take(10);
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
    }
}
