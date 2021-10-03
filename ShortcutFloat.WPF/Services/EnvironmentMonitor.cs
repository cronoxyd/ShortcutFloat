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

namespace ShortcutFloat.WPF.Services
{
    public class EnvironmentMonitor
    {
        private bool _running = false;
        public bool Running => _running;
        public string ForegroundWindowText { get; private set; } = null;
        public IntPtr? ForegroundWindowHandle { get; private set; } = null;
        public Rectangle? ForegroundWindowBounds { get; private set; } = null;
        public Process ForegroundWindowProcess { get; private set; } = null;
        private static Process CurrentProcess { get; } = Process.GetCurrentProcess();

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
                InteropServices.GetWindowThreadProcessId(currentForegroundWindowHandle, out uint currentForegroundWindowProcessId);
                if (currentForegroundWindowProcessId == CurrentProcess.Id)
                    continue;

                ForegroundWindowHandle = currentForegroundWindowHandle;
                ForegroundWindowText = InteropServices.GetActiveWindowTitle();
                InteropServices.GetWindowThreadProcessId(ForegroundWindowHandle.Value, out uint procId);
                ForegroundWindowProcess = Process.GetProcessById((int)procId);

                InteropServices.GetWindowRect(ForegroundWindowHandle.Value, out RECT foregroundWindowRect);
                ForegroundWindowBounds = foregroundWindowRect.ToRectangle();

                if (
                    (ForegroundWindowText != lastForegroundWindowText && lastForegroundWindowText != null) ||
                    (ForegroundWindowHandle != lastForegroundWindowHandle && lastForegroundWindowHandle != null)
                )
                    ForegroundWindowChanged(this, new(ForegroundWindowHandle.Value));

                if (lastWindowRects.ContainsKey(ForegroundWindowHandle.Value) && !foregroundWindowRect.Equals(lastWindowRects[ForegroundWindowHandle.Value]))
                    ForegroundWindowBoundsChanged(this, new(foregroundWindowRect.ToRectangle(), lastWindowRects[ForegroundWindowHandle.Value].ToRectangle()));

                lastForegroundWindowHandle = ForegroundWindowHandle;
                lastForegroundWindowText = ForegroundWindowText;
                lastWindowRects[ForegroundWindowHandle.Value] = foregroundWindowRect;

                Thread.Sleep(10);
            }
        }

        public void Start()
        {
            if (!Running)
            {
                new Thread(MonitorLoop).Start();
                _running = true;
            }
        }

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

                InteropServices.GetWindowThreadProcessId(handle, out uint procId);
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
    }
}
