using System.Collections.Immutable;
using CommunityToolkit.Mvvm.ComponentModel;

namespace WinsorApps.MAUI.THEAssessmentCalendar.ViewModels;

public partial class CalendarDayViewModel : ObservableObject
{
    [ObservableProperty] private string cycleDay;
    [ObservableProperty] private DateTime date;
    [ObservableProperty] private ImmutableArray<AssessmentsViewModel> assessments;

    /// <summary>
    /// Initialize a single Day on the Assessment Calendar
    /// </summary>
    public CalendarDayViewModel(
        string cycleDay, 
        DateTime date, 
        ImmutableArray<AssessmentsViewModel> assessments)
    {
        
    }
}