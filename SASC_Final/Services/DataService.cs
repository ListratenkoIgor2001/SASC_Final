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
        private string _baseUri = "http://sacs-data.somee.com";
        public async Task<List<StudentDto>> GetStudentsByGroup(string groupsNumber)
        {
            var AppData = DependencyService.Get<AppData>();
            RestRequest request;
            request = new RestRequest($"Students/group/");
            //request = new RestRequest($"data/Students/group/");
            request.AddQueryParameter("groupsNumber", groupsNumber);
            request.AddHeader("Authorization", $"Bearer {TokenStorage.GetTokenAsync().Result}");
            var client = new RestClient(_baseUri);
            var response = await client.GetAsync<List<StudentDto>>(request);
            //var response = AppData.RestClient.Get<List<StudentDto>>(request);
            return response;
        }

        public async Task<StudentsStream> GetStudentsByGroups(List<string> groupsNumbers)
        {
            var AppData = DependencyService.Get<AppData>();
            RestRequest request;
            //request = new RestRequest($"data/Students/stream");
            //JavaScriptSerializer javaScriptSerializer = new JavaScriptSerializer();
            //var groups = javaScriptSerializer.Serialize(groupsNumbers);
            var groups =JsonConvert.SerializeObject(groupsNumbers);
            //var groups = Newtonsoft.Json.JsonConvert.SerializeObject(new{ groupsNumbers=groupsNumbers});
            request = new RestRequest($"Students/stream/{groups}");
            //request.AddQueryParameter("groupsNumbers", groups);
            request.AddHeader("Authorization", $"Bearer {TokenStorage.GetTokenAsync().Result}");

            //var client = new RestClient(_baseUri);
            //var response =  client.Get<StudentsStream>(request);
            //var response = AppData.RestClient.Get<StudentsStream>(request);

            try
            {
                var client = new RestClient(_baseUri);
                var response = await client.GetAsync(request);
        
                //RestResponse response = await AppData.RestClient.GetAsync(request);
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
            catch(Exception e)
            {
                throw e;
            }
            //return response;
        }

        public async Task<EmployeeDto> GetEmployee(int id)
        {
            RestRequest request;
            request = new RestRequest($"Employees/{id}");
            //request.AddHeader("Authorization", $"Bearer {TokenStorage.GetTokenAsync().Result}");
            var client = new RestClient(_baseUri);
            var response = await client.GetAsync(request);
            if (response.IsSuccessful)
            {
                Console.WriteLine(response.Content);
                var result = JsonConvert.DeserializeObject<EmployeeDto>(response.Content);
                return result;
            }
            return null;
        }
        public async Task<StudentDto> GetStudent(int id)
        {
            RestRequest request;
            request = new RestRequest($"Students/{id}");
            //request.AddHeader("Authorization", $"Bearer {TokenStorage.GetTokenAsync().Result}");
            var client = new RestClient(_baseUri);
            var response = await client.GetAsync(request);
            if (response.IsSuccessful)
            {
                Console.WriteLine(response.Content);
                var result = JsonConvert.DeserializeObject<StudentDto>(response.Content);
                return result;
            }
            return null;
        }

        public async Task<List<StudentDto>> GetStudentsByGroup(string groupsNumber, AppData x)
        {
            var AppData = x;
            RestRequest request;
            request = new RestRequest($"Students/group/");
            //request = new RestRequest($"data/Students/group/");
            request.AddQueryParameter("groupsNumber", groupsNumber);
            //request.AddHeader("Authorization", $"Bearer {TokenStorage.GetTokenAsync().Result}");
            var client = new RestClient(_baseUri);
            var response = await client.GetAsync(request);
            if (response.IsSuccessful)
            {
                Console.WriteLine(response.Content);
                var result = JsonConvert.DeserializeObject<List<StudentDto>>(response.Content);
                return result;
            }
            return null;
        }

        public async Task<List<StudentDto>> GetStudentsByPlannedLesson(int id)
        {
            RestRequest request;
            request = new RestRequest($"Students/PlannedLesson/{id}");
            //request.AddHeader("Authorization", $"Bearer {TokenStorage.GetTokenAsync().Result}");
            var client = new RestClient(_baseUri);
            var response = await client.GetAsync(request);
            if (response.IsSuccessful)
            {
                Console.WriteLine(response.Content);
                var result = JsonConvert.DeserializeObject<List<StudentDto>>(response.Content);
                return result;
            }
            return null;
        }

        public async Task<bool> SendAttendances(IEnumerable<AttendanceDto> attendances)
        {
            RestRequest request;
            request = new RestRequest($"Attendance/AddList", Method.Post);
            //request.AddHeader("Authorization", $"Bearer {TokenStorage.GetTokenAsync().Result}");
            request.AddJsonBody(attendances);
            var client = new RestClient(_baseUri);
            var response = await client.PostAsync(request);
            if (response.IsSuccessful)
            {
                Console.WriteLine(response.Content);
                var result = JsonConvert.DeserializeObject<bool>(response.Content);
                return result;
            }
            return false;
        }
    }
}
