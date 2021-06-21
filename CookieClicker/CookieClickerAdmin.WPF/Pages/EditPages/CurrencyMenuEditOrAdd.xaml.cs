using CookieClicker.ClassLibrary.ApiRequest;
using CookieClicker.ClassLibrary.Models;
using CookieClickerAdmin.WPF.Pages.MenuPages;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace CookieClickerAdmin.WPF.Pages.EditPages
{
    /// <summary>
    /// Логика взаимодействия для CurrencyMenuEditOrAdd.xaml
    /// </summary>
    public partial class CurrencyMenuEditOrAdd : Page
    {
        Currency currency;
        Regex number = new Regex("[0-9]");
        Regex point = new Regex("[.]");
        public CurrencyMenuEditOrAdd(Currency _currency)
        {
            InitializeComponent();
            currency = _currency;
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            LoadingParametrsPage();
        }
        private void LoadingParametrsPage()
        {
            if (currency.IdCurrency == 0)
            {
                GetItem.TitleTextBlock.Text = "Меню добавление новой валюты";
                btnEditOrAdd.Content = "Добавить";
                btnEditOrAdd.Click += AddCurrency_Click;
            }
            else
            {
                GetItem.TitleTextBlock.Text = "Меню редактирования валюты";
                btnEditOrAdd.Content = "Редактировать";
                btnEditOrAdd.Click += EditCurrency_Click;

                txtIdCurrency.Text = $"{currency.IdCurrency}";
                txtName.Text = currency.Name;
                txtRublesToOneCurrency.Text = $"{currency.RublesToOneCurrency}";
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
        private async void AddCurrency_Click(object sender, RoutedEventArgs e)
        {
            if (txtName.Text.Length != 0 && txtRublesToOneCurrency.Text.Length != 0)
            {
                CollectCurrency();
                var answer = await CurrencyMethod.CreateCurrency(currency);
                if (answer == null)
                    MessageBox.Show("Cервер не доступен\nили такой Id существует.", "Уведомление", MessageBoxButton.OK, MessageBoxImage.Information);
                else if (answer.Contains("Success"))
                {
                    MessageBox.Show("Данные были успешно сохранены.", "Уведомление", MessageBoxButton.OK, MessageBoxImage.Information);
                    OpenLastPage();
                }
            }
        }

        private async void EditCurrency_Click(object sender, RoutedEventArgs e)
        {
            if (txtName.Text.Length != 0 && txtRublesToOneCurrency.Text.Length != 0)
            {
                CollectCurrency();
                var answer = await CurrencyMethod.UpdateCurrency(currency);
                if (answer == null)
                    MessageBox.Show("Cервер не доступен\nили такой Id существует.", "Уведомление", MessageBoxButton.OK, MessageBoxImage.Information);
                else if (answer.Contains("Success"))
                {
                    MessageBox.Show("Данные были успешно сохранены.", "Уведомление", MessageBoxButton.OK, MessageBoxImage.Information);
                    OpenLastPage();
                }
            }
        }

        private void CollectCurrency()
        {
            currency.Name = txtName.Text;
            currency.RublesToOneCurrency = decimal.Parse(txtRublesToOneCurrency.Text);
        }

        private void OpenLastPage() =>
            GetItem.MenuFrame.Content = new CurrencyMenu();

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

        private void OnlyNumber_PreviewTextInput(object sender, TextCompositionEventArgs e) =>
            e.Handled = !number.IsMatch(e.Text);
    }
}
