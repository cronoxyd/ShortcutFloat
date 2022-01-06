using ShortcutFloat.Common.Input;
using ShortcutFloat.Common.Runtime;
using ShortcutFloat.Common.Runtime.Interop;
using ShortcutFloat.Common.Runtime.Interop.Drawing;
using ShortcutFloat.Common.Runtime.Interop.Input;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Input;

namespace ShortcutFloat.Common.Services
{
    public class EnvironmentMonitor
    {
        private bool _running = false;

        /// <summary>
        /// Whether the <see cref="EnvironmentMonitor"/> is currently running.
        /// </summary>
        /// <seealso cref="Start"/>
        /// <seealso cref="Stop"/>
        public bool Running => _running;

        /// <summary>
        /// The text in the title bar of the current foreground window.
        /// </summary>
        public string ForegroundWindowText { get; private set; } = null;

        /// <summary>
        /// The handle of the current foreground window.
        /// </summary>
        public IntPtr? ForegroundWindowHandle { get; private set; } = null;

        /// <summary>
        /// The bounds of the current foreground window.
        /// </summary>
        public Rectangle? ForegroundWindowBounds { get; private set; } = null;

        /// <summary>
        /// The process of the current foreground window.
        /// </summary>
        public Process ForegroundWindowProcess { get; private set; } = null;

        /// <summary>
        /// The process of this application (will be used e.g. for <see cref="IgnoreCurrentProcess"/>).
        /// </summary>
        public static Process CurrentProcess { get; } = Process.GetCurrentProcess();

        /// <summary>
        /// Specifies whether to not raise changed events when the foreground process id is 0.
        /// </summary>
        public bool IgnoreIdleProcess { get; set; } = true;

        /// <summary>
        /// Specifies whether to not raise changed events when the foreground process id is that of the current process.
        /// </summary>
        public bool IgnoreCurrentProcess { get; set; } = true;

        /// <summary>
        /// Specifies whether to not raise changed events when the handle of the foreground window is zero.
        /// </summary>
        public bool IgnoreZeroHandle { get; set; } = true;

        /// <summary>
        /// Specifies whether to ignore window positions that are out of the <see cref="MaxScreenBounds"/>
        /// </summary>
        /// <remarks>
        /// Use <see cref="OutOfBoundsTolerance"/> to configure the tolerance.
        /// </remarks>
        public bool IgnoreOutOfBounds { get; set; } = true;

        /// <summary>
        /// Specifies the number of pixels for which an out-of-bounds window is still considered in-bounds.
        /// </summary>
        public int OutOfBoundsTolerance { get; set; } = 2000;

        /// <summary>
        /// Returns the maximum screen bounds, as if all screens form one large screen.
        /// </summary>
        public Rectangle MaxScreenBounds { get; } = GetMaxScreenBounds();

        /// <summary>
        /// Specifies the interval at which the environment is inspected in milliseconds.
        /// </summary>
        public int MonitorIntervalMilliseconds { get; set; } = 10;

        public bool MuteNextKeyboardEvent { get; set; } = false;

        public bool MuteNextMouseEvent { get; set; } = false;

        private IntPtr? LowLevelKeyboardHookHandle { get; set; } = null;
        private IntPtr? LowLevelMouseHookHandle { get; set; } = null;

        public event ForegroundWindowChangedEventHandler ForegroundWindowChanged = (sender, e) => { };
        public event ForegroundWindowBoundsChangedEventHandler ForegroundWindowBoundsChanged = (sender, e) => { };

        public event MaskedKeyEventHandler KeyDown = (sender, e) => Debug.WriteLine($"Key down: {e.Key}");
        public event MaskedKeyEventHandler KeyUp = (sender, e) => Debug.WriteLine($"Key up: {e.Key}");
        public event MouseButtonEventHandler MouseDown = (sender, e) => Debug.WriteLine($"Mouse down: {e.Button}");
        public event MouseButtonEventHandler MouseUp = (sender, e) => Debug.WriteLine($"Mouse up: {e.Button}");

