// Decompiled with JetBrains decompiler
// Type: SASC_Final.Models.Common.IisApi.Studentgroupdto
// Assembly: DataModels, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 50CE441B-A79A-414F-B653-C6BF6814A121
// Assembly location: E:\!!!!!!!!!!!DP\SASC_Final-Data\Libraries\DataModels.dll


namespace SASC_Final.Models.Common.IisApi
{
    public class Studentgroupdto
    {
        public string name { get; set; }

        public int facultyId { get; set; }

        public string facultyAbbrev { get; set; }

        public int specialityDepartmentEducationFormId { get; set; }

        public string specialityName { get; set; }

        public string specialityAbbrev { get; set; }

        public int course { get; set; }

        public int id { get; set; }

        public string calendarId { get; set; }

        public int educationDegree { get; set; }
    }
}
