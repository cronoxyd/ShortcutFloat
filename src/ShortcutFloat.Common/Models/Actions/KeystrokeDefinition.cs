using ShortcutFloat.Common.Extensions;
using ShortcutFloat.Common.Input;
using System;
using System.Collections.Generic;
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
        public int? HoldTimeLimitSeconds { get; set; } = null;

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

        public override InputItem GetInputItem()
        {
            var retVal = new InputItem()
            {
                HoldAndRelease = HoldAndRelease,
                HoldTimeLimitSeconds = HoldTimeLimitSeconds
            };

            var keyList = new List<Key>();
            keyList.AddRange(ModifierKeys.ToKeys());
            if (Key != null)
                keyList.Add(Key.Value);
            retVal.Keys = keyList.ToArray();
            return retVal;
        }
    }
}
