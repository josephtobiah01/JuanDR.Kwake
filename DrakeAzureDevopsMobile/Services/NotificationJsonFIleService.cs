using DrakeAzureDevopsMobile.Services.Interfaces;
using Newtonsoft.Json;
using Plugin.LocalNotification;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DrakeAzureDevopsMobile.Services
{
    public class NotificationJsonFIleService : INotificationJsonFileService
    {
        public static string FilePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "notification.json");


        public async Task CreateFile()
        {
            if(!File.Exists(FilePath))
            {
                //await using FileStream fs = File.Create(FilePath, FileMode.CreateNew);
                await using (var fs = new FileStream(FilePath, FileMode.Create, FileAccess.Write, FileShare.None))
                {
                    var emptyArray = new List<NotificationRequest>();
                    var json = JsonConvert.SerializeObject(emptyArray);
                    await using var sw = new StreamWriter(fs);
                    await sw.WriteAsync(json);

                }
            }
        }

        public async Task ClearFile()
        {
            await using (var fs = new FileStream(FilePath, FileMode.Truncate))
            {
                await fs.FlushAsync();
            }
        }

        public List<NotificationRequest> ReadFile()
        {
            //string content = "";
            //using (StreamReader sr = new StreamReader(FilePath))
            //{
            //    content = sr.ReadToEnd();
            //}
            //return content;

            List<NotificationRequest> notifications = new List<NotificationRequest>();
            using (StreamReader sr = new StreamReader(FilePath))
            {
                string json = sr.ReadToEnd();
                JsonSerializerSettings settings = new JsonSerializerSettings()
                {
                    Error = (sender, args) =>
                    {
                        args.ErrorContext.Handled = true;
                    }
                };
                notifications = JsonConvert.DeserializeObject<List<NotificationRequest>>(json, settings);
            }
            return notifications;
        }

        public Task WriteToFile(List<NotificationRequest> notifications)
        {
            //using (StreamWriter sw = new StreamWriter(FilePath, true))
            //{
            //    sw.WriteLine(content);
            //}

            //string json = JsonConvert.SerializeObject(notifications, Formatting.Indented);
            //await using (StreamWriter sw = new StreamWriter(FilePath))
            //{
            //     sw.WriteLine(json);
            //}

            File.WriteAllText(FilePath, JsonConvert.SerializeObject(notifications));

            return Task.CompletedTask;
        }

        public async Task AddNotification(NotificationRequest notification)
        {

            //List<NotificationRequest> notifications = ReadFile();
            //notifications.Insert(0, notification);
            //await WriteToFile(notifications);

            var notifications = ReadFile();
            notifications.Add(notification);
            await WriteToFile(notifications);
        }

        public async Task RemoveNotification(NotificationRequest notification)
        {
            List<NotificationRequest> notifications = ReadFile();
            notifications.Remove(notification);
            await ClearFile();
            await WriteToFile(notifications);
        }
    }
}
