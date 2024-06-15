
using SASC_Final.Helpers;
using SASC_Final.Services;
using SASC_Final.Views;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using Microsoft.AppCenter;
using Microsoft.AppCenter.Analytics;
using Microsoft.AppCenter.Crashes;

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
            //DependencyService.Register<IDataStore,ItemsStorage>();
            var AppData = new AppData();
            DependencyService.RegisterSingleton(AppData);
            var x = DependencyService.Get<AppData>();

            DependencyService.Register<MockDataStore>();
            MainPage = new AppShell();
        }

        protected override void OnStart()
        {
            AppCenter.Start("android=783e3766-217c-4474-bc0c-21bb6947f049", typeof(Analytics), typeof(Crashes));
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
