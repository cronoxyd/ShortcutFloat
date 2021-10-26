using System;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows.Input;

namespace ShortcutFloat.Common.Runtime
{
    public static class InteropServices
    {
        [DllImport("user32.dll", SetLastError = true)]
        public static extern IntPtr SetActiveWindow(IntPtr hWnd);

        /// <summary>
        ///     Brings the thread that created the specified window into the foreground and activates the window. Keyboard input is
        ///     directed to the window, and various visual cues are changed for the user. The system assigns a slightly higher
        ///     priority to the thread that created the foreground window than it does to other threads.
        ///     <para>See for https://msdn.microsoft.com/en-us/library/windows/desktop/ms633539%28v=vs.85%29.aspx more information.</para>
        /// </summary>
        /// <param name="hWnd">
        ///     C++ ( hWnd [in]. Type: HWND )<br />A handle to the window that should be activated and brought to the foreground.
        /// </param>
        /// <returns>
        ///     <c>true</c> or nonzero if the window was brought to the foreground, <c>false</c> or zero If the window was not
        ///     brought to the foreground.
        /// </returns>
        /// <remarks>
        ///     The system restricts which processes can set the foreground window. A process can set the foreground window only if
        ///     one of the following conditions is true:
        ///     <list type="bullet">
        ///     <listheader>
        ///         <term>Conditions</term><description></description>
        ///     </listheader>
        ///     <item>The process is the foreground process.</item>
        ///     <item>The process was started by the foreground process.</item>
        ///     <item>The process received the last input event.</item>
        ///     <item>There is no foreground process.</item>
        ///     <item>The process is being debugged.</item>
        ///     <item>The foreground process is not a Modern Application or the Start Screen.</item>
        ///     <item>The foreground is not locked (see LockSetForegroundWindow).</item>
        ///     <item>The foreground lock time-out has expired (see SPI_GETFOREGROUNDLOCKTIMEOUT in SystemParametersInfo).</item>
        ///     <item>No menus are active.</item>
        ///     </list>
        ///     <para>
        ///     An application cannot force a window to the foreground while the user is working with another window.
        ///     Instead, Windows flashes the taskbar button of the window to notify the user.
        ///     </para>
        ///     <para>
        ///     A process that can set the foreground window can enable another process to set the foreground window by
        ///     calling the AllowSetForegroundWindow function. The process specified by dwProcessId loses the ability to set
        ///     the foreground window the next time the user generates input, unless the input is directed at that process, or
        ///     the next time a process calls AllowSetForegroundWindow, unless that process is specified.
        ///     </para>
        ///     <para>
        ///     The foreground process can disable calls to SetForegroundWindow by calling the LockSetForegroundWindow
        ///     function.
        ///     </para>
        /// </remarks>
        // For Windows Mobile, replace user32.dll with coredll.dll
        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool SetForegroundWindow(IntPtr hWnd);

        /// <summary>
        ///     Retrieves a handle to the foreground window (the window with which the user is currently working). The system
        ///     assigns a slightly higher priority to the thread that creates the foreground window than it does to other threads.
        ///     <para>See https://msdn.microsoft.com/en-us/library/windows/desktop/ms633505%28v=vs.85%29.aspx for more information.</para>
        /// </summary>
        /// <returns>
        ///     C++ ( Type: Type: HWND )<br /> The return value is a handle to the foreground window. The foreground window
        ///     can be NULL in certain circumstances, such as when a window is losing activation.
        /// </returns>
        [DllImport("user32.dll")]
        public static extern IntPtr GetForegroundWindow();

        /// <summary>
        ///     Copies the text of the specified window's title bar (if it has one) into a buffer. If the specified window is a
        ///     control, the text of the control is copied. However, GetWindowText cannot retrieve the text of a control in another
        ///     application.
        ///     <para>
        ///     Go to https://msdn.microsoft.com/en-us/library/windows/desktop/ms633520%28v=vs.85%29.aspx  for more
        ///     information
        ///     </para>
        /// </summary>
        /// <param name="hWnd">
        ///     C++ ( hWnd [in]. Type: HWND )<br />A <see cref="IntPtr" /> handle to the window or control containing the text.
        /// </param>
        /// <param name="lpString">
        ///     C++ ( lpString [out]. Type: LPTSTR )<br />The <see cref="StringBuilder" /> buffer that will receive the text. If
        ///     the string is as long or longer than the buffer, the string is truncated and terminated with a null character.
        /// </param>
        /// <param name="nMaxCount">
        ///     C++ ( nMaxCount [in]. Type: int )<br /> Should be equivalent to
        ///     <see cref="StringBuilder.Length" /> after call returns. The <see cref="int" /> maximum number of characters to copy
        ///     to the buffer, including the null character. If the text exceeds this limit, it is truncated.
        /// </param>
        /// <returns>
        ///     If the function succeeds, the return value is the length, in characters, of the copied string, not including
        ///     the terminating null character. If the window has no title bar or text, if the title bar is empty, or if the window
        ///     or control handle is invalid, the return value is zero. To get extended error information, call GetLastError.<br />
        ///     This function cannot retrieve the text of an edit control in another application.
        /// </returns>
        /// <remarks>
        ///     If the target window is owned by the current process, GetWindowText causes a WM_GETTEXT message to be sent to the
        ///     specified window or control. If the target window is owned by another process and has a caption, GetWindowText
        ///     retrieves the window caption text. If the window does not have a caption, the return value is a null string. This
        ///     behavior is by design. It allows applications to call GetWindowText without becoming unresponsive if the process
        ///     that owns the target window is not responding. However, if the target window is not responding and it belongs to
        ///     the calling application, GetWindowText will cause the calling application to become unresponsive. To retrieve the
        ///     text of a control in another process, send a WM_GETTEXT message directly instead of calling GetWindowText.<br />For
        ///     an example go to
        ///     <see cref="!:https://msdn.microsoft.com/en-us/library/windows/desktop/ms644928%28v=vs.85%29.aspx#sending">
        ///     Sending a
        ///     Message.
        ///     </see>
        /// </remarks>
        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        public static extern int GetWindowText(IntPtr hWnd, StringBuilder lpString, int nMaxCount);

        public static string GetActiveWindowTitle()
        {
            const int nChars = 256;
            StringBuilder Buff = new(nChars);
            IntPtr handle = GetForegroundWindow();

            if (GetWindowText(handle, Buff, nChars) > 0)
            {
                return Buff.ToString();
            }
            return null;
        }

        public static string GetWindowTitle(IntPtr hWnd)
        {
            const int nChars = 256;
            StringBuilder Buff = new(nChars);

            if (GetWindowText(hWnd, Buff, nChars) > 0)
            {
                return Buff.ToString();
            }
            return null;
        }

        [DllImport("user32.dll", SetLastError = true)]
        public static extern uint GetWindowThreadProcessId(IntPtr hWnd, out uint lpdwProcessId);

        // When you don't want the ProcessId, use this overload and pass IntPtr.Zero for the second parameter
        [DllImport("user32.dll")]
        public static extern uint GetWindowThreadProcessId(IntPtr hWnd, IntPtr ProcessId);

        [DllImport("user32.dll", SetLastError = true)]
        public static extern bool GetWindowRect(IntPtr hwnd, out RECT lpRect);

        /// <summary>
        /// Synthesizes a keystroke. The system can use such a synthesized keystroke to generate a WM_KEYUP or WM_KEYDOWN message. The keyboard driver's interrupt handler calls the keybd_event function.
        /// </summary>
        /// <param name="bVk">A virtual-key code. The code must be a value in the range 1 to 254. For a complete list, see Virtual Key Codes.</param>
        /// <param name="bScan">A hardware scan code for the key.</param>
        /// <param name="dwFlags">Controls various aspects of function operation. This parameter can be one or more of the following values.</param>
        /// <param name="dwExtraInfo">An additional value associated with the key stroke.</param>
        [DllImport("user32.dll")]
        public static extern void keybd_event(byte bVk, byte bScan, uint dwFlags, UIntPtr dwExtraInfo);

        public static void keybd_event(byte bVk, uint dwFlags)
        {
            keybd_event(bVk, 0x45, dwFlags, UIntPtr.Zero);
        }

        public static void keybd_event(VirtualKeyCode bVk, KeyEventFlag dwFlags)
        {
            keybd_event((byte)bVk, (uint)dwFlags);
        }

        /// <summary>
        /// Retrieves the status of the specified virtual key. The status specifies whether the key is up, down, or toggled (on, off—alternating each time the key is pressed).
        /// </summary>
        /// <param name="nVirtKey">
        /// A virtual key. If the desired virtual key is a letter or digit (A through Z, a through z, or 0 through 9), nVirtKey must be set to the ASCII value of that character. For other keys, it must be a virtual-key code.<br/>
        /// If a non-English keyboard layout is used, virtual keys with values in the range ASCII A through Z and 0 through 9 are used to specify most of the character keys.For example, for the German keyboard layout, the virtual key of value ASCII O(0x4F) refers to the "o" key, whereas VK_OEM_1 refers to the "o with umlaut" key.
        /// </param>
        /// <returns>
        /// The return value specifies the status of the specified virtual key, as follows:
        /// <list type="bullet">
        /// <item>If the high-order bit is 1, the key is down; otherwise, it is up.</item>
        /// <item>If the low-order bit is 1, the key is toggled. A key, such as the CAPS LOCK key, is toggled if it is turned on. The key is off and untoggled if the low-order bit is 0. A toggle key's indicator light (if any) on the keyboard will be on when the key is toggled, and off when the key is untoggled.</item>
        /// </list>
        /// </returns>
        [DllImport("USER32.dll")]
        public static extern short GetKeyState(int nVirtKey);

        public static KeyStates GetKeyState(VirtualKeyCode virtKey)
        {
            short rawState = GetKeyState((short)virtKey);
            KeyStates retVal = 0;

            if ((rawState & 0x8000) == 1)
                retVal |= KeyStates.Down;

            if ((rawState & 0x1) == 1)
                retVal |= KeyStates.Toggled;

            return retVal;
        }

        [DllImport("user32.dll", SetLastError = true)]
        public static extern IntPtr SetWindowsHookEx(HookType hookType, HookProc lpfn, IntPtr hMod, uint dwThreadId);

        [DllImport("user32.dll", SetLastError = true)]
        public static extern IntPtr SetWindowsHookEx(HookType hookType, LowLevelKeyboardProc lpfn, IntPtr hMod, uint dwThreadId);

