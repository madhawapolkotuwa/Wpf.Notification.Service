using MpCoding.WPF.Notification.Commands;
using System;
using System.Linq;
using System.Windows.Input;
using System.Windows.Media.Animation;
using System.Windows.Media.Effects;
using System.Windows.Media;
using System.Windows.Threading;
using System.Windows;
using MpCoding.WPF.Notification.Abstractions;
using MpCoding.WPF.Notification.Enums;

namespace MpCoding.WPF.Notification.Controls;

public class NotificationControl : Window, INotification
{
    private BlurEffect _wndwBlur = new BlurEffect();

    private static SolidColorBrush _baseAccent;
    private static SolidColorBrush _baseToastAccent;
    private static SolidColorBrush _baseAccentForeground;
    private static SolidColorBrush _headerBackground;
    private static SolidColorBrush _baseToastBackground;
    private static SolidColorBrush _baseToastForeground;

    private int _displayDuration = 5;
    private DispatcherTimer _autoCloseTimer;
    private int _timerCount;

    public static readonly DependencyProperty TypeProperty;
    public static readonly DependencyProperty AccentForegroundProperty;
    public static readonly DependencyProperty AccentColorProperty;
    public static readonly DependencyProperty ToastAccentColorProperty;
    public static readonly DependencyProperty ToastForegroundProperty;
    public static readonly DependencyProperty ToastBackgroundProperty;
    public static readonly DependencyProperty ShowNotificationIconProperty;
    public static readonly DependencyProperty NotificationIconProperty;
    public static readonly DependencyProperty MessageProperty;
    public static readonly DependencyProperty AppNameProperty;
    public static readonly DependencyProperty AutoCloseProperty;
    public static readonly DependencyProperty CountDownProperty;
    public static readonly DependencyProperty UserInputProperty;
    public DisplayType Type
    {
        get
        {
            return (DisplayType)GetValue(TypeProperty);
        }
        set
        {
            SetValue(TypeProperty, value);
        }
    }
    public SolidColorBrush AccentForeground
    {
        get { return (SolidColorBrush)GetValue(AccentForegroundProperty); }
        set { SetValue(AccentForegroundProperty, value); }
    }
    public SolidColorBrush AccentColor
    {
        get { return (SolidColorBrush)GetValue(AccentColorProperty); }
        set { SetValue(AccentColorProperty, value); }
    }
    public SolidColorBrush ToastAccentColor
    {
        get { return (SolidColorBrush)GetValue(ToastAccentColorProperty); }
        set { SetValue(ToastAccentColorProperty, value); }
    }
    public SolidColorBrush ToastForeground
    {
        get { return (SolidColorBrush)GetValue(ToastForegroundProperty); }
        set { SetValue(ToastForegroundProperty, value); }
    }
    public Brush ToastBackground
    {
        get { return (Brush)GetValue(ToastBackgroundProperty); }
        set { SetValue(ToastBackgroundProperty, value); }
    }
    public bool ShowNotificationIcon
    {
        get { return (bool)GetValue(ShowNotificationIconProperty); }
        set { SetValue(ShowNotificationIconProperty, value); }
    }
    public NotificationIcon NotificationIcon
    {
        get { return (NotificationIcon)GetValue(NotificationIconProperty); }
        set { SetValue(NotificationIconProperty, value); }
    }
    public string Message
    {
        get { return (string)GetValue(MessageProperty); }
        set { SetValue(MessageProperty, value); }
    }
    public string AppName
    {
        get { return (string)GetValue(AppNameProperty); }
        set { SetValue(AppNameProperty, value); }
    }
    public bool AutoClose
    {
        get { return (bool)GetValue(AutoCloseProperty); }
        set { SetValue(AutoCloseProperty, value); }
    }
    public int CountDown
    {
        get { return (int)GetValue(CountDownProperty); }
        set { SetValue(CountDownProperty, value); }
    }
    public string UserInput
    {
        get { return (string)GetValue(UserInputProperty); }
        set { SetValue(UserInputProperty, value); }
    }
    static NotificationControl()
    {
        _baseAccent = (SolidColorBrush)new BrushConverter().ConvertFromString("#FF1678A3");
        _baseToastAccent = (SolidColorBrush)new BrushConverter().ConvertFromString("#8C1678A3");
        _baseAccentForeground = new SolidColorBrush(Colors.White);
        _headerBackground = new SolidColorBrush(Colors.Gray);
        _baseToastBackground = (SolidColorBrush)new BrushConverter().ConvertFromString("#8C0F1122");
        _baseToastForeground = new SolidColorBrush(Colors.White);

        AppNameProperty = DependencyProperty.Register("AppName", typeof(string), typeof(NotificationControl), new PropertyMetadata(null));
        MessageProperty = DependencyProperty.Register("Message", typeof(string), typeof(NotificationControl), new PropertyMetadata(null));

        TypeProperty = DependencyProperty.Register("Type", typeof(DisplayType), typeof(NotificationControl), new PropertyMetadata(DisplayType.ShowInfo));

        AccentForegroundProperty = DependencyProperty.Register("AccentForeground", typeof(SolidColorBrush), typeof(NotificationControl), new FrameworkPropertyMetadata(_baseAccentForeground));
        ShowNotificationIconProperty = DependencyProperty.Register("ShowNotificationIcon", typeof(bool), typeof(NotificationControl), new PropertyMetadata(true));
        ToastForegroundProperty = DependencyProperty.Register("ToastForeground", typeof(SolidColorBrush), typeof(NotificationControl), new PropertyMetadata(_baseToastForeground));
        ToastBackgroundProperty = DependencyProperty.Register("ToastBackground", typeof(Brush), typeof(NotificationControl), new PropertyMetadata(_baseToastBackground));

        AccentColorProperty = DependencyProperty.Register("AccentColor", typeof(SolidColorBrush), typeof(NotificationControl), new FrameworkPropertyMetadata(_baseAccent));
        AccentColorProperty = DependencyProperty.Register("ToastAccentColor", typeof(SolidColorBrush), typeof(NotificationControl), new FrameworkPropertyMetadata(_baseToastAccent));

        NotificationIconProperty = DependencyProperty.Register("NotificationIcon", typeof(NotificationIcon), typeof(NotificationControl), new PropertyMetadata(NotificationIcon.Info));

        AutoCloseProperty = DependencyProperty.Register("AutoClose", typeof(bool), typeof(NotificationControl), new PropertyMetadata(true));
        CountDownProperty = DependencyProperty.Register("CountDown", typeof(int), typeof(NotificationControl), new FrameworkPropertyMetadata(0));

        UserInputProperty = DependencyProperty.Register("UserInput", typeof(string), typeof(NotificationControl), new FrameworkPropertyMetadata(null, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault));

        FrameworkElement.DefaultStyleKeyProperty.OverrideMetadata(typeof(NotificationControl), new FrameworkPropertyMetadata(typeof(NotificationControl)));
    }
    public NotificationControl()
    {
        InitiateWindows();
        base.AllowsTransparency = true;
        base.WindowStyle = WindowStyle.None;
        base.WindowStartupLocation = WindowStartupLocation.CenterOwner;
        base.CommandBindings.Add(new CommandBinding(NotificationCommands.CloseAllToasts, _closeAllToasts));
        base.CommandBindings.Add(new CommandBinding(NotificationCommands.DragMove, _dragMove));
        base.CommandBindings.Add(new CommandBinding(NotificationCommands.CancelResult, delegate (object s, ExecutedRoutedEventArgs e)
        {
            _closeAction(s, e, dialogresult: false);
        }));
        base.CommandBindings.Add(new CommandBinding(NotificationCommands.OkayResult, delegate (object s, ExecutedRoutedEventArgs e)
        {
            _closeAction(s, e, dialogresult: true);
        }));
    }
    private void InitiateWindows()
    {
        if (Application.Current == null)
        {
            new Application();
            Application.Current.ShutdownMode = ShutdownMode.OnExplicitShutdown;
        }
    }
    private void _closeAction(object sender, ExecutedRoutedEventArgs e, bool dialogresult)
    {
        try
        {
            if (Type != DisplayType.ToastInfo)
            {
                base.DialogResult = dialogresult;
            }
        }
        catch
        {
            if (Type != DisplayType.ToastInfo)
            {
                base.DialogResult = false;
            }
        }
        finally
        {
            Close();
        }
    }
    private void _dragMove(object sender, ExecutedRoutedEventArgs e)
    {
        DragMove();
    }
    public override void OnApplyTemplate()
    {
        base.OnApplyTemplate();
    }

    public static INotification ShowDialog(NotificationControl input, bool blurOtherWindows)
    {
        if (input.Type == DisplayType.ToastInfo || input.Type == DisplayType.ContainerView)
        {
            input.Type = DisplayType.ShowInfo;
        }

        _showDialog(ref input, blurOtherWindows);
        return input;

    }

    public static bool SendToast(NotificationControl input, int dispaly_seconds = 7)
    {
        string text = AppDomain.CurrentDomain?.FriendlyName;
        string[] array = text.Split(".");
        if (array.Length > 1)
        {
            array = array.ToList().Take(array.Length - 1).ToArray();
            text = string.Join(".", array);
        }
        if (input.AppName == null)
        {
            input.AppName = text;
        }

        input.Type = DisplayType.ToastInfo;
        input.Opacity = 0.0;
        input.Show();
        double left = SystemParameters.PrimaryScreenWidth - input.ActualWidth;
        double toValue = SystemParameters.PrimaryScreenHeight - input.ActualHeight;
        input.Left = left;
        input.Top = SystemParameters.PrimaryScreenHeight;
        input.Opacity = 1.0;
        DoubleAnimation animation = new DoubleAnimation(toValue, new Duration(TimeSpan.FromMilliseconds(1500.0)))
        {
            EasingFunction = new PowerEase
            {
                EasingMode = EasingMode.EaseInOut
            }
        };
        input.BeginAnimation(TopProperty, animation);

        if (input.AutoClose)
        {
            input._displayDuration = dispaly_seconds;
            input._autoClose();
        }

        return true;
    }