        private void WindowMonitorLoop()
        {
            if (MonitorIntervalMilliseconds <= 0)
                throw new InvalidOperationException($"{nameof(MonitorIntervalMilliseconds)} must be a positive integer.");

            IntPtr? lastForegroundWindowHandle = null;
            string lastForegroundWindowText = null;
            Dictionary<IntPtr, RECT> lastWindowRects = new();

            while (Running)
            {
                var currentForegroundWindowHandle = InteropServices.GetForegroundWindow();
                _ = InteropServices.GetWindowThreadProcessId(currentForegroundWindowHandle, out uint currentForegroundWindowProcessId);

                // Ignore current process
                if (IgnoreCurrentProcess && currentForegroundWindowProcessId == CurrentProcess.Id)
                    continue;

                // Ignore "Idle" process
                if (IgnoreIdleProcess && currentForegroundWindowProcessId == 0)
                    continue;

                if (IgnoreZeroHandle && currentForegroundWindowHandle == IntPtr.Zero)
                    continue;

                ForegroundWindowHandle = currentForegroundWindowHandle;
                ForegroundWindowText = InteropServices.GetActiveWindowTitle();
                _ = InteropServices.GetWindowThreadProcessId(ForegroundWindowHandle.Value, out uint procId);
                ForegroundWindowProcess = Process.GetProcessById((int)procId);

                _ = InteropServices.GetWindowRect(ForegroundWindowHandle.Value, out RECT foregroundWindowRect);
                ForegroundWindowBounds = foregroundWindowRect.ToRectangle();

                if (IgnoreOutOfBounds && (
                    foregroundWindowRect.Top < (MaxScreenBounds.Top - OutOfBoundsTolerance) ||
                    foregroundWindowRect.Right > (MaxScreenBounds.Right + OutOfBoundsTolerance) ||
                    foregroundWindowRect.Bottom > (MaxScreenBounds.Bottom + OutOfBoundsTolerance) ||
                    foregroundWindowRect.Left < (MaxScreenBounds.Left - OutOfBoundsTolerance)
                ))
                {
                    Debug.WriteLine($"Ignore out-of-bounds foreground rect\n\t{{Left = {foregroundWindowRect.Left}, Top = {foregroundWindowRect.Top}, Right = {foregroundWindowRect.Right}, Bottom = {foregroundWindowRect.Bottom}}}");
                    continue;
                }

                if (ForegroundWindowText != lastForegroundWindowText && lastForegroundWindowText != null)
                {
                    Debug.WriteLine($"Foreground window text changed (\"{lastForegroundWindowText}\" -> \"{ForegroundWindowText}\")");
                    ForegroundWindowChanged(this, new(ForegroundWindowHandle.Value));
                }

                if (ForegroundWindowHandle != lastForegroundWindowHandle && lastForegroundWindowHandle != null)
                {
                    Debug.WriteLine($"Foreground window handle changed (\"{lastForegroundWindowHandle}\" -> \"{ForegroundWindowHandle}\")");
                    ForegroundWindowChanged(this, new(ForegroundWindowHandle.Value));
                }

                if (lastWindowRects.ContainsKey(ForegroundWindowHandle.Value) && !foregroundWindowRect.Equals(lastWindowRects[ForegroundWindowHandle.Value]))
                {
                    var lastWindowRect = lastWindowRects[ForegroundWindowHandle.Value].ToRectangle();
                    ForegroundWindowBoundsChanged(this, new(foregroundWindowRect.ToRectangle(), lastWindowRect));
                }

                lastForegroundWindowHandle = ForegroundWindowHandle;
                lastForegroundWindowText = ForegroundWindowText;
                lastWindowRects[ForegroundWindowHandle.Value] = foregroundWindowRect;

                Thread.Sleep(MonitorIntervalMilliseconds);
            }
        }

        /// <remarks>
        /// Delegate is assigned to a variable to prevent garbage collector cleanup
        /// </remarks>
        private InteropServices.LowLevelKeyboardProc LowLevelKeyboardDelegate { get; set; } = null;

        /// <remarks>
        /// Delegate is assigned to a variable to prevent garbage collector cleanup
        /// </remarks>
        private InteropServices.LowLevelMouseProc LowLevelMouseDelegate { get; set; } = null;

