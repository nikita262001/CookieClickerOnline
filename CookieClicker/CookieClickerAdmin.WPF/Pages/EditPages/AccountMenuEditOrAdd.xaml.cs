using CookieClicker.ClassLibrary.ApiRequest;
using CookieClicker.ClassLibrary.Models;
using CookieClickerAdmin.WPF.Pages.MenuPages;
using System;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace CookieClickerAdmin.WPF.Pages.EditPages
{
    /// <summary>
    /// Логика взаимодействия для AccountMenuEditOrAdd.xaml
    /// </summary>
    public partial class AccountMenuEditOrAdd : Page
    {
        Account account;
        Regex number = new Regex("[0-9]");
        Regex point = new Regex("[.]");
        public AccountMenuEditOrAdd(Account _account)
        {
            InitializeComponent();
            account = _account;
        }
        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            LoadingParametrsPage();
        }

        private void LoadingParametrsPage()
        {
            if (account.IdAccount == 0)
            {
                GetItem.TitleTextBlock.Text = "Меню добавление нового аккаунта";
                btnEditOrAdd.Content = "Добавить";
                btnEditOrAdd.Click += AddAccount_Click;
            }
            else
            {
                GetItem.TitleTextBlock.Text = "Меню редактирования аккаунта";
                btnEditOrAdd.Content = "Редактировать";
                btnEditOrAdd.Click += EditAccount_Click;
                txtLogin.Visibility = Visibility.Visible;
                txtBLogin.Visibility = Visibility.Collapsed;

                txtIdAccount.Text = $"{account.IdAccount}";
                txtLogin.Text = account.Login;
                txtPassword.Text = account.Password;
                txtCookies.Text = $"{account.Cookies}";
                txtClickGold.Text = $"{account.ClickGold}";
                txtChocolateTime.Text = $"{account.ChocolateTime}";
                txtDateRegistration.Text = $"{account.DateRegistration}";
                txtLastEntrance.Text = $"{account.LastEntrance}";
            }
        }

        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            var answer = MessageBox.Show($"Вы хотите закрыть {GetItem.TitleTextBlock.Text}?", "Уведомление", MessageBoxButton.YesNo, MessageBoxImage.Information);
            if (answer == MessageBoxResult.Yes)
            {
                OpenLastPage();
            }
        }

        private async void AddAccount_Click(object sender, RoutedEventArgs e)
        {
            if (txtBLogin.Text.Length != 0 && txtPassword.Text.Length != 0 &&
                txtCookies.Text.Length != 0 && txtClickGold.Text.Length != 0 && txtChocolateTime.Text.Length != 0)
            {
                CollectAccount();
                account.Login = txtBLogin.Text;
                var answer = await AccountMethod.CreateAccount(account);
                if (answer == null)
                    MessageBox.Show("Такой логин существует \nили сервер не доступен \nили данные были введены некорректно.", "Уведомление", MessageBoxButton.OK, MessageBoxImage.Information);
                else if (answer.Contains("Success"))
                {
                    MessageBox.Show("Данные были успешно сохранены.", "Уведомление", MessageBoxButton.OK, MessageBoxImage.Information);
                    OpenLastPage();
                }
            }
        }

        private async void EditAccount_Click(object sender, RoutedEventArgs e)
        {
            if (txtPassword.Text.Length != 0 &&
                txtCookies.Text.Length != 0 && txtClickGold.Text.Length != 0 && txtChocolateTime.Text.Length != 0)
            {
                CollectAccount();
                var answer = await AccountMethod.UpdateAccount(account);
                if (answer == null)
                    MessageBox.Show("Cервер не доступен \nили данные были введены некорректно.", "Уведомление", MessageBoxButton.OK, MessageBoxImage.Information);
                else if (answer.Contains("Success"))
                {
                    MessageBox.Show("Данные были успешно сохранены.", "Уведомление", MessageBoxButton.OK, MessageBoxImage.Information);
                    OpenLastPage();
                }
            }
        }

        private void CollectAccount()
        {
            account.Password = txtPassword.Text;
            account.Cookies = decimal.Parse(txtCookies.Text);
            account.ClickGold = decimal.Parse(txtClickGold.Text);
            account.ChocolateTime = decimal.Parse(txtChocolateTime.Text);
        }

        private void OpenLastPage() =>
            GetItem.MenuFrame.Content = new AccountMenu();

        private void Number_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            var txtB = sender as TextBox;
            if (txtB.Text.Length == 0 && number.IsMatch(e.Text))
                return;
            else if (number.IsMatch(e.Text))
                return;
            else if (txtB.Text.IndexOf('.') == -1 && point.IsMatch(e.Text) && txtB.Text.Length != 0)
                return;

            e.Handled = true;
        }
    }
}
