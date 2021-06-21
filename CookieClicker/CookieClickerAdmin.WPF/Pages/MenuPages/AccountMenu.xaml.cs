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
    /// Логика взаимодействия для AccountMenu.xaml
    /// </summary>
    public partial class AccountMenu : Page
    {
        private decimal nowPage = 1;
        private decimal maxPage;
        private List<Account> accounts = new List<Account>();
        public AccountMenu()
        {
            InitializeComponent();
            GetItem.TitleTextBlock.Text = "Меню Аккаунтов";
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            UpdatePage();
        }

        private async void UpdatePage()
        {
            listAccount.ItemsSource = null;
            while (listAccount.ItemsSource == null)
            {
                if (GetItem.MenuFrame.Content != this)
                    return;

                accounts = await AccountMethod.GetAllAccounts();
                listAccount.ItemsSource = accounts;

                if (listAccount.ItemsSource == null)
                    MessageBox.Show("В данный момент не возможно связаться с сервером", "Уведомление", MessageBoxButton.OK, MessageBoxImage.Information);
                else
                {
                    if (accounts.Count != 0)
                    {
                        stackPage.Visibility = Visibility.Visible;
                        maxPage = Math.Ceiling(accounts.Count / 10m);
                        UpdateNumberPage();
                    }
                }
            }
        }

        private void UpdateNumberPage()
        {
            runNowPage.Text = $"{nowPage}";
            runMaxPage.Text = $"{maxPage}";
            listAccount.ItemsSource = accounts.Skip(((int)nowPage - 1) * 10).Take(10);
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

        private void listAccount_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            var account = listAccount.SelectedItem as Account;
            var answer = MessageBox.Show("Вы уверены что хотите редактировать выбранный аккаунт.", "Уведомление", MessageBoxButton.YesNo, MessageBoxImage.Information);
            if (answer == MessageBoxResult.Yes)
            {
                GetItem.MenuFrame.Content = new AccountMenuEditOrAdd(account);
            }
        }

        private void AddAccount_Click(object sender, RoutedEventArgs e)
        {
            var answer = MessageBox.Show("Вы уверены что хотите добавить новый аккаунт.", "Уведомление", MessageBoxButton.YesNo, MessageBoxImage.Information);
            if (answer == MessageBoxResult.Yes)
            {
                GetItem.MenuFrame.Content = new AccountMenuEditOrAdd(new Account());
            }
        }

        private async void RemoveAccount_Click(object sender, RoutedEventArgs e)
        {
            var accountDel = listAccount.SelectedItem as Account;
            if (accountDel != null)
            {
                var answerDel = MessageBox.Show("Вы уверены что хотите удалить выбранный аккаунт.", "Уведомление", MessageBoxButton.YesNo, MessageBoxImage.Information);
                if (answerDel == MessageBoxResult.Yes)
                {
                    var answer = await AccountMethod.DeleteAccount(accountDel.IdAccount);
                    if (answer.Contains("Success"))
                    {
                        MessageBox.Show("Выбранный аккаунт был успешно удален.", "Уведомление", MessageBoxButton.OK, MessageBoxImage.Information);
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
                MessageBox.Show("Вы не выбрали аккаунт.", "Уведомление", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }
    }
}
