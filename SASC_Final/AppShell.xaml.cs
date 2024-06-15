using System;
using System.Collections.Generic;

using SASC_Final.Helpers;
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
            //Routing.RegisterRoute(nameof(LessonPage), typeof(LessonPage));
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            Shell.Current.GoToAsync("//LoginPage").Wait();
        }
        private async void OnMenuItemClicked(object sender, EventArgs e)
        {
            var AppData = DependencyService.Get<AppData>();
            AppData.Clear();
            await Shell.Current.GoToAsync("//LoginPage");
        }
    }
}
