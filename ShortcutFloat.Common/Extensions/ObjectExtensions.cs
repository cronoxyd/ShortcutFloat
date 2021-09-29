using System.Text.Json;

namespace ShortcutFloat.Common.Extensions
{
    public static class ObjectExtensions
    {
        public static T DeepClone<T>(this T source)
            where T : class
        {
            return JsonSerializer.Deserialize<T>(JsonSerializer.Serialize(source));
        }
    }
}