        [DllImport("user32.dll", SetLastError = true)]
        public static extern IntPtr SetWindowsHookEx(HookType hookType, LowLevelMouseProc lpfn, IntPtr hMod, uint dwThreadId);
        
        [DllImport("user32.dll", SetLastError = true)]
        public static extern bool UnhookWindowsHookEx(IntPtr hhk);

        public delegate IntPtr HookProc(int code, IntPtr wParam, IntPtr lParam);
        public delegate IntPtr LowLevelKeyboardProc(int code, WM wParam, KBDLLHOOKSTRUCT lParam);
        public delegate IntPtr LowLevelMouseProc(int code, WM wParam, MSLLHOOKSTRUCT lParam);

        /// <summary>
        ///     Passes the hook information to the next hook procedure in the current hook chain. A hook procedure can call this
        ///     function either before or after processing the hook information.
        ///     <para>
        ///     See [ https://msdn.microsoft.com/en-us/library/windows/desktop/ms644974%28v=vs.85%29.aspx ] for more
        ///     information.
        ///     </para>
        /// </summary>
        /// <param name="hhk">C++ ( hhk [in, optional]. Type: HHOOK )<br />This parameter is ignored. </param>
        /// <param name="nCode">
        ///     C++ ( nCode [in]. Type: int )<br />The hook code passed to the current hook procedure. The next
        ///     hook procedure uses this code to determine how to process the hook information.
        /// </param>
        /// <param name="wParam">
        ///     C++ ( wParam [in]. Type: WPARAM )<br />The wParam value passed to the current hook procedure. The
        ///     meaning of this parameter depends on the type of hook associated with the current hook chain.
        /// </param>
        /// <param name="lParam">
        ///     C++ ( lParam [in]. Type: LPARAM )<br />The lParam value passed to the current hook procedure. The
        ///     meaning of this parameter depends on the type of hook associated with the current hook chain.
        /// </param>
        /// <returns>
        ///     C++ ( Type: LRESULT )<br />This value is returned by the next hook procedure in the chain. The current hook
        ///     procedure must also return this value. The meaning of the return value depends on the hook type. For more
        ///     information, see the descriptions of the individual hook procedures.
        /// </returns>
        /// <remarks>
        ///     <para>
        ///     Hook procedures are installed in chains for particular hook types. <see cref="CallNextHookEx" /> calls the
        ///     next hook in the chain.
        ///     </para>
        ///     <para>
        ///     Calling CallNextHookEx is optional, but it is highly recommended; otherwise, other applications that have
        ///     installed hooks will not receive hook notifications and may behave incorrectly as a result. You should call
        ///     <see cref="CallNextHookEx" /> unless you absolutely need to prevent the notification from being seen by other
        ///     applications.
        ///     </para>
        /// </remarks>
        [DllImport("user32.dll")]
        public static extern IntPtr CallNextHookEx(IntPtr hhk, int nCode, IntPtr wParam,
           IntPtr lParam);

        // overload for use with LowLevelKeyboardProc
        [DllImport("user32.dll")]
        public static extern IntPtr CallNextHookEx(IntPtr hhk, int nCode, WM wParam, [In] KBDLLHOOKSTRUCT lParam);

