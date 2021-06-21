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
    /// Логика взаимодействия для ImageIBMenu.xaml
    /// </summary>

    public partial class ImageIBMenu : Page
    {
        private decimal nowPage = 1;
        private decimal maxPage;
        private List<ImageIB> imageIBs = new List<ImageIB>();
        public ImageIBMenu()
        {
            InitializeComponent();
            GetItem.TitleTextBlock.Text = "Меню картинок";
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            UpdatePage();
        }

        private async void UpdatePage()
        {
            listImageIB.ItemsSource = null;
            while (listImageIB.ItemsSource == null)
            {
                imageIBs = await ImageIBMethod.GetImageIBs();
                listImageIB.ItemsSource = imageIBs;

                if (GetItem.MenuFrame.Content != this)
                    return;

                if (listImageIB.ItemsSource == null)
                {
                    MessageBox.Show("В данный момент не возможно связаться с сервером", "Уведомление", MessageBoxButton.OK, MessageBoxImage.Information);
                    await Task.Run(() => Thread.Sleep(10000));
                }
                else
                {
                    if (imageIBs.Count != 0)
                    {
                        stackPage.Visibility = Visibility.Visible;
                        maxPage = Math.Ceiling(imageIBs.Count / 10m);
                        UpdateNumberPage();
                    }
                }
            }
        }

        private void UpdateNumberPage()
        {
            runNowPage.Text = $"{nowPage}";
            runMaxPage.Text = $"{maxPage}";
            listImageIB.ItemsSource = imageIBs.Skip(((int)nowPage - 1) * 10).Take(10);
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

        private void listImageIB_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            var ImageIB = listImageIB.SelectedItem as ImageIB;
            var answer = MessageBox.Show("Вы уверены что хотите редактировать выбранную картинку.", "Уведомление", MessageBoxButton.YesNo, MessageBoxImage.Information);
            if (answer == MessageBoxResult.Yes)
            {
                GetItem.MenuFrame.Content = new ImageIBMenuEditOrAdd(ImageIB);
            }
        }

        private void AddImageIB_Click(object sender, RoutedEventArgs e)
        {
            var answer = MessageBox.Show("Вы уверены что хотите добавить новую картинку.", "Уведомление", MessageBoxButton.YesNo, MessageBoxImage.Information);
            if (answer == MessageBoxResult.Yes)
            {
                GetItem.MenuFrame.Content = new ImageIBMenuEditOrAdd(new ImageIB());
            }
        }

        private async void RemoveImageIB_Click(object sender, RoutedEventArgs e)
        {
            var ImageIBDel = listImageIB.SelectedItem as ImageIB;
            if (ImageIBDel != null)
            {
                var answerDel = MessageBox.Show("Вы уверены что хотите удалить выбранную картинку.", "Уведомление", MessageBoxButton.YesNo, MessageBoxImage.Information);
                if (answerDel == MessageBoxResult.Yes)
                {
                    var answer = await ImageIBMethod.DeleteImageIB(ImageIBDel.IdImageIB);
                    if (answer.Contains("Success"))
                    {
                        MessageBox.Show("Выбранная картинка была успешно удалена.", "Уведомление", MessageBoxButton.OK, MessageBoxImage.Information);
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
                MessageBox.Show("Вы не выбрали картинку.", "Уведомление", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }
    }
}
