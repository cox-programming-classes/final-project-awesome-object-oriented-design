using AssessmentCalendarApp.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AssessmentCalendarApp.Pages;

public partial class assessmenttest : ContentPage
{
    public assessmenttest(AssessmentCollectionViewModel vm)
    {
        InitializeComponent();
        BindingContext = vm;
    }
}