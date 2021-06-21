using CookieClicker.Classes.ApiRequest;
using CookieClicker.Classes.Models;
using System;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace CookieClicker.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Registration : ContentPage
    {
        public Registration()
        {
            InitializeComponent();
        }

        protected override void OnAppearing()
        {
            imageBackGround.Source = Device.RuntimePlatform == Device.Android ? "BackgroundCookie.png" : "Images/BackgroundCookie.jpg";
            #region Расстановка элементов
            mainRelative.Children.Add(imageBackGround,
            Constraint.RelativeToParent((parent) => 0),
                Constraint.RelativeToParent((parent) => 0),
                Constraint.RelativeToParent((parent) => parent.Width),
                Constraint.RelativeToParent((parent) => parent.Height));

            mainRelative.Children.Add(relativeMenu,
                Constraint.RelativeToParent((parent) => parent.Width / 6),
                Constraint.RelativeToParent((parent) => parent.Height / 5),
                Constraint.RelativeToParent((parent) => parent.Width * 4 / 6),
                Constraint.RelativeToParent((parent) => parent.Height * 3 / 5));

            relativeMenu.Children.Add(labelTitle,
                Constraint.RelativeToParent((parent) => 0),
                Constraint.RelativeToParent((parent) => parent.Height * 0.05),
                Constraint.RelativeToParent((parent) => parent.Width),
                Constraint.RelativeToParent((parent) => parent.Height * 0.4));

            relativeMenu.Children.Add(stackLogin,
                Constraint.RelativeToParent((parent) => parent.Width * 0.1),
                Constraint.RelativeToView(labelTitle, (parent, view) => view.Y + view.Height + 5),
                Constraint.RelativeToParent((parent) => parent.Width * 0.95),
                Constraint.RelativeToParent((parent) => parent.Height * 0.125));

            relativeMenu.Children.Add(stackPas,
                Constraint.RelativeToParent((parent) => parent.Width * 0.1),
                Constraint.RelativeToView(stackLogin, (parent, view) => view.Y + view.Height + 5),
                Constraint.RelativeToParent((parent) => parent.Width * 0.95),
                Constraint.RelativeToParent((parent) => parent.Height * 0.125));

            relativeMenu.Children.Add(button,
                Constraint.RelativeToParent((parent) => parent.Width * 0.25),
                Constraint.RelativeToView(stackPas, (parent, view) => view.Y + view.Height + 5),
                Constraint.RelativeToParent((parent) => parent.Width * 0.65),
                Constraint.RelativeToParent((parent) => parent.Height * 0.125));
            #endregion

        }

        private async void CheckAndPushPage(object sende, EventArgs e)
        {
            if (loginE.Text.Length != 0 && pasE.Text.Length != 0)
            {
                var answer = await AccountMethod.CreateAccount(new Account { Login = loginE.Text, Password = pasE.Text });
                if (answer != null)
                {
                    if (answer.Contains("Success"))
                        await Navigation.PopAsync();
                }
                else
                    await DisplayAlert("Уведомление", "В данный момент сервер не доступен или\nтакой логин уже существует.", "Ок");
            }
            else
                await DisplayAlert("Уведомление","Вы не ввели все данные для регистрации.","Ок");
        }
    }
}