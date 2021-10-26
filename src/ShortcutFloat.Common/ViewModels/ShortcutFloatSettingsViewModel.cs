using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Windows.Data;
using System.Windows.Input;
using ShortcutFloat.Common.Extensions;
using ShortcutFloat.Common.Models;

namespace ShortcutFloat.Common.ViewModels
{
    public class ShortcutFloatSettingsViewModel : TypedViewModel<ShortcutFloatSettings>
    {
        public bool UseDefaultConfiguration { get => Model.UseDefaultConfiguration; set => Model.UseDefaultConfiguration = value; }
        public bool StickyFloatWindow { get => Model.StickyFloatWindow; set => Model.StickyFloatWindow = value; }
        public ShortcutConfigurationViewModel DefaultConfiguration { get; }
        public ObservableCollection<ShortcutConfigurationViewModel> ShortcutConfigurations { get; }
        public ICollectionView ShortcutConfigurationsView { get; }
        public ShortcutConfigurationViewModel SelectedConfiguration { get; set; } = null;
        public FloatWindowPositionReference FloatWindowPositionReference { get => Model.FloatWindowPositionReference; set => Model.FloatWindowPositionReference = value; }
        public int FloatWindowGridColumns { get => Model.FloatWindowGridColumns; set => Model.FloatWindowGridColumns = value; }
        public int FloatWindowGridRows { get => Model.FloatWindowGridRows; set => Model.FloatWindowGridRows = value; }

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
            DefaultConfiguration.PropertyChanged += DefaultConfiguration_PropertyChanged;

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
                        var vm = new ShortcutConfigurationViewModel(e.Model);
                        ShortcutConfigurations.Replace(SelectedConfiguration, vm);
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

        private void DefaultConfiguration_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            switch (e.PropertyName)
            {
                case nameof(ShortcutConfigurationViewModel.Model):
                    {
                        Model.DefaultConfiguration = DefaultConfiguration.Model;
                        break;
                    }
            }
        }

        private void ShortcutConfigurations_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            Model.ShortcutConfigurations.Clear();
            Model.ShortcutConfigurations.AddRange(ShortcutConfigurations.Select(cfg => cfg.Model));
        }

        public delegate void ShortcutConfigurationModelHandler(object sender, ModelEventArgs<ShortcutConfiguration> e);
    }
}