        private void SetupInputMonitor()
        {
            LowLevelKeyboardDelegate = new(LowLevelKeyboardProc);
            LowLevelMouseDelegate = new(LowLevelMouseProc);

            LowLevelKeyboardHookHandle = InteropServices.SetWindowsHookEx(HookType.WH_KEYBOARD_LL, LowLevelKeyboardDelegate, IntPtr.Zero, 0);
            LowLevelMouseHookHandle = InteropServices.SetWindowsHookEx(HookType.WH_MOUSE_LL, LowLevelMouseDelegate, IntPtr.Zero, 0);
        }

        private void DisposeInputMonitor()
        {
            if (LowLevelKeyboardHookHandle != null)
                InteropServices.UnhookWindowsHookEx(LowLevelKeyboardHookHandle.Value);
            else
                Debug.Fail("Handle of low-level keyboard hook was unset");

            if (LowLevelMouseHookHandle != null)
                InteropServices.UnhookWindowsHookEx(LowLevelMouseHookHandle.Value);
            else
                Debug.Fail("Handle of low-level mouse hook was unset");
        }

        private IntPtr LowLevelKeyboardProc(int code, WM wParam, KBDLLHOOKSTRUCT lParam)
        {
            if (!MuteNextKeyboardEvent)
            {
                // TODO: Implement handling of keyboard event
                // See: https://docs.microsoft.com/en-us/previous-versions/windows/desktop/legacy/ms644985(v=vs.85)

                Key? key = ((VirtualKeyCode)lParam.vkCode).ToKey();

                if (key != null)
                {
                    MaskedKeyEventHandler eventHandler = null;
                    KeyStates states = KeyStates.None;

                    switch (wParam)
                    {
                        case WM.KEYDOWN or WM.SYSKEYDOWN:
                            eventHandler = KeyDown;
                            states |= KeyStates.Down;
                            break;
                        case WM.KEYUP or WM.SYSKEYUP:
                            eventHandler = KeyUp;
                            break;
                    }

                    if (eventHandler == null)
                        Debug.Fail("Failed to compose event for keyboard hook");
                    else
                    {
                        var e = new MaskedKeyEventArgs(key.Value, states, lParam.time);
                        eventHandler(this, e);
                    }
                }
            }
            else
                MuteNextKeyboardEvent = false;

            return InteropServices.CallNextHookEx(IntPtr.Zero, code, wParam, lParam);
        }

        private IntPtr LowLevelMouseProc(int code, WM wParam, MSLLHOOKSTRUCT lParam)
        {
            if (!MuteNextMouseEvent)
            {
                MouseButton? mouseButton = wParam switch
                {
                    WM.LBUTTONDOWN or WM.LBUTTONUP => MouseButton.Left,

                    WM.RBUTTONDOWN or WM.RBUTTONUP => MouseButton.Right,

                    WM.MBUTTONDOWN or WM.MBUTTONUP => MouseButton.Middle,

                    WM.XBUTTONDOWN or WM.XBUTTONUP => (lParam.mouseData >> 16) switch
                    {
                        0x0001 => MouseButton.XButton1,
                        0x0002 => MouseButton.XButton2,
                        _ => null
                    },

                    _ => null
                };

                if (mouseButton != null)
                {
                    MouseButtonEventHandler eventHandler = null;
                    MouseButtonState? state = null;

                    switch (wParam)
                    {

                        case WM.LBUTTONDOWN or WM.RBUTTONDOWN or WM.XBUTTONDOWN or
                            WM.MBUTTONDOWN:

                            state = MouseButtonState.Pressed;
                            eventHandler = MouseDown;
                            break;
                        case WM.LBUTTONUP or WM.RBUTTONUP or WM.XBUTTONUP or
                            WM.MBUTTONUP:

                            state = MouseButtonState.Released;
                            eventHandler = MouseUp;
                            break;
                    }

                    if (eventHandler == null || state == null)
                        Debug.Fail("Failed to compose event for mouse hook");
                    else
                    {
                        var e = new MouseButtonEventArgs(mouseButton.Value, state.Value, lParam.time);
                        eventHandler(this, e);
                    }
                }
            }
            else
                MuteNextMouseEvent = false;

            return InteropServices.CallNextHookEx(IntPtr.Zero, code, wParam, lParam);
        }

        /// <summary>
        /// Starts the monitor loop.
        /// </summary>
        public void Start()
        {
            if (!Running)
            {
                new Thread(WindowMonitorLoop).Start();
                // new Thread(KeyboardMonitorLoop).Start();
                SetupInputMonitor();
                _running = true;
            }
        }

        /// <summary>
        /// Stops the monitor loop.
        /// </summary>
        public void Stop()
        {
            if (Running)
                _running = false;

            DisposeInputMonitor();
        }

        public delegate void ForegroundWindowChangedEventHandler(object sender, ForegroundWindowChangedEventArgs e);

        public class ForegroundWindowChangedEventArgs : EventArgs
        {
            public IntPtr WindowHandle { get; }
            public string WindowText { get; }
            public Process WindowProcess { get; }

            public ForegroundWindowChangedEventArgs(IntPtr handle)
            {
                WindowHandle = handle;
                WindowText = InteropServices.GetWindowTitle(handle);

                _ = InteropServices.GetWindowThreadProcessId(handle, out uint procId);
                WindowProcess = Process.GetProcessById((int)procId);
            }
        }

        public delegate void ForegroundWindowBoundsChangedEventHandler(object sender, ForegroundWindowBoundsChangedEventArgs e);

        public class ForegroundWindowBoundsChangedEventArgs : EventArgs
        {
            public Rectangle WindowBounds { get; }
            public Padding Delta { get; }

            public ForegroundWindowBoundsChangedEventArgs(Rectangle bounds, Rectangle lastBounds)
            {
                Delta = new(
                    lastBounds.Left - bounds.Left, lastBounds.Top - bounds.Top,
                    lastBounds.Right - bounds.Right, lastBounds.Bottom - bounds.Bottom
                );
                WindowBounds = bounds;
            }
        }

        public delegate void KeyEventHandler(object sender, KeyEventArgs e);

        public class KeyEventArgs : EventArgs
        {
            KeyStates States { get; }
            public Key Key { get; }
            public uint Timestamp { get; }

            public KeyEventArgs(Key Key, KeyStates states, uint timestamp)
            {
                this.Key = Key;
                States = states;
                Timestamp = timestamp;
            }
        }

        public delegate void MaskedKeyEventHandler(object sender, MaskedKeyEventArgs e);

        public class MaskedKeyEventArgs : EventArgs
        {
            KeyStates States { get; }
            public MaskedKey Key { get; }
            public uint Timestamp { get; }

            public MaskedKeyEventArgs(MaskedKey Key, KeyStates states, uint timestamp)
            {
                this.Key = Key;
                States = states;
                Timestamp = timestamp;
            }

            public MaskedKeyEventArgs(Key Key, KeyStates states, uint timestamp)
            {
                this.Key = ObscureKey(Key);
                States = states;
                Timestamp = timestamp;
            }

