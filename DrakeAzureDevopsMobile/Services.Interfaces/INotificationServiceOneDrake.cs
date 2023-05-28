using Plugin.LocalNotification;
using DrakeAzureDevopsMobile.ViewModels.Models.Global;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DrakeAzureDevopsMobile.ViewModels.Models;

namespace DrakeAzureDevopsMobile.Services.Interfaces
{
    public interface INotificationServiceOneDrake
    {
        bool IsNotificationAfterSignOut { get; set; }

        void AddNotification(NotificationRequest notificationDetails);

        void RemoveNotification(NotificationRequest notificationDetails);

        IEnumerable<NotificationRequest> GetNotification();
    }
}
