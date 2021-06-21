using CookieClicker.ClassLibrary.ApiRequest;
using CookieClicker.ClassLibrary.Models;
using CookieClickerAdmin.WPF.Pages.MenuPages;
using Microsoft.Win32;
using System;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace CookieClickerAdmin.WPF.Pages.EditPages
{
    /// <summary>
    /// Логика взаимодействия для ImageIBMenuEditOrAdd.xaml
    /// </summary>
    public partial class ImageIBMenuEditOrAdd : Page
    {
        ImageIB imageIB;
        public ImageIBMenuEditOrAdd(ImageIB _imageIB)
        {
            imageIB = _imageIB;
            InitializeComponent();
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            LoadingParametrsPage();
        }
        private void LoadingParametrsPage()
        {
            if (imageIB.IdImageIB == 0)
            {
                GetItem.TitleTextBlock.Text = "Меню добавления новой картинки";
                btnEditOrAdd.Content = "Добавить";
                btnEditOrAdd.Click += AddImageIB_Click;
            }
            else
            {
                GetItem.TitleTextBlock.Text = "Меню редактирования картинки";
                btnEditOrAdd.Content = "Редактировать";
                btnEditOrAdd.Click += EditImageIB_Click;

                txtIdImageIB.Text = $"{imageIB.IdImageIB}";
                txtVersion.Text = $"{imageIB.Version}";
                image.Source = (BitmapSource)new ImageSourceConverter().ConvertFrom(imageIB.ImageInByte);
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

        private void SelectImage_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFile = new OpenFileDialog();
            if (openFile.ShowDialog().GetValueOrDefault())
            {
                image.Source = new BitmapImage(new Uri(openFile.FileName));
                imageIB.ImageInByte = File.ReadAllBytes(openFile.FileName);
            }
        }
        private async void AddImageIB_Click(object sender, RoutedEventArgs e)
        {
            if (imageIB.ImageInByte != null)
            {

                var answer = await ImageIBMethod.CreateImageIB(imageIB);
                if (answer == null)
                    MessageBox.Show("Cервер не доступен\nили такой Id существует.", "Уведомление", MessageBoxButton.OK, MessageBoxImage.Information);
                else if (answer.Contains("Success"))
                {
                    MessageBox.Show("Данные были успешно сохранены.", "Уведомление", MessageBoxButton.OK, MessageBoxImage.Information);
                    OpenLastPage();
                }
            }
        }

        private async void EditImageIB_Click(object sender, RoutedEventArgs e)
        {
            if (imageIB.ImageInByte != null)
            {

                var answer = await ImageIBMethod.UpdateImageIB(imageIB);
                if (answer == null)
                    MessageBox.Show("Cервер не доступен\nили такой Id существует.", "Уведомление", MessageBoxButton.OK, MessageBoxImage.Information);
                else if (answer.Contains("Success"))
                {
                    MessageBox.Show("Данные были успешно сохранены.", "Уведомление", MessageBoxButton.OK, MessageBoxImage.Information);
                    OpenLastPage();
                }
            }
        }

        private void OpenLastPage() =>
            GetItem.MenuFrame.Content = new ImageIBMenu();
    }
}
