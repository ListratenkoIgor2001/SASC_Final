using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using Foundation;
using Xamarin.Forms;

using SASC_Final.iOS.Services;
using SASC_Final.Services.Interfaces;

using UIKit;

[assembly: Dependency(typeof(DeviceInfoiOS))]

namespace SASC_Final.iOS.Services
{
    public class DeviceInfoiOS : IDeviceInfo
    {
        public byte[] GetSerialHash()
        {
            var identifier = UIDevice.CurrentDevice.IdentifierForVendor.AsString();
            using (var sha256 = SHA256.Create())
            {
                return sha256.ComputeHash(Encoding.UTF8.GetBytes(identifier));
            }
        }
    }
}