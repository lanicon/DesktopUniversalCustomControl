using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
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
    /// 输入法选择控件
    /// </summary>
    public class InputMethodView : Control
    {
        static InputMethodView()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(InputMethodView), new FrameworkPropertyMetadata(typeof(InputMethodView)));
        }

        Dictionary<string, CultureInfo> keyValues = new Dictionary<string, CultureInfo>();
        CustomPopupEx pop;
        ListView lv;
        string language = "[中文]";
        public InputMethodView()
        {     
            this.Loaded += InputMethodView_Loaded;
        }

        private void InputMethodView_Loaded(object sender, RoutedEventArgs e)
        {           
            pop = this.Template.FindName("cpe", this) as CustomPopupEx;
            lv = this.Template.FindName("lv", this) as ListView;
            var btn = this.Template.FindName("languageExange", this) as Button;

            lv.SelectionChanged += Lv_SelectionChanged;
            btn.Click += Btn_Click;

            lv.ItemsSource = GetAllInstalledInputMethods();
        }

        //输入法切换
        private void Lv_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (keyValues.Values.ToArray()[lv.SelectedIndex].LCID == 2052)
            {
                InputLanguageManager.Current.CurrentInputLanguage = keyValues.Values.ToArray()[lv.SelectedIndex];
                ExposedMethod.TSFWrapper.ActiveInputMethodWithDesc(2052, lv.SelectedItem.ToString());
            }
            else
                InputLanguageManager.Current.CurrentInputLanguage = keyValues.Values.ToArray()[lv.SelectedIndex];

            ChineseEnglishMethod();
            pop.IsOpen = false;
        }

        //中英文切换
        private void Btn_Click(object sender, RoutedEventArgs e)
        {
            if (language == "[英文]")
                language = "[中文]";
            else
                language = "[英文]";

            ChineseEnglishMethod();

            var state = System.Windows.Input.InputMethod.Current;
            if (state.ImeState == InputMethodState.Off)
                state.ImeState = InputMethodState.On;
            else
                state.ImeState = InputMethodState.Off;
        }

        private void ChineseEnglishMethod()
        {
            if (keyValues.Values.ToArray()[lv.SelectedIndex].LCID == 2052)
            {
                LanguageExangeButton = Visibility.Visible;
                CurrentVisibleInputMethod = lv.SelectedItem.ToString() + language;
            }
            else
            {
                LanguageExangeButton = Visibility.Hidden;
                CurrentVisibleInputMethod = lv.SelectedItem.ToString();
            }
        }

        //获取所有安装的语言输入法
        private List<string> GetAllInstalledInputMethods()
        {
            IEnumerable collections = InputLanguageManager.Current.AvailableInputLanguages;
            foreach (CultureInfo item in collections)
            {
                if (!keyValues.ContainsKey(item.DisplayName) && !item.DisplayName.Contains("中文"))
                    keyValues.Add(item.DisplayName, item);
            }

            var langIDs = ExposedMethod.TSFWrapper.GetLangIDs();
            foreach (var id in langIDs)
            {
                if (id.Equals(2052))
                {
                    var chineses = ExposedMethod.TSFWrapper.GetInputMethodList(2052);
                    foreach (var l in chineses)
                    {
                        keyValues.Add(l, CultureInfo.GetCultureInfo(2052));
                    }
                }
            }

            return keyValues.Keys.ToList<string>();
        }



        public static readonly DependencyProperty CurrentVisibleInputMethodProperty =
            DependencyProperty.Register("CurrentVisibleInputMethod", typeof(string), typeof(InputMethodView), new PropertyMetadata(default(string)));
        public static readonly DependencyProperty LanguageExangeButtonProperty =
            DependencyProperty.Register("LanguageExangeButton", typeof(Visibility), typeof(InputMethodView), new PropertyMetadata(default(Visibility)));

        /// <summary>
        /// 当前显示的输入法
        /// </summary>
        public string CurrentVisibleInputMethod
        {
            get { return (string)GetValue(CurrentVisibleInputMethodProperty); }
            set { SetValue(CurrentVisibleInputMethodProperty, value); }
        }

        /// <summary>
        /// 中英文切换按钮
        /// </summary>
        public Visibility LanguageExangeButton
        {
            get { return (Visibility)GetValue(LanguageExangeButtonProperty); }
            set { SetValue(LanguageExangeButtonProperty, value); }
        }

    }
}
