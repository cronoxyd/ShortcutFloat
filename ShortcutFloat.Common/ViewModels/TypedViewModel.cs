namespace ShortcutFloat.Common.ViewModels
{
    public class TypedViewModel<T> : ViewModel, ITypedViewModel<T> where T : class, new()
    {
        public T Model { get; set; }

        public TypedViewModel(T Model) : base()
        {
            if (Model == null)
                this.Model = new();
            else
                this.Model = Model;
        }
    }
    public interface ITypedViewModel<T> : IViewModel where T : class
    {
        public T Model { get; set; }
    }
}
