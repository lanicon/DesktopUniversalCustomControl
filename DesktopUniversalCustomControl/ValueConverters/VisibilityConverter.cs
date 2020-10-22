using DesktopUniversalCustomControl.CustomView.MsgDlg;
using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace DesktopUniversalCustomControl.ValueConverters
{
    public class MessageDialogButtonToVisibilityConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var count = (MessageButtonCount)value;
            if (count.ToString().Contains(parameter.ToString())) //注意大小写，不然要会找不到界面报错
                return Visibility.Visible;
            else
                return Visibility.Collapsed;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
