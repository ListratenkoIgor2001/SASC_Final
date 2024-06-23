using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Diagnostics;
using System.Threading.Tasks;
using Xamarin.Forms;
using System.Collections.ObjectModel;

using SASC_Final.Services;
using SASC_Final.Models;
using SASC_Final.Helpers;
using SASC_Final.ViewModels.Base;
using Microsoft.AppCenter.Crashes;
using SASC_Final.Views;
using SASC_Final.Models.Common.DTOs;

namespace SASC_Final.ViewModels
{
    [QueryProperty(nameof(LessonId), nameof(LessonId))]
    public class LessonViewModel : BaseViewModel
    {
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
                //LoadItemId(value);
            }
        }

        private Lesson _selectedItem = null;
        public ObservableCollection<Grouping<string, AttendanceStudentViewModel>> Groups { get; set; } = new ObservableCollection<Grouping<string, AttendanceStudentViewModel>>();
        public ObservableCollection<AttendanceStudentViewModel> Students { get; }
        public Command LoadItemsCommand { get; }
        AppData AppData = DependencyService.Get<AppData>();

        public LessonViewModel()
        {
            Groups = new ObservableCollection<Grouping<string, AttendanceStudentViewModel>>();
            Students = new ObservableCollection<AttendanceStudentViewModel>();
            LoadItemsCommand = new Command(async () => await LoadStudents());
            Title = GetTitle();
            _selectedItem = AppData.CurrentLessons.CurrentItem;
            //LoadState();
        }
        public LessonViewModel(int id)
        {
            Groups = new ObservableCollection<Grouping<string, AttendanceStudentViewModel>>();
            Students = new ObservableCollection<AttendanceStudentViewModel>();
            _selectedItem = AppData.CurrentLessons.CurrentItem;
            //_lessonId = id;
            LoadItemsCommand = new Command(async () => await LoadStudents());
            //LoadItemId(id);
            Title = GetTitle();
            //ItemTapped = new Command<Item>(OnItemSelected);
        }
        private string GetTitle()
        {
            return $"{_selectedItem?.Subject} ({_selectedItem?.LessonType})";
        }
        public async Task LoadStudents(bool loadFromContext = false) {

            IsBusy = true;
            //var AppData = DependencyService.Get<AppData>();
            try
            {
                if (!loadFromContext || AppData.CurrentAttendances.Count == 0)
                {
                    AppData.CurrentAttendances.Clear();
                    Students.Clear();
                    var _service = DependencyService.Get<IData>();
                    var students = await _service.GetStudentsByPlannedLesson(_selectedItem.LessonId);
                    var filteredStudents = students.OrderBy(x=>x.LastName).ThenBy(x=>x.FirstName).ToList();
                    var subgroups = _selectedItem.SubgroupNumber;
                    if (subgroups > 0)
                    {
                        filteredStudents = students.Where(x => x.Subgroup == subgroups).ToList();
                    }

                    var attendanceStudents = filteredStudents.ToAttendanceStudentsList(AppData.User.Id, _selectedItem.LessonId);
                    foreach (var ats in attendanceStudents)
                    {
                        Students.Add(new AttendanceStudentViewModel(ats));
                    }
                    AppData.CurrentAttendances.SetItems(Students.ToList());
                }
                else 
                {
                    if (AppData.CurrentAttendances == null || AppData.CurrentAttendances.Count == 0)
                    {
                        throw new Exception("CurrentAttendances is null or empty");
                    }
                    foreach (var ats in AppData.CurrentAttendances.GetItems())
                    {
                        Students.Add(ats);
                    }                    
                }
                var groups = Students
                    .OrderBy(x => x.studentModel.Group)
                    .GroupBy(x => x.studentModel.Group)
                    .Select(g => new Grouping<string, AttendanceStudentViewModel>(g.Key, g))
                    .ToList();
                Groups = new ObservableCollection<Grouping<string, AttendanceStudentViewModel>>(groups);
            }
            catch (Exception ex)
            {
                Error = ex.Message;
                DisplayError();
                Debug.WriteLine(ex);
            }
            finally
            {
                IsBusy = false;
            }

        }
        public void LoadItemId(int itemId)
        {
            //var AppData = DependencyService.Get<AppData>();
            try
            {
                IsBusy = true;
                _selectedItem = AppData.CurrentLessons.CurrentItem = AppData.CurrentLessons?.Items?.FirstOrDefault(x => x.LessonId == itemId);
                if (_selectedItem == null)
                {
                    throw new ArgumentNullException("My_Ex_LessonViewModel_LoadItemId_SelectedItemNull");
                }
               
            }
            catch (Exception ex)
            {
                var properties = new Dictionary<string, string> {
                    { "count", AppData.CurrentLessons.Items.Count().ToString() }
                };
                Crashes.TrackError(ex, properties);
                GoBack();
            }
            finally
            {
                IsBusy = false;
            }
        }
        public async void SendAttendances() 
        {
            try
            {
                IsBusy = true;
                //var AppData = DependencyService.Get<AppData>();
                var service = DependencyService.Get<IAttendance>();
                var result = await service.SendAttendances(Students.ToList().GetAttendanceDtoList());
                if (result)
                {
                    AppData.CurrentAttendances.Clear();
                    AppData.CurrentStudents.Clear();
                    _selectedItem.IsClosed = true;
                    AppData.CurrentLessons.CurrentItem = _selectedItem = null;
                    SuccessSend();
                    GoBack();
                }
                else
                {
                    throw new Exception("При отправке данных произошла ошибка. Проверьте подключение и попробуйте снова");
                }
            }
            catch (Exception ex)
            {
                Error = ex.Message;
                DisplayError();
            }
            finally 
            {
                IsBusy = false;
            }
        }

        public void SaveState() 
        {
            var AppData = DependencyService.Get<AppData>();
            AppData.CurrentAttendances.SetItems(Students.ToList());
            AppData.CurrentLessons.CurrentItem = _selectedItem;
            //AppData.SelectedLesson = _selectedItem;
        }
        public void LoadState() 
        {
            var AppData = DependencyService.Get<AppData>();
            if (_selectedItem == null)
            {
                _selectedItem = AppData.CurrentLessons.CurrentItem;
                //_selectedItem = AppData.SelectedLesson;
                LoadStudents(false);
            }
            else { 
                LoadStudents(true);
            }
        }

        public Action DisplayError;
        public Action SuccessSend;
        public Action GoBack;
        //public Action DisplayError;

        public Action Refresh;
        public Action ScheduleNotFound;

        private string error;
        public string Error
        {
            get => error;
            set => SetProperty(ref error, value);
        }
    }
}
