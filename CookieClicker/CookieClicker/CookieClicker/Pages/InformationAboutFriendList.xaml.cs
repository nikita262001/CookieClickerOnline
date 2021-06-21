using CookieClicker.Classes;
using CookieClicker.Classes.ApiRequest;
using CookieClicker.Classes.Models;
using CookieClicker.ClassLibrary.ApiRequest;
using System;
using System.Linq;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace CookieClicker.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class InformationAboutFriendList : ContentPage
    {
        public InformationAboutFriendList()
        {
            InitializeComponent();
        }

        protected async override void OnAppearing()
        {
            btnAccounts.FontSize = Device.RuntimePlatform == Device.Android ? 10 : 25;
            btnFriendTirget.FontSize = Device.RuntimePlatform == Device.Android ? 10 : 25;
            btnInvitations.FontSize = Device.RuntimePlatform == Device.Android ? 10 : 25;
            list.ItemTemplate = new DataTemplate(() =>
            {
                Label labelLogin = new Label { };
                labelLogin.SetBinding(Label.TextProperty, new Binding { Path = "Login", StringFormat = "Логин: {0}" });

                Label labelCookies = new Label { };
                labelCookies.SetBinding(Label.TextProperty, new Binding { Path = "Cookies", StringFormat = "Количество печенек: {0:N2}" });

                Label labelDateRegistration = new Label { };
                labelDateRegistration.SetBinding(Label.TextProperty, new Binding { Path = "DateRegistration", StringFormat = "Дата регистрации: {0:dd/MM/yyyy}" });


                return new Frame
                {
                    Content = new Frame
                    {
                        Margin = 15,
                        Content = new StackLayout { Children = { labelLogin, labelCookies, labelDateRegistration } },
                        CornerRadius = 20,
                        BackgroundColor = Color.LightSalmon,
                    },
                    BackgroundColor = Color.Transparent,
                };
            });
            list.ItemsSource = (await AccountMethod.GetAllAccounts()).Where(obj => obj.IdAccount != GetItems.SelectedAccount.IdAccount).ToList();

            BackgroundColor = Color.LightSkyBlue;
        }

        private async void GetAccounts_Clicked(object sender, EventArgs e)
        {
            titletxt.Text = "Информация о пользователях";
            list.ItemsSource = null;
            list.ItemsSource = (await AccountMethod.GetAllAccounts()).Where(obj => obj.IdAccount != GetItems.SelectedAccount.IdAccount).ToList();
            list.SelectionChanged += GetAccounts_SelectionChanged;
            list.SelectionChanged -= GetFriendTarget_SelectionChanged;
            list.SelectionChanged -= GetInvitations_SelectionChanged;
        }

        private async void GetAccounts_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var answer = await DisplayAlert("Уведомление", "Вы уверены что хотите добавить в друзья выбранный аккаунт?", "Да", "Нет");
            if (answer)
            {
                var select = e.CurrentSelection.FirstOrDefault() as Account;
                var answerRequest = await FriendMethod.AddFriend(new Friend { Inviting = GetItems.SelectedAccount.IdAccount, Invited = select.IdAccount, BeFriends = false, FriendshipDate = DateTime.Now });
                if (answerRequest == null)
                    await DisplayAlert("Уведомление", "В данный момент не возможно добавить в друзья по не известным причинам.", "Ок");
                else
                {
                    await DisplayAlert("Уведомление", "Вы отправили приглашение в друзья.", "Ок");
                    await Navigation.PopAsync();
                }
            }
        }

        private async void GetFriendTarget_Clicked(object sender, EventArgs e)
        {
            titletxt.Text = "Информация о друзья";
            list.ItemsSource = null;
            list.ItemsSource = await FriendMethod.GetAllFriendTarget(GetItems.SelectedAccount.IdAccount);
            list.SelectionChanged -= GetAccounts_SelectionChanged;
            list.SelectionChanged += GetFriendTarget_SelectionChanged;
            list.SelectionChanged -= GetInvitations_SelectionChanged;
        }

        private async void GetFriendTarget_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var answer = await DisplayAlert("Уведомление", "Вы уверены что хотите удалить из друзей выбранный аккаунт?", "Да", "Нет");
            if (answer)
            {
                var select = e.CurrentSelection.FirstOrDefault() as Account;
                var selectedFriend = (await FriendMethod.GetFriends()).FirstOrDefault(
                    obj => (obj.Invited == select.IdAccount && obj.Inviting == GetItems.SelectedAccount.IdAccount) || (obj.Inviting == select.IdAccount && obj.Invited == GetItems.SelectedAccount.IdAccount));
                var answerRequest = await FriendMethod.DeleteFriend(selectedFriend.IdFriend);

                if (answerRequest == null)
                    await DisplayAlert("Уведомление", "В данный момент не возможно удалить из друзей по не известным причинам.", "Ок");
                else
                {
                    await DisplayAlert("Уведомление", "Вы удалили выбранного пользователя из друзей.", "Ок");
                    await Navigation.PopAsync();
                }
            }
        }

        private async void GetInvitations_Clicked(object sender, EventArgs e)
        {
            titletxt.Text = "Информация о приглашениях";
            list.ItemsSource = null;
            list.ItemsSource = await FriendMethod.GetAllInvitedFriendTarget(GetItems.SelectedAccount.IdAccount);
            list.SelectionChanged -= GetAccounts_SelectionChanged;
            list.SelectionChanged -= GetFriendTarget_SelectionChanged;
            list.SelectionChanged += GetInvitations_SelectionChanged;
        }

        private async void GetInvitations_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var answer = await DisplayAlert("Уведомление", "Вы уверены что хотите принять приглашение в друзья от выбранного аккаунта?", "Да", "Нет");
            if (answer)
            {
                var select = e.CurrentSelection.FirstOrDefault() as Account;
                var answerRequest = await FriendMethod.BeFriend((await FriendMethod.GetFriends()).FirstOrDefault(
                    obj => (obj.Invited == select.IdAccount && obj.Inviting == GetItems.SelectedAccount.IdAccount) || (obj.Inviting == select.IdAccount && obj.Invited == GetItems.SelectedAccount.IdAccount)).IdFriend);

                if (answerRequest == null)
                    await DisplayAlert("Уведомление", "В данный момент не возможно принять приглашение по не известным причинам.", "Ок");
                else
                {
                    await DisplayAlert("Уведомление", "Вы приняли приглашение выбранного пользователя.", "Ок");
                    await Navigation.PopAsync();
                }
            }
        }
    }
}