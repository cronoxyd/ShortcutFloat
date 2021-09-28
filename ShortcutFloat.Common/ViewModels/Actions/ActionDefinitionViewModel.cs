using ShortcutFloat.Common.Models.Actions;
using System.Windows.Forms;
using System.Windows.Input;

namespace ShortcutFloat.Common.ViewModels.Actions
{
    public class ActionDefinitionViewModel : ViewModel, IActionDefinitionViewModel
    {
        public IActionDefinition Model { get; set; }

        public string Name { get => Model.Name; set => Model.Name = value; }

        public string ToSendKeysString() => Model.ToSendKeysString();

        public ICommand SendCommand { get; }

        public ActionDefinitionViewModel(IActionDefinition Model) : base()
        {
            this.Model = Model;

            SendCommand = new RelayCommand(
                () => SendKeys.Send(ToSendKeysString()),
                () => true
            );
        }
    }

    public interface IActionDefinitionViewModel : IViewModel
    {
        public IActionDefinition Model { get; set; }
        public string Name { get; set; }
        public string ToSendKeysString();
        public ICommand SendCommand { get; }
    }
}
