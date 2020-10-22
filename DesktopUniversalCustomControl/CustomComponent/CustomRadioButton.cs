using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace DesktopUniversalCustomControl.CustomComponent
{
    /// <summary>
    /// ChanceDesignRadioButton
    /// </summary>
    public class CustomRadioButton : RadioButton
    {
        static CustomRadioButton()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(CustomRadioButton), new FrameworkPropertyMetadata(typeof(CustomRadioButton)));
        }

        public CustomRadioButton()
        {

        }


        public Brush FillBrush
        {
            get
            {
                return (Brush)GetValue(FillBrushProperty);
            }
            set
            {
                SetValue(FillBrushProperty, value);
            }
        }
        public static readonly DependencyProperty FillBrushProperty =
            DependencyProperty.Register("FillBrush", typeof(Brush), typeof(CustomRadioButton), new PropertyMetadata(default(Brush)));
    }
}
