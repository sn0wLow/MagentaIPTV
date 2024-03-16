using System;
using System.Globalization;


namespace MagentaIPTV
{
    public class BooleanToMuteIconValueConverter : BaseValueConverter<BooleanToMuteIconValueConverter>
    {
        public override object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return (bool)value == true ? "\uf6a9" : "\uf026";
        }

        public override object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
