namespace SASC_Final.Models.Common.DTOs
{
    public class EducationFormDto : IDtoEntity
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public EducationFormDto()
        {
        }

        public EducationFormDto(int Id, string Name)
        {
            this.Id = Id;
            this.Name = Name;
        }
    }
}
