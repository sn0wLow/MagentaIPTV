using System;
using System.Globalization;

namespace MagentaIPTV
{
    public class BooleanToMediaIconValueConverter : BaseValueConverter<BooleanToMediaIconValueConverter>
    {
        public override object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return (bool)value == true ? "\uf04c" : "\uf04b";
        }

        public override object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
