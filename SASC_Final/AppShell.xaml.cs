using System;
using System.Collections.Generic;

using SASC_Final.Helpers;
using SASC_Final.Services;
using SASC_Final.ViewModels;
using SASC_Final.Views;

using Xamarin.Forms;

namespace SASC_Final
{
    public partial class AppShell : Xamarin.Forms.Shell
    {
        public AppShell()
        {
            InitializeComponent();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
        }

        private async void OnMenuItemClicked(object sender, EventArgs e)
        {
            var AppData = DependencyService.Get<AppData>();
            var auth = DependencyService.Get<IAuth>();
            await auth.Logout();
            AppData.Clear();
            await Shell.Current.GoToAsync($"//{nameof(LoginPage)}");
        }
    }
}
