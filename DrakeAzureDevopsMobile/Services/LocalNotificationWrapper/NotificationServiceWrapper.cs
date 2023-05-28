using DrakeAzureDevopsMobile.Helpers;
using DrakeAzureDevopsMobile.Services.Interfaces;
using Plugin.LocalNotification;
using Plugin.LocalNotification.EventArgs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace DrakeAzureDevopsMobile.Services.LocalNotificationWrapper
{
    public class NotificationServiceWrapper: Bindablebase, INotificationServiceWrapper
    {
        private INotificationService _notificationService;


        public NotificationServiceWrapper(
            INotificationService notificationService)
        {
            _notificationService = notificationService ?? throw new ArgumentNullException(nameof(notificationService));
        }

        public event NotificationActionTappedEventHandler NotificationActionTapped
        {
            add => _notificationService.NotificationActionTapped += value;
            remove => _notificationService.NotificationActionTapped -= value;
        }

        public event NotificationReceivedEventHandler NotificationReceived
        {
            add => _notificationService.NotificationReceived += value;
            remove => _notificationService.NotificationReceived -= value;
        }

        public event NotificationDisabledEventHandler NotificationsDisabled
        {
            add => _notificationService.NotificationsDisabled += value;
            remove => _notificationService.NotificationsDisabled -= value;
        }

        public bool Cancel(params int[] notificationIdList)
        {
            return _notificationService.Cancel(notificationIdList);
        }

        public bool CancelAll()
        {
            return _notificationService.CancelAll();
        }

        public bool Clear(params int[] notificationIdList)
        {
            return _notificationService.Clear(notificationIdList);
        }

        public bool ClearAll()
        {
            return _notificationService.ClearAll();
        }

        public Task<IList<NotificationRequest>> GetDeliveredNotificationList()
        {
            return _notificationService.GetDeliveredNotificationList();
        }

        public void OnNotificationActionTapped(NotificationActionEventArgs e)
        {
            _notificationService.OnNotificationActionTapped(e);
        }

        public void OnNotificationReceived(NotificationEventArgs e)
        {
            _notificationService.OnNotificationReceived(e);
        }

        public Task<IList<NotificationRequest>> GetPendingNotificationList()
        {
            return _notificationService.GetPendingNotificationList();
        }

        public void RegisterCategoryList(HashSet<NotificationCategory> categoryList)
        {
            _notificationService.RegisterCategoryList(categoryList);
        }

        public Task<bool> Show(NotificationRequest request)
        {
            return _notificationService.Show(request);
        }

        public Func<NotificationRequest, Task<NotificationEventReceivingArgs>> NotificationReceiving
        {
            get => _notificationService.NotificationReceiving;
            set => _notificationService.NotificationReceiving = value;
        }

        public Task<bool> AreNotificationsEnabled()
        {
            return _notificationService.AreNotificationsEnabled();
        }

        public Task<bool> RequestNotificationPermission(NotificationPermission permission = null)
        {
            return _notificationService.RequestNotificationPermission(permission);
        }
    }
}
