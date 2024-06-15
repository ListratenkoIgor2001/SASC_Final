using RestSharp;

using SASC_Final.Helpers;
using SASC_Final.Models;
using SASC_Final.Models.Common.AuthModels;
using SASC_Final.Models.Common.AuthModels.Enums;

using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
//using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace SASC_Final.Services
{
    public class AuthService : IAuth
    {
        private string _baseUri = "http://sacs-auth.somee.com";
        
            /*
        public async Task<string> Login(string username, string password)
        {
            //var request = new RestRequest("auth/Account/Login", Method.Post);
                         //.AddHeader("Content-Type", "application/json;charset=utf-8")
                         //.AddHeader("Content-Length", 0)
                         //.AddQueryParameter("username", username).AddQueryParameter("password", password);
            var request = new RestRequest("Account/Login", Method.Post);
            request.RequestFormat = DataFormat.Json;
            //request.AddBody(new LoginModel { Username=username,Password=password});
            request.AddBody(new LoginModel { Username="g-danilova",Password="Test2Test2"});
            //request.AddBody(new LoginModel { Username="85100093",Password="Test1Test1"});
            var AppData = DependencyService.Get<AppData>();
            //var response = AppData.RestClient.PostAsync(request);
            var client = new RestClient(_baseUri);
            var response = client.ExecuteAsync(request);
            try
            {
                response.Wait();
                if (response.Result.IsSuccessful)
                {
                    string token = response.Result.Content;
                    if (token != null)
                    {
                        AppData.token = token;
                        var handler = new JwtSecurityTokenHandler();
                        var jwtSecurityToken = handler.ReadJwtToken(token);
                        var user = new PhysicalEntity()
                        {
                            FirstName = jwtSecurityToken.PayloadExist("FirstName").ToString(),
                            LastName = jwtSecurityToken.PayloadExist("LastName").ToString(),
                            MiddleName = jwtSecurityToken.PayloadExist("MiddleName").ToString(),
                            isStudent = (bool)jwtSecurityToken.PayloadExist("isStudent"),
                            RecordBookNumber = jwtSecurityToken.PayloadExist("RecordBookNumber")?.ToString(),
                            Group = jwtSecurityToken.PayloadExist("Group")?.ToString(),
                            UrlId = jwtSecurityToken.PayloadExist("UrlId")?.ToString()
                        };
                        AppData.Role = jwtSecurityToken.PayloadExist("role").ToString();
                        //AppData.Role = jwtSecurityToken.Claims.First(c => c.Type == ClaimTypes.Role).Value ;
                        AppData.User = user;
                    }
                    return string.Empty;
                }
                else
                {
                    return $"Error {response.Result.StatusCode}. {response.Result.ErrorMessage}\n{response.Result.Content}";
                }
            }
            catch { return $"Error {response.Result.StatusCode}. {response.Result.ErrorMessage}\n{response.Result.Content}"; }
        }
        */
        public async Task<string> Login(string username, string password)
        {
            
            TokenStorage.SetTokenAsync("   ").Wait();
            return null;
            //string token = "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJ1bmlxdWVfbmFtZSI6ImctZGFuaWxvdmEiLCJuYW1laWQiOiJnLWRhbmlsb3ZhIiwicm9sZSI6IkVtcGxveWVlIiwibmJmIjoxNjYxNzU1ODEwLCJleHAiOjE2NjE3OTE4MTAsImlhdCI6MTY2MTc1NTgxMCwiRmlyc3ROYW1lIjoi0JPQsNC70LjQvdCwIiwiTGFzdE5hbWUiOiLQlNCw0L3QuNC70L7QstCwIiwiTWlkZGxlTmFtZSI6ItCS0LvQsNC00LjQvNC40YDQvtCy0L3QsCIsImlzU3R1ZGVudCI6ZmFsc2UsIlVybElkIjoiZy1kYW5pbG92YSJ9.aXzbmoPon9QY0k1sYZshXz4UzbvTDqug8q6R0mcKGh4";
            /*
            var request = new RestRequest("Account/login", Method.Post);
            request.RequestFormat = DataFormat.Json;
            request.AddJsonBody(new LoginModel { Username = username, Password = password });
            var client = new RestClient(_baseUri);
            var response = client.ExecuteAsync(request);
            response.Wait();
                if (response.Result.IsSuccessful)
                {
                    //Convert.ToInt32(response.Result.Content);
                    string token = response.Result.Content;
                    if (token != null)
                    {
                        //AppData.Role = token;
                        TokenStorage.SetTokenAsync(token).Wait();
                        var handler = new JwtSecurityTokenHandler();
                        var jsonToken = handler.ReadToken(token) as JwtSecurityToken;
                        var user = new PhysicalEntity();
                        user.Id = Convert.ToInt32(jsonToken.Claims.First(c => c.Type == "user_id").Value);
                        AppData.Role = jsonToken.Claims.First(c => c.Type == ClaimTypes.Role).Value;
                        AppData.User = user;
                      
                      return string.Empty;
                    }
                    return "TokenIsNull";
                }
            */
            var AppData = DependencyService.Get<AppData>();
            var request = new RestRequest($"Account/login3/{username}/{password}", Method.Get);
            var client = new RestClient(_baseUri);
            var response = client.ExecuteAsync(request);
            try
            {
                response.Wait();
                if (response.Result.IsSuccessful)
                {
                    //Convert.ToInt32(response.Result.Content);
                    string token = response.Result.Content;
                    if (token != null)
                    {
                        //AppData.Role = token;
                        TokenStorage.SetTokenAsync(token).Wait();
                        var handler = new JwtSecurityTokenHandler();
                        var jsonToken = handler.ReadToken(token) as JwtSecurityToken;
                        var user = new PhysicalEntity();
                        user.Id = Convert.ToInt32(jsonToken.Claims.First(c => c.Type == "user_id").Value);
                        AppData.Role = jsonToken.Claims.First(c => c.Type == ClaimTypes.Role).Value;
                        AppData.User = user;
                        /*
                        var handler = new JwtSecurityTokenHandler();
                        var jwtSecurityToken = handler.ReadJwtToken(token);
                        var user = new PhysicalEntity();                      
                        user.Id = Convert.ToInt32(jwtSecurityToken.PayloadExist("user_id")?.ToString());
                        //AppData.Role = jwtSecurityToken.PayloadExist("role").ToString();
                        AppData.Role = jwtSecurityToken.Claims.First(c => c.Type == ClaimTypes.Role).Value ;
                        AppData.User = user;
                        */
                        return string.Empty;
                    }
                    return "TokenIsNull";
                }
                else
                {
                    return $"Error {response.Result.StatusCode}. {response.Result.ErrorMessage}\n{response.Result.Content}";
                }
            }
            catch 
            { 
                return $"Error {response.Result.StatusCode}. {response.Result.ErrorMessage}\n{response.Result.Content}"; 
            }
        }
             

        public void Logout()
        {
            var AppData = DependencyService.Get<AppData>();
            var token = TokenStorage.GetTokenAsync().Result;
            if(!string.IsNullOrEmpty(token))
            { 
                var request = new RestRequest("Account/Logout")
                //var request = new RestRequest("auth/Account/Logout")
                    .AddHeader("Authorization", $"Bearer {token}");
                AppData.RestClient.GetAsync(request);
            }
            AppData.Clear();
        }

        public async Task<string> Register(RegistrationModel model)
        {
            return null;
            var request = new RestRequest($"Account/Register", Method.Post);
            request.RequestFormat = DataFormat.Json;
            request.AddJsonBody(model);
            var client = new RestClient(_baseUri);
            var response = client.ExecuteAsync(request);
            try
            {
                response.Wait();
                if (response.Result.IsSuccessful)
                {
                    return null;
                }
                else
                {
                    return $"Error {response.Result.StatusCode}. {response.Result.ErrorMessage}\n{response.Result.Content}";
                }
            }
            catch
            {
                return $"Error {response.Result.StatusCode}. {response.Result.ErrorMessage}\n{response.Result.Content}";
            }
        }
    }  
}
