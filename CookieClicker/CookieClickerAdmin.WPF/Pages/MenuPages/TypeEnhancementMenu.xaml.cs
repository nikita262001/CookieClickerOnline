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
    /// Логика взаимодействия для TypeEnhancementMenu.xaml
    /// </summary>
    public partial class TypeEnhancementMenu : Page
    {
        private decimal nowPage = 1;
        private decimal maxPage;
        private List<TypeEnhancement> TypeEnhancements = new List<TypeEnhancement>();
        public TypeEnhancementMenu()
        {
            InitializeComponent();
            GetItem.TitleTextBlock.Text = "Меню типов улучшений";
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            UpdatePage();
        }

        private async void UpdatePage()
        {
            listTypeEnhancement.ItemsSource = null;
            while (listTypeEnhancement.ItemsSource == null)
            {
                TypeEnhancements = await TypeEnhancementMethod.GetTypeEnhancements();
                listTypeEnhancement.ItemsSource = TypeEnhancements;

                if (GetItem.MenuFrame.Content != this)
                    return;

                if (listTypeEnhancement.ItemsSource == null)
                {
                    MessageBox.Show("В данный момент не возможно связаться с сервером", "Уведомление", MessageBoxButton.OK, MessageBoxImage.Information);
                    await Task.Run(() => Thread.Sleep(10000));
                }
                else
                {
                    if (TypeEnhancements.Count != 0)
                    {
                        stackPage.Visibility = Visibility.Visible;
                        maxPage = Math.Ceiling(TypeEnhancements.Count / 10m);
                        UpdateNumberPage();
                    }
                }
            }
        }

        private void UpdateNumberPage()
        {
            runNowPage.Text = $"{nowPage}";
            runMaxPage.Text = $"{maxPage}";
            listTypeEnhancement.ItemsSource = TypeEnhancements.Skip(((int)nowPage - 1) * 10).Take(10);
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

        private void listTypeEnhancement_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            var typeEnhancement = listTypeEnhancement.SelectedItem as TypeEnhancement;
            var answer = MessageBox.Show("Вы уверены что хотите редактировать выбранный тип.", "Уведомление", MessageBoxButton.YesNo, MessageBoxImage.Information);
            if (answer == MessageBoxResult.Yes)
            {
                GetItem.MenuFrame.Content = new TypeEnhancementMenuEdit(typeEnhancement);
            }
        }
    }
}
