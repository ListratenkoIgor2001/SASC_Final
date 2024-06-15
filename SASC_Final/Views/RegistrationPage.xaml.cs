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
    public partial class RegistrationPage : ContentPage
    {
        public RegistrationPage()
        {
            var vm = new RegistrationViewModel();
            vm.DisplayError += () => DisplayAlert("Error", vm.Error, "Оk");
            vm.SuccessRegister += () => DisplayAlert("", "Запрос на регистрацию успешно принят", "Оk");
            this.BindingContext = vm;
            InitializeComponent();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            Shell.SetBackButtonBehavior(this, new BackButtonBehavior
            {
                Command = new Command(async () => await Shell.Current.GoToAsync(".."))//,IconOverride = "over.jpg"
            });
        }

        private async void ToolbarItem_Clicked(object sender, EventArgs e)
        {
            await Shell.Current.GoToAsync($"//{nameof(LoginPage)}");
            //await Shell.Current.GoToAsync("..");
        }
    }
}