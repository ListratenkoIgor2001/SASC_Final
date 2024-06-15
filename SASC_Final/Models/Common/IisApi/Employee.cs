namespace SASC_Final.Models.Common.IisApi
{
    public class Employee
    {
        public int id { get; set; }

        public string firstName { get; set; }

        public string middleName { get; set; }

        public string lastName { get; set; }

        public string photoLink { get; set; }

        public string degree { get; set; }

        public string degreeAbbrev { get; set; }

        public object rank { get; set; }

        public object email { get; set; }

        public object department { get; set; }

        public string urlId { get; set; }

        public string calendarId { get; set; }

        public object jobPositions { get; set; }
    }
}
