using DrakeAzureDevopsMobile.Interfaces;
using DrakeAzureDevopsMobile.ViewModels;
using DrakeAzureDevopsMobile.ViewModels.Models.Global;

namespace DrakeAzureDevopsMobile.Views;

public partial class NewWorkItemPage : ContentPage
{
    public NewWorkItemPage(NewWorkItemPageViewModel vm)
    {
        BindingContext = vm;
        InitializeComponent();
    }
    
}