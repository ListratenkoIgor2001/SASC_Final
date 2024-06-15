namespace SASC_Final.Models.Common.DTOs
{
    public class SpecialityDto : IDtoEntity
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Abbrev { get; set; }

        public int EducationFormId { get; set; }

        public int FacultyId { get; set; }

        public string Code { get; set; }

        public SpecialityDto() { }
    }
}
