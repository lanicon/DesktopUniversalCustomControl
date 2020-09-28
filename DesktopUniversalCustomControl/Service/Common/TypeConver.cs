using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Linq;

namespace DesktopUniversalCustomControl.Service.Common
{
    public class StringToListTypeConver : TypeConverter
    {
        public override bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType)
        {
            return true;
        }
        public override object ConvertFrom(ITypeDescriptorContext context, CultureInfo culture, object value)
        {
            List<string> desList = new List<string>();
            string v = value.ToString();
            desList = v.Split(',').ToList();
            return desList;
        }
    }
}
