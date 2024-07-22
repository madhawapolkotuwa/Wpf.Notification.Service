using System.Windows.Input;

namespace MpCoding.WPF.Notification.Commands;

public class NotificationCommands
{
    public static readonly RoutedUICommand OkayResult = new RoutedUICommand("To set the dialog result of the notification window to True", "OkayResult", typeof(NotificationCommands));

    public static readonly RoutedUICommand CancelResult = new RoutedUICommand("To set the dialog result of the notification window to False", "CancelResult", typeof(NotificationCommands));

    public static readonly RoutedUICommand CloseAllToasts = new RoutedUICommand("Close all the open toasts", "CloseAllToasts", typeof(NotificationCommands));

    public static readonly RoutedUICommand DragMove = new RoutedUICommand("Drag and move", "DragMove", typeof(NotificationCommands));
}
