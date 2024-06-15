namespace SASC_Final.Models.Common
{
    public class Student
    {
        public int Id { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string MiddleName { get; set; }

        public string RecordBookNumber { get; set; }

        public string ImageURL { get; set; }

        public virtual StudentsGroup StudentsGroup { get; set; }

        public int Subgroup { get; set; }
    }
}
