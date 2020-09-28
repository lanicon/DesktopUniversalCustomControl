using System;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace DesktopUniversalCustomControl.CustomComponent
{
    /// <summary>
    /// CustomPasswordBox控件
    /// </summary>
    public class CustomPasswordBox : Control
    {
        static CustomPasswordBox()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(CustomPasswordBox), new FrameworkPropertyMetadata(typeof(CustomPasswordBox)));
        }


        [Description("密码更新后发生")]
        public event RoutedEventHandler PasswordChanged
        {
            add
            {
                this.AddHandler(PasswordChangedEvent, value);
            }
            remove
            {
                this.RemoveHandler(PasswordChangedEvent, value);
            }
        }

        public static readonly RoutedEvent PasswordChangedEvent =
            EventManager.RegisterRoutedEvent("PasswordChanged", RoutingStrategy.Bubble, typeof(RoutedEventHandler), typeof(CustomPasswordBox));

        protected virtual void OnPasswordChanged(string oldPassword, string newPassword)
        {
            RoutedPropertyChangedEventArgs<string> arg = new RoutedPropertyChangedEventArgs<string>(oldPassword, newPassword, PasswordChangedEvent);
            this.RaiseEvent(arg);
        }


        /// <summary>
        /// Password
        /// </summary>
        public string Password
        {
            get { return (string)GetValue(PasswordProperty); }
            set { SetValue(PasswordProperty, value); }
        }

        public static readonly DependencyProperty PasswordProperty =
            DependencyProperty.Register("Password", typeof(string), typeof(CustomPasswordBox),
            new PropertyMetadata(string.Empty, new PropertyChangedCallback(OnPasswordValueChanged), new CoerceValueCallback(OnPasswordValue)));

        private static object OnPasswordValue(DependencyObject d, object value)
        {
            return value;
        }

        private static void OnPasswordValueChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d != null && d is CustomPasswordBox)
            {
                var pwd = d as CustomPasswordBox;
                pwd.OnPasswordChanged(e.OldValue as string, e.NewValue as string);

                Console.WriteLine("CustomPasswordBox密码为：" + pwd.Password);
            }
        }

        /// <summary>
        /// 眼睛和锁图标是否可见（默认false）
        /// </summary>
        public bool IsEyeVisible
        {
            get { return (bool)GetValue(IsEyeVisibleProperty); }
            set { SetValue(IsEyeVisibleProperty, value); }
        }

        public static readonly DependencyProperty IsEyeVisibleProperty =
            DependencyProperty.Register("IsEyeVisible", typeof(bool), typeof(CustomPasswordBox), new PropertyMetadata(false, OnVisibleChanged));

        private static void OnVisibleChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var pwd = d as CustomPasswordBox;
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
            DependencyProperty.Register("IconSizePercent", typeof(double), typeof(CustomPasswordBox), new PropertyMetadata(1.0));


        /// <summary>
        /// PasswordChar
        /// </summary>
        public char PasswordChar
        {
            get { return (char)GetValue(PasswordCharProperty); }
            set { SetValue(PasswordCharProperty, value); }
        }

        public static readonly DependencyProperty PasswordCharProperty =
            DependencyProperty.Register("PasswordChar", typeof(char), typeof(CustomPasswordBox), new PropertyMetadata('●'));


        /// <summary>
        /// 最大长度
        /// </summary>
        public int MaxLength
        {
            get { return (int)GetValue(MaxLengthProperty); }
            set { SetValue(MaxLengthProperty, value); }
        }

        public static readonly DependencyProperty MaxLengthProperty =
            DependencyProperty.Register("MaxLength", typeof(int), typeof(CustomPasswordBox), new PropertyMetadata());


        /// <summary>
        /// 占位符提示
        /// </summary>
        public string PasswordPlaceHolder
        {
            get { return (string)GetValue(PasswordPlaceHolderProperty); }
            set { SetValue(PasswordPlaceHolderProperty, value); }
        }

        public static readonly DependencyProperty PasswordPlaceHolderProperty =
            DependencyProperty.Register("PasswordPlaceHolder", typeof(string), typeof(CustomPasswordBox), new PropertyMetadata(null));


        /// <summary>
        /// CornerRadius
        /// </summary>
        public CornerRadius CornerRadius
        {
            get { return (CornerRadius)GetValue(CornerRadiusProperty); }
            set { SetValue(CornerRadiusProperty, value); }
        }

        public static readonly DependencyProperty CornerRadiusProperty =
            DependencyProperty.Register("CornerRadius", typeof(CornerRadius), typeof(CustomPasswordBox), new PropertyMetadata());


        /// <summary>
        /// CaretBrush
        /// </summary>
        public Brush CaretBrush
        {
            get { return (Brush)GetValue(CaretBrushProperty); }
            set { SetValue(CaretBrushProperty, value); }
        }
        public static readonly DependencyProperty CaretBrushProperty =
            DependencyProperty.Register("CaretBrush", typeof(Brush), typeof(CustomPasswordBox), new PropertyMetadata(Brushes.Black));
    }
}
