using ShortcutFloat.Common.Models;
using ShortcutFloat.Common.Models.Actions;
using ShortcutFloat.Common.ViewModels.Actions;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;

namespace ShortcutFloat.Common.ViewModels
{
    public class ShortcutDefinitionViewModel : TypedViewModel<ShortcutDefinition>
    {
        public string Name { get => Model.Name; set => Model.Name = value; }
        public ObservableCollection<IActionDefinitionViewModel> Actions { get; }

        public ICommand SendCommand { get; }

        public ShortcutDefinitionViewModel(ShortcutDefinition Model) : base(Model)
        {
            Actions = new(Model.Actions.Select(act => new ActionDefinitionViewModel(act)));
            Actions.CollectionChanged += ShortcutDefinitionViewModel_CollectionChanged;

            SendCommand = new RelayCommand(
                () =>
                {
                    foreach (var action in Actions)
                        action.SendCommand.Execute(null);
                },
                () => true
            );
        }

        private void ShortcutDefinitionViewModel_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            Model.Actions.Clear();
            Model.Actions.AddRange(Actions.Select(act => act.Model));
        }
    }
}
