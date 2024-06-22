using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

using SASC_Final.Models;
using SASC_Final.ViewModels.Base;

using Xamarin.Forms;

namespace SASC_Final.ViewModels
{
    public class SettingsViewModel : BaseViewModel
    {
        private AppData AppData = DependencyService.Get<AppData>();

        private PhysicalEntity _user;

        public string FirstName
        {
            get => _user.FirstName;
        }

        public string LastName
        {
            get => _user.LastName;
        }

        public string MiddleName
        {
            get => _user.MiddleName;
        }

        public string ImageUrl
        {
            get => _user.ImageUrl;
        }

        public string RecordBookNumber
        {
            get => _user.RecordBookNumber;
        }

        public string Group
        {
            get => _user.Group;
        }

        public string Degree
        {
            get => _user.Degree;
        }

        public string Rank
        {
            get => _user.Rank;
        }

        private bool _useFrontCamera;
        public bool UseFrontCamera
        {
            get => AppData.Settings.UseFrontCamera;
            set 
            {
                OnSettingsChange(value);
            }

        }

        private void OnSettingsChange(bool newValue)
        {
            AppData.Settings.UseFrontCamera = newValue;
            AppData.Settings.SaveAsync();
        }

        public bool IsStudent { get; set; }
        public bool IsEmployee { get; set; }

        public ObservableCollection<KeyValuePair<string, string>> StudentFields { get; set; }
        public ObservableCollection<KeyValuePair<string, string>> EmployeeFields { get; set; }

        public SettingsViewModel() {
            var AppData = DependencyService.Get<AppData>();
            _user = AppData.User;
            IsStudent = AppData.Role == "Student";
            IsEmployee = AppData.Role == "Employee";
            StudentFields = new ObservableCollection<KeyValuePair<string, string>>
            {
                new KeyValuePair<string, string>("Group Number", Group),
                new KeyValuePair<string, string>("Record Book Number", RecordBookNumber)
            };

            EmployeeFields = new ObservableCollection<KeyValuePair<string, string>>
            {
                new KeyValuePair<string, string>("Degree", Degree),
                new KeyValuePair<string, string>("Rank", Rank)
            };
        }
    }
}
