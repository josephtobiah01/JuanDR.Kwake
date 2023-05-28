using CommunityToolkit.Mvvm.Input;
using DrakeAzureDevopsMobile.Services.Interfaces;
using Plugin.LocalNotification;

namespace DrakeAzureDevopsMobile.ViewModels
{
    public partial class NewWorkItemPageViewModel : VIewModelBase
    {
        #region Fields


        private readonly INotificationService _notificationService;
        private readonly INotificationJsonFileService _notificationJsonFileService;
        private int _notificationId = 0;



        #endregion Fields


        #region Properties

        public int NotificationId
        {
            get => _notificationId;
            set { SetPropertyValue(ref _notificationId, value); }
        }
        #endregion Properties

        #region Constructor

        public NewWorkItemPageViewModel(
            INavigationService navigationService,
            INotificationService notificationService,
            INotificationJsonFileService notificationJsonFileService) : base(navigationService)
        {
            _notificationService = notificationService ?? throw new ArgumentNullException(nameof(notificationService));
            _notificationJsonFileService = notificationJsonFileService ?? throw new ArgumentNullException(nameof(notificationJsonFileService));

        }


        #endregion Constructor




        #region Methods

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
                Title = "New Work Item Page",
                Subtitle = "Alert",
                Description = "This is a notification from the New Work Item Page",
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
