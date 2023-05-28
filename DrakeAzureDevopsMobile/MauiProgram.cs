using DrakeAzureDevopsMobile.BusinessControllers;
using DrakeAzureDevopsMobile.BusinessControllers.Interfaces;
using DrakeAzureDevopsMobile.DataServices;
using DrakeAzureDevopsMobile.DataServices.Interfaces;
using DrakeAzureDevopsMobile.Interfaces;
using DrakeAzureDevopsMobile.Services;
using DrakeAzureDevopsMobile.Services.Interfaces;
using DrakeAzureDevopsMobile.Services.LocalNotificationWrapper;
using DrakeAzureDevopsMobile.ViewModels;
using DrakeAzureDevopsMobile.ViewModels.Interfaces;
using DrakeAzureDevopsMobile.Views;
using Plugin.LocalNotification;
using Syncfusion.Maui.Core.Hosting;

namespace DrakeAzureDevopsMobile;

public static class MauiProgram
{
    public static MauiApp CreateMauiApp()
    {
        var builder = MauiApp.CreateBuilder();
        builder
            .UseMauiApp<App>()
            .UseLocalNotification()
            .ConfigureSyncfusionCore()
            .ConfigureFonts(fonts =>
            {
                fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
            });

        SetupServices(builder.Services);
        RegisterNavigation();

        builder.Services.AddTransient<BoardsPage>();
        builder.Services.AddTransient<ReposPage>();
        builder.Services.AddTransient<PipelinesPage>();

        builder.Services.AddTransient<AzureAdLoginPage>();
        builder.Services.AddTransient<AzureAdLoginPageViewModel>();

        builder.Services.AddTransient<RecentlyUpdatedPage>();
        builder.Services.AddTransient<RecentlyUpdatedPageViewModel>();

        builder.Services.AddTransient<ProfilePage>();
        builder.Services.AddTransient<ProfilePageViewModel>();

        builder.Services.AddTransient<NotificationsPage>();
        builder.Services.AddTransient<NotificationsPageViewModel>();

        builder.Services.AddTransient<NewWorkItemPage>();
        builder.Services.AddTransient<NewWorkItemPageViewModel>(); 

        builder.Services.AddTransient<AppShellViewModel>();

        



        return builder.Build();

    }

    private static void SetupServices(IServiceCollection services) 
    {
        services.AddSingleton<ISessionController, SessionController>();
        services.AddSingleton<IPersonController, PersonController>();
        services.AddScoped<IPersonDataService, PersonDataService>();
        services.AddSingleton<INavigationService, NavigationService>();
        services.AddSingleton<IAzureLoginPageViewModel, AzureAdLoginPageViewModel>();
        services.AddSingleton<INotificationServiceOneDrake, NotificationServiceOneDrake>();
        services.AddSingleton<INotificationServiceWrapper, NotificationServiceWrapper>();
        services.AddSingleton<INotificationJsonFileService, NotificationJsonFIleService>();
    }

    
    private static void RegisterNavigation()
    {
        RegisterForNavigation.Register<AzureAdLoginPage>();
        RegisterForNavigation.Register<BoardsPage>();
        RegisterForNavigation.Register<ProfilePage>();
        RegisterForNavigation.Register<PipelinesPage>();
        RegisterForNavigation.Register<RecentlyUpdatedPage>();
        RegisterForNavigation.Register<ReposPage>();
        RegisterForNavigation.Register<NotificationsPage>();
    }
}