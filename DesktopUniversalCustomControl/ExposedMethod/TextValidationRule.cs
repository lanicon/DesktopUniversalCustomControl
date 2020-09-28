using System;
using System.Globalization;
using System.Windows.Controls;

namespace DesktopUniversalCustomControl.ExposedMethod
{
    public class TextValidationRule : ValidationRule
    {
        private int _min;
        public int min
        {
            get { return _min; }
            set { _min = value; }
        }

        private int _max;
        public int max
        {
            get { return _max; }
            set { _max = value; }
        }


        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            int input = 0;

            try
            {
                if (((string)value).Length > 0)
                    input = int.Parse((string)value);

            }
            catch (Exception ex)
            {
                return new ValidationResult(false, ex.Message);
            }

            if (input < min || input > max)
            {
                //下面的return返回的参数在页面中可以通过TextBox的(ValidationErrors)[0].ErrorContent获取
                return new ValidationResult(false, string.Format("取值范围为{0}～{1}", min, max));
            }
            else
            {
                return new ValidationResult(true, null);
            }
        }
    }
}
