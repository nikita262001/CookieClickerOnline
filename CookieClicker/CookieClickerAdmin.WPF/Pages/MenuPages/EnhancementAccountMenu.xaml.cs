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
    /// Логика взаимодействия для EnhancementAccountMenu.xaml
    /// </summary>
    public partial class EnhancementAccountMenu : Page
    {
        private decimal nowPage = 1;
        private decimal maxPage;
        private List<EnhancementAccount> enhancementAccounts = new List<EnhancementAccount>();
        public EnhancementAccountMenu()
        {
            InitializeComponent();
            GetItem.TitleTextBlock.Text = "Меню улучшений аккаунтов";
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            UpdatePage();
        }

        private async void UpdatePage()
        {
            listEnhancementAccount.ItemsSource = null;
            while (listEnhancementAccount.ItemsSource == null)
            {
                enhancementAccounts = await EnhancementAccountMethod.GetEnhancementAccounts();
                listEnhancementAccount.ItemsSource = enhancementAccounts;

                if (GetItem.MenuFrame.Content != this)
                    return;

                if (listEnhancementAccount.ItemsSource == null)
                {
                    MessageBox.Show("В данный момент не возможно связаться с сервером", "Уведомление", MessageBoxButton.OK, MessageBoxImage.Information);
                    await Task.Run(() => Thread.Sleep(10000));
                }
                else
                {
                    if (enhancementAccounts.Count != 0)
                    {
                        stackPage.Visibility = Visibility.Visible;
                        maxPage = Math.Ceiling(enhancementAccounts.Count / 10m);
                        UpdateNumberPage();
                    }
                }
            }
        }

        private void UpdateNumberPage()
        {
            runNowPage.Text = $"{nowPage}";
            runMaxPage.Text = $"{maxPage}";
            listEnhancementAccount.ItemsSource = enhancementAccounts.Skip(((int)nowPage - 1) * 10).Take(10);
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

        private void AddEnhancementAccount_Click(object sender, RoutedEventArgs e)
        {
            var answer = MessageBox.Show("Вы уверены что хотите добавить новое улучшение аккаунта.", "Уведомление", MessageBoxButton.YesNo, MessageBoxImage.Information);
            if (answer == MessageBoxResult.Yes)
            {
                GetItem.MenuFrame.Content = new EnhancementAccountMenuEditOrAdd(new EnhancementAccount());
            }
        }

        private async void RemoveEnhancementAccount_Click(object sender, RoutedEventArgs e)
        {
            var enhancementAccountDel = listEnhancementAccount.SelectedItem as EnhancementAccount;
            if (enhancementAccountDel != null)
            {
                var answerDel = MessageBox.Show("Вы уверены что хотите удалить выбранное улучшение аккаунта.", "Уведомление", MessageBoxButton.YesNo, MessageBoxImage.Information);
                if (answerDel == MessageBoxResult.Yes)
                {
                    var answer = await EnhancementAccountMethod.DeleteEnhancementAccount(enhancementAccountDel.IdEnhancementAccount);
                    if (answer.Contains("Success"))
                    {
                        MessageBox.Show("Выбранное улучшение аккаунта было успешно удалено.", "Уведомление", MessageBoxButton.OK, MessageBoxImage.Information);
                        UpdatePage();
                    }
                    else
                    {
                        MessageBox.Show("Произошла ошибка при удалении.", "Уведомление", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }
            }
            else
            {
                MessageBox.Show("Вы не выбрали улучшение аккаунта.", "Уведомление", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }

        private void listEnhancementAccount_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            var enhancementAccount = listEnhancementAccount.SelectedItem as EnhancementAccount;
            var answer = MessageBox.Show("Вы уверены что хотите редактировать выбранный аккаунт.", "Уведомление", MessageBoxButton.YesNo, MessageBoxImage.Information);
            if (answer == MessageBoxResult.Yes)
            {
                GetItem.MenuFrame.Content = new EnhancementAccountMenuEditOrAdd(enhancementAccount);
            }
        }
    }
}
