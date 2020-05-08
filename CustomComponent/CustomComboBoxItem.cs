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


        /// <summary>
        /// 子项类型
        /// </summary>
        public ItemType ItemType
        {
            get { return (ItemType)GetValue(ItemTypeProperty); }
            set { SetValue(ItemTypeProperty, value); }
        }

        public static readonly DependencyProperty ItemTypeProperty =
            DependencyProperty.Register("ItemType", typeof(ItemType), typeof(CustomComboBoxItem), new PropertyMetadata(ItemType.Text));


        /// <summary>
        /// CheckBox是否选中
        /// </summary>
        public bool ItemCheckBoxIsChecked
        {
            get { return (bool)GetValue(ItemCheckBoxIsCheckedProperty); }
            set { SetValue(ItemCheckBoxIsCheckedProperty, value); }
        }

        public static readonly DependencyProperty ItemCheckBoxIsCheckedProperty =
            DependencyProperty.Register("ItemCheckBoxIsChecked", typeof(bool), typeof(CustomComboBoxItem), 
                new PropertyMetadata(default(bool),new PropertyChangedCallback(IsCheckedChanged)));

        private List<string> contentList = new List<string>();
        private static void IsCheckedChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var cus = d as CustomComboBoxItem;
            var content = cus.Content.ToString().Split(':')[1] ?? string.Empty;
            if (cus.ItemCheckBoxIsChecked)
            {
                if (!cus.contentList.Contains(content))
                    cus.contentList.Add(cus.Content.ToString().Split(':')[1]);
            }
            else
                cus.contentList.Remove(content);
            string text = "";
            foreach (var s in cus.contentList)
            {
                text += s;
            }
            //MutilComboBoxControl.SetMutilComboBoxContent(d, text);
            //Console.WriteLine(MutilComboBoxControl.GetMutilComboBoxContentt(d) + "sowhiche");
        }


        /// <summary>
        /// 按钮内容
        /// </summary>
        public string ItemButtonContent
        {
            get { return (string)GetValue(ItemButtonContentProperty); }
            set { SetValue(ItemButtonContentProperty, value); }
        }

        public static readonly DependencyProperty ItemButtonContentProperty =
            DependencyProperty.Register("ItemButtonContent", typeof(string), typeof(CustomComboBoxItem), new PropertyMetadata(string.Empty));


        /// <summary>
        /// 图片地址
        /// </summary>
        public ImageSource ItemImageSource
        {
            get { return (ImageSource)GetValue(ItemImageSourceProperty); }
            set { SetValue(ItemImageSourceProperty, value); }
        }

        public static readonly DependencyProperty ItemImageSourceProperty =
            DependencyProperty.Register("ItemImageSource", typeof(ImageSource), typeof(CustomComboBoxItem), new PropertyMetadata());


    }


    /// <summary>
    /// 内容类型
    /// </summary>
    public enum ItemType
    {
        Text = 1, //纯文本
        CheckBox = 2, //文本+选择框
        Button = 3, //文本+按钮
        Image = 4, //文本+图片
    }
}
