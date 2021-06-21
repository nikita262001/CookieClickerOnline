
using System.Linq;

namespace CookieClicker.Classes.Models
{
    public partial class Enhancement
    {
        public int IdEnhancement { get; set; }
        public string Name { get; set; }
        public int FirstCost { get; set; }
        public int CookiePerSecond { get; set; }
        public string BonusFormat { get; set; }
        public int IdTypeEnhancement { get; set; }
        public int IdImageIB { get; set; }

        public TypeEnhancement TypeEnhancement() =>
            GetItems.TypeEnhancements.FirstOrDefault(obj => obj.IdTypeEnhancement == IdTypeEnhancement);

        public ImageIB ImageIB() =>
            GetItems.Images.FirstOrDefault(obj => obj.IdImageIB == IdImageIB);
    }
}
