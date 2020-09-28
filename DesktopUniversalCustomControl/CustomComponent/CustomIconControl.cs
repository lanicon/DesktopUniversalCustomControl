using System.Windows;
using System.Windows.Controls.Primitives;

namespace DesktopUniversalCustomControl.CustomComponent
{
    /// <summary>
    /// IconControl 图标控件
    /// </summary>
    public class CustomIconControl : ToggleButton
    {
        static CustomIconControl()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(CustomIconControl), new FrameworkPropertyMetadata(typeof(CustomIconControl)));
        }

        /// <summary>
        /// 图标类型
        /// </summary>
        public IconType Kind
        {
            get { return (IconType)GetValue(KindProperty); }
            set { SetValue(KindProperty, value); }
        }

        public static readonly DependencyProperty KindProperty =
            DependencyProperty.Register("Kind", typeof(IconType), typeof(CustomIconControl),
               new FrameworkPropertyMetadata(IconType.Lock, new PropertyChangedCallback(KindChanged)));

        private static void KindChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            //var icon = d as CustomIconControl;
            //var userControl = icon.Template.FindName("userControl", icon) as UserControl;
            //var templateRoot = userControl.Template.FindName("TemplateRoot", userControl) as Grid;
            //foreach (var canvas in templateRoot.Children)  //遍历Grid下面所有Canvas控件控制它们的显示与否
            //{
            //    Canvas can = (canvas as Canvas);
            //    can.Visibility = Visibility.Collapsed;
            //    if (can.Name.Contains(icon.Kind.ToString()))  //注意Contains函数区分字母大小写
            //        can.Visibility = Visibility.Visible;
            //}
        }


        /// <summary>
        /// 图标百分比大小
        /// </summary>
        public double IconSizePercent
        {
            get { return (double)GetValue(IconSizePercentProperty); }
            set { SetValue(IconSizePercentProperty, value); }
        }

        public static readonly DependencyProperty IconSizePercentProperty =
            DependencyProperty.Register("IconSizePercent", typeof(double), typeof(CustomIconControl), new PropertyMetadata(1.0));


        /// <summary>
        /// 是否有阴影效果
        /// </summary>
        public bool ExistEffect
        {
            get { return (bool)GetValue(ExistEffectProperty); }
            set { SetValue(ExistEffectProperty, value); }
        }

        public static readonly DependencyProperty ExistEffectProperty =
            DependencyProperty.Register("ExistEffect", typeof(bool), typeof(CustomIconControl), new PropertyMetadata(false));
    }

    /// <summary>
    /// 图标类型
    /// </summary>
    public enum IconType
    {
        Eye = 1,  //眼睛
        Lock = 2,  //锁
        Bulb = 3,  //电灯泡 
        Message = 4, //消息
        CloseCircle = 5, //圆形关闭
        Play = 6, //播放
        Pause = 7, //暂停
        Restart = 8, //重放
        Last = 9, //上一个
        Next = 10, //下一个
        VolumeMedium = 11, //音量（50%）
        VolumeHigh = 12,  //音量（>50%）
        VolumeOff = 13,   //音量（=0）
        FullScreen = 14,  //全屏
        FullScreenExit = 15,  //退出全屏
    }
}
