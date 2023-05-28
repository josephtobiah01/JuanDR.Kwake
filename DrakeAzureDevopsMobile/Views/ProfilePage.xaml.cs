using DrakeAzureDevopsMobile.Interfaces;
using DrakeAzureDevopsMobile.ViewModels;
using DrakeAzureDevopsMobile.ViewModels.Models.Global;

namespace DrakeAzureDevopsMobile.Views;

public partial class ProfilePage : ContentPage
{
    public ProfilePage(ProfilePageViewModel vm)
	{
		InitializeComponent();
		BindingContext= vm;

    }


}