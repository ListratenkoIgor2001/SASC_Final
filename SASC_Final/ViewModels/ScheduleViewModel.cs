using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Text;
using System.Threading.Tasks;

using SASC_Final.Models.Common.IisApi;

using SASC_Final.Models;

using SASC_Final.ViewModels.Base;

using Xamarin.Forms;
using SASC_Final.Services;
using System.Linq;
using SASC_Final.Views;
using Microsoft.AppCenter.Crashes;
using System.Globalization;
using SASC_Final.Helpers;

namespace SASC_Final.ViewModels
{
    public class ScheduleViewModel : BaseViewModel
    {
        private Lesson _selectedItem;
        public ObservableCollection<Lesson> Items { get; }
        public Command LoadItemsCommand { get; }
        public Command<Lesson> ItemTapped { get; }

        public ScheduleViewModel()
        {
            Title = GetTitle();
            Items = new ObservableCollection<Lesson>();
            LoadItemsCommand = new Command(async () => await ExecuteLoadItemsCommand(true));
            ItemTapped = new Command<Lesson>(OnItemSelected);           
        }
        private string GetTitle()
        {
            return $"{new CultureInfo("ru-RU").DateTimeFormat.GetDayName(DateTime.Today.DayOfWeek).Capitalize()} {DateTime.Today.ToString("dd.MM.yyyy")}";
        }
        public async Task ExecuteLoadItemsCommand(bool forced = false)
        {
            IsBusy = true;
            try
            {
                var AppData = DependencyService.Get<AppData>();
                var lessons = AppData.CurrentLessons.GetItems();
                if (lessons.Count <= 0)
                {
                    Items.Clear();
                    AppData.CurrentLessons.Clear();
                    var _service = DependencyService.Get<ISchedule>();
                    var itemsResult = await _service.LoadSchedule();

                    var week = AppData.CurrentWeek;
                    var items = itemsResult;
                    if (items.Count == 0) ScheduleNotFound();
                    else Refresh();
                    foreach (var item in items)
                    {
                        //Items.Add(new Lesson(item.Id.ToString(), item));
                        Items.Add(new Lesson(Guid.NewGuid().ToString(), item));
                    }
                }
                else 
                {
                    Items.Clear();
                    foreach (var item in lessons)
                    {
                        //Items.Add(new Lesson(item.Id.ToString(), item));
                        Items.Add(item);
                    }
                }                    
                AppData.CurrentLessons.SetItems(Items.ToList());
            }
            catch (Exception ex)
            {
                Crashes.TrackError(ex);
                Error = ex.Message;
                DisplayError();
                Debug.WriteLine(ex);
            }
            finally
            {
                IsBusy = false;
            }
        }

        public void OnAppearing()
        {
            //IsBusy = true;
            SelectedItem = null;
            ExecuteLoadItemsCommand().Wait();
        }

        public Lesson SelectedItem
        {
            get => _selectedItem;
            set
            {
                SetProperty(ref _selectedItem, value);
                OnItemSelected(value);
            }
        }

        private async void OnItemSelected(Lesson item)
        {
            if (item == null)
                return;

            var appData = DependencyService.Get<AppData>();
            appData.CurrentLessons.CurrentItem = item;
            if (appData.Role == "Student")
            {
                GotoQRGeneration();
                //await Shell.Current.GoToAsync($"//{nameof(QRGeneratorPage)}?LessonId={item.Id}");
            }
            else if (appData.Role == "Employee")
            {
                GotoLesson();
                //await Shell.Current.GoToAsync($"//{nameof(LessonPage)}?LessonId={item.Id}&LoadFromContext={string.Empty}");
            }               
        }

        public Action DisplayError;
        public Action Refresh;
        public Action ScheduleNotFound;
        public Action GotoLesson;
        public Action GotoQRGeneration;


        private string error;
        public string Error
        {
            get => error;
            set => SetProperty(ref error, value);
        }
    }
}
