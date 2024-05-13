using WinsorApps.MAUI.Shared.Pages;
using WinsorApps.MAUI.Shared.ViewModels;
using WinsorApps.Services.Global.Services;

namespace WinsorApps.MAUI.THEAssessmentCalendar;

public partial class MainPage : ContentPage
{
    public MainPage(RegistrarService registrar, ApiService api, LocalLoggingService logging)
    {
        MainPageViewModel vm = new(
        [
            new(registrar, "Registrar Data")
        ]);
        BindingContext = vm;

        LoginPage loginPage = new(logging, vm.LoginVM);
        loginPage.OnLoginComplete += (_, _) => Navigation.PopAsync();

        Navigation.PushAsync(loginPage);
        InitializeComponent();
    }
}