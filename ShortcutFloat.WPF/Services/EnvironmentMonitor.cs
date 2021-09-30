using ShortcutFloat.Common.Runtime;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ShortcutFloat.WPF.Services
{
    public class EnvironmentMonitor
    {
        private bool _running = false;
        public bool Running => _running;

        public event ForegroundWindowChangedEventHandler ForegroundWindowChanged = (sender, e) => { };

        private void MonitorLoop()
        {
            IntPtr? lastForegroundWindowHandle = null;
            string lastForegroundWindowText = null;

            while (Running)
            {
                var foregroundWindowHandle = InteropServices.GetForegroundWindow();
                var foregroundWindowText = InteropServices.GetActiveWindowTitle();

                if (
                    (foregroundWindowText != lastForegroundWindowText && lastForegroundWindowText != null) ||
                    (foregroundWindowHandle != lastForegroundWindowHandle && lastForegroundWindowHandle != null)
                   )
                    ForegroundWindowChanged(this, new(foregroundWindowHandle));

                lastForegroundWindowHandle = foregroundWindowHandle;
                lastForegroundWindowText = foregroundWindowText;

                Thread.Sleep(100);
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
                WindowProcess = Process.GetProcessById(procId);
            }
        }
    }
}
