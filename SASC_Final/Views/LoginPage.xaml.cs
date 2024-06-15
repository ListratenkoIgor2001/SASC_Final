using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using SASC_Final.ViewModels;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace SASC_Final.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class LoginPage : ContentPage
    {
        public LoginPage()
        {
            InitializeComponent();
            var vm = new LoginViewModel();
            vm.DisplayError += () => DisplayAlert("Error", vm.Error, "OK");
            vm.SuccessLogin += async () => await Shell.Current.GoToAsync($"//{nameof(SchedulePage)}");
            this.BindingContext = vm;         
        }
        private async void TapGestureRecognizer_Tapped(object sender, EventArgs e)
        {
            await Shell.Current.GoToAsync($"//{nameof(RegistrationPage)}");
            //Navigation.PushAsync(new RegistrationPage()).Wait();
        }
    }
}