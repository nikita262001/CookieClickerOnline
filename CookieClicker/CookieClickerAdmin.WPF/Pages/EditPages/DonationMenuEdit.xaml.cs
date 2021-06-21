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
    /// Логика взаимодействия для DonationMenuEditOrAdd.xaml
    /// </summary>
    public partial class DonationMenuEdit : Page
    {
        Donation donation;
        List<Currency> currencies;
        List<DonateStatus> donateStatuses;
        public DonationMenuEdit(Donation _donation)
        {
            InitializeComponent();
            donation = _donation;
        }

        private async void Page_Loaded(object sender, RoutedEventArgs e)
        {
            currencies = await CurrencyMethod.GetCurrencies();
            donateStatuses = await DonateStatusMethod.GetDonateStatus();

            comboCurrency.ItemsSource = currencies;
            comboStatusDonation.ItemsSource = donateStatuses;

            LoadingParametrsPage();
        }
        private void LoadingParametrsPage()
        {
            GetItem.TitleTextBlock.Text = "Меню редактирования доната";

            txtIdDonat.Text = $"{donation.IdDonat}";
            txtIdQiwi.Text = $"{donation.IdQiwi}";
            txtComment.Text = donation.Comment;
            txtDate.Text = $"{donation.Date}";
            txtPaymentAmount.Text = $"{donation.PaymentAmount}";
            comboCurrency.SelectedItem = currencies.FirstOrDefault(obj => obj.IdCurrency == donation.IdCurrency);
            comboStatusDonation.SelectedItem = donateStatuses.FirstOrDefault(obj => obj.IdDonateStatus == donation.IdDonateStatus);
        }
        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            var answer = MessageBox.Show($"Вы хотите закрыть {GetItem.TitleTextBlock.Text}?", "Уведомление", MessageBoxButton.YesNo, MessageBoxImage.Information);
            if (answer == MessageBoxResult.Yes)
            {
                OpenLastPage();
            }
        }
        private async void EditDonation_Click(object sender, RoutedEventArgs e)
        {
            if (txtComment.Text.Length != 0 && comboStatusDonation.SelectedItem != null)
            {
                donation.IdDonateStatus = (comboStatusDonation.SelectedItem as DonateStatus).IdDonateStatus;
                donation.Comment = txtComment.Text;
                var answer = await DonationMethod.UpdateDonation(donation);
                if (answer == null)
                    MessageBox.Show("Cервер не доступен \nили данные были введены некорректно.", "Уведомление", MessageBoxButton.OK, MessageBoxImage.Information);
                else if (answer.Contains("Success"))
                {
                    MessageBox.Show("Данные были успешно сохранены.", "Уведомление", MessageBoxButton.OK, MessageBoxImage.Information);
                    OpenLastPage();
                }
            }
        }

        private void OpenLastPage() =>
            GetItem.MenuFrame.Content = new DonationMenu();
    }
}
