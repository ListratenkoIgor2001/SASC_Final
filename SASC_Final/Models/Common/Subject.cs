namespace SASC_Final.Models.Common
{
    public class Subject : IEntity
    {
        public int Id { get; set; }

        public virtual Speciality Speciality { get; set; }

        public string FullName { get; set; }
        
        public string Abbrev { get; set; }

        public int PlannedHours { get; set; }
    }
}
