namespace SASC_Final.Models.Common.DTOs
{
    public class AttendanceDto
    {
        public int StudentId { get; set; }

        public int EmployeeId { get; set; }

        public int PlannedLessonId { get; set; }

        public string EventDate { get; set; }

        public string EventTime { get; set; }

        public string AttendanceTime { get; set; }

        public bool IsPresent { get; set; }

        public bool IsValidReason { get; set; }

        public string Reason { get; set; }

        public AttendanceDto() { }
    }
}
