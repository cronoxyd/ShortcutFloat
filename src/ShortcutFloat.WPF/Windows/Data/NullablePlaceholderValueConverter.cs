using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace ShortcutFloat.WPF.Windows.Data
{
    public abstract class NullablePlaceholderValueConverter<T> : IMultiValueConverter
        where T : struct
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            if (values == null) throw new ArgumentNullException(nameof(values));

            T? val = (T?)values[0];
            return val != null ? val.Value.ToString() : (values[1]?.ToString() ?? string.Empty);
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture) =>
            value switch
            {
                T => new object[] { (T)value },
                string => new object[] { FromString(value.ToString()) },
                null => null,
                _ => throw new ArgumentException(),
            };

        protected abstract T? FromString(string value);
    }

    public class NullableInt32PlaceholderValueConverter : NullablePlaceholderValueConverter<Int32>
    {
        protected override int? FromString(string value) =>
            Int32.TryParse(value, out Int32 parsedValue) ? parsedValue : null;
    }

    public class NullableDoublePlaceholderValueConverter : NullablePlaceholderValueConverter<double>
    {
        protected override double? FromString(string value) =>
            double.TryParse(value, out double parsedValue) ? parsedValue : null;
    }
}
