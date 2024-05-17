using System.Collections.Immutable;
using System.Globalization;
using CommunityToolkit.Mvvm.ComponentModel;
using WinsorApps.Services.Global.Services;

namespace WinsorApps.MAUI.THEAssessmentCalendar.ViewModels;

public partial class CalendarViewModel : ObservableObject
{
    private readonly AssessmentCalService _Calendar;
    [ObservableProperty] private DateTime day;
    [ObservableProperty] private ImmutableArray<DayViewModel> days;

    public CalendarViewModel(AssessmentCalService calendar)
    {
        _Calendar = calendar;
        day = DateTime.Today;
        var start = new(day.Year, day.Month, 1);
        var end = start.AddMonths(1).AddDays(-1);
        calendar.GetAssessmentsByDate(start, end);
    }

    public void PlusOneMonth()
    {
        day = day.AddMonths(1);
    }

    public void MinusOneMonth()
    {
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