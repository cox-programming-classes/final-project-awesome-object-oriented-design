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
    await api.Login("jcox@winsor.edu", "not my password", OnError);
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

AssessmentCalService acalService = new(api);
var assessCal = await acalService.GetAssessments();
foreach (var assessment in assessCal)
{
    Console.WriteLine($"Here are your assessments: {assessment.description} " +
                      $"for {assessment.summary} at {assessment.start:dddd dd MMMM} " +
                      $"until {assessment.end:hh:mm tt}"); 
}
