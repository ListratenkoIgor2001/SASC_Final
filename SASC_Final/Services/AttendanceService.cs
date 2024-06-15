
using Newtonsoft.Json;
using RestSharp;

using SASC_Final.Models.Common;
using SASC_Final.Models.Common.DTOs;

using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SASC_Final.Services
{
    internal class AttendanceService : IAttendance
    {
        private string _baseUri = "http://sacs-data.somee.com";
        public async Task<bool> SendAttendances(IEnumerable<AttendanceDto> attendances)
        {
            //return true;
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
        public async Task<bool> SendAttendance(AttendanceDto attendances)
        {
            return true;
            RestRequest request;
            request = new RestRequest($"Attendance/AddList", Method.Post);
            //request.AddHeader("Authorization", $"Bearer {TokenStorage.GetTokenAsync().Result}");
            request.AddJsonBody(attendances);
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
