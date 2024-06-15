using System.Security.Cryptography;
using System.Text;
using Android.Content;
using Android.OS;

using Android.App;
using Android.Runtime;
using Android.Views;
using Android.Widget;

using SASC_Final.Droid.Services;
using SASC_Final.Services.Interfaces;

using Xamarin.Forms;

[assembly: Dependency(typeof(DeviceInfoAndroid))]

namespace SASC_Final.Droid.Services
{
    public class DeviceInfoAndroid : IDeviceInfo
    {
        public byte[] GetSerialHash()
        {
            var androidId = Android.Provider.Settings.Secure.GetString(Android.App.Application.Context.ContentResolver, Android.Provider.Settings.Secure.AndroidId);
            using (var sha256 = SHA256.Create())
            {
                return sha256.ComputeHash(Encoding.UTF8.GetBytes(androidId));
            }
        }
    }
}