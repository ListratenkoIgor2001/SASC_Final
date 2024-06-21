using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using SASC_Final.Models;
using SASC_Final.Services;
using SASC_Final.ViewModels;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using ZXing.Net.Mobile.Forms;

namespace SASC_Final.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    [QueryProperty(nameof(LessonId), nameof(LessonId))]
    public partial class QRGeneratorPage : ContentPage
    {
        private string lessonId;
        public string LessonId
        {
            get => lessonId;
            set
            {
                lessonId = value;
                OnPropertyChanged();
            }
        }
        QRGeneratorViewModel _viewModel;

        public QRGeneratorPage()
        {
            BindingContext = _viewModel = new QRGeneratorViewModel();
            InitializeComponent();
            this.qrCodeImageView.BarcodeValue = _viewModel.QRCodeString;
        }
        protected override void OnDisappearing()
        {
            _viewModel.TimerAlive = false;
            base.OnDisappearing();
        }

        private async void ToolbarItem_Clicked(object sender, EventArgs e)
        {
            await Shell.Current.GoToAsync($"//{nameof(SchedulePage)}");
        }

        private async void SettingsButton_Clicked(object sender, EventArgs e)
        {
            //await Navigation.PushModalAsync(new SettingsPage());
        }
        private async void LogoutButton_Clicked(object sender, EventArgs e)
        {
            var AppData = DependencyService.Get<AppData>();
            var auth = DependencyService.Get<IAuth>();
            await auth.Logout();
            AppData.Clear(); 
            
            Application.Current.MainPage = new NavigationPage(new LoginPage()); 
            await Navigation.PopToRootAsync();
        }

    }
}