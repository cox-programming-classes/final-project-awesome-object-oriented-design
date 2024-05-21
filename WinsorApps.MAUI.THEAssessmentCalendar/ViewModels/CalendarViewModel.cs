using System.Collections.Immutable;
using System.Globalization;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using WinsorApps.Services.Global;
using WinsorApps.Services.Global.Services;

namespace WinsorApps.MAUI.THEAssessmentCalendar.ViewModels;

public partial class CalendarViewModel : ObservableObject
{
    private readonly AssessmentCalService _calendar;
    [ObservableProperty] private DateTime day;
    [ObservableProperty] private ImmutableArray<DayViewModel> days;
    
    // consider adding properties for things like IsMonthlyView or IsWeeklyView
    // and a private field that holds all the assessments that you have cached

    public CalendarViewModel(AssessmentCalService calendar)
    {
        _calendar = calendar;
        day = DateTime.Today;
        var start = new DateTime(day.Year, day.Month, 1);
        var end = start.AddMonths(1).AddDays(-1);
        var task = calendar.GetAssessmentsByDate(start, end);
        
        
        task.WhenCompleted(() =>
        {
            var assessments = task.Result;
            // initialize Days with all the ViewModels in this list that
            // should be displayed in the view.
            var ViewModels = assessments.Select(a => new AssessmentsViewModel(a));
            List<DayViewModel> temp = new();
            var Dates = assessments.Select(a => a.Start.Date).Distinct();
            foreach (var date in Dates)
            {
                
                temp.Add(new DayViewModel("", date, ViewModels.Where(a => a.Start.Date == date)));
            }

            Days = temp;
        });
    }

    [RelayCommand]
    public void PlusOneMonth()
    {
        Day = Day.AddMonths(1);
        // update the Days collection with new stuff
        // that might entail making this an async Task
        // also each of these needs to be a Relay Command as well~
    }

    public void MinusOneMonth()
    {
        // use the capital letter Day variables~
        // using the lowercase version skips all the 
        // Observable Property magic
        day = day.AddMonths(-1);
    }

    public void AddOneWeek()
    {
        day = day.AddDays(7);
    }

    public void MinusOneWeek()
    {
        day = day.AddDays(-7);
    }

}