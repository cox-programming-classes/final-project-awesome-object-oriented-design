using System.Collections.Immutable;
using WinsorApps.Services.Global.Models;

namespace WinsorApps.Services.Global.Services;

/// <summary>
/// Creating the Assessment Cal service (Note: most of this is copy and pasted over from the "Practice with endpoints")
/// </summary>
public class AssessmentCalService : IAsyncInitService
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
// get Assessments by date 
    public async Task<ImmutableArray<AssessmentRecords.Assessment>> GetAssessmentsByDate(DateTime start, DateTime end)
    {
        return await _api.SendAsync<ImmutableArray<AssessmentRecords.Assessment>>(HttpMethod.Get, 
            $"api/assessment-calendar/my-calendar?start={start:yyyy-MM-dd}&end={end:yyyy-MM-dd}");
    }
    
    // Get Assessment info
    public async Task<ImmutableArray<AssessmentRecords.Assessment>> GetAssessmentInfo(string type, string summary,
        string description)
    {
        return await _api.SendAsync<ImmutableArray<AssessmentRecords.Assessment>>(HttpMethod.Get,
            $"api/assessment-calendar/my-calendar");
    }
    
    /// <summary>
    /// My Assessments Cache.
    /// </summary>
    private ImmutableArray<AssessmentRecords.Assessment> _myAssessments;
    
    /// <summary>
    /// Access My Assessments cache.
    /// </summary>
    public ImmutableArray<AssessmentRecords.Assessment> MyAssessments => _myAssessments;

    public async Task<ImmutableArray<AssessmentRecords.Assessment>> GetAssessmentsAsync()
    {
        return await _api.SendAsync<ImmutableArray<AssessmentRecords.Assessment>>(HttpMethod.Get, "api/assessment-calendar/my-calendar");
    }

    public async Task GetAssessmentsAsync(ImmutableArray<AssessmentRecords.Assessment> _myAssessments)
    {
        this._myAssessments = await GetAssessmentsAsync();
    }
    /// <summary>
    /// My Late Pass Cache. 
    /// </summary>
    private ImmutableArray<AssessmentRecords.LatePass> _myLatePasses;

    /// <summary>
    /// Access My Late Passes cache. 
    /// </summary>

    public ImmutableArray<AssessmentRecords.LatePass> MyLatePasses => _myLatePasses;

    public async Task<ImmutableArray<AssessmentRecords.LatePass>> GetLatePassesAsync()
    {
        return await _api.SendAsync<ImmutableArray<AssessmentRecords.LatePass>>(HttpMethod.Get,
            "api/assessment-calendar/students/passes?showPast=true");
    }
    public async Task WithdrawLatePassesAsync(ImmutableArray<AssessmentRecords.LatePass> _myLatePasses)
    {
        this._myLatePasses = await GetLatePassesAsync();
    }
    
    /// <summary>
    /// Use Late Pass
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    public async Task<ImmutableArray<AssessmentRecords.LatePass>> PostLatePass(string id)
    {
        // this endpoint only returns a single late pass~
        // get that in the 
        return await _api.SendAsync<ImmutableArray<AssessmentRecords.LatePass>>(HttpMethod.Post,
            $"api/assessment-calendar/students/passes{id}");
    }
    
        
    // Withdraw late pass - what http method would I use here? (delete?)

    /// <summary>
    /// Initialize the service's stored Cache
    /// </summary>
    /// <param name="onError">Handle an Error from the API</param>
    public async Task Initialize(ErrorAction onError)
    {
        // Can't initialize until the API is ready.
        await _api.WaitForInit(onError);

        Started = true;
        Progress = 0;
        
        // Initialize your cache here~
        // await any api calls that you want
        // to populate in memory.
       
        _myAssessments = await GetAssessmentsAsync();
        _myLatePasses = await GetLatePassesAsync();

        Progress = 1;
        Ready = true;
    }

    /// <summary>
    /// Wait until the service is Ready!
    /// </summary>
    /// <param name="onError"></param>
    public async Task WaitForInit(ErrorAction onError)
    {
        while (!Ready)
            await Task.Delay(250);
    }

    public async Task Refresh(ErrorAction onError)
    {
        Progress = 0;
        await Initialize(onError);
    }

    public bool Started { get; private set; }
    public bool Ready { get; private set; }
    public double Progress { get; private set; }
}