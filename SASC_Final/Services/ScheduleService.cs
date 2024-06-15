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

namespace SASC_Final.Services
{
    public class ScheduleService : ISchedule
    {
        private string _baseUri = "http://sacs-data.somee.com";

        public async Task<int> GetCurrentWeek()
        {
            return 2;
            var AppData = DependencyService.Get<AppData>();
            RestRequest request;
            request = new RestRequest($"Schedule/CurrentWeek");
            try
            {
                var client = new RestClient(_baseUri);
                var response1 = client.GetAsync(request);
                response1.Wait();
                var response = response1.Result;
                //RestResponse response = await AppData.RestClient.GetAsync(request);
                if (response.IsSuccessful)
                {
                    int result = JsonConvert.DeserializeObject<int>(response.Content);
                    return result;
                }
                else
                {
                    throw new Exception(response.ErrorMessage);
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
            var AppData = DependencyService.Get<AppData>();
            RestRequest request;

            if (AppData.Role == "Student")
            {
                request = new RestRequest($"Schedule/Student/{AppData.User.Id}");
                //request = new RestRequest($"Schedule/StudentsGroups/{user.Group}");
            }
            else
            {
                request = new RestRequest($"Schedule/Employee/{AppData.User.Id}");
                //request = new RestRequest($"Schedule/Employees/{user.UrlId}");
            }
            //request.AddHeader("Authorization", $"Bearer {TokenStorage.GetTokenAsync().Result}");
            try
            {

                var client = new RestClient(_baseUri);
                var response = client.GetAsync(request);
                response.Wait();
                //RestResponse response = await AppData.RestClient.GetAsync(request);
                if (response.Result.IsSuccessful)
                {
                    Console.WriteLine(response.Result.Content);
                    var result = JsonConvert.DeserializeObject<List<PlannedLessonDto>>(response.Result.Content);
                    return result;
                    //ScheduleResponseDto result = JsonConvert.DeserializeObject<ScheduleResponseDto>(response.Result.Content);
                    //return result.GetWeekShedule();
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

        public async Task<List<PlannedLessonDto>> LoadSchedule(PhysicalEntity user, string date = null)
        {
            var AppData = DependencyService.Get<AppData>();
            RestRequest request;
            if (AppData.Role == "Student")
            {
                request = new RestRequest($"Schedule/Student/{user.Id}/6.11.2024");
                //request = new RestRequest($"Schedule/StudentsGroups/{user.Group}");
            }
            else
            {
                request = new RestRequest($"Schedule/Employee/{user.Id}/6.11.2024");
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
                    var result = JsonConvert.DeserializeObject<List<PlannedLessonDto>>(response.Result.Content);
                    return result;
                    //ScheduleResponseDto result = JsonConvert.DeserializeObject<ScheduleResponseDto>(response.Result.Content);
                    //return result.GetWeekShedule();
                }
                else
                {
                    throw new Exception(response.Result.ErrorMessage);
                }

            }
            catch (Exception ex)
            {
                Crashes.TrackError(ex);
                throw;
            }
            return null;
        }
        /*
        public async Task<List<DayShedule>> LoadScheduleIIS(PhysicalEntity user)
        {
            var AppData = DependencyService.Get<AppData>();
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
