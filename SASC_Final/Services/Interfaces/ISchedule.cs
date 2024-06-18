using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using RestSharp;
using SASC_Final.Models.Common.IisApi;
using SASC_Final.Models;
using SASC_Final.Models.Common;
using SASC_Final.Models.Common.DTOs;

namespace SASC_Final.Services
{
    public interface ISchedule
    {
        Task<List<PlannedLessonDto>> LoadSchedule(string date = null);
        Task<int> GetCurrentWeek();
    }
}
