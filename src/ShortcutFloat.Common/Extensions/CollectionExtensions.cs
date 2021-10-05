using ShortcutFloat.Common.ViewModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShortcutFloat.Common.Extensions
{
    public static class CollectionExtensions
    {
        public static void Add<TViewModel, TModel>(this ICollection<TViewModel> target, TModel model)
            where TViewModel : TypedViewModel<TModel>
            where TModel : class, new()
        {
            target.Add((TViewModel)Activator.CreateInstance(typeof(TViewModel), model));
        }

        public static void AddRange<TViewModel, TModel>(this ICollection<TViewModel> target, IEnumerable<TModel> items)
            where TViewModel : TypedViewModel<TModel>
            where TModel : class, new()
        {
            target.AddRange(items.Select(model => (TViewModel)Activator.CreateInstance(typeof(TViewModel), model)));
        }

        public static void AddRange<T>(this ICollection<T> target, IEnumerable<T> items)
        {
            foreach (var itm in items)
                target.Add(itm);
        }

        public static void Replace<T>(this IList<T> source, T oldItem, T newItem)
        {
            var originalIndex = source.IndexOf(oldItem);
            if (originalIndex < 0)
                throw new ArgumentException($"{nameof(source)} does not contain {nameof(oldItem)}");

            source.Remove(oldItem);
            source.Insert(originalIndex, newItem);
        }
    }
}
