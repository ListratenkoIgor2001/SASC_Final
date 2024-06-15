namespace SASC_Final.Models.Common.DTOs
{
    public class SubjectDto
    {
        public int Id { get; set; }

        public int SpecialityId { get; set; }

        public string FullName { get; set; }

        public string Abbrev { get; set; }

        public int PlannedHours { get; set; }

        public SubjectDto() { }
    }
}
