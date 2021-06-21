using CookieClicker.Classes.ApiRequest;
using CookieClicker.Classes.Models;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace CookieClicker
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MasterMenu : ContentPage
    {
        private int i = 0;
        public MasterMenu()
        {
            InitializeComponent();
        }
        protected override void OnAppearing()
        {
            if (Device.RuntimePlatform == Device.Android)
                list.RowHeight = 190;

            list.ItemTemplate = new DataTemplate(() =>
            {
                i++;
                Label labelTop = new Label { };
                labelTop.Text = $"Место номер {i}.";

                Label labelLogin = new Label { };
                labelLogin.SetBinding(Label.TextProperty, new Binding { Path = "Login", StringFormat = "Логин: {0}" });

                Label labelCookies = new Label { };
                labelCookies.SetBinding(Label.TextProperty, new Binding { Path = "Cookies", StringFormat = "Количество печенек: {0:N2}" });

                Label labelDateRegistration = new Label { };
                labelDateRegistration.SetBinding(Label.TextProperty, new Binding { Path = "DateRegistration", StringFormat = "Дата регистрации: {0:dd/MM/yyyy}" });


                return new ViewCell
                {
                    View = new Frame
                    {
                        Margin = 15,
                        Content = new StackLayout { Children = { labelTop, labelLogin, labelCookies, labelDateRegistration } },
                        CornerRadius = 20,
                        BackgroundColor = Color.LightSalmon,
                    }
                };
            });

            BackgroundColor = Color.LightSkyBlue;

            UpdateTopAccounts();
        }

        private async void UpdateTopAccounts()
        {
            while (true)
            {
                var accounts = await AccountMethod.GetTakeCountTop(15);
                MainThread.BeginInvokeOnMainThread(() =>
                {
                    i = 0;
                    list.ItemsSource = null;
                    list.ItemsSource = accounts;
                });
                await Task.Delay(15000);
            }
        }
    }
}