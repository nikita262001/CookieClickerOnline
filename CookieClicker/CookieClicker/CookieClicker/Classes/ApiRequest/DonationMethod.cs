using CookieClicker.Classes.Models;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace CookieClicker.Classes.ApiRequest
{
    public static class DonationMethod
    {
        private static string linq = "/api/Donation";
        public static async Task<List<Donation>> GetDonations()
        {
            var answer = await OptionsRequest.Request(linq, HttpMethod.Get);
            if (answer != null)
                return JsonConvert.DeserializeObject<List<Donation>>(answer);
            return null;
        }
        public static async Task<string> UpdateDonation(Donation donate)
        {
            var answer = await OptionsRequest.Request(linq, HttpMethod.Put, donate);
            if (answer != null)
                return answer;
            return null;
        }
    }
}

