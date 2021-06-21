using CookieClicker.ClassLibrary.ApiRequest;
using CookieClicker.ClassLibrary.Models;
using CookieClickerAdmin.WPF.Pages.MenuPages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace CookieClickerAdmin.WPF.Pages.EditPages
{
    /// <summary>
    /// Логика взаимодействия для EnhancementAccountMenuEditOrAdd.xaml
    /// </summary>
    public partial class EnhancementAccountMenuEditOrAdd : Page
    {
        List<Account> accounts;
        List<Enhancement> enhancements;
        EnhancementAccount enhancementAccount;
        Regex number = new Regex("[0-9]");
        public EnhancementAccountMenuEditOrAdd(EnhancementAccount _enhancementAccount)
        {
            InitializeComponent();
            enhancementAccount = _enhancementAccount;
        }

        private async void Page_Loaded(object sender, RoutedEventArgs e)
        {
            accounts = await AccountMethod.GetAllAccounts();
            enhancements = await EnhancementMethod.GetEnhancements();

            comboAccount.SelectedItem = accounts;
            comboEnhancement.SelectedItem = enhancements;

            LoadingParametrsPage();
        }

        private void LoadingParametrsPage()
        {
            if (enhancementAccount.IdAccount == 0)
            {
                GetItem.TitleTextBlock.Text = "Меню добавление нового улучшения аккаунта";
                btnEditOrAdd.Content = "Добавить";
            }
            else
            {
                GetItem.TitleTextBlock.Text = "Меню редактирования улучшения аккаунта";
                btnEditOrAdd.Content = "Редактировать";

                txtIdEnhancementAccount.Text = $"{enhancementAccount.IdEnhancementAccount}";
                comboAccount.SelectedItem = accounts.FirstOrDefault(obj=> obj.IdAccount == enhancementAccount.IdAccount);
                comboEnhancement.SelectedItem = enhancements.FirstOrDefault(obj=> obj.IdEnhancement == enhancementAccount.IdEnhancement);
                txtQuantity.Text = $"{enhancementAccount.Quantity}";
            }
        }

        private async void AddOrEditEnhancementAccount_Click(object sender, RoutedEventArgs e)
        {
            if (comboAccount.SelectedItem != null && comboEnhancement.SelectedItem != null &&  txtQuantity.Text.Length != 0)
            {
                enhancementAccount.IdAccount = (comboAccount.SelectedItem as Account).IdAccount;
                enhancementAccount.IdEnhancement = (comboEnhancement.SelectedItem as Enhancement).IdEnhancement;
                enhancementAccount.Quantity = int.Parse(txtQuantity.Text);

                string answer = "";
                if (enhancementAccount.IdEnhancementAccount == 0)
                    answer = await EnhancementAccountMethod.CreateEnhancementAccount(enhancementAccount);
                else
                    answer = await EnhancementAccountMethod.UpdateEnhancementAccount(enhancementAccount);

                if (answer == null)
                    MessageBox.Show("Такой улучшение у аккаунта уже существует\nили сервер не доступен \nили данные были введены некорректно.", "Уведомление", MessageBoxButton.OK, MessageBoxImage.Information);
                else if (answer.Contains("Success"))
                {
                    MessageBox.Show("Данные были успешно сохранены.", "Уведомление", MessageBoxButton.OK, MessageBoxImage.Information);
                    OpenLastPage();
                }
            }
        }

        private void Number_PreviewTextInput(object sender, TextCompositionEventArgs e) =>
            e.Handled = !number.IsMatch(e.Text);

        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            var answer = MessageBox.Show($"Вы хотите закрыть {GetItem.TitleTextBlock.Text}?", "Уведомление", MessageBoxButton.YesNo, MessageBoxImage.Information);
            if (answer == MessageBoxResult.Yes)
                OpenLastPage();
        }

        private void OpenLastPage() =>
            GetItem.MenuFrame.Content = new EnhancementAccountMenu();
    }
}
