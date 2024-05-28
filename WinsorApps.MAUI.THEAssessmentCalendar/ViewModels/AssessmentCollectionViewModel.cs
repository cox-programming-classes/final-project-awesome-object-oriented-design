using System.Collections.Immutable;
using CommunityToolkit.Mvvm.ComponentModel;
using WinsorApps.Services.Global.Services;

namespace WinsorApps.MAUI.THEAssessmentCalendar.ViewModels;

public partial class AssessmentCollectionViewModel: ObservableObject
{
    [ObservableProperty] private ImmutableArray<AssessmentsViewModel> assessments;

    public AssessmentCollectionViewModel(AssessmentCalService calendar)
    {
        assessments = calendar.MyAssessments.Select(a=>new AssessmentsViewModel(a, calendar)).ToImmutableArray();
    }
}