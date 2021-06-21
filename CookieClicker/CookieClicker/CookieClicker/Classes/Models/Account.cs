
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;

namespace CookieClicker.Classes.Models
{
    public partial class Account : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        decimal cookies;
        decimal clickGold;
        decimal chocolateTime;

        public int IdAccount { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }
        public decimal Cookies 
        {
            get
            {
                return cookies;
            }
            set 
            {
                cookies = value;
                NotifyPropertyChanged();
            }
        }
        public decimal ClickGold
        {
            get
            {
                return clickGold;
            }
            set
            {
                clickGold = value;
                NotifyPropertyChanged();
            }
        }
        public decimal ChocolateTime
        {
            get
            {
                return chocolateTime;
            }
            set
            {
                chocolateTime = value;
                NotifyPropertyChanged();
            }
        }
        public System.DateTime DateRegistration { get; set; }
        public System.DateTime LastEntrance { get; set; }

        private void NotifyPropertyChanged([CallerMemberName] String propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public List<EnhancementAccount> EnhancementAccounts() =>
             GetItems.EnhancementAccounts.Where(obj => obj.IdAccount == IdAccount).ToList();
    }
}