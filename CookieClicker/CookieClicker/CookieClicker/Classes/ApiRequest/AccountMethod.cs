using CookieClicker.Classes.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace CookieClicker.Classes.ApiRequest
{
    public static class AccountMethod
    {
        private static string linq = "/api/Account";
        public static async Task<List<Account>> GetAllAccounts()
        {
            var answer = await OptionsRequest.Request(linq, HttpMethod.Get);
            if (answer != null)
                return JsonConvert.DeserializeObject<List<Account>>(answer);
            return null;
        }
        public static async Task<List<Account>> GetTakeCountTop(int takeCount)
        {
            var answer = await OptionsRequest.Request($"{linq}/take={takeCount}", HttpMethod.Get);
            if (answer != null)
                return JsonConvert.DeserializeObject<List<Account>>(answer);
            return null;
        }
        public static async Task<Account> GetTargetAccount(string login, string password)
        {
            var answer = await OptionsRequest.Request($"{linq}/login={login}&password={password}", HttpMethod.Get);
            if (answer != null)
                return JsonConvert.DeserializeObject<Account>(answer);
            return null;
        }
        public static async Task<string> CreateAccount(Account account)
        {
            var answer = await OptionsRequest.Request(linq, HttpMethod.Post, account);
            if (answer != null)
                return answer;
            return null;
        }
        public static async Task<string> UpdateAccount(Account account)
        {
            var answer = await OptionsRequest.Request(linq, HttpMethod.Put, account);
            if (answer != null)
                return answer;
            return null;
        }
        public static async Task<string> DeleteAccount(int id)
        {
            var answer = await OptionsRequest.Request($"{linq}/id={id}", HttpMethod.Delete);
            if (answer != null)
                return answer;
            return null;
        }

        public static async Task<string> UpdateCookie(Account account, decimal cookie)
        {
            var answer = await OptionsRequest.Request($"{linq}/account={account.IdAccount}&countCookies={Math.Round(cookie, 4)}", HttpMethod.Put);
            if (answer != null)
                return answer;
            return null;
        }

        public static async Task<string> UpdateAccountForTime(Account account, decimal cookie)
        {
            var answer = await OptionsRequest.Request($"{linq}/account={account.IdAccount}&cookies={Math.Round(cookie, 4)}&clickGold={account.ClickGold}&chocolateTime={account.ChocolateTime}".Replace("0,","0."), HttpMethod.Put);
            if (answer != null)
                return answer;
            return null;
        }
    }
}
