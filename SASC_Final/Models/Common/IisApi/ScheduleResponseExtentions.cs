using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

using SASC_Final.Models.Common.DTOs;

namespace SASC_Final.Models.Common.IisApi
{
    public static class ScheduleResponseExtentions
    {
        private static List<DayShedule> GetScheduleByDayOfWeek(
          ScheduleResponseDto responseDto,
          DayOfWeek dayOfWeek)
        {
            switch (dayOfWeek)
            {
                case DayOfWeek.Monday:
                    if (responseDto.schedules.Понедельник != null)
                        return new List<DayShedule>((IEnumerable<DayShedule>)responseDto.schedules.Понедельник);
                    break;
                case DayOfWeek.Tuesday:
                    if (responseDto.schedules.Вторник != null)
                        return new List<DayShedule>((IEnumerable<DayShedule>)responseDto.schedules.Вторник);
                    break;
                case DayOfWeek.Wednesday:
                    if (responseDto.schedules.Среда != null)
                        return new List<DayShedule>((IEnumerable<DayShedule>)responseDto.schedules.Среда);
                    break;
                case DayOfWeek.Thursday:
                    if (responseDto.schedules.Четверг != null)
                        return new List<DayShedule>((IEnumerable<DayShedule>)responseDto.schedules.Четверг);
                    break;
                case DayOfWeek.Friday:
                    if (responseDto.schedules.Пятница != null)
                        return new List<DayShedule>((IEnumerable<DayShedule>)responseDto.schedules.Пятница);
                    break;
                case DayOfWeek.Saturday:
                    if (responseDto.schedules.Суббота != null)
                        return new List<DayShedule>((IEnumerable<DayShedule>)responseDto.schedules.Суббота);
                    break;
                default:
                    return (List<DayShedule>)null;
            }
            return (List<DayShedule>)null;
        }

        public static DateTime ConvertToDate(this string date, string[] dateFormats = null)
        {
            CultureInfo provider = new CultureInfo("en-US");
            string[] strArray;
            if (dateFormats != null)
                strArray = dateFormats;
            else
                strArray = new string[6]
                {
                  "dd.MM.yyyy",
                  "yyyy.MM.dd",
                  "MM.dd.yyyy",
                  "MM/dd/yyyy",
                  "dd/MM/yyyy",
                  "yyyy/MM/dd"
                };
            foreach (string format in strArray)
            {
                try
                {
                    return DateTime.ParseExact(date, format, (IFormatProvider)provider);
                }
                catch (FormatException ex)
                {
                    Console.WriteLine("Unable to parse '{0}'", (object)date);
                }
            }
            return new DateTime();
        }

        public static List<DayShedule> GetSheduleByDate(
          this ScheduleResponseDto responseDto,
          string date = "")
        {
            DateTime date1 = date.ConvertToDate();
            return date1 != new DateTime() ? ScheduleResponseExtentions.GetScheduleByDayOfWeek(responseDto, date1.DayOfWeek) : (List<DayShedule>)null;
        }

        public static List<DayShedule> GetWeekShedule(this ScheduleResponseDto responseDto)
        {
            return ScheduleResponseExtentions.GetScheduleByDayOfWeek(responseDto, DateTime.Today.DayOfWeek);
        }

        public static List<DayShedule> GetTodayLessons(
          this List<DayShedule> dayShedules,
          int weeknumber)
        {
            if (dayShedules == null)
                return (List<DayShedule>)null;
            List<DayShedule> todayLessons = new List<DayShedule>();
            todayLessons.AddRange(dayShedules.Where<DayShedule>((Func<DayShedule, bool>)(lesson => lesson.weekNumber.Any<int>((Func<int, bool>)(x => x == weeknumber)))));
            return todayLessons;
        }

        public static string GetLastNameInitiales(this Employee employee)
        {
            if (employee == null)
                return (string)null;
            return employee.lastName + " " + employee.firstName.Substring(0, 1) + "." + employee.middleName.Substring(0, 1) + ".";
        }
        public static string GetLastNameInitiales(this EmployeeDto employee)
        {
            if (employee == null)
                return (string)null;
            return employee.LastName + " " + employee.FirstName.Substring(0, 1) + "." + employee.MiddleName.Substring(0, 1) + ".";
        }

        public static string GetGroups(this List<Studentgroup> studentgroups)
        {
            if (studentgroups == null)
                return (string)null;
            List<string> list = studentgroups.Select<Studentgroup, string>((Func<Studentgroup, string>)(x => x.name)).OrderBy<string, string>((Func<string, string>)(x => x)).ToList<string>();
            if (list.Count == 1)
                return list[0] ?? "";
            return list.Count > 1 ? list[0] + "-" + list[list.Count - 1].Substring(list[list.Count - 1].Length - 1, 1) : "Groups not found";
        }
        public static string GetGroups(this List<string> studentgroups)
        {
            if (studentgroups == null)
                return (string)null;
            List<string> list = studentgroups.Select(x=>x).OrderBy(x=>x).ToList();
            if (list.Count == 1)
                return list[0] ?? "";
            return list.Count > 1 ? list[0] + "-" + list[list.Count - 1].Substring(list[list.Count - 1].Length - 1, 1) : "Groups not found";
        }
        public static List<string> GetGroupsList(this List<Studentgroup> studentgroups)
        {
            return studentgroups == null ? (List<string>)null : studentgroups.Select<Studentgroup, string>((Func<Studentgroup, string>)(x => x.name)).OrderBy<string, string>((Func<string, string>)(x => x)).ToList<string>();
        }
    }
}
