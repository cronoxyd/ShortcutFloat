using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Windows.Data;
using ShortcutFloat.Common.Models;

namespace ShortcutFloat.Common.ViewModels
{
    public class ShortcutFloatSettingsViewModel : TypedViewModel<ShortcutFloatSettings>
    {
        public bool UseDefaultConfiguration { get => Model.UseDefaultConfiguration; set => Model.UseDefaultConfiguration = value; }
        public ShortcutConfigurationViewModel DefaultConfiguration { get; }
        public ObservableCollection<ShortcutConfigurationViewModel> ShortcutConfigurations { get; }
        public ICollectionView ShortcutConfigurationsView { get; }

        public ShortcutFloatSettingsViewModel(ShortcutFloatSettings Model) : base(Model)
        {
            ShortcutConfigurations = new(this.Model.ShortcutConfigurations.Select(cfg => new ShortcutConfigurationViewModel(cfg)));
            ShortcutConfigurationsView = CollectionViewSource.GetDefaultView(ShortcutConfigurations);

            ShortcutConfigurations.CollectionChanged += ShortcutConfigurations_CollectionChanged;

            DefaultConfiguration = new(this.Model.DefaultConfiguration);
        }

        private void ShortcutConfigurations_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            Model.ShortcutConfigurations.Clear();
            Model.ShortcutConfigurations.AddRange(ShortcutConfigurations.Select(cfg => cfg.Model));
        }
    }
}
