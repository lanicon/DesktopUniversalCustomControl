using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace DesktopUniversalCustomControl.CustomComponent
{
    /// <summary>
    /// CustomSlider控件
    /// </summary>
    public class CustomSlider : Slider
    {
        static CustomSlider()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(CustomSlider), new FrameworkPropertyMetadata(typeof(CustomSlider)));
        }

        /// <summary>
        /// SliderHeight(用于横向)
        /// </summary>
        public double SliderHeight
        {
            get { return (double)GetValue(SliderHeightProperty); }
            set { SetValue(SliderHeightProperty, value); }
        }

        public static readonly DependencyProperty SliderHeightProperty =
            DependencyProperty.Register("SliderHeight", typeof(double), typeof(CustomSlider), new PropertyMetadata());


        /// <summary>
        /// SliderWidth(用于纵向)
        /// </summary>
        public double SliderWidth
        {
            get { return (double)GetValue(SliderWidthProperty); }
            set { SetValue(SliderWidthProperty, value); }
        }

        public static readonly DependencyProperty SliderWidthProperty =
            DependencyProperty.Register("SliderWidth", typeof(double), typeof(CustomSlider), new PropertyMetadata());


        /// <summary>
        /// 刻度文本
        /// </summary>
        public StringCollection TicksText
        {
            get { return (StringCollection)GetValue(TicksTextProperty); }
            set { SetValue(TicksTextProperty, value); }
        }

        public static readonly DependencyProperty TicksTextProperty =
            DependencyProperty.Register("TicksText", typeof(StringCollection), typeof(CustomSlider), new PropertyMetadata());


        /// <summary>
        /// SliderBackground
        /// </summary>
        public Brush SliderBackground
        {
            get { return (Brush)GetValue(SliderBackgroundProperty); }
            set { SetValue(SliderBackgroundProperty, value); }
        }

        public static readonly DependencyProperty SliderBackgroundProperty =
            DependencyProperty.Register("SliderBackground", typeof(Brush), typeof(CustomSlider), new PropertyMetadata());


        /// <summary>
        /// 形状类型
        /// </summary>
        public ShapeEnum ShapeType
        {
            get { return (ShapeEnum)GetValue(ShapeTypeProperty); }
            set { SetValue(ShapeTypeProperty, value); }
        }

        public static readonly DependencyProperty ShapeTypeProperty =
            DependencyProperty.Register("ShapeType", typeof(ShapeEnum), typeof(CustomSlider), new PropertyMetadata(ShapeEnum.Circle));


        /// <summary>
        /// Thumb的填充颜色
        /// </summary>
        public Brush ThumbBrush
        {
            get { return (Brush)GetValue(ThumbBrushProperty); }
            set { SetValue(ThumbBrushProperty, value); }
        }

        public static readonly DependencyProperty ThumbBrushProperty =
            DependencyProperty.Register("ThumbBrush", typeof(Brush), typeof(CustomSlider), new PropertyMetadata(SystemColors.ControlBrush));


        /// <summary>
        /// Thumb呈现大小
        /// </summary>
        public double ThumbSize
        {
            get { return (double)GetValue(ThumbSizeProperty); }
            set { SetValue(ThumbSizeProperty, value); }
        }

        public static readonly DependencyProperty ThumbSizeProperty =
            DependencyProperty.Register("ThumbSize", typeof(double), typeof(CustomSlider), new PropertyMetadata(20D));


        /// <summary>
        /// Value是否显示
        /// </summary>
        public bool SliderTextShow
        {
            get { return (bool)GetValue(SliderTextShowProperty); }
            set { SetValue(SliderTextShowProperty, value); }
        }

        public static readonly DependencyProperty SliderTextShowProperty =
            DependencyProperty.Register("SliderTextShow", typeof(bool), typeof(CustomSlider), new PropertyMetadata(false));


        /// <summary>
        /// CornerRadius
        /// </summary>
        public CornerRadius CornerRadius
        {
            get { return (CornerRadius)GetValue(CornerRadiusProperty); }
            set { SetValue(CornerRadiusProperty, value); }
        }

        public static readonly DependencyProperty CornerRadiusProperty =
            DependencyProperty.Register("CornerRadius", typeof(CornerRadius), typeof(CustomSlider), new PropertyMetadata(OnCornerRadiusChanged));

        private static void OnCornerRadiusChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if(d != null && d is CustomSlider)
            {
                var slider = d as CustomSlider;
                double value = slider.CornerRadius.TopLeft;
                if (slider.Orientation == Orientation.Horizontal)
                {
                    slider.DecreaseRepeatButtonCornerRadius = new CornerRadius(value, 0, 0, value);
                    slider.IncreaseRepeatButtonCornerRadius = new CornerRadius(0, value, value, 0);
                }
                else
                {
                    slider.DecreaseRepeatButtonCornerRadius = new CornerRadius(0, 0, value, value);
                    slider.IncreaseRepeatButtonCornerRadius = new CornerRadius(value, value, 0, 0);
                }
            }
        }


        /// <summary>
        /// CustomSlider方向
        /// </summary>
        public new Orientation Orientation
        {
            get { return (Orientation)GetValue(OrientationProperty); }
            set { SetValue(OrientationProperty, value); }
        }

        public new static readonly DependencyProperty OrientationProperty =
            DependencyProperty.Register("Orientation", typeof(Orientation), typeof(CustomSlider), new PropertyMetadata(Orientation.Horizontal, OnCornerRadiusChanged));


        /// <summary>
        /// DecreaseRepeatButtonCornerRadius
        /// </summary>
        public CornerRadius DecreaseRepeatButtonCornerRadius
        {
            get { return (CornerRadius)GetValue(DecreaseRepeatButtonCornerRadiusProperty); }
            set { SetValue(DecreaseRepeatButtonCornerRadiusProperty, value); }
        }

        public static readonly DependencyProperty DecreaseRepeatButtonCornerRadiusProperty =
            DependencyProperty.Register("DecreaseRepeatButtonCornerRadius", typeof(CornerRadius), typeof(CustomSlider), new PropertyMetadata());


        /// <summary>
        /// IncreaseRepeatButtonCornerRadius
        /// </summary>
        public CornerRadius IncreaseRepeatButtonCornerRadius
        {
            get { return (CornerRadius)GetValue(IncreaseRepeatButtonCornerRadiusProperty); }
            set { SetValue(IncreaseRepeatButtonCornerRadiusProperty, value); }
        }

        public static readonly DependencyProperty IncreaseRepeatButtonCornerRadiusProperty =
            DependencyProperty.Register("IncreaseRepeatButtonCornerRadius", typeof(CornerRadius), typeof(CustomSlider), new PropertyMetadata());


        public static ShapeEnum GetShape(DependencyObject obj)
        {
            return (ShapeEnum)obj.GetValue(ShapeProperty);
        }

        public static void SetShape(DependencyObject obj, ShapeEnum value)
        {
            obj.SetValue(ShapeProperty, value);
        }

        //Thumb形状
        public static readonly DependencyProperty ShapeProperty =
            DependencyProperty.RegisterAttached("Shape", typeof(ShapeEnum), typeof(CustomSlider), new PropertyMetadata(ShapeEnum.Circle));    
    }

    public enum ShapeEnum
    {
        Rectangle = 0,
        Ellipse = 1,
        Circle = 2,
    }
}
