using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace DesktopUniversalCustomControl.CustomComponent
{
    /// <summary>
    /// ProgressBar控件
    /// </summary>
    public class CustomProgressBar : ProgressBar
    {
        static CustomProgressBar()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(CustomProgressBar), new FrameworkPropertyMetadata(typeof(CustomProgressBar)));
        }


        /// <summary>
        /// 字体颜色
        /// </summary>
        public Brush TextColor
        {
            get { return (Brush)GetValue(TextColorProperty); }
            set { SetValue(TextColorProperty, value); }
        }

        public static readonly DependencyProperty TextColorProperty =
            DependencyProperty.Register("TextColor", typeof(Brush), typeof(CustomProgressBar), new PropertyMetadata());


        /// <summary>
        /// CornerRadius
        /// </summary>
        public CornerRadius CornerRadius
        {
            get { return (CornerRadius)GetValue(CornerRadiusProperty); }
            set { SetValue(CornerRadiusProperty, value); }
        }

        public static readonly DependencyProperty CornerRadiusProperty =
            DependencyProperty.Register("CornerRadius", typeof(CornerRadius), typeof(CustomProgressBar), new PropertyMetadata());
    }
}
