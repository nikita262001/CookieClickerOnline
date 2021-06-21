using System.ComponentModel.DataAnnotations;

namespace CookieClicker.ClassLibrary.Models
{
    public partial class Account
    {
        [Key]
        public int IdAccount { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }
        public decimal Cookies { get; set; }
        public decimal ClickGold { get; set; }
        public decimal ChocolateTime { get; set; }
        public System.DateTime DateRegistration { get; set; }
        public System.DateTime LastEntrance { get; set; }
    }
}