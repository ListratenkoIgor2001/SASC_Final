using System.Collections.Generic;


namespace SASC_Final.Models.Common.IisApi
{
    public class ScheduleResponseDto
    {
        public string startDate { get; set; }

        public string endDate { get; set; }

        public object startExamsDate { get; set; }

        public object endExamsDate { get; set; }

        public Employeedto employeeDto { get; set; }

        public Studentgroupdto studentGroupDto { get; set; }

        public Schedules schedules { get; set; }

        public List<object> exams { get; set; }
    }
}
