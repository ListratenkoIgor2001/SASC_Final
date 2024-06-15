using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using SASC_Final.Models;
using SASC_Final.Models.Common;
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
                LoadLessonDetails(lessonId);
            }
        }
        private string loadFromContext;
        public string LoadFromContext
        {
            get => loadFromContext;
            set
            {
                loadFromContext = value;
                OnPropertyChanged();
            }
        }
        private async void LoadLessonDetails(string lessonId)
        {
            if (!string.IsNullOrEmpty(lessonId))
            {
                var AppData = DependencyService.Get<AppData>();
                var lesson = AppData.CurrentLessons.Items.FirstOrDefault(x => x.Id == LessonId);
                if (lesson != null)
                {
                    BindingContext = _viewModel = new LessonViewModel(lesson.LessonId);
                    _viewModel.DisplayError += () => DisplayAlert("Error", _viewModel.Error, "OK");
                    _viewModel.ScheduleNotFound += () => { labelNotFound.IsVisible = true; GroupsList.IsVisible = false; };
               
                    await _viewModel.LoadStudents(!string.IsNullOrEmpty(LoadFromContext));
                    this.GroupsList.ItemsSource = _viewModel.Groups;
                }
                else {
                    await DisplayAlert("Error", _viewModel.Error, "OK");
                    await Shell.Current.GoToAsync("..");
                }
                //UpdateAttendanceList();
            }
        }
        LessonViewModel _viewModel;
        public LessonPage()
        {
            InitializeComponent();
            //_viewModel.ScheduleNotFound += () => { labelNotFound.IsVisible = true; ScheduleListView.IsVisible = false; };
            //_viewModel.Refresh += () => { labelNotFound.IsVisible = false; ScheduleListView.IsVisible = true; };
            //ScheduleListView.ItemsSource = _viewModel.Items;
            //ScheduleListView.ItemTemplate = GetDataTemplate();
            //LessonListView.BindingContext = _viewModel;
            //LessonListView.ItemsSource = _viewModel.Items2;
            //LessonListView.IsGroupingEnabled = true;
            //LessonListView.GroupDisplayBinding = new Binding("Key");
            //LessonListView.ItemTemplate = GetDataTemplate();
        }
        protected override void OnAppearing()
        {
            base.OnAppearing();
        }
        private async void ScanButton_Clicked(object sender, EventArgs e)
        {
            await Shell.Current.GoToAsync($"//{nameof(QRScannerPage2)}");
        }

        private async void SendButton_Clicked(object sender, EventArgs e)
        {
            bool answer = await DisplayAlert("Подтверждение", "Отправить данные?", "Да", "Нет");
            if (answer)
            {
                _viewModel.SendAttendances();
            }
        }
        private DataTemplate GetDataTemplate2()
        {
            DataTemplate result;
            result = new DataTemplate(() =>
            {
                StackLayout stack = new StackLayout();
                Grid grid = new Grid
                {
                    RowDefinitions =
                    {
                        new RowDefinition(),
                        new RowDefinition(),
                        new RowDefinition()
                    },
                    ColumnDefinitions =
                    {
                        new ColumnDefinition{ Width = new GridLength(2, GridUnitType.Star)},
                        new ColumnDefinition(),
                        new ColumnDefinition(),
                        new ColumnDefinition{Width  = new GridLength(100)}
                    }
                };
                grid.SetBinding(Grid.ClassIdProperty, "RecordbookNumber");
                Image image = new Image { HeightRequest = 120, WidthRequest = 100 };
                Label labelFirstname = new Label();
                Label labelLastname = new Label();
                Label LabelMiddlename = new Label();
                Label labelAttendanceTime = new Label();
                Switch switchPresent = new Switch();
                image.SetBinding(Image.SourceProperty, "ImageUrl");
                labelAttendanceTime.SetBinding(Label.TextProperty, "Auditory");
                labelFirstname.SetBinding(Label.TextProperty, "Start");
                labelLastname.SetBinding(Label.TextProperty, "End");
                LabelMiddlename.SetBinding(Label.TextProperty, "Subject");
                switchPresent.SetBinding(Switch.IsToggledProperty, "IsPresent", BindingMode.TwoWay);

                grid.Children.Add(image, 0, 0);
                grid.Children.Add(labelAttendanceTime, 1, 1);
                grid.Children.Add(labelLastname, 2, 0);
                grid.Children.Add(labelFirstname, 2, 1);
                grid.Children.Add(LabelMiddlename, 2, 2);
                grid.Children.Add(switchPresent, 3, 1);
                stack.Children.Add(grid);
                stack.Padding = 10;
                return stack;
            });
            return result;
        }
        private DataTemplate GetDataTemplate()
        {
            DataTemplate result;
            result = new DataTemplate(() =>
            {
                StackLayout stack = new StackLayout();
                Grid grid = new Grid
                {
                    RowDefinitions =
                    {
                        new RowDefinition(),
                        new RowDefinition(),
                        new RowDefinition()
                    },
                    ColumnDefinitions =
                    {
                        new ColumnDefinition{ Width = new GridLength(2, GridUnitType.Star)},
                        new ColumnDefinition(),
                        new ColumnDefinition(),
                        new ColumnDefinition{Width  = new GridLength(100)}
                    }
                };
                grid.SetBinding(Grid.ClassIdProperty, "RecordbookNumber");
                Image image = new Image { HeightRequest = 120, WidthRequest = 100 };
                Label labelFirstname = new Label();
                Label labelLastname = new Label();
                Label LabelMiddlename = new Label();
                Label labelAttendanceTime = new Label();
                Switch switchPresent = new Switch();
                image.SetBinding(Image.SourceProperty, "ImageUrl");
                labelAttendanceTime.SetBinding(Label.TextProperty, "Auditory");
                labelFirstname.SetBinding(Label.TextProperty, "Start");
                labelLastname.SetBinding(Label.TextProperty, "End");
                LabelMiddlename.SetBinding(Label.TextProperty, "Subject");
                switchPresent.SetBinding(Switch.IsToggledProperty, "IsPresent", BindingMode.TwoWay);

                grid.Children.Add(image, 0, 0);
                grid.Children.Add(labelAttendanceTime, 1, 1);
                grid.Children.Add(labelLastname, 2, 0);
                grid.Children.Add(labelFirstname, 2, 1);
                grid.Children.Add(LabelMiddlename, 2, 2);
                grid.Children.Add(switchPresent, 3, 1);
                stack.Children.Add(grid);
                stack.Padding = 10;
                return stack;
            });
            return result;
        }
    }
}