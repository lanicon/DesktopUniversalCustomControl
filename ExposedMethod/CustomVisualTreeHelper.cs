﻿using Org.BouncyCastle.Asn1.X509.Qualified;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Forms;
using System.Windows.Media;

namespace CustomControl.ExposedMethod
{
    public class CustomVisualTreeHelper
    {
        /// <summary>
        /// 查找对象
        /// </summary>
        /// <param name="obj">传入对象</param>
        /// <param name="type">要求满足的类型</param>
        /// <param name="frameworkElement">返回</param>
        public static T FindChildByTree<T>(DependencyObject obj, Type type) where T : FrameworkElement
        {
            for (int i = 0; i < VisualTreeHelper.GetChildrenCount(obj); i++)
            {
                var el = VisualTreeHelper.GetChild(obj, i) as FrameworkElement;
                if(el.GetType() is T)
                {                   
                    return default(T);
                }
                else
                    FindChildByTree<T>(el, type);
                    
            }
            return null;
        }
    }
}