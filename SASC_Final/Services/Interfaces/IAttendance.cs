using SASC_Final.Models.Common;
using SASC_Final.Models.Common.DTOs;

using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SASC_Final.Services
{
    public interface IAttendance
    {
        Task<bool> SendAttendances(IEnumerable<AttendanceDto> attendances);
    }
}
