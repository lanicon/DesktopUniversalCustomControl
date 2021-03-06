﻿using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;

namespace DesktopUniversalCustomControl.CustomComponent
{
    /// <summary>
    /// 文本控件 
    /// </summary>
    public class CustomTextControl : TextBox
    {
        static CustomTextControl()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(CustomTextControl), new FrameworkPropertyMetadata(typeof(CustomTextControl)));
        }


        /// <summary>
        /// 输入类型(默认defaultText)
        /// </summary>
        public TextInputType TextInputType
        {
            get { return (TextInputType)GetValue(TextInputTypeProperty); }
            set { SetValue(TextInputTypeProperty, value); }
        }

        public static readonly DependencyProperty TextInputTypeProperty =
            DependencyProperty.Register("TextInputType", typeof(TextInputType), typeof(CustomTextControl), new PropertyMetadata(TextInputType.defaultText, OnInputTypeChanged));

        private static void OnInputTypeChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var textControl = d as CustomTextControl;
            string pattern; //匹配规则 

            if ((TextInputType)e.NewValue == TextInputType.digit)
                pattern = @"^(\-|\+?)\d+(\.\d+)?$"; //"^[0-9]*$"; //数字
            else if ((TextInputType)e.NewValue == TextInputType.letter)
                pattern = "^[A-Za-z]+$"; //只能输入字母 大写->A-Z,小写a-z
            else if ((TextInputType)e.NewValue == TextInputType.digitOrLetterLine)
                pattern = @"^\w+$"; //由数字、26个英文字母或者下划线组成的字符串
            else if ((TextInputType)e.NewValue == TextInputType.digitAndLetter)
                pattern = @"^(?![^\d]+$)(?![^a-zA-Z]+$)[^\u4e00-\u9fa5\s]+$"; //不能没有数字，不能没有字母，不能包含中文和空白符(这条如果没有限制[^\u4e00-\u9fa5\s]+$改为.+$ 
            else if ((TextInputType)e.NewValue == TextInputType.chinese)
                pattern = "^[\u4e00-\u9fa5]+$"; //只能输入汉字
            else
                pattern = string.Empty; //默认

            TextControlTextChanged(textControl, (int)textControl.TextInputType, pattern);
            textControl.TextChanged += delegate { TextControlTextChanged(textControl, (int)textControl.TextInputType, pattern); };
        }

        static string[] wrongs = new string[6] { null, "请输入数字", "请输入字母", "请输入数字、字母或者下划线组成的字符串", "请输入数字和字母组成的字符串", "只能输入汉字" };
        private static void TextControlTextChanged(CustomTextControl textControl, int index, string pattern)
        {
            Regex regex = new Regex(pattern);
            bool result = regex.IsMatch(textControl.Text);
            if (result)
                SetIsMatchRule(textControl, true);
            else
                SetIsMatchRule(textControl, false);

            Validation.ErrorEvent.AddOwner(typeof(CustomTextControl));
            SetWrongTextIndicate(textControl, wrongs[index]);
            if (string.IsNullOrEmpty(textControl.TextPlaceHolder))
                textControl.TextPlaceHolder = wrongs[index];
        }


        /// <summary>
        /// 占位符提示
        /// </summary>
        public string TextPlaceHolder
        {
            get { return (string)GetValue(TextPlaceHolderProperty); }
            set { SetValue(TextPlaceHolderProperty, value); }
        }

        public static readonly DependencyProperty TextPlaceHolderProperty =
            DependencyProperty.Register("TextPlaceHolder", typeof(string), typeof(CustomTextControl), new PropertyMetadata(null));


        /// <summary>
        /// CornerRadius
        /// </summary>
        public CornerRadius CornerRadius
        {
            get { return (CornerRadius)GetValue(CornerRadiusProperty); }
            set { SetValue(CornerRadiusProperty, value); }
        }

        public static readonly DependencyProperty CornerRadiusProperty =
            DependencyProperty.Register("CornerRadius", typeof(CornerRadius), typeof(CustomTextControl), new PropertyMetadata());


        public static bool GetIsMatchRule(DependencyObject obj)
        {
            return (bool)obj.GetValue(IsMatchRuleProperty);
        }

        public static void SetIsMatchRule(DependencyObject obj, bool value)
        {
            obj.SetValue(IsMatchRuleProperty, value);
        }

        //是否满足规则
        public static readonly DependencyProperty IsMatchRuleProperty =
            DependencyProperty.RegisterAttached("IsMatchRule", typeof(bool), typeof(CustomTextControl), new PropertyMetadata(true));


        public static string GetWrongTextIndicate(DependencyObject obj)
        {
            return (string)obj.GetValue(WrongTextIndicateProperty);
        }

        public static void SetWrongTextIndicate(DependencyObject obj, string value)
        {
            obj.SetValue(WrongTextIndicateProperty, value);
        }

        //错误内容提示
        public static readonly DependencyProperty WrongTextIndicateProperty =
            DependencyProperty.RegisterAttached("WrongTextIndicate", typeof(string), typeof(CustomTextControl), new PropertyMetadata(null));
    }

    /// <summary>
    /// 文本框输入类型
    /// </summary>
    public enum TextInputType
    {
        defaultText = 0,//普通文本(包括汉字)
        digit = 1, //仅实数
        letter = 2,  //仅字母
        digitOrLetterLine = 3, //数字、字母或下划线
        digitAndLetter = 4, //数字和字母(密码使用)
        chinese = 5,  //汉字
    }
}
