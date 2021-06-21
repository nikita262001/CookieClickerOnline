using System.ComponentModel.DataAnnotations;

namespace CookieClicker.ClassLibrary.Models
{
    public partial class Currency
    {
        [Key]
        public int IdCurrency { get; set; }
        public string Name { get; set; }
        public decimal RublesToOneCurrency { get; set; }
    }
}
