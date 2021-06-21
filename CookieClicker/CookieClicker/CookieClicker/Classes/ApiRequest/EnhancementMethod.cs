using CookieClicker.Classes.Models;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace CookieClicker.Classes.ApiRequest
{
    public static class EnhancementMethod
    {
        private static string linq = "/api/Enhancement";
        public static async Task<List<Enhancement>> GetEnhancements()
        {
            var answer = await OptionsRequest.Request(linq, HttpMethod.Get);
            if (answer != null)
                return JsonConvert.DeserializeObject<List<Enhancement>>(answer);
            return null;
        }
        public static async Task<string> CreateEnhancement(Enhancement enhancement)
        {
            var answer = await OptionsRequest.Request(linq, HttpMethod.Post, enhancement);
            if (answer != null)
                return answer;
            return null;
        }
        public static async Task<string> UpdateEnhancement(Enhancement enhancement)
        {
            var answer = await OptionsRequest.Request(linq, HttpMethod.Put, enhancement);
            if (answer != null)
                return answer;
            return null;
        }
        public static async Task<string> DeleteEnhancement(int id)
        {
            var answer = await OptionsRequest.Request($"{linq}/id={id}", HttpMethod.Delete);
            if (answer != null)
                return answer;
            return null;
        }
    }
}
