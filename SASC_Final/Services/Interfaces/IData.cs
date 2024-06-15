//using SASC_Final.Models.Common.DTOs;
using SASC_Final.Models.Common;
using SASC_Final.Models.Common.DTOs;

using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SASC_Final.Services
{
    public interface IData
    {
        Task<EmployeeDto> GetEmployee(int id);
        Task<StudentDto> GetStudent(int id);
        Task<List<StudentDto>> GetStudentsByPlannedLesson(int id);
    }
}
