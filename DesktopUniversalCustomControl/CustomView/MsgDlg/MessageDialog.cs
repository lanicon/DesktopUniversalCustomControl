using DesktopUniversalCustomControl.CustomComponent;
using DesktopUniversalCustomControl.NotifycationObject;
using DesktopUniversalCustomControl.Service.Common;
using Prism.Commands;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.NetworkInformation;
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

namespace DesktopUniversalCustomControl.CustomView.MsgDlg
{
    /// <summary>
    /// MessageDialogView
    /// </summary>
    public class MessageDialog : Control
    {
        static MessageDialog()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(MessageDialog), new FrameworkPropertyMetadata(typeof(MessageDialog)));
        }

        public MessageButtonCount MessageButtonResult
        {
            get { return (MessageButtonCount)GetValue(MessageButtonResultProperty); }
            set { SetValue(MessageButtonResultProperty, value); }
        }       
        public static readonly DependencyProperty MessageButtonResultProperty =
            DependencyProperty.Register("MessageButtonResult", typeof(MessageButtonCount), typeof(MessageDialog), new PropertyMetadata(MessageButtonCount.Single));


        /// <summary>
        /// 显示一条窗口信息，并返回结果
        /// </summary>
        /// <param name="indicateText"></param>
        /// <returns></returns>
        public static MessageBoxResult Show(string indicateText, MessageButtonCount messageButtonCount = MessageButtonCount.Single)
        {
            MsgViewModel.msg_IndicateText = indicateText;

            return OpenMsgWindow(messageButtonCount);
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

            return OpenMsgWindow(messageButtonCount);
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
            localMsg.MessageButtonResult = messageButtonCount; //更改button的个数
        }
    }

    public enum MessageButtonCount
    {
        Single,
        More,
    }
}
