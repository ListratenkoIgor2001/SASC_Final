using System;
using System.Collections.Generic;
using System.Text;

namespace SASC_Final.Services.Interfaces
{
    public interface IDeviceInfo
    {
        byte[] GetSerialHash();
    }
}
