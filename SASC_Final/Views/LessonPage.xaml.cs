using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using SASC_Final.Models;
using SASC_Final.Models.Common;
using SASC_Final.Services;
using SASC_Final.ViewModels;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace SASC_Final.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    [QueryProperty(nameof(LessonId), nameof(LessonId))]
    [QueryProperty(nameof(LoadFromContext), nameof(LoadFromContext))]
    public partial class LessonPage : ContentPage
    {
        private string lessonId;
        public string LessonId
        {
            get => lessonId;
            set
            {
                lessonId = value;
                OnPropertyChanged();
            }
        }
        private string loadFromContext = null;
        public string LoadFromContext
        {
            get => loadFromContext;
            set
            {
                loadFromContext = value;
                OnPropertyChanged();
                //LoadLessonDetails(lessonId);
            }
        }
        private async void LoadLessonDetails(string lessonId)
        {
            var AppData = DependencyService.Get<AppData>();
            if (lessonId == null)
            {
                lessonId = AppData.CurrentLessons.CurrentItem.Id;
                loadFromContext = "true";
            }
            if (!string.IsNullOrEmpty(lessonId))
            {
                var lesson = AppData.CurrentLessons.Items.FirstOrDefault(x => x.Id == lessonId);
                if (lesson != null)
                {
                    //BindingContext = _viewModel = new LessonViewModel(lesson.LessonId);
                    BindingContext = _viewModel = new LessonViewModel();
                    _viewModel.DisplayError += () => DisplayAlert("Error", _viewModel.Error, "OK");
                    _viewModel.SuccessSend += () => DisplayAlert("", "Статистика посещений успешно отправлена", "OK");
                    _viewModel.GoBack += async() => await Navigation.PopModalAsync();

                    _viewModel.ScheduleNotFound += () => { labelNotFound.IsVisible = true; GroupsList.IsVisible = false; };
               
                    await _viewModel.LoadStudents(!string.IsNullOrEmpty(LoadFromContext));
                    this.GroupsList.ItemsSource = _viewModel.Groups;
                }
                else {
                    await DisplayAlert("Error", "Внутренняя ошибка. Урок не найден. ", "OK");
                }
            }
            else
            {
                await DisplayAlert("Error", "Внутренняя ошибка. Урок не найден. ", "OK");
            }
        }
        LessonViewModel _viewModel;
        public LessonPage()
        {
            InitializeComponent();
            LoadLessonDetails(DependencyService.Get<AppData>().CurrentLessons.CurrentItem.Id);
        }
        protected override void OnAppearing()
        {
            base.OnAppearing();
        }

        protected override void OnDisappearing()
        {
            base.OnDisappearing();
        }

        private async void SettingsButton_Clicked(object sender, EventArgs e) 
        {
            await Navigation.PushModalAsync(new SettingsPage());
        }


        private async void ScanButton_Clicked(object sender, EventArgs e)
        {
             await Navigation.PushModalAsync(new QRScannerPage2());
            //await Shell.Current.GoToAsync($"//{nameof(QRScannerPage2)}");
        }

        private async void SendButton_Clicked(object sender, EventArgs e)
        {
            bool answer = await DisplayAlert("Закончить", "Отправить данные? Регистрация будет завершена", "Да", "Нет");
            if (answer)
            {
                _viewModel.SendAttendances();
            }
        }
    }
}