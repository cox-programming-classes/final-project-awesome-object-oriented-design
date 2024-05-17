using System.Collections.Immutable;
using WinsorApps.Services.Global.Models;

namespace WinsorApps.Services.Global.Services;

/// <summary>
/// Creating the Assessment Cal service (Note: most of this is copy and pasted over from the "Practice with endpoints")
/// </summary>
public class AssessmentCalService
{
    private readonly ApiService _api; //dependency injection

    public AssessmentCalService(ApiService api)
    {
        _api = api;
    }

    public async Task<ImmutableArray<AssessmentRecords.Assessment>> GetAssessments(bool detailed = false)
    {
        return await _api.SendAsync<ImmutableArray<AssessmentRecords.Assessment>>(HttpMethod.Get, $"api/assessment-calendar/my-calendar");

    }

    //public async Task<ImmutableArray<Assessment>> GetAssessments(string id, string type, string summary,
    //string description, DateTime start, DateTime end, bool allDay, List<string> affectedClasses);
    //{
   //     return await _api.SendAsync<>()
  //  }
}