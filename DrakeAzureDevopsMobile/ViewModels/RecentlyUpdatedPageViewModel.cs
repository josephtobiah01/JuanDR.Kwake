using CommunityToolkit.Mvvm.Input;
using DrakeAzureDevopsMobile.BusinessControllers.Interfaces;
using DrakeAzureDevopsMobile.Models.BusinessModels;
using DrakeAzureDevopsMobile.Services.Interfaces;
using DrakeAzureDevopsMobile.ViewModels.Models.Global;
using Plugin.LocalNotification;
using Timer = System.Timers.Timer;

namespace DrakeAzureDevopsMobile.ViewModels
{
    public partial class RecentlyUpdatedPageViewModel : VIewModelBase
    {


        #region Fields

        private INotificationJsonFileService _notificationJsonFileService;
        private readonly INotificationServiceWrapper _notificationServiceWrapper;
        private readonly ISessionController _sessionController;
        private readonly IPersonController _personController;
        private readonly INotificationServiceOneDrake _notificationServiceOneDrake;
        private readonly INotificationService _notificationService;
        private int _notificationId = 0;
        private string _notificationTitle;
        private string _notificationDescription;
        private string _notificationSubtitle;
        private NotificationImage _notificationImage;

        private NotificationRequest _notificationRequest;

        private string _accessToken;
        private string _name;

        
        
        private bool _isNotificationAfterSignOut;
        private DateTime NotificationItemDateTime;
        private Timer NotificationTimer = new Timer();



        #endregion Fields







        #region Properties

        public List<NotificationRequest> Notifications { get; set; } = new List<NotificationRequest>();

        public NotificationRequest NotificationRequest
        {
            get => _notificationRequest;
            set { SetPropertyValue(ref _notificationRequest, value); }
        }
        
        public int NotificationId
        {
            get => _notificationId;
            set { SetPropertyValue(ref _notificationId, value);}
        }

        public bool IsNotificationAfterSignOut
        {
            get => _isNotificationAfterSignOut;
            set { SetPropertyValue(ref _isNotificationAfterSignOut, value); }
        }



        public string Name 
        { 
            get => _name; 
            set { SetPropertyValue(ref _name, value); }
        }

        public string AccessToken 
        {
            get => _accessToken;
            set { SetPropertyValue(ref _accessToken, value); }
        }

        public string NotificationTitle
        {
            get => _notificationTitle;
            set { SetPropertyValue(ref _notificationTitle, value); }
        }

        public string NotificationDescription
        {
            get => _notificationDescription;
            set { SetPropertyValue(ref _notificationDescription, value); }
        }

        public string NotificationSubtitle
        {
            get => _notificationSubtitle;
            set { SetPropertyValue(ref _notificationSubtitle, value); }
        }

        public NotificationImage NotificationImage
        {
            get => _notificationImage;
            set { SetPropertyValue(ref _notificationImage, value); }
        }



        #endregion Properties







        #region Methods
        #region Constructors

        public RecentlyUpdatedPageViewModel(
            INotificationJsonFileService notificationJsonFileService,
            INotificationServiceWrapper notificationServiceWrapper,
            ISessionController sessionController,
            IPersonController personController,
            INavigationService navigationService,
            INotificationServiceOneDrake notificationServiceOneDrake,
            INotificationService notificationService) : base(navigationService)
        {
            _notificationJsonFileService = notificationJsonFileService ?? throw new ArgumentNullException(nameof(notificationJsonFileService));
            _notificationServiceWrapper = notificationServiceWrapper ?? throw new ArgumentNullException(nameof(notificationServiceWrapper));
            _sessionController = sessionController ?? throw new ArgumentNullException(nameof(sessionController));
            _personController = personController ?? throw new ArgumentNullException(nameof(personController));
            _notificationServiceOneDrake = notificationServiceOneDrake ?? throw new ArgumentNullException(nameof(notificationServiceOneDrake));
            _notificationService = notificationService ?? throw new ArgumentNullException(nameof(notificationService));


            Initialize();

            NotificationImage = new NotificationImage();

        }


        #endregion Constructors



        private Task ShowOkMessage(string title, string message, string cancel = "OK")
        {

            App.Current.MainPage.DisplayAlert(title, message, cancel);
            return Task.CompletedTask;

        }



        private async void Initialize()
        {
            _accessToken = _sessionController.AccessToken;

            await GetProfile();

            Name = Person.DisplayName;
            
            _notificationServiceOneDrake.IsNotificationAfterSignOut = false;

            (App.Current as App).ResetIdleTimer();

        }



        private async Task GetProfile()
        {
            try
            {
                var result = await _personController.GetPersonProfileAsync(_sessionController.AccessToken);

                SetPersonInfo(result);
            }

            catch(Exception ex) 
            {
                await ShowAlertAsync("Exception", $"{ex}");
            }
        }

        private void SetPersonInfo(PersonDetails result)
        {

            Person.BusinessPhones = result.BusinessPhones;
            Person.OfficeLocation = result.OfficeLocation;
            Person.JobTitle = result.JobTitle;
            Person.GivenName= result.GivenName;
            Person.Surname = result.Surname;
            Person.DisplayName= result.DisplayName;
            Person.Mail = result.Mail;
            Person.MobilePhone = result.MobilePhone;

        }

        private async Task CheckNotificationPermission()
        {
            if (await _notificationService.AreNotificationsEnabled() == false)
            {
                await _notificationService.RequestNotificationPermission();
            }
        }

        [RelayCommand]

        async Task SetReminder()
        {
            
            await CheckNotificationPermission();


            var request = new NotificationRequest
            {
                NotificationId = 0,
                Title = "Recently Updated Page",
                Subtitle = "Alert",
                Description = "This is a notification from the Recently Updated Page",
                Image = new NotificationImage
                {
                    FilePath = "drake_logo.png"
                },
                Schedule = new NotificationRequestSchedule
                {
                    NotifyTime = DateTime.Now.AddSeconds(10),
                    //NotifyRepeatInterval = TimeSpan.FromDays(1)

                }
            };


            await _notificationJsonFileService.CreateFile();


            if (NotificationId == request.NotificationId)
            {
                NotificationId = NotificationId + 1;
                request.NotificationId = NotificationId;
            }

            
            
            
            await _notificationService.Show(request);

        }

        #endregion Methods
    }
}
