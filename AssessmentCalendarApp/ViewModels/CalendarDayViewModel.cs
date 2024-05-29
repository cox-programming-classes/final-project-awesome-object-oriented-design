using System.Collections.Immutable;
using CommunityToolkit.Mvvm.ComponentModel;

namespace AssessmentCalendarApp.ViewModels;

public partial class DayViewModel : ObservableObject
{
    [ObservableProperty] private string cycleDay;
    [ObservableProperty] private DateTime date;
    [ObservableProperty] private ImmutableArray<AssessmentsViewModel> assessments;

    /// <summary>
    /// Initialize a single Day on the Assessment Calendar
    /// </summary>
    public DayViewModel(
        string cycleDay, 
        DateTime date, 
        ImmutableArray<AssessmentsViewModel> assessments)
    {
        
    }
}