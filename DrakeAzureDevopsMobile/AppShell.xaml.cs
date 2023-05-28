using DrakeAzureDevopsMobile.ViewModels;
using DrakeAzureDevopsMobile.Views;

namespace DrakeAzureDevopsMobile;

public partial class AppShell : Shell
{
    public AppShell()
	{
		
		InitializeComponent();

        
        


        Routing.RegisterRoute(nameof(BoardsPage), typeof(BoardsPage));
		Routing.RegisterRoute(nameof(ReposPage), typeof(ReposPage));
		Routing.RegisterRoute(nameof(PipelinesPage), typeof(PipelinesPage));
		Routing.RegisterRoute(nameof(RecentlyUpdatedPage), typeof(RecentlyUpdatedPage));
		Routing.RegisterRoute(nameof(AzureAdLoginPage), typeof(AzureAdLoginPage));
		Routing.RegisterRoute(nameof(ProfilePage), typeof(ProfilePage));
		Routing.RegisterRoute(nameof(MainPage), typeof(MainPage));
		Routing.RegisterRoute(nameof(NotificationsPage), typeof(NotificationsPage));

	}
	

}
