using CookieClicker.Classes;
using CookieClicker.Classes.Models;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace CookieClicker.ClassLibrary.ApiRequest
{
    public static class FriendMethod
    {
        private static string linq = "/api/Friend";
        public static async Task<List<Friend>> GetFriends()
        {
            var answer = await OptionsRequest.Request(linq, HttpMethod.Get);
            if (answer != null)
                return JsonConvert.DeserializeObject<List<Friend>>(answer);
            return null;
        }

        public static async Task<List<Account>> GetAllFriendTarget(int idAccount)
        {
            var answer = await OptionsRequest.Request($"{linq}/idFriend={idAccount}/BeFriends={true}", HttpMethod.Get);
            if (answer != null)
                return JsonConvert.DeserializeObject<List<Account>>(answer);
            return null;
        }

        public static async Task<List<Account>> GetAllInvitedFriendTarget(int idAccount)
        {
            var answer = await OptionsRequest.Request($"{linq}/idFriend={idAccount}/BeFriends={false}", HttpMethod.Get);
            if (answer != null)
                return JsonConvert.DeserializeObject<List<Account>>(answer);
            return null;
        }
        public static async Task<string> BeFriend(int idFriend)
        {
            var answer = await OptionsRequest.Request($"{linq}/id={idFriend}", HttpMethod.Put);
            if (answer != null)
                return answer;
            return null;
        }

        public static async Task<string> AddFriend(Friend friend)
        {
            var answer = await OptionsRequest.Request($"{linq}", HttpMethod.Post, friend);
            if (answer != null)
                return answer;
            return null;
        }

        public static async Task<string> DeleteFriend(int idFriend)
        {
            var answer = await OptionsRequest.Request($"{linq}/id={idFriend}", HttpMethod.Delete);
            if (answer != null)
                return answer;
            return null;
        }
    }
}
