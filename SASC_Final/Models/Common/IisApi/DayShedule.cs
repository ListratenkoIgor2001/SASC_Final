// Decompiled with JetBrains decompiler
// Type: SASC_Final.Models.Common.IisApi.DayShedule
// Assembly: DataModels, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 50CE441B-A79A-414F-B653-C6BF6814A121
// Assembly location: E:\!!!!!!!!!!!DP\SASC_Final-Data\Libraries\DataModels.dll

using System.Collections.Generic;


namespace SASC_Final.Models.Common.IisApi
{
  public class DayShedule
  {
    public DayShedule(
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
    {
      this.auditories = auditories;
      this.endLessonTime = endLessonTime;
      this.lessonTypeAbbrev = lessonTypeAbbrev;
      this.note = note;
      this.numSubgroup = numSubgroup;
      this.startLessonTime = startLessonTime;
      this.studentGroups = studentGroups;
      this.subject = subject;
      this.subjectFullName = subjectFullName;
      this.weekNumber = weekNumber;
      this.employees = employees;
      this.dateLesson = dateLesson;
      this.startLessonDate = startLessonDate;
      this.endLessonDate = endLessonDate;
      this.announcementStart = announcementStart;
      this.announcementEnd = announcementEnd;
      this.split = split;
      this.announcement = announcement;
    }

    public DayShedule()
    {
    }

    public List<string> auditories { get; set; }

    public string endLessonTime { get; set; }

    public string lessonTypeAbbrev { get; set; }

    public object note { get; set; }

    public int numSubgroup { get; set; }

    public string startLessonTime { get; set; }

    public List<Studentgroup> studentGroups { get; set; }

    public string subject { get; set; }

    public string subjectFullName { get; set; }

    public List<int> weekNumber { get; set; }

    public List<Employee> employees { get; set; }

    public object dateLesson { get; set; }

    public string startLessonDate { get; set; }

    public string endLessonDate { get; set; }

    public object announcementStart { get; set; }

    public object announcementEnd { get; set; }

    public bool split { get; set; }

    public bool announcement { get; set; }
  }
}
