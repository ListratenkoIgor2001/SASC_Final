using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using SASC_Final.Services;
using SASC_Final.ViewModels;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace SASC_Final.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SettingsPage : ContentPage
    {
        public SettingsPage()
        {
            InitializeComponent();
            BindingContext = new SettingsViewModel();
        }

        private async void LogoutButton_Clicked(object sender, EventArgs e)
        {
            bool answer = await DisplayAlert("Подтверждение", "Выйти из аккаунта?", "Да", "Нет");
            if (answer)
            {
                var AppData = DependencyService.Get<AppData>();
                DependencyService.Get<IAuth>().Logout();
                AppData.Clear();
                Application.Current.SavePropertiesAsync();
                await Navigation.PopModalAsync();
                Application.Current.MainPage = new NavigationPage(new LoginPage());
                //await Navigation.PopToRootAsync();
            }
        }
    }
}