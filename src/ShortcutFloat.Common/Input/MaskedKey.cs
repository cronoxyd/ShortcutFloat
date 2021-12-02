using System;
using System.Windows.Input;

namespace ShortcutFloat.Common.Input
{
    [Flags]
    public enum MaskedKey
    {
        /// <summary>
        /// No key.
        /// </summary>
        None = 0,

        /// <summary>
        /// Alphabetical, numeric, and punctuation keys, including the space bar.
        /// </summary>
        Alphanumeric,

        /// <summary>
        /// The Control, Shift, Alt and AltGr keys.
        /// </summary>
        Modifier,

        /// <summary>
        /// The Arrow, Page, Home, End, Tab, Insert, Delete, Backspace keys.
        /// </summary>
        Cursor,

        /// <summary>
        /// The Break, Escape, Enter and Menu keys.
        /// </summary>
        System,

        /// <summary>
        /// The keys of the numeric keypad.
        /// </summary>
        NumPad
    }
}
