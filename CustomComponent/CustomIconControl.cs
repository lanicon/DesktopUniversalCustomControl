using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Security.RightsManagement;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Automation.Peers;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace CustomControl.CustomComponent
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

        public CustomIconControl()
        {

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
                new PropertyMetadata(IconType.Lock, new PropertyChangedCallback(KindChanged)));

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

            var icon = d as CustomIconControl;
            var TemplateRoot = icon.Template.FindName("TemplateRoot", icon) as Grid;
            foreach (var canvas in TemplateRoot.Children)  //遍历Grid下面所有Canvas控件控制它们的显示与否
            {
                Canvas can = (canvas as Canvas);
                can.Visibility = Visibility.Collapsed;
                if (can.Name.Contains(icon.Kind.ToString()))  //注意Contains函数区分字母大小写
                    can.Visibility = Visibility.Visible;
            }
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
    }

    /// <summary>
    /// 图标类型
    /// </summary>
    public enum IconType
    {
        Eye = 1,  //眼睛
        Lock = 2,  //锁
        Bulb = 3,  //电灯泡 
    }
}
