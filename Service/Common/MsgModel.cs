using CustomControl.CustomComponent;
using CustomControl.NotifycationObject;
using CustomControl.Service.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomControl.Service.Common
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
