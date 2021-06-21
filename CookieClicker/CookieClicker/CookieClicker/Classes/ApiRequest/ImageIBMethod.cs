using CookieClicker.Classes.Models;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace CookieClicker.Classes.ApiRequest
{
    public static class ImageIBMethod
    {
        private static string linq = "/api/ImageIB";
        public static async Task<List<ImageIB>> GetImageIBs()
        {
            var answer = await OptionsRequest.Request(linq, HttpMethod.Get);
            if (answer != null)
                return JsonConvert.DeserializeObject<List<ImageIB>>(answer);
            return null;
        }
        public static async Task<ImageIB> GetImageIB(int id)
        {
            var answer = await OptionsRequest.Request($"{linq}/id={id}", HttpMethod.Get);
            if (answer != null)
                return JsonConvert.DeserializeObject<ImageIB>(answer);
            return null;
        }
        public static async Task<string> CreateImageIB(ImageIB imageIB)
        {
            var answer = await OptionsRequest.Request(linq, HttpMethod.Post, imageIB);
            if (answer != null)
                return answer;
            return null;
        }
        public static async Task<string> UpdateImageIB(ImageIB imageIB)
        {
            var answer = await OptionsRequest.Request(linq, HttpMethod.Put, imageIB);
            if (answer != null)
                return answer;
            return null;
        }
        public static async Task<string> DeleteImageIB(int id)
        {
            var answer = await OptionsRequest.Request($"{linq}/id={id}", HttpMethod.Delete);
            if (answer != null)
                return answer;
            return null;
        }
    }
}
