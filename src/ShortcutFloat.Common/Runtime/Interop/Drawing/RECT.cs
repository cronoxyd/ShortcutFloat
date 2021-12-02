using System.Drawing;
using System.Runtime.InteropServices;

namespace ShortcutFloat.Common.Runtime.Interop.Drawing
{
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
}
