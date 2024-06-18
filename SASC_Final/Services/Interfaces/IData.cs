using System.Collections.Generic;
using System.Threading.Tasks;

using SASC_Final.Models.Common.DTOs;

namespace SASC_Final.Services
{
    public interface IData
    {
        Task<EmployeeDto> GetEmployee(int id);
        Task<StudentDto> GetStudent(int id);
        Task<EmployeeDto> GetEmployeeByGuid(string id);
        Task<StudentDto> GetStudentByGuid(string id);
        Task<List<StudentDto>> GetStudentsByPlannedLesson(int id);
    }
}
