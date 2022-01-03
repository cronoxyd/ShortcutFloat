using ShortcutFloat.Common.Extensions;
using ShortcutFloat.Common.Helper;
using ShortcutFloat.Common.Models.Actions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Input;

namespace ShortcutFloat.Common.Input
{
    public class InputItem
    {
        public Key[] Keys { get; set; } = Array.Empty<Key>();

        public MouseButton[] MouseButtons { get; set; } = Array.Empty<MouseButton>();

        public string Text { get; set; } = string.Empty;

        public bool HoldAndRelease { get; set; } = false;

        public KeystrokeReleaseTriggerType ReleaseTriggerType { get; set; } =
            KeystrokeReleaseTriggerType.Mouse | KeystrokeReleaseTriggerType.Keyboard;

        /// <summary>
        /// Specifies the maximum amount of time the keystroke will be held
        /// </summary>
        /// <remarks>
        /// If set to <c>0</c>, the keystroke will be held indefinitely.
        /// </remarks>
        public uint HoldTimeLimitSeconds { get; set; } = 0;

        public string GetSendKeysString()
        {
            if (MouseButtons.Length > 0)
                throw new InvalidOperationException($"Cannot create {nameof(SendKeys)} string for {nameof(MouseButton)}.");

            if (Keys.Length > 0 && Text.Length > 0)
                throw new InvalidOperationException($"Cannot create {nameof(SendKeys)} string for both {nameof(Key)}s and {nameof(Text)}.");

            if (Keys.Length > 0)
                return string.Join(string.Empty, Keys.Select(key => key.ToSendKeysString()));

            if (Text.Length > 0)
                return SendKeysHelper.EscapeForSendKeys(Text);

            throw new InvalidOperationException();
        }
    }
}
