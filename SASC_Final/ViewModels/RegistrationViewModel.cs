using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Windows.Input;

using Microsoft.AppCenter.Crashes;

using SASC_Final.Models.Common.AuthModels;
using SASC_Final.Models.Common.AuthModels.Enums;
using SASC_Final.Services;
using SASC_Final.ViewModels.Base;

using Xamarin.Forms;

namespace SASC_Final.ViewModels
{
    public class RegistrationViewModel : BaseViewModel
    {
        private string email;
        public string Email
        {
            get => email;
            set => SetProperty(ref email, value);
        }

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

        private string role;
        public string Role
        {
            get => role;
            set => SetProperty(ref role, value);
        }

        private string firstName;
        public string FirstName
        {
            get => firstName;
            set => SetProperty(ref firstName, value);
        }

        private string lastName;
        public string LastName
        {
            get => lastName;
            set => SetProperty(ref lastName, value);
        }

        private string middleName;
        public string MiddleName
        {
            get => middleName;
            set => SetProperty(ref middleName, value);
        }

        private string recordBookNumber;
        public string RecordBookNumber
        {
            get => recordBookNumber;
            set => SetProperty(ref recordBookNumber, value);
        }

        private string error;
        public string Error
        {
            get => error;
            set => SetProperty(ref error, value);
        }

        public ICommand RegisterCommand { get; }

        public Action DisplayError;
        public Action SuccessRegister;

        public RegistrationViewModel()
        {
            Role = UserRoles.STUDENT;            
            RegisterCommand = new Command(OnRegister);
        }

        private async void OnRegister()
        {
            try
            {
                IsBusy = true;

                var service = DependencyService.Get<IAuth>();
                var newUser = new RegistrationModel
                {
                    Email = Email,
                    Username = Username,
                    Password = Password,
                    Role = Role,
                    FirstName = FirstName,
                    LastName = LastName,
                    MiddleName = MiddleName,
                    RecordBookNumber = RecordBookNumber
                };
                var result = service.Register(newUser);
                if (!string.IsNullOrEmpty(result.Result))
                {
                    Error = result.Result;
                    DisplayError();
                }
                else
                {
                    SuccessRegister();
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
        }
    }
}