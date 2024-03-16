using System;
using System.Diagnostics;
using System.Globalization;


namespace MagentaIPTV
{
    /// <summary>
    /// Converters an <see cref="ApplicationPage"/> to an actual Page/UserControl
    /// </summary>
    public class ApplicationPageValueConverter : BaseValueConverter<ApplicationPageValueConverter>
    {
        public override object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            switch ((ApplicationPage)value)
            {
                case ApplicationPage.Overview:
                    return new OverviewPage();
                default:
                    Debugger.Break();
                    throw new ArgumentException("Could not convert the Page");
            }
        }

        public override object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
