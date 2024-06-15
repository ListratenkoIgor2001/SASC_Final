// Decompiled with JetBrains decompiler
// Type: SASC_Final.Models.Common.IisApi.Среда
// Assembly: DataModels, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 50CE441B-A79A-414F-B653-C6BF6814A121
// Assembly location: E:\!!!!!!!!!!!DP\SASC_Final-Data\Libraries\DataModels.dll

using System.Collections.Generic;


namespace SASC_Final.Models.Common.IisApi
{
    public class Среда : DayShedule
    {
        public Среда()
        {
        }

        public Среда(
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
