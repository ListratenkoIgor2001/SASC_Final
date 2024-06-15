using System;
using System.Collections.Generic;
using System.Text;

using Newtonsoft.Json;

using SASC_Final.Services.Interfaces;

using Xamarin.Forms;

namespace SASC_Final.Models
{
    public class StudentInfo
    {
        public int Id { get; set; }
        public int LessonId { get; set; }
        public byte[] DeviceHash { get; set; }
        public DateTime TimeCrt { get; set; }
        public DateTime TimeNbf { get; set; }
        public DateTime TimeExp { get; set; }

        public StudentInfo() 
        {
            var AppData = DependencyService.Get<AppData>();
            var service = DependencyService.Get<IDeviceInfo>();
            Id = AppData.User.Id;
            LessonId = AppData.SelectedLesson.LessonId;
            DeviceHash = service.GetSerialHash();
            TimeCrt = TimeNbf = DateTime.Now;
            TimeExp = TimeCrt.AddDays(5).AddSeconds(10);
        }

        public string ToJson()
        {
            return JsonConvert.SerializeObject(this);
        }
    }
}
