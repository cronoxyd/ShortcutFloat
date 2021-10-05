using ShortcutFloat.Common.Runtime;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

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

        public bool IgnoreOutOfBounds { get; set; } = true;

        public int OutOfBoundsTolerance { get; set; } = 2000;

        public Rectangle MaxScreenBounds { get; } = GetMaxScreenBounds();

        public event ForegroundWindowChangedEventHandler ForegroundWindowChanged = (sender, e) => { };
        public event ForegroundWindowBoundsChangedEventHandler ForegroundWindowBoundsChanged = (sender, e) => { };

        private void MonitorLoop()
        {
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

                Thread.Sleep(10);
            }
        }

        /// <summary>
        /// Starts the monitor loop.
        /// </summary>
        public void Start()
        {
            if (!Running)
            {
                new Thread(MonitorLoop).Start();
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
