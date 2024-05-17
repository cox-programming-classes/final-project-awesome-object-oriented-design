// See https://aka.ms/new-console-template for more information

using WinsorApps.Services.Global.Models;
using WinsorApps.Services.Global.Services;

#region Service Declarations
LocalLoggingService logging = new();
ApiService api = new(logging);
RegistrarService registrar = new RegistrarService(api, logging);
#endregion

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
Console.WriteLine(assessCal);