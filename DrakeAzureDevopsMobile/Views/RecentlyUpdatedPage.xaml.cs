using DrakeAzureDevopsMobile.Interfaces;
using DrakeAzureDevopsMobile.ViewModels;
using System.Diagnostics;
using DrakeAzureDevopsMobile.ViewModels.Models.Global;

namespace DrakeAzureDevopsMobile.Views;

public partial class RecentlyUpdatedPage : ContentPage
{
    public RecentlyUpdatedPage(RecentlyUpdatedPageViewModel vm)
	{
		InitializeComponent();
		this.BindingContext= vm;

		

	}

}