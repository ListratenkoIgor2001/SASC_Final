using Newtonsoft.Json.Linq;
using Newtonsoft.Json;

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
using System.Net.Http;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using System.Net;

namespace SASC_Final.Services
{
    public class AuthService : IAuth
    {
        private string _apiTemplate = "auth/";
        private AppData AppData = DependencyService.Get<AppData>();
        public async Task<string> Login(LoginModel loginModel)
        {
            var client = AppData.RestClient;
            RestResponse response = new RestResponse();
            var request = new RestRequest(_apiTemplate + $"Account/SignIn", Method.Post);
            request.AddHeader("Content-Type", "application/json");
            request.AddJsonBody(loginModel);
            try
            {
                response = await client.ExecuteAsync(request);
                if (response.IsSuccessful)
                {
                    var result = JObject.Parse(response.Content);
                    var token = result["token"].ToString();
                    await TokenStorage.SetTokenAsync(token);
                    return null;
                }
                else
                {
                    if (response.StatusCode == HttpStatusCode.Unauthorized)
                    {
                        return response.Content;
                    }
                    return $"Error {response.StatusCode}. {response.ErrorMessage}\n{response.Content}";
                }
            }
            catch
            {
                return $"Error {response.StatusCode}. {response.ErrorMessage}\n{response.Content}";
            }
        }

        public async Task<string> Register(RegistrationModel model)
        {
            var client = AppData.RestClient;
            RestResponse response = new RestResponse();
            var request = new RestRequest(_apiTemplate + $"Account/SignUp", Method.Post);
            request.AddHeader("Content-Type", "application/json");
            request.AddJsonBody(model);
            try
            {
                response = await client.ExecuteAsync(request);

                if (response.IsSuccessful)
                {
                    return null;
                }
                else
                {
                    return $"Error {response.StatusCode}. {response.ErrorMessage}\n{response.Content}";
                }
            }
            catch
            {
                return $"Error {response.StatusCode}. {response.ErrorMessage}\n{response.Content}";
            }
        }

        public async Task Logout()
        {
            try
            {
                var client = AppData.RestClient;
                var token = await TokenStorage.GetTokenAsync();
                if (!string.IsNullOrEmpty(token))
                {
                    var request = new RestRequest(_apiTemplate + "Account/SignOut");
                    request.AddHeader("Authorization", $"Bearer {token}");
                    client.PostAsync(request);
                }
            }
            finally 
            { 
            }
        }

    }
}
