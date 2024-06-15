namespace SASC_Final.Models.Common.DTOs
{
  public class _tempdto
  {
    public int id { get; set; }

    public string name { get; set; }

    public string abbrev { get; set; }

    public Educationform educationForm { get; set; }

    public int facultyId { get; set; }

    public string code { get; set; }

    public SpecialityDto GetDTO()
    {
      return new SpecialityDto()
      {
        Id = this.id,
        Name = this.name,
        Abbrev = this.abbrev,
        EducationFormId = this.educationForm.id,
        FacultyId = this.facultyId,
        Code = this.code
      };
    }
  }
}
