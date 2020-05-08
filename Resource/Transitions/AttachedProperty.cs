using System;
using System.Windows;
using System.Windows.Media;

namespace CustomControl.Resource.Transitions
{
    public class AttachedProperty: DependencyObject
    {
        public static Brush GetFillColor(DependencyObject obj)
        {
            return (Brush)obj.GetValue(FillColorProperty);
        }

        public static void SetFillColor(DependencyObject obj, Brush value)
        {
            obj.SetValue(FillColorProperty, value);
        }

        //ContentControlStyle动画填充颜色
        public static readonly DependencyProperty FillColorProperty =
            DependencyProperty.RegisterAttached("FillColor", typeof(Brush), typeof(AttachedProperty), new PropertyMetadata(Brushes.Yellow));

    }
}
