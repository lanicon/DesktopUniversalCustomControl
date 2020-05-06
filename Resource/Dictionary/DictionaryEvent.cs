using CustomControl.CustomComponent;
using CustomControl.ExposedMethod;
using CustomControl.Resource.Transitions;
using MaterialDesignThemes.Wpf;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Effects;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace CustomControl.Resource.Dictionary
{
    public partial class DictionaryEvent : ResourceDictionary, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

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
            timer.Tick += delegate { ClickWindow(point,canvas); };
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
            if(timer != null)
                timer.Stop();
            count = 0;

            var border = sender as Border;
            var canvas = border.FindName("canvas") as Canvas;
            canvas.Children.Clear();
        }


        int index = 0;
        CustomPasswordBox cpb;
        PasswordBox pb;
        private void Eye_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            pb = (sender as PackIcon).TemplatedParent as PasswordBox;
            cpb = pb.TemplatedParent as CustomPasswordBox;
            index++;
            if (index % 2 != 0)
            {
                CustomPasswordBoxhelper.SetPassword(cpb, pb.Password);
                CustomPasswordBoxhelper.SetIsShowPassword(pb, true);
            }
            else
            {
                CustomPasswordBoxhelper.SetPassword(cpb, pb.Password);
                CustomPasswordBoxhelper.SetIsShowPassword(pb, false);
            }
        }

        private void VisiblePassword_TextChanged(object sender, TextChangedEventArgs e)
        {
            pb = (sender as CustomTextControl).TemplatedParent as PasswordBox;
            cpb = pb.TemplatedParent as CustomPasswordBox;

            pb.Password = (sender as CustomTextControl).Text;
            CustomPasswordBoxhelper.SetPassword(cpb, pb.Password);
        }
    }
}
