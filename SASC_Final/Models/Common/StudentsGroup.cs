namespace SASC_Final.Models.Common
{
    public class StudentsGroup
    {
        public int Id { get; set; }

        public int Course { get; set; }

        public string Number { get; set; }

        public virtual Speciality Speciality { get; set; }
    }
}
