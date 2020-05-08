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
            //if (MutilComboBox.ItemsSource == null)
            //    MutilComboBox.ItemsSource = MutilComboBox.Items;
            //SetListItemsSource(MutilComboBox, MutilComboBox.Items);
        }


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
            DependencyProperty.Register("StrokeLineColor", typeof(Brush), typeof(MutilComboBoxControl), new PropertyMetadata(Brushes.White));


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


        [Description("删除按钮点击时发生")]
        public event RoutedEventHandler DeleteButtonIsCheckedChanged
        {
            add
            {
                this.AddHandler(DeleteButtonIsCheckedChangedEvent, value);
            }
            remove
            {
                this.RemoveHandler(DeleteButtonIsCheckedChangedEvent, value);
            }
        }

        public static readonly RoutedEvent DeleteButtonIsCheckedChangedEvent =
            EventManager.RegisterRoutedEvent("DeleteButtonIsCheckedChanged", RoutingStrategy.Bubble, typeof(Boolean), typeof(MutilComboBoxControl));

        protected virtual void OnDeleteButtonIsCheckedChanged(bool oldString, bool newString)
        {
            RoutedPropertyChangedEventArgs<bool> arg = new RoutedPropertyChangedEventArgs<bool>(oldString, newString, DeleteButtonIsCheckedChangedEvent);
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
}
