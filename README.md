# Wpf.Notification.Service

## When using MpCoding.WPF.Notification, 
   please add the below resource dictionary directly.
```xml
<ResourceDictionary.MergedDictionaries>
	<ResourceDictionary Source="pack://application:,,,/MpCoding.WPF.Notification;component/Resources/NotificationView.xaml" />
</ResourceDictionary.MergedDictionaries>  
```

## Dependency Injection Integration:

```csharp
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using MpCoding.WPF.Notification.Abstractions;
using MpCoding.WPF.Notification.Servicers;

public partial class App : Application
    {
        private readonly IHost _host;

        public App()
        {
            _host = Host.CreateDefaultBuilder().ConfigureServices(services =>
            {
                services.AddSingleton<INotificationDialogService, NotificationDialogService>(); // Add this Dependency Injection
                services.AddSingleton<MainWindow>();
            }).Build();
        }

        protected override void OnStartup(StartupEventArgs e)
        {
            _host.Start();

            MainWindow = _host.Services.GetRequiredService<MainWindow>();
            MainWindow.Show();

            base.OnStartup(e);
        }
    }
```
### Show Notification Dialog
```csharp
INotification ShowDialog(
       string title,
       string message,
       NotificationIcon icon,
       DisplayType type,
       bool hideIcon = false,
       bool blurOtherWindows = false);
```
### Send Notification Toast
```csharp
bool SendToast(
        string title,
        string message,
        NotificationIcon icon = NotificationIcon.Info,
        bool hideIcon = false,
        bool autoClose = true,
        int display_seconds = 7);
```
### Example :-
```csharp
private INotificationDialogService _ds;

public MainWindow(INotificationDialogService ds)
{
    InitializeComponent();
    _ds = ds;
}
```
1. Success Notify
```csharp
var result = _ds.ShowDialog("Success", "Success dialog", NotificationIcon.Success, DisplayType.ShowInfo, false, false);
```
2. Info Notify
```csharp
var result = _ds.ShowDialog("Info", "Information dialog", NotificationIcon.Info, DisplayType.ShowInfo, false);
```
3. Warning Notify
```csharp
var result = _ds.ShowDialog("Warning", "Warning dialog", NotificationIcon.Warning, DisplayType.ShowInfo, false, false);
```
4. Error Notify
```csharp
var result = _ds.ShowDialog("Error", "Error dialog", NotificationIcon.Error, DisplayType.ShowInfo, false, true);
```
5. Confirm Notify
```csharp
var result = _ds.ShowDialog("Error", "Get Error Confirmation", NotificationIcon.Error, DisplayType.GetConfirmation, false, true);
```
6. Success Toase Send
```csharp
var result = _ds.SendToast("Success", "Success Toast msg", NotificationIcon.Success);
```
etc.



