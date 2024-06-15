using System;

using SASC_Final.Models.Common.Enums;

namespace SASC_Final.Models.Common
{
  public class Attendance
  {
        public int Id { get; set; }

        public virtual Employee Employee { get; set; }

        public virtual Student Student { get; set; }

        public virtual PlannedLesson Lesson { get; set; }

        public DateTime EventDate { get; set; }

        public DateTime EventTime { get; set; }

        public DateTime AttendanceTime { get; set; }

        public bool IsPresent { get; set; } = false;

        public bool IsValidReason { get; set; } = false;

        public AbsenceReason Reason { get; set; } = AbsenceReason.NONE;
    }
}
