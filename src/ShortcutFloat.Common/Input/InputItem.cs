using ShortcutFloat.Common.Extensions;
using ShortcutFloat.Common.Helper;
using ShortcutFloat.Common.Models.Actions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Input;

namespace ShortcutFloat.Common.Input
{
    public class InputItem
    {
        /// <summary>
        /// Specifies the <see cref="Key"/>s that are operated.
        /// </summary>
        /// <remarks>
        /// Either <see cref="Keys"/> or <see cref="Text"/> is supported. Specifying both will throw a <see cref="NotSupportedException"/>.
        /// </remarks>
        public Key[] Keys { get; set; } = Array.Empty<Key>();

        /// <summary>
        /// Specifies the <see cref="MouseButton"/>s that are operated.
        /// </summary>
        public MouseButton[] MouseButtons { get; set; } = Array.Empty<MouseButton>();

        /// <summary>
        /// Specifies the text that is sent.
        /// </summary>
        /// <remarks>
        /// <inheritdoc cref="Keys"/>
        /// </remarks>
        public string Text { get; set; } = string.Empty;

        /// <summary>
        /// Specifies whether the <see cref="Key"/>s and <see cref="MouseButton"/>s are held until a second event occurs.
        /// </summary>
        /// <remarks>
        /// The event to release the held <see cref="Key"/>s and <see cref="MouseButton"/>s is set using <see cref="ReleaseTriggerType"/>.
        /// </remarks>
        public bool HoldAndRelease { get; set; } = false;

        /// <summary>
        /// Specifies the type(s) of environment events that cause the held <see cref="Key"/>s and <see cref="MouseButton"/>s to be released.
        /// </summary>
        /// <remarks>
        /// This only applies if <see cref="HoldAndRelease"/> is set to <see langword="true"/>.
        /// </remarks>
        public KeystrokeReleaseTriggerType ReleaseTriggerType { get; set; } =
            KeystrokeReleaseTriggerType.Mouse | KeystrokeReleaseTriggerType.Keyboard;

        /// <summary>
        /// Specifies the maximum amount of time the keystroke will be held
        /// </summary>
        /// <remarks>
        /// If set to <c>0</c>, the keystroke will be held indefinitely.
        /// </remarks>
        public int? HoldTimeLimitSeconds { get; set; } = null;

        /// <summary>
        /// Converts the specified <see cref="Key"/>s or <see cref="Text"/> to a <see cref="string"/> that follows the <see cref="SendKeys"/> notation.
        /// </summary>
        /// <returns>The string to be used in <see cref="SendKeys.Send(string)"/> or <see cref="SendKeys.SendWait(string)"/>.</returns>
        /// <exception cref="InvalidOperationException">If <see cref="MouseButtons"/> is not empty or if both <see cref="Keys"/> and <see cref="Text"/> contain values.</exception>
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

            return string.Empty;
        }

        public override string ToString()
        {
            return JsonSerializer.Serialize(this);
        }
    }
}
