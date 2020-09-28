using DesktopUniversalCustomControl.CustomComponent;
using DesktopUniversalCustomControl.NotifycationObject;
using DesktopUniversalCustomControl.Service.Interface;

namespace DesktopUniversalCustomControl.Service.Common
{
    public class MsgModel : IMsgData
    {
        MsgViewModel msgViewModel;
        public MsgModel()
        {
            msgViewModel = new MsgViewModel();
        }

        public void GetMsgData(string caption, string indicateText, IconType msgIcon)
        {
            msgViewModel.caption = caption;
            msgViewModel.indicateText = indicateText;
            msgViewModel.msgIcon = msgIcon;
        }
    }
}
