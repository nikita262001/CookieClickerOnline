using CookieClicker.Classes;
using CookieClicker.Pages;
using System;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace CookieClicker
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Menu : ContentPage
    {
        bool answerGetDataFull = false;
        public Menu()
        {
            InitializeComponent();
        }

        protected override void OnAppearing()
        {
            imageBackGround.Source = Device.RuntimePlatform == Device.Android ? "BackgroundCookie.jpg" : "Images/BackgroundCookie.jpg";
            imageLogotype.Source = Device.RuntimePlatform == Device.Android ? "LogotypeCookie.png" : "Images/LogotypeCookie.png";
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

            relativeMenu.Children.Add(imageLogotype,
                Constraint.RelativeToParent((parent) => 0),
                Constraint.RelativeToParent((parent) => parent.Height * 0.05),
                Constraint.RelativeToParent((parent) => parent.Width),
                Constraint.RelativeToParent((parent) => parent.Height * 0.4));

            relativeMenu.Children.Add(stackLogin,
                Constraint.RelativeToParent((parent) => parent.Width * 0.1),
                Constraint.RelativeToView(imageLogotype, (parent, view) => view.Y + view.Height + 5),
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

            relativeMenu.Children.Add(buttonRegist,
                Constraint.RelativeToView(button, (parent, view) => view.X),
                Constraint.RelativeToView(button, (parent, view) => view.Y + view.Height + 5),
                Constraint.RelativeToView(button, (parent, view) => view.Width),
                Constraint.RelativeToView(button, (parent, view) => view.Height));
            #endregion

            GetDataFull();
        }

        private async void GetDataFull()
        {
            while (!answerGetDataFull)
                answerGetDataFull = await GetItems.GetFull();
        }

        private async void CheckAndPushPage(object sende, EventArgs e)
        {
            if (answerGetDataFull == true)
            {
                var answer = await GetItems.GetSelectedAccount(loginE.Text, pasE.Text);
                if (answer != null)
                    await Navigation.PushAsync(new ItemCookie());
                else
                    await DisplayAlert("Уведомление", "Аккаунта с таким логином и паролем не существует.", "Ок");
            }
            else
                await DisplayAlert("Уведомление", "В данный момент проблема с подключением к серверу,\nпожалуйста подождите.", "Ок");
        }
        private async void RegistrationPage(object sender, EventArgs e)
        {
            if (answerGetDataFull == true)
                await Navigation.PushAsync(new Registration());
            else
                await DisplayAlert("Уведомление", "В данный момент проблема с подключением к серверу,\nпожалуйста подождите.", "Ок");
        }
    }
}