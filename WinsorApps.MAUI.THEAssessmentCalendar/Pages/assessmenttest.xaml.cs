using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WinsorApps.MAUI.THEAssessmentCalendar.ViewModels;

namespace WinsorApps.MAUI.THEAssessmentCalendar.Pages;

public partial class assessmenttest : ContentPage
{
    public assessmenttest(AssessmentCollectionViewModel vm)
    {
        BindingContext = vm;
        InitializeComponent();
    }
}