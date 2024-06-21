using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using SASC_Final;
using RestSharp;
using Xamarin.Forms;
using Newtonsoft.Json;
//using Nancy.Json;
using SASC_Final.Models.Common;
using SASC_Final.Models.Common.DTOs;
using SASC_Final.Helpers;

namespace SASC_Final.Services
{
    public class DataService : IData
    {
        private string _apiTemplate = "data/";

        public async Task<EmployeeDto> GetEmployee(int id)
        {
            RestRequest request;
            var AppData = DependencyService.Get<AppData>();
            var client = AppData.RestClient;
            request = new RestRequest(_apiTemplate + $"Employees/id/{id}");
            request.AddHeader("Authorization", $"Bearer {await TokenStorage.GetTokenAsync()}");
            var response = client.GetAsync(request);
            response.Wait();
            if (response.Result.IsSuccessful)
            {
                var result = JsonConvert.DeserializeObject<EmployeeDto>(response.Result.Content);
                return result;
            }
            return null;
        }
        public async Task<StudentDto> GetStudent(int id)
        {
            RestRequest request;
            var AppData = DependencyService.Get<AppData>();
            var client = AppData.RestClient;
            request = new RestRequest(_apiTemplate + $"Students/id/{id}");
            request.AddHeader("Authorization", $"Bearer {await TokenStorage.GetTokenAsync()}");
            var response = client.GetAsync(request);
            response.Wait();
            if (response.Result.IsSuccessful)
            {
                var result = JsonConvert.DeserializeObject<StudentDto>(response.Result.Content);
                return result;
            }
            return null;
        }

        public async Task<StudentDto> GetStudentByGuid(string id)
        {
            RestRequest request;
            var AppData = DependencyService.Get<AppData>();
            var client = AppData.RestClient;
            request = new RestRequest(_apiTemplate + $"Students/Guid/{id}");
            request.AddHeader("Authorization", $"Bearer {await TokenStorage.GetTokenAsync()}");
            var response = client.GetAsync(request);
            response.Wait();
            if (response.Result.IsSuccessful)
            {
                var result = JsonConvert.DeserializeObject<StudentDto>(response.Result.Content);
                return result;
            }
            return null;
        }

        public async Task<EmployeeDto> GetEmployeeByGuid(string id)
        {
            RestRequest request;
            var AppData = DependencyService.Get<AppData>();
            var client = AppData.RestClient;
            request = new RestRequest(_apiTemplate + $"Employees/Guid/{id}");
            request.AddHeader("Authorization", $"Bearer {await TokenStorage.GetTokenAsync()}");
            var response = client.GetAsync(request);
            response.Wait();
            if (response.Result.IsSuccessful)
            {
                Console.WriteLine(response.Result.Content);
                var result = JsonConvert.DeserializeObject<EmployeeDto>(response.Result.Content);
                return result;
            }
            return null;
        }

        public async Task<List<StudentDto>> GetStudentsByPlannedLesson(int id)
        {
            RestRequest request;
            var AppData = DependencyService.Get<AppData>();
            var client = AppData.RestClient;
            request = new RestRequest(_apiTemplate + $"Students/PlannedLesson/{id}");
            request.AddHeader("Authorization", $"Bearer {await TokenStorage.GetTokenAsync()}");
            var response = await client.GetAsync(request);
            if (response.IsSuccessful)
            {
                Console.WriteLine(response.Content);
                var result = JsonConvert.DeserializeObject<List<StudentDto>>(response.Content);
                return result;
            }
            return null;
        }
        /*
        public async Task<List<StudentDto>> GetStudentsByGroup(string groupsNumber)
        {
            var AppData = DependencyService.Get<AppData>();
            var client = AppData.RestClient;
            RestRequest request;
            request = new RestRequest(_apiTemplate + $"Students/group/");
            request.AddQueryParameter("groupsNumber", groupsNumber);
            request.AddHeader("Authorization", $"Bearer {TokenStorage.GetTokenAsync().Result}");
            var response = await client.GetAsync(request);
            if (response.IsSuccessful)
            {
                Console.WriteLine(response.Content);
                var result = JsonConvert.DeserializeObject<List<StudentDto>>(response.Content);
                return result;
            }
            return null;
        }


        public async Task<StudentsStream> GetStudentsByGroups(List<string> groupsNumbers)
        {
            var AppData = DependencyService.Get<AppData>();
            var client = AppData.RestClient;
            RestRequest request;
            var groups = JsonConvert.SerializeObject(groupsNumbers);
            request = new RestRequest(_apiTemplate + $"Students/stream/{groups}");
            request.AddHeader("Authorization", $"Bearer {TokenStorage.GetTokenAsync().Result}");

            try
            {
                var response = await client.GetAsync(request);
                if (response.IsSuccessful)
                {
                    var result = JsonConvert.DeserializeObject<StudentsStream>(response.Content);
                    return result;
                }
                else
                {
                    throw new Exception(response.ErrorMessage);
                }
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        */
    }
}
