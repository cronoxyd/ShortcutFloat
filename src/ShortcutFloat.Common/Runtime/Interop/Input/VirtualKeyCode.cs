using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace ShortcutFloat.Common.Runtime.Interop.Input
{
    public enum VirtualKeyCode : byte
    {

        /// <summary>
        /// Left mouse button
        /// </summary>
        VK_LBUTTON = 0x01,


        /// <summary>
        /// Right mouse button
        /// </summary>
        VK_RBUTTON = 0x02,


        /// <summary>
        /// Control-break processing
        /// </summary>
        VK_CANCEL = 0x03,


        /// <summary>
        /// Middle mouse button (three-button mouse)
        /// </summary>
        VK_MBUTTON = 0x04,


        /// <summary>
        /// X1 mouse button
        /// </summary>
        VK_XBUTTON1 = 0x05,


        /// <summary>
        /// X2 mouse button
        /// </summary>
        VK_XBUTTON2 = 0x06,


        /// <summary>
        /// BACKSPACE key
        /// </summary>
        VK_BACK = 0x08,


        /// <summary>
        /// TAB key
        /// </summary>
        VK_TAB = 0x09,


        /// <summary>
        /// CLEAR key
        /// </summary>
        VK_CLEAR = 0x0C,


        /// <summary>
        /// ENTER key
        /// </summary>
        VK_RETURN = 0x0D,


        /// <summary>
        /// SHIFT key
        /// </summary>
        VK_SHIFT = 0x10,


        /// <summary>
        /// CTRL key
        /// </summary>
        VK_CONTROL = 0x11,


        /// <summary>
        /// ALT key
        /// </summary>
        VK_MENU = 0x12,


        /// <summary>
        /// PAUSE key
        /// </summary>
        VK_PAUSE = 0x13,


        /// <summary>
        /// CAPS LOCK key
        /// </summary>
        VK_CAPITAL = 0x14,


        /// <summary>
        /// IME Kana mode
        /// </summary>
        VK_KANA = 0x15,


        /// <summary>
        /// IME Hanguel mode (maintained for compatibility; use **VK_HANGUL**)
        /// </summary>
        VK_HANGUEL = 0x15,


        /// <summary>
        /// IME Hangul mode
        /// </summary>
        VK_HANGUL = 0x15,


        /// <summary>
        /// IME On
        /// </summary>
        VK_IME_ON = 0x16,


        /// <summary>
        /// IME Junja mode
        /// </summary>
        VK_JUNJA = 0x17,


        /// <summary>
        /// IME final mode
        /// </summary>
        VK_FINAL = 0x18,


        /// <summary>
        /// IME Hanja mode
        /// </summary>
        VK_HANJA = 0x19,


        /// <summary>
        /// IME Kanji mode
        /// </summary>
        VK_KANJI = 0x19,


        /// <summary>
        /// IME Off
        /// </summary>
        VK_IME_OFF = 0x1A,


        /// <summary>
        /// ESC key
        /// </summary>
        VK_ESCAPE = 0x1B,


        /// <summary>
        /// IME convert
        /// </summary>
        VK_CONVERT = 0x1C,


        /// <summary>
        /// IME nonconvert
        /// </summary>
        VK_NONCONVERT = 0x1D,


        /// <summary>
        /// IME accept
        /// </summary>
        VK_ACCEPT = 0x1E,


        /// <summary>
        /// IME mode change request
        /// </summary>
        VK_MODECHANGE = 0x1F,


        /// <summary>
        /// SPACEBAR
        /// </summary>
        VK_SPACE = 0x20,


        /// <summary>
        /// PAGE UP key
        /// </summary>
        VK_PRIOR = 0x21,


        /// <summary>
        /// PAGE DOWN key
        /// </summary>
        VK_NEXT = 0x22,


        /// <summary>
        /// END key
        /// </summary>
        VK_END = 0x23,


        /// <summary>
        /// HOME key
        /// </summary>
        VK_HOME = 0x24,


        /// <summary>
        /// LEFT ARROW key
        /// </summary>
        VK_LEFT = 0x25,


        /// <summary>
        /// UP ARROW key
        /// </summary>
        VK_UP = 0x26,


        /// <summary>
        /// RIGHT ARROW key
        /// </summary>
        VK_RIGHT = 0x27,


        /// <summary>
        /// DOWN ARROW key
        /// </summary>
        VK_DOWN = 0x28,


        /// <summary>
        /// SELECT key
        /// </summary>
        VK_SELECT = 0x29,


        /// <summary>
        /// PRINT key
        /// </summary>
        VK_PRINT = 0x2A,


        /// <summary>
        /// EXECUTE key
        /// </summary>
        VK_EXECUTE = 0x2B,


        /// <summary>
        /// PRINT SCREEN key
        /// </summary>
        VK_SNAPSHOT = 0x2C,


        /// <summary>
        /// INS key
        /// </summary>
        VK_INSERT = 0x2D,


        /// <summary>
        /// DEL key
        /// </summary>
        VK_DELETE = 0x2E,


        /// <summary>
        /// HELP key
        /// </summary>
        VK_HELP = 0x2F,


        /// <summary>
        /// 0 key
        /// </summary>
        VK_0 = 0x30,


        /// <summary>
        /// 1 key
        /// </summary>
        VK_1 = 0x31,


        /// <summary>
        /// 2 key
        /// </summary>
        VK_2 = 0x32,


        /// <summary>
        /// 3 key
        /// </summary>
        VK_3 = 0x33,


        /// <summary>
        /// 4 key
        /// </summary>
        VK_4 = 0x34,


        /// <summary>
        /// 5 key
        /// </summary>
        VK_5 = 0x35,


        /// <summary>
        /// 6 key
        /// </summary>
        VK_6 = 0x36,


        /// <summary>
        /// 7 key
        /// </summary>
        VK_7 = 0x37,


        /// <summary>
        /// 8 key
        /// </summary>
        VK_8 = 0x38,


        /// <summary>
        /// 9 key
        /// </summary>
        VK_9 = 0x39,


        /// <summary>
        /// A key
        /// </summary>
        VK_A = 0x41,


        /// <summary>
        /// B key
        /// </summary>
        VK_B = 0x42,


        /// <summary>
        /// C key
        /// </summary>
        VK_C = 0x43,


        /// <summary>
        /// D key
        /// </summary>
        VK_D = 0x44,


        /// <summary>
        /// E key
        /// </summary>
        VK_E = 0x45,


        /// <summary>
        /// F key
        /// </summary>
        VK_F = 0x46,


        /// <summary>
        /// G key
        /// </summary>
        VK_G = 0x47,


        /// <summary>
        /// H key
        /// </summary>
        VK_H = 0x48,


        /// <summary>
        /// I key
        /// </summary>
        VK_I = 0x49,


        /// <summary>
        /// J key
        /// </summary>
        VK_J = 0x4A,


        /// <summary>
        /// K key
        /// </summary>
        VK_K = 0x4B,


        /// <summary>
        /// L key
        /// </summary>
        VK_L = 0x4C,


        /// <summary>
        /// M key
        /// </summary>
        VK_M = 0x4D,


        /// <summary>
        /// N key
        /// </summary>
        VK_N = 0x4E,


        /// <summary>
        /// O key
        /// </summary>
        VK_O = 0x4F,


        /// <summary>
        /// P key
        /// </summary>
        VK_P = 0x50,


        /// <summary>
        /// Q key
        /// </summary>
        VK_Q = 0x51,


        /// <summary>
        /// R key
        /// </summary>
        VK_R = 0x52,


        /// <summary>
        /// S key
        /// </summary>
        VK_S = 0x53,


        /// <summary>
        /// T key
        /// </summary>
        VK_T = 0x54,


        /// <summary>
        /// U key
        /// </summary>
        VK_U = 0x55,


        /// <summary>
        /// V key
        /// </summary>
        VK_V = 0x56,


        /// <summary>
        /// W key
        /// </summary>
        VK_W = 0x57,


        /// <summary>
        /// X key
        /// </summary>
        VK_X = 0x58,


        /// <summary>
        /// Y key
        /// </summary>
        VK_Y = 0x59,


        /// <summary>
        /// Z key
        /// </summary>
        VK_Z = 0x5A,


        /// <summary>
        /// Left Windows key (Natural keyboard)
        /// </summary>
        VK_LWIN = 0x5B,


        /// <summary>
        /// Right Windows key (Natural keyboard)
        /// </summary>
        VK_RWIN = 0x5C,


        /// <summary>
        /// Applications key (Natural keyboard)
        /// </summary>
        VK_APPS = 0x5D,


        /// <summary>
        /// Computer Sleep key
        /// </summary>
        VK_SLEEP = 0x5F,


        /// <summary>
        /// Numeric keypad 0 key
        /// </summary>
        VK_NUMPAD0 = 0x60,


        /// <summary>
        /// Numeric keypad 1 key
        /// </summary>
        VK_NUMPAD1 = 0x61,


        /// <summary>
        /// Numeric keypad 2 key
        /// </summary>
        VK_NUMPAD2 = 0x62,


        /// <summary>
        /// Numeric keypad 3 key
        /// </summary>
        VK_NUMPAD3 = 0x63,


        /// <summary>
        /// Numeric keypad 4 key
        /// </summary>
        VK_NUMPAD4 = 0x64,


        /// <summary>
        /// Numeric keypad 5 key
        /// </summary>
        VK_NUMPAD5 = 0x65,


        /// <summary>
        /// Numeric keypad 6 key
        /// </summary>
        VK_NUMPAD6 = 0x66,


        /// <summary>
        /// Numeric keypad 7 key
        /// </summary>
        VK_NUMPAD7 = 0x67,


        /// <summary>
        /// Numeric keypad 8 key
        /// </summary>
        VK_NUMPAD8 = 0x68,


        /// <summary>
        /// Numeric keypad 9 key
        /// </summary>
        VK_NUMPAD9 = 0x69,


        /// <summary>
        /// Multiply key
        /// </summary>
        VK_MULTIPLY = 0x6A,


        /// <summary>
        /// Add key
        /// </summary>
        VK_ADD = 0x6B,


        /// <summary>
        /// Separator key
        /// </summary>
        VK_SEPARATOR = 0x6C,


        /// <summary>
        /// Subtract key
        /// </summary>
        VK_SUBTRACT = 0x6D,


        /// <summary>
        /// Decimal key
        /// </summary>
        VK_DECIMAL = 0x6E,


        /// <summary>
        /// Divide key
        /// </summary>
        VK_DIVIDE = 0x6F,


        /// <summary>
        /// F1 key
        /// </summary>
        VK_F1 = 0x70,


        /// <summary>
        /// F2 key
        /// </summary>
        VK_F2 = 0x71,


        /// <summary>
        /// F3 key
        /// </summary>
        VK_F3 = 0x72,


        /// <summary>
        /// F4 key
        /// </summary>
        VK_F4 = 0x73,


        /// <summary>
        /// F5 key
        /// </summary>
        VK_F5 = 0x74,


        /// <summary>
        /// F6 key
        /// </summary>
        VK_F6 = 0x75,


        /// <summary>
        /// F7 key
        /// </summary>
        VK_F7 = 0x76,


        /// <summary>
        /// F8 key
        /// </summary>
        VK_F8 = 0x77,


        /// <summary>
        /// F9 key
        /// </summary>
        VK_F9 = 0x78,


        /// <summary>
        /// F10 key
        /// </summary>
        VK_F10 = 0x79,


        /// <summary>
        /// F11 key
        /// </summary>
        VK_F11 = 0x7A,


        /// <summary>
        /// F12 key
        /// </summary>
        VK_F12 = 0x7B,


        /// <summary>
        /// F13 key
        /// </summary>
        VK_F13 = 0x7C,


        /// <summary>
        /// F14 key
        /// </summary>
        VK_F14 = 0x7D,


        /// <summary>
        /// F15 key
        /// </summary>
        VK_F15 = 0x7E,


        /// <summary>
        /// F16 key
        /// </summary>
        VK_F16 = 0x7F,


        /// <summary>
        /// F17 key
        /// </summary>
        VK_F17 = 0x80,


        /// <summary>
        /// F18 key
        /// </summary>
        VK_F18 = 0x81,


        /// <summary>
        /// F19 key
        /// </summary>
        VK_F19 = 0x82,


        /// <summary>
        /// F20 key
        /// </summary>
        VK_F20 = 0x83,


        /// <summary>
        /// F21 key
        /// </summary>
        VK_F21 = 0x84,


        /// <summary>
        /// F22 key
        /// </summary>
        VK_F22 = 0x85,


        /// <summary>
        /// F23 key
        /// </summary>
        VK_F23 = 0x86,


        /// <summary>
        /// F24 key
        /// </summary>
        VK_F24 = 0x87,


        /// <summary>
        /// NUM LOCK key
        /// </summary>
        VK_NUMLOCK = 0x90,


        /// <summary>
        /// SCROLL LOCK key
        /// </summary>
        VK_SCROLL = 0x91,


        /// <summary>
        /// Left SHIFT key
        /// </summary>
        VK_LSHIFT = 0xA0,


        /// <summary>
        /// Right SHIFT key
        /// </summary>
        VK_RSHIFT = 0xA1,


        /// <summary>
        /// Left CONTROL key
        /// </summary>
        VK_LCONTROL = 0xA2,


        /// <summary>
        /// Right CONTROL key
        /// </summary>
        VK_RCONTROL = 0xA3,


        /// <summary>
        /// Left MENU key
        /// </summary>
        VK_LMENU = 0xA4,


        /// <summary>
        /// Right MENU key
        /// </summary>
        VK_RMENU = 0xA5,


        /// <summary>
        /// Browser Back key
        /// </summary>
        VK_BROWSER_BACK = 0xA6,


        /// <summary>
        /// Browser Forward key
        /// </summary>
        VK_BROWSER_FORWARD = 0xA7,


        /// <summary>
        /// Browser Refresh key
        /// </summary>
        VK_BROWSER_REFRESH = 0xA8,


        /// <summary>
        /// Browser Stop key
        /// </summary>
        VK_BROWSER_STOP = 0xA9,


        /// <summary>
        /// Browser Search key
        /// </summary>
        VK_BROWSER_SEARCH = 0xAA,


        /// <summary>
        /// Browser Favorites key
        /// </summary>
        VK_BROWSER_FAVORITES = 0xAB,


        /// <summary>
        /// Browser Start and Home key
        /// </summary>
        VK_BROWSER_HOME = 0xAC,


        /// <summary>
        /// Volume Mute key
        /// </summary>
        VK_VOLUME_MUTE = 0xAD,


        /// <summary>
        /// Volume Down key
        /// </summary>
        VK_VOLUME_DOWN = 0xAE,


        /// <summary>
        /// Volume Up key
        /// </summary>
        VK_VOLUME_UP = 0xAF,


        /// <summary>
        /// Next Track key
        /// </summary>
        VK_MEDIA_NEXT_TRACK = 0xB0,


        /// <summary>
        /// Previous Track key
        /// </summary>
        VK_MEDIA_PREV_TRACK = 0xB1,


        /// <summary>
        /// Stop Media key
        /// </summary>
        VK_MEDIA_STOP = 0xB2,


        /// <summary>
        /// Play/Pause Media key
        /// </summary>
        VK_MEDIA_PLAY_PAUSE = 0xB3,


        /// <summary>
        /// Start Mail key
        /// </summary>
        VK_LAUNCH_MAIL = 0xB4,


        /// <summary>
        /// Select Media key
        /// </summary>
        VK_LAUNCH_MEDIA_SELECT = 0xB5,


        /// <summary>
        /// Start Application 1 key
        /// </summary>
        VK_LAUNCH_APP1 = 0xB6,


        /// <summary>
        /// Start Application 2 key
        /// </summary>
        VK_LAUNCH_APP2 = 0xB7,


        /// <summary>
        /// Used for miscellaneous characters; it can vary by keyboard. For the US standard keyboard, the ';:' key
        /// </summary>
        VK_OEM_1 = 0xBA,


        /// <summary>
        /// For any country/region, the '+' key
        /// </summary>
        VK_OEM_PLUS = 0xBB,


        /// <summary>
        /// For any country/region, the ',' key
        /// </summary>
        VK_OEM_COMMA = 0xBC,


        /// <summary>
        /// For any country/region, the '-' key
        /// </summary>
        VK_OEM_MINUS = 0xBD,


        /// <summary>
        /// For any country/region, the '.' key
        /// </summary>
        VK_OEM_PERIOD = 0xBE,


        /// <summary>
        /// Used for miscellaneous characters; it can vary by keyboard. For the US standard keyboard, the '/?' key
        /// </summary>
        VK_OEM_2 = 0xBF,


        /// <summary>
        /// Used for miscellaneous characters; it can vary by keyboard.  For the US standard keyboard, the '`~' key
        /// </summary>
        VK_OEM_3 = 0xC0,


        /// <summary>
        /// Used for miscellaneous characters; it can vary by keyboard.  For the US standard keyboard, the '[{' key
        /// </summary>
        VK_OEM_4 = 0xDB,


        /// <summary>
        /// Used for miscellaneous characters; it can vary by keyboard.  For the US standard keyboard, the '|' key
        /// </summary>
        VK_OEM_5 = 0xDC,


        /// <summary>
        /// Used for miscellaneous characters; it can vary by keyboard.  For the US standard keyboard, the ']}' key
        /// </summary>
        VK_OEM_6 = 0xDD,


        /// <summary>
        /// Used for miscellaneous characters; it can vary by keyboard.  For the US standard keyboard, the 'single-quote/double-quote' key
        /// </summary>
        VK_OEM_7 = 0xDE,


        /// <summary>
        /// Used for miscellaneous characters; it can vary by keyboard.
        /// </summary>
        VK_OEM_8 = 0xDF,


        /// <summary>
        /// Either the angle bracket key or the backslash key on the RT 102-key keyboard
        /// </summary>
        VK_OEM_102 = 0xE2,


        /// <summary>
        /// IME PROCESS key
        /// </summary>
        VK_PROCESSKEY = 0xE5,


        /// <summary>
        /// Used to pass Unicode characters as if they were keystrokes. The VK_PACKET key is the low word of a 32-bit Virtual Key value used for non-keyboard input methods. For more information, see Remark in [**KEYBDINPUT**](/windows/win32/api/winuser/ns-winuser-keybdinput), [**SendInput**](/windows/win32/api/winuser/nf-winuser-sendinput), [**WM_KEYDOWN**](wm-keydown.md), and [**WM_KEYUP**](wm-keyup.md)
        /// </summary>
        VK_PACKET = 0xE7,


        /// <summary>
        /// Attn key
        /// </summary>
        VK_ATTN = 0xF6,


        /// <summary>
        /// CrSel key
        /// </summary>
        VK_CRSEL = 0xF7,


        /// <summary>
        /// ExSel key
        /// </summary>
        VK_EXSEL = 0xF8,


        /// <summary>
        /// Erase EOF key
        /// </summary>
        VK_EREOF = 0xF9,


        /// <summary>
        /// Play key
        /// </summary>
        VK_PLAY = 0xFA,


        /// <summary>
        /// Zoom key
        /// </summary>
        VK_ZOOM = 0xFB,


        /// <summary>
        /// Reserved
        /// </summary>
        VK_NONAME = 0xFC,


        /// <summary>
        /// PA1 key
        /// </summary>
        VK_PA1 = 0xFD,


        /// <summary>
        /// Clear key
        /// </summary>
        VK_OEM_CLEAR = 0xFE,
    }


    public static class VirtualKeyCodeExtensions
    {
        /// <summary>
        /// Converts the specified <paramref name="vkCode"/> to a <see cref="MouseButton"/> enum.
        /// </summary>
        /// <param name="vkCode">The virtual key code to convert.</param>
        /// <returns>The value of the <paramref name="vkCode"/> in the <see cref="MouseButton"/> enum or <see langword="null"/> if the specified <paramref name="vkCode"/> does not have an equivalent in the <see cref="MouseButton"/> enum.</returns>
        public static MouseButton? ToMouseButton(this VirtualKeyCode vkCode) => vkCode switch
        {
            VirtualKeyCode.VK_LBUTTON => MouseButton.Left,
            VirtualKeyCode.VK_RBUTTON => MouseButton.Right,
            VirtualKeyCode.VK_MBUTTON => MouseButton.Middle,
            VirtualKeyCode.VK_XBUTTON1 => MouseButton.XButton1,
            VirtualKeyCode.VK_XBUTTON2 => MouseButton.XButton2,
            _ => null
        };

        /// <summary>
        /// Converts the specified <paramref name="vkCode"/> to a <see cref="Key"/> enum.
        /// </summary>
        /// <param name="vkCode">The virtual key code to convert.</param>
        /// <returns>The value of the <paramref name="vkCode"/> in the <see cref="Key"/> enum or <see langword="null"/> if the specified <paramref name="vkCode"/> does not have an equivalent in the <see cref="Key"/> enum.</returns>
        public static Key? ToKey(this VirtualKeyCode vkCode) => vkCode switch
        {
            VirtualKeyCode.VK_LBUTTON => null,
            VirtualKeyCode.VK_RBUTTON => null,
            VirtualKeyCode.VK_CANCEL => Key.Cancel,
            VirtualKeyCode.VK_MBUTTON => null,
            VirtualKeyCode.VK_XBUTTON1 => null,
            VirtualKeyCode.VK_XBUTTON2 => null,
            VirtualKeyCode.VK_BACK => Key.Back,
            VirtualKeyCode.VK_TAB => Key.Tab,
            VirtualKeyCode.VK_CLEAR => Key.Clear,
            VirtualKeyCode.VK_RETURN => Key.Return,
            VirtualKeyCode.VK_SHIFT => Key.LeftShift,
            VirtualKeyCode.VK_CONTROL => Key.LeftCtrl,
            VirtualKeyCode.VK_MENU => Key.LeftAlt,
            VirtualKeyCode.VK_PAUSE => Key.Pause,
            VirtualKeyCode.VK_CAPITAL => Key.CapsLock,
            VirtualKeyCode.VK_KANA => Key.KanaMode,
            VirtualKeyCode.VK_IME_ON => null,
            VirtualKeyCode.VK_JUNJA => Key.JunjaMode,
            VirtualKeyCode.VK_FINAL => Key.FinalMode,
            VirtualKeyCode.VK_HANJA => Key.HanjaMode,
            VirtualKeyCode.VK_IME_OFF => null,
            VirtualKeyCode.VK_ESCAPE => Key.Escape,
            VirtualKeyCode.VK_CONVERT => Key.ImeConvert,
            VirtualKeyCode.VK_NONCONVERT => Key.ImeNonConvert,
            VirtualKeyCode.VK_ACCEPT => Key.ImeAccept,
            VirtualKeyCode.VK_MODECHANGE => Key.ImeModeChange,
            VirtualKeyCode.VK_SPACE => Key.Space,
            VirtualKeyCode.VK_PRIOR => Key.Prior,
            VirtualKeyCode.VK_NEXT => Key.Next,
            VirtualKeyCode.VK_END => Key.End,
            VirtualKeyCode.VK_HOME => Key.Home,
            VirtualKeyCode.VK_LEFT => Key.Left,
            VirtualKeyCode.VK_UP => Key.Up,
            VirtualKeyCode.VK_RIGHT => Key.Right,
            VirtualKeyCode.VK_DOWN => Key.Down,
            VirtualKeyCode.VK_SELECT => Key.Select,
            VirtualKeyCode.VK_PRINT => Key.Print,
            VirtualKeyCode.VK_EXECUTE => Key.Execute,
            VirtualKeyCode.VK_SNAPSHOT => Key.Snapshot,
            VirtualKeyCode.VK_INSERT => Key.Insert,
            VirtualKeyCode.VK_DELETE => Key.Delete,
            VirtualKeyCode.VK_HELP => Key.Help,
            VirtualKeyCode.VK_0 => Key.D0,
            VirtualKeyCode.VK_1 => Key.D1,
            VirtualKeyCode.VK_2 => Key.D2,
            VirtualKeyCode.VK_3 => Key.D3,
            VirtualKeyCode.VK_4 => Key.D4,
            VirtualKeyCode.VK_5 => Key.D5,
            VirtualKeyCode.VK_6 => Key.D6,
            VirtualKeyCode.VK_7 => Key.D7,
            VirtualKeyCode.VK_8 => Key.D8,
            VirtualKeyCode.VK_9 => Key.D9,
            VirtualKeyCode.VK_A => Key.A,
            VirtualKeyCode.VK_B => Key.B,
            VirtualKeyCode.VK_C => Key.C,
            VirtualKeyCode.VK_D => Key.D,
            VirtualKeyCode.VK_E => Key.E,
            VirtualKeyCode.VK_F => Key.F,
            VirtualKeyCode.VK_G => Key.G,
            VirtualKeyCode.VK_H => Key.H,
            VirtualKeyCode.VK_I => Key.I,
            VirtualKeyCode.VK_J => Key.J,
            VirtualKeyCode.VK_K => Key.K,
            VirtualKeyCode.VK_L => Key.L,
            VirtualKeyCode.VK_M => Key.M,
            VirtualKeyCode.VK_N => Key.N,
            VirtualKeyCode.VK_O => Key.O,
            VirtualKeyCode.VK_P => Key.P,
            VirtualKeyCode.VK_Q => Key.Q,
            VirtualKeyCode.VK_R => Key.R,
            VirtualKeyCode.VK_S => Key.S,
            VirtualKeyCode.VK_T => Key.T,
            VirtualKeyCode.VK_U => Key.U,
            VirtualKeyCode.VK_V => Key.V,
            VirtualKeyCode.VK_W => Key.W,
            VirtualKeyCode.VK_X => Key.X,
            VirtualKeyCode.VK_Y => Key.Y,
            VirtualKeyCode.VK_Z => Key.Z,
            VirtualKeyCode.VK_LWIN => Key.LWin,
            VirtualKeyCode.VK_RWIN => Key.RWin,
            VirtualKeyCode.VK_APPS => Key.Apps,
            VirtualKeyCode.VK_SLEEP => Key.Sleep,
            VirtualKeyCode.VK_NUMPAD0 => Key.NumPad0,
            VirtualKeyCode.VK_NUMPAD1 => Key.NumPad1,
            VirtualKeyCode.VK_NUMPAD2 => Key.NumPad2,
            VirtualKeyCode.VK_NUMPAD3 => Key.NumPad3,
            VirtualKeyCode.VK_NUMPAD4 => Key.NumPad4,
            VirtualKeyCode.VK_NUMPAD5 => Key.NumPad5,
            VirtualKeyCode.VK_NUMPAD6 => Key.NumPad6,
            VirtualKeyCode.VK_NUMPAD7 => Key.NumPad7,
            VirtualKeyCode.VK_NUMPAD8 => Key.NumPad8,
            VirtualKeyCode.VK_NUMPAD9 => Key.NumPad9,
            VirtualKeyCode.VK_MULTIPLY => Key.Multiply,
            VirtualKeyCode.VK_ADD => Key.Add,
            VirtualKeyCode.VK_SEPARATOR => Key.Separator,
            VirtualKeyCode.VK_SUBTRACT => Key.Subtract,
            VirtualKeyCode.VK_DECIMAL => Key.Decimal,
            VirtualKeyCode.VK_DIVIDE => Key.Divide,
            VirtualKeyCode.VK_F1 => Key.F1,
            VirtualKeyCode.VK_F2 => Key.F2,
            VirtualKeyCode.VK_F3 => Key.F3,
            VirtualKeyCode.VK_F4 => Key.F4,
            VirtualKeyCode.VK_F5 => Key.F5,
            VirtualKeyCode.VK_F6 => Key.F6,
            VirtualKeyCode.VK_F7 => Key.F7,
            VirtualKeyCode.VK_F8 => Key.F8,
            VirtualKeyCode.VK_F9 => Key.F9,
            VirtualKeyCode.VK_F10 => Key.F10,
            VirtualKeyCode.VK_F11 => Key.F11,
            VirtualKeyCode.VK_F12 => Key.F12,
            VirtualKeyCode.VK_F13 => Key.F13,
            VirtualKeyCode.VK_F14 => Key.F14,
            VirtualKeyCode.VK_F15 => Key.F15,
            VirtualKeyCode.VK_F16 => Key.F16,
            VirtualKeyCode.VK_F17 => Key.F17,
            VirtualKeyCode.VK_F18 => Key.F18,
            VirtualKeyCode.VK_F19 => Key.F19,
            VirtualKeyCode.VK_F20 => Key.F20,
            VirtualKeyCode.VK_F21 => Key.F21,
            VirtualKeyCode.VK_F22 => Key.F22,
            VirtualKeyCode.VK_F23 => Key.F23,
            VirtualKeyCode.VK_F24 => Key.F24,
            VirtualKeyCode.VK_NUMLOCK => Key.NumLock,
            VirtualKeyCode.VK_SCROLL => Key.Scroll,
            VirtualKeyCode.VK_LSHIFT => Key.LeftShift,
            VirtualKeyCode.VK_RSHIFT => Key.RightShift,
            VirtualKeyCode.VK_LCONTROL => Key.LeftCtrl,
            VirtualKeyCode.VK_RCONTROL => Key.RightCtrl,
            VirtualKeyCode.VK_LMENU => null,
            VirtualKeyCode.VK_RMENU => null,
            VirtualKeyCode.VK_BROWSER_BACK => Key.BrowserBack,
            VirtualKeyCode.VK_BROWSER_FORWARD => Key.BrowserForward,
            VirtualKeyCode.VK_BROWSER_REFRESH => Key.BrowserRefresh,
            VirtualKeyCode.VK_BROWSER_STOP => Key.BrowserStop,
            VirtualKeyCode.VK_BROWSER_SEARCH => Key.BrowserSearch,
            VirtualKeyCode.VK_BROWSER_FAVORITES => Key.BrowserFavorites,
            VirtualKeyCode.VK_BROWSER_HOME => Key.BrowserHome,
            VirtualKeyCode.VK_VOLUME_MUTE => Key.VolumeMute,
            VirtualKeyCode.VK_VOLUME_DOWN => Key.VolumeDown,
            VirtualKeyCode.VK_VOLUME_UP => Key.VolumeUp,
            VirtualKeyCode.VK_MEDIA_NEXT_TRACK => Key.MediaNextTrack,
            VirtualKeyCode.VK_MEDIA_PREV_TRACK => Key.MediaPreviousTrack,
            VirtualKeyCode.VK_MEDIA_STOP => Key.MediaStop,
            VirtualKeyCode.VK_MEDIA_PLAY_PAUSE => Key.MediaPlayPause,
            VirtualKeyCode.VK_LAUNCH_MAIL => Key.LaunchMail,
            VirtualKeyCode.VK_LAUNCH_MEDIA_SELECT => Key.SelectMedia,
            VirtualKeyCode.VK_LAUNCH_APP1 => Key.LaunchApplication1,
            VirtualKeyCode.VK_LAUNCH_APP2 => Key.LaunchApplication2,
            VirtualKeyCode.VK_OEM_1 => Key.Oem1,
            VirtualKeyCode.VK_OEM_PLUS => Key.OemPlus,
            VirtualKeyCode.VK_OEM_COMMA => Key.OemComma,
            VirtualKeyCode.VK_OEM_MINUS => Key.OemMinus,
            VirtualKeyCode.VK_OEM_PERIOD => Key.OemPeriod,
            VirtualKeyCode.VK_OEM_2 => Key.Oem2,
            VirtualKeyCode.VK_OEM_3 => Key.Oem3,
            VirtualKeyCode.VK_OEM_4 => Key.Oem4,
            VirtualKeyCode.VK_OEM_5 => Key.Oem5,
            VirtualKeyCode.VK_OEM_6 => Key.Oem6,
            VirtualKeyCode.VK_OEM_7 => Key.Oem7,
            VirtualKeyCode.VK_OEM_8 => Key.Oem8,
            VirtualKeyCode.VK_OEM_102 => Key.Oem102,
            VirtualKeyCode.VK_PROCESSKEY => Key.ImeProcessed,
            VirtualKeyCode.VK_PACKET => null,
            VirtualKeyCode.VK_ATTN => Key.Attn,
            VirtualKeyCode.VK_CRSEL => Key.CrSel,
            VirtualKeyCode.VK_EXSEL => Key.ExSel,
            VirtualKeyCode.VK_EREOF => Key.EraseEof,
            VirtualKeyCode.VK_PLAY => Key.Play,
            VirtualKeyCode.VK_ZOOM => Key.Zoom,
            VirtualKeyCode.VK_NONAME => Key.NoName,
            VirtualKeyCode.VK_PA1 => Key.Pa1,
            VirtualKeyCode.VK_OEM_CLEAR => Key.OemClear,
            _ => null,
        };

        public static VirtualKeyCode? ToVirtualKeyCode(this Key key) => key switch
        {
            Key.Cancel => VirtualKeyCode.VK_CANCEL,
            Key.Back => VirtualKeyCode.VK_BACK,
            Key.Tab => VirtualKeyCode.VK_TAB,
            Key.Clear => VirtualKeyCode.VK_CLEAR,
            Key.Return => VirtualKeyCode.VK_RETURN,
            Key.Pause => VirtualKeyCode.VK_PAUSE,
            Key.CapsLock => VirtualKeyCode.VK_CAPITAL,
            Key.KanaMode => VirtualKeyCode.VK_KANA,
            Key.JunjaMode => VirtualKeyCode.VK_JUNJA,
            Key.FinalMode => VirtualKeyCode.VK_FINAL,
            Key.HanjaMode => VirtualKeyCode.VK_HANJA,
            Key.Escape => VirtualKeyCode.VK_ESCAPE,
            Key.ImeConvert => VirtualKeyCode.VK_CONVERT,
            Key.ImeNonConvert => VirtualKeyCode.VK_NONCONVERT,
            Key.ImeAccept => VirtualKeyCode.VK_ACCEPT,
            Key.ImeModeChange => VirtualKeyCode.VK_MODECHANGE,
            Key.Space => VirtualKeyCode.VK_SPACE,
            Key.Prior => VirtualKeyCode.VK_PRIOR,
            Key.Next => VirtualKeyCode.VK_NEXT,
            Key.End => VirtualKeyCode.VK_END,
            Key.Home => VirtualKeyCode.VK_HOME,
            Key.Left => VirtualKeyCode.VK_LEFT,
            Key.Up => VirtualKeyCode.VK_UP,
            Key.Right => VirtualKeyCode.VK_RIGHT,
            Key.Down => VirtualKeyCode.VK_DOWN,
            Key.Select => VirtualKeyCode.VK_SELECT,
            Key.Print => VirtualKeyCode.VK_PRINT,
            Key.Execute => VirtualKeyCode.VK_EXECUTE,
            Key.Snapshot => VirtualKeyCode.VK_SNAPSHOT,
            Key.Insert => VirtualKeyCode.VK_INSERT,
            Key.Delete => VirtualKeyCode.VK_DELETE,
            Key.Help => VirtualKeyCode.VK_HELP,
            Key.D0 => VirtualKeyCode.VK_0,
            Key.D1 => VirtualKeyCode.VK_1,
            Key.D2 => VirtualKeyCode.VK_2,
            Key.D3 => VirtualKeyCode.VK_3,
            Key.D4 => VirtualKeyCode.VK_4,
            Key.D5 => VirtualKeyCode.VK_5,
            Key.D6 => VirtualKeyCode.VK_6,
            Key.D7 => VirtualKeyCode.VK_7,
            Key.D8 => VirtualKeyCode.VK_8,
            Key.D9 => VirtualKeyCode.VK_9,
            Key.A => VirtualKeyCode.VK_A,
            Key.B => VirtualKeyCode.VK_B,
            Key.C => VirtualKeyCode.VK_C,
            Key.D => VirtualKeyCode.VK_D,
            Key.E => VirtualKeyCode.VK_E,
            Key.F => VirtualKeyCode.VK_F,
            Key.G => VirtualKeyCode.VK_G,
            Key.H => VirtualKeyCode.VK_H,
            Key.I => VirtualKeyCode.VK_I,
            Key.J => VirtualKeyCode.VK_J,
            Key.K => VirtualKeyCode.VK_K,
            Key.L => VirtualKeyCode.VK_L,
            Key.M => VirtualKeyCode.VK_M,
            Key.N => VirtualKeyCode.VK_N,
            Key.O => VirtualKeyCode.VK_O,
            Key.P => VirtualKeyCode.VK_P,
            Key.Q => VirtualKeyCode.VK_Q,
            Key.R => VirtualKeyCode.VK_R,
            Key.S => VirtualKeyCode.VK_S,
            Key.T => VirtualKeyCode.VK_T,
            Key.U => VirtualKeyCode.VK_U,
            Key.V => VirtualKeyCode.VK_V,
            Key.W => VirtualKeyCode.VK_W,
            Key.X => VirtualKeyCode.VK_X,
            Key.Y => VirtualKeyCode.VK_Y,
            Key.Z => VirtualKeyCode.VK_Z,
            Key.LWin => VirtualKeyCode.VK_LWIN,
            Key.RWin => VirtualKeyCode.VK_RWIN,
            Key.Apps => VirtualKeyCode.VK_APPS,
            Key.Sleep => VirtualKeyCode.VK_SLEEP,
            Key.NumPad0 => VirtualKeyCode.VK_NUMPAD0,
            Key.NumPad1 => VirtualKeyCode.VK_NUMPAD1,
            Key.NumPad2 => VirtualKeyCode.VK_NUMPAD2,
            Key.NumPad3 => VirtualKeyCode.VK_NUMPAD3,
            Key.NumPad4 => VirtualKeyCode.VK_NUMPAD4,
            Key.NumPad5 => VirtualKeyCode.VK_NUMPAD5,
            Key.NumPad6 => VirtualKeyCode.VK_NUMPAD6,
            Key.NumPad7 => VirtualKeyCode.VK_NUMPAD7,
            Key.NumPad8 => VirtualKeyCode.VK_NUMPAD8,
            Key.NumPad9 => VirtualKeyCode.VK_NUMPAD9,
            Key.Multiply => VirtualKeyCode.VK_MULTIPLY,
            Key.Add => VirtualKeyCode.VK_ADD,
            Key.Separator => VirtualKeyCode.VK_SEPARATOR,
            Key.Subtract => VirtualKeyCode.VK_SUBTRACT,
            Key.Decimal => VirtualKeyCode.VK_DECIMAL,
            Key.Divide => VirtualKeyCode.VK_DIVIDE,
            Key.F1 => VirtualKeyCode.VK_F1,
            Key.F2 => VirtualKeyCode.VK_F2,
            Key.F3 => VirtualKeyCode.VK_F3,
            Key.F4 => VirtualKeyCode.VK_F4,
            Key.F5 => VirtualKeyCode.VK_F5,
            Key.F6 => VirtualKeyCode.VK_F6,
            Key.F7 => VirtualKeyCode.VK_F7,
            Key.F8 => VirtualKeyCode.VK_F8,
            Key.F9 => VirtualKeyCode.VK_F9,
            Key.F10 => VirtualKeyCode.VK_F10,
            Key.F11 => VirtualKeyCode.VK_F11,
            Key.F12 => VirtualKeyCode.VK_F12,
            Key.F13 => VirtualKeyCode.VK_F13,
            Key.F14 => VirtualKeyCode.VK_F14,
            Key.F15 => VirtualKeyCode.VK_F15,
            Key.F16 => VirtualKeyCode.VK_F16,
            Key.F17 => VirtualKeyCode.VK_F17,
            Key.F18 => VirtualKeyCode.VK_F18,
            Key.F19 => VirtualKeyCode.VK_F19,
            Key.F20 => VirtualKeyCode.VK_F20,
            Key.F21 => VirtualKeyCode.VK_F21,
            Key.F22 => VirtualKeyCode.VK_F22,
            Key.F23 => VirtualKeyCode.VK_F23,
            Key.F24 => VirtualKeyCode.VK_F24,
            Key.NumLock => VirtualKeyCode.VK_NUMLOCK,
            Key.Scroll => VirtualKeyCode.VK_SCROLL,
            Key.LeftShift => VirtualKeyCode.VK_LSHIFT,
            Key.RightShift => VirtualKeyCode.VK_RSHIFT,
            Key.LeftCtrl => VirtualKeyCode.VK_LCONTROL,
            Key.RightCtrl => VirtualKeyCode.VK_RCONTROL,
            Key.BrowserBack => VirtualKeyCode.VK_BROWSER_BACK,
            Key.BrowserForward => VirtualKeyCode.VK_BROWSER_FORWARD,
            Key.BrowserRefresh => VirtualKeyCode.VK_BROWSER_REFRESH,
            Key.BrowserStop => VirtualKeyCode.VK_BROWSER_STOP,
            Key.BrowserSearch => VirtualKeyCode.VK_BROWSER_SEARCH,
            Key.BrowserFavorites => VirtualKeyCode.VK_BROWSER_FAVORITES,
            Key.BrowserHome => VirtualKeyCode.VK_BROWSER_HOME,
            Key.VolumeMute => VirtualKeyCode.VK_VOLUME_MUTE,
            Key.VolumeDown => VirtualKeyCode.VK_VOLUME_DOWN,
            Key.VolumeUp => VirtualKeyCode.VK_VOLUME_UP,
            Key.MediaNextTrack => VirtualKeyCode.VK_MEDIA_NEXT_TRACK,
            Key.MediaPreviousTrack => VirtualKeyCode.VK_MEDIA_PREV_TRACK,
            Key.MediaStop => VirtualKeyCode.VK_MEDIA_STOP,
            Key.MediaPlayPause => VirtualKeyCode.VK_MEDIA_PLAY_PAUSE,
            Key.LaunchMail => VirtualKeyCode.VK_LAUNCH_MAIL,
            Key.SelectMedia => VirtualKeyCode.VK_LAUNCH_MEDIA_SELECT,
            Key.LaunchApplication1 => VirtualKeyCode.VK_LAUNCH_APP1,
            Key.LaunchApplication2 => VirtualKeyCode.VK_LAUNCH_APP2,
            Key.Oem1 => VirtualKeyCode.VK_OEM_1,
            Key.OemPlus => VirtualKeyCode.VK_OEM_PLUS,
            Key.OemComma => VirtualKeyCode.VK_OEM_COMMA,
            Key.OemMinus => VirtualKeyCode.VK_OEM_MINUS,
            Key.OemPeriod => VirtualKeyCode.VK_OEM_PERIOD,
            Key.Oem2 => VirtualKeyCode.VK_OEM_2,
            Key.Oem3 => VirtualKeyCode.VK_OEM_3,
            Key.Oem4 => VirtualKeyCode.VK_OEM_4,
            Key.Oem5 => VirtualKeyCode.VK_OEM_5,
            Key.Oem6 => VirtualKeyCode.VK_OEM_6,
            Key.Oem7 => VirtualKeyCode.VK_OEM_7,
            Key.Oem8 => VirtualKeyCode.VK_OEM_8,
            Key.Oem102 => VirtualKeyCode.VK_OEM_102,
            Key.ImeProcessed => VirtualKeyCode.VK_PROCESSKEY,
            Key.Attn => VirtualKeyCode.VK_ATTN,
            Key.CrSel => VirtualKeyCode.VK_CRSEL,
            Key.ExSel => VirtualKeyCode.VK_EXSEL,
            Key.EraseEof => VirtualKeyCode.VK_EREOF,
            Key.Play => VirtualKeyCode.VK_PLAY,
            Key.Zoom => VirtualKeyCode.VK_ZOOM,
            Key.NoName => VirtualKeyCode.VK_NONAME,
            Key.Pa1 => VirtualKeyCode.VK_PA1,
            Key.OemClear => VirtualKeyCode.VK_OEM_CLEAR,
            Key.LeftAlt => VirtualKeyCode.VK_MENU,
            Key.RightAlt => VirtualKeyCode.VK_MENU,
            _ => null,
        };

        public static VirtualKeyCode? ToVirtualKeyCode(this MouseButton button) => button switch
        {
            MouseButton.Left => VirtualKeyCode.VK_LBUTTON,
            MouseButton.Right => VirtualKeyCode.VK_RBUTTON,
            MouseButton.Middle => VirtualKeyCode.VK_MBUTTON,
            MouseButton.XButton1 => VirtualKeyCode.VK_XBUTTON1,
            MouseButton.XButton2 => VirtualKeyCode.VK_XBUTTON2,
            _ => null
        };
    }
}
