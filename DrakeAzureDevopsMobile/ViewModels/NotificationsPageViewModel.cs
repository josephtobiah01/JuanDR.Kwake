using CommunityToolkit.Mvvm.Input;
using DrakeAzureDevopsMobile.BusinessControllers.Interfaces;
using DrakeAzureDevopsMobile.Interfaces;
using DrakeAzureDevopsMobile.Services.Interfaces;
using DrakeAzureDevopsMobile.ViewModels.Interfaces;
using DrakeAzureDevopsMobile.ViewModels.Models;
using DrakeAzureDevopsMobile.ViewModels.Models.Global;
using Newtonsoft.Json;
using Plugin.LocalNotification;
using Plugin.LocalNotification.EventArgs;
using System.Collections.ObjectModel;
using System.ComponentModel;



namespace DrakeAzureDevopsMobile.ViewModels
{
    public partial class NotificationsPageViewModel : VIewModelBase
    {

        #region Fields

        private readonly INotificationServiceWrapper _notificationServiceWrapper;
        private INotificationJsonFileService _notificationJsonFileService;
        private bool _backgroundColor;
        private List<NotificationRequest> _notifications = null;

        #endregion Fields



        #region Properties

        
        public List<NotificationRequest> Notifications
        { 
            get { return _notifications; }
            set { SetPropertyValue(ref _notifications, value); }
        }

        public bool BackgroundColor
        {
            get => _backgroundColor;
            set { SetPropertyValue(ref _backgroundColor, value); }
        }   
        #endregion Properties



        #region Constructor

        public NotificationsPageViewModel(
            INotificationServiceWrapper notificationServiceWrapper,
            INotificationJsonFileService notificationJsonFileService,
            INavigationService navigationService) : base(navigationService)
        {
            _notificationServiceWrapper = notificationServiceWrapper ?? throw new ArgumentNullException(nameof(notificationServiceWrapper));
            _notificationJsonFileService = notificationJsonFileService ?? throw new ArgumentNullException(nameof(notificationJsonFileService));

            Reload();

            MessagingCenter.Subscribe<App, bool>(this, "ReloadNotifications", (sender, shouldReload) =>
            {
                if (shouldReload)
                {
                    Reload();
                }
            });

        }



        #endregion Constructor




        #region Methods


        public Color GetBackgroundColor(bool backgroundColor)
        {
            return backgroundColor == true? Color.FromArgb("#E8E8E8") : Color.FromArgb("#FFFFFF");
        }

        public void Reload()
        {
            Notifications = new List<NotificationRequest>();

            for(int i = 0; i < Notifications.Count; i++)
            {
                Notifications[i].NotificationId = i;
                
            }

            foreach(var x in Notifications)
            {
                if(x.NotificationId % 2 == 0)
                {
                    BackgroundColor = true;
                }
                else
                {
                    BackgroundColor = false;
                }
            }

            
            OnPropertyChanged(nameof(Notifications));

            GetBackgroundColor(BackgroundColor);

            _notificationJsonFileService.CreateFile();

            var result = _notificationJsonFileService.ReadFile();

            Notifications = result;
        }

        [RelayCommand]

        public async void ClearNotifications()
        {
            Notifications.Clear();
            OnPropertyChanged(nameof(Notifications));
            await _notificationJsonFileService.WriteToFile(Notifications);
            Reload();
        }


        #endregion Methods
    }
}
