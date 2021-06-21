using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace CookieClicker.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class InformationAboutDonate : ContentPage
    {
        public InformationAboutDonate()
        {
            InitializeComponent();

            title.FontSize = Device.RuntimePlatform == Device.Android ? 18 : 25;
            var fs = new FormattedString();
            fs.Spans.Add(new Span { Text = "Для того чтобы пожертвовать на данный аккаунт нужно:\n"});
            fs.Spans.Add(new Span { Text = "  1) Нажать на кнопку \"Далее\"(вы перейдете на сайт Qiwi).\n" });
            fs.Spans.Add(new Span { Text = "  2) Нужно ввести в \"Комментарий к переводу\" логин пользователя которому хотите задонатить.\n" });
            fs.Spans.Add(new Span { Text = "  3) Выбираете способ оплаты.\n" });
            fs.Spans.Add(new Span { Text = "  4) Вводите сумму и валюту.\n" });
            fs.Spans.Add(new Span { Text = "  5) Нажимаете на кнопку \"Оплатить\".\n" });
            fs.Spans.Add(new Span { Text = "  6) Переходите обратно в приложение и вас сразу будет ждать ваши печеньки.\n" });
            fs.Spans.Add(new Span { Text = "  7) Перезайдите в аккаунт.\n\n" });
            fs.Spans.Add(new Span { Text = "Данные о сумме Пожертвовании:\n" });
            fs.Spans.Add(new Span { Text = "  Российский рубль = 150 000 печенек.\n" });
            fs.Spans.Add(new Span { Text = "  Американский доллар = 74.87 * 150 000 = 11 230 500 печенек.\n" });
            fs.Spans.Add(new Span { Text = "  Евро = 90.59 * 150 000 = 13 588 500 печенек.\n" });
            labelDiscriptions.FormattedText = fs;
        }

        private async void ButtonNext_Clicked(object sender, EventArgs e)
        {
            bool exception = false;
            try
            {
                await Browser.OpenAsync(new Uri("https://qiwi.com/p/79991556838"), BrowserLaunchMode.SystemPreferred);
            }
            catch (Exception ex)
            {
                exception = true;
                await DisplayAlert("Уведомление", "У вас нет интернета или сайт временно недоступен.", "Окей");
            }
            if (!exception)
                await Navigation.PopAsync();
        }
    }
}