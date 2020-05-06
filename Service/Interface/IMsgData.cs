using MaterialDesignThemes.Wpf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomControl.Service.Interface
{
    public interface IMsgData
    {
        void GetMsgData(string caption, string indicateText, PackIconKind msgIcon);
    }
}
