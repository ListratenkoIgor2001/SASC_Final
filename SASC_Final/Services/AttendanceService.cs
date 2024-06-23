
using Newtonsoft.Json;
using RestSharp;

using SASC_Final.Helpers;
using SASC_Final.Models.Common;
using SASC_Final.Models.Common.AuthModels;
using SASC_Final.Models.Common.DTOs;

using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;

namespace SASC_Final.Services
{
    internal class AttendanceService : IAttendance
    {

        private string _baseUri = "http://sacs-data.somee.com";
        private string _apiTemplate = "data/";
        private AppData AppData = DependencyService.Get<AppData>();

        public async Task<bool> SendAttendances(IEnumerable<AttendanceDto> attendances)
        {
            return true;
            RestRequest request;
            request = new RestRequest(_apiTemplate+$"Attendance/AddList", Method.Post);
            request.AddHeader("Authorization", $"Bearer {await TokenStorage.GetTokenAsync()}");
            request.AddHeader("Content-Type", "application/json");
            request.AddJsonBody(attendances);
            var client = AppData.RestClient;
            var response = await client.PostAsync(request);
            if (response.IsSuccessful)
            {
                Console.WriteLine(response.Content);
                //var result = JsonConvert.DeserializeObject<bool>(response.Content);
                return true;
            }
            return false;
        }
        public async Task<bool> SendAttendance(AttendanceDto attendance)
        {
            return true;
            RestRequest request;
            request = new RestRequest(_apiTemplate+$"Attendance/AddList", Method.Post);
            request.AddHeader("Authorization", $"Bearer {await TokenStorage.GetTokenAsync()}");
            request.AddHeader("Content-Type", "application/json");
            request.AddJsonBody(attendance);
            var client = new RestClient(_baseUri);
            var response = await client.PostAsync(request);
            if (response.IsSuccessful)
            {
                Console.WriteLine(response.Content);
                //var result = JsonConvert.DeserializeObject<bool>(response.Content);
                return true;
            }
            return false;
        }
    }
}
