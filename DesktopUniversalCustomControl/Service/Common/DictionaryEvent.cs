using DesktopUniversalCustomControl.CustomComponent;
using DesktopUniversalCustomControl.ExposedMethod;
using DesktopUniversalCustomControl.Resource.Transitions;
using System;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Effects;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace DesktopUniversalCustomControl.Service.Common
{
    public partial class DictionaryEvent : ResourceDictionary, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        public static RoutedCommand PasswordConvertToTextCommand { get; private set; }
        public static readonly CommandBinding ConvertToTextCommandBinding;

        private Point _clickPoint;
        /// <summary>
        /// 点击坐标
        /// </summary>
        public Point ClickPoint
        {
            get { return _clickPoint; }
            set
            {
                _clickPoint = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("ClickPoint"));
            }
        }

        public DictionaryEvent()
        {
            InitializeComponent();
        }

        static DictionaryEvent()
        {
            //密码转文字命令
            PasswordConvertToTextCommand = new RoutedCommand();
            ConvertToTextCommandBinding = new CommandBinding(PasswordConvertToTextCommand);
            ConvertToTextCommandBinding.Executed += ConvertToTextCommandBinding_Executed;
        }

        private static void ConvertToTextCommandBinding_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            //var toggle = sender as ToggleButton;
            //var control = e.Parameter as FrameworkElement;
            //if (control is CustomPasswordBox)
            //    ((CustomPasswordBox)control).Password;
            Console.WriteLine("进来了");
        }


        //移动鼠标
        private void MouseMovePoint(object sender, MouseEventArgs e)
        {
            var border = sender as Border;
            var canvas = border.FindName("canvas") as Canvas;
            Point point = e.GetPosition(border);
            canvas.Children.Clear();

            Path path = new Path();
            path.Fill = AttachedProperty.GetFillColor(path);
            path.Opacity = 0.35;
            DropShadowEffect dropShadowEffect = new DropShadowEffect();
            dropShadowEffect.BlurRadius = 10;
            dropShadowEffect.Color = Colors.LightGray;
            dropShadowEffect.ShadowDepth = 1;
            dropShadowEffect.Direction = 90;
            path.Effect = dropShadowEffect;
            EllipseGeometry ellipseGeometry = new EllipseGeometry(point, 35, 35);
            path.Data = ellipseGeometry;
            canvas.Children.Add(path);
        }

        DispatcherTimer timer;
        //获取点击的坐标
        private void MouseLeftButtonPoint(object sender, MouseButtonEventArgs e)
        {
            var border = sender as Border;
            var canvas = border.FindName("canvas") as Canvas;
            Point point = e.GetPosition(border);

            timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromMilliseconds(1);
            timer.Tick += delegate { ClickWindow(point, canvas); };
            timer.Start();
        }

        private int count = 0;
        private void ClickWindow(Point point, Canvas canvas)
        {
            count++;
            if (count > 10)
                return;

            Path path = new Path();
            //path.Fill = AttachedProperty.GetFillColor(path);
            path.Stroke = AttachedProperty.GetFillColor(path);
            path.Opacity = 1 - count * 0.1 >= 0.5 ? 1 - count * 0.1 : 0.5;
            DropShadowEffect dropShadowEffect = new DropShadowEffect();
            dropShadowEffect.BlurRadius = 10;
            dropShadowEffect.Color = Colors.LightGray;
            dropShadowEffect.ShadowDepth = 1;
            dropShadowEffect.Direction = 90;
            path.Effect = dropShadowEffect;
            EllipseGeometry ellipseGeometry = new EllipseGeometry(point, 1 + 3 * count, 1 + 3 * count);
            path.Data = ellipseGeometry;
            canvas.Children.Add(path);
        }

        //释放点击的坐标
        private void MouseLeftButtonPointClear(object sender, MouseButtonEventArgs e)
        {
            if (timer != null)
                timer.Stop();
            count = 0;

            var border = sender as Border;
            var canvas = border.FindName("canvas") as Canvas;
            canvas.Children.Clear();
        }

        //明码与密码同步
        private void ToggleButtonIconClick(object sender, RoutedEventArgs e)
        {
            var pb = (sender as CustomIconControl).TemplatedParent as PasswordBox;
            var cpb = pb.TemplatedParent as CustomPasswordBox;

            var clearPassword = pb.Template.FindName("visiblePassword", pb) as CustomTextControl;
            clearPassword.Text = cpb.Password;
        }

        //明码与密码同步
        private void VisiblePasswordTextChanged(object sender, TextChangedEventArgs e)
        {
            var pb = (sender as CustomTextControl).TemplatedParent as PasswordBox;
            var cpb = pb.TemplatedParent as CustomPasswordBox;

            pb.Password = (sender as CustomTextControl).Text;
            CustomPasswordBoxhelper.SetPassword(cpb, pb.Password);
        }
    }
}
