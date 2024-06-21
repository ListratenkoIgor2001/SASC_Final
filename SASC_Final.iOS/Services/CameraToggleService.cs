using Xamarin.Forms;
using SASC_Final.iOS.Services;
using ZXing.Net.Mobile.Forms;
using SASC_Final.Services.Interfaces;

[assembly: Dependency(typeof(CameraToggleService))]
namespace SASC_Final.iOS.Services
{
    public class CameraToggleService : ICameraToggleService
    {
        private bool isUsingFrontCamera = false;
        public bool IsUsingFrontCamera => isUsingFrontCamera;

        public void ToggleCamera(ZXingScannerView scannerView)
        {
            isUsingFrontCamera = !isUsingFrontCamera;
            scannerView.Options.UseFrontCameraIfAvailable = isUsingFrontCamera;
            scannerView.IsScanning = false;
            scannerView.IsScanning = true;
        }

    }
}