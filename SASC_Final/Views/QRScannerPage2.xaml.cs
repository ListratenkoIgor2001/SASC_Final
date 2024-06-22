using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;

using Microsoft.AppCenter.Crashes;

using SASC_Final.Services.Interfaces;
using SASC_Final.ViewModels;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using ZXing.Mobile;
using ZXing.Net.Mobile.Forms;

using static Xamarin.Essentials.AppleSignInAuthenticator;

namespace SASC_Final.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class QRScannerPage2 : ContentPage
    {
        private ZXingScannerView zxing;
        private ZXingDefaultOverlay overlay;
        private QRScannerViewModel _viewModel;
        private bool useFrontCamera = true;
        //ICameraToggleService _cameraToggleService;

        public QRScannerPage2() : base()
        {
            //_cameraToggleService = DependencyService.Get<ICameraToggleService>();
            
            _viewModel = new QRScannerViewModel();
            _viewModel.DisplayError += () => DisplayAlert("Error", _viewModel.Error, "OK");
            _viewModel.GoBack += async () => await Navigation.PopAsync();
            this.BindingContext = _viewModel;
            //InitializeComponent();

            zxing = new ZXingScannerView
            {
                HorizontalOptions = LayoutOptions.FillAndExpand,
                VerticalOptions = LayoutOptions.FillAndExpand,
                AutomationId = "zxingScannerView",
                Options = new MobileBarcodeScanningOptions
                {
                    DelayBetweenContinuousScans = 1500,
                    //UseFrontCameraIfAvailable = false,
                    UseFrontCameraIfAvailable = _viewModel.UseFrontCamera,
                    AutoRotate = true,
                    TryHarder = true,
                    PossibleFormats = new List<ZXing.BarcodeFormat>
                    {
                        ZXing.BarcodeFormat.QR_CODE
                    }
                }
            };
            zxing.OnScanResult += (result) => _viewModel.OnScanResult(result);
            overlay = new ZXingDefaultOverlay
            {
                TopText = "",
                BottomText = "Сканирование начинается автоматически",
                ShowFlashButton = zxing.HasTorch,
                AutomationId = "zxingDefaultOverlay",
            };
            overlay.FlashButtonClicked += (sender, e) =>
            {
                zxing.IsTorchOn = !zxing.IsTorchOn;
            };

            //var switchCameraButton = new ToolbarItem();
            //switchCameraButton.IconImageSource = "camera_switch.png";
            //switchCameraButton.Order = ToolbarItemOrder.Primary;
            //switchCameraButton.Priority = 0;
            //switchCameraButton.Clicked += CameraSwitch_Clicked;
            //this.ToolbarItems.Add(switchCameraButton);

            //var backButton = new ToolbarItem();
            //backButton.Text = "Назад";
            //backButton.Clicked += ToolbarItem_Clicked;
            //this.ToolbarItems.Add(backButton);


            var grid = new Grid
            {
                VerticalOptions = LayoutOptions.FillAndExpand,
                HorizontalOptions = LayoutOptions.FillAndExpand,
            };
            grid.Children.Add(zxing);
            grid.Children.Add(overlay);

            Content = grid;
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            zxing.IsScanning = true;
            zxing.IsAnalyzing = true;
        }

        protected override void OnDisappearing()
        {
            zxing.IsScanning = false;
            zxing.IsAnalyzing = false;
            base.OnDisappearing();
        }

        private async void ToolbarItem_Clicked(object sender, EventArgs e)
        {
            var AppData = DependencyService.Get<AppData>();
            var item = AppData.CurrentLessons.CurrentItem;
            //await Shell.Current.GoToAsync($"//{nameof(LessonPage)}?LessonId={item.Id}&LoadFromContext=11001");
            //await Shell.Current.GoToAsync($"..?LessonId={item.Id}&LoadFromContext=11001");
            //await Shell.Current.GoToAsync($"..?LessonId={item.Id}");
            //await Shell.Current.GoToAsync($"..");
            //zxing.IsScanning = false;
            //zxing.IsAnalyzing = false;
            //await Shell.Current.Navigation.PopAsync();
            //AppShell shell = Shell.Current as AppShell;
            //shell.GoBack();
            await Navigation.PopModalAsync();
        }

        private async void FlashButtonClicked(object sender, EventArgs e)
        {
            zxing.IsTorchOn = !zxing.IsTorchOn;
        }

        private void CameraSwitch_Clicked(object sender, EventArgs e)
        {
            _viewModel.UseFrontCamera = !_viewModel.UseFrontCamera;
            //InitializeComponent();
            //this.BindingContext = _viewModel;
            /*
            zxing = new ZXingScannerView
            {
                HorizontalOptions = LayoutOptions.FillAndExpand,
                VerticalOptions = LayoutOptions.FillAndExpand,
                AutomationId = "zxingScannerView",
                Options = new MobileBarcodeScanningOptions
                {
                    UseFrontCameraIfAvailable = useFrontCamera,
                    AutoRotate = true,
                    TryHarder = true,
                    PossibleFormats = new List<ZXing.BarcodeFormat>
                    {
                        ZXing.BarcodeFormat.QR_CODE
                    }
                }
            };
            useFrontCamera = !useFrontCamera;
            zxing.Options = new MobileBarcodeScanningOptions
            {
                UseFrontCameraIfAvailable = useFrontCamera,
                AutoRotate = true,
                TryHarder = true,
                PossibleFormats = new List<ZXing.BarcodeFormat>
                    {
                        ZXing.BarcodeFormat.QR_CODE
                    }
            };
            */
        }

        private void zxing_OnScanResult(ZXing.Result result)
        {
            _viewModel.OnScanResult(result);
        }
    }
}