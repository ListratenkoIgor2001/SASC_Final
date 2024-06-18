using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using RestSharp;
using Xamarin.Forms;
using Newtonsoft.Json;
using SASC_Final.Models.Common.IisApi;
using SASC_Final.Models;
using SASC_Final.Models.Common;
using Microsoft.AppCenter.Crashes;
using SASC_Final.Models.Common.DTOs;
using SASC_Final.Helpers;

namespace SASC_Final.Services
{
    public class ScheduleService : ISchedule
    {
        //private string _baseUri = "http://sacs-data.somee.com";
        private string _apiTemplate = "schedule/";

        public async Task<int> GetCurrentWeek()
        {
            return 2;
            var AppData = DependencyService.Get<AppData>();
            var client = AppData.RestClient;
            RestRequest request;
            request = new RestRequest(_apiTemplate+$"Schedule/CurrentWeek");
            request.AddHeader("Authorization", $"Bearer {await TokenStorage.GetTokenAsync()}");
            try
            {
                var response = client.GetAsync(request);
                response.Wait();
                if (response.Result.IsSuccessful)
                {
                    int result = JsonConvert.DeserializeObject<int>(response.Result.Content);
                    return result;
                }
                else
                {
                    throw new Exception(response.Result.ErrorMessage);
                }
            }
            catch(Exception ex)
            {
                Crashes.TrackError(ex);
                throw;
            }
            return 0;
        }

        public async Task<List<PlannedLessonDto>> LoadSchedule(string date = null)
        {
            RestRequest request;
            var AppData = DependencyService.Get<AppData>();
            var client = AppData.RestClient;

            if (AppData.Role == "Student")
            {
                request = new RestRequest(_apiTemplate + $"Schedule/Student/{AppData.User.Id}"
                    + (string.IsNullOrWhiteSpace(date) ? "" : $"/{date}"));
            }
            else
            {
                request = new RestRequest(_apiTemplate + $"Schedule/Employee/{AppData.User.Id}"
                    + (string.IsNullOrWhiteSpace(date) ? "" : $"/{date}"));
            }
            request.AddHeader("Authorization", $"Bearer {await TokenStorage.GetTokenAsync()}");
            try
            {
                var response = client.GetAsync(request);
                response.Wait();
                if (response.Result.IsSuccessful)
                {
                    Console.WriteLine(response.Result.Content);
                    var result = JsonConvert.DeserializeObject<List<PlannedLessonDto>>(response.Result.Content);
                    return result;
                }
                else
                {
                    throw new Exception(response.Result.ErrorMessage);
                }
            }
            catch (Exception ex)
            {
                throw;
            }
            return null;
        }
      
        /*
        public async Task<List<DayShedule>> LoadScheduleIIS()
        {
            var AppData = DependencyService.Get<AppData>();
            user = AppData.User;
            RestRequest request;
            if (AppData.Role=="Student")
            {
                request = new RestRequest($"StudentsGroups/{user.Group}");
                //request = new RestRequest($"Schedule/StudentsGroups/{user.Group}");
            }
            else
            {
                request = new RestRequest($"Employees/{user.UrlId}");
                //request = new RestRequest($"Schedule/Employees/{user.UrlId}");
            }
            try
            {
                
                var client = new RestClient(_baseUri);
                var response = client.GetAsync(request);
                response.Wait();
                //RestResponse response = await AppData.RestClient.GetAsync(request);
                if (response.Result.IsSuccessful)
                {
                    ScheduleResponseDto result = JsonConvert.DeserializeObject<ScheduleResponseDto>(response.Result.Content);
                    return result.GetWeekShedule();
                }
                else
                {
                    throw new Exception(response.Result.ErrorMessage);
                }
                
            }
            catch
            {
                throw;
            }
            return null;
        }
        */
    }
}
