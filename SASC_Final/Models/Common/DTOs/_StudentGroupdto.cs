using System;
using System.Text.RegularExpressions;


namespace SASC_Final.Models.Common.DTOs
{
  public class _StudentGroupdto
  {
    public string name { get; set; }

    public int specialityDepartmentEducationFormId { get; set; }

    public object course { get; set; }

    public int id { get; set; }

    public StudentsGroupDto GetStudentsGroup()
    {
      string str;
      try
      {
        str = new Regex("(\\d+)").Matches(this.course.ToString())[0].ToString();
        Convert.ToInt32(str);
      }
      catch
      {
        str = "-1";
      }
      return new StudentsGroupDto()
      {
        Id = this.id,
        Course = Convert.ToInt32(str),
        Number = this.name,
        SpecialityId = this.specialityDepartmentEducationFormId
      };
    }
  }
}
