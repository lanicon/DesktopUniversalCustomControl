using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
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
    /// CustomCheckBox
    /// </summary>
    public class CustomCheckBox : CheckBox
    {
        static CustomCheckBox()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(CustomCheckBox), new FrameworkPropertyMetadata(typeof(CustomCheckBox)));
        }

        public Brush IsMouseOverBackground
        {
            get { return (Brush)GetValue(IsMouseOverBackgroundProperty); }
            set { SetValue(IsMouseOverBackgroundProperty, value); }
        }
        public static readonly DependencyProperty IsMouseOverBackgroundProperty =
            DependencyProperty.Register("IsMouseOverBackground", typeof(Brush), typeof(CustomCheckBox), new PropertyMetadata(default(Brush)));


        public Brush FillBrush
        {
            get { return (Brush)GetValue(FillBrushProperty); }
            set { SetValue(FillBrushProperty, value); }
        }
        public static readonly DependencyProperty FillBrushProperty =
            DependencyProperty.Register("FillBrush", typeof(Brush), typeof(CustomCheckBox), new PropertyMetadata(default(Brush)));


        public FillType FillType
        {
            get { return (FillType)GetValue(FillTypeProperty); }
            set {  SetValue(FillTypeProperty, value); }
        }
        public static readonly DependencyProperty FillTypeProperty =
            DependencyProperty.Register("FillType", typeof(FillType), typeof(CustomCheckBox), new PropertyMetadata(default(FillType)));
    }


    public enum FillType
    {
        Check,
        Rectangle,
    }
}
