using System.Windows;
using System.Windows.Controls;

namespace DesktopUniversalCustomControl.CustomComponent
{
    /// <summary>
    /// CustomComboBoxItem控件
    /// </summary>
    public class CustomComboBoxItem : ComboBoxItem
    {
        static CustomComboBoxItem()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(CustomComboBoxItem), new FrameworkPropertyMetadata(typeof(CustomComboBoxItem)));
        }
    }
}
