using DrakeAzureDevopsMobile.ViewModels;

namespace DrakeAzureDevopsMobile.Views;

public partial class AzureAdLoginPage : ContentPage
{
	public AzureAdLoginPage(AzureAdLoginPageViewModel vm)
	{
		InitializeComponent();
		this.BindingContext= vm;
    }
}