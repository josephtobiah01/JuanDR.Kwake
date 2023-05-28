using Foundation;
using UserNotifications;
using Microsoft.Identity.Client;
using Microsoft.Maui.Controls.Compatibility.Platform.iOS;
using UIKit;
using Plugin.LocalNotification.Platforms;

namespace DrakeAzureDevopsMobile;

[Register("AppDelegate")]
public class AppDelegate : MauiUIApplicationDelegate
{
	protected override MauiApp CreateMauiApp() => MauiProgram.CreateMauiApp();


    public override bool FinishedLaunching(UIApplication app, NSDictionary options)
    {
        // Request authorization for local notifications
        UNUserNotificationCenter.Current.RequestAuthorization(
            UNAuthorizationOptions.Alert | UNAuthorizationOptions.Badge | UNAuthorizationOptions.Sound,
            (approved, error) => {
                // Handle approval and errors
            });

        UNUserNotificationCenter.Current.Delegate = new UserNotificationCenterDelegate();

        // Handle remote notifications
        if (options != null && options.ContainsKey(UIApplication.LaunchOptionsRemoteNotificationKey))
        {
            // Handle the notification
        }

        return base.FinishedLaunching(app, options);
    }

}
