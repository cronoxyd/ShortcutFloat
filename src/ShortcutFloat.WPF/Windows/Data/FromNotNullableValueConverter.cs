using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace ShortcutFloat.WPF.Windows.Data
{
    public class FromNotNullableValueConverter<T> : IValueConverter
        where T : struct
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value is not T ? throw new ArgumentException() : value;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null) return 0;
            else return (T)value;
        }
    }

    public class ToNotNullableIntValueConverter : FromNotNullableValueConverter<int> { }
    public class ToNotNullableDoubleValueConverter : FromNotNullableValueConverter<double> { }
}
