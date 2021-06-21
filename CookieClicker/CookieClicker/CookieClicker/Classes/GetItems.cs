using CookieClicker.Classes.ApiRequest;
using CookieClicker.Classes.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CookieClicker.Classes
{
    public static class GetItems
    {
        public static Account SelectedAccount { get; set; }
        public static List<Account> Accounts { get; set; }
        public static List<EnhancementAccount> EnhancementAccounts { get; set; }
        public static List<Enhancement> Enhancements { get; set; }
        public static List<TypeEnhancement> TypeEnhancements { get; set; }
        public static List<ImageIB> Images { get; set; }

        public static async Task<bool> GetFull()
        {
            await GetAccounts();
            await GetEnhancementAccounts();
            await GetEnhancements();
            await GetTypeEnhancements();
            await GetImages();
            if (Accounts != null && 
                EnhancementAccounts != null && 
                Enhancements != null && 
                TypeEnhancements != null && 
                Images != null )
                return true;
            else
                return false;
        }

        public static async Task<Account> GetSelectedAccount(string login, string password) =>
            SelectedAccount = await AccountMethod.GetTargetAccount(login, password);

        public static async Task GetAccounts() =>
            Accounts = await AccountMethod.GetAllAccounts();

        public static async Task GetEnhancementAccounts() =>
            EnhancementAccounts = await EnhancementAccountMethod.GetEnhancementAccounts();

        public static async Task GetEnhancements() =>
            Enhancements = await EnhancementMethod.GetEnhancements();

        public static async Task GetTypeEnhancements() =>
            TypeEnhancements = await TypeEnhancementMethod.GetTypeEnhancements();

        public static async Task GetImages() =>
            Images = await ImageIBMethod.GetImageIBs();
    }
}
