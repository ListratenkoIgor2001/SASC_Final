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
            TokenStorage.RemoveToken();
            SyncWithToken();
            CurrentLessons = new ItemsStorage<Lesson>();
            CurrentAttendances = new ItemsStorage<AttendanceStudentViewModel>();
            CurrentStudents = new ItemsStorage<StudentInfo>();
        }
        public void Clear()
        {
            User = null;
            Role = null;
            TokenStorage.RemoveToken();
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
                if (this.User == null || !this.User.IsSynced)
                {
                    /*
                    var handler = new JwtSecurityTokenHandler();
                    var jwtSecurityToken = handler.ReadJwtToken(token);
                    var user = new PhysicalEntity();
                    user.Id = Convert.ToInt32(jwtSecurityToken.PayloadExist("user_id")?.ToString());
                    this.Role = jwtSecurityToken.Claims.First(c => c.Type == ClaimTypes.Role).Value;
                    this.User = user;
                    */
                    var dataService = DependencyService.Get<IData>();
                    if (this.Role == UserRoles.STUDENT)
                    {
                        var res1 = dataService.GetStudent(this.User.Id);
                        this.User = new Models.PhysicalEntity(res1.Result);
                    }
                    else
                    {
                        var res2 = dataService.GetEmployee(this.User.Id);
                        this.User = new Models.PhysicalEntity(res2.Result);
                    }
                }
                return true;
                /*
                //AppData.token = token;
                var handler = new JwtSecurityTokenHandler();
                var jwtSecurityToken = handler.ReadJwtToken(token);
                var user = new PhysicalEntity()
                {
                    FirstName = jwtSecurityToken.PayloadExist("FirstName").ToString(),
                    LastName = jwtSecurityToken.PayloadExist("LastName").ToString(),
                    MiddleName = jwtSecurityToken.PayloadExist("MiddleName").ToString(),
                    //isStudent = (bool)jwtSecurityToken.PayloadExist("isStudent"),
                    RecordBookNumber = jwtSecurityToken.PayloadExist("RecordBookNumber")?.ToString(),
                    Group = jwtSecurityToken.PayloadExist("Group")?.ToString(),
                    UrlId = jwtSecurityToken.PayloadExist("UrlId")?.ToString()
                };
                Role = jwtSecurityToken.PayloadExist("role").ToString();
                Role2 = jwtSecurityToken.Claims.First(c => c.Type == ClaimTypes.Role).Value;
                //user.UrlId = "s-kulikov";
                User = user;
                */
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
