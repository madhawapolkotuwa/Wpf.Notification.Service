using MpCoding.WPF.Notification.Abstractions;
using MpCoding.WPF.Notification.Enums;
using System.Windows;


namespace Testing
{
    public partial class MainWindow : Window
    {
        private INotificationDialogService _ds;

        public MainWindow(INotificationDialogService ds)
        {
            InitializeComponent();
            _ds = ds;
        }

        private void Button_Click_Success(object sender, RoutedEventArgs e)
        {
            var result = _ds.ShowDialog("Success", "Success dialog", NotificationIcon.Success, DisplayType.ShowInfo, false, false);
        }

        private void Button_Click_Info(object sender, RoutedEventArgs e)
        {
            var result = _ds.ShowDialog("Info", "Information dialog", NotificationIcon.Info, DisplayType.ShowInfo, false);
        }

        private void Button_Click_Warning(object sender, RoutedEventArgs e)
        {
            var result = _ds.ShowDialog("Warning", "Warning dialog", NotificationIcon.Warning, DisplayType.ShowInfo, false, false);
        }

        private void Button_Click_Error(object sender, RoutedEventArgs e)
        {
            var result = _ds.ShowDialog("Error", "Error dialog", NotificationIcon.Error, DisplayType.ShowInfo, false, true);
        }

        private void Button_Click_Confirm(object sender, RoutedEventArgs e)
        {
            var result = _ds.ShowDialog("Error", "Get Error Confirmation", NotificationIcon.Error, DisplayType.GetConfirmation, false, true);
            tblockDialogResult.Text = result.DialogResult.ToString();
        }

        private void Button_Click_Success_Toase(object sender, RoutedEventArgs e)
        {
            var result = _ds.SendToast("Success", "Success Toast msg", NotificationIcon.Success);
        }

        private void Button_Click_Warnin_Toase(object sender, RoutedEventArgs e)
        {
            var result = _ds.SendToast("Warning", "Warning Toast msg", NotificationIcon.Warning);
        }

        private void Button_Click_GetInput(object sender, RoutedEventArgs e)
        {
            var result = _ds.ShowDialog("Info", "Get User input", NotificationIcon.Info, DisplayType.GetInput);
            tblockDialogResult.Text = result.DialogResult.ToString();
            if (result.DialogResult == true)
                tblockUserInput.Text = result.UserInput?.ToString();
        }
    }
}
