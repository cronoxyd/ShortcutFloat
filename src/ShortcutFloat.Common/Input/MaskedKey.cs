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
        Alphanumeric = 1,

        /// <summary>
        /// The Control, Shift, Alt and AltGr keys.
        /// </summary>
        Modifier = 2,

        /// <summary>
        /// The Arrow, Page, Home, End, Tab, Insert, Delete, Backspace keys.
        /// </summary>
        Cursor = 3,

        /// <summary>
        /// The Break, Escape, Enter and Menu keys.
        /// </summary>
        System = 4,

        /// <summary>
        /// The keys of the numeric keypad.
        /// </summary>
        NumPad = 5,

        /// <summary>
        /// The Play/Pause, Previous, Next, Stop and Volume keys.
        /// </summary>
        Media = 6,

        /// <summary>
        /// The Function keys.
        /// </summary>
        Function = 7,

        /// <summary>
        /// The Ime-specific keys.
        /// </summary>
        Ime = 8,

        /// <summary>
        /// The Oem keys.
        /// </summary>
        Oem = 9,

        /// <summary>
        /// The Browser navigation keys.
        /// </summary>
        Browser = 10
    }
}
