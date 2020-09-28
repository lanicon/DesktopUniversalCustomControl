using System.Windows;
using System.Windows.Controls;

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


    }
}
