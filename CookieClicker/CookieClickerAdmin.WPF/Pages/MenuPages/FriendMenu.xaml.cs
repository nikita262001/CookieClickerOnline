using CookieClicker.ClassLibrary.ApiRequest;
using CookieClicker.ClassLibrary.Models;
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
    /// Логика взаимодействия для FriendMenu.xaml
    /// </summary>
    public partial class FriendMenu : Page
    {
        private decimal nowPage = 1;
        private decimal maxPage;
        private List<Friend> friends = new List<Friend>();
        public FriendMenu()
        {
            InitializeComponent();
            GetItem.TitleTextBlock.Text = "Меню друзей";
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            UpdatePage();
        }

        private async void UpdatePage()
        {
            listFriends.ItemsSource = null;
            while (listFriends.ItemsSource == null)
            {
                friends = await FriendMethod.GetFriends();
                listFriends.ItemsSource = friends;

                if (GetItem.MenuFrame.Content != this)
                    return;

                if (listFriends.ItemsSource == null)
                {
                    MessageBox.Show("В данный момент не возможно связаться с сервером", "Уведомление", MessageBoxButton.OK, MessageBoxImage.Information);
                    await Task.Run(() => Thread.Sleep(10000));
                }
                else
                {
                    if (friends.Count != 0)
                    {
                        stackPage.Visibility = Visibility.Visible;
                        maxPage = Math.Ceiling(friends.Count / 10m);
                        UpdateNumberPage();
                    }
                }
            }
        }

        private void UpdateNumberPage()
        {
            runNowPage.Text = $"{nowPage}";
            runMaxPage.Text = $"{maxPage}";
            listFriends.ItemsSource = friends.Skip(((int)nowPage - 1) * 10).Take(10);
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

        private async void RemoveFriend_Click(object sender, RoutedEventArgs e)
        {
            var friendDel = listFriends.SelectedItem as Friend;
            if (friendDel != null)
            {
                var answerDel = MessageBox.Show("Вы уверены что хотите удалить выбранную дружбу.", "Уведомление", MessageBoxButton.YesNo, MessageBoxImage.Information);
                if (answerDel == MessageBoxResult.Yes)
                {
                    var answer = await FriendMethod.DeleteFriend(friendDel.IdFriend);
                    if (answer.Contains("Success"))
                    {
                        MessageBox.Show("Выбранная дружба была успешно удалена.", "Уведомление", MessageBoxButton.OK, MessageBoxImage.Information);
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
                MessageBox.Show("Вы не выбрали дружбу которую хотели удалить.", "Уведомление", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }
    }
}
