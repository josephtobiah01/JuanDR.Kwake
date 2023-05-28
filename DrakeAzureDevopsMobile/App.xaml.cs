using DrakeAzureDevopsMobile.Interfaces;
using DrakeAzureDevopsMobile.Services.Interfaces;
using DrakeAzureDevopsMobile.ViewModels;
using Plugin.LocalNotification;
using System.Diagnostics;
using Application = Microsoft.Maui.Controls.Application;
using Timer = System.Timers.Timer;

namespace DrakeAzureDevopsMobile;

public partial class App : Application
{



    #region Fields
    private readonly NotificationsPageViewModel _notificationsPageViewModel;
    private readonly INotificationServiceWrapper _notificationServiceWrapper;
    private readonly INotificationJsonFileService _notificationJsonFileService;
    private IAzureLoginPageViewModel _azureLoginPageViewModel;
    private INotificationService _notificationService;
    private INavigationService _navigationService;
    private Timer IdleTimer = new Timer(60 * 2000);
    private INotificationServiceOneDrake _notificationServiceOneDrake;

    #endregion Fields




    #region Properties

    
    #endregion Properties




    #region Constructor
    public App(
        NotificationsPageViewModel notificationsPageViewModel,
        INotificationServiceWrapper notificationServiceWrapper,
        INotificationJsonFileService notificationJsonFileService,
        IAzureLoginPageViewModel azureLoginPageViewModel,
        INotificationService notificationService,
        INavigationService navigationService,
        INotificationServiceOneDrake notificationServiceOneDrake)
    {
        _notificationsPageViewModel = notificationsPageViewModel;
        _notificationServiceWrapper = notificationServiceWrapper;
        _notificationJsonFileService = notificationJsonFileService;
        _azureLoginPageViewModel = azureLoginPageViewModel ?? throw new ArgumentNullException(nameof(azureLoginPageViewModel));
        _notificationService = notificationService ?? throw new ArgumentNullException(nameof(notificationService));
        _navigationService = navigationService ?? throw new ArgumentNullException(nameof(navigationService));
        _notificationServiceOneDrake = notificationServiceOneDrake ?? throw new ArgumentNullException(nameof(notificationServiceOneDrake));

        InitializeComponent();

        

        MainPage = new AppShell();

        IdleTimer.Elapsed += IdleTimer_Elapsed;

        _notificationServiceWrapper.NotificationReceived += _notificationServiceWrapper_NotificationReceived;

        

        LocalNotificationCenter.Current.NotificationActionTapped += Current_NotificationActionTapped;

    }

    private void _notificationServiceWrapper_NotificationReceived(Plugin.LocalNotification.EventArgs.NotificationEventArgs e)
    {
        try
        {
            _notificationJsonFileService.AddNotification(e.Request);
            Debug.WriteLine("Notification added successfully.");
        }
        catch (Exception ex)
        {
            Debug.WriteLine($"Failed to add notification: {ex.Message}");
        }

        MessagingCenter.Send<App, bool>(this, "ReloadNotifications", true);

    }
    #endregion Constructor




    #region Methods

    private void Current_NotificationActionTapped(Plugin.LocalNotification.EventArgs.NotificationActionEventArgs e)
    {
        if (e.IsTapped)
        {
            if (_notificationServiceOneDrake.IsNotificationAfterSignOut == true)
            {
                Shell.Current.GoToAsync("//AzureAdLoginPage");
            }
            else
            {
                Shell.Current.GoToAsync("//NotificationsPage");
            }
        }
    }


    async void IdleTimer_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
    {
        if (MainThread.IsMainThread)
        {
            _notificationServiceOneDrake.IsNotificationAfterSignOut = true;

            IdleTimer.Stop();

            await App.Current.MainPage.DisplayAlert("Session Expired", "Please Sign In Again", "OK");

            await Shell.Current.GoToAsync("//AzureAdLoginPage");

            _azureLoginPageViewModel.EmailAddSignInIsVisible = true;

            _azureLoginPageViewModel.BiometricsSignInIsVisible = false;

        }
        else
        {
            MainThread.BeginInvokeOnMainThread(async () =>
            {
                _notificationServiceOneDrake.IsNotificationAfterSignOut = true;

                IdleTimer.Stop();

                await App.Current.MainPage.DisplayAlert("Session Expired", "Please Sign In Again", "OK");

                await Shell.Current.GoToAsync("//AzureAdLoginPage");

                _azureLoginPageViewModel.EmailAddSignInIsVisible = true;

                _azureLoginPageViewModel.BiometricsSignInIsVisible = false;

            });
        }
    }

    public void StartTimer()
    {
        IdleTimer.Start();
    }

    public void ResetIdleTimer()
    {

        if (_notificationServiceOneDrake.IsNotificationAfterSignOut == false)
        {
            IdleTimer.Stop();
            IdleTimer.Start();
        }
        else
        {
            IdleTimer.Stop();
        }

    }

    protected override void OnStart()
    {
        base.OnStart();
        _notificationJsonFileService.CreateFile();
    }


    #endregion Methods




}
