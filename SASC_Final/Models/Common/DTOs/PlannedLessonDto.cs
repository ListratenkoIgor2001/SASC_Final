using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SASC_Final.Models.Common.DTOs
{
    public class PlannedLessonDto
    {
        public int Id { get; set; }

        public EmployeeDto Employee { get; set; }

        public SubjectDto Subject { get; set; }

        public List<string> Groups { get; set; }

        public string LessonType { get; set; }

        public string LessonAbbrev { get; set; }

        public string PlannedDate { get; set; }

        public string PlannedTime { get; set; }

        public string PlannedEndTime { get; set; }

        public string Auditory { get; set; }

        public int SubGroup { get; set; }
    }
}
