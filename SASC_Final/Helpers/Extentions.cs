using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;

using SASC_Final.Models.Common;
using SASC_Final.Models;
using SASC_Final.Models.Common.DTOs;
using SASC_Final.ViewModels;
using SASC_Final.Models.Common.Enums;
using Xamarin.Forms;
using System.IdentityModel.Tokens.Jwt;
using System.Globalization;

namespace SASC_Final.Helpers
{
    public static partial class Extentions
    {
        public static DateTime? ParseDateTime(this string input)
        {
            string[] formats = {
            "MM/dd/yyyy", "dd/MM/yyyy", "yyyy-MM-dd", "yyyy/MM/dd", "dd-MM-yyyy",
            "MM-dd-yyyy", "M/d/yyyy", "d/M/yyyy", "yyyy-M-d", "yyyy/M/d",
            "dd MMM yyyy", "MMM dd, yyyy", "yyyy MMM dd",
            "dd-MM-yyyy HH:mm:ss", "MM/dd/yyyy HH:mm:ss", "yyyy-MM-dd HH:mm:ss",
            "yyyy/MM/dd HH:mm:ss", "dd/MM/yyyy HH:mm:ss","MM/dd/yyyy hh:mm:ss tt","M/d/yyyy h:mm:ss tt","MM/dd/yyyy HH:mm:ss tt"
            // Add more formats as needed
        };

            foreach (var format in formats)
            {
                if (DateTime.TryParseExact(input, format, CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime date))
                {
                    return date;
                }
            }

            return null; // Return null if no formats matched
        }


        public static bool CanRegisterOnLesson(this string lessonStart, DateTime now)
        {
            var start = lessonStart.ParseDateTime();
            if (start.HasValue){
                
                TimeSpan difference = now.Subtract(start.Value);
                return Math.Abs(difference.TotalMinutes) <= 5;
            }
            return false;
        }

        public static string ToShortDateTime(this string date, string format = "HH:mm")
        {
            var datetime = date.ParseDateTime();
            if (datetime.HasValue)
            {
                return datetime.Value.ToString(format);
            }
            return string.Empty;
        }

        public static string GetLastNameInitiales(this Employee employee)
        {
            if (employee == null)
                return (string)null;
            return employee.LastName + " " + employee.FirstName.Substring(0, 1) + "." + employee.MiddleName.Substring(0, 1) + ".";
        }
        public static string GetGroups(this List<StudentsGroup> groups)
        {
            var result = new List<string>();
            if (groups == null)
                return (string)null;
            //List<string> list = studentgroups.Select<Studentgroup, string>((Func<Studentgroup, string>)(x => x.name)).OrderBy<string, string>((Func<string, string>)(x => x)).ToList<string>();
            List<string> list = groups.Select(x => x.Number).OrderBy(x => x).ToList();
            if (list.Count == 1)
                return list[0] ?? "";
            return list.Count > 1 ? list[0] + "-" + list[list.Count - 1].Substring(list[list.Count - 1].Length - 1, 1) : "Groups not found";
            //return employee.LastName + " " + employee.FirstName.Substring(0, 1) + "." + employee.MiddleName.Substring(0, 1) + ".";
        }
        public static void ClearStack(this INavigation navigation)
        {
            foreach (var p in navigation.NavigationStack)
            {
                navigation.RemovePage(p);
            }
        }
        public static List<AttendanceStudent> ToAttendanceStudentsList(this List<Student> studs, int emplId, int planLesId)
        {
            var result = new List<AttendanceStudent>();
            studs.ForEach(s => result.Add(new AttendanceStudent(s, emplId, planLesId)));
            return result;
        }
        public static List<AttendanceStudent> ToAttendanceStudentsList(this List<StudentDto> studs, int emplId, int planLesId)
        {
            var result = new List<AttendanceStudent>();
            studs.ForEach(s => result.Add(new AttendanceStudent(s, emplId, planLesId)));
            return result;
        }
        public static AttendanceDto GetAttendance(this AttendanceStudent stud) 
        {
            return null;
        }
        public static AttendanceDto GetAttendanceDto(this AttendanceStudentViewModel stud)
        {
            return new AttendanceDto
            {
                EmployeeId = stud.studentModel.EmployeeId,
                StudentId = stud.studentModel.Id,
                PlannedLessonId = stud.studentModel.PlannedLessonId,
                AttendanceTime = stud.RegisteredAt,
                EventDate = DateTime.Now.Date.ToString(),
                EventTime = DateTime.Now.AddMinutes(-5).ToString(),
                IsPresent = stud.IsPresent,
                IsValidReason = false,
                Reason = stud.IsPresent ? AbsenceReason.NONE.ToString() : AbsenceReason.NO_SHOW.ToString()
            };
        }
        public static List<AttendanceDto> GetAttendanceDtoList(this IEnumerable<AttendanceStudentViewModel> studs)
        {
            return studs.Select(x => x.GetAttendanceDto()).ToList();           
        }
    }

    public static class PayloadExtention
    {

        public static object PayloadExist(this JwtSecurityToken jwtSecurityToken, string subject)
        {
            try
            {
                return jwtSecurityToken.Payload[subject];
            }
            catch
            {
                return null;
            }
        }
    }
}

