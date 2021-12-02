using System;

namespace ShortcutFloat.Common.Runtime.Interop.Input
{
    [Flags]
    public enum KeyEventFlag : uint
    {
        KEYEVENTF_NONE = 0x0000,

        /// <summary>
        /// If specified, the scan code was preceded by a prefix byte having the value 0xE0 (224).
        /// </summary>
        KEYEVENTF_EXTENDEDKEY = 0x0001,

        /// <summary>
        /// If specified, the key is being released. If not specified, the key is being depressed.
        /// </summary>
        KEYEVENTF_KEYUP = 0x0002
    }
}
