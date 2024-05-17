using CommunityToolkit.Mvvm.ComponentModel;

namespace WinsorApps.MAUI.THEAssessmentCalendar.ViewModels;

public partial class AssessmentsViewModel : ObservableObject
{
    [ObservableProperty] private DateTime start;
    [ObservableProperty] private DateTime end;
    [ObservableProperty] private string summary;
    [ObservableProperty] private string description;
    
    

}