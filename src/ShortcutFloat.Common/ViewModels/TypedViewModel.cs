using System.Diagnostics.CodeAnalysis;

namespace ShortcutFloat.Common.ViewModels
{
    public class TypedViewModel<T> : ViewModel, ITypedViewModel<T> where T : class
    {
        public T Model { get; set; }

        public TypedViewModel([NotNull] T Model) : base()
        {
            this.Model = Model;
        }
    }
    public interface ITypedViewModel<T> : IViewModel where T : class
    {
        public T Model { get; set; }
    }
}
