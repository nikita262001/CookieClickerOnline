using System.ComponentModel.DataAnnotations;

namespace CookieClicker.ClassLibrary.Models
{
    public partial class Enhancement
    {
        [Key]
        public int IdEnhancement { get; set; }
        public string Name { get; set; }
        public int FirstCost { get; set; }
        public int CookiePerSecond { get; set; }
        public string BonusFormat { get; set; }
        public int IdTypeEnhancement { get; set; }
        public int IdImageIB { get; set; }
    }
}
