using CookieClicker.Classes;
using CookieClicker.Classes.ApiRequest;
using CookieClicker.Classes.Models;
using CookieClicker.Pages;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace CookieClicker
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ItemCookie : ContentPage
    {
        Image rays;
        RelativeLayout mainR;
        List<ImageButton> imageButtons = new List<ImageButton>();
        Label labelCookies, labelCookiesSecond, labelSecondСhocolate;
        ProgressBar progressCookiesGold, progressСhocolate;
        Account selectedAccount = GetItems.SelectedAccount;
        bool boostChocolate = false, updateUserСontinue = true;
        decimal cookiesSecond = 0, clickPower = 1, cookiesForTime = 0;
        List<decimal> listCookiesForTime = new List<decimal>();
        int timerUpdateUser = 0;

        public ItemCookie()
        {
            BackgroundColor = Color.FromRgb(146, 227, 255);

            #region stackCookies
            labelCookies = new Label { FontSize = 20, HorizontalOptions = LayoutOptions.CenterAndExpand };
            labelCookiesSecond = new Label { FontSize = 20, HorizontalOptions = LayoutOptions.CenterAndExpand };
            StackLayout stackCookies = new StackLayout { Children = { labelCookies, labelCookiesSecond }, BackgroundColor = Color.FromHsla(0, 0, 0, 0.3) };
            #endregion

            #region relativeGoldBar
            progressCookiesGold = new ProgressBar { ProgressColor = Color.Gold };
            Image imageCookieGold = new Image { Source = (Device.RuntimePlatform == Device.Android ? "GoldCookie.png" : "Images/GoldCookie.png"), Aspect = Aspect.AspectFit };
            RelativeLayout relativeGoldBar = new RelativeLayout { BackgroundColor = Color.FromHsla(0, 0, 0, 0.3) };

            relativeGoldBar.Children.Add(imageCookieGold,
                Constraint.RelativeToParent((parent) => parent.Width * 0.96 - 5),
                Constraint.RelativeToParent((parent) => 0),
                Constraint.RelativeToParent((parent) => parent.Width * 0.04),
                Constraint.RelativeToParent((parent) => parent.Height));

            relativeGoldBar.Children.Add(progressCookiesGold,
                Constraint.RelativeToParent((parent) => 0),
                Constraint.RelativeToParent((parent) => parent.Height * 0.46),
                Constraint.RelativeToView(imageCookieGold, (parent, view) => view.X - 5),
                Constraint.RelativeToParent((parent) => parent.Height * 0.08));
            #endregion

            ImageButton cookie = new ImageButton { Source = (Device.RuntimePlatform == Device.Android ? "CookieItem.png" : "Images/CookieItem.png"), Aspect = Aspect.AspectFit, BackgroundColor = Color.Transparent, Style = null };
            cookie.Clicked += Cookie_Click;

            ImageButton shop = new ImageButton { Source = (Device.RuntimePlatform == Device.Android ? "Shop.png" : "Images/Shop.png"), Aspect = Aspect.AspectFit, BackgroundColor = Color.Transparent, Style = null, };
            shop.Clicked += ShopClick;

            ImageButton booster = new ImageButton { Source = (Device.RuntimePlatform == Device.Android ? "Boosters.png" : "Images/Boosters.png"), Aspect = Aspect.AspectFit, BackgroundColor = Color.Transparent };
            booster.Clicked += Booster_Click;

            ImageButton qiwiDonate = new ImageButton { Source = (Device.RuntimePlatform == Device.Android ? "QiwiDonate.png" : "Images/QiwiDonate.png"), Aspect = Aspect.AspectFit, BackgroundColor = Color.Transparent };
            qiwiDonate.Clicked += QiwiDonate_Click;

            ImageButton friend = new ImageButton { Source = (Device.RuntimePlatform == Device.Android ? "friend.png" : "Images/friend.png"), Aspect = Aspect.AspectFit, BackgroundColor = Color.Transparent };
            friend.Clicked += Friend_Click;

            rays = new Image { Source = (Device.RuntimePlatform == Device.Android ? "Rays.png" : "Images/Rays.png"), Aspect = Aspect.AspectFit, BackgroundColor = Color.Transparent };

            #region relativeСhocolateBar
            labelSecondСhocolate = new Label { FontSize = 15 };
            progressСhocolate = new ProgressBar { ProgressColor = Color.FromRgb(126, 65, 0) };
            Image imageСhocolate = new Image { Source = "Images/Chocolate.png", Aspect = Aspect.AspectFit, WidthRequest = 20 };
            RelativeLayout relativeСhocolateBar = new RelativeLayout { BackgroundColor = Color.FromHsla(0, 0, 0, 0.3) };

            relativeСhocolateBar.Children.Add(labelSecondСhocolate,
                Constraint.RelativeToParent((parent) => 5),
                Constraint.RelativeToParent((parent) => parent.Height * 0.2),
                Constraint.RelativeToParent((parent) => 30),
                Constraint.RelativeToParent((parent) => parent.Height));

            relativeСhocolateBar.Children.Add(imageСhocolate,
                Constraint.RelativeToParent((parent) => parent.Width * 0.96 - 5),
                Constraint.RelativeToParent((parent) => parent.Height * 0.1),
                Constraint.RelativeToParent((parent) => parent.Width * 0.04),
                Constraint.RelativeToParent((parent) => parent.Height * 0.8));

            relativeСhocolateBar.Children.Add(progressСhocolate,
                Constraint.RelativeToView(labelSecondСhocolate, (parent, view) => view.Width + view.X),
                Constraint.RelativeToParent((parent) => parent.Height * 0.46),
                Constraint.RelativeToView(imageСhocolate, (parent, view) => view.X - (labelSecondСhocolate.Width + labelSecondСhocolate.X)),
                Constraint.RelativeToParent((parent) => parent.Height * 0.08));
            #endregion

            #region mainR
            mainR = new RelativeLayout();

            mainR.Children.Add(rays,
                Constraint.RelativeToParent((parent) => parent.Width * -0.5),
                Constraint.RelativeToParent((parent) => 0),
                Constraint.RelativeToParent((parent) => parent.Width * 2),
                Constraint.RelativeToParent((parent) => parent.Height));

            mainR.Children.Add(stackCookies,
                Constraint.RelativeToParent((parent) => 0),
                Constraint.RelativeToParent((parent) => 0),
                Constraint.RelativeToParent((parent) => parent.Width),
                Constraint.RelativeToParent((parent) => 50));

            mainR.Children.Add(relativeGoldBar,
                Constraint.RelativeToParent((parent) => 0),
                Constraint.RelativeToView(stackCookies, (parent, view) => view.Y + view.Height),
                Constraint.RelativeToParent((parent) => parent.Width),
                Constraint.RelativeToParent((parent) => parent.Height * 0.05));

            mainR.Children.Add(cookie,
                Constraint.RelativeToParent((parent) => parent.Width / 7),
                Constraint.RelativeToView(relativeGoldBar, (parent, view) => parent.Height * 0.08 + view.Y + view.Height),
                Constraint.RelativeToParent((parent) => parent.Width * 5 / 7),
                Constraint.RelativeToParent((parent) => parent.Height * 0.5));

            mainR.Children.Add(shop,
                Constraint.RelativeToParent((parent) => 0),
                Constraint.RelativeToView(relativeСhocolateBar, (parent, view) => view.Y - parent.Height * 0.25),
                Constraint.RelativeToParent((parent) => parent.Width * 0.25),
                Constraint.RelativeToParent((parent) => parent.Height * 0.25));

            mainR.Children.Add(booster,
                Constraint.RelativeToView(shop, (parent, view) => view.Width + view.X),
                Constraint.RelativeToView(shop, (parent, view) => view.Y),
                Constraint.RelativeToParent((parent) => parent.Width * 0.25),
                Constraint.RelativeToParent((parent) => parent.Height * 0.25));

            mainR.Children.Add(qiwiDonate,
                Constraint.RelativeToView(booster, (parent, view) => view.Width + view.X),
                Constraint.RelativeToView(booster, (parent, view) => view.Y),
                Constraint.RelativeToParent((parent) => parent.Width * 0.25),
                Constraint.RelativeToParent((parent) => parent.Height * 0.25));

            mainR.Children.Add(friend,
                Constraint.RelativeToView(qiwiDonate, (parent, view) => view.Width + view.X),
                Constraint.RelativeToView(qiwiDonate, (parent, view) => view.Y),
                Constraint.RelativeToParent((parent) => parent.Width * 0.25),
                Constraint.RelativeToParent((parent) => parent.Height * 0.25));

            mainR.Children.Add(relativeСhocolateBar,
                Constraint.RelativeToParent((parent) => 0),
                Constraint.RelativeToParent((parent) => parent.Height * 0.95),
                Constraint.RelativeToParent((parent) => parent.Width),
                Constraint.RelativeToParent((parent) => parent.Height * 0.05));
            #endregion

            #region CookieGold
            Random random = new Random();
            for (int i = 0; i < 10; i++)
            {
                ImageButton cookieGold = new ImageButton { Source = "Images/GoldCookie.png", Aspect = Aspect.AspectFit, BackgroundColor = Color.Transparent, Padding = 15 };
                cookieGold.Clicked += CookieGold_Clicked;
                var rnd = random.Next(20, 80);

                mainR.Children.Add(cookieGold,
                    Constraint.RelativeToParent((parent) => parent.Width * rnd / 100),
                    Constraint.RelativeToParent((parent) => parent.Height * -0.5),
                    Constraint.RelativeToParent((parent) => (Device.RuntimePlatform == Device.Android ? 100 : parent.Width * 0.15)),
                    Constraint.RelativeToParent((parent) => (Device.RuntimePlatform == Device.Android ? 100 : parent.Height * 0.15)));
                imageButtons.Add(cookieGold);
            }
            #endregion


            Content = mainR;
            Device.StartTimer(new TimeSpan(0, 0, 1), UpdateUser);
        }
        private async void СhangeOfLocationInHeight()
        {
            foreach (var item in imageButtons)
            {
                await Task.Delay(Device.RuntimePlatform == Device.Android ? 1500 : 400);
                Task.Run(async () =>
                {
                    for (int i = 0; i < 200; i++)
                    {
                        await Task.Delay(/*Device.RuntimePlatform == Device.Android ? 60 : 20*/10);
                        Device.BeginInvokeOnMainThread(() => { item.TranslationY += mainR.Height * /*(Device.RuntimePlatform == Device.Android ? 0.05 : 0.02)*/0.01; });
                    }
                });
            }
        }

        protected override void OnAppearing()
        {
            cookiesSecond = 0;
            decimal offlinePercent = 0;

            foreach (var enhancementAccount in selectedAccount.EnhancementAccounts())
            {
                var enhancement = enhancementAccount.Enhancement();
                cookiesSecond += enhancement.CookiePerSecond * enhancementAccount.Quantity; // Подсчитывает количество получаемых печенек в секунду

                if (enhancement.Name == "Мощный щелчок")
                {
                    clickPower = Convert.ToDecimal(Math.Pow(2, enhancementAccount.Quantity)); // мощность клика (количество печенек за клик)
                }
                if (enhancement.Name == "Автономная ферма")
                {
                    offlinePercent = Convert.ToDecimal(0.05 * enhancementAccount.Quantity); // Процент оффлайн фарма
                }
            }

            labelCookies.SetBinding(Label.TextProperty, new Binding { Path = "Cookies", Source = selectedAccount, StringFormat = "{0:N2} Печенек!" });
            Device.BeginInvokeOnMainThread(() => { labelCookiesSecond.Text = $"{cookiesSecond} печенек в секунду"; });
            progressCookiesGold.SetBinding(ProgressBar.ProgressProperty, new Binding { Path = "ClickGold", Source = selectedAccount });
            progressСhocolate.SetBinding(ProgressBar.ProgressProperty, new Binding { Path = "ChocolateTime", Source = selectedAccount });

            var offline = offlinePercent * cookiesSecond * Convert.ToDecimal(DateInSecond(DateTime.Now) - DateInSecond(selectedAccount.LastEntrance)); // Подсчитывает сколько печенек получим за оффлайн
            cookiesForTime += offline;
            selectedAccount.Cookies += offline;
        }

        protected override bool OnBackButtonPressed()
        {
            updateUserСontinue = false;
            return base.OnBackButtonPressed();
        }

        private double DateInSecond(DateTime date)
        {
            return Math.Round(new TimeSpan(date.Ticks).TotalSeconds, 0);
        }

        private void Cookie_Click(object sender, EventArgs e)
        {
            if (!boostChocolate)
            {
                cookiesForTime += clickPower;
                selectedAccount.Cookies += clickPower;
            }
            else
            {
                cookiesForTime += clickPower;
                selectedAccount.Cookies += clickPower * 10;
            }

            if (selectedAccount.ClickGold < 1)
            {
                double i = 1;
                selectedAccount.ClickGold += Convert.ToDecimal(i / 300);
            }
            else
            {
                selectedAccount.ClickGold = 0;
                foreach (var item in imageButtons)
                {
                    item.TranslationY = 0;
                    item.IsVisible = true;
                }
                СhangeOfLocationInHeight();
            }
        }

        private void CookieGold_Clicked(object sender, EventArgs e)
        {
            ImageButton button = (ImageButton)sender;
            button.IsVisible = false;
            if (cookiesSecond > 0)
            {
                cookiesForTime += cookiesSecond * 240;
                selectedAccount.Cookies += cookiesSecond * 240;
            }
            else
            {
                cookiesForTime += 240;
                selectedAccount.Cookies += 240;
            }
        }

        private void ShopClick(object sender, EventArgs e)
        {
            Navigation.PushAsync(new ItemShop() { Title = "Магазин" });
        }

        private async void Booster_Click(object sender, EventArgs e) // Использование предмета TimeWarp
        {
            if (cookiesSecond != 0)
            {
                bool result = await DisplayAlert("Подтвердить действие", $"Вы хотите получить {(cookiesSecond) * 86400} Cookie за 1-о Искажение времени?", "Да", "Нет");
                if (result)
                {
                    var itemEnhancement = selectedAccount.EnhancementAccounts().FirstOrDefault(obj => obj.Enhancement().Name == "Искажение времени");

                    if (itemEnhancement != null)
                    {
                        if (itemEnhancement.Quantity > 0) // Проверка на количество
                        {
                            itemEnhancement.Quantity--;
                            cookiesForTime += cookiesSecond * 86400;
                            selectedAccount.Cookies += cookiesSecond * 86400;
                            await AccountMethod.UpdateCookie(selectedAccount, cookiesSecond * 86400);
                            await EnhancementAccountMethod.UpdateEnhancementAccount(itemEnhancement);

                            await DisplayAlert("Уведомление", "Вы успешно преобрели печеньки за Искажение времени", "Окей");
                        }
                        else
                            await DisplayAlert("Уведомление", "У вас нет Искажения времени", "Окей");
                    }
                    else
                        await DisplayAlert("Уведомление", "У вас нет Искажения времени", "Окей");
                }
            }
            else
                await DisplayAlert("Уведомление", "Так как у вас в данный момент вы получаете печенек в секунду 0\nто этой функцией не возможно воспользоваться.", "Окей");
        }

        private void QiwiDonate_Click(object sender, EventArgs e)
        {
            Navigation.PushAsync(new InformationAboutDonate { Title = "Описание о донате на Qiwi" });
        }

        private void Friend_Click(object sender, EventArgs e)
        {
            Navigation.PushAsync(new InformationAboutFriendList { Title = "Друзья" });
        }

        private bool UpdateUser() // Обновление аккаунта
        {
            MetChocolate();

            int time = Convert.ToInt32(300 - selectedAccount.ChocolateTime * 300); // int убирает погрешности в вычислении
            labelSecondСhocolate.Text = $"{time}";

            selectedAccount.LastEntrance = DateTime.Now;
            cookiesForTime += cookiesSecond;
            selectedAccount.Cookies += cookiesSecond;

            timerUpdateUser++;

            if (timerUpdateUser >= 15)
            {
                listCookiesForTime.Add(cookiesForTime);
                Task.Run(async () => await AccountMethod.UpdateAccountForTime(selectedAccount, listCookiesForTime.Last()));
                timerUpdateUser = 0;
                cookiesForTime = 0;
            }
            return updateUserСontinue;
        }

        private void MetChocolate()
        {
            if (selectedAccount.ChocolateTime < 1 && selectedAccount.ChocolateTime >= 0 && !boostChocolate) // Проверка на то что время меньше 1 и больше нуля и не работает boostChocolate
            {
                BackgroundColor = Color.FromRgb(146, 227, 255);
                double i = 1; // int будет округлять при делении из-за этого double
                selectedAccount.ChocolateTime += Convert.ToDecimal(i / 300);
                if (selectedAccount.ChocolateTime >= 1)
                {
                    selectedAccount.ChocolateTime = 1;
                }
            }
            else if (boostChocolate || selectedAccount.ChocolateTime == 1) // работает boostChocolate() или у аккаунта ChocolateTime = 1 (чтобы запустить метод)
            {
                boostChocolate = true;
                BackgroundColor = Color.FromRgb(187, 117, 42);

                Task.Run(async () =>
                {
                    for (int d = 0; d < 10; d++)
                    {
                        await Task.Delay(100);
                        double i = 1;
                        selectedAccount.ChocolateTime -= Convert.ToDecimal(i / 300);

                        int timeC = Convert.ToInt32(300 - selectedAccount.ChocolateTime * 300); // int убирает погрешности в вычислении
                        Device.BeginInvokeOnMainThread(() => { labelSecondСhocolate.Text = $"{timeC}"; }); // во вторичном потоке не будет изменять значение
                        if (selectedAccount.ChocolateTime <= 0)
                        {
                            boostChocolate = false;

                            if (selectedAccount.ChocolateTime <= 0)
                            {
                                selectedAccount.ChocolateTime = 0;
                                break;
                            }
                        }
                    }
                });
            }
        }
    }
}