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
    /// Логика взаимодействия для CurrencyMenu.xaml
    /// </summary>
    public partial class CurrencyMenu : Page
    {
        private decimal nowPage = 1;
        private decimal maxPage;
        private List<Currency> currencies = new List<Currency>();
        public CurrencyMenu()
        {
            InitializeComponent();
            GetItem.TitleTextBlock.Text = "Меню валют";
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            UpdatePage();
        }
        private async void UpdatePage()
        {
            listCurrency.ItemsSource = null;
            while (listCurrency.ItemsSource == null)
            {
                currencies = await CurrencyMethod.GetCurrencies();
                listCurrency.ItemsSource = currencies;

                if (GetItem.MenuFrame.Content != this)
                    return;

                if (listCurrency.ItemsSource == null)
                {
                    MessageBox.Show("В данный момент не возможно связаться с сервером", "Уведомление", MessageBoxButton.OK, MessageBoxImage.Information);
                    await Task.Run(() => Thread.Sleep(10000));
                }
                else
                {
                    if (currencies.Count != 0)
                    {
                        stackPage.Visibility = Visibility.Visible;
                        maxPage = Math.Ceiling(currencies.Count / 10m);
                        UpdateNumberPage();
                    }
                }
            }
        }
        private void UpdateNumberPage()
        {
            runNowPage.Text = $"{nowPage}";
            runMaxPage.Text = $"{maxPage}";
            listCurrency.ItemsSource = currencies.Skip(((int)nowPage - 1) * 10).Take(10);
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

        private void listCurrency_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            var currency = listCurrency.SelectedItem as Currency;
            var answer = MessageBox.Show("Вы уверены что хотите редактировать выбранную валюту.", "Уведомление", MessageBoxButton.YesNo, MessageBoxImage.Information);
            if (answer == MessageBoxResult.Yes)
            {
                GetItem.MenuFrame.Content = new CurrencyMenuEditOrAdd(currency);
            }
        }

        private void AddCurrency_Click(object sender, RoutedEventArgs e)
        {
            var answer = MessageBox.Show("Вы уверены что хотите добавить новую валюту.", "Уведомление", MessageBoxButton.YesNo, MessageBoxImage.Information);
            if (answer == MessageBoxResult.Yes)
            {
                GetItem.MenuFrame.Content = new CurrencyMenuEditOrAdd(new Currency());
            }
        }

        private async void RemoveCurrency_Click(object sender, RoutedEventArgs e)
        {
            var currencyDel = listCurrency.SelectedItem as Currency;
            if (currencyDel != null)
            {
                var answerDel = MessageBox.Show("Вы уверены что хотите удалить выбранную валюту.", "Уведомление", MessageBoxButton.YesNo, MessageBoxImage.Information);
                if (answerDel == MessageBoxResult.Yes)
                {
                    var answer = await CurrencyMethod.DeleteCurrency(currencyDel.IdCurrency);
                    if (answer.Contains("Success"))
                    {
                        MessageBox.Show("Выбранная валюта была успешно удалена.", "Уведомление", MessageBoxButton.OK, MessageBoxImage.Information);
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
                MessageBox.Show("Вы не выбрали валюту.", "Уведомление", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }
    }
}
