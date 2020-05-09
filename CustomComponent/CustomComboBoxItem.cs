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
