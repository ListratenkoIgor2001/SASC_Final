using SASC_Final.Droid.Services;
using SASC_Final.Services.Interfaces;
using Xamarin.Forms;
using ZXing.Net.Mobile.Forms;

[assembly: Dependency(typeof(CameraToggleService))]
namespace SASC_Final.Droid.Services
{
    public class CameraToggleService : ICameraToggleService
    {
        private bool isUsingFrontCamera = false;

        public void ToggleCamera(ZXingScannerView scannerView)
        {
            isUsingFrontCamera = !isUsingFrontCamera;
            scannerView.Options.UseFrontCameraIfAvailable = isUsingFrontCamera;
            scannerView.IsScanning = false;
            scannerView.IsScanning = true;
        }

        public bool IsUsingFrontCamera => isUsingFrontCamera;
    }
}