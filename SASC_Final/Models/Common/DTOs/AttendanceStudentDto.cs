namespace SASC_Final.Models.Common.DTOs
{
    public class AttendanceStudentDto
    {
        public int Id { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string MiddleName { get; set; }

        public string RecordbookNumber { get; set; }

        public string ImageUrl { get; set; }

        public string Group { get; set; }

        public string AttendanceTime { get; set; }

        public bool IsPresent { get; set; }
    }
}
