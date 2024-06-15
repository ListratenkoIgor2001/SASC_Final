namespace SASC_Final.Models.Common.DTOs
{
    public class StudentsGroupDto : IDtoEntity
    {
        public int Id { get; set; }

        public int Course { get; set; }

        public string Number { get; set; }

        public int SpecialityId { get; set; }
    }
}
