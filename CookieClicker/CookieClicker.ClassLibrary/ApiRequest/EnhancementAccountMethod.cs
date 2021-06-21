using CookieClicker.ClassLibrary.Models;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace CookieClicker.ClassLibrary.ApiRequest
{
    public static class EnhancementAccountMethod
    {
        private static string linq = "/api/EnhancementAccount";
        public static async Task<List<EnhancementAccount>> GetEnhancementAccounts()
        {
            var answer = await OptionsRequest.Request(linq, HttpMethod.Get);
            if (answer != null)
                return JsonConvert.DeserializeObject<List<EnhancementAccount>>(answer);
            return null;
        }
        public static async Task<string> CreateEnhancementAccount(EnhancementAccount enhancementAccount)
        {
            var answer = await OptionsRequest.Request(linq, HttpMethod.Post, enhancementAccount);
            if (answer != null)
                return answer;
            return null;
        }
        public static async Task<string> UpdateEnhancementAccount(EnhancementAccount enhancementAccount)
        {
            var answer = await OptionsRequest.Request(linq, HttpMethod.Put, enhancementAccount);
            if (answer != null)
                return answer;
            return null;
        }
        public static async Task<string> DeleteEnhancementAccount(int id)
        {
            var answer = await OptionsRequest.Request($"{linq}/id={id}", HttpMethod.Delete);
            if (answer != null)
                return answer;
            return null;
        }
    }
}
