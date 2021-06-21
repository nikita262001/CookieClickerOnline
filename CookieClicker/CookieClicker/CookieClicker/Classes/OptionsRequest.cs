using Newtonsoft.Json;
using System;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace CookieClicker.Classes
{
    static internal class OptionsRequest
    {
        static private string linq = "http://93.157.248.51:65000",  //https://localhost:44390
            keyApi = "SecretKeyVolk";

        public static async Task<string> Request(string additionalLink, HttpMethod httpMethod, object obj = null)
        {
            HttpClient client = new HttpClient();
            HttpRequestMessage request = new HttpRequestMessage
            {
                Method = httpMethod,
                RequestUri = new Uri(linq + additionalLink),
            };
            request.Headers.Add("Accept", "application/json");
            request.Headers.Add("ApiKey", keyApi);

            if (obj != null)
                request.Content = new StringContent(JsonConvert.SerializeObject(obj), Encoding.UTF8, "application/json");

            try
            {
                var response = await client.SendAsync(request);
                if (response.StatusCode == HttpStatusCode.OK)
                {
                    var answer = response.Content;
                    return await answer.ReadAsStringAsync();
                }
            }
            catch (Exception) // Если сервер не доступен или не получили ответа или ошибка в запросе
            {
            }
            return null;
        }
    }
}
