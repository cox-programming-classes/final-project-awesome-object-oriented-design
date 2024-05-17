using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using WinsorApps.MAUI.THEAssessmentCalendar.Views;
using WinsorApps.Services.Global.Models;
using WinsorApps.Services.Global.Services;

namespace WinsorApps.MAUI.THEAssessmentCalendar.ViewModels;

public partial class AssessmentsViewModel : ObservableObject
{
    /// <summary>
    /// this is the Assessment Model that you will create
    /// the ViewModel around
    /// </summary>
    private readonly AssessmentRecords.Assessment _assessment;

    /// <summary>
    /// This is the dependency injected Service that
    /// lets you do stuff with the assessment
    /// </summary>
    private readonly AssessmentCalService _calendarService;

    [ObservableProperty] private DateTime start;
    [ObservableProperty] private DateTime end;
    [ObservableProperty] private string summary;
    [ObservableProperty] private string description;
    [ObservableProperty] private bool passavailable;
    [ObservableProperty] private bool passused;

/// <summary>
    /// Turn a Model into a ViewModel!
    /// </summary>
    /// <param name="assessment">the Assessment Model that you are handed.</param>
    /// <param name="service">Dependency Injection!</param>
    public AssessmentsViewModel(AssessmentRecords.Assessment assessment, AssessmentCalService service)
    {
        _assessment = assessment;
        _calendarService = service;
        

        //  Initialize all of your ObservableProperties here~
        start = assessment.start;
        end = assessment.end;
        summary = assessment.summary;
        description = assessment.description;
        
        assessment.
    }

    [RelayCommand]
    public async Task RequestLatePass()
    {
        // use the _calendarService to request a LatePass for THIS assessment.
        await _calendarService.PostLatePass(_assessment.id);
    }
}