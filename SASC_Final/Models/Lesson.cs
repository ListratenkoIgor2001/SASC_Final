using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using SASC_Final.Models.Common;
using SASC_Final.Models.Common.IisApi;
using SASC_Final.Helpers;
using SASC_Final.Models.Common.DTOs;
using SASC_Final.Models.Common;

namespace SASC_Final.Models
{
    //   https://docs.microsoft.com/en-us/xamarin/xamarin-forms/user-interface/listview/customizing-list-appearance
    public class Lesson
    {
        public string Id { get; set; }
        private bool _isClosed { get; set; } = false;
        public bool IsClosed
        {
            get
            {
                return !startLessonTime.CanRegisterOnLesson(DateTime.Now) || _isClosed;
            }
            set
            {
                _isClosed = value;
            }
        } 

        public int LessonId { get; set; }
        private List<string> auditories { get; set; }
        private string startLessonTime { get; set; }
        private string endLessonTime { get; set; }
        private string numSubgroup { get; set; }
        private string subject { get; set; }
        private string subjectFullName { get; set; }
        private string lessonTypeAbbrev { get; set; }
        public string Note { get; set; }
        private EmployeeDto employee { get; set; }
        private List<string> studentGroups { get; set; }

        private List<Common.IisApi.Employee> iisEmployees { get; set; }
        private List<Common.IisApi.Studentgroup> iisStudentGroups { get; set; }

        public string Auditory
        {
            get
            {
                if ((auditories != null) && (auditories?.Count != 0))
                    return auditories[0];
                return "";
            }
        }
        public string Start { get => startLessonTime.ToShortDateTime(); }
        public string End { get => endLessonTime.ToShortDateTime(); }
        public string Subject { get => $"{subject} ({lessonTypeAbbrev})"; }
        public string LessonType { get => $"{lessonTypeAbbrev}"; }
        public string Employee { get => employee.GetLastNameInitiales(); }
        public string Groups { get => studentGroups.GetGroups(); }
        public string IisEmployee { get => iisEmployees[0]?.GetLastNameInitiales(); }
        public string IisGroups { get => iisStudentGroups.GetGroups(); }
        public List<string> GetIisGroupsList() { return iisStudentGroups.GetGroupsList(); }
        public Lesson(string Id, DayShedule map)
        {
            this.Id = Id;
            this.LessonId = -1;
            this.auditories = map.auditories;
            this.endLessonTime = map.endLessonTime;
            this.lessonTypeAbbrev = map.lessonTypeAbbrev;
            this.numSubgroup = map.numSubgroup.ToString();
            this.startLessonTime = map.startLessonTime;
            this.iisStudentGroups = map.studentGroups;
            this.subject = map.subject;
            this.subjectFullName = map.subjectFullName;
            this.iisEmployees = map.employees;
        }
        public Lesson(string Id, PlannedLessonDto map)
        {
            this.Id = Id;
            this.LessonId = map.Id;
            this.auditories = new List<string> { map.Auditory};
            this.endLessonTime = map.PlannedEndTime;
            this.lessonTypeAbbrev = map.LessonType;
            this.numSubgroup = map.SubGroup == "0" ? "" : map.SubGroup;
            this.startLessonTime = map.PlannedTime;
            this.studentGroups = map.Groups.ToList();
            this.subject = map.Subject.Abbrev;
            this.subjectFullName = map.Subject.FullName;
            this.employee = map.Employee;
        }

        public int GetTimeDifference()
        {
            var start = this.startLessonTime.ParseDateTime();
            if (start.HasValue)
            {
                TimeSpan difference = DateTime.Now.Subtract(start.Value);
                return (int)difference.TotalMinutes;
            }
            return int.MinValue;
        }
        public bool IsClosedByEmployee => _isClosed;
        /*
        public Lesson(string Id, PlannedLesson map)
        {
            this.Id = Id;
            this.LessonId = map.Id;
            this.auditories = new List<string>();
            this.endLessonTime = map.PlannedTime.AddHours(1).AddMinutes(40).ToString("hh:mm");
            this.lessonTypeAbbrev = map.LessonType;
            this.numSubgroup = map.SubGroup=="0"?"": map.SubGroup;
            this.startLessonTime = map.PlannedTime.ToString("hh:mm");
            this.studentGroups = map.Groups.Select(x=>x.Number).ToList();
            this.subject = map.Subject.Abbrev;
            this.subjectFullName = map.Subject.FullName;
            this.employee = map.Employee;
        }
        */
    }
}
