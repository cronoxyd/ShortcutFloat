using ShortcutFloat.Common.Models;
using ShortcutFloat.Common.Models.Actions;
using ShortcutFloat.Common.ViewModels.Actions;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Windows.Data;
using System.Windows.Input;

namespace ShortcutFloat.Common.ViewModels
{
    public class ShortcutDefinitionViewModel : TypedViewModel<ShortcutDefinition>
    {
        public string Name { get => Model.Name; set => Model.Name = value; }
        public ObservableCollection<IActionDefinitionViewModel> Actions { get; }
        public ICollectionView ActionsView { get; }
        public IActionDefinitionViewModel SelectedAction { get; set; } = null;

        public ICommand NewActionCommand { get; }
        public ICommand RemoveActionCommand { get; }
        public ICommand SendCommand { get; }
        public ICommand MoveActionUpCommand { get; }
        public ICommand MoveActionDownCommand { get; }

        public event SendKeysEventHandler SendKeysRequested = (sender, e) => { };

        public ShortcutDefinitionViewModel(ShortcutDefinition Model) : base(Model)
        {
            Actions = new(Model.Actions.Select(act => act switch
            {
                KeystrokeDefinition => new KeystrokeDefinitionViewModel(act as KeystrokeDefinition),
                _ => throw new NotImplementedException()
            }).ToList());

            foreach (var vm in Actions)
            {
                vm.PropertyChanged += (sender, e) => UpdateActions();
            }

            ActionsView = CollectionViewSource.GetDefaultView(Actions);

            Actions.CollectionChanged += ShortcutDefinitionViewModel_CollectionChanged;

            SendCommand = new RelayCommand(
                () =>
                {
                    foreach (var action in Actions)
                        SendKeysRequested(this, new(action.GetSendKeysString()));
                },
                () => true
            );

            NewActionCommand = new RelayCommand(
                () => Actions.Add(new KeystrokeDefinitionViewModel(new())),
                () => true
            );

            RemoveActionCommand = new RelayCommand(
                () => Actions.Remove(SelectedAction),
                () => SelectedAction != null
            );

            MoveActionDownCommand = new RelayCommand(
                () =>
                {
                    var selectedIndex = Actions.IndexOf(SelectedAction);
                    Actions.Move(selectedIndex, selectedIndex + 1);
                },
                () =>
                {
                    if (SelectedAction == null) return false;
                    return Actions.IndexOf(SelectedAction) < Actions.Count - 1;
                }
            );

            MoveActionUpCommand = new RelayCommand(
                () =>
                {
                    var selectedIndex = Actions.IndexOf(SelectedAction);
                    Actions.Move(selectedIndex, selectedIndex - 1);
                },
                () =>
                {
                    if (SelectedAction == null) return false;
                    return Actions.IndexOf(SelectedAction) > 0;
                }
            );
        }

        private void ShortcutDefinitionViewModel_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e) =>
            UpdateActions();

        private void UpdateActions()
        {
            Model.Actions.Clear();
            Model.Actions.AddRange(Actions.Select(act => act.Model));
        }
    }
}
