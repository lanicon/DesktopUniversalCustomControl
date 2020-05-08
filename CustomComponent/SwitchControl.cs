using CustomControl.Service.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace CustomControl.CustomComponent
{
    /// <summary>
    /// 开关控件
    /// </summary>
    public class SwitchControl : CheckBox
    {
        static SwitchControl()
        {           
            DefaultStyleKeyProperty.OverrideMetadata(typeof(SwitchControl), new FrameworkPropertyMetadata(typeof(SwitchControl),FrameworkPropertyMetadataOptions.None,new PropertyChangedCallback(OnValueChanged)));
        }

        private static void OnValueChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            Console.WriteLine("纳尼");
        }

        public static double sliderw { get; set; }

        public SwitchControl()
        {
            this.Loaded += SwitchControl_Loaded;
        }

        Border borderCheckFlag;
        TranslateTransform translate;
        private void SwitchControl_Loaded(object sender, RoutedEventArgs e)
        {
            sliderw = this.Width - this.Height;
            SetSliderWidth(this, sliderw);
            this.CornerRadius = new CornerRadius(this.Height / 2);

            borderCheckFlag = this.Template.FindName("CheckFlag", this) as Border;
            translate = new TranslateTransform();
            borderCheckFlag.RenderTransform = translate;

            this.SizeChanged += SwitchControl_SizeChanged;
        }

        private void SwitchControl_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            sliderw = this.Width - this.Height;
            SetSliderWidth(this, sliderw);
            this.CornerRadius = new CornerRadius(this.Height / 2);
            if(this.IsChecked == true)
            {
                OnChecked(e);
            }
        }


        /// <summary>
        /// CornerRadius      
        /// </summary>
        public CornerRadius CornerRadius
        {
            get { return (CornerRadius)GetValue(CornerRadiusProperty); }
            set { SetValue(CornerRadiusProperty, value); }
        }

        public static readonly DependencyProperty CornerRadiusProperty =
            DependencyProperty.Register("CornerRadius", typeof(CornerRadius), typeof(SwitchControl), new PropertyMetadata());


        /// <summary>
        /// 内容
        /// </summary>
        public string SwitchContent
        {
            get { return (string)GetValue(SwitchContentProperty); }
            set { SetValue(SwitchContentProperty, value); }
        }

        public static readonly DependencyProperty SwitchContentProperty =
            DependencyProperty.Register("SwitchContent", typeof(string), typeof(SwitchControl), new PropertyMetadata());


        /// <summary>
        /// 打开的背景颜色
        /// </summary>
        public Brush SwitchOpenBackground
        {
            get { return (Brush)GetValue(SwitchOpenBackgroundProperty); }
            set { SetValue(SwitchOpenBackgroundProperty, value); }
        }

        public static readonly DependencyProperty SwitchOpenBackgroundProperty =
            DependencyProperty.Register("SwitchOpenBackground", typeof(Brush), typeof(SwitchControl), new PropertyMetadata());


        /// <summary>
        /// 关闭的背景颜色
        /// </summary>
        public Brush SwicthCloseBackground
        {
            get { return (Brush)GetValue(SwicthCloseBackgroundProperty); }
            set { SetValue(SwicthCloseBackgroundProperty, value); }
        }

        public static readonly DependencyProperty SwicthCloseBackgroundProperty =
            DependencyProperty.Register("SwicthCloseBackground", typeof(Brush), typeof(SwitchControl), new PropertyMetadata());


        public static double GetSliderWidth(DependencyObject obj)
        {
            return (double)obj.GetValue(SliderWidthProperty);
        }

        public static void SetSliderWidth(DependencyObject obj, double value)
        {
            obj.SetValue(SliderWidthProperty, value);
        }

        //切换长度(Width-Height)
        public static readonly DependencyProperty SliderWidthProperty =
            DependencyProperty.RegisterAttached("SliderWidth", typeof(double), typeof(SwitchControl), new PropertyMetadata(OnSliderWidthChanged));

        private static void OnSliderWidthChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            //var switchButton = d as SwitchControl;
            //double sliderWidth = switchButton.Width - switchButton.Height;
        }

        //选中
        protected override void OnChecked(RoutedEventArgs e)
        {
            //lock (borderCheckFlag)
            //{
            //    DoubleAnimationUsingKeyFrames doubleAnimationUsingKeyFramesChecked = new DoubleAnimationUsingKeyFrames();
            //    EasingDoubleKeyFrame easingDoubleKeyFrame1 = new EasingDoubleKeyFrame(0, KeyTime.FromTimeSpan(TimeSpan.FromMilliseconds(0)));
            //    EasingDoubleKeyFrame easingDoubleKeyFrame2 = new EasingDoubleKeyFrame(sliderw, KeyTime.FromTimeSpan(TimeSpan.FromMilliseconds(200)));
            //    AnimationClock clock = doubleAnimationUsingKeyFramesChecked.CreateClock();
            //    translate.ApplyAnimationClock(TranslateTransform.XProperty, clock);
            //}
            DoubleAnimation doubleAnimation = new DoubleAnimation();
            Duration duration = new Duration(TimeSpan.FromMilliseconds(200));
            translate.X = 0;
            doubleAnimation.To = sliderw;
            doubleAnimation.Duration = duration;
            translate.BeginAnimation(TranslateTransform.XProperty, doubleAnimation);
        }

        //未选中
        protected override void OnUnchecked(RoutedEventArgs e)
        {
            //lock (borderCheckFlag)
            //{
            //    DoubleAnimationUsingKeyFrames doubleAnimationUsingKeyFramesUnChecked = new DoubleAnimationUsingKeyFrames();
            //    EasingDoubleKeyFrame easingDoubleKeyFrame1 = new EasingDoubleKeyFrame(sliderw, KeyTime.FromTimeSpan(TimeSpan.FromMilliseconds(0)));
            //    EasingDoubleKeyFrame easingDoubleKeyFrame2 = new EasingDoubleKeyFrame(0, KeyTime.FromTimeSpan(TimeSpan.FromMilliseconds(200)));
            //    AnimationClock clock = doubleAnimationUsingKeyFramesUnChecked.CreateClock();
            //    translate.ApplyAnimationClock(TranslateTransform.XProperty, clock);
            //}
            DoubleAnimation doubleAnimation = new DoubleAnimation();
            Duration duration = new Duration(TimeSpan.FromMilliseconds(200));
            translate.X = sliderw;
            doubleAnimation.To = 0;
            doubleAnimation.Duration = duration;
            translate.BeginAnimation(TranslateTransform.XProperty, doubleAnimation);
        }
    }
}
