using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
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
    /// ImageButton控件
    /// </summary>
    public class ImageButton : Button
    {
        static ImageButton()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(ImageButton), new FrameworkPropertyMetadata(typeof(ImageButton)));
        }

        /// <summary>
        /// ImageButtonSource
        /// </summary>
        public ImageSource ImageButtonSource
        {
            get { return (ImageSource)GetValue(ImageButtonSourceProperty); }
            set { SetValue(ImageButtonSourceProperty, value); }
        }

        public static readonly DependencyProperty ImageButtonSourceProperty =
            DependencyProperty.Register("ImageButtonSource", typeof(ImageSource), typeof(ImageButton), new PropertyMetadata());


        /// <summary>
        /// ImageButtonContent
        /// </summary>
        public String ImageButtonContent
        {
            get { return (String)GetValue(ImageButtonContentProperty); }
            set { SetValue(ImageButtonContentProperty, value); }
        }

        public static readonly DependencyProperty ImageButtonContentProperty =
            DependencyProperty.Register("ImageButtonContent", typeof(String), typeof(ImageButton), new PropertyMetadata());


        /// <summary>
        /// ImageVisibility
        /// </summary>
        public Visibility ImageVisibility
        {
            get { return (Visibility)GetValue(ImageVisibilityProperty); }
            set { SetValue(ImageVisibilityProperty, value); }
        }

        public static readonly DependencyProperty ImageVisibilityProperty =
            DependencyProperty.Register("ImageVisibility", typeof(Visibility), typeof(ImageButton), new PropertyMetadata(Visibility.Visible));


        /// <summary>
        /// ImageWidth
        /// </summary>
        public int ImageWidth
        {
            get { return (int)GetValue(ImageWidthProperty); }
            set { SetValue(ImageWidthProperty, value); }
        }

        public static readonly DependencyProperty ImageWidthProperty =
            DependencyProperty.Register("ImageWidth", typeof(int), typeof(ImageButton), new FrameworkPropertyMetadata(30, FrameworkPropertyMetadataOptions.Inherits));


        /// <summary>
        /// ImageHeight
        /// </summary>
        public int ImageHeight
        {
            get { return (int)GetValue(ImageHeightProperty); }
            set { SetValue(ImageHeightProperty, value); }
        }

        public static readonly DependencyProperty ImageHeightProperty =
            DependencyProperty.Register("ImageHeight", typeof(int), typeof(ImageButton), new FrameworkPropertyMetadata(30, FrameworkPropertyMetadataOptions.Inherits));


        /// <summary>
        /// CornerRadius
        /// </summary>
        public CornerRadius CornerRadius
        {
            get { return (CornerRadius)GetValue(CornerRadiusProperty); }
            set { SetValue(CornerRadiusProperty, value); }
        }

        public static readonly DependencyProperty CornerRadiusProperty =
            DependencyProperty.Register("CornerRadius", typeof(CornerRadius), typeof(ImageButton), new PropertyMetadata());


        /// <summary>
        /// MouseOver Background
        /// </summary>
        public Brush OverBackground
        {
            get { return (Brush)GetValue(OverBackgroundProperty); }
            set { SetValue(OverBackgroundProperty, value); }
        }

        public static readonly DependencyProperty OverBackgroundProperty =
            DependencyProperty.Register("OverBackground", typeof(Brush), typeof(ImageButton), new PropertyMetadata());
    }
}
