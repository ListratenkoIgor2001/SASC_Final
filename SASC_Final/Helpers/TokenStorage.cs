using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Essentials;

namespace SASC_Final.Helpers
{
    public static class TokenStorage
    {
        public static async Task<string> GetTokenAsync(string tokenName = "authtoken")
        {
            return await SecureStorage.GetAsync(tokenName);
        }

        public static async Task SetTokenAsync(string token, string tokenName = "authtoken")
        {
            await SecureStorage.SetAsync(tokenName, token);
        }

        public static void RemoveToken(string tokenName = "authtoken")
        {
            SecureStorage.Remove(tokenName);
        }
    }
}
