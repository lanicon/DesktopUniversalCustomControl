using DesktopUniversalCustomControl.CustomComponent;
using DesktopUniversalCustomControl.NotifycationObject;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Threading;

namespace DesktopUniversalCustomControl.CustomView.MsgDlg
{
    /// <summary>
    /// MessageDialogView
    /// </summary>
    public class MessageDialog : Control
    {
        private static Brush fore;
        private static Brush back;

        static MessageDialog()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(MessageDialog), new FrameworkPropertyMetadata(typeof(MessageDialog)));
            InitMessageDialogTheme(Brushes.White, Brushes.BlueViolet);
        }

        /// <summary>
        /// 初始化弹出框主题颜色
        /// </summary>
        /// <param name="_fore"></param>
        /// <param name="_back"></param>
        public static void InitMessageDialogTheme(Brush _fore, Brush _back)
        {
            fore = _fore;
            back = _back;
        }

        public MessageButtonCount MessageButtonResult
        {
            get { return (MessageButtonCount)GetValue(MessageButtonResultProperty); }
            set { SetValue(MessageButtonResultProperty, value); }
        }
        public static readonly DependencyProperty MessageButtonResultProperty =
            DependencyProperty.Register("MessageButtonResult", typeof(MessageButtonCount), typeof(MessageDialog), new PropertyMetadata(MessageButtonCount.Single));

        [Bindable(true)]
        public Brush AdornerForeground
        {
            get { return (Brush)GetValue(AdornerForegroundProperty); }
            set { SetValue(AdornerForegroundProperty, value); }
        }
        public static readonly DependencyProperty AdornerForegroundProperty =
            DependencyProperty.Register("AdornerForeground", typeof(Brush), typeof(MessageDialog), new PropertyMetadata());

        [Bindable(true)]
        public Brush AdornerBackground
        {
            get { return (Brush)GetValue(AdornerBackgroundProperty); }
            set { SetValue(AdornerBackgroundProperty, value); }
        }
        public static readonly DependencyProperty AdornerBackgroundProperty =
            DependencyProperty.Register("AdornerBackground", typeof(Brush), typeof(MessageDialog), new PropertyMetadata());



        /// <summary>
        /// 显示一条窗口信息，并返回结果
        /// </summary>
        /// <param name="indicateText"></param>
        /// <returns></returns>
        public static MessageBoxResult Show(string indicateText, MessageButtonCount messageButtonCount = MessageButtonCount.Single)
        {
            MsgViewModel.msg_IndicateText = indicateText;

            return Application.Current.Dispatcher.Invoke(() => OpenMsgWindow(messageButtonCount));
        }

        /// <summary>
        /// 显示一条窗口信息，并返回结果
        /// </summary>
        /// <returns></returns>
        public static MessageBoxResult Show(string caption, string indicateText, IconType msgIcon, MessageButtonCount messageButtonCount = MessageButtonCount.Single)
        {
            MsgViewModel.msg_Caption = caption;
            MsgViewModel.msg_IndicateText = indicateText;
            MsgViewModel.msg_Icon = msgIcon;

            return Application.Current.Dispatcher.Invoke(() => OpenMsgWindow(messageButtonCount));
        }

        /// <summary>
        /// 打开MessageDialog窗口
        /// </summary>
        /// <returns></returns>
        private static MessageBoxResult OpenMsgWindow(MessageButtonCount messageButtonCount)
        {
            MessageWindow msgWin = new MessageWindow();
            StateChanged(msgWin, messageButtonCount);

            msgWin.ShowDialog();
            return MsgViewModel.msg_Result;
        }

        private static void StateChanged(MessageWindow msgWin, MessageButtonCount messageButtonCount)
        {
            var localMsg = msgWin.FindName("customMsg") as MessageDialog;
            localMsg.AdornerForeground = fore;
            localMsg.AdornerBackground = back;
            localMsg.MessageButtonResult = messageButtonCount; //更改button的个数
        }
    }

    public enum MessageButtonCount
    {
        Single,
        Two,
        More,
    }
}
