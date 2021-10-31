using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace ShortcutFloat.WPF.Windows.Data
{
    public class NullablePlaceholderValueConverter<T> : IValueConverter
        where T : struct
    {
        public string NullPlaceholder { get; set; }

        public NullablePlaceholderValueConverter(string nullPlaceholder) =>
            NullPlaceholder = nullPlaceholder;

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            T? val = (T?)value;
            return val != null ? val.Value : NullPlaceholder;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