        // overload for use with LowLevelMouseProc
        [DllImport("user32.dll")]
        public static extern IntPtr CallNextHookEx(IntPtr hhk, int nCode, WM wParam, [In] MSLLHOOKSTRUCT lParam);
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct RECT
    {
        public int Left;        // x position of upper-left corner
        public int Top;         // y position of upper-left corner
        public int Right;       // x position of lower-right corner
        public int Bottom;      // y position of lower-right corner

        public bool Equals(RECT other)
        {
            return Top == other.Top &&
                Right == other.Right &&
                Bottom == other.Bottom &&
                Left == other.Left;
        }

        public Rectangle ToRectangle()
        {
            return new(Left, Top, Right - Left, Bottom - Top);
        }
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct KBDLLHOOKSTRUCT
    {
        /// <summary>
        /// A virtual-key code. The code must be a value in the range 1 to 254.
        /// </summary>
        public uint vkCode;

        /// <summary>
        /// A hardware scan code for the key.
        /// </summary>
        public uint scanCode;

        /// <summary>
        /// The extended-key flag, event-injected flags, context code, and transition-state flag. This member is specified as follows. An application can use the following values to test the keystroke flags. Testing LLKHF_INJECTED (bit 4) will tell you whether the event was injected. If it was, then testing LLKHF_LOWER_IL_INJECTED (bit 1) will tell you whether or not the event was injected from a process running at lower integrity level.
        /// </summary>
        public KBDLLHOOKSTRUCTFlags flags;

        /// <summary>
        /// The time stamp for this message, equivalent to what GetMessageTime would return for this message.
        /// </summary>
        public uint time;

        /// <summary>
        /// Additional information associated with the message.
        /// </summary>
        public UIntPtr dwExtraInfo;
    }

    /// <summary>
    /// Contains information about a low-level mouse input event.
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    public struct MSLLHOOKSTRUCT
    {
        /// <summary>
        /// The x- and y-coordinates of the cursor, in per-monitor-aware screen coordinates.
        /// </summary>
        public POINT pt;

        /// <summary>
        /// If the message is WM_MOUSEWHEEL, the high-order word of this member is the wheel delta. The low-order word is reserved. A positive value indicates that the wheel was rotated forward, away from the user; a negative value indicates that the wheel was rotated backward, toward the user. One wheel click is defined as WHEEL_DELTA, which is 120.<br/>
        /// If the message is WM_XBUTTONDOWN, WM_XBUTTONUP, WM_XBUTTONDBLCLK, WM_NCXBUTTONDOWN, WM_NCXBUTTONUP, or WM_NCXBUTTONDBLCLK, the high-order word specifies which X button was pressed or released, and the low-order word is reserved.This value can be one or more of the following values.Otherwise, mouseData is not used.
        /// </summary>
        public uint mouseData;

        /// <summary>
        /// The event-injected flags. An application can use the following values to test the flags. Testing LLMHF_INJECTED (bit 0) will tell you whether the event was injected. If it was, then testing LLMHF_LOWER_IL_INJECTED (bit 1) will tell you whether or not the event was injected from a process running at lower integrity level.
        /// </summary>
        public uint flags;

        /// <summary>
        /// The time stamp for this message.
        /// </summary>
        public uint time;

        /// <summary>
        /// Additional information associated with the message.
        /// </summary>
        public ulong dwExtraInfo;
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct MOUSEHOOKSTRUCT
    {
        /// <summary>
        /// The x- and y-coordinates of the cursor, in screen coordinates.
        /// </summary>
        public POINT pt;

        /// <summary>
        /// A handle to the window that will receive the mouse message corresponding to the mouse event.
        /// </summary>
        public IntPtr hwnd;

        /// <summary>
        /// The hit-test value. For a list of hit-test values, see the description of the WM_NCHITTEST message.
        /// </summary>
        public uint wHitTestCode;

        /// <summary>
        /// Additional information associated with the message.
        /// </summary>
        public ulong dwExtraInfo;
    }

    [Flags]
    public enum KBDLLHOOKSTRUCTFlags : uint
    {
        LLKHF_EXTENDED = 0x01,
        LLKHF_INJECTED = 0x10,
        LLKHF_ALTDOWN = 0x20,
        LLKHF_UP = 0x80,
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct POINT
    {
        public int X;
        public int Y;

        public POINT(int x, int y)
        {
            this.X = x;
            this.Y = y;
        }

        public static implicit operator System.Drawing.Point(POINT p)
        {
            return new System.Drawing.Point(p.X, p.Y);
        }

        public static implicit operator POINT(System.Drawing.Point p)
        {
            return new POINT(p.X, p.Y);
        }
    }

    /// <summary>
    /// Windows Messages
    /// Defined in winuser.h from Windows SDK v6.1
    /// Documentation pulled from MSDN.
    /// </summary>
    public enum WM : uint
    {
        /// <summary>
        /// The WM_NULL message performs no operation. An application sends the WM_NULL message if it wants to post a message that the recipient window will ignore.
        /// </summary>
        NULL = 0x0000,
        /// <summary>
        /// The WM_CREATE message is sent when an application requests that a window be created by calling the CreateWindowEx or CreateWindow function. (The message is sent before the function returns.) The window procedure of the new window receives this message after the window is created, but before the window becomes visible.
        /// </summary>
        CREATE = 0x0001,
        /// <summary>
        /// The WM_DESTROY message is sent when a window is being destroyed. It is sent to the window procedure of the window being destroyed after the window is removed from the screen.
        /// This message is sent first to the window being destroyed and then to the child windows (if any) as they are destroyed. During the processing of the message, it can be assumed that all child windows still exist.
        /// /// </summary>
        DESTROY = 0x0002,
        /// <summary>
        /// The WM_MOVE message is sent after a window has been moved.
        /// </summary>
        MOVE = 0x0003,
        /// <summary>
        /// The WM_SIZE message is sent to a window after its size has changed.
        /// </summary>
        SIZE = 0x0005,
        /// <summary>
        /// The WM_ACTIVATE message is sent to both the window being activated and the window being deactivated. If the windows use the same input queue, the message is sent synchronously, first to the window procedure of the top-level window being deactivated, then to the window procedure of the top-level window being activated. If the windows use different input queues, the message is sent asynchronously, so the window is activated immediately.
        /// </summary>
        ACTIVATE = 0x0006,
        /// <summary>
        /// The WM_SETFOCUS message is sent to a window after it has gained the keyboard focus.
        /// </summary>
        SETFOCUS = 0x0007,
        /// <summary>
        /// The WM_KILLFOCUS message is sent to a window immediately before it loses the keyboard focus.
        /// </summary>
        KILLFOCUS = 0x0008,
        /// <summary>
        /// The WM_ENABLE message is sent when an application changes the enabled state of a window. It is sent to the window whose enabled state is changing. This message is sent before the EnableWindow function returns, but after the enabled state (WS_DISABLED style bit) of the window has changed.
        /// </summary>
        ENABLE = 0x000A,
        /// <summary>
        /// An application sends the WM_SETREDRAW message to a window to allow changes in that window to be redrawn or to prevent changes in that window from being redrawn.
        /// </summary>
        SETREDRAW = 0x000B,
        /// <summary>
        /// An application sends a WM_SETTEXT message to set the text of a window.
        /// </summary>
        SETTEXT = 0x000C,
        /// <summary>
        /// An application sends a WM_GETTEXT message to copy the text that corresponds to a window into a buffer provided by the caller.
        /// </summary>
        GETTEXT = 0x000D,
        /// <summary>
        /// An application sends a WM_GETTEXTLENGTH message to determine the length, in characters, of the text associated with a window.
        /// </summary>
        GETTEXTLENGTH = 0x000E,
        /// <summary>
        /// The WM_PAINT message is sent when the system or another application makes a request to paint a portion of an application's window. The message is sent when the UpdateWindow or RedrawWindow function is called, or by the DispatchMessage function when the application obtains a WM_PAINT message by using the GetMessage or PeekMessage function.
        /// </summary>
        PAINT = 0x000F,
        /// <summary>
        /// The WM_CLOSE message is sent as a signal that a window or an application should terminate.
        /// </summary>
        CLOSE = 0x0010,
        /// <summary>
        /// The WM_QUERYENDSESSION message is sent when the user chooses to end the session or when an application calls one of the system shutdown functions. If any application returns zero, the session is not ended. The system stops sending WM_QUERYENDSESSION messages as soon as one application returns zero.
        /// After processing this message, the system sends the WM_ENDSESSION message with the wParam parameter set to the results of the WM_QUERYENDSESSION message.
        /// </summary>
        QUERYENDSESSION = 0x0011,
        /// <summary>
        /// The WM_QUERYOPEN message is sent to an icon when the user requests that the window be restored to its previous size and position.
        /// </summary>
        QUERYOPEN = 0x0013,
        /// <summary>
        /// The WM_ENDSESSION message is sent to an application after the system processes the results of the WM_QUERYENDSESSION message. The WM_ENDSESSION message informs the application whether the session is ending.
        /// </summary>
        ENDSESSION = 0x0016,
        /// <summary>
        /// The WM_QUIT message indicates a request to terminate an application and is generated when the application calls the PostQuitMessage function. It causes the GetMessage function to return zero.
        /// </summary>
        QUIT = 0x0012,
        /// <summary>
        /// The WM_ERASEBKGND message is sent when the window background must be erased (for example, when a window is resized). The message is sent to prepare an invalidated portion of a window for painting.
        /// </summary>
        ERASEBKGND = 0x0014,
        /// <summary>
        /// This message is sent to all top-level windows when a change is made to a system color setting.
        /// </summary>
        SYSCOLORCHANGE = 0x0015,
        /// <summary>
        /// The WM_SHOWWINDOW message is sent to a window when the window is about to be hidden or shown.
        /// </summary>
        SHOWWINDOW = 0x0018,
        /// <summary>
        /// An application sends the WM_WININICHANGE message to all top-level windows after making a change to the WIN.INI file. The SystemParametersInfo function sends this message after an application uses the function to change a setting in WIN.INI.
        /// Note  The WM_WININICHANGE message is provided only for compatibility with earlier versions of the system. Applications should use the WM_SETTINGCHANGE message.
        /// </summary>
        WININICHANGE = 0x001A,
        /// <summary>
        /// An application sends the WM_WININICHANGE message to all top-level windows after making a change to the WIN.INI file. The SystemParametersInfo function sends this message after an application uses the function to change a setting in WIN.INI.
        /// Note  The WM_WININICHANGE message is provided only for compatibility with earlier versions of the system. Applications should use the WM_SETTINGCHANGE message.
        /// </summary>
        SETTINGCHANGE = WININICHANGE,
        /// <summary>
        /// The WM_DEVMODECHANGE message is sent to all top-level windows whenever the user changes device-mode settings.
        /// </summary>
        DEVMODECHANGE = 0x001B,
        /// <summary>
        /// The WM_ACTIVATEAPP message is sent when a window belonging to a different application than the active window is about to be activated. The message is sent to the application whose window is being activated and to the application whose window is being deactivated.
        /// </summary>
        ACTIVATEAPP = 0x001C,
        /// <summary>
        /// An application sends the WM_FONTCHANGE message to all top-level windows in the system after changing the pool of font resources.
        /// </summary>
        FONTCHANGE = 0x001D,
        /// <summary>
        /// A message that is sent whenever there is a change in the system time.
        /// </summary>
        TIMECHANGE = 0x001E,
        /// <summary>
        /// The WM_CANCELMODE message is sent to cancel certain modes, such as mouse capture. For example, the system sends this message to the active window when a dialog box or message box is displayed. Certain functions also send this message explicitly to the specified window regardless of whether it is the active window. For example, the EnableWindow function sends this message when disabling the specified window.
        /// </summary>
        CANCELMODE = 0x001F,
        /// <summary>
        /// The WM_SETCURSOR message is sent to a window if the mouse causes the cursor to move within a window and mouse input is not captured.
        /// </summary>
        SETCURSOR = 0x0020,
        /// <summary>
        /// The WM_MOUSEACTIVATE message is sent when the cursor is in an inactive window and the user presses a mouse button. The parent window receives this message only if the child window passes it to the DefWindowProc function.
        /// </summary>
        MOUSEACTIVATE = 0x0021,
        /// <summary>
        /// The WM_CHILDACTIVATE message is sent to a child window when the user clicks the window's title bar or when the window is activated, moved, or sized.
        /// </summary>
        CHILDACTIVATE = 0x0022,
        /// <summary>
        /// The WM_QUEUESYNC message is sent by a computer-based training (CBT) application to separate user-input messages from other messages sent through the WH_JOURNALPLAYBACK Hook procedure.
        /// </summary>
        QUEUESYNC = 0x0023,
        /// <summary>
        /// The WM_GETMINMAXINFO message is sent to a window when the size or position of the window is about to change. An application can use this message to override the window's default maximized size and position, or its default minimum or maximum tracking size.
        /// </summary>
        GETMINMAXINFO = 0x0024,
        /// <summary>
        /// Windows NT 3.51 and earlier: The WM_PAINTICON message is sent to a minimized window when the icon is to be painted. This message is not sent by newer versions of Microsoft Windows, except in unusual circumstances explained in the Remarks.
        /// </summary>
        PAINTICON = 0x0026,
        /// <summary>
        /// Windows NT 3.51 and earlier: The WM_ICONERASEBKGND message is sent to a minimized window when the background of the icon must be filled before painting the icon. A window receives this message only if a class icon is defined for the window; otherwise, WM_ERASEBKGND is sent. This message is not sent by newer versions of Windows.
        /// </summary>
        ICONERASEBKGND = 0x0027,
        /// <summary>
        /// The WM_NEXTDLGCTL message is sent to a dialog box procedure to set the keyboard focus to a different control in the dialog box.
        /// </summary>
        NEXTDLGCTL = 0x0028,
        /// <summary>
        /// The WM_SPOOLERSTATUS message is sent from Print Manager whenever a job is added to or removed from the Print Manager queue.
        /// </summary>
        SPOOLERSTATUS = 0x002A,
        /// <summary>
        /// The WM_DRAWITEM message is sent to the parent window of an owner-drawn button, combo box, list box, or menu when a visual aspect of the button, combo box, list box, or menu has changed.
        /// </summary>
        DRAWITEM = 0x002B,
        /// <summary>
        /// The WM_MEASUREITEM message is sent to the owner window of a combo box, list box, list view control, or menu item when the control or menu is created.
        /// </summary>
        MEASUREITEM = 0x002C,
        /// <summary>
        /// Sent to the owner of a list box or combo box when the list box or combo box is destroyed or when items are removed by the LB_DELETESTRING, LB_RESETCONTENT, CB_DELETESTRING, or CB_RESETCONTENT message. The system sends a WM_DELETEITEM message for each deleted item. The system sends the WM_DELETEITEM message for any deleted list box or combo box item with nonzero item data.
        /// </summary>
        DELETEITEM = 0x002D,
        /// <summary>
        /// Sent by a list box with the LBS_WANTKEYBOARDINPUT style to its owner in response to a WM_KEYDOWN message.
        /// </summary>
        VKEYTOITEM = 0x002E,
        /// <summary>
        /// Sent by a list box with the LBS_WANTKEYBOARDINPUT style to its owner in response to a WM_CHAR message.
        /// </summary>
        CHARTOITEM = 0x002F,
        /// <summary>
        /// An application sends a WM_SETFONT message to specify the font that a control is to use when drawing text.
        /// </summary>
        SETFONT = 0x0030,
        /// <summary>
        /// An application sends a WM_GETFONT message to a control to retrieve the font with which the control is currently drawing its text.
        /// </summary>
        GETFONT = 0x0031,
        /// <summary>
        /// An application sends a WM_SETHOTKEY message to a window to associate a hot key with the window. When the user presses the hot key, the system activates the window.
        /// </summary>
        SETHOTKEY = 0x0032,
        /// <summary>
        /// An application sends a WM_GETHOTKEY message to determine the hot key associated with a window.
        /// </summary>
        GETHOTKEY = 0x0033,
        /// <summary>
        /// The WM_QUERYDRAGICON message is sent to a minimized (iconic) window. The window is about to be dragged by the user but does not have an icon defined for its class. An application can return a handle to an icon or cursor. The system displays this cursor or icon while the user drags the icon.
        /// </summary>
        QUERYDRAGICON = 0x0037,
        /// <summary>
        /// The system sends the WM_COMPAREITEM message to determine the relative position of a new item in the sorted list of an owner-drawn combo box or list box. Whenever the application adds a new item, the system sends this message to the owner of a combo box or list box created with the CBS_SORT or LBS_SORT style.
        /// </summary>
        COMPAREITEM = 0x0039,
        /// <summary>
        /// Active Accessibility sends the WM_GETOBJECT message to obtain information about an accessible object contained in a server application.
        /// Applications never send this message directly. It is sent only by Active Accessibility in response to calls to AccessibleObjectFromPoint, AccessibleObjectFromEvent, or AccessibleObjectFromWindow. However, server applications handle this message.
        /// </summary>
        GETOBJECT = 0x003D,
        /// <summary>
        /// The WM_COMPACTING message is sent to all top-level windows when the system detects more than 12.5 percent of system time over a 30- to 60-second interval is being spent compacting memory. This indicates that system memory is low.
        /// </summary>
        COMPACTING = 0x0041,
        /// <summary>
        /// WM_COMMNOTIFY is Obsolete for Win32-Based Applications
        /// </summary>
        [Obsolete("Obsolete for Win32 Based Applications")]
        COMMNOTIFY = 0x0044,
        /// <summary>
        /// The WM_WINDOWPOSCHANGING message is sent to a window whose size, position, or place in the Z order is about to change as a result of a call to the SetWindowPos function or another window-management function.
        /// </summary>
        WINDOWPOSCHANGING = 0x0046,
        /// <summary>
        /// The WM_WINDOWPOSCHANGED message is sent to a window whose size, position, or place in the Z order has changed as a result of a call to the SetWindowPos function or another window-management function.
        /// </summary>
        WINDOWPOSCHANGED = 0x0047,
        /// <summary>
        /// Notifies applications that the system, typically a battery-powered personal computer, is about to enter a suspended mode.
        /// Use: POWERBROADCAST
        /// </summary>
        [Obsolete("Provided only for compatibility with 16-bit Windows-based applications")]
        POWER = 0x0048,
        /// <summary>
        /// An application sends the WM_COPYDATA message to pass data to another application.
        /// </summary>
        COPYDATA = 0x004A,
        /// <summary>
        /// The WM_CANCELJOURNAL message is posted to an application when a user cancels the application's journaling activities. The message is posted with a NULL window handle.
        /// </summary>
        CANCELJOURNAL = 0x004B,
        /// <summary>
        /// Sent by a common control to its parent window when an event has occurred or the control requires some information.
        /// </summary>
        NOTIFY = 0x004E,
        /// <summary>
        /// The WM_INPUTLANGCHANGEREQUEST message is posted to the window with the focus when the user chooses a new input language, either with the hotkey (specified in the Keyboard control panel application) or from the indicator on the system taskbar. An application can accept the change by passing the message to the DefWindowProc function or reject the change (and prevent it from taking place) by returning immediately.
        /// </summary>
        INPUTLANGCHANGEREQUEST = 0x0050,
        /// <summary>
        /// The WM_INPUTLANGCHANGE message is sent to the topmost affected window after an application's input language has been changed. You should make any application-specific settings and pass the message to the DefWindowProc function, which passes the message to all first-level child windows. These child windows can pass the message to DefWindowProc to have it pass the message to their child windows, and so on.
        /// </summary>
        INPUTLANGCHANGE = 0x0051,
        /// <summary>
        /// Sent to an application that has initiated a training card with Microsoft Windows Help. The message informs the application when the user clicks an authorable button. An application initiates a training card by specifying the HELP_TCARD command in a call to the WinHelp function.
        /// </summary>
        TCARD = 0x0052,
        /// <summary>
        /// Indicates that the user pressed the F1 key. If a menu is active when F1 is pressed, WM_HELP is sent to the window associated with the menu; otherwise, WM_HELP is sent to the window that has the keyboard focus. If no window has the keyboard focus, WM_HELP is sent to the currently active window.
        /// </summary>
        HELP = 0x0053,
        /// <summary>
        /// The WM_USERCHANGED message is sent to all windows after the user has logged on or off. When the user logs on or off, the system updates the user-specific settings. The system sends this message immediately after updating the settings.
        /// </summary>
        USERCHANGED = 0x0054,
        /// <summary>
        /// Determines if a window accepts ANSI or Unicode structures in the WM_NOTIFY notification message. WM_NOTIFYFORMAT messages are sent from a common control to its parent window and from the parent window to the common control.
        /// </summary>
        NOTIFYFORMAT = 0x0055,
        /// <summary>
        /// The WM_CONTEXTMENU message notifies a window that the user clicked the right mouse button (right-clicked) in the window.
        /// </summary>
        CONTEXTMENU = 0x007B,
        /// <summary>
        /// The WM_STYLECHANGING message is sent to a window when the SetWindowLong function is about to change one or more of the window's styles.
        /// </summary>
        STYLECHANGING = 0x007C,
        /// <summary>
        /// The WM_STYLECHANGED message is sent to a window after the SetWindowLong function has changed one or more of the window's styles
        /// </summary>
        STYLECHANGED = 0x007D,
        /// <summary>
        /// The WM_DISPLAYCHANGE message is sent to all windows when the display resolution has changed.
        /// </summary>
        DISPLAYCHANGE = 0x007E,
        /// <summary>
        /// The WM_GETICON message is sent to a window to retrieve a handle to the large or small icon associated with a window. The system displays the large icon in the ALT+TAB dialog, and the small icon in the window caption.
        /// </summary>
        GETICON = 0x007F,
        /// <summary>
        /// An application sends the WM_SETICON message to associate a new large or small icon with a window. The system displays the large icon in the ALT+TAB dialog box, and the small icon in the window caption.
        /// </summary>
        SETICON = 0x0080,
        /// <summary>
        /// The WM_NCCREATE message is sent prior to the WM_CREATE message when a window is first created.
        /// </summary>
        NCCREATE = 0x0081,
        /// <summary>
        /// The WM_NCDESTROY message informs a window that its nonclient area is being destroyed. The DestroyWindow function sends the WM_NCDESTROY message to the window following the WM_DESTROY message. WM_DESTROY is used to free the allocated memory object associated with the window.
        /// The WM_NCDESTROY message is sent after the child windows have been destroyed. In contrast, WM_DESTROY is sent before the child windows are destroyed.
        /// </summary>
        NCDESTROY = 0x0082,
        /// <summary>
        /// The WM_NCCALCSIZE message is sent when the size and position of a window's client area must be calculated. By processing this message, an application can control the content of the window's client area when the size or position of the window changes.
        /// </summary>
        NCCALCSIZE = 0x0083,
        /// <summary>
        /// The WM_NCHITTEST message is sent to a window when the cursor moves, or when a mouse button is pressed or released. If the mouse is not captured, the message is sent to the window beneath the cursor. Otherwise, the message is sent to the window that has captured the mouse.
        /// </summary>
        NCHITTEST = 0x0084,
        /// <summary>
        /// The WM_NCPAINT message is sent to a window when its frame must be painted.
        /// </summary>
        NCPAINT = 0x0085,
        /// <summary>
        /// The WM_NCACTIVATE message is sent to a window when its nonclient area needs to be changed to indicate an active or inactive state.
        /// </summary>
        NCACTIVATE = 0x0086,
        /// <summary>
        /// The WM_GETDLGCODE message is sent to the window procedure associated with a control. By default, the system handles all keyboard input to the control; the system interprets certain types of keyboard input as dialog box navigation keys. To override this default behavior, the control can respond to the WM_GETDLGCODE message to indicate the types of input it wants to process itself.
        /// </summary>
        GETDLGCODE = 0x0087,
        /// <summary>
        /// The WM_SYNCPAINT message is used to synchronize painting while avoiding linking independent GUI threads.
        /// </summary>
        SYNCPAINT = 0x0088,
        /// <summary>
        /// The WM_NCMOUSEMOVE message is posted to a window when the cursor is moved within the nonclient area of the window. This message is posted to the window that contains the cursor. If a window has captured the mouse, this message is not posted.
        /// </summary>
        NCMOUSEMOVE = 0x00A0,
        /// <summary>
        /// The WM_NCLBUTTONDOWN message is posted when the user presses the left mouse button while the cursor is within the nonclient area of a window. This message is posted to the window that contains the cursor. If a window has captured the mouse, this message is not posted.
        /// </summary>
        NCLBUTTONDOWN = 0x00A1,
        /// <summary>
        /// The WM_NCLBUTTONUP message is posted when the user releases the left mouse button while the cursor is within the nonclient area of a window. This message is posted to the window that contains the cursor. If a window has captured the mouse, this message is not posted.
        /// </summary>
        NCLBUTTONUP = 0x00A2,
        /// <summary>
        /// The WM_NCLBUTTONDBLCLK message is posted when the user double-clicks the left mouse button while the cursor is within the nonclient area of a window. This message is posted to the window that contains the cursor. If a window has captured the mouse, this message is not posted.
        /// </summary>
        NCLBUTTONDBLCLK = 0x00A3,
        /// <summary>
        /// The WM_NCRBUTTONDOWN message is posted when the user presses the right mouse button while the cursor is within the nonclient area of a window. This message is posted to the window that contains the cursor. If a window has captured the mouse, this message is not posted.
        /// </summary>
        NCRBUTTONDOWN = 0x00A4,
        /// <summary>
        /// The WM_NCRBUTTONUP message is posted when the user releases the right mouse button while the cursor is within the nonclient area of a window. This message is posted to the window that contains the cursor. If a window has captured the mouse, this message is not posted.
        /// </summary>
        NCRBUTTONUP = 0x00A5,
        /// <summary>
        /// The WM_NCRBUTTONDBLCLK message is posted when the user double-clicks the right mouse button while the cursor is within the nonclient area of a window. This message is posted to the window that contains the cursor. If a window has captured the mouse, this message is not posted.
        /// </summary>
        NCRBUTTONDBLCLK = 0x00A6,
        /// <summary>
        /// The WM_NCMBUTTONDOWN message is posted when the user presses the middle mouse button while the cursor is within the nonclient area of a window. This message is posted to the window that contains the cursor. If a window has captured the mouse, this message is not posted.
        /// </summary>
        NCMBUTTONDOWN = 0x00A7,
        /// <summary>
        /// The WM_NCMBUTTONUP message is posted when the user releases the middle mouse button while the cursor is within the nonclient area of a window. This message is posted to the window that contains the cursor. If a window has captured the mouse, this message is not posted.
        /// </summary>
        NCMBUTTONUP = 0x00A8,
        /// <summary>
        /// The WM_NCMBUTTONDBLCLK message is posted when the user double-clicks the middle mouse button while the cursor is within the nonclient area of a window. This message is posted to the window that contains the cursor. If a window has captured the mouse, this message is not posted.
        /// </summary>
        NCMBUTTONDBLCLK = 0x00A9,
        /// <summary>
        /// The WM_NCXBUTTONDOWN message is posted when the user presses the first or second X button while the cursor is in the nonclient area of a window. This message is posted to the window that contains the cursor. If a window has captured the mouse, this message is not posted.
        /// </summary>
        NCXBUTTONDOWN = 0x00AB,
        /// <summary>
        /// The WM_NCXBUTTONUP message is posted when the user releases the first or second X button while the cursor is in the nonclient area of a window. This message is posted to the window that contains the cursor. If a window has captured the mouse, this message is not posted.
        /// </summary>
        NCXBUTTONUP = 0x00AC,
        /// <summary>
        /// The WM_NCXBUTTONDBLCLK message is posted when the user double-clicks the first or second X button while the cursor is in the nonclient area of a window. This message is posted to the window that contains the cursor. If a window has captured the mouse, this message is not posted.
        /// </summary>
        NCXBUTTONDBLCLK = 0x00AD,
        /// <summary>
        /// The WM_INPUT_DEVICE_CHANGE message is sent to the window that registered to receive raw input. A window receives this message through its WindowProc function.
        /// </summary>
        INPUT_DEVICE_CHANGE = 0x00FE,
        /// <summary>
        /// The WM_INPUT message is sent to the window that is getting raw input.
        /// </summary>
        INPUT = 0x00FF,
        /// <summary>
        /// This message filters for keyboard messages.
        /// </summary>
        KEYFIRST = 0x0100,
        /// <summary>
        /// The WM_KEYDOWN message is posted to the window with the keyboard focus when a nonsystem key is pressed. A nonsystem key is a key that is pressed when the ALT key is not pressed.
        /// </summary>
        KEYDOWN = 0x0100,
        /// <summary>
        /// The WM_KEYUP message is posted to the window with the keyboard focus when a nonsystem key is released. A nonsystem key is a key that is pressed when the ALT key is not pressed, or a keyboard key that is pressed when a window has the keyboard focus.
        /// </summary>
        KEYUP = 0x0101,
        /// <summary>
        /// The WM_CHAR message is posted to the window with the keyboard focus when a WM_KEYDOWN message is translated by the TranslateMessage function. The WM_CHAR message contains the character code of the key that was pressed.
        /// </summary>
        CHAR = 0x0102,
        /// <summary>
        /// The WM_DEADCHAR message is posted to the window with the keyboard focus when a WM_KEYUP message is translated by the TranslateMessage function. WM_DEADCHAR specifies a character code generated by a dead key. A dead key is a key that generates a character, such as the umlaut (double-dot), that is combined with another character to form a composite character. For example, the umlaut-O character (Ö) is generated by typing the dead key for the umlaut character, and then typing the O key.
        /// </summary>
        DEADCHAR = 0x0103,
        /// <summary>
        /// The WM_SYSKEYDOWN message is posted to the window with the keyboard focus when the user presses the F10 key (which activates the menu bar) or holds down the ALT key and then presses another key. It also occurs when no window currently has the keyboard focus; in this case, the WM_SYSKEYDOWN message is sent to the active window. The window that receives the message can distinguish between these two contexts by checking the context code in the lParam parameter.
        /// </summary>
        SYSKEYDOWN = 0x0104,
        /// <summary>
        /// The WM_SYSKEYUP message is posted to the window with the keyboard focus when the user releases a key that was pressed while the ALT key was held down. It also occurs when no window currently has the keyboard focus; in this case, the WM_SYSKEYUP message is sent to the active window. The window that receives the message can distinguish between these two contexts by checking the context code in the lParam parameter.
        /// </summary>
        SYSKEYUP = 0x0105,
        /// <summary>
        /// The WM_SYSCHAR message is posted to the window with the keyboard focus when a WM_SYSKEYDOWN message is translated by the TranslateMessage function. It specifies the character code of a system character key — that is, a character key that is pressed while the ALT key is down.
        /// </summary>
        SYSCHAR = 0x0106,
        /// <summary>
        /// The WM_SYSDEADCHAR message is sent to the window with the keyboard focus when a WM_SYSKEYDOWN message is translated by the TranslateMessage function. WM_SYSDEADCHAR specifies the character code of a system dead key — that is, a dead key that is pressed while holding down the ALT key.
        /// </summary>
        SYSDEADCHAR = 0x0107,
        /// <summary>
        /// The WM_UNICHAR message is posted to the window with the keyboard focus when a WM_KEYDOWN message is translated by the TranslateMessage function. The WM_UNICHAR message contains the character code of the key that was pressed.
        /// The WM_UNICHAR message is equivalent to WM_CHAR, but it uses Unicode Transformation Format (UTF)-32, whereas WM_CHAR uses UTF-16. It is designed to send or post Unicode characters to ANSI windows and it can can handle Unicode Supplementary Plane characters.
        /// </summary>
        UNICHAR = 0x0109,
        /// <summary>
        /// This message filters for keyboard messages.
        /// </summary>
        KEYLAST = 0x0108,
        /// <summary>
        /// Sent immediately before the IME generates the composition string as a result of a keystroke. A window receives this message through its WindowProc function.
        /// </summary>
        IME_STARTCOMPOSITION = 0x010D,
        /// <summary>
        /// Sent to an application when the IME ends composition. A window receives this message through its WindowProc function.
        /// </summary>
        IME_ENDCOMPOSITION = 0x010E,
        /// <summary>
        /// Sent to an application when the IME changes composition status as a result of a keystroke. A window receives this message through its WindowProc function.
        /// </summary>
        IME_COMPOSITION = 0x010F,
        IME_KEYLAST = 0x010F,
        /// <summary>
        /// The WM_INITDIALOG message is sent to the dialog box procedure immediately before a dialog box is displayed. Dialog box procedures typically use this message to initialize controls and carry out any other initialization tasks that affect the appearance of the dialog box.
        /// </summary>
        INITDIALOG = 0x0110,
        /// <summary>
        /// The WM_COMMAND message is sent when the user selects a command item from a menu, when a control sends a notification message to its parent window, or when an accelerator keystroke is translated.
        /// </summary>
        COMMAND = 0x0111,
        /// <summary>
        /// A window receives this message when the user chooses a command from the Window menu, clicks the maximize button, minimize button, restore button, close button, or moves the form. You can stop the form from moving by filtering this out.
        /// </summary>
        SYSCOMMAND = 0x0112,
        /// <summary>
        /// The WM_TIMER message is posted to the installing thread's message queue when a timer expires. The message is posted by the GetMessage or PeekMessage function.
        /// </summary>
        TIMER = 0x0113,
        /// <summary>
        /// The WM_HSCROLL message is sent to a window when a scroll event occurs in the window's standard horizontal scroll bar. This message is also sent to the owner of a horizontal scroll bar control when a scroll event occurs in the control.
        /// </summary>
        HSCROLL = 0x0114,
        /// <summary>
        /// The WM_VSCROLL message is sent to a window when a scroll event occurs in the window's standard vertical scroll bar. This message is also sent to the owner of a vertical scroll bar control when a scroll event occurs in the control.
        /// </summary>
        VSCROLL = 0x0115,
        /// <summary>
        /// The WM_INITMENU message is sent when a menu is about to become active. It occurs when the user clicks an item on the menu bar or presses a menu key. This allows the application to modify the menu before it is displayed.
        /// </summary>
        INITMENU = 0x0116,
        /// <summary>
        /// The WM_INITMENUPOPUP message is sent when a drop-down menu or submenu is about to become active. This allows an application to modify the menu before it is displayed, without changing the entire menu.
        /// </summary>
        INITMENUPOPUP = 0x0117,
        /// <summary>
        /// The WM_MENUSELECT message is sent to a menu's owner window when the user selects a menu item.
        /// </summary>
        MENUSELECT = 0x011F,
        /// <summary>
        /// The WM_MENUCHAR message is sent when a menu is active and the user presses a key that does not correspond to any mnemonic or accelerator key. This message is sent to the window that owns the menu.
        /// </summary>
        MENUCHAR = 0x0120,
        /// <summary>
        /// The WM_ENTERIDLE message is sent to the owner window of a modal dialog box or menu that is entering an idle state. A modal dialog box or menu enters an idle state when no messages are waiting in its queue after it has processed one or more previous messages.
        /// </summary>
        ENTERIDLE = 0x0121,
        /// <summary>
        /// The WM_MENURBUTTONUP message is sent when the user releases the right mouse button while the cursor is on a menu item.
        /// </summary>
        MENURBUTTONUP = 0x0122,
        /// <summary>
        /// The WM_MENUDRAG message is sent to the owner of a drag-and-drop menu when the user drags a menu item.
        /// </summary>
        MENUDRAG = 0x0123,
        /// <summary>
        /// The WM_MENUGETOBJECT message is sent to the owner of a drag-and-drop menu when the mouse cursor enters a menu item or moves from the center of the item to the top or bottom of the item.
        /// </summary>
        MENUGETOBJECT = 0x0124,
        /// <summary>
        /// The WM_UNINITMENUPOPUP message is sent when a drop-down menu or submenu has been destroyed.
        /// </summary>
        UNINITMENUPOPUP = 0x0125,
        /// <summary>
        /// The WM_MENUCOMMAND message is sent when the user makes a selection from a menu.
        /// </summary>
        MENUCOMMAND = 0x0126,
        /// <summary>
        /// An application sends the WM_CHANGEUISTATE message to indicate that the user interface (UI) state should be changed.
        /// </summary>
        CHANGEUISTATE = 0x0127,
        /// <summary>
        /// An application sends the WM_UPDATEUISTATE message to change the user interface (UI) state for the specified window and all its child windows.
        /// </summary>
        UPDATEUISTATE = 0x0128,
        /// <summary>
        /// An application sends the WM_QUERYUISTATE message to retrieve the user interface (UI) state for a window.
        /// </summary>
        QUERYUISTATE = 0x0129,
        /// <summary>
        /// The WM_CTLCOLORMSGBOX message is sent to the owner window of a message box before Windows draws the message box. By responding to this message, the owner window can set the text and background colors of the message box by using the given display device context handle.
        /// </summary>
        CTLCOLORMSGBOX = 0x0132,
        /// <summary>
        /// An edit control that is not read-only or disabled sends the WM_CTLCOLOREDIT message to its parent window when the control is about to be drawn. By responding to this message, the parent window can use the specified device context handle to set the text and background colors of the edit control.
        /// </summary>
        CTLCOLOREDIT = 0x0133,
        /// <summary>
        /// Sent to the parent window of a list box before the system draws the list box. By responding to this message, the parent window can set the text and background colors of the list box by using the specified display device context handle.
        /// </summary>
        CTLCOLORLISTBOX = 0x0134,
        /// <summary>
        /// The WM_CTLCOLORBTN message is sent to the parent window of a button before drawing the button. The parent window can change the button's text and background colors. However, only owner-drawn buttons respond to the parent window processing this message.
        /// </summary>
        CTLCOLORBTN = 0x0135,
        /// <summary>
        /// The WM_CTLCOLORDLG message is sent to a dialog box before the system draws the dialog box. By responding to this message, the dialog box can set its text and background colors using the specified display device context handle.
        /// </summary>
        CTLCOLORDLG = 0x0136,
        /// <summary>
        /// The WM_CTLCOLORSCROLLBAR message is sent to the parent window of a scroll bar control when the control is about to be drawn. By responding to this message, the parent window can use the display context handle to set the background color of the scroll bar control.
        /// </summary>
        CTLCOLORSCROLLBAR = 0x0137,
        /// <summary>
        /// A static control, or an edit control that is read-only or disabled, sends the WM_CTLCOLORSTATIC message to its parent window when the control is about to be drawn. By responding to this message, the parent window can use the specified device context handle to set the text and background colors of the static control.
        /// </summary>
        CTLCOLORSTATIC = 0x0138,
        /// <summary>
        /// Use WM_MOUSEFIRST to specify the first mouse message. Use the PeekMessage() Function.
        /// </summary>
        MOUSEFIRST = 0x0200,
        /// <summary>
        /// The WM_MOUSEMOVE message is posted to a window when the cursor moves. If the mouse is not captured, the message is posted to the window that contains the cursor. Otherwise, the message is posted to the window that has captured the mouse.
        /// </summary>
        MOUSEMOVE = 0x0200,
        /// <summary>
        /// The WM_LBUTTONDOWN message is posted when the user presses the left mouse button while the cursor is in the client area of a window. If the mouse is not captured, the message is posted to the window beneath the cursor. Otherwise, the message is posted to the window that has captured the mouse.
        /// </summary>
        LBUTTONDOWN = 0x0201,
        /// <summary>
        /// The WM_LBUTTONUP message is posted when the user releases the left mouse button while the cursor is in the client area of a window. If the mouse is not captured, the message is posted to the window beneath the cursor. Otherwise, the message is posted to the window that has captured the mouse.
        /// </summary>
        LBUTTONUP = 0x0202,
        /// <summary>
        /// The WM_LBUTTONDBLCLK message is posted when the user double-clicks the left mouse button while the cursor is in the client area of a window. If the mouse is not captured, the message is posted to the window beneath the cursor. Otherwise, the message is posted to the window that has captured the mouse.
        /// </summary>
        LBUTTONDBLCLK = 0x0203,
        /// <summary>
        /// The WM_RBUTTONDOWN message is posted when the user presses the right mouse button while the cursor is in the client area of a window. If the mouse is not captured, the message is posted to the window beneath the cursor. Otherwise, the message is posted to the window that has captured the mouse.
        /// </summary>
        RBUTTONDOWN = 0x0204,
        /// <summary>
        /// The WM_RBUTTONUP message is posted when the user releases the right mouse button while the cursor is in the client area of a window. If the mouse is not captured, the message is posted to the window beneath the cursor. Otherwise, the message is posted to the window that has captured the mouse.
        /// </summary>
        RBUTTONUP = 0x0205,
        /// <summary>
        /// The WM_RBUTTONDBLCLK message is posted when the user double-clicks the right mouse button while the cursor is in the client area of a window. If the mouse is not captured, the message is posted to the window beneath the cursor. Otherwise, the message is posted to the window that has captured the mouse.
        /// </summary>
        RBUTTONDBLCLK = 0x0206,
        /// <summary>
        /// The WM_MBUTTONDOWN message is posted when the user presses the middle mouse button while the cursor is in the client area of a window. If the mouse is not captured, the message is posted to the window beneath the cursor. Otherwise, the message is posted to the window that has captured the mouse.
        /// </summary>
        MBUTTONDOWN = 0x0207,
        /// <summary>
        /// The WM_MBUTTONUP message is posted when the user releases the middle mouse button while the cursor is in the client area of a window. If the mouse is not captured, the message is posted to the window beneath the cursor. Otherwise, the message is posted to the window that has captured the mouse.
        /// </summary>
        MBUTTONUP = 0x0208,
        /// <summary>
        /// The WM_MBUTTONDBLCLK message is posted when the user double-clicks the middle mouse button while the cursor is in the client area of a window. If the mouse is not captured, the message is posted to the window beneath the cursor. Otherwise, the message is posted to the window that has captured the mouse.
        /// </summary>
        MBUTTONDBLCLK = 0x0209,
        /// <summary>
        /// The WM_MOUSEWHEEL message is sent to the focus window when the mouse wheel is rotated. The DefWindowProc function propagates the message to the window's parent. There should be no internal forwarding of the message, since DefWindowProc propagates it up the parent chain until it finds a window that processes it.
        /// </summary>
        MOUSEWHEEL = 0x020A,
        /// <summary>
        /// The WM_XBUTTONDOWN message is posted when the user presses the first or second X button while the cursor is in the client area of a window. If the mouse is not captured, the message is posted to the window beneath the cursor. Otherwise, the message is posted to the window that has captured the mouse.
        /// </summary>
        XBUTTONDOWN = 0x020B,
        /// <summary>
        /// The WM_XBUTTONUP message is posted when the user releases the first or second X button while the cursor is in the client area of a window. If the mouse is not captured, the message is posted to the window beneath the cursor. Otherwise, the message is posted to the window that has captured the mouse.
        /// </summary>
        XBUTTONUP = 0x020C,
        /// <summary>
        /// The WM_XBUTTONDBLCLK message is posted when the user double-clicks the first or second X button while the cursor is in the client area of a window. If the mouse is not captured, the message is posted to the window beneath the cursor. Otherwise, the message is posted to the window that has captured the mouse.
        /// </summary>
        XBUTTONDBLCLK = 0x020D,
        /// <summary>
        /// The WM_MOUSEHWHEEL message is sent to the focus window when the mouse's horizontal scroll wheel is tilted or rotated. The DefWindowProc function propagates the message to the window's parent. There should be no internal forwarding of the message, since DefWindowProc propagates it up the parent chain until it finds a window that processes it.
        /// </summary>
        MOUSEHWHEEL = 0x020E,
        /// <summary>
        /// Use WM_MOUSELAST to specify the last mouse message. Used with PeekMessage() Function.
        /// </summary>
        MOUSELAST = 0x020E,
        /// <summary>
        /// The WM_PARENTNOTIFY message is sent to the parent of a child window when the child window is created or destroyed, or when the user clicks a mouse button while the cursor is over the child window. When the child window is being created, the system sends WM_PARENTNOTIFY just before the CreateWindow or CreateWindowEx function that creates the window returns. When the child window is being destroyed, the system sends the message before any processing to destroy the window takes place.
        /// </summary>
        PARENTNOTIFY = 0x0210,
        /// <summary>
        /// The WM_ENTERMENULOOP message informs an application's main window procedure that a menu modal loop has been entered.
        /// </summary>
        ENTERMENULOOP = 0x0211,
        /// <summary>
        /// The WM_EXITMENULOOP message informs an application's main window procedure that a menu modal loop has been exited.
        /// </summary>
        EXITMENULOOP = 0x0212,
        /// <summary>
        /// The WM_NEXTMENU message is sent to an application when the right or left arrow key is used to switch between the menu bar and the system menu.
        /// </summary>
        NEXTMENU = 0x0213,
        /// <summary>
        /// The WM_SIZING message is sent to a window that the user is resizing. By processing this message, an application can monitor the size and position of the drag rectangle and, if needed, change its size or position.
        /// </summary>
        SIZING = 0x0214,
        /// <summary>
        /// The WM_CAPTURECHANGED message is sent to the window that is losing the mouse capture.
        /// </summary>
        CAPTURECHANGED = 0x0215,
        /// <summary>
        /// The WM_MOVING message is sent to a window that the user is moving. By processing this message, an application can monitor the position of the drag rectangle and, if needed, change its position.
        /// </summary>
        MOVING = 0x0216,
        /// <summary>
        /// Notifies applications that a power-management event has occurred.
        /// </summary>
        POWERBROADCAST = 0x0218,
        /// <summary>
        /// Notifies an application of a change to the hardware configuration of a device or the computer.
        /// </summary>
        DEVICECHANGE = 0x0219,
        /// <summary>
        /// An application sends the WM_MDICREATE message to a multiple-document interface (MDI) client window to create an MDI child window.
        /// </summary>
        MDICREATE = 0x0220,
        /// <summary>
        /// An application sends the WM_MDIDESTROY message to a multiple-document interface (MDI) client window to close an MDI child window.
        /// </summary>
        MDIDESTROY = 0x0221,
        /// <summary>
        /// An application sends the WM_MDIACTIVATE message to a multiple-document interface (MDI) client window to instruct the client window to activate a different MDI child window.
        /// </summary>
        MDIACTIVATE = 0x0222,
        /// <summary>
        /// An application sends the WM_MDIRESTORE message to a multiple-document interface (MDI) client window to restore an MDI child window from maximized or minimized size.
        /// </summary>
        MDIRESTORE = 0x0223,
        /// <summary>
        /// An application sends the WM_MDINEXT message to a multiple-document interface (MDI) client window to activate the next or previous child window.
        /// </summary>
        MDINEXT = 0x0224,
        /// <summary>
        /// An application sends the WM_MDIMAXIMIZE message to a multiple-document interface (MDI) client window to maximize an MDI child window. The system resizes the child window to make its client area fill the client window. The system places the child window's window menu icon in the rightmost position of the frame window's menu bar, and places the child window's restore icon in the leftmost position. The system also appends the title bar text of the child window to that of the frame window.
        /// </summary>
        MDIMAXIMIZE = 0x0225,
        /// <summary>
        /// An application sends the WM_MDITILE message to a multiple-document interface (MDI) client window to arrange all of its MDI child windows in a tile format.
        /// </summary>
        MDITILE = 0x0226,
        /// <summary>
        /// An application sends the WM_MDICASCADE message to a multiple-document interface (MDI) client window to arrange all its child windows in a cascade format.
        /// </summary>
        MDICASCADE = 0x0227,
        /// <summary>
        /// An application sends the WM_MDIICONARRANGE message to a multiple-document interface (MDI) client window to arrange all minimized MDI child windows. It does not affect child windows that are not minimized.
        /// </summary>
        MDIICONARRANGE = 0x0228,
        /// <summary>
        /// An application sends the WM_MDIGETACTIVE message to a multiple-document interface (MDI) client window to retrieve the handle to the active MDI child window.
        /// </summary>
        MDIGETACTIVE = 0x0229,
        /// <summary>
        /// An application sends the WM_MDISETMENU message to a multiple-document interface (MDI) client window to replace the entire menu of an MDI frame window, to replace the window menu of the frame window, or both.
        /// </summary>
        MDISETMENU = 0x0230,
        /// <summary>
        /// The WM_ENTERSIZEMOVE message is sent one time to a window after it enters the moving or sizing modal loop. The window enters the moving or sizing modal loop when the user clicks the window's title bar or sizing border, or when the window passes the WM_SYSCOMMAND message to the DefWindowProc function and the wParam parameter of the message specifies the SC_MOVE or SC_SIZE value. The operation is complete when DefWindowProc returns.
        /// The system sends the WM_ENTERSIZEMOVE message regardless of whether the dragging of full windows is enabled.
        /// </summary>
        ENTERSIZEMOVE = 0x0231,
        /// <summary>
        /// The WM_EXITSIZEMOVE message is sent one time to a window, after it has exited the moving or sizing modal loop. The window enters the moving or sizing modal loop when the user clicks the window's title bar or sizing border, or when the window passes the WM_SYSCOMMAND message to the DefWindowProc function and the wParam parameter of the message specifies the SC_MOVE or SC_SIZE value. The operation is complete when DefWindowProc returns.
        /// </summary>
        EXITSIZEMOVE = 0x0232,
        /// <summary>
        /// Sent when the user drops a file on the window of an application that has registered itself as a recipient of dropped files.
        /// </summary>
        DROPFILES = 0x0233,
        /// <summary>
        /// An application sends the WM_MDIREFRESHMENU message to a multiple-document interface (MDI) client window to refresh the window menu of the MDI frame window.
        /// </summary>
        MDIREFRESHMENU = 0x0234,
        /// <summary>
        /// Sent to an application when a window is activated. A window receives this message through its WindowProc function.
        /// </summary>
        IME_SETCONTEXT = 0x0281,
        /// <summary>
        /// Sent to an application to notify it of changes to the IME window. A window receives this message through its WindowProc function.
        /// </summary>
        IME_NOTIFY = 0x0282,
        /// <summary>
        /// Sent by an application to direct the IME window to carry out the requested command. The application uses this message to control the IME window that it has created. To send this message, the application calls the SendMessage function with the following parameters.
        /// </summary>
        IME_CONTROL = 0x0283,
        /// <summary>
        /// Sent to an application when the IME window finds no space to extend the area for the composition window. A window receives this message through its WindowProc function.
        /// </summary>
        IME_COMPOSITIONFULL = 0x0284,
        /// <summary>
        /// Sent to an application when the operating system is about to change the current IME. A window receives this message through its WindowProc function.
        /// </summary>
        IME_SELECT = 0x0285,
        /// <summary>
        /// Sent to an application when the IME gets a character of the conversion result. A window receives this message through its WindowProc function.
        /// </summary>
        IME_CHAR = 0x0286,
        /// <summary>
        /// Sent to an application to provide commands and request information. A window receives this message through its WindowProc function.
        /// </summary>
        IME_REQUEST = 0x0288,
        /// <summary>
        /// Sent to an application by the IME to notify the application of a key press and to keep message order. A window receives this message through its WindowProc function.
        /// </summary>
        IME_KEYDOWN = 0x0290,
        /// <summary>
        /// Sent to an application by the IME to notify the application of a key release and to keep message order. A window receives this message through its WindowProc function.
        /// </summary>
        IME_KEYUP = 0x0291,
        /// <summary>
        /// The WM_MOUSEHOVER message is posted to a window when the cursor hovers over the client area of the window for the period of time specified in a prior call to TrackMouseEvent.
        /// </summary>
        MOUSEHOVER = 0x02A1,
        /// <summary>
        /// The WM_MOUSELEAVE message is posted to a window when the cursor leaves the client area of the window specified in a prior call to TrackMouseEvent.
        /// </summary>
        MOUSELEAVE = 0x02A3,
        /// <summary>
        /// The WM_NCMOUSEHOVER message is posted to a window when the cursor hovers over the nonclient area of the window for the period of time specified in a prior call to TrackMouseEvent.
        /// </summary>
        NCMOUSEHOVER = 0x02A0,
        /// <summary>
        /// The WM_NCMOUSELEAVE message is posted to a window when the cursor leaves the nonclient area of the window specified in a prior call to TrackMouseEvent.
        /// </summary>
        NCMOUSELEAVE = 0x02A2,
        /// <summary>
        /// The WM_WTSSESSION_CHANGE message notifies applications of changes in session state.
        /// </summary>
        WTSSESSION_CHANGE = 0x02B1,
        TABLET_FIRST = 0x02c0,
        TABLET_LAST = 0x02df,
        /// <summary>
        /// An application sends a WM_CUT message to an edit control or combo box to delete (cut) the current selection, if any, in the edit control and copy the deleted text to the clipboard in CF_TEXT format.
        /// </summary>
        CUT = 0x0300,
        /// <summary>
        /// An application sends the WM_COPY message to an edit control or combo box to copy the current selection to the clipboard in CF_TEXT format.
        /// </summary>
        COPY = 0x0301,
        /// <summary>
        /// An application sends a WM_PASTE message to an edit control or combo box to copy the current content of the clipboard to the edit control at the current caret position. Data is inserted only if the clipboard contains data in CF_TEXT format.
        /// </summary>
        PASTE = 0x0302,
        /// <summary>
        /// An application sends a WM_CLEAR message to an edit control or combo box to delete (clear) the current selection, if any, from the edit control.
        /// </summary>
        CLEAR = 0x0303,
        /// <summary>
        /// An application sends a WM_UNDO message to an edit control to undo the last operation. When this message is sent to an edit control, the previously deleted text is restored or the previously added text is deleted.
        /// </summary>
        UNDO = 0x0304,
        /// <summary>
        /// The WM_RENDERFORMAT message is sent to the clipboard owner if it has delayed rendering a specific clipboard format and if an application has requested data in that format. The clipboard owner must render data in the specified format and place it on the clipboard by calling the SetClipboardData function.
        /// </summary>
        RENDERFORMAT = 0x0305,
        /// <summary>
        /// The WM_RENDERALLFORMATS message is sent to the clipboard owner before it is destroyed, if the clipboard owner has delayed rendering one or more clipboard formats. For the content of the clipboard to remain available to other applications, the clipboard owner must render data in all the formats it is capable of generating, and place the data on the clipboard by calling the SetClipboardData function.
        /// </summary>
        RENDERALLFORMATS = 0x0306,
        /// <summary>
        /// The WM_DESTROYCLIPBOARD message is sent to the clipboard owner when a call to the EmptyClipboard function empties the clipboard.
        /// </summary>
        DESTROYCLIPBOARD = 0x0307,
        /// <summary>
        /// The WM_DRAWCLIPBOARD message is sent to the first window in the clipboard viewer chain when the content of the clipboard changes. This enables a clipboard viewer window to display the new content of the clipboard.
        /// </summary>
        DRAWCLIPBOARD = 0x0308,
        /// <summary>
        /// The WM_PAINTCLIPBOARD message is sent to the clipboard owner by a clipboard viewer window when the clipboard contains data in the CF_OWNERDISPLAY format and the clipboard viewer's client area needs repainting.
        /// </summary>
        PAINTCLIPBOARD = 0x0309,
        /// <summary>
        /// The WM_VSCROLLCLIPBOARD message is sent to the clipboard owner by a clipboard viewer window when the clipboard contains data in the CF_OWNERDISPLAY format and an event occurs in the clipboard viewer's vertical scroll bar. The owner should scroll the clipboard image and update the scroll bar values.
        /// </summary>
        VSCROLLCLIPBOARD = 0x030A,
        /// <summary>
        /// The WM_SIZECLIPBOARD message is sent to the clipboard owner by a clipboard viewer window when the clipboard contains data in the CF_OWNERDISPLAY format and the clipboard viewer's client area has changed size.
        /// </summary>
        SIZECLIPBOARD = 0x030B,
        /// <summary>
        /// The WM_ASKCBFORMATNAME message is sent to the clipboard owner by a clipboard viewer window to request the name of a CF_OWNERDISPLAY clipboard format.
        /// </summary>
        ASKCBFORMATNAME = 0x030C,
        /// <summary>
        /// The WM_CHANGECBCHAIN message is sent to the first window in the clipboard viewer chain when a window is being removed from the chain.
        /// </summary>
        CHANGECBCHAIN = 0x030D,
        /// <summary>
        /// The WM_HSCROLLCLIPBOARD message is sent to the clipboard owner by a clipboard viewer window. This occurs when the clipboard contains data in the CF_OWNERDISPLAY format and an event occurs in the clipboard viewer's horizontal scroll bar. The owner should scroll the clipboard image and update the scroll bar values.
        /// </summary>
        HSCROLLCLIPBOARD = 0x030E,
        /// <summary>
        /// This message informs a window that it is about to receive the keyboard focus, giving the window the opportunity to realize its logical palette when it receives the focus.
        /// </summary>
        QUERYNEWPALETTE = 0x030F,
        /// <summary>
        /// The WM_PALETTEISCHANGING message informs applications that an application is going to realize its logical palette.
        /// </summary>
        PALETTEISCHANGING = 0x0310,
        /// <summary>
        /// This message is sent by the OS to all top-level and overlapped windows after the window with the keyboard focus realizes its logical palette.
        /// This message enables windows that do not have the keyboard focus to realize their logical palettes and update their client areas.
        /// </summary>
        PALETTECHANGED = 0x0311,
        /// <summary>
        /// The WM_HOTKEY message is posted when the user presses a hot key registered by the RegisterHotKey function. The message is placed at the top of the message queue associated with the thread that registered the hot key.
        /// </summary>
        HOTKEY = 0x0312,
        /// <summary>
        /// The WM_PRINT message is sent to a window to request that it draw itself in the specified device context, most commonly in a printer device context.
        /// </summary>
        PRINT = 0x0317,
        /// <summary>
        /// The WM_PRINTCLIENT message is sent to a window to request that it draw its client area in the specified device context, most commonly in a printer device context.
        /// </summary>
        PRINTCLIENT = 0x0318,
        /// <summary>
        /// The WM_APPCOMMAND message notifies a window that the user generated an application command event, for example, by clicking an application command button using the mouse or typing an application command key on the keyboard.
        /// </summary>
        APPCOMMAND = 0x0319,
        /// <summary>
        /// The WM_THEMECHANGED message is broadcast to every window following a theme change event. Examples of theme change events are the activation of a theme, the deactivation of a theme, or a transition from one theme to another.
        /// </summary>
        THEMECHANGED = 0x031A,
        /// <summary>
        /// Sent when the contents of the clipboard have changed.
        /// </summary>
        CLIPBOARDUPDATE = 0x031D,
        /// <summary>
        /// The system will send a window the WM_DWMCOMPOSITIONCHANGED message to indicate that the availability of desktop composition has changed.
        /// </summary>
        DWMCOMPOSITIONCHANGED = 0x031E,
        /// <summary>
        /// WM_DWMNCRENDERINGCHANGED is called when the non-client area rendering status of a window has changed. Only windows that have set the flag DWM_BLURBEHIND.fTransitionOnMaximized to true will get this message.
        /// </summary>
        DWMNCRENDERINGCHANGED = 0x031F,
        /// <summary>
        /// Sent to all top-level windows when the colorization color has changed.
        /// </summary>
        DWMCOLORIZATIONCOLORCHANGED = 0x0320,
        /// <summary>
        /// WM_DWMWINDOWMAXIMIZEDCHANGE will let you know when a DWM composed window is maximized. You also have to register for this message as well. You'd have other windowd go opaque when this message is sent.
        /// </summary>
        DWMWINDOWMAXIMIZEDCHANGE = 0x0321,
        /// <summary>
        /// Sent to request extended title bar information. A window receives this message through its WindowProc function.
        /// </summary>
        GETTITLEBARINFOEX = 0x033F,
        HANDHELDFIRST = 0x0358,
        HANDHELDLAST = 0x035F,
        AFXFIRST = 0x0360,
        AFXLAST = 0x037F,
        PENWINFIRST = 0x0380,
        PENWINLAST = 0x038F,
        /// <summary>
        /// The WM_APP constant is used by applications to help define private messages, usually of the form WM_APP+X, where X is an integer value.
        /// </summary>
        APP = 0x8000,
        /// <summary>
        /// The WM_USER constant is used by applications to help define private messages for use by private window classes, usually of the form WM_USER+X, where X is an integer value.
        /// </summary>
        USER = 0x0400,

        /// <summary>
        /// An application sends the WM_CPL_LAUNCH message to Windows Control Panel to request that a Control Panel application be started.
        /// </summary>
        CPL_LAUNCH = USER + 0x1000,
        /// <summary>
        /// The WM_CPL_LAUNCHED message is sent when a Control Panel application, started by the WM_CPL_LAUNCH message, has closed. The WM_CPL_LAUNCHED message is sent to the window identified by the wParam parameter of the WM_CPL_LAUNCH message that started the application.
        /// </summary>
        CPL_LAUNCHED = USER + 0x1001,
        /// <summary>
        /// WM_SYSTIMER is a well-known yet still undocumented message. Windows uses WM_SYSTIMER for internal actions like scrolling.
        /// </summary>
        SYSTIMER = 0x118,

        /// <summary>
        /// The accessibility state has changed.
        /// </summary>
        HSHELL_ACCESSIBILITYSTATE = 11,
        /// <summary>
        /// The shell should activate its main window.
        /// </summary>
        HSHELL_ACTIVATESHELLWINDOW = 3,
        /// <summary>
        /// The user completed an input event (for example, pressed an application command button on the mouse or an application command key on the keyboard), and the application did not handle the WM_APPCOMMAND message generated by that input.
        /// If the Shell procedure handles the WM_COMMAND message, it should not call CallNextHookEx. See the Return Value section for more information.
        /// </summary>
        HSHELL_APPCOMMAND = 12,
        /// <summary>
        /// A window is being minimized or maximized. The system needs the coordinates of the minimized rectangle for the window.
        /// </summary>
        HSHELL_GETMINRECT = 5,
        /// <summary>
        /// Keyboard language was changed or a new keyboard layout was loaded.
        /// </summary>
        HSHELL_LANGUAGE = 8,
        /// <summary>
        /// The title of a window in the task bar has been redrawn.
        /// </summary>
        HSHELL_REDRAW = 6,
        /// <summary>
        /// The user has selected the task list. A shell application that provides a task list should return TRUE to prevent Windows from starting its task list.
        /// </summary>
        HSHELL_TASKMAN = 7,
        /// <summary>
        /// A top-level, unowned window has been created. The window exists when the system calls this hook.
        /// </summary>
        HSHELL_WINDOWCREATED = 1,
        /// <summary>
        /// A top-level, unowned window is about to be destroyed. The window still exists when the system calls this hook.
        /// </summary>
        HSHELL_WINDOWDESTROYED = 2,
        /// <summary>
        /// The activation has changed to a different top-level, unowned window.
        /// </summary>
        HSHELL_WINDOWACTIVATED = 4,
        /// <summary>
        /// A top-level window is being replaced. The window exists when the system calls this hook.
        /// </summary>
        HSHELL_WINDOWREPLACED = 13
    }


    public enum HookType : int
    {
        /// <summary>
        /// Installs a hook procedure that monitors messages before the system sends them to the destination window procedure. For more information, see the <see href="https://docs.microsoft.com/previous-versions/windows/desktop/legacy/ms644975(v=vs.85)">CallWndProc</see> hook procedure.
        /// </summary>
        WH_CALLWNDPROC = 4,

        /// <summary>
        /// Installs a hook procedure that monitors messages after they have been processed by the destination window procedure. For more information, see the <see href="https://docs.microsoft.com/windows/desktop/api/winuser/nc-winuser-hookproc">CallWndRetProc</see> hook procedure.
        /// </summary>
        WH_CALLWNDPROCRET = 12,

        /// <summary>
        /// Installs a hook procedure that receives notifications useful to a CBT application. For more information, see the <see href="https://docs.microsoft.com/previous-versions/windows/desktop/legacy/ms644977(v=vs.85)">CBTProc</see> hook procedure.
        /// </summary>
        WH_CBT = 5,

        /// <summary>
        /// Installs a hook procedure useful for debugging other hook procedures. For more information, see the <see href="https://docs.microsoft.com/previous-versions/windows/desktop/legacy/ms644978(v=vs.85)">DebugProc</see> hook procedure.
        /// </summary>
        WH_DEBUG = 9,

        /// <summary>
        /// Installs a hook procedure that will be called when the application's foreground thread is about to become idle. This hook is useful for performing low priority tasks during idle time. For more information, see the <see href="https://docs.microsoft.com/previous-versions/windows/desktop/legacy/ms644980(v=vs.85)">ForegroundIdleProc</see> hook procedure.
        /// </summary>
        WH_FOREGROUNDIDLE = 11,

        /// <summary>
        /// Installs a hook procedure that monitors messages posted to a message queue. For more information, see the <see href="https://docs.microsoft.com/previous-versions/windows/desktop/legacy/ms644981(v=vs.85)">GetMsgProc</see> hook procedure.
        /// </summary>
        WH_GETMESSAGE = 3,

        /// <summary>
        /// Installs a hook procedure that posts messages previously recorded by a <see href="https://docs.microsoft.com/windows/desktop/winmsg/about-hooks">WH_JOURNALRECORD</see> hook procedure. For more information, see the <see href="https://docs.microsoft.com/previous-versions/windows/desktop/legacy/ms644982(v=vs.85)">JournalPlaybackProc</see> hook procedure.
        /// </summary>
        WH_JOURNALPLAYBACK = 1,

        /// <summary>
        /// Installs a hook procedure that records input messages posted to the system message queue. This hook is useful for recording macros. For more information, see the <see href="https://docs.microsoft.com/previous-versions/windows/desktop/legacy/ms644983(v=vs.85)">JournalRecordProc</see> hook procedure.
        /// </summary>
        WH_JOURNALRECORD = 0,

        /// <summary>
        /// Installs a hook procedure that monitors keystroke messages. For more information, see the <see href="https://docs.microsoft.com/previous-versions/windows/desktop/legacy/ms644984(v=vs.85)">KeyboardProc</see> hook procedure.
        /// </summary>
        WH_KEYBOARD = 2,

        /// <summary>
        /// Installs a hook procedure that monitors low-level keyboard input events. For more information, see the <see href="https://docs.microsoft.com/previous-versions/windows/desktop/legacy/ms644985(v=vs.85)">LowLevelKeyboardProc</see> hook procedure.
        /// </summary>
        WH_KEYBOARD_LL = 13,

        /// <summary>
        /// Installs a hook procedure that monitors mouse messages. For more information, see the <see href="https://docs.microsoft.com/previous-versions/windows/desktop/legacy/ms644988(v=vs.85)">MouseProc</see> hook procedure.
        /// </summary>
        WH_MOUSE = 7,

        /// <summary>
        /// Installs a hook procedure that monitors low-level mouse input events. For more information, see the <see href="https://docs.microsoft.com/previous-versions/windows/desktop/legacy/ms644986(v=vs.85)">LowLevelMouseProc</see> hook procedure.
        /// </summary>
        WH_MOUSE_LL = 14,

        /// <summary>
        /// Installs a hook procedure that monitors messages generated as a result of an input event in a dialog box, message box, menu, or scroll bar. For more information, see the <see href="https://docs.microsoft.com/previous-versions/windows/desktop/legacy/ms644987(v=vs.85)">MessageProc</see> hook procedure.
        /// </summary>
        WH_MSGFILTER = -1,

        /// <summary>
        /// Installs a hook procedure that receives notifications useful to shell applications. For more information, see the <see href="https://docs.microsoft.com/previous-versions/windows/desktop/legacy/ms644991(v=vs.85)">ShellProc</see> hook procedure.
        /// </summary>
        WH_SHELL = 10,

        /// <summary>
        /// Installs a hook procedure that monitors messages generated as a result of an input event in a dialog box, message box, menu, or scroll bar. The hook procedure monitors these messages for all applications in the same desktop as the calling thread. For more information, see the <see href="https://docs.microsoft.com/previous-versions/windows/desktop/legacy/ms644992(v=vs.85)">SysMsgProc</see> hook procedure.
        /// </summary>
        WH_SYSMSGFILTER = 6
    }

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
            VirtualKeyCode.VK_MENU => null,
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
