
using SASC_Final.Helpers;
using SASC_Final.Services;
using SASC_Final.Views;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using Microsoft.AppCenter;
using Microsoft.AppCenter.Analytics;
using Microsoft.AppCenter.Crashes;
using SASC_Final.Services.Interfaces;
using System.Collections.Generic;
using SASC_Final.Models;

namespace SASC_Final
{
    public partial class App : Application
    {

        public App()
        {
            InitializeComponent();
            DependencyService.Register<AuthService>();
            DependencyService.Register<ScheduleService>();
            DependencyService.Register<DataService>();
            DependencyService.Register<AttendanceService>();
            DependencyService.Register<CryptografyService>();
            DependencyService.Register<ILocalStore<PhysicalEntity>, LocalStore<PhysicalEntity>>();
            DependencyService.Register<ILocalStore<Settings>, LocalStore<Settings>>();
            //Application.Current.Properties.Clear();
            //Application.Current.SavePropertiesAsync();
            var settings = new Settings();
            settings.UseFrontCamera = true;
            new LocalStore<Settings>().SaveData(settings);
            Application.Current.SavePropertiesAsync();
            var AppData = new AppData();
            DependencyService.RegisterSingleton(AppData);
            
            //MainPage = new AppShell();
            //Shell.Current.GoToAsync($"//{nameof(LoginPage)}");

    
            var sync = AppData.SyncWithToken();
            if (sync)
            {
                //MainPage = new NavigationPage(new LoginPage());
                MainPage = new NavigationPage(new SchedulePage());
                //MainPage = new AppShell();
                //Shell.Current.GoToAsync($"//{nameof(SchedulePage)}");
            }
            else
            {
                MainPage = new NavigationPage(new LoginPage());
                //MainPage = new AppShell();
                //Shell.Current.GoToAsync($"//{nameof(LoginPage)}");
            }
        }

        protected override void OnStart()
        {
            AppCenter.Start("android=783e3766-217c-4474-bc0c-21bb6947f049", typeof(Analytics), typeof(Crashes));
        }

        protected override void OnSleep()
        {
            Application.Current.SavePropertiesAsync();
        }

        protected override void OnResume()
        {
        }
    }
}
