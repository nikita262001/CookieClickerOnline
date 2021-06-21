using System.ComponentModel.DataAnnotations;

namespace CookieClicker.ClassLibrary.Models
{
    public partial class TypeEnhancement
    {
        [Key]
        public int IdTypeEnhancement { get; set; }
        public string Name { get; set; }
        public double СoefNextBuy { get; set; }
    }
}