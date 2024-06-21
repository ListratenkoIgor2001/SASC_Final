using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using SASC_Final.Helpers;
using SASC_Final.Services;
using SASC_Final.ViewModels;
using SASC_Final.Views;

using Xamarin.Forms;

namespace SASC_Final
{
    public partial class AppShell : Xamarin.Forms.Shell
    {
        private AppData AppData = DependencyService.Get<AppData>();

        private Stack<ShellNavigationState> Uri { get; set; } // Navigation stack.  
        private ShellNavigationState temp; // Prevents applications from adding redundant data to the stack when the back button is clicked. 

        public AppShell()
        {
            InitializeComponent();
            //Routing.RegisterRoute("LessonPage", typeof(LessonPage));
            //Routing.RegisterRoute("SchedulePage", typeof(SchedulePage));
            Uri = new Stack<ShellNavigationState>();
        }

        public void GoBack() => OnBackButtonPressed();
        protected override void OnNavigated(ShellNavigatedEventArgs args)
        {
            base.OnNavigated(args);
            if (Uri != null && args.Previous != null)
            {
                if (temp == null || temp != args.Previous)
                {
                    AppData.PreviousPage = args.Previous.Location.ToString();
                    Uri.Push(args.Previous);
                    temp = args.Current;
                }
            }
        }

        protected override bool OnBackButtonPressed()
        {
            if (Uri.Count > 0)
            {
                Shell.Current.GoToAsync(Uri.Pop());
                return true;
            }
            else
            {
                return false;
            }
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
