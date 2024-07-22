using MpCoding.WPF.Notification.Enums;

namespace MpCoding.WPF.Notification.Abstractions;

public interface INotificationDialogService
{
    INotification ShowDialog(
        string title,
        string message,
        NotificationIcon icon,
        DisplayType type,
        bool hideIcon = false,
        bool blurOtherWindows = false);

    bool SendToast(
        string title,
        string message,
        NotificationIcon icon = NotificationIcon.Info,
        bool hideIcon = false,
        bool autoClose = true,
        int display_seconds = 7);
}
