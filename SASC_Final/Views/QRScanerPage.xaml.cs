using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using SASC_Final.Models;
using SASC_Final.ViewModels;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using ZXing;
using ZXing.Net.Mobile.Forms;

namespace SASC_Final.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class QRScanerPage : ContentPage
    {
        QRScannerViewModel _viewModel;
        public QRScanerPage()
        {
            InitializeComponent();

            BindingContext = _viewModel = new QRScannerViewModel();
            this.scannerView = new ZXingScannerView();
            this.scannerView.Options = _viewModel.ScanningOptions;
            this.scannerView.IsScanning = scannerView.IsAnalyzing = _viewModel.IsScanning = true;
            _viewModel.DisplayError += () => DisplayAlert("Error", _viewModel.Error, "OK");
        }

        private async void ToolbarItem_Clicked(object sender, EventArgs e)
        {
            var AppData = DependencyService.Get<AppData>();
            var item = AppData.CurrentLessons.CurrentItem;
            await Shell.Current.GoToAsync($"//{nameof(LessonPage)}?LessonId={item.Id}&LoadFromContext={item.Id}");
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            //_viewModel.StartScanningCommand.Execute(scannerView);
        }

        protected override void OnDisappearing()
        {
            base.OnDisappearing();

            //_viewModel.StartScanningCommand.Execute(scannerView);
        }

        private void OnScanResult(Result result)
        {
            _viewModel.OnScanResult(result);
        }
    }
}