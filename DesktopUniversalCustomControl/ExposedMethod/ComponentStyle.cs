using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace DesktopUniversalCustomControl.ExposedMethod
{
    /// <summary>
    /// 控件样式
    /// </summary>
    public class ComponentStyle
    {
        public ComponentStyle()
        {
            
        }

        /// <summary>
        /// 获取组件样式
        /// </summary>
        /// <param name="keyName">键值</param>
        /// <returns></returns>
        public static Style GetComponentStyle(string keyName)
        {
            //AppDomainManager appDomainManager = new AppDomainManager();
            ResourceDictionary resourceDictionary = new ResourceDictionary();
            resourceDictionary.Source = new Uri("/DesktopUniversalCustomControl;component/Resource/Dictionary/ControlDictionary.xaml", UriKind.Relative);
            var contentStyle = resourceDictionary[keyName] as Style;
            return contentStyle;
        }
    }
}
