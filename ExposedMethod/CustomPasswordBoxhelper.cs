using CustomControl.CustomComponent;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Interactivity;

namespace CustomControl.ExposedMethod
{
    /// <summary>
    /// 为CustomPasswordBox增加Password绑定功能
    /// </summary>
    public class CustomPasswordBoxhelper
    {
        //密码
        public static readonly DependencyProperty PasswordProperty =
            DependencyProperty.RegisterAttached("Password", typeof(string), typeof(CustomPasswordBoxhelper),
                new FrameworkPropertyMetadata(string.Empty, OnPasswordPropertyChanged));
        //触发PasswordChanged事件
        public static readonly DependencyProperty IsTriggerProperty =
            DependencyProperty.RegisterAttached("IsTrigger", typeof(bool), typeof(CustomPasswordBoxhelper),
                new PropertyMetadata(false, IsTriggerChanged));


        public static bool GetIsTrigger(DependencyObject obj)
        {
            return (bool)obj.GetValue(IsTriggerProperty);
        }
        public static void SetIsTrigger(DependencyObject obj, bool value)
        {
            obj.SetValue(IsTriggerProperty, value);
        }


        public static string GetPassword(DependencyObject obj)
        {
            return (string)obj.GetValue(PasswordProperty);
        }

        public static void SetPassword(DependencyObject obj, string value)
        {
            obj.SetValue(PasswordProperty, value);
        }


        private static void IsTriggerChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var pwd = d as PasswordBox;
            pwd.PasswordChanged += Pwd_PasswordChanged;
        }


        private static void OnPasswordPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            //CustomPasswordBox pwd = d as CustomPasswordBox;
            PasswordBox pwd = d as PasswordBox;
            //pwd.PasswordChanged += Pwd_PasswordChanged;
            string password = e.NewValue.ToString();
            if (pwd != null && pwd.Password != password)
            {
                pwd.Password = password;
            }
        }

        private static void Pwd_PasswordChanged(object sender, RoutedEventArgs e)
        {
            var pwd = sender as PasswordBox;
            SetPassword(pwd, pwd.Password);
        }



        public static bool GetIsShowPassword(DependencyObject obj)
        {
            return (bool)obj.GetValue(IsShowPasswordProperty);
        }

        public static void SetIsShowPassword(DependencyObject obj, bool value)
        {
            obj.SetValue(IsShowPasswordProperty, value);
        }
        //密码显示
        public static readonly DependencyProperty IsShowPasswordProperty =
            DependencyProperty.RegisterAttached("IsShowPassword", typeof(bool), typeof(CustomPasswordBoxhelper), new PropertyMetadata(false));


        public static double GetIconSizePercent(DependencyObject obj)
        {
            return (double)obj.GetValue(IconSizePercentProperty);
        }

        public static void SetIconSizePercent(DependencyObject obj, double value)
        {
            obj.SetValue(IconSizePercentProperty, value);
        }

        //图标大小
        public static readonly DependencyProperty IconSizePercentProperty =
            DependencyProperty.RegisterAttached("IconSizePercent", typeof(double), typeof(CustomPasswordBoxhelper), new PropertyMetadata(1.0));


        public static bool GetIsShowIcon(DependencyObject obj)
        {
            return (bool)obj.GetValue(IsShowIconProperty);
        }

        public static void SetIsShowIcon(DependencyObject obj, bool value)
        {
            obj.SetValue(IsShowIconProperty, value);
        }
        //图标显示
        public static readonly DependencyProperty IsShowIconProperty =
            DependencyProperty.RegisterAttached("IsShowIcon", typeof(bool), typeof(CustomPasswordBoxhelper), new PropertyMetadata(false));
    }

    public class PasswordBoxBehavior: Behavior<PasswordBox>
    {
        [DllImport("user32")]
        //得到光标在屏幕上的位置
        private static extern bool GetCursorPos(out Point lpPoint);

        [DllImport("user32")]
        private static extern Point SetCursorPos(Point lpPoint);

        /// <summary>
        /// 获取光标相对于显示器的位置
        /// </summary>
        /// <returns></returns>
        private static Point GetCursorPosition()
        {
            Point showPoint = new Point();
            GetCursorPos(out showPoint);
            return showPoint;
        }

        protected override void OnAttached()
        {
            //base.OnAttached();          
            AssociatedObject.PasswordChanged += AssociatedObject_PasswordChanged;
        }


        private void AssociatedObject_PasswordChanged(object sender, RoutedEventArgs e)
        {
            var pwd = sender as PasswordBox;
            string password = CustomPasswordBoxhelper.GetPassword(pwd);
            if(pwd != null && pwd.Password != password)
            {
                CustomPasswordBoxhelper.SetPassword(pwd, pwd.Password);
            }

            //int start = (int)pwd.PointToScreen(GetCursorPosition()).X;
            //SetSelection(AssociatedObject, start, start); //AssociatedObject.Password.Length
        }

        protected override void OnDetaching()
        {
            //base.OnDetaching();
            AssociatedObject.PasswordChanged -= AssociatedObject_PasswordChanged;
        }

        /// <summary>
        /// 设置光标焦点
        /// </summary>
        /// <param name="pwd">控件</param>
        /// <param name="start">起始位置</param>
        /// <param name="length">长度</param>
        private void SetSelection(PasswordBox pwd, int start, int length)
        {
            var scr = pwd.Template.FindName("PART_ContentHost", pwd) as ScrollViewer;
            pwd.IsInactiveSelectionHighlightEnabled = false;
            pwd.GetType().GetMethod("Select", System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.NonPublic)
                .Invoke(pwd, new object[] { start, length });
        }
    }


    /// <summary>
    /// 实现 ScrollViewer 滚动到指定控件处
    /// </summary>
    public class ScrollToControlAction: TriggerAction<FrameworkElement>
    {
        /// <summary>
        /// 目标ScrollViewer
        /// </summary>
        public ScrollViewer ScrollViewer
        {
            get { return (ScrollViewer)GetValue(ScrollViewerProperty); }
            set { SetValue(ScrollViewerProperty, value); }
        }

        public static readonly DependencyProperty ScrollViewerProperty =
            DependencyProperty.Register("ScrollViewer", typeof(ScrollViewer), typeof(ScrollToControlAction), new PropertyMetadata(null));


        /// <summary>
        /// 要定位到的控件
        /// </summary>
        public FrameworkElement TargetControl
        {
            get { return (FrameworkElement)GetValue(TargetControlProperty); }
            set { SetValue(TargetControlProperty, value); }
        }

        public static readonly DependencyProperty TargetControlProperty =
            DependencyProperty.Register("TargetControl", typeof(FrameworkElement), typeof(ScrollToControlAction), new PropertyMetadata(null));


        protected override void Invoke(object parameter)
        {
            if(TargetControl == null || ScrollViewer == null)
            {
                throw new ArgumentNullException($"{ScrollViewer} or {TargetControl} cannot be null");
            }

            //检查指定的控件是否在指定的ScrollViewer中
            //TODO:这里只指定离它最近的ScrollViewer，并没有继续向上找
            var container = TargetControl.Parent;
            //var container = TargetControl.FindParent<ScrollViewer>();
            if (container == null || container != ScrollViewer)
            {
                throw new Exception("Thre TargetControl is not the target ScollViewer");
            }

            var currentScrollPosition = ScrollViewer.VerticalOffset;
            var point = new Point(0, currentScrollPosition);
            var targetPosition = TargetControl.TransformToVisual(ScrollViewer).Transform(point);
            ScrollViewer.ScrollToVerticalOffset(targetPosition.Y);
        }
    }
}
