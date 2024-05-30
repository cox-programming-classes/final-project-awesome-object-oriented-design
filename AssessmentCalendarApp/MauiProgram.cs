using AsyncAwaitBestPractices;
using CommunityToolkit.Maui.Core;
using Microsoft.Extensions.Logging;
using WinsorApps.MAUI.Shared;
using WinsorApps.MAUI.Shared.Pages;
using AssessmentCalendarApp.ViewModels;
using WinsorApps.Services.Global;
using WinsorApps.Services.Global.Services;
using AssessmentCalendarApp.Pages;

namespace AssessmentCalendarApp
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                })
            .UseMauiCommunityToolkitCore();

            builder.Services.AddSingleton<ApiService>();
            builder.Services.AddSingleton<RegistrarService>();
            builder.Services.AddSingleton<LocalLoggingService>();

            builder.Services.AddSingleton<MainPage>();
            builder.Services.AddSingleton<LoginPage>();
            builder.Services.AddSingleton<AssessmentCalService>();
            builder.Services.AddSingleton<AssessmentCollectionViewModel>();
            builder.Services.AddSingleton<assessmenttest>();


#if DEBUG
            builder.Logging.AddDebug();
#endif

            var app = builder.Build();

            ServiceHelper.Initialize(app.Services);

            var logging = ServiceHelper.GetService<LocalLoggingService>()!;
            ServiceHelper.GetService<ApiService>()!.Initialize(err => logging.LogMessage(LocalLoggingService.LogLevel.Error,
                err.type, err.error)).SafeFireAndForget(e => e.LogException(logging));
            return app;
        }
    }
}
