using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.AppCenter.Crashes;

using SASC_Final.Helpers;
using SASC_Final.Models.Common.AuthModels.Enums;
using SASC_Final.ViewModels;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace SASC_Final.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SchedulePage : ContentPage
    {
        ScheduleViewModel _viewModel;
        public SchedulePage()
        {
            try
            {
                //Navigation.ClearStack();
                BindingContext = _viewModel = new ScheduleViewModel();
                InitializeComponent();
                _viewModel.DisplayError += () => DisplayAlert("Error", _viewModel.Error, "OK");
                _viewModel.ScheduleNotFound += () => { labelNotFound.IsVisible = true; ScheduleListView.IsVisible = false; };
                _viewModel.Refresh += () => { labelNotFound.IsVisible = false; ScheduleListView.IsVisible = true; };
                ScheduleListView.ItemsSource = _viewModel.Items;
                ScheduleListView.ItemTemplate = GetDataTemplate();
                //throw new Exception("MyEx_SchedulePage_SchedulePage");
            }
            catch (Exception ex)
            {
                var properties = new Dictionary<string, string>
                {
                    { "vm", this.BindingContext.ToString() }
                };
                Crashes.TrackError(ex, properties);
            }
        }

        private DataTemplate GetDataTemplate()
        {
            DataTemplate result;
            var AppData = DependencyService.Get<AppData>();
            if (AppData.Role == UserRoles.STUDENT)
            {
                result = new DataTemplate(() =>
                {
                    StackLayout stack = new StackLayout();
                    Label labelStart = new Label { FontAttributes = FontAttributes.Bold }; ;
                    Label labelEnd = new Label();
                    Label labelSubject = new Label { FontAttributes = FontAttributes.Bold }; ;
                    Label labelAuditories = new Label();
                    Label labelGroups = new Label { HorizontalTextAlignment = TextAlignment.End };
                    Label labelEmployee = new Label { HorizontalTextAlignment = TextAlignment.End };

                    labelStart.SetBinding(Label.TextProperty, "Start");
                    labelEnd.SetBinding(Label.TextProperty, "End");
                    labelSubject.SetBinding(Label.TextProperty, "Subject");
                    labelAuditories.SetBinding(Label.TextProperty, "Auditory");
                    labelEmployee.SetBinding(Label.TextProperty, "Employee");

                    var multiBinding = new MultiBinding { StringFormat = "{0}{1}" };
                    multiBinding.Bindings.Add(new Binding("Groups"));
                    multiBinding.Bindings.Add(new Binding("Subgroup"));
                    labelGroups.SetBinding(Label.TextProperty, multiBinding);

                    Grid grid = new Grid
                    {
                        RowDefinitions =
                        {
                            new RowDefinition(),
                            new RowDefinition(),
                        },
                        ColumnDefinitions =
                        {
                            new ColumnDefinition{ Width = new GridLength(2, GridUnitType.Star)},
                            new ColumnDefinition(),
                            new ColumnDefinition{Width  = new GridLength(100)}
                        }
                    };
                    grid.SetBinding(Grid.ClassIdProperty, "Id");
                    grid.Children.Add(labelStart, 0, 0);
                    grid.Children.Add(labelEnd, 0, 1);
                    grid.Children.Add(labelSubject, 1, 0);
                    grid.Children.Add(labelAuditories, 1, 1);
                    grid.Children.Add(labelGroups, 2, 0);
                    grid.Children.Add(labelEmployee, 2, 1);
                    stack.Children.Add(grid);
                    stack.Padding = 10;

                    var tapGestureRecognizer = new TapGestureRecognizer();
                    tapGestureRecognizer.Tapped += (s, e) =>
                    {
                        var item = _viewModel.Items.FirstOrDefault(x => x.Id == grid.ClassId);
                        var closed = item.IsClosed;
                        //убрать отрицание
                        if (!closed)
                        {
                            var diff = item.GetTimeDifference();
                            if (diff > 0)
                            {
                                DisplayAlert("", "Регистрация начнётся за 5 минут до начала занятия.", "Ок");
                            }
                            else
                            {
                                DisplayAlert("", "Регистрация на данное занятие закончено.", "Ок");
                            }
                        }
                        else
                        {
                            _viewModel.SelectedItem = item;
                        }
                    };
                    grid.GestureRecognizers.Add(tapGestureRecognizer);
                    return stack;
                });
            }
            else
            {
                result = new DataTemplate(() =>
                {
                    StackLayout stack = new StackLayout();
                    Label labelStart = new Label { FontAttributes = FontAttributes.Bold }; ;
                    Label labelEnd = new Label();
                    Label labelSubject = new Label { FontAttributes = FontAttributes.Bold }; ;
                    Label labelAuditories = new Label();
                    Label labelGroups = new Label { HorizontalTextAlignment = TextAlignment.End };

                    labelStart.SetBinding(Label.TextProperty, "Start");
                    labelEnd.SetBinding(Label.TextProperty, "End");
                    labelSubject.SetBinding(Label.TextProperty, "Subject");
                    labelAuditories.SetBinding(Label.TextProperty, "Auditory");
                    //labelGroups.SetBinding(Label.TextProperty, "Groups");
                    var multiBinding = new MultiBinding { StringFormat = "{0}{1}" };
                    multiBinding.Bindings.Add(new Binding("Groups"));
                    multiBinding.Bindings.Add(new Binding("Subgroup"));
                    labelGroups.SetBinding(Label.TextProperty, multiBinding);

                    Grid grid = new Grid
                    {
                        RowDefinitions =
                        {
                        new RowDefinition(),
                        new RowDefinition(),
                        },
                        ColumnDefinitions =
                        {
                        new ColumnDefinition{ Width = new GridLength(2, GridUnitType.Star)},
                        new ColumnDefinition(),
                        new ColumnDefinition{Width  = new GridLength(100)}
                        }
                    };
                    grid.SetBinding(Grid.ClassIdProperty, "Id");
                    grid.Children.Add(labelStart, 0, 0);
                    grid.Children.Add(labelEnd, 0, 1);
                    grid.Children.Add(labelSubject, 1, 0);
                    grid.Children.Add(labelAuditories, 1, 1);
                    grid.Children.Add(labelGroups, 2, 1);
                    var recognizer = new TapGestureRecognizer
                    {
                        NumberOfTapsRequired = 1
                    };
                    recognizer.Tapped += (s, e) =>
                    {
                        var item = _viewModel.Items.FirstOrDefault(x => x.Id == grid.ClassId);
                        var closed = item.IsClosed;
                        //убрать отрицание
                        if (!closed)
                        {
                            if (item.IsClosedByEmployee)
                            {
                                DisplayAlert("", "Посещаемость по этому занятию уже была отправлена.", "Ок");
                            }
                            else
                            {
                                var diff = item.GetTimeDifference();
                                if (diff > 0)
                                {
                                    DisplayAlert("", "Регистрация начнётся за 5 минут до начала занятия.", "Ок");
                                }
                                else
                                {
                                    DisplayAlert("", "Регистрация на данное занятие закончено.", "Ок");
                                }
                            }
                        }
                        else
                        {
                            _viewModel.SelectedItem = item;
                        }
                    };
                    grid.GestureRecognizers.Add(recognizer);
                    stack.Children.Add(grid);
                    stack.Padding = 10;
                    return stack;
                });
            }
            return result;
        }
        protected override void OnAppearing()
        {
            base.OnAppearing();
            _viewModel.OnAppearing();
        }
    }
}