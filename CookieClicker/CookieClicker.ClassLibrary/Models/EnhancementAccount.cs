using System.ComponentModel.DataAnnotations;

namespace CookieClicker.ClassLibrary.Models
{
    public partial class EnhancementAccount
    {
        [Key]
        public int IdEnhancementAccount { get; set; }
        public int IdAccount { get; set; }
        public int IdEnhancement { get; set; }
        public int Quantity { get; set; }
    }
}
