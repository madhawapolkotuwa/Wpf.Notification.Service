using MpCoding.WPF.Notification.Enums;

namespace MpCoding.WPF.Notification.Abstractions;

public interface INotification
{
    DisplayType Type { get; set; }
    string Message { get; set; }
    NotificationIcon NotificationIcon { get; set; }
    bool ShowNotificationIcon { get; set; }
    string UserInput { get; set; }
    bool? DialogResult { get; set; }
    string AppName { get; set; }
    bool AutoClose { get; set; }
    int CountDown { get; set; }

}
