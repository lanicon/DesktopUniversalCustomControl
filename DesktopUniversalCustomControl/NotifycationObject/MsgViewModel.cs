using DesktopUniversalCustomControl.CustomComponent;
using DesktopUniversalCustomControl.CustomView.MsgDlg;
using Prism.Commands;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace DesktopUniversalCustomControl.NotifycationObject
{
    public class MsgViewModel : NotifyPropertyChanged
    {
        #region Command

        public DelegateCommand<Window> MoveWindowCommand
        {
            get
            {
                return new DelegateCommand<Window>(win =>
                {
                    if (win != null)
                    {
                        win.MouseMove += (sender, e) =>
                        {
                            var point = e.GetPosition(win);
                            if (e.LeftButton == MouseButtonState.Pressed && point.Y <= 50)
                            {
                                win.DragMove();
                            }
                            e.Handled = true;
                        };
                    }
                });
            }
        }

        private DelegateCommand<Window> _loadedWindowCommand;
        public DelegateCommand<Window> LoadedWindowCommand
        {
            get { return _loadedWindowCommand; }
            set { _loadedWindowCommand = value; }
        }


        public DelegateCommand<Window> CloseWindowCommand
        {
            get
            {
                return new DelegateCommand<Window>((win) =>
                {
                    win.Close();
                });
            }
        }

        private DelegateCommand<Window> _yesCommand;
        /// <summary>
        /// 是
        /// </summary>
        public DelegateCommand<Window> YesCommand
        {
            get { return _yesCommand; }
            set { _yesCommand = value; }
        }

        private DelegateCommand<Window> _noCommand;
        /// <summary>
        /// 否
        /// </summary>
        public DelegateCommand<Window> NoCommand
        {
            get { return _noCommand; }
            set { _noCommand = value; }
        }

        private DelegateCommand<Window> _cancelCommand;
        /// <summary>
        /// 取消
        /// </summary>
        public DelegateCommand<Window> CancelCommand
        {
            get { return _cancelCommand; }
            set { _cancelCommand = value; }
        }

        private DelegateCommand<Window> _sureCommand;
        /// <summary>
        /// 确定
        /// </summary>
        public DelegateCommand<Window> SureCommand
        {
            get { return _sureCommand; }
            set { _sureCommand = value; }
        }

        #endregion

        #region Data

        public static MessageBoxResult msg_Result;
        public static string msg_Caption;
        public static string msg_IndicateText;
        public static IconType msg_Icon;

        private string _caption = msg_Caption;
        /// <summary>
        /// 标题
        /// </summary>
        public string caption
        {
            get { return _caption; }
            set { SetProperty(ref _caption, value); }
        }

        private string _indicateText = msg_IndicateText;
        /// <summary>
        /// 消息文本
        /// </summary>
        public string indicateText
        {
            get { return _indicateText; }
            set { SetProperty(ref _indicateText, value); }
        }

        private IconType _msgIcon = msg_Icon;
        /// <summary>
        /// 消息图标
        /// </summary>
        public IconType msgIcon
        {
            get { return _msgIcon; }
            set { SetProperty(ref _msgIcon, value); }
        }


        #endregion


        public MsgViewModel()
        {
            LoadedWindowCommand = new DelegateCommand<Window>(LoadedWindow);
            YesCommand = new DelegateCommand<Window>(Yes);
            NoCommand = new DelegateCommand<Window>(No);
            CancelCommand = new DelegateCommand<Window>(Cancel);
            SureCommand = new DelegateCommand<Window>(Sure);
        }

        private void LoadedWindow(Window win)
        {
            var localMsg = win.FindName("customMsg") as MessageDialog;
            var yesBtn = localMsg.Template.FindName("yes", localMsg) as Button;
            yesBtn.Focus();
        }

        /// <summary>
        /// 点击是
        /// </summary>
        /// <param name="win"></param>
        public void Yes(Window win)
        {
            win.Close();
            msg_Result = MessageBoxResult.Yes;
        }

        /// <summary>
        /// 点击否
        /// </summary>
        /// <param name="win"></param>
        private void No(Window win)
        {
            win.Close();
            msg_Result = MessageBoxResult.No;
        }

        /// <summary>
        /// 点击取消
        /// </summary>
        /// <param name="win"></param>
        private void Cancel(Window win)
        {
            win.Close();
            msg_Result = MessageBoxResult.Cancel;
        }

        /// <summary>
        /// 确定
        /// </summary>
        /// <param name="win"></param>
        private void Sure(Window win)
        {
            win.Close();
            msg_Result = MessageBoxResult.OK;
        }
    }
}
