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
    /// Логика взаимодействия для EnhancementMenu.xaml
    /// </summary>
    public partial class EnhancementMenu : Page
    {
        private decimal nowPage = 1;
        private decimal maxPage;
        private List<Enhancement> enhancements = new List<Enhancement>();
        public EnhancementMenu()
        {
            InitializeComponent();
            GetItem.TitleTextBlock.Text = "Меню улучшений";
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            UpdatePage();
        }

        private async void UpdatePage()
        {
            listEnhancement.ItemsSource = null;
            while (listEnhancement.ItemsSource == null)
            {
                enhancements = await EnhancementMethod.GetEnhancements();
                listEnhancement.ItemsSource = enhancements;

                if (GetItem.MenuFrame.Content != this)
                    return;

                if (listEnhancement.ItemsSource == null)
                {
                    MessageBox.Show("В данный момент не возможно связаться с сервером", "Уведомление", MessageBoxButton.OK, MessageBoxImage.Information);
                    await Task.Run(() => Thread.Sleep(10000));
                }
                else
                {
                    if (enhancements.Count != 0)
                    {
                        stackPage.Visibility = Visibility.Visible;
                        maxPage = Math.Ceiling(enhancements.Count / 10m);
                        UpdateNumberPage();
                    }
                }
            }
        }

        private void UpdateNumberPage()
        {
            runNowPage.Text = $"{nowPage}";
            runMaxPage.Text = $"{maxPage}";
            listEnhancement.ItemsSource = enhancements.Skip(((int)nowPage - 1) * 10).Take(10);
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

        private void listEnhancement_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            var enhancement = listEnhancement.SelectedItem as Enhancement;
            var answer = MessageBox.Show("Вы уверены что хотите редактировать выбранное улучшение.", "Уведомление", MessageBoxButton.YesNo, MessageBoxImage.Information);
            if (answer == MessageBoxResult.Yes)
            {
                GetItem.MenuFrame.Content = new EnhancementMenuEditOrAdd(enhancement);
            }
        }

        private void AddEnhancement_Click(object sender, RoutedEventArgs e)
        {
            var answer = MessageBox.Show("Вы уверены что хотите добавить новое улучшение.", "Уведомление", MessageBoxButton.YesNo, MessageBoxImage.Information);
            if (answer == MessageBoxResult.Yes)
            {
                GetItem.MenuFrame.Content = new EnhancementMenuEditOrAdd(new Enhancement());
            }
        }

        private async void RemoveEnhancement_Click(object sender, RoutedEventArgs e)
        {
            var enhancementDel = listEnhancement.SelectedItem as Enhancement;
            if (enhancementDel != null)
            {
                var answerDel = MessageBox.Show("Вы уверены что хотите удалить выбранное улучшение.", "Уведомление", MessageBoxButton.YesNo, MessageBoxImage.Information);
                if (answerDel == MessageBoxResult.Yes)
                {
                    var answer = await EnhancementMethod.DeleteEnhancement(enhancementDel.IdEnhancement);
                    if (answer.Contains("Success"))
                    {
                        MessageBox.Show("Выбранное улучшение было успешно удалено.", "Уведомление", MessageBoxButton.OK, MessageBoxImage.Information);
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
                MessageBox.Show("Вы не выбрали улучшение.", "Уведомление", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }
    }
}
