using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using CookieClicker.Classes.Models;
using CookieClicker.Classes;
using CookieClicker.Classes.ApiRequest;

namespace CookieClicker
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ItemShop : ContentPage
    {
        Account selectedAccount = GetItems.SelectedAccount;
        CollectionView listEnhancements;
        Label labelCookies, labelCookiesSecond;

        public ItemShop()
        {
            BackgroundColor = Color.FromRgb(146, 227, 255);
            #region stackCookies
            labelCookies = new Label { FontSize = 20, HorizontalOptions = LayoutOptions.CenterAndExpand };
            labelCookies.SetBinding(Label.TextProperty, new Binding { Path = "Cookies", Source = selectedAccount, StringFormat = "{0:N2} Печенек!" });
            labelCookiesSecond = new Label { FontSize = 20, HorizontalOptions = LayoutOptions.CenterAndExpand };
            UpdateCookiesSecond();
            StackLayout stackCookies = new StackLayout { Children = { labelCookies, labelCookiesSecond }, BackgroundColor = Color.FromHsla(0, 0, 0, 0.3) };
            #endregion

            var size15 = Device.RuntimePlatform == Device.Android ? 10 : 15;
            var size20 = Device.RuntimePlatform == Device.Android ? 12 : 20;
            var size25 = Device.RuntimePlatform == Device.Android ? 14 : 25;
            //InitializeComponent();

            var formattedSpecial = new FormattedString();
            formattedSpecial.Spans.Add(new Span { Text = "Покупка", TextColor = Color.Yellow });
            formattedSpecial.Spans.Add(new Span { Text = " улучшений" });
            Label labelTitle = new Label { VerticalOptions = LayoutOptions.StartAndExpand, FormattedText = formattedSpecial, FontSize = 25, HeightRequest = 50, HorizontalTextAlignment = TextAlignment.Center, VerticalTextAlignment = TextAlignment.Center, BackgroundColor = Color.FromHex("#EA8C34") };
            listEnhancements = new CollectionView { SelectionMode = SelectionMode.Single, VerticalOptions = LayoutOptions.StartAndExpand, ItemsLayout = new GridItemsLayout(Device.RuntimePlatform == Device.Android ? 1 : 2, ItemsLayoutOrientation.Vertical) };
            SetItemsSource();
            listEnhancements.ItemTemplate = new DataTemplate(() =>
            {
                Image imageItem = new Image { Aspect = Aspect.AspectFit };
                imageItem.SetBinding(Image.SourceProperty, new Binding("PathImage"));

                Label labelName = new Label { FontSize = size25, };
                labelName.SetBinding(Label.TextProperty, new Binding { Path = "Name", });

                Image imageCookie = new Image { Source = "Images/CookieItem.png", Aspect = Aspect.AspectFit, HeightRequest = 30 };
                Label labelCost = new Label { FontSize = size20 };
                labelCost.SetBinding(Label.TextProperty, new Binding { Path = "Cost", StringFormat = "{0:F0}" });
                StackLayout stackPayCookie = new StackLayout { Children = { imageCookie, labelCost }, Orientation = StackOrientation.Horizontal, VerticalOptions = LayoutOptions.Center};

                Label labelLev = new Label { Text = "Количество: ", TextColor = Color.Yellow, FontSize = size15 };
                Label labelLevCount = new Label { FontSize = size20 };
                labelLevCount.SetBinding(Label.TextProperty, new Binding { Path = "Quantity" });
                StackLayout stackLev = new StackLayout { Children = { labelLev, labelLevCount }, Orientation = StackOrientation.Horizontal };

                Label labelBonus = new Label { Text = "Бонус: ", TextColor = Color.Yellow, FontSize = size15 };
                Label labelBonusFormat = new Label { FontSize = size20 };
                labelBonusFormat.SetBinding(Label.TextProperty, new Binding { Path = "BonusFormat" });
                StackLayout stackBonus = new StackLayout { Children = { labelBonus, labelBonusFormat }, Orientation = StackOrientation.Horizontal };

                #region relative
                RelativeLayout relative = new RelativeLayout { HeightRequest = Device.RuntimePlatform == Device.Android ? 70 : 100 };

                relative.Children.Add(imageItem,
                    Constraint.RelativeToParent((parent) => 0),
                    Constraint.RelativeToParent((parent) => 0),
                    Constraint.RelativeToParent((parent) => parent.Width * 2 / 9),
                    Constraint.RelativeToParent((parent) => parent.Height));

                relative.Children.Add(labelName,
                    Constraint.RelativeToView(imageItem, (parent, view) => view.Width + 3),
                    Constraint.RelativeToParent((parent) => 0),
                    Constraint.RelativeToParent((parent) => parent.Width * 4 / 9),
                    Constraint.RelativeToParent((parent) => parent.Height / 2));

                relative.Children.Add(stackPayCookie,
                    Constraint.RelativeToView(imageItem, (parent, view) => view.Width + 3),
                    Constraint.RelativeToParent((parent) => parent.Height / 2),
                    Constraint.RelativeToParent((parent) => parent.Width * 4 / 9),
                    Constraint.RelativeToParent((parent) => parent.Height / 2));

                relative.Children.Add(stackLev,
                    Constraint.RelativeToParent((parent) => parent.Width - (Device.RuntimePlatform == Device.Android ? 100 : 250)),
                    Constraint.RelativeToParent((parent) => 0),
                    Constraint.RelativeToParent((parent) => (Device.RuntimePlatform == Device.Android ? 100 : 250)),
                    Constraint.RelativeToParent((parent) => parent.Height * 0.3));

                relative.Children.Add(stackBonus,
                    Constraint.RelativeToView(stackLev, (parent, view) => view.X),
                    Constraint.RelativeToView(stackLev, (parent, view) => view.Y + view.Height + 10),
                    Constraint.RelativeToView(stackLev, (parent, view) => view.Width),
                    Constraint.RelativeToParent((parent) => parent.Height * 0.7));


                #endregion
                Frame frame = new Frame
                {
                    Content = new Frame
                    {
                        Margin = 2,
                        Content = relative,
                        CornerRadius = 8,
                        BackgroundColor = Color.FromHex("#EA8C34"),
                        HasShadow = true,
                        BorderColor = Color.Black,
                    },
                    BackgroundColor = Color.Transparent,
                };

                return frame;
            });

            listEnhancements.SelectionChanged += ListSpecial_ItemTapped;

            RelativeLayout mainR = new RelativeLayout { };

            #region mainR

            mainR.Children.Add(stackCookies,
                Constraint.RelativeToParent((parent) => 0),
                Constraint.RelativeToParent((parent) => 0),
                Constraint.RelativeToParent((parent) => parent.Width),
                Constraint.RelativeToParent((parent) => 75));

            mainR.Children.Add(labelTitle,
                Constraint.RelativeToParent((parent) => 0),
                Constraint.RelativeToView(stackCookies, (parent, stackTop) => stackTop.Height),
                Constraint.RelativeToParent((parent) => parent.Width),
                Constraint.RelativeToParent((parent) => parent.Height * 0.08));

            mainR.Children.Add(listEnhancements,
                Constraint.RelativeToParent((parent) => 0),
                Constraint.RelativeToView(labelTitle, (parent, view) => view.Height + view.Y),
                Constraint.RelativeToParent((parent) => parent.Width),
                Constraint.RelativeToView(stackCookies, (parent, stackTop) => parent.Height * 0.92 - stackTop.Height));
            #endregion
            Content = mainR;
        }

        private void UpdateCookiesSecond()
        {
            decimal cookiesSecond = 0;
            foreach (var enhancementAccount in selectedAccount.EnhancementAccounts())
            {
                var enhancement = enhancementAccount.Enhancement();
                cookiesSecond += enhancement.CookiePerSecond * enhancementAccount.Quantity; // Подсчитывает количество получаемых печенек в секунду
            }
            labelCookiesSecond.Text = $"{cookiesSecond} печенек в секунду";
        }

        private async void ListSpecial_ItemTapped(object sender, SelectionChangedEventArgs e)
        {
            if (e.CurrentSelection.FirstOrDefault() != null)
            {
                var shoppingItem = e.CurrentSelection.FirstOrDefault() as Shopping;
                if (selectedAccount.Cookies >= shoppingItem.Cost)
                {
                    var enhancement = selectedAccount.EnhancementAccounts().FirstOrDefault(obj => obj.IdEnhancement == shoppingItem.BuyEnh.IdEnhancement);
                    if (enhancement != null)
                    {
                        enhancement.Quantity += 1;
                        await EnhancementAccountMethod.UpdateEnhancementAccount(enhancement);
                    }
                    else // если такого нет то создаем новое
                    {
                        EnhancementAccount newEnhAcc = new EnhancementAccount { IdAccount = selectedAccount.IdAccount, IdEnhancement = shoppingItem.BuyEnh.IdEnhancement, Quantity = 1, };
                        GetItems.EnhancementAccounts.Add(newEnhAcc);
                        await EnhancementAccountMethod.CreateEnhancementAccount(newEnhAcc);
                    }
                    selectedAccount.Cookies -= shoppingItem.Cost;
                    await AccountMethod.UpdateCookie(selectedAccount, -shoppingItem.Cost);

                    shoppingItem.Quantity++;
                    switch (shoppingItem.Name)
                    {
                        case "Автономная ферма":
                            if (shoppingItem.Quantity < 20)
                            {
                                shoppingItem.Cost = Convert.ToDecimal(shoppingItem.FirstCost * Math.Pow(shoppingItem.Coef, shoppingItem.Quantity));
                            }
                            break;
                        case "Искажение времени":
                            shoppingItem.Cost = 100000;
                            break;
                        default:
                            shoppingItem.Cost = Convert.ToDecimal(shoppingItem.FirstCost * Math.Pow(shoppingItem.Coef, shoppingItem.Quantity));
                            break;
                    }
                }
                else
                    await DisplayAlert("Уведомление", "Не хватка печенек для покупки улучшения", "Ок");


                UpdateCookiesSecond();
                listEnhancements.SelectedItem = null; // CollectionView выделяет Item на который начали , и не дает еще раз нажать на тот же объект
            }
        }
        private void SetItemsSource() // Распределение и добавление в данных listEnhancements.ItemsSource
        {
            var shopItems = new ObservableCollection<Shopping>(); // лист улучшений

            IEnumerable<Enhancement> specialItems = from enhancement in GetItems.Enhancements
                                                    where enhancement.CookiePerSecond == 0 // special
                                                    select enhancement;
            AddItem(shopItems, specialItems, selectedAccount.EnhancementAccounts(), 2.5);

            IEnumerable<Enhancement> CPSItems = from enhancement in GetItems.Enhancements
                                                where enhancement.CookiePerSecond > 0 // CPS (CookiePerSecond)
                                                select enhancement;
            AddItem(shopItems, CPSItems, selectedAccount.EnhancementAccounts(), 1.3);


            listEnhancements.ItemsSource = shopItems.OrderBy(obj => obj.PerSecond).ToList();
        }
        private void AddItem(
            ObservableCollection<Shopping> shopItems /*список для листа*/,
            IEnumerable<Enhancement> buys /*все special или CPS*/,
            IEnumerable<EnhancementAccount> enhancementsAcc /*все улучшения аккаунта*/,
            double coef /*коэффициент*/)
        {
            foreach (var buyEnh in buys)
            {
                Shopping shopping = new Shopping
                {
                    BuyEnh = buyEnh,
                    Name = buyEnh.Name,
                    FormatBonus = $"{buyEnh.CookiePerSecond} Cps",
                    PathImage = buyEnh.ImageIB().GetImage(),
                    PerSecond = buyEnh.CookiePerSecond,
                    Quantity = 0,
                    Cost = buyEnh.FirstCost,
                    FirstCost = buyEnh.FirstCost,
                    Coef = coef,
                    BonusFormat = buyEnh.BonusFormat,
                };

                var enhancement = enhancementsAcc.FirstOrDefault(obj => obj.IdEnhancement == buyEnh.IdEnhancement);
                if (enhancement != null)
                {
                    shopping.Quantity = enhancement.Quantity;
                    if (buyEnh.Name == "Искажение времени")
                        shopping.Cost = 100000;
                    else
                        shopping.Cost = Convert.ToDecimal(buyEnh.FirstCost * Math.Pow(coef, enhancement.Quantity));
                }
                shopItems.Add(shopping);
            }
        }
    }
}