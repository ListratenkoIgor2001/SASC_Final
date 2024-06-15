using System;
using System.Collections.Generic;
using System.Text;

namespace SASC_Final.Models.Common
{
    public class Subject
    {
        public int Id { get; set; }

        public virtual Speciality Speciality { get; set; }

        public string FullName { get; set; }

        public string Abbrev { get; set; }

        public int PlannedHours { get; set; }
    }
}
