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
// get Assesments by date 
    public async Task<ImmutableArray<AssessmentRecords.Assessment>> GetAssessmentsByDate(DateTime start, DateTime end)
    {
        return await _api.SendAsync<ImmutableArray<AssessmentRecords.Assessment>>(HttpMethod.Get, $"api/assessment-calendar/my-calendar");
    }
    
    // Get Assessment info
    public async Task<ImmutableArray<AssessmentRecords.Assessment>> GetAssessmentInfo(string type, string summary,
        string description)
    {
        return await _api.SendAsync<ImmutableArray<AssessmentRecords.Assessment>>(HttpMethod.Get,
            $"api/assessment-calendar/my-calendar");
    }
    
    // Use late pass command 

    public async Task<ImmutableArray<AssessmentRecords.LatePass>> PostLatePass(bool latepassused,
        bool latepassavailable)
    {
        return await _api.SendAsync<ImmutableArray<AssessmentRecords.LatePass>>(HttpMethod.Post,
            $"api/assessment-calendar/students/passes");
    }
        
    // Withdraw late pass - what http method would I use here? (delete?)
    
}