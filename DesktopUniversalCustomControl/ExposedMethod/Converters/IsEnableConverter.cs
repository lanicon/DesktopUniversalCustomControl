using System;
using System.Globalization;
using System.Windows.Data;

namespace DesktopUniversalCustomControl.ExposedMethod.Converters
{
    /***
     * 控件是否可用
     * 转换器
     * ***/

    public class ToggleButtonSingleIsEnabled : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

    public class ToggleButtonMultiIsEnabled : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
