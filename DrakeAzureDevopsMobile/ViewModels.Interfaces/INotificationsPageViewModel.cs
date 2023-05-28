using Plugin.LocalNotification;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DrakeAzureDevopsMobile.ViewModels.Interfaces
{
    public interface INotificationsPageViewModel
    {
        bool IsNotificationAfterSignOut { get; set; }

        void AddNotification(string Title, string Subtitle, string Description, NotificationImage NotificationImage);
    }
}
