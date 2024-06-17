﻿using System;
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
            LoadItemsCommand = new Command(async () => await ExecuteLoadItemsCommand());
            ItemTapped = new Command<Lesson>(OnItemSelected);           
        }
        private string GetTitle()
        {
            return $"{new CultureInfo("ru-RU").DateTimeFormat.GetDayName(DateTime.Today.DayOfWeek)} {DateTime.Today.ToString("dd.MM.yyyy")}";
        }
        public async Task ExecuteLoadItemsCommand()
        {
            IsBusy = true;
            var AppData = DependencyService.Get<AppData>();
            try
            {
                AppData.CurrentLessons.Clear();
                Items.Clear();
                var _service = DependencyService.Get<ISchedule>();
                var itemsResult = await _service.LoadSchedule();

                var week = AppData.CurrentWeek;
                var items = itemsResult;
                if (items.Count == 0) ScheduleNotFound();
                foreach (var item in items)
                {
                    //Items.Add(new Lesson(item.Id.ToString(), item));
                    Items.Add(new Lesson(Guid.NewGuid().ToString(), item));
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
            ExecuteLoadItemsCommand().Wait();
            SelectedItem = null;
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
                appData.SelectedLesson = item;
                await Shell.Current.GoToAsync($"//{nameof(QRGeneratorPage)}?LessonId={item.Id}");
            }
            else if (appData.Role == "Employee")
            {
                await Shell.Current.GoToAsync($"//{nameof(LessonPage)}?LessonId={item.Id}&LoadFromContext={string.Empty}");
            }               
        }

        public Action DisplayError;
        public Action Refresh;
        //public Action ItemClicked;
        public Action ScheduleNotFound;
        private string error;
        public string Error
        {
            get => error;
            set => SetProperty(ref error, value);
        }
    }
}