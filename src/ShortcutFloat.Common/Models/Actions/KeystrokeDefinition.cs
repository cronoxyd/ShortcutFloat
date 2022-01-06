using ShortcutFloat.Common.Extensions;
using ShortcutFloat.Common.Input;
using System;
using System.Collections.Generic;
using System.Windows.Input;

namespace ShortcutFloat.Common.Models.Actions
{
    public class KeystrokeDefinition : ActionDefinition, IKeystrokeDefinition
    {
        public ModifierKeys ModifierKeys { get; set; } = ModifierKeys.None;
        public Key? Key { get; set; } = null;

        public KeystrokeDefinition() { }

        public KeystrokeDefinition(string Name, Key? Key)
        {
            this.Name = Name;
            this.Key = Key;
        }

        public KeystrokeDefinition(string Name, ModifierKeys ModifierKeys, Key? Key)
        {
            this.Name = Name;
            this.ModifierKeys = ModifierKeys;
            this.Key = Key;
        }
    }

    public interface IKeystrokeDefinition : IActionDefinition
    {
        public ModifierKeys ModifierKeys { get; set; }
        public Key? Key { get; set; }
    }
}
