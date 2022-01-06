using ShortcutFloat.Common.Models.Actions;

namespace ShortcutFloat.Common.ViewModels.Actions
{
    public abstract class ActionDefinitionViewModel : TypedViewModel<ActionDefinition>, IActionDefinitionViewModel
    {

        public string Name { get => Model.Name; set => Model.Name = value; }

        public ActionDefinitionViewModel(ActionDefinition Model) : base(Model) { }
    }

    public interface IActionDefinitionViewModel : ITypedViewModel<ActionDefinition>, IActionDefinition
    { }
}
