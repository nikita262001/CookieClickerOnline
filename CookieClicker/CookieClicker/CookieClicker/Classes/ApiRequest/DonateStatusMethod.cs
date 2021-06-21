using CookieClicker.Classes.Models;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace CookieClicker.Classes.ApiRequest
{
    public static class DonateStatusMethod
    {
        private static string linq = "/api/DonateStatus";
        public static async Task<List<DonateStatus>> GetDonateStatus()
        {
            var answer = await OptionsRequest.Request(linq, HttpMethod.Get);
            if (answer != null)
                return JsonConvert.DeserializeObject<List<DonateStatus>>(answer);
            return null;
        }
    }
}
