using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShortcutFloat.Common.Extensions
{
    public static class PointFExtensions
    {
        public static PointF Add(this PointF a, PointF b)
        {
            return new(a.X + b.X, a.Y + b.Y);
        }
        public static PointF Add(this PointF a, SizeF b)
        {
            return new(a.X + b.Width, a.Y + b.Height);
        }

        public static PointF Subtract(this PointF a, PointF b)
        {
            return new(a.X - b.X, a.Y - b.Y);
        }

        public static PointF Subtract(this PointF a, SizeF b)
        {
            return new(a.X - b.Width, a.Y - b.Height);
        }
    }
}
