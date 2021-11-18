using ShortcutFloat.Common.Extensions;
using System;
using System.Windows.Input;

namespace ShortcutFloat.Common.Models.Actions
{
    public class KeystrokeDefinition : ActionDefinition
    {
        public ModifierKeys ModifierKeys { get; set; } = ModifierKeys.None;
        public Key? Key { get; set; } = null;
        public bool HoldAndRelease { get; set; } = false;

        /// <summary>
        /// Specifies the maximum amount of time the keystroke will be held
        /// </summary>
        /// <remarks>
        /// If set to <c>0</c>, the keystroke will be held indefinitely.
        /// </remarks>
        public uint HoldTimeLimitSeconds { get; set; } = 0;

        /// <summary>
        /// Specifies the type of user interaction that releases the keystroke
        /// </summary>
        public KeystrokeReleaseTriggerType ReleaseTriggerType { get; set; } =
            KeystrokeReleaseTriggerType.Mouse | KeystrokeReleaseTriggerType.Keyboard;

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

        public override string GetSendKeysString() =>
            string.Join(
                string.Empty,
                (new string[] { ModifierKeys.ToSendKeysString(), Key.ToSendKeysString() }).NotNullOrEmpty()
            );
    }

    [Flags]
    public enum KeystrokeReleaseTriggerType
    {
        None,
        Mouse,
        Keyboard
    }
}
