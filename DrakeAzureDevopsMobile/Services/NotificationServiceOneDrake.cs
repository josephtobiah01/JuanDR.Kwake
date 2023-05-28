using DrakeAzureDevopsMobile.Services.Interfaces;
using DrakeAzureDevopsMobile.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DrakeAzureDevopsMobile.ViewModels.Models.Global;
using System.Collections.ObjectModel;
using Plugin.LocalNotification;
using DrakeAzureDevopsMobile.ViewModels.Models;

namespace DrakeAzureDevopsMobile.Services
{
    public class NotificationServiceOneDrake : Bindablebase, INotificationServiceOneDrake
    {
        private List<NotificationRequest> _notificationDetails;
        private bool _isNotificationAfterSignOut;

        public bool IsNotificationAfterSignOut
        {
            get => _isNotificationAfterSignOut;
            set { SetPropertyValue(ref _isNotificationAfterSignOut, value); }
        }

        public List<NotificationRequest> NotificationDetails
        {
            get => _notificationDetails;
            set { SetPropertyValue(ref _notificationDetails, value); }
        }

        public void AddNotification(NotificationRequest notificationDetails)
        {
            NotificationDetails.Add(notificationDetails);
        }

        public void RemoveNotification(NotificationRequest notificationDetails)
        {
            NotificationDetails.Remove(notificationDetails);
        }

        public IEnumerable<NotificationRequest> GetNotification()
        {
            return NotificationDetails;
        }
    }
}
