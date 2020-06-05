using System;
using System.Collections.Generic;
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
    /// CustomComboBox控件
    /// </summary>
    public class CustomComboBox : ComboBox
    {
        static CustomComboBox()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(CustomComboBox), new FrameworkPropertyMetadata(typeof(CustomComboBox)));
        }

        public CustomComboBox()
        {
            this.Loaded += CustomComboBox_Loaded;         
        }

        private CustomComboBox cus;
        private void CustomComboBox_Loaded(object sender, RoutedEventArgs e)
        {
            cus = sender as CustomComboBox;
            var border = this.Template.FindName("border", cus) as Border;
            if(border != null)
                border.MouseLeftButtonDown += Border_MouseLeftButtonDown;   //控制下拉框开关     
        }

        private void Border_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {          
            if (cus.IsDropDownOpen)
                cus.IsDropDownOpen = false;
            else
                cus.IsDropDownOpen = true;
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
            DependencyProperty.Register("CornerRadius", typeof(CornerRadius), typeof(CustomComboBox), new PropertyMetadata());


        /// <summary>
        /// 下拉列表背景颜色
        /// </summary>
        public Brush ComboBoxListBackground
        {
            get { return (Brush)GetValue(ComboBoxListBackgroundProperty); }
            set { SetValue(ComboBoxListBackgroundProperty, value); }
        }

        public static readonly DependencyProperty ComboBoxListBackgroundProperty =
            DependencyProperty.Register("ComboBoxListBackground", typeof(Brush), typeof(CustomComboBox), new PropertyMetadata());


        /// <summary>
        /// 鼠标在ComboBoxItem上方时背景色
        /// </summary>
        public Brush ComboxBoxItemMouseOverBackground
        {
            get { return (Brush)GetValue(ComboxBoxItemMouseOverBackgroundProperty); }
            set { SetValue(ComboxBoxItemMouseOverBackgroundProperty, value); }
        }

        public static readonly DependencyProperty ComboxBoxItemMouseOverBackgroundProperty =
            DependencyProperty.Register("ComboxBoxItemMouseOverBackground", typeof(Brush), typeof(CustomComboBox), new PropertyMetadata());


        /// <summary>
        /// ToggleButton填充颜色
        /// </summary>
        public Brush ToggleButtonBackground
        {
            get { return (Brush)GetValue(ToggleButtonBackgroundProperty); }
            set { SetValue(ToggleButtonBackgroundProperty, value); }
        }

        public static readonly DependencyProperty ToggleButtonBackgroundProperty =
            DependencyProperty.Register("ToggleButtonBackground", typeof(Brush), typeof(CustomComboBox), new PropertyMetadata());
    }
}
