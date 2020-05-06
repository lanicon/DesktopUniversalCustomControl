using CustomControl.NotifycationObject;
using CustomControl.Resource.Dictionary;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Effects;

namespace CustomControl.Resource.Transitions
{
    public class AttachedProperty: DependencyObject
    {
        public static bool GetDisableTransitions(DependencyObject obj)
        {
            return (bool)obj.GetValue(DisableTransitionsProperty);
        }

        public static void SetDisableTransitions(DependencyObject obj, bool value)
        {
            obj.SetValue(DisableTransitionsProperty, value);
        }

        //Button的状态(是否被点击过)
        public static readonly DependencyProperty DisableTransitionsProperty =
            DependencyProperty.RegisterAttached("DisableTransitions", typeof(bool), typeof(AttachedProperty), new FrameworkPropertyMetadata(default(bool), FrameworkPropertyMetadataOptions.Inherits));


        public static Brush GetFillColor(DependencyObject obj)
        {
            return (Brush)obj.GetValue(FillColorProperty);
        }

        public static void SetFillColor(DependencyObject obj, Brush value)
        {
            obj.SetValue(FillColorProperty, value);
        }

        //ContentControlStyle动画填充颜色
        public static readonly DependencyProperty FillColorProperty =
            DependencyProperty.RegisterAttached("FillColor", typeof(Brush), typeof(AttachedProperty), new PropertyMetadata(Brushes.Yellow));



        //给定的某个值(如Height)
        private static double knownParameter = 0;
        public static double GetKnownParameter(DependencyObject obj)
        {
            return (double)obj.GetValue(KnownParameterProperty);
        }

        public static void SetKnownParameter(DependencyObject obj, double value)
        {
            obj.SetValue(KnownParameterProperty, value);
        }

        public static readonly DependencyProperty KnownParameterProperty =
            DependencyProperty.RegisterAttached("KnownParameter", typeof(double), typeof(AttachedProperty), new PropertyMetadata(0.0, OnKnownParameterChanged));

        private static void OnKnownParameterChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            knownParameter = (double)e.NewValue;
            SetCalculateByParameter(d, knownParameter * constantScale);
        }



        //固定比例
        private static double constantScale = 1;
        public static double GetConstantScale(DependencyObject obj)
        {
            return (double)obj.GetValue(ConstantScaleProperty);
        }

        public static void SetConstantScale(DependencyObject obj, double value)
        {
            obj.SetValue(ConstantScaleProperty, value);
        }

        public static readonly DependencyProperty ConstantScaleProperty =
            DependencyProperty.RegisterAttached("ConstantScale", typeof(double), typeof(AttachedProperty), new PropertyMetadata(1.0, OnConstantScaleChanged));

        private static void OnConstantScaleChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            constantScale = (double)e.NewValue;
            SetCalculateByParameter(d, knownParameter * constantScale);
        }


       
        public static double GetCalculateByParameter(DependencyObject obj)
        {
            return (double)obj.GetValue(CalculateByParameterProperty);
        }

        public static void SetCalculateByParameter(DependencyObject obj, double value)
        {
            obj.SetValue(CalculateByParameterProperty, value);
        }

        //根据给定的某个值(如Height)乘以固定比例(如,2*,1/2*)得到的值
        public static readonly DependencyProperty CalculateByParameterProperty =
            DependencyProperty.RegisterAttached("CalculateByParameter", typeof(double), typeof(AttachedProperty), new PropertyMetadata(0.0));
    }
}
