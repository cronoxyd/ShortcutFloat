using ShortcutFloat.Common.Extensions;
using ShortcutFloat.Common.Models;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Windows.Data;

namespace ShortcutFloat.Common.ViewModels
{
    public class ShortcutConfigurationViewModel : TypedViewModel<ShortcutConfiguration>
    {
        public ShortcutTarget Target { get => Model.Target; set => Model.Target = value; }
        public ObservableCollection<ShortcutDefinitionViewModel> ShortcutDefinitions { get; } = new();
        public ICollectionView ShortcutDefinitionsView { get; }
        public ShortcutConfigurationViewModel(ShortcutConfiguration Model) : base(Model)
        {
            ShortcutDefinitions.CollectionChanged += ShortcutDefinitions_CollectionChanged;
            ShortcutDefinitions.AddRange(this.Model.ShortcutDefinitions.Select(def => new ShortcutDefinitionViewModel(def)));
            ShortcutDefinitionsView = CollectionViewSource.GetDefaultView(ShortcutDefinitions);
        }

        private void ShortcutDefinitions_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            Model.ShortcutDefinitions.Clear();
            Model.ShortcutDefinitions.AddRange(ShortcutDefinitions.Select(def => def.Model));
        }
    }
}
