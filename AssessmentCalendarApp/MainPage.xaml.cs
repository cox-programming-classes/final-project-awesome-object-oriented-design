using WinsorApps.MAUI.Shared.Pages;
using WinsorApps.MAUI.Shared.ViewModels;
using WinsorApps.Services.Global.Services;

namespace AssessmentCalendarApp
{
    public partial class MainPage : ContentPage
    {

        public MainPage(RegistrarService registrar, ApiService api, LocalLoggingService logging, AssessmentCalService calendar)

        {
            MainPageViewModel vm = new(
            [
                new(registrar, "Registrar Data"),  
                new ServiceAwaiterViewModel(calendar,"AssessmentCalendar")
            ]);
            BindingContext = vm;
            InitializeComponent();

            LoginPage loginPage = new(logging, vm.LoginVM);
            loginPage.OnLoginComplete += (_, _) => Navigation.PopAsync();

            Navigation.PushAsync(loginPage);
        }
    }
}
