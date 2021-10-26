using ShortcutFloat.Common.Models.Actions;
using System.Collections.Generic;
using System.Linq;

namespace ShortcutFloat.Common.Models.Triggers
{
    public class ShortcutDefinition : TriggerDefinition
    {
        public List<IActionDefinition> Actions { get; } = new();

        public string[] ToSendKeysStrings() =>
            Actions.Select(def => def.GetSendKeysString()).ToArray();

        public ShortcutDefinition() { }

        public ShortcutDefinition(string Name, IActionDefinition Action)
        {
            this.Name = Name;
            Actions.Add(Action);
        }

        public ShortcutDefinition(string Name, IActionDefinition[] Actions)
        {
            this.Name = Name;
            this.Actions.AddRange(Actions);
        }
    }
}
