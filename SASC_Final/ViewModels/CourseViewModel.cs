using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;

using Microsoft.AppCenter.Crashes;

using SASC_Final.Helpers;
using SASC_Final.Models;
using SASC_Final.Models.Common.DTOs;
using SASC_Final.Services;
using SASC_Final.ViewModels.Base;

using Xamarin.Forms;

namespace SASC_Final.ViewModels
{
    public class CourseViewModel:BaseViewModel
    {
        private ObservableCollection<GroupViewModel> _groups = new ObservableCollection<GroupViewModel>();
        public ObservableCollection<GroupViewModel> Groups
        {
            get { return _groups; }
            set { SetProperty(ref _groups, value); }
        }

        public Lesson _selectedItem;
        private int _lessonId;
        public int Id { get; set; }
        public int LessonId
        {
            get
            {
                return _lessonId;
            }
            set
            {
                _lessonId = value;
                LoadItemId(value);
            }
        }

        public CourseViewModel()
        {
            
        }

        public async void LoadData()
        {
            var newGroups = new ObservableCollection<GroupViewModel>();
            var AppData = DependencyService.Get<AppData>();
            try
            {
                IsBusy = true;
                Groups.Clear();
                var _service = DependencyService.Get<IData>();
                var students = await _service.GetStudentsByPlannedLesson(_selectedItem.LessonId);
                var attendanceStudents = students.ToAttendanceStudentsList(AppData.User.Id, _selectedItem.LessonId);

                //var plannedLessonId = 17;
                //var EmployeeId = 2;
                //var stds = _service.GetStudentsByPlannedLesson(17);
                //var attendanceStudents = stds;
                //var groups = await DataStore.GetItemsAsync();
                Groups = new ObservableCollection<GroupViewModel>();
                var grouping = students.GroupBy(g => g.GroupNumber);
                foreach (var group in grouping) 
                {
                    var studs = new List<AttendanceStudent>();
                    foreach (var stud in group)
                    {
                        studs.Add(new AttendanceStudent(studentDto:stud, AppData.User.Id, _selectedItem.LessonId));
                    }
                    GroupViewModel viewModel = new GroupViewModel(studs);
                    newGroups.Add(viewModel);
                }
                Groups = newGroups;
            }
            catch(Exception ex)
            { 

            }
            finally
            {
                IsBusy = false;
            }
        }
        public void LoadItemId(int itemId)
        {
            var AppData = DependencyService.Get<AppData>();
            try
            {
                _selectedItem = AppData.CurrentLessons.CurrentItem = AppData.CurrentLessons?.Items?.FirstOrDefault(x => x.LessonId == itemId);
                if (_selectedItem == null)
                {
                    throw new ArgumentNullException("My_Ex_LessonViewModel_LoadItemId_SelectedItemNull");
                }
                //_selectedItem= AppData.CurrentLessons.Items.Where(x => x.Id == itemId).FirstOrDefault();

            }
            catch (Exception ex)
            {
                var properties = new Dictionary<string, string> {
                    { "count", AppData.CurrentLessons.Items.Count().ToString() }
                };
                Crashes.TrackError(ex, properties);
            }
        }

    }
}
