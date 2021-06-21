
using System.Linq;

namespace CookieClicker.Classes.Models
{
    public partial class EnhancementAccount
    {
        public int IdEnhancementAccount { get; set; }
        public int IdAccount { get; set; }
        public int IdEnhancement { get; set; }
        public int Quantity { get; set; }


        public Account Account() =>
            GetItems.Accounts.FirstOrDefault(obj => obj.IdAccount == IdAccount);

        public Enhancement Enhancement() =>
            GetItems.Enhancements.FirstOrDefault(obj => obj.IdEnhancement == IdEnhancement);
    }
}
