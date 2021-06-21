using CookieClicker.ClassLibrary.ApiRequest;
using CookieClicker.ClassLibrary.Models;
using CookieClickerAdmin.WPF.Pages.MenuPages;
using System.Linq;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace CookieClickerAdmin.WPF.Pages.EditPages
{
    /// <summary>
    /// Логика взаимодействия для EnhancementMenuEditOrAdd.xaml
    /// </summary>
    public partial class EnhancementMenuEditOrAdd : Page
    {
        Enhancement enhancement;
        Regex number = new Regex("[0-9]");
        List<ImageIB> images;
        List<TypeEnhancement> typeEnhancements;
        public EnhancementMenuEditOrAdd(Enhancement _enhancement)
        {
            InitializeComponent();
            enhancement = _enhancement;
        }

        private async void Page_Loaded(object sender, RoutedEventArgs e)
        {
            images = await ImageIBMethod.GetImageIBs();
            typeEnhancements = await TypeEnhancementMethod.GetTypeEnhancements();

            comboTypeEnhancement.ItemsSource = typeEnhancements;
            comboxImage.ItemsSource = images;

            LoadingParametrsPage();
        }
        private void LoadingParametrsPage()
        {
            if (enhancement.IdEnhancement == 0)
            {
                GetItem.TitleTextBlock.Text = "Меню добавление нового улучшения";
                btnEditOrAdd.Content = "Добавить";
                btnEditOrAdd.Click += AddEnhancement_Click;
            }
            else
            {
                GetItem.TitleTextBlock.Text = "Меню редактирования улучшения";
                btnEditOrAdd.Content = "Редактировать";
                btnEditOrAdd.Click += EditEnhancement_Click;

                var typeEnhancementItem = typeEnhancements.FirstOrDefault(obj => obj.IdTypeEnhancement == enhancement.IdTypeEnhancement);
                var imageItem = images.FirstOrDefault(obj => obj.IdImageIB == enhancement.IdImageIB);

                txtIdEnhancement.Text = $"{enhancement.IdEnhancement}";
                txtName.Text = $"{enhancement.Name}";
                txtFirstCost.Text = $"{enhancement.FirstCost}";

                if (enhancement.CookiePerSecond != 0)
                {
                    stackCPS.Visibility = Visibility.Visible;
                    txtCookiePerSecond.Text = $"{enhancement.CookiePerSecond}";
                }
                else
                {
                    stackBonus.Visibility = Visibility.Visible;
                    txtBonusFormat.Text = $"{enhancement.BonusFormat}";
                }

                comboTypeEnhancement.SelectedItem = typeEnhancementItem;
                comboxImage.SelectedItem = imageItem;
                image.Source = (BitmapSource)new ImageSourceConverter().ConvertFrom(imageItem.ImageInByte);
                image.Visibility = Visibility.Visible;
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
        private async void AddEnhancement_Click(object sender, RoutedEventArgs e)
        {
            if (txtName.Text.Length != 0 && txtFirstCost.Text.Length != 0 && txtCookiePerSecond.Text.Length != 0 &&
                comboTypeEnhancement.ItemsSource != null && comboxImage.ItemsSource != null)
            {
                CollectEnhancement();
                var answer = await EnhancementMethod.CreateEnhancement(enhancement);
                if (answer == null)
                    MessageBox.Show("Cервер не доступен\nили данные ведены не корректно.", "Уведомление", MessageBoxButton.OK, MessageBoxImage.Information);
                else if (answer.Contains("Success"))
                {
                    MessageBox.Show("Данные были успешно сохранены.", "Уведомление", MessageBoxButton.OK, MessageBoxImage.Information);
                    OpenLastPage();
                }
            }
        }

        private async void EditEnhancement_Click(object sender, RoutedEventArgs e)
        {
            if (txtName.Text.Length != 0 && txtFirstCost.Text.Length != 0 && (txtCookiePerSecond.Text.Length != 0 || txtBonusFormat.Text.Length != 0) &&
                comboTypeEnhancement.ItemsSource != null && comboxImage.ItemsSource != null)
            {
                CollectEnhancement();
                var answer = await EnhancementMethod.UpdateEnhancement(enhancement);
                if (answer == null)
                    MessageBox.Show("Cервер не доступен\nили данные ведены не корректно.", "Уведомление", MessageBoxButton.OK, MessageBoxImage.Information);
                else if (answer.Contains("Success"))
                {
                    MessageBox.Show("Данные были успешно сохранены.", "Уведомление", MessageBoxButton.OK, MessageBoxImage.Information);
                    OpenLastPage();
                }
            }
        }

        private void CollectEnhancement()
        {
            enhancement.Name = txtName.Text;
            enhancement.FirstCost = int.Parse(txtFirstCost.Text);
            enhancement.IdTypeEnhancement = (comboTypeEnhancement.SelectedItem as TypeEnhancement).IdTypeEnhancement;
            enhancement.IdImageIB = (comboxImage.SelectedItem as ImageIB).IdImageIB;
            if (stackBonus.Visibility == Visibility.Visible)
            {
                enhancement.BonusFormat = txtBonusFormat.Text;
                enhancement.CookiePerSecond = 0;
            }
            else if(stackCPS.Visibility == Visibility.Visible)
            {
                enhancement.BonusFormat = $"+{enhancement.CookiePerSecond} Печенек/сек.";
                enhancement.CookiePerSecond = int.Parse(txtCookiePerSecond.Text);
            }
        }

        private void OpenLastPage() =>
            GetItem.MenuFrame.Content = new EnhancementMenu();

        private void Number_PreviewTextInput(object sender, TextCompositionEventArgs e) =>
            e.Handled = !number.IsMatch(e.Text);

        private void Image_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ImageIB selectedImage = comboxImage.SelectedItem as ImageIB;
            image.Source = (BitmapSource)new ImageSourceConverter().ConvertFrom(selectedImage.ImageInByte);
            image.Visibility = Visibility.Visible;
        }

        private void TypeEnhancement_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if ((comboTypeEnhancement.SelectedItem as TypeEnhancement).Name == "Печенек в секунду")
            {
                stackBonus.Visibility = Visibility.Collapsed;
                stackCPS.Visibility = Visibility.Visible;
            }
            else
            {
                stackBonus.Visibility = Visibility.Visible;
                stackCPS.Visibility = Visibility.Collapsed;
            }
        }
    }
}
