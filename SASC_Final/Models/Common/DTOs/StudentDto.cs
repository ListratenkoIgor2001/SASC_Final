namespace SASC_Final.Models.Common.DTOs
{
    public class StudentDto : IDtoEntity
    {
        public int Id { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string MiddleName { get; set; }

        public string RecordBookNumber { get; set; }

        public string ImageURL { get; set; }

        public int GroupId { get; set; }

        public string GroupNumber { get; set; }

        public int Subgroup { get; set; }

        public StudentDto() { }
    }
}
