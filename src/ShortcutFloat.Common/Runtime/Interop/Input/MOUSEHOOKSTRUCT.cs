using ShortcutFloat.Common.Runtime.Interop.Drawing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace ShortcutFloat.Common.Runtime.Interop.Input
{
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

}
