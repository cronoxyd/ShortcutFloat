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
        public ICommand DeleteConfigurationCommand { get; }

        public event ShortcutConfigurationViewModelHandler NewConfigurationRequested = (sender, e) => { };
        public event ShortcutConfigurationViewModelHandler EditConfigurationRequested = (sender, e) => { };

        public ShortcutFloatSettingsViewModel(ShortcutFloatSettings Model) : base(Model)
        {
            ShortcutConfigurations = new(this.Model.ShortcutConfigurations.Select(cfg => new ShortcutConfigurationViewModel(cfg)));
            ShortcutConfigurationsView = CollectionViewSource.GetDefaultView(ShortcutConfigurations);

            ShortcutConfigurations.CollectionChanged += ShortcutConfigurations_CollectionChanged;

            DefaultConfiguration = new(this.Model.DefaultConfiguration);

            NewConfigurationCommand = new RelayCommand(
                () =>
                {
                    var e = new ShortcutConfigurationViewModelEventArgs();
                    NewConfigurationRequested(this, e);
                    if (e.ViewModel != null) ShortcutConfigurations.Add(e.ViewModel);
                },
                () => true
            );

            EditConfigurationCommand = new RelayCommand(
                () =>
                {
                    var e = new ShortcutConfigurationViewModelEventArgs(SelectedConfiguration);
                    EditConfigurationRequested(this, e);
                    if (e.ViewModel != null)
                    {
                        ShortcutConfigurations.Remove(SelectedConfiguration);
                        ShortcutConfigurations.Add(e.ViewModel);
                        SelectedConfiguration = e.ViewModel;
                    }
                },
                () => SelectedConfiguration != null
            );

            DeleteConfigurationCommand = new RelayCommand(
                () => ShortcutConfigurations.Remove(SelectedConfiguration),
                () => SelectedConfiguration != null
            );
        }

        private void ShortcutConfigurations_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            Model.ShortcutConfigurations.Clear();
            Model.ShortcutConfigurations.AddRange(ShortcutConfigurations.Select(cfg => cfg.Model));
        }

        public delegate void ShortcutConfigurationViewModelHandler(object sender, ShortcutConfigurationViewModelEventArgs e);

        public class ShortcutConfigurationViewModelEventArgs : EventArgs
        {
            public ShortcutConfigurationViewModel ViewModel { get; set; } = null;

            public ShortcutConfigurationViewModelEventArgs(ShortcutConfigurationViewModel vm) => ViewModel = vm;
            public ShortcutConfigurationViewModelEventArgs(ShortcutConfiguration m) => ViewModel = new(m);
            public ShortcutConfigurationViewModelEventArgs() { }
        }
    }
}
