namespace SASC_Final.Models.Common
{
    public class Speciality
    { 
        public int Id { get; set; }

        public string Abbrev { get; set; }

        public string Name { get; set; }

        public virtual Faculty Faculty { get; set; }

        public virtual EducationForm EducationForm { get; set; }

        public string Code { get; set; }
    }
}
