using CookieClicker.ClassLibrary.Models;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace CookieClicker.ClassLibrary.ApiRequest
{
    public static class CurrencyMethod
    {
        private static string linq = "/api/Currency";
        public static async Task<List<Currency>> GetCurrencies()
        {
            var answer = await OptionsRequest.Request(linq, HttpMethod.Get);
            if (answer != null)
                return JsonConvert.DeserializeObject<List<Currency>>(answer);
            return null;
        }
        public static async Task<string> CreateCurrency(Currency currency)
        {
            var answer = await OptionsRequest.Request(linq, HttpMethod.Post, currency);
            if (answer != null)
                return answer;
            return null;
        }
        public static async Task<string> UpdateCurrency(Currency currency)
        {
            var answer = await OptionsRequest.Request(linq, HttpMethod.Put, currency);
            if (answer != null)
                return answer;
            return null;
        }
        public static async Task<string> DeleteCurrency(int id)
        {
            var answer = await OptionsRequest.Request($"{linq}/id={id}", HttpMethod.Delete);
            if (answer != null)
                return answer;
            return null;
        }
    }
}
