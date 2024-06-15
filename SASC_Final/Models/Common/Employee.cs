using System.Collections.Generic;

namespace SASC_Final.Models.Common
{
    public class Employee
    {
        public int Id { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string MiddleName { get; set; }

        public string Degree { get; set; }

        public string Rank { get; set; }

        public string UrlId { get; set; }

        public string ImageURL { get; set; }

        public virtual ICollection<Department> AcademicDepartment { get; set; }
    }
}
