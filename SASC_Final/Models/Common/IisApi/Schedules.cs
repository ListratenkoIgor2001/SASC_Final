// Decompiled with JetBrains decompiler
// Type: SASC_Final.Models.Common.IisApi.Schedules
// Assembly: DataModels, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 50CE441B-A79A-414F-B653-C6BF6814A121
// Assembly location: E:\!!!!!!!!!!!DP\SASC_Final-Data\Libraries\DataModels.dll

using System.Collections.Generic;


namespace SASC_Final.Models.Common.IisApi
{
    public class Schedules
    {
        public List<SASC_Final.Models.Common.IisApi.Понедельник> Понедельник { get; set; }

        public List<SASC_Final.Models.Common.IisApi.Вторник> Вторник { get; set; }

        public List<SASC_Final.Models.Common.IisApi.Среда> Среда { get; set; }

        public List<SASC_Final.Models.Common.IisApi.Четверг> Четверг { get; set; }

        public List<SASC_Final.Models.Common.IisApi.Пятница> Пятница { get; set; }

        public List<SASC_Final.Models.Common.IisApi.Суббота> Суббота { get; set; }
    }
}
