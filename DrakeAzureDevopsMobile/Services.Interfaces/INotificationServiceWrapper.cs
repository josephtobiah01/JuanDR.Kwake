using Plugin.LocalNotification;
using Plugin.LocalNotification.EventArgs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DrakeAzureDevopsMobile.Services.Interfaces
{
    public interface INotificationServiceWrapper
    {
        event NotificationActionTappedEventHandler NotificationActionTapped;

        event NotificationReceivedEventHandler NotificationReceived;

        event NotificationDisabledEventHandler NotificationsDisabled;

        bool Cancel(params int[] notificationIdList);

        bool CancelAll();

        bool Clear(params int[] notificationIdList);

        bool ClearAll();

        Task<IList<NotificationRequest>> GetDeliveredNotificationList();

        void OnNotificationActionTapped(NotificationActionEventArgs e);

        void OnNotificationReceived(NotificationEventArgs e);

        Task<IList<NotificationRequest>> GetPendingNotificationList();

        void RegisterCategoryList(HashSet<NotificationCategory> categoryList);

        Task<bool> Show(NotificationRequest request);

        Func<NotificationRequest, Task<NotificationEventReceivingArgs>> NotificationReceiving { get; set; }

        Task<bool> AreNotificationsEnabled();

        Task<bool> RequestNotificationPermission(NotificationPermission permission = null);
    }
}

