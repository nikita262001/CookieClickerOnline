using System.ComponentModel.DataAnnotations;

namespace CookieClicker.ClassLibrary.Models
{
    public partial class Donation
    {
        [Key]
        public int IdDonat { get; set; }
        public string IdQiwi { get; set; }
        public string Comment { get; set; }
        public System.DateTime Date { get; set; }
        public decimal PaymentAmount { get; set; }
        public int IdCurrency { get; set; }
        public int IdDonateStatus { get; set; }
    }
}
