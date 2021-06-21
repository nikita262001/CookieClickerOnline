using System.ComponentModel.DataAnnotations;

namespace CookieClicker.ClassLibrary.Models
{
    public partial class DonateStatus
    {
        [Key]
        public int IdDonateStatus { get; set; }
        public string Name { get; set; }
    }
}
