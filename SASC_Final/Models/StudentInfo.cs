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
        public string DeviceHash { get; set; }
        public DateTime TimeCrt { get; set; }
        public DateTime TimeNbf { get; set; }
        public DateTime TimeExp { get; set; }

        public StudentInfo(){ }

        public StudentInfo(AppData AppData) 
        {
            var service = DependencyService.Get<IDeviceInfo>();
            Id = AppData.User.Id;
            LessonId = AppData.CurrentLessons.CurrentItem.LessonId;
            DeviceHash = Convert.ToBase64String(service.GetSerialHash());
            TimeCrt = TimeNbf = DateTime.Now;
            TimeExp = TimeCrt.AddSeconds(10);
        }

        public string ToJson()
        {
            return JsonConvert.SerializeObject(this);
        }
    }
}
