using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;

using SASC_Final.Models;
using SASC_Final.Models.Common;
using SASC_Final.ViewModels.Base;

namespace SASC_Final.ViewModels
{
    public class GroupViewModel : BaseViewModel
    {
        private string _key;
        public string Key
        {
            get { return _key; }
            set { SetProperty(ref _key, value); }
        }

        private ObservableCollection<AttendanceStudentViewModel> _students = new ObservableCollection<AttendanceStudentViewModel>();
        public ObservableCollection<AttendanceStudentViewModel> Students
        {
            get { return _students; }
            set { SetProperty(ref _students, value); }
        }
        public GroupViewModel() { }

        public GroupViewModel(IEnumerable<AttendanceStudent> group)
        {
            Title = group.FirstOrDefault()?.Group;
            Key = group.FirstOrDefault()?.Group;
            LoadStudents(group);
        }

        private void LoadStudents(IEnumerable<AttendanceStudent> students)
        {
            Students = new ObservableCollection<AttendanceStudentViewModel>(students.Select(s => new AttendanceStudentViewModel(s)));
        }
    }
}
