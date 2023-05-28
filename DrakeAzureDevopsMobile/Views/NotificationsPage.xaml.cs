using DrakeAzureDevopsMobile.ViewModels;

namespace DrakeAzureDevopsMobile.Views;

public partial class NotificationsPage : ContentPage
{
	public NotificationsPage(NotificationsPageViewModel vm)
	{
		BindingContext = vm;
		InitializeComponent();
	}
}