using DrakeAzureDevopsMobile.BusinessControllers.Interfaces;
using DrakeAzureDevopsMobile.Interfaces;
using DrakeAzureDevopsMobile.MsalClient;
using DrakeAzureDevopsMobile.Services.Interfaces;
using DrakeAzureDevopsMobile.ViewModels.Interfaces;
using DrakeAzureDevopsMobile.ViewModels.Models.Global;
using DrakeAzureDevopsMobile.Views;
using Microsoft.Maui;
using Microsoft.Maui.Controls.PlatformConfiguration.WindowsSpecific;
using System.Windows.Input;
using Person = DrakeAzureDevopsMobile.ViewModels.Models.Global.Person;
using WebView = Microsoft.Maui.Controls.PlatformConfiguration.WindowsSpecific.WebView;

namespace DrakeAzureDevopsMobile.ViewModels
{
    public class ProfilePageViewModel : VIewModelBase
    {
        #region Fields


        private readonly ISessionController _sessionController;
        private readonly IPersonController _personController;
        private readonly INotificationServiceOneDrake _notificationServiceOneDrake;
        private string _accessToken;
        private string _fullName;
        private string _firstName;
        private string _lastName;
        private string _jobTitle;
        private string _mail;
        private string _mobilePhone;
        private string _officeLocation;
        

        #endregion Fields




        #region Properties

        
        public string FullName
        {
            get => _fullName;
            set { SetPropertyValue(ref _fullName, value); }
        }

        public string FirstName
        {
            get => _firstName;
            set { SetPropertyValue(ref _firstName, value); }
        }

        public string LastName
        {
            get => _lastName;
            set { SetPropertyValue(ref _lastName, value); }
        }

        public string JobTitle
        {
            get => _jobTitle;
            set { SetPropertyValue(ref _jobTitle, value); }
        }

        public string Mail
        {
            get => _mail;
            set { SetPropertyValue(ref _mail, value); }
        }

        public string MobilePhone
        {
            get => _mobilePhone;
            set { SetPropertyValue(ref _mobilePhone, value); }
        }

        public string OfficeLocation
        {
            get => _officeLocation;
            set { SetPropertyValue(ref _officeLocation, value); }
        }


        public string AccessToken
        {
            get => _accessToken;
            set { SetPropertyValue(ref _accessToken, value); }
        }

        public ICommand SignOutCommand { get; set; }


        #endregion Properties




        #region Constructor

        public ProfilePageViewModel(
            IPersonController personController,
            ISessionController sessionController,
            INavigationService navigationService,
            INotificationServiceOneDrake notificationServiceOneDrake) : base(navigationService)
        {
            _personController = personController ?? throw new ArgumentNullException(nameof(_personController));
            _sessionController = sessionController ?? throw new ArgumentNullException(nameof(_sessionController));
            _notificationServiceOneDrake = notificationServiceOneDrake ?? throw new ArgumentNullException(nameof(notificationServiceOneDrake));

            Initialize();
            

            SignOutCommand = new Command(SignOutCommandExcecute);
        }


        #endregion Constructor



        #region Methods

        private void MobileNumberResult()
        {
            if(Person.MobilePhone == null)
            {   
                MobilePhone = "Not Available";
            }
            else
            {
                MobilePhone = Person.MobilePhone;
            }
        }
        private void Initialize()
        {
            

            FullName = Person.DisplayName;
            FirstName = Person.GivenName;
            LastName = Person.Surname;
            JobTitle = Person.JobTitle;
            Mail = Person.Mail;
            OfficeLocation = Person.OfficeLocation;
         

            MobileNumberResult();
        }

        private void SignOutCommandExcecute()
        {

            try
            {
                 PCAWrapper.Instance.SignOutAsync().ContinueWith(async x =>
                {
                    _sessionController.AccessToken = null;

                    _notificationServiceOneDrake.IsNotificationAfterSignOut = true;

                    await MainThread.InvokeOnMainThreadAsync(async () =>
                    {
                        await Shell.Current.GoToAsync("//AzureAdLoginPage", true);
                    });
                });

                
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }

            


        }

        private async Task ShowOkMessage(string title, string message)
        {

            await ShowAlertAsync(title, message, "OK").ConfigureAwait(false);

        }

        #endregion Methods

    }
}
