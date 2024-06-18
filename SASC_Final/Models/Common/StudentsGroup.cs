using System.Collections.Generic;

namespace SASC_Final.Models.Common
{
    public class StudentsGroup : IEntity
    {
        public int Id { get; set; }

        public int Course { get; set; }

        public string Number { get; set; }

        public virtual Speciality Speciality { get; set; }

        public virtual ICollection<PlannedLesson> Lessons { get; set; }
    }
}
