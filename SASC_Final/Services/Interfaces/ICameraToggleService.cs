using System;
using System.Collections.Generic;
using System.Text;

using ZXing.Net.Mobile.Forms;

namespace SASC_Final.Services.Interfaces
{
    public interface ICameraToggleService
    {
        bool IsUsingFrontCamera { get; }
        void ToggleCamera(ZXingScannerView scannerView);
    }
}
