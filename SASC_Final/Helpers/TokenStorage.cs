using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Essentials;

namespace SASC_Final.Helpers
{
    public static class TokenStorage
    {
        public static async Task<string> GetTokenAsync(string fieldName = "authtoken")
        {
            return await SecureStorage.GetAsync(fieldName);
        }

        public static async Task SetTokenAsync(string token, string fieldName = "authtoken")
        {
            await SecureStorage.SetAsync(fieldName, token);
        }

        public static void RemoveToken(string fieldName = "authtoken")
        {
            SecureStorage.Remove(fieldName);
        }
    }
}
