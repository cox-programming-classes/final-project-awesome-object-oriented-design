// See https://aka.ms/new-console-template for more information

using System.IdentityModel.Tokens.Jwt;
using Microsoft.Maui.ApplicationModel.Communication;
using WinsorApps.Services.Global.Models;
using WinsorApps.Services.Global.Services;

#region Service Declarations
LocalLoggingService logging = new();
ApiService api = new(logging);
RegistrarService registrar = new RegistrarService(api, logging);
#endregion

// Calling the assessment calendar service for getting assessments only by type/summary/description
//await api.Login("")

// Do Login Stuff
await api.Initialize(OnError);
int retryCount = 0;
while (!api.Ready && retryCount < 10)
{
    await api.Login("jcox@winsor.edu", "!-8L49snDyYvcNJe29a.p!N4ka3wf", OnError);
    if (!api.Ready)
        retryCount++;
}

if (!api.Ready)
    return;

AssessmentCalService acalService = new(api);
var assessCal = await acalService.GetAssessments();
foreach (var assessment in assessCal)
{
    Console.WriteLine($"Here are your assessments: {assessment.description} for {assessment.summary} at {assessment.type}"); 
}
Console.WriteLine(assessCal);

// Calling the assessment calendar service for getting assessments "by date"
var byDate = await acalService.GetAssessmentsByDate();
foreach (var assessment in assessCal)
{
    Console.WriteLine($"Assessments by date: from {assessment.start} to {assessment.end}");
}

// Initialize Registrar Service

await registrar.Initialize(OnError);

foreach(var item in await registrar.GetMyScheduleAsync())
    Console.WriteLine(item);

Console.WriteLine("Yay!");

void OnError(ErrorRecord err)
{
    Console.ForegroundColor = ConsoleColor.Red;
    Console.WriteLine(err.type);
    Console.WriteLine(err.error);
    Console.ResetColor();
}

/// <summary>
/// Login Model used for Logging into the App!
/// </summary>
/// <param name="email"></param>
/// <param name="password"></param>
public readonly record struct Login(string email, string password);

public sealed record AuthResponse(string userId = "", string jwt = "", DateTime expires = default, string refreshToken = "")
{
    public JwtSecurityToken? GetJwt()
    {
        if (string.IsNullOrEmpty(jwt))
            return null;

        try
        {
            return (JwtSecurityToken)new JwtSecurityTokenHandler().ReadToken(jwt);
        }
        catch
        {
            return null;
        }
    }
}
