using MpCoding.WPF.Notification.Abstractions;
using MpCoding.WPF.Notification.Controls;
using MpCoding.WPF.Notification.Enums;

namespace MpCoding.WPF.Notification.Servicers;

public class NotificationDialogService : INotificationDialogService
{
    public INotification ShowDialog(
        string title,
        string message,
        NotificationIcon icon,
        DisplayType type,
        bool hideIcon = false,
        bool blurOtherWindows = false)
    {
        NotificationControl input = _getNotificationConterolWindow(title, message, icon, type, hideIcon);
        return NotificationControl.ShowDialog(input, blurOtherWindows);
    }

    public bool SendToast(
        string title,
        string message,
        NotificationIcon icon = NotificationIcon.Info,
        bool hideIcon = false,
        bool autoClose = true,
        int display_seconds = 7)
    {
        DisplayType type = DisplayType.ToastInfo;
        NotificationControl notify = _getNotificationConterolWindow(title, message, icon, type, hideIcon);
        notify.AutoClose = autoClose;
        return NotificationControl.SendToast(notify, display_seconds);
    }

    private NotificationControl _getNotificationConterolWindow(
        string title,
        string message,
        NotificationIcon icon,
        DisplayType type,
        bool hideIcon)
    {
        NotificationControl notify = new NotificationControl();
        notify.Title = title;
        notify.Message = message;
        notify.NotificationIcon = icon;
        notify.Type = type;
        notify.ShowNotificationIcon = !hideIcon;
        return notify;
    }
}
