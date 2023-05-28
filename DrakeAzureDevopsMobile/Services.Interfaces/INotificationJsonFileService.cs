using Plugin.LocalNotification;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DrakeAzureDevopsMobile.Services.Interfaces
{
    public interface INotificationJsonFileService
    {
        Task CreateFile();
        Task WriteToFile(List<NotificationRequest> notifications);
        List<NotificationRequest> ReadFile();
        Task AddNotification(NotificationRequest notification);
        Task RemoveNotification(NotificationRequest notification);
    }
}
