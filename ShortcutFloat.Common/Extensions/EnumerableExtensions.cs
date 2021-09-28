using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShortcutFloat.Common.Extensions
{
    public static class EnumerableExtensions
    {
        public static IEnumerable<T> NotNull<T>(this IEnumerable<T> source) =>
            source.Where(itm => itm != null);
        public static IEnumerable<string> NotNullOrEmpty(this IEnumerable<string> source) =>
            source.Where(itm => !string.IsNullOrEmpty(itm));
        public static IEnumerable<string> NotNullOrWhiteSpace(this IEnumerable<string> source) =>
            source.Where(itm => !string.IsNullOrWhiteSpace(itm));
    }
}
