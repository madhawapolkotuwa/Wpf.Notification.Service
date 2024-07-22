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
