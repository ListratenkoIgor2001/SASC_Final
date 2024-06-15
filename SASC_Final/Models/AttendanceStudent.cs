using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using SASC_Final.Models.Common;
using SASC_Final.Models.Common.DTOs;
using SASC_Final.Models.Common.IisApi;

namespace SASC_Final.Models
{
    public class AttendanceStudent
    {
        public int Id { get; set; }
        public int EmployeeId { get; set; }
        public int PlannedLessonId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string MiddleName { get; set; }
        public string RecordbookNumber { get; set; }
        public string ImageUrl { get; set; }
        public string Group { get; set; }
        public string AttendanceTime;
        public bool IsPresent;

        public AttendanceStudent() { }

        public AttendanceStudent(Student student, int Employee, int PlannedLesson)
{
            Id = student.Id;
            EmployeeId = Employee;
            PlannedLessonId = PlannedLesson;
            FirstName = student.FirstName;
            LastName = student.LastName;
            MiddleName = student.MiddleName;
            RecordbookNumber = student.RecordBookNumber;
            ImageUrl = student.ImageURL;
            Group = student.StudentsGroup.Number;
            AttendanceTime = null;
            IsPresent = false;
        }
        public AttendanceStudent(StudentDto studentDto, int Employee, int PlannedLesson)
        {
            Id = studentDto.Id;
            EmployeeId = Employee;
            PlannedLessonId = PlannedLesson;
            FirstName = studentDto.FirstName;
            LastName = studentDto.LastName;
            MiddleName = studentDto.MiddleName;
            RecordbookNumber = studentDto.RecordBookNumber;
            ImageUrl = studentDto.ImageURL;
            Group = studentDto.GroupNumber;
            AttendanceTime = null;
            IsPresent = false;
        }
    }
    /*
    public static class StudentsStreamExtention
    {
        public static List<AttendanceStudent> GetAttendanceStudents(this List<StudentDto> stream)
        {
            var list = new List<AttendanceStudent>();
            foreach (var student in stream)
            {
                list.Add(student.GetAttendanceStudent());
            }
            return list;
        }
        public static AttendanceStudent GetAttendanceStudent(this StudentDto student)
        {
            return new AttendanceStudent
            {
                FirstName = student.FirstName,
                LastName = student.LastName,
                MiddleName = student.MiddleName,
                ImageUrl = student.ImageUrl,
                Id = student.Id,
                Group = student.Group,
                RecordbookNumber = student.RecordbookNumber,
                AttendanceTime = null,
                IsPresent = false
            };
        }
        public static List<AttendanceStudent> GetStudents(this StudentsStream student)
        {
            var result = new List<AttendanceStudent>();
            foreach (var pair in student)
            {
                result.AddRange(pair.Value.GetAttendanceStudents());
            }
            return result.Select(x => x).OrderBy(x => x.Group).ToList();
        }
    }
    */
}
