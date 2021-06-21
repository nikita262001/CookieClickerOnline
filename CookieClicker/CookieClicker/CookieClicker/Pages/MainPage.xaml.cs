using System.ComponentModel;

using Xamarin.Forms;

namespace CookieClicker
{
    [DesignTimeVisible(false)]
    public partial class MainPage : MasterDetailPage
    {
        public MainPage()
        {

            Master = new MasterMenu { Title = "Топ игроков"};
            Detail = new NavigationPage( new Menu());

            MasterBehavior = MasterBehavior.Popover;
        }
    }
}