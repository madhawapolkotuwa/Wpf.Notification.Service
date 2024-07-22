using System;
using System.Globalization;
using System.Windows.Data;
using System.Windows;

namespace MpCoding.WPF.Notification.Converters;

public class BoolToVisibilityConverter : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        bool input = (bool)value;
        int param = 0; // Sometimes users can choose not to enter parameter value, in such cases, we make 0 as default.
        if (parameter != null) int.TryParse(parameter.ToString(), out param);
        switch (input)
        {
            case true:
                if (param == 0) return Visibility.Visible;
                return Visibility.Collapsed;
            case false:
            default:
                if (param == 0) return Visibility.Collapsed;
                return Visibility.Visible;
        }
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        bool input = (bool)value;
        int param = 0;
        if (parameter != null) int.TryParse(parameter.ToString(), out param);
        switch (input)
        {
            case true:
                if (param == 0) return Visibility.Visible;
                return Visibility.Collapsed;
            case false:
            default:
                if (param == 0) return Visibility.Collapsed;
                return Visibility.Visible;
        }
    }
}
