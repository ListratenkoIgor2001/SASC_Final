using System.Collections.Generic;
using System;

namespace SASC_Final.Models.Common
{
    public class PlannedLesson
    {
        public int Id { get; set; }

        public virtual Employee Employee { get; set; }

        public string LessonType { get; set; }

        public virtual Subject Subject { get; set; }

        public virtual ICollection<StudentsGroup> Groups { get; set; }

        public DateTime PlannedDate { get; set; }

        public DateTime PlannedTime { get; set; }

        public string SubGroup { get; set; }
    }
}
