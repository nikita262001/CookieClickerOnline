using CookieClicker.Classes.Models;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace CookieClicker.Classes.ApiRequest
{
    public static class TypeEnhancementMethod
    {
        private static string linq = "/api/TypeEnhancement";
        public static async Task<List<TypeEnhancement>> GetTypeEnhancements()
        {
            var answer = await OptionsRequest.Request(linq, HttpMethod.Get);
            if (answer != null)
                return JsonConvert.DeserializeObject<List<TypeEnhancement>>(answer);
            return null;
        }
        public static async Task<string> UpdateTypeEnhancement(TypeEnhancement typeEnhancement)
        {
            var answer = await OptionsRequest.Request(linq, HttpMethod.Put, typeEnhancement);
            if (answer != null)
                return answer;
            return null;
        }
    }
}

