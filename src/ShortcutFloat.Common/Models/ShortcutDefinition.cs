using ShortcutFloat.Common.Models.Actions;
using System.Collections.Generic;
using System.Linq;

namespace ShortcutFloat.Common.Models
{
    public class ShortcutDefinition
    {
        public string Name { get; set; }
        public List<IActionDefinition> Actions { get; } = new();
        public bool HoldAndRelease { get; set; } = false;

        /// <summary>
        /// Specifies the maximum amount of time the keystroke will be held
        /// </summary>
        /// <remarks>
        /// If set to <c>0</c>, the keystroke will be held indefinitely.
        /// </remarks>
        public int? HoldTimeLimitSeconds { get; set; } = null;

        /// <summary>
        /// Specifies the type of user interaction that releases the keystroke
        /// </summary>
        public KeystrokeReleaseTriggerType ReleaseTriggerType { get; set; } =
            KeystrokeReleaseTriggerType.Mouse | KeystrokeReleaseTriggerType.Keyboard;

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
