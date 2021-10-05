using ShortcutFloat.Common.Extensions;
using System.Windows.Input;

namespace ShortcutFloat.Common.Models.Actions
{
    public class KeystrokeDefinition : ActionDefinition
    {
        public ModifierKeys? ModifierKey { get; set; } = null;
        public Key? Key { get; set; } = null;

        public KeystrokeDefinition() { }

        public KeystrokeDefinition(string Name, Key? Key)
        {
            this.Name = Name;
            this.Key = Key;
        }

        public KeystrokeDefinition(string Name, ModifierKeys? ModifierKey, Key? Key)
        {
            this.Name = Name;
            this.ModifierKey = ModifierKey;
            this.Key = Key;
        }

        public override string GetSendKeysString() =>
            string.Join(
                string.Empty,
                (new string[] { ModifierKey.ToSendKeysString(), Key.ToSendKeysString() }).NotNullOrEmpty()
            );
    }
}
