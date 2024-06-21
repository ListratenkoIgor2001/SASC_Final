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
        LoginViewModel vm;
        public LoginPage()
        {
            this.ToolbarItems.Clear();
            foreach (var page in Navigation.NavigationStack.ToList())
            {
                Navigation.RemovePage(page);
            }
            foreach (var page in Navigation.ModalStack.ToList())
            {
                Navigation.RemovePage(page);
            }
            InitializeComponent();
            vm = new LoginViewModel();
            vm.DisplayError += () => DisplayAlert("Error", vm.Error, "OK");
            vm.SuccessLogin += async () => {
                Application.Current.MainPage = new NavigationPage(new SchedulePage());
                await Navigation.PopToRootAsync();
            };
            this.BindingContext = vm;         
        }
        private async void TapGestureRecognizer_Tapped(object sender, EventArgs e)
        {
            //await Shell.Current.GoToAsync($"//{nameof(RegistrationPage)}");
            await Navigation.PushAsync(new RegistrationPage());
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            //vm.OnAppearing();
        }
    }
}