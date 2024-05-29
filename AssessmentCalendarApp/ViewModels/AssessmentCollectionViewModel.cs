using System.Collections.Immutable;
using AsyncAwaitBestPractices;
using CommunityToolkit.Mvvm.ComponentModel;
using WinsorApps.Services.Global;
using WinsorApps.Services.Global.Models;
using WinsorApps.Services.Global.Services;

namespace AssessmentCalendarApp.ViewModels;

public partial class AssessmentCollectionViewModel: ObservableObject
{
    [ObservableProperty] private ImmutableArray<AssessmentsViewModel> assessments = [];

    public event EventHandler<ErrorRecord>? OnError;

    public AssessmentCollectionViewModel(AssessmentCalService calendar)
    {
        var initTask = calendar.WaitForInit(err => OnError?.Invoke(this, err));

        initTask.WhenCompleted(() =>
        {
            var task = calendar.GetAssessmentsByDate(new(2023, 08, 01), new(2024, 06, 01));
            task.WhenCompleted(() =>
            {
                Assessments = task.Result.Select(a => new AssessmentsViewModel(a, calendar)).OrderByDescending(a => a.Start).ToImmutableArray();
            });
        });
        initTask.SafeFireAndForget();
    }
}