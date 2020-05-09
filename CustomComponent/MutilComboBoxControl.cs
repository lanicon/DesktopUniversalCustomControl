using CustomControl.ExposedMethod;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
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

namespace CustomControl.CustomComponent
{
    /// <summary>
    /// 多选下拉框
    /// MutilComboBoxControl
    /// </summary>
    public class MutilComboBoxControl : CustomComboBox
    {
        static MutilComboBoxControl()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(MutilComboBoxControl), new FrameworkPropertyMetadata(typeof(MutilComboBoxControl)));
        }

        private static MutilComboBoxControl MutilComboBox = null;
        public MutilComboBoxControl()
        {
            MutilComboBox = this;
            MutilComboBox.Loaded += MutilComboBox_Loaded;
            
        }

        private void MutilComboBox_Loaded(object sender, RoutedEventArgs e)
        {
 
        }



        /// <summary>
        /// 子项的Lable内容
        /// </summary>
        public object SelectedItemContent
        {
            get { return (object)GetValue(SelectedItemContentProperty); }
            set { SetValue(SelectedItemContentProperty, value); }
        }

        public static readonly DependencyProperty SelectedItemContentProperty =
            DependencyProperty.Register("SelectedItemContent", typeof(object), typeof(MutilComboBoxControl), new PropertyMetadata(default(object)));


        /// <summary>
        /// 子项类型
        /// </summary>
        public ItemType ItemType
        {
            get { return (ItemType)GetValue(ItemTypeProperty); }
            set { SetValue(ItemTypeProperty, value); }
        }

        public static readonly DependencyProperty ItemTypeProperty =
            DependencyProperty.Register("ItemType", typeof(ItemType), typeof(MutilComboBoxControl), 
                new PropertyMetadata(ItemType.CheckBox,changed));

        private static void changed(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            SetCheckBoxItemTypeIndex(MutilComboBox, false);
            SetButtonItemTypeIndex(MutilComboBox, false);
            SetImageItemTypeIndex(MutilComboBox, false);

            switch (MutilComboBox.ItemType)
            {
                case ItemType.CheckBox:
                    SetCheckBoxItemTypeIndex(MutilComboBox, true);
                    break;
                case ItemType.Button:
                    SetButtonItemTypeIndex(MutilComboBox, true);
                    break;
                case ItemType.Image:
                    SetImageItemTypeIndex(MutilComboBox, true);
                    break;
                default:
                    break;
            }
            
            //Console.WriteLine($"序号：{MutilComboBox.ItemType}"); 
        }

        public static bool GetCheckBoxItemTypeIndex(DependencyObject obj)
        {
            return (bool)obj.GetValue(CheckBoxItemTypeIndexProperty);
        }

        public static void SetCheckBoxItemTypeIndex(DependencyObject obj, bool value)
        {
            obj.SetValue(CheckBoxItemTypeIndexProperty, value);
        }

        // CheckBoxItemTypeIndex的序号
        public static readonly DependencyProperty CheckBoxItemTypeIndexProperty =
            DependencyProperty.RegisterAttached("CheckBoxItemTypeIndex", typeof(bool), typeof(MutilComboBoxControl), new PropertyMetadata(true));


        /// <summary>
        /// 按钮内容
        /// </summary>
        public string ItemButtonContent
        {
            get { return (string)GetValue(ItemButtonContentProperty); }
            set { SetValue(ItemButtonContentProperty, value); }
        }

        public static readonly DependencyProperty ItemButtonContentProperty =
            DependencyProperty.Register("ItemButtonContent", typeof(string), typeof(MutilComboBoxControl), new PropertyMetadata(string.Empty));


        public static bool GetButtonItemTypeIndex(DependencyObject obj)
        {
            return (bool)obj.GetValue(ButtonItemTypeIndexProperty);
        }

        public static void SetButtonItemTypeIndex(DependencyObject obj, bool value)
        {
            obj.SetValue(ButtonItemTypeIndexProperty, value);
        }

        // ButtonItemTypeIndex的序号
        public static readonly DependencyProperty ButtonItemTypeIndexProperty =
            DependencyProperty.RegisterAttached("ButtonItemTypeIndex", typeof(bool), typeof(MutilComboBoxControl), new PropertyMetadata(false));



        public static bool GetIsDeleteButtonPressed(DependencyObject obj)
        {
            return (bool)obj.GetValue(IsDeleteButtonPressedProperty);
        }

        public static void SetIsDeleteButtonPressed(DependencyObject obj, bool value)
        {
            obj.SetValue(IsDeleteButtonPressedProperty, value);
        }

        //是否点击删除子项按钮
        public static readonly DependencyProperty IsDeleteButtonPressedProperty =
            DependencyProperty.RegisterAttached("IsDeleteButtonPressed", typeof(bool), typeof(MutilComboBoxControl), 
                new PropertyMetadata(false,IsPressedChanged));
        private static Label lable;
        private static void IsPressedChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var listBoxItem = d as ListBoxItem;
            lable = listBoxItem.Template.FindName("lb", listBoxItem) as Label;
            if((bool)e.NewValue)
            {
                MutilComboBox.SelectedItemContent = lable.Content;
                MutilComboBox.OnDeleteItemValueEvent(e.OldValue, e.NewValue);
                Console.WriteLine(lable.Content);
            }              
        }


        [Description("删除按钮点击时发生")]
        public event RoutedEventHandler DeleteItemValue
        {
            add
            {
                this.AddHandler(DeleteItemValueEvent, value);
            }
            remove
            {
                this.RemoveHandler(DeleteItemValueEvent, value);
            }
        }

        //删除按钮事件
        public static readonly RoutedEvent DeleteItemValueEvent =
            EventManager.RegisterRoutedEvent("DeleteItemValue", RoutingStrategy.Bubble, typeof(RoutedPropertyChangedEventArgs<object>), typeof(MutilComboBoxControl));
        
        protected virtual void OnDeleteItemValueEvent(object oldString, object newString)
        {
            //RoutedEventArgs args = new RoutedEventArgs(DeleteItemValueEvent, this);
            RoutedPropertyChangedEventArgs<object> arg = new RoutedPropertyChangedEventArgs<object>(oldString, newString, DeleteItemValueEvent);
            MutilComboBox.RaiseEvent(arg); //引用自定义事件
            //Button.RaiseEvent(new RoutedEventArgs(Button.ClickEvent));
        }



        /// <summary>
        /// 图片地址
        /// </summary>
        public ImageSource ItemImageSource
        {
            get { return (ImageSource)GetValue(ItemImageSourceProperty); }
            set { SetValue(ItemImageSourceProperty, value); }
        }

        public static readonly DependencyProperty ItemImageSourceProperty =
            DependencyProperty.Register("ItemImageSource", typeof(ImageSource), typeof(MutilComboBoxControl), new PropertyMetadata());


        public static bool GetImageItemTypeIndex(DependencyObject obj)
        {
            return (bool)obj.GetValue(ImageItemTypeIndexProperty);
        }

        public static void SetImageItemTypeIndex(DependencyObject obj, bool value)
        {
            obj.SetValue(ImageItemTypeIndexProperty, value);
        }

        //ImageItemTypeIndex的序号
        public static readonly DependencyProperty ImageItemTypeIndexProperty =
            DependencyProperty.RegisterAttached("ImageItemTypeIndex", typeof(bool), typeof(MutilComboBoxControl), new PropertyMetadata(false));



        /// <summary>
        /// 是否添加删除按钮
        /// </summary>
        public bool AddDeleteFun
        {
            get { return (bool)GetValue(AddDeleteFunProperty); }
            set { SetValue(AddDeleteFunProperty, value); }
        }

        public static readonly DependencyProperty AddDeleteFunProperty =
            DependencyProperty.Register("AddDeleteFun", typeof(bool), typeof(MutilComboBoxControl), new PropertyMetadata(false));


        /// <summary>
        /// 删除按钮Stroke的颜色
        /// </summary>
        public Brush StrokeLineColor
        {
            get { return (Brush)GetValue(StrokeLineColorProperty); }
            set { SetValue(StrokeLineColorProperty, value); }
        }
      
        public static readonly DependencyProperty StrokeLineColorProperty =
            DependencyProperty.Register("StrokeLineColor", typeof(Brush), typeof(MutilComboBoxControl), new PropertyMetadata());


        public static bool GetIsDeletedContent(DependencyObject obj)
        {
            return (bool)obj.GetValue(IsDeletedContentProperty);
        }

        public static void SetIsDeletedContent(DependencyObject obj, bool value)
        {
            obj.SetValue(IsDeletedContentProperty, value);
        }

        // 是否清空内容
        public static readonly DependencyProperty IsDeletedContentProperty =
            DependencyProperty.RegisterAttached("IsDeletedContent", typeof(bool), typeof(MutilComboBoxControl), 
                new PropertyMetadata(false,new PropertyChangedCallback(DeleteContentChanged)));

        private static void DeleteContentChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var mutil = d as MutilComboBoxControl;
            mutil.Text = null;
            contentList.Clear();
            var lb = mutil.Template.FindName("dropDownListBox", mutil) as ListBox;
            lb.UnselectAll();
        }


        [Description("清空文本按钮点击时发生")]
        public event RoutedEventHandler ClearButtonIsCheckedChanged
        {
            add
            {
                this.AddHandler(ClearButtonIsCheckedChangedEvent, value);
            }
            remove
            {
                this.RemoveHandler(ClearButtonIsCheckedChangedEvent, value);
            }
        }
        //清空文本事件
        public static readonly RoutedEvent ClearButtonIsCheckedChangedEvent =
            EventManager.RegisterRoutedEvent("ClearButtonIsCheckedChanged", RoutingStrategy.Bubble, typeof(Boolean), typeof(MutilComboBoxControl));
        
        protected virtual void OnClearButtonIsCheckedChanged(bool oldString, bool newString)
        {
            RoutedPropertyChangedEventArgs<bool> arg = new RoutedPropertyChangedEventArgs<bool>(oldString, newString, ClearButtonIsCheckedChangedEvent);
            this.RaiseEvent(arg);
        }



        public static SelectedType GetSelectedType(DependencyObject obj)
        {
            return (SelectedType)obj.GetValue(SelectedTypeProperty);
        }

        public static void SetSelectedType(DependencyObject obj, SelectedType value)
        {
            obj.SetValue(SelectedTypeProperty, value);
        }

        //子项选择方式(默认整个子项而非只有CheckBox)
        public static readonly DependencyProperty SelectedTypeProperty =
            DependencyProperty.RegisterAttached("SelectedType", typeof(SelectedType), typeof(MutilComboBoxControl), new PropertyMetadata(SelectedType.MutilItem));


        public static bool GetIsChecked(DependencyObject obj)
        {
            return (bool)obj.GetValue(IsCheckedProperty);
        }

        public static void SetIsChecked(DependencyObject obj, bool value)
        {
            obj.SetValue(IsCheckedProperty, value);
        }

        // 检查CheckBox是否Checked
        public static readonly DependencyProperty IsCheckedProperty =
            DependencyProperty.RegisterAttached("IsChecked", typeof(bool), typeof(MutilComboBoxControl), 
                new PropertyMetadata(default(bool),new PropertyChangedCallback(IsCheckedChanged)));

        private static List<string> contentList = new List<string>();
        public static string textMutil = "";
        //选择子项改变文本显示内容
        private static void IsCheckedChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {         
            var listBoxItem = d as ListBoxItem;
            bool isChecked = (bool)e.NewValue;
            string content = listBoxItem.Content.ToString();
            if (isChecked)
                contentList.Add(content);
            else
                contentList.Remove(content);
            textMutil = string.Empty;
            foreach (var s in contentList)
            {
                textMutil += s + ',';
            }
            if (textMutil.Length != 0)
                textMutil = textMutil.Remove(textMutil.Length - 1);

            SetMutilComboBoxContent(MutilComboBox, textMutil);
            MutilComboBox.Text = textMutil;   //设置文本值
            //Console.WriteLine("MutilComboBoxContent:" + GetMutilComboBoxContentt(MutilComboBox));
        }


        public static string GetMutilComboBoxContentt(DependencyObject obj)
        {
            return (string)obj.GetValue(MutilComboBoxContentProperty);
        }

        public static void SetMutilComboBoxContent(DependencyObject obj, string value)
        {
            obj.SetValue(MutilComboBoxContentProperty, value);
        }

        //MutilComboBoxControl文本内容
        public static readonly DependencyProperty MutilComboBoxContentProperty =
            DependencyProperty.RegisterAttached("MutilComboBoxContent", typeof(string), typeof(MutilComboBoxControl), new PropertyMetadata());


        public static ItemCollection GetListItemsSource(DependencyObject obj)
        {
            return (ItemCollection)obj.GetValue(ListItemsSourceProperty);
        }

        public static void SetListItemsSource(DependencyObject obj, ItemCollection value)
        {
            obj.SetValue(ListItemsSourceProperty, value);
        }

        // 获取绑定资源集合(预留)
        public static readonly DependencyProperty ListItemsSourceProperty =
            DependencyProperty.RegisterAttached("ListItemsSource", typeof(ItemCollection), typeof(MutilComboBoxControl), new PropertyMetadata());
    }
    
    /// <summary>
    /// 选择类型
    /// </summary>
    public enum SelectedType
    {
        OnlyCheckBox = 1, //仅CheckBox
        MutilItem = 2     //整个子项
    }

    /// <summary>
    /// 内容类型
    /// </summary>
    public enum ItemType
    {
        CheckBox = 1, //文本+选择框
        Button = 2, //文本+按钮
        Image = 3, //文本+图片
    }
}
