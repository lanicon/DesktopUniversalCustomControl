using CustomControl.CustomComponent;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;

namespace CustomControl.Service.Common
{
    /// <summary>
    /// MediaPlayerState转Visibility
    /// </summary>
    public class MediaPlayerStateToVisibility : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return (MediaPlayerState)value == MediaPlayerState.Pause ? Visibility.Collapsed : Visibility.Visible;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

    /// <summary>
    /// Double转Int
    /// </summary>
    public class DoubleToInt : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return Math.Round((double)value,0);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return TimeSpan.FromSeconds((double)value).ToString();
        }
    }

    /// <summary>
    /// Double转TimeSpan
    /// </summary>
    public class DoubleToTimeSpan : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return TimeSpan.FromSeconds(Math.Round((double)value, 0));
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

    /// <summary>
    /// SelectedItem取值
    /// </summary>
    public class SelectedItemToString : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value == null ? string.Empty : value.ToString().Split(':')[1];
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

    /// <summary>
    /// MutilComboBoxControl中的子项类型转换
    /// </summary>
    public class IntConverToVisiblity : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            int i = (int)value;
            int[] index = new int[3] { 1, 2, 3 };
            Type[] types = new Type[3] { typeof(CheckBox), typeof(Button), typeof(Image) };
            Console.WriteLine("目标类型：" + targetType);
            if (index[i - 1] == i)
            {
                return Visibility.Visible;
            }
            else
            {
                return Visibility.Collapsed;
            }        
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
