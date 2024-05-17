namespace WinsorApps.Services.Global.Models;

public class AssessmentRecords
{
    public readonly record struct Assessment(string id, string type, string summary, string description, DateTime start, DateTime end, bool allDay, List<string> affectedClasses);

    public readonly record struct LatePass(bool? passUsed, bool? passAvailable);
    
    public readonly record struct Login(string email, string password);

}