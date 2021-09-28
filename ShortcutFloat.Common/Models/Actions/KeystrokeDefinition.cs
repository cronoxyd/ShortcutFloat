using ShortcutFloat.Common.Extensions;
using System.Windows.Input;

namespace ShortcutFloat.Common.Models.Actions
{
    public class KeystrokeDefinition : ActionDefinition
    {
        public ModifierKeys? ModifierKeys { get; set; } = null;
        public Key? Key { get; set; } = null;

        public KeystrokeDefinition() { }

        public KeystrokeDefinition(string Name, Key? Key)
        {
            this.Name = Name;
            this.Key = Key;
        }

        public KeystrokeDefinition(string Name, ModifierKeys? ModifierKeys, Key? Key)
        {
            this.Name = Name;
            this.ModifierKeys = ModifierKeys;
            this.Key = Key;
        }

        public override string ToSendKeysString() =>
            string.Join(
                string.Empty,
                (new string[] { ModifierKeys.ToSendKeysString(), Key.ToSendKeysString() }).NotNullOrEmpty()
            );
    }
}