    public static INotification ShowContainerView(NotificationControl input, bool blurOtherWindows = false)
    {

        return input;
    }

    private static void _showDialog(ref NotificationControl input, bool blurOtherWindows)
    {
        Window window = input;
        if (window != null && window != null && window.WindowStartupLocation == WindowStartupLocation.CenterOwner)
        {
            Window mainWindow = Application.Current.MainWindow;
            if (mainWindow != null && mainWindow.IsVisible)
            {
                window.Owner = Application.Current.MainWindow;
            }
            else
            {
                foreach (Window window2 in Application.Current.Windows)
                {
                    if (window2.IsActive)
                    {
                        window.Owner = window2;
                        break;
                    }
                }
            }
        }

        if (blurOtherWindows)
        {
            input.blurOtherWindows(activate: true);
        }

        bool? flag = input.ShowDialog();
        if (blurOtherWindows)
        {
            input.blurOtherWindows(activate: false);
        }
    }

    private void blurOtherWindows(bool activate)
    {
        try
        {
            if (activate)
            {
                _wndwBlur.Radius = 8.0;
                {
                    foreach (Window window in Application.Current.Windows)
                    {
                        if (!(window is NotificationControl))
                        {
                            window.Effect = _wndwBlur;
                        }
                    }

                    return;
                }
            }

            _wndwBlur.Radius = 0.0;
        }
        catch (Exception)
        {
        }
    }

    private void _autoClose()
    {
        _autoCloseTimer = new DispatcherTimer
        {
            Interval = TimeSpan.FromSeconds(1.0)
        };
        _autoCloseTimer.Tick += _autoCloseTimer_Tick;
        _autoCloseTimer.Start();
    }

    private void _autoCloseTimer_Tick(object? sender, EventArgs e)
    {
        if (base.IsMouseOver)
        {
            _timerCount = 0;
            return;
        }
        _timerCount++;
        SetCurrentValue(CountDownProperty, _displayDuration - _timerCount);
        if (_timerCount >= _displayDuration)
        {
            Close();
        }
    }

    private void _closeAllToasts(object sender, ExecutedRoutedEventArgs e)
    {
        try
        {
            foreach (object window in Application.Current.Windows)
            {
                if (window is NotificationControl notification && notification.Type == DisplayType.ToastInfo)
                {
                    try
                    {
                        notification.Close();
                    }
                    catch
                    {
                    }
                }
            }
        }
        catch
        {
        }
    }

}
