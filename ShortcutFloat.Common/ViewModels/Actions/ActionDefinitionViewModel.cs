using ShortcutFloat.Common.Models.Actions;
using System.Windows.Forms;
using System.Windows.Input;

namespace ShortcutFloat.Common.ViewModels.Actions
{
    public abstract class ActionDefinitionViewModel : TypedViewModel<ActionDefinition>, IActionDefinitionViewModel
    {

        public string Name { get => Model.Name; set => Model.Name = value; }

        public string ToSendKeysString() => Model.ToSendKeysString();

        public ICommand SendCommand { get; }

        public ActionDefinitionViewModel(ActionDefinition Model) : base(Model)
        {
            SendCommand = new RelayCommand(
                () => SendKeys.Send(ToSendKeysString()),
                () => true
            );
        }
    }

    public interface IActionDefinitionViewModel : ITypedViewModel<ActionDefinition>
    {
        public string Name { get; set; }
        public string ToSendKeysString();
        public ICommand SendCommand { get; }
    }
}
