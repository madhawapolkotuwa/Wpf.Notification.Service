using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using MpCoding.WPF.Notification.Abstractions;
using MpCoding.WPF.Notification.Servicers;
using System.Windows;

namespace Testing
{

    public partial class App : Application
    {
        private readonly IHost _host;

        public App()
        {
            _host = Host.CreateDefaultBuilder().ConfigureServices(services =>
            {
                services.AddSingleton<INotificationDialogService, NotificationDialogService>();
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
}