            /// <summary>
            /// Returns the corresponding <see cref="MaskedKey"/> for the specified <paramref name="Key"/>.
            /// </summary>
            /// <param name="Key">The <see cref="System.Windows.Input.Key"/> to obscure</param>
            /// <remarks>This conversion is exhaustive with <see cref="MaskedKey.Alphanumeric"/> being the catch-all.</remarks>
            public static MaskedKey ObscureKey(Key Key)
            {
                return Key switch
                {
                    Key.None => MaskedKey.None,

                    Key.LeftCtrl or Key.RightCtrl or 
                    Key.LeftAlt or Key.RightAlt or 
                    Key.LeftShift or Key.RightShift or 
                    Key.CapsLock => MaskedKey.Modifier,

                    Key.Escape or Key.Enter or Key.LWin or Key.RWin or
                    Key.PrintScreen or Key.Scroll or Key.Pause or Key.Apps or 
                    Key.Sleep or Key.System or Key.LineFeed => MaskedKey.System,

                    Key.Up or Key.Right or Key.Down or Key.Left or
                    Key.PageUp or Key.PageDown or Key.Home or Key.End or
                    Key.Insert or Key.Delete or Key.Back or Key.Tab or
                    Key.Next or Key.Prior => MaskedKey.Cursor,

                    Key.NumLock or Key.NumPad0 or Key.NumPad1 or Key.NumPad2 or
                    Key.NumPad3 or Key.NumPad4 or Key.NumPad5 or Key.NumPad6 or
                    Key.NumPad7 or Key.NumPad8 or Key.NumPad9 => MaskedKey.NumPad,

                    Key.Play or Key.MediaPlayPause or Key.MediaNextTrack or
                    Key.MediaPreviousTrack or Key.MediaStop or Key.SelectMedia or
                    Key.VolumeDown or Key.VolumeMute or Key.VolumeUp => MaskedKey.Media,

                    Key.F1 or Key.F2 or Key.F3 or Key.F4 or Key.F5 or Key.F6 or 
                    Key.F7 or Key.F8 or Key.F9 or Key.F10 or Key.F11 or Key.F12 or 
                    Key.F13 or Key.F14 or Key.F15 or Key.F16 or Key.F17 or 
                    Key.F18 or Key.F19 or Key.F20 or Key.F21 or Key.F22 or 
                    Key.F23 or Key.F24 => MaskedKey.Function,

                    Key.FinalMode or Key.HangulMode or Key.HanjaMode or 
                    Key.ImeAccept or Key.ImeConvert or Key.ImeModeChange or 
                    Key.ImeNonConvert or Key.ImeProcessed or Key.JunjaMode or 
                    Key.KanaMode or Key.KanjiMode or Key.DbeEnterImeConfigureMode => MaskedKey.Ime,

                    Key.OemSemicolon or Key.Oem1 or Key.OemPlus or 
                    Key.OemComma or Key.OemMinus or Key.OemPeriod or 
                    Key.Oem2 or Key.OemQuestion or Key.Oem3 or 
                    Key.OemTilde or Key.Oem4 or Key.OemOpenBrackets or 
                    Key.OemPipe or Key.Oem5 or Key.OemCloseBrackets or 
                    Key.Oem6 or Key.OemQuotes or Key.Oem7 or Key.Oem8 or 
                    Key.Oem102 or Key.OemBackslash or Key.OemClear => MaskedKey.Oem,

                    Key.BrowserBack or Key.BrowserForward or 
                    Key.BrowserRefresh or Key.BrowserStop or 
                    Key.BrowserSearch or Key.BrowserFavorites or 
                    Key.BrowserHome => MaskedKey.Browser,

                    _ => MaskedKey.Alphanumeric
                };
            }
        }

        public delegate void MouseButtonEventHandler(object sender, MouseButtonEventArgs e);

        public class MouseButtonEventArgs : EventArgs
        {
            public MouseButton Button { get; }
            public MouseButtonState State { get; }
            public uint Timestamp { get; set; }

            public MouseButtonEventArgs(MouseButton mouseButton, MouseButtonState state, uint timestamp)
            {
                Button = mouseButton;
                State = state;
                Timestamp = timestamp;
            }
        }

        public static Rectangle GetMaxScreenBounds()
        {
            int? minLeft = null;
            int? minTop = null;
            int? maxRight = null;
            int? maxBottom = null;

            foreach (var screen in System.Windows.Forms.Screen.AllScreens)
            {
                if (minLeft == null || screen.Bounds.Left < minLeft) minLeft = screen.Bounds.Left;
                if (minTop == null || screen.Bounds.Top < minTop) minTop = screen.Bounds.Top;
                if (maxRight == null || screen.Bounds.Right > maxRight) maxRight = screen.Bounds.Right;
                if (maxBottom == null || screen.Bounds.Bottom > maxBottom) maxBottom = screen.Bounds.Bottom;
            }

            return new(
                minLeft.Value, minTop.Value,
                maxRight.Value - minLeft.Value, maxBottom.Value - minTop.Value
            );
        }
    }

}
