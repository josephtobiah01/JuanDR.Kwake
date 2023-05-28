using DrakeAzureDevopsMobile.Interfaces;
using DrakeAzureDevopsMobile.Services.Interfaces;
using DrakeAzureDevopsMobile.ViewModels.Interfaces;
using DrakeAzureDevopsMobile.ViewModels.Models.Global;
using ObjCRuntime;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserNotifications;

namespace DrakeAzureDevopsMobile.Platforms.iOS
{
    public class UserNotificationCenterDelegate : UNUserNotificationCenterDelegate
    {

        private INotificationServiceOneDrake _notificationServiceOneDrake;

        public UserNotificationCenterDelegate(
            INotificationServiceOneDrake notificationServiceOneDrake)
        {
            _notificationServiceOneDrake = notificationServiceOneDrake ?? throw new ArgumentNullException(nameof(notificationServiceOneDrake));
        }

        public override void DidReceiveNotificationResponse(UNUserNotificationCenter center, UNNotificationResponse response, Action completionHandler)
        {
            if (response.IsDefaultAction)
            {
                if(_notificationServiceOneDrake.IsNotificationAfterSignOut == true)
                {
                    Shell.Current.GoToAsync("//AzureAdLoginPage");
                }
                else
                {
                    Shell.Current.GoToAsync("//NotificationsPage");
                }
            }

            base.DidReceiveNotificationResponse(center, response, completionHandler);
        }
    }
}
