using System;
using System.Globalization;
using System.Windows.Data;

namespace ShortcutFloat.WPF.Windows.Data
{
    /// <summary>
    /// A <see cref="IValueConverter"/> that does not convert and simply returns the value as is.
    /// </summary>
    public class NOPValueConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value;
        }
    }
}
