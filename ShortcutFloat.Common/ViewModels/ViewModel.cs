using PropertyChanged;
using System.ComponentModel;

namespace ShortcutFloat.Common.ViewModels
{
    [AddINotifyPropertyChangedInterface]
    public class ViewModel : IViewModel
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public bool IsModified { get; protected set; } = false;

        public ViewModel()
        {
            PropertyChanged += ViewModel_PropertyChanged;
        }

        private void ViewModel_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            IsModified = true;
        }
    }
    public interface IViewModel : INotifyPropertyChanged
    {
        public bool IsModified { get; }
    }
}
