using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;

using Microsoft.AppCenter.Crashes;

using SASC_Final.ViewModels;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using ZXing.Net.Mobile.Forms;

namespace SASC_Final.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class QRScannerPage2 : ContentPage
    {
        ZXingScannerView zxing;
        ZXingDefaultOverlay overlay;
        QRScannerViewModel _viewModel;
        public QRScannerPage2():base()
        {
            
            _viewModel = new QRScannerViewModel();
            zxing = new ZXingScannerView
            {
                HorizontalOptions = LayoutOptions.FillAndExpand,
                VerticalOptions = LayoutOptions.FillAndExpand,
                AutomationId = "zxingScannerView",
            };
            zxing.OnScanResult += (result) => _viewModel.OnScanResult(result);
            _viewModel.DisplayError += () => DisplayAlert("Error", _viewModel.Error, "OK");
            /*  
                (result) =>
            Device.BeginInvokeOnMainThread(async () =>
                {

                    // Stop analysis until we navigate away so we don't keep reading barcodes
                    zxing.IsAnalyzing = false;

                    // Show an alert
                    await DisplayAlert("Scanned Barcode", result.Text, "OK");

                    // Navigate away
                    await Navigation.PopAsync();
                });
            */
            overlay = new ZXingDefaultOverlay
            {
                TopText = "Hold your phone up to the barcode",
                BottomText = "Scanning will happen automatically",
                ShowFlashButton = zxing.HasTorch,
                AutomationId = "zxingDefaultOverlay",
            };
            overlay.FlashButtonClicked += (sender, e) =>
            {
                zxing.IsTorchOn = !zxing.IsTorchOn;
            };
            var grid = new Grid
            {
                VerticalOptions = LayoutOptions.FillAndExpand,
                HorizontalOptions = LayoutOptions.FillAndExpand,
            };
            var x = new ToolbarItem();
            this.ToolbarItems.Add(x);
            grid.Children.Add(zxing);
            grid.Children.Add(overlay);

            x.Text = "Назад";
            x.Clicked += ToolbarItem_Clicked;
            
            Content = grid;

        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            zxing.IsScanning = true;
        }

        protected override void OnDisappearing()
        {
            zxing.IsScanning = false;

            base.OnDisappearing();
        }
        private async void ToolbarItem_Clicked(object sender, EventArgs e)
        {
            var AppData = DependencyService.Get<AppData>();
            var item = AppData.CurrentLessons.CurrentItem;
            await Shell.Current.GoToAsync($"//{nameof(LessonPage)}?LessonId={item.Id}&LoadFromContext={item.Id}");
        }
    }
}