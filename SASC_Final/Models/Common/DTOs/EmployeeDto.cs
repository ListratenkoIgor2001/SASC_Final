namespace SASC_Final.Models.Common.DTOs
{
    public class EmployeeDto : IDtoEntity
    {
        public int Id { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string MiddleName { get; set; }

        public string Degree { get; set; }

        public string Rank { get; set; }

        public string UrlId { get; set; }

        public string ImageURL { get; set; }

        public string AcademicDepartment { get; set; }

        public EmployeeDto() { }
    }
}
