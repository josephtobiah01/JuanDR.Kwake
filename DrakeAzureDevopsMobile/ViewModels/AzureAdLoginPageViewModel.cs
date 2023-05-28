using CommunityToolkit.Mvvm.Input;
using DrakeAzureDevopsMobile.MsalClient;
using Microsoft.Identity.Client;
using DrakeAzureDevopsMobile.Models;
using DrakeAzureDevopsMobile.BusinessControllers.Interfaces;
using DrakeAzureDevopsMobile.Services.Interfaces;
using DrakeAzureDevopsMobile.Views;
using Plugin.Fingerprint;
using DrakeAzureDevopsMobile.Interfaces;
using System.Diagnostics;
using DrakeAzureDevopsMobile.ViewModels.Models.Global;
using DrakeAzureDevopsMobile.ViewModels.Interfaces;
using Plugin.LocalNotification;
using Plugin.LocalNotification.EventArgs;
using DrakeAzureDevopsMobile.Services.LocalNotificationWrapper;

namespace DrakeAzureDevopsMobile.ViewModels
{
    public partial class AzureAdLoginPageViewModel : VIewModelBase, IAzureLoginPageViewModel
    {


        #region Fields

        private INotificationServiceWrapper _notificationServiceWrapper;
        private INotificationService _notificationService;
        private readonly ISessionController _sessionController;
        private string _accessToken = string.Empty;
        private bool _isLoggedIn = false;
        private bool _isRunning = false;
        private bool _emailAddSignInIsVisible = true;
        private bool _biometricsSignInIsVisible = false;
        private INotificationServiceOneDrake _notificationServiceOneDrake;

        #endregion Fields



        #region Properties


        public bool IsLoggedIn
        {
            get => _isLoggedIn;
            set
            {
                if (value == _isLoggedIn) return;
                _isLoggedIn = value;
            }
        }

        public string AccessToken
        {
            get => _accessToken;
            set => SetPropertyValue(ref _accessToken, value);
        }

        public bool IsRunning
        {
            get => _isRunning;
            set=> SetPropertyValue(ref _isRunning, value);
        }
        public bool EmailAddSignInIsVisible
        {
            get => _emailAddSignInIsVisible;
            set => SetPropertyValue(ref _emailAddSignInIsVisible, value);
        }

        public bool BiometricsSignInIsVisible
        {
            get => _biometricsSignInIsVisible;
            set =>  SetPropertyValue(ref _biometricsSignInIsVisible, value);
        }
        
        #endregion Properties



        #region Constructor

        public AzureAdLoginPageViewModel(
            INotificationServiceWrapper notificationServiceWrapper,
            ISessionController sessionController,
            INavigationService navigationService,
            INotificationService notificationService,
            INotificationServiceOneDrake notificationServiceOneDrake) : base(navigationService)
        {
            _notificationServiceWrapper = notificationServiceWrapper ?? throw new ArgumentNullException(nameof(notificationServiceWrapper));
            _notificationService = notificationService ?? throw new ArgumentNullException(nameof(notificationService));
            _sessionController = sessionController ?? throw new ArgumentNullException(nameof(sessionController));
            _notificationServiceOneDrake = notificationServiceOneDrake ?? throw new ArgumentNullException(nameof(notificationServiceOneDrake));

            Initialize();

            


        }




        #endregion Constructor



        #region Methods


        private Task ShowOkMessage(string title, string message, string cancel = "OK")
        {

            App.Current.MainPage.DisplayAlert(title, message, cancel);
            return Task.CompletedTask;

        }

        private void Initialize()
        {
            EmailAddSignInIsVisible = true;
            BiometricsSignInIsVisible = false;
            _notificationServiceOneDrake.IsNotificationAfterSignOut = true;
        }


        [RelayCommand]
        async Task SignIn()
        {
            IsRunning = true;
           
            await AcquireToken().ContinueWith((x) =>
            {
                IsRunning = false;
                
            });
        }

        [RelayCommand]
        async Task BiometricsSignIn()
        {
            if(_sessionController.AccessToken == null)
            {
                EmailAddSignInIsVisible = true;
                BiometricsSignInIsVisible = false;

                await ShowOkMessage("Sorry", "Your session has expired. Sign in with your email.");
            }
            else 
            {

                var availability = await CrossFingerprint.Current.IsAvailableAsync();

                if (!availability)
                {
                    await ShowOkMessage("Biometrics Failed", "Register your biometrics information on this mobile device, or use a biometrics capable device");

                    return;
                }

                var authResult = await CrossFingerprint.Current.AuthenticateAsync(new Plugin.Fingerprint.Abstractions.AuthenticationRequestConfiguration("Sign In", "Please scan your fingerprint"));

                if (authResult.Authenticated)
                {
                    _notificationServiceOneDrake.IsNotificationAfterSignOut = false;

                    (App.Current as App).ResetIdleTimer();

                    await MainThread.InvokeOnMainThreadAsync(async () =>
                    {

                        await Shell.Current.GoToAsync("//RecentlyUpdatedPage");
                    });

                    BiometricsSignInIsVisible = false;
                    EmailAddSignInIsVisible = true;
                }
            }
        }

        

        private async Task AcquireTokenInitial()
        {
            if(_sessionController.AccessToken == null)
            {
                try
                {
                    // Attempt silent login, and obtain access token.
                    var result = await PCAWrapper.Instance.AcquireTokenSilentAsync(PCAWrapper.Scopes);
                    if (result != null)
                    {
                        IsLoggedIn = true;

                        _sessionController.AccessToken = result.AccessToken;


                        //await Task.Delay(2000).ContinueWith(async (x) => 
                        //{
                        //});

                        await MainThread.InvokeOnMainThreadAsync(async () =>
                        {
                            await Shell.Current.GoToAsync("//RecentlyUpdatedPage", true);
                        });

                    }

                }
                // A MsalUiRequiredException will be thrown, if this is the first attempt to login, or after logging out.
                catch (MsalUiRequiredException)
                {
                    await AcquireToken();
                }
                catch (Exception ex)
                {
                    IsLoggedIn = false;
                    await ShowOkMessage("Exception in AcquireTokenSilentAsync", ex.Message).ConfigureAwait(false);
                }
            }

            else
            {
                EmailAddSignInIsVisible = false;
                BiometricsSignInIsVisible = true;
            }
            
        }   
        private async Task AcquireToken()
        {
            if (_sessionController.AccessToken == null)
            {
                try
                {
                    // Perform interactive login, and obtain access token.

                    var result = await PCAWrapper.Instance.AcquireTokenInteractiveAsync(PCAWrapper.Scopes);
                    if (result != null)
                    {
                        IsLoggedIn = true;

                        EmailAddSignInIsVisible = true;

                        _sessionController.AccessToken = result.AccessToken;

                        _notificationServiceOneDrake.IsNotificationAfterSignOut = false;

                        (App.Current as App).ResetIdleTimer();

                        await MainThread.InvokeOnMainThreadAsync(async () =>
                        {
                            await Shell.Current.GoToAsync("//RecentlyUpdatedPage", false);
                        });

                    }

                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex);
                }
            }
            else
            {
                EmailAddSignInIsVisible = false;
                BiometricsSignInIsVisible = true;
            }
            
        }

        


        private async Task ShowOkMessage(string title, string message)
        {

            await ShowAlertAsync(title, message, "OK").ConfigureAwait(false);
            
        }


        #endregion Methods       
    }
}
