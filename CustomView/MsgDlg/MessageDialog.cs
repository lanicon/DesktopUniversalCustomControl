using CustomControl.CustomComponent;
using CustomControl.NotifycationObject;
using CustomControl.Service.Common;
using Prism.Commands;
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

namespace CustomControl.CustomView.MsgDlg
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


        /// <summary>
        /// 显示一条窗口信息，并返回结果
        /// </summary>
        /// <param name="indicateText"></param>
        /// <returns></returns>
        public static MessageBoxResult Show(string indicateText)
        {
            MsgViewModel.msg_IndicateText = indicateText;

            return OpenMsgWindow();
        }

        /// <summary>
        /// 显示一条窗口信息，并返回结果
        /// </summary>
        /// <returns></returns>
        public static MessageBoxResult Show(string caption, string indicateText, IconType msgIcon)
        {
            MsgViewModel.msg_Caption = caption;
            MsgViewModel.msg_IndicateText = indicateText;
            MsgViewModel.msg_Icon = msgIcon;

            return OpenMsgWindow();
        }

        /// <summary>
        /// 打开MessageDialog窗口
        /// </summary>
        /// <returns></returns>
        private static MessageBoxResult OpenMsgWindow()
        {
            MessageWindow msgWin = new MessageWindow();
            msgWin.ShowDialog();
            return MsgViewModel.msg_Result;
        }
    }
}
