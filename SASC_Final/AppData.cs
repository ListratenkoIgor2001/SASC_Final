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
        public PhysicalEntity User;
        public readonly RestClient RestClient;
        public string BaseUri;
        public string Role;
        public int CurrentWeek { get => GetCurrentWeek(); }
        public ItemsStorage<Lesson> CurrentLessons;
        public Lesson SelectedLesson;
        public ItemsStorage<AttendanceStudentViewModel> CurrentAttendances;
        public ItemsStorage<StudentInfo> CurrentStudents;
        public AppData()
        {
            BaseUri = "http://sacs-gateway.somee.com";
            Role = null;
            User = null;
            RestClient = new RestClient(BaseUri);
            //TokenStorage.RemoveToken();
            //SyncWithToken().Wait();
            CurrentLessons = new ItemsStorage<Lesson>();
            CurrentAttendances = new ItemsStorage<AttendanceStudentViewModel>();
            CurrentStudents = new ItemsStorage<StudentInfo>();
        }
        public void Clear()
        {
            User = null;
            Role = null;
            TokenStorage.RemoveToken();
            DependencyService.Get<ILocalStore<PhysicalEntity>>().DeleteData("User");
            ClearContext();
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
    public class AppDataToken
    {
        PhysicalEntity User;
        DateTime nbf;
        DateTime exp;
        DateTime iat;
        public AppDataToken(PhysicalEntity entity)
        {
            User = entity;
            iat = DateTime.Now;
            nbf = iat;
            exp = iat.AddMinutes(5);
        }
    }
    public static class AppDataTokenExtentions
    {
        public static string EncodeToken(this AppData appData)
        {
            return EncodeDecrypt(JsonConvert.SerializeObject(new AppDataToken(appData.User))).ToString();
        }
        public static AppDataToken DecodeToken(this string token)
        {
            return JsonConvert.DeserializeObject<AppDataToken>(EncodeDecrypt(token));
        }
        public static string EncodeDecrypt(string str)
        {
            ushort secretKey = 0x0088; // Секретный ключ (длина - 16 bit).
            var ch = str.ToArray(); //преобразуем строку в символы
            string newStr = "";      //переменная которая будет содержать зашифрованную строку
            foreach (var c in ch)  //выбираем каждый элемент из массива символов нашей строки
                newStr += TopSecret(c, secretKey);  //производим шифрование каждого отдельного элемента и сохраняем его в строку
            return newStr;
        }

        public static char TopSecret(char character, ushort secretKey)
        {
            character = (char)(character ^ secretKey);
            return character;
        }


    }

}
