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
    /// Логика взаимодействия для TypeEnhancementMenuEdit.xaml
    /// </summary>
    public partial class TypeEnhancementMenuEdit : Page
    {
        TypeEnhancement typeEnhancement;
        Regex number = new Regex("[0-9]");
        Regex point = new Regex("[.]");
        public TypeEnhancementMenuEdit(TypeEnhancement _typeEnhancement)
        {
            InitializeComponent();
            typeEnhancement = _typeEnhancement;
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            LoadingParametrsPage();
        }

        private void LoadingParametrsPage()
        {
            GetItem.TitleTextBlock.Text = "Меню редактирования типа улучшения";

            txtName.Text = $"{typeEnhancement.Name}";
            txtСoefNextBuy.Text = $"{typeEnhancement.СoefNextBuy}";
        }
        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            var answer = MessageBox.Show($"Вы хотите закрыть {GetItem.TitleTextBlock.Text}?", "Уведомление", MessageBoxButton.YesNo, MessageBoxImage.Information);
            if (answer == MessageBoxResult.Yes)
            {
                OpenLastPage();
            }
        }
        private async void EditTypeEnhancement_Click(object sender, RoutedEventArgs e)
        {
            if (txtСoefNextBuy.Text.Length != 0 && txtName.Text.Length != 0)
            {
                CollectTypeEnhancement();
                var answer = await TypeEnhancementMethod.UpdateTypeEnhancement(typeEnhancement);
                if (answer == null)
                    MessageBox.Show("Cервер не доступен \nили данные были введены некорректно.", "Уведомление", MessageBoxButton.OK, MessageBoxImage.Information);
                else if (answer.Contains("Success"))
                {
                    MessageBox.Show("Данные были успешно сохранены.", "Уведомление", MessageBoxButton.OK, MessageBoxImage.Information);
                    OpenLastPage();
                }
            }
        }
        private void CollectTypeEnhancement()
        {
            typeEnhancement.Name = txtName.Text;
            typeEnhancement.СoefNextBuy = int.Parse(txtСoefNextBuy.Text);
        }
        private void OpenLastPage() =>
            GetItem.MenuFrame.Content = new TypeEnhancementMenu();

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
