using System;
using System.Collections.Generic;
using System.Text;

using SASC_Final.Views;

using Xamarin.Forms;
using System.Windows.Input;

using Microsoft.AppCenter.Crashes;

using SASC_Final.Helpers;
using SASC_Final.Services;
using SASC_Final.ViewModels.Base;
using SASC_Final.Models.Common.AuthModels.Enums;
using SASC_Final.Models;
using System.Diagnostics;
using SASC_Final.Models.Common.AuthModels;
using SASC_Final.Services.Interfaces;

namespace SASC_Final.ViewModels
{
    public class LoginViewModel : BaseViewModel
    {
        public Command LoginCommand { get; }

        public Action DisplayError;
        public Action SuccessLogin;
        public Action GoToRegister;
        private string username;
        public string Username
        {
            get => username;
            set => SetProperty(ref username, value);
        }
        private string password;
        public string Password
        {
            get => password;
            set => SetProperty(ref password, value);
        }
        private string error;
        public string Error
        {
            get => error;
            set => SetProperty(ref error, value);
        }

        public LoginViewModel()
        {
            username = "t-test1";
            //username = "052100155";
            //username = "052100141";
            password = "1100111001";
            LoginCommand = new Command(OnLoginClicked);
        }

        private async void OnLoginClicked(object obj)
        {
            /*
            var AppData = DependencyService.Get<AppData>();
            var userx = new PhysicalEntity("");
            if (Username == "t-test1")
            {
                userx.Id = 1;
                AppData.User = userx;
                AppData.Role = UserRoles.EMPLOYEE;
            }
            else
            {
                userx.Id = 964;
                AppData.User = userx;
                AppData.Role = UserRoles.STUDENT;
            }
            */
            try
            {
                var AppData = DependencyService.Get<AppData>();
                var authService = DependencyService.Get<IAuth>();
                var storeService = DependencyService.Get<ILocalStore<PhysicalEntity>>();
                storeService.DeleteData("User");

                var authResult = await authService.Login(new LoginModel
                {
                    Username = username,
                    Password = password
                });

                if (string.IsNullOrEmpty(authResult) && AppData.SyncWithToken())
                {
                    SuccessLogin();
                }
                else
                {
                    Error = authResult;
                    DisplayError();
                }

            }
            catch (Exception ex)
            {
                Crashes.TrackError(ex);
                Error = ex.Message;
                DisplayError();
                Debug.WriteLine(ex);
            }
            finally
            {
                IsBusy = false;
            }
            /*
            AppData.Role = "Student";
            AppData.User = new SASC_Final.Models.PhysicalEntity();
            AppData.User.Id = 970;

            AppData.Role = "Employee";
            AppData.User = new SASC_Final.Models.PhysicalEntity();
            AppData.User.Id = 1;
            */
            //проверить токен


        }
        private async void LoadEntity()
        {
            var AppData = DependencyService.Get<AppData>();
            var dataService = DependencyService.Get<DataService>();
            if (AppData.Role == UserRoles.STUDENT)
            {
                var res1 = dataService.GetStudent(AppData.User.Id);
                AppData.User = new SASC_Final.Models.PhysicalEntity(res1.Result);
            }
            else
            {
                var res2 = dataService.GetEmployee(AppData.User.Id);
                AppData.User = new SASC_Final.Models.PhysicalEntity(res2.Result);
            }
        }
    }
}
