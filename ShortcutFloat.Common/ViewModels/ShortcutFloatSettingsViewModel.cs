using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Windows.Data;
using System.Windows.Input;
using ShortcutFloat.Common.Models;

namespace ShortcutFloat.Common.ViewModels
{
    public class ShortcutFloatSettingsViewModel : TypedViewModel<ShortcutFloatSettings>
    {
        public bool UseDefaultConfiguration { get => Model.UseDefaultConfiguration; set => Model.UseDefaultConfiguration = value; }
        public ShortcutConfigurationViewModel DefaultConfiguration { get; }
        public ObservableCollection<ShortcutConfigurationViewModel> ShortcutConfigurations { get; }
        public ICollectionView ShortcutConfigurationsView { get; }
        public ShortcutConfigurationViewModel SelectedConfiguration { get; set; } = null;

        public ICommand NewConfigurationCommand { get; }
        public ICommand EditConfigurationCommand { get; }
        public ICommand RemoveConfigurationCommand { get; }

        public event ShortcutConfigurationModelHandler NewConfigurationRequested = (sender, e) => { };
        public event ShortcutConfigurationModelHandler EditConfigurationRequested = (sender, e) => { };

        public ShortcutFloatSettingsViewModel(ShortcutFloatSettings Model) : base(Model)
        {
            ShortcutConfigurations = new(this.Model.ShortcutConfigurations.Select(cfg => new ShortcutConfigurationViewModel(cfg)));
            ShortcutConfigurationsView = CollectionViewSource.GetDefaultView(ShortcutConfigurations);

            ShortcutConfigurations.CollectionChanged += ShortcutConfigurations_CollectionChanged;

            DefaultConfiguration = new(this.Model.DefaultConfiguration);

            NewConfigurationCommand = new RelayCommand(
                () =>
                {
                    var e = new ModelEventArgs<ShortcutConfiguration>();
                    NewConfigurationRequested(this, e);
                    if (e.Model != null) ShortcutConfigurations.Add(new(e.Model));
                },
                () => true
            );

            EditConfigurationCommand = new RelayCommand(
                () =>
                {
                    var e = new ModelEventArgs<ShortcutConfiguration>(SelectedConfiguration.Model);
                    EditConfigurationRequested(this, e);
                    if (e.Model != null)
                    {
                        ShortcutConfigurations.Remove(SelectedConfiguration);
                        var vm = new ShortcutConfigurationViewModel(e.Model);
                        ShortcutConfigurations.Add(vm);
                        SelectedConfiguration = vm;
                    }
                },
                () => SelectedConfiguration != null
            );

            RemoveConfigurationCommand = new RelayCommand(
                () => ShortcutConfigurations.Remove(SelectedConfiguration),
                () => SelectedConfiguration != null
            );
        }

        private void ShortcutConfigurations_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            Model.ShortcutConfigurations.Clear();
            Model.ShortcutConfigurations.AddRange(ShortcutConfigurations.Select(cfg => cfg.Model));
        }

        public delegate void ShortcutConfigurationModelHandler(object sender, ModelEventArgs<ShortcutConfiguration> e);
    }
}
