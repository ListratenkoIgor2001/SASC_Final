using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SASC_Final.Models.Common.IisApi;
using SASC_Final.Models;
using SASC_Final.Helpers;
using SASC_Final.Services;

using Xamarin.Forms;
using Newtonsoft.Json;
using RestSharp;
//using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using SASC_Final.ViewModels;
using SASC_Final.Models.Common.AuthModels.Enums;
using System.IdentityModel.Tokens.Jwt;
using System.Threading.Tasks;
using Microsoft.AppCenter.Crashes;
using System.Diagnostics;
using SASC_Final.Services.Interfaces;

namespace SASC_Final
{
    public class AppData
    {
        #region User Info
        public PhysicalEntity User;
        public string Role;
        #endregion

        #region AppData
        public Settings Settings = new Settings();
        public readonly RestClient RestClient;
        public string BaseUri;
        public string PreviousPage;
        #endregion

        private int week = -1;
        public int CurrentWeek { get => week <= 0 ? week = GetCurrentWeek() : week; }

        public ItemsStorage<Lesson> CurrentLessons;
        public ItemsStorage<AttendanceStudentViewModel> CurrentAttendances;
        public ItemsStorage<StudentInfo> CurrentStudents;
        public AppData()
        {
            Role = null;
            User = null;
            BaseUri = "http://sacs-gateway.somee.com";
            RestClient = new RestClient(BaseUri);
            var settings = DependencyService.Get<ILocalStore<Settings>>().LoadData();
            if (settings != null) { Settings = settings; }
            //TokenStorage.RemoveToken();
            //Clear(); 
            CurrentLessons = new ItemsStorage<Lesson>(useLocalStorage: false);
            CurrentAttendances = new ItemsStorage<AttendanceStudentViewModel>(useLocalStorage: true);
            CurrentStudents = new ItemsStorage<StudentInfo>(useLocalStorage:false);
        }
        public void Clear()
        {
            User = null;
            Role = null;
            TokenStorage.RemoveToken();
            DependencyService.Get<ILocalStore<PhysicalEntity>>().DeleteData("User");
            ClearContext();
            Application.Current.SavePropertiesAsync();
        }
        public void ClearContext()
        {
            CurrentLessons.Clear();
            CurrentAttendances.Clear();
            CurrentStudents.Clear();
        }
        private int GetCurrentWeek()
        {
            return DependencyService.Get<ISchedule>().GetCurrentWeek().Result;
        }

        public bool FastSyncWithToken()
        {
            try
            {
                var token = TokenStorage.GetTokenAsync().Result;
                if (!string.IsNullOrEmpty(token))
                {
                    var claims = JwtHelper.GetClaims(token);
                    User = new PhysicalEntity(claims["CorrelationId"]);
                    Role = claims[ClaimTypes.Role];
                    var storeService = DependencyService.Get<ILocalStore<PhysicalEntity>>();
                    var user = storeService.LoadData("User");
                    if (user != null)
                    {
                        User = user;
                        return true;
                    }
                }
            }
            finally { }
            return false;
        }
        public bool SyncWithToken() 
        {
            var token = TokenStorage.GetTokenAsync().Result;
            if (!string.IsNullOrEmpty(token))
            {
                if (!JwtHelper.TokenIsValid(token))
                {
                    TokenStorage.RemoveToken();
                    return false;
                }
                if (User == null || !User.IsSynced)
                {
                    Dictionary<string, string> claims = null;
                    try
                    {
                        claims = JwtHelper.GetClaims(token);
                        if (claims == null || claims.Count == 0)
                        {
                            TokenStorage.RemoveToken();
                            return false;
                        }
                        User = new PhysicalEntity(claims["CorrelationId"]);
                        Role = claims[ClaimTypes.Role];
                    }
                    catch (Exception ex)
                    {
                        Crashes.TrackError(ex);
                        Debug.WriteLine(ex);
                    }
                    var storeService = DependencyService.Get<ILocalStore<PhysicalEntity>>();
                    var user = storeService.LoadData("User");
                    if (user == null)
                    {
                        var dataService = DependencyService.Get<IData>();
                        if (Role == UserRoles.STUDENT)
                        {
                            var result = dataService.GetStudentByGuid(User.Guid).Result;
                            if (result != null)
                            {
                                User = new PhysicalEntity(result);
                            }
                            else
                            {
                                TokenStorage.RemoveToken();
                                return false;
                            }
                        }
                        else
                        {
                            var result = dataService.GetEmployeeByGuid(User.Guid).Result;
                            if (result != null)
                            {
                                User = new PhysicalEntity(result);
                            }
                            else
                            {
                                return false;
                            }
                        }
                        storeService.SaveData(User, "User");
                        Application.Current.SavePropertiesAsync();
                    }
                    else 
                    {
                        User = user;
                    }
                }
                return true;
            }
            return false;
        }
    }    
}
