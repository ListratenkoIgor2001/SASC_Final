using System.Collections.Generic;


namespace SASC_Final.Models.Common.IisApi
{
    public class Вторник : DayShedule
    {
        public Вторник()
        {
        }

        public Вторник(
          List<string> auditories,
          string endLessonTime,
          string lessonTypeAbbrev,
          object note,
          int numSubgroup,
          string startLessonTime,
          List<Studentgroup> studentGroups,
          string subject,
          string subjectFullName,
          List<int> weekNumber,
          List<Employee> employees,
          object dateLesson,
          string startLessonDate,
          string endLessonDate,
          object announcementStart,
          object announcementEnd,
          bool split,
          bool announcement)
          : base(auditories, endLessonTime, lessonTypeAbbrev, note, numSubgroup, startLessonTime, studentGroups, subject, subjectFullName, weekNumber, employees, dateLesson, startLessonDate, endLessonDate, announcementStart, announcementEnd, split, announcement)
        {
        }
    }
}
