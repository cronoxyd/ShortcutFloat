using ShortcutFloat.Common.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace ShortcutFloat.Common.Extensions
{
    public static class KeyExtensions
    {
        public static string ToSendKeysString(this Key? key)
        {
            if (key == null) return string.Empty;

            return key.Value.ToSendKeysString();
        }

        public static string ToSendKeysString(this IEnumerable<Key> keys) =>
            string.Join(string.Empty, keys.Select((key) => key.ToSendKeysString()).NotNullOrEmpty());

        public static string ToSendKeysString(this Key key) =>
            key switch
            {
                Key.Back => "{BACKSPACE}",
                Key.CapsLock => "{CAPSLOCK}",
                Key.Delete => "{DEL}",
                Key.Down => "{DOWN}",
                Key.End => "{END}",
                Key.Enter => "{ENTER}",
                Key.Escape => "{ESC}",
                Key.Help => "{HELP}",
                Key.Home => "{HOME}",
                Key.Insert => "{INS}",
                Key.Left => "{LEFT}",
                Key.NumLock => "{NUMLOCK}",
                Key.PageDown => "{PGDN}",
                Key.PageUp => "{PGUP}",
                Key.PrintScreen => "{PRTSC}",
                Key.Right => "{RIGHT}",
                Key.Scroll => "{SCROLLLOCK}",
                Key.Tab => "{TAB}",
                Key.Up => "{UP}",

                Key.F1 => "{F1}",
                Key.F2 => "{F2}",
                Key.F3 => "{F3}",
                Key.F4 => "{F4}",
                Key.F5 => "{F5}",
                Key.F6 => "{F6}",
                Key.F7 => "{F7}",
                Key.F8 => "{F8}",
                Key.F9 => "{F9}",
                Key.F10 => "{F10}",
                Key.F11 => "{F11}",
                Key.F12 => "{F12}",
                Key.F13 => "{F13}",
                Key.F14 => "{F14}",
                Key.F15 => "{F15}",
                Key.F16 => "{F16}",

                Key.Add => "{ADD}",
                Key.Subtract => "{SUBTRACT}",
                Key.Multiply => "{MULTIPLY}",
                Key.Divide => "{DIVIDE}",

                Key.LeftShift => "+",
                Key.RightShift => "+",

                Key.LeftCtrl => "^",
                Key.RightCtrl => "^",

                Key.LeftAlt => "%",
                Key.RightAlt => "%",

                _ => SendKeysHelper.EscapeForSendKeys(key.ToString().ToLower()),
            };

        public static string ToSendKeysString(this ModifierKeys? keys)
        {
            if (keys == null) return string.Empty;
            return keys.Value.ToSendKeysString();
        }

        public static string ToSendKeysString(this ModifierKeys keys) =>
            keys.ToKeys().ToSendKeysString();

        public static IEnumerable<Key> ToKeys(this ModifierKeys keys)
        {
            var retVal = new List<Key>();
            if (keys.HasFlag(ModifierKeys.Control)) retVal.Add(Key.LeftCtrl);
            if (keys.HasFlag(ModifierKeys.Shift)) retVal.Add(Key.LeftShift);
            if (keys.HasFlag(ModifierKeys.Alt)) retVal.Add(Key.LeftAlt);
            if (keys.HasFlag(ModifierKeys.Windows)) retVal.Add(Key.LWin);

            return retVal;
        }
    }
}
