using System.Collections.Generic;
using System.Drawing;

namespace ShortcutFloat.Common.Models
{
    public class ShortcutConfiguration
    {
        public ShortcutTarget Target { get; set; } = new();
        public List<ShortcutDefinition> ShortcutDefinitions { get; set; } = new();
        public PointF FloatWindowAbsoluteOffset { get; set; } = Point.Empty;
        public PointF FloatWindowRelativeOffset { get; set; } = Point.Empty;
        public bool Enabled { get; set; } = true;
        public bool? StickyFloatWindow { get; set; } = null;
        public FloatWindowPositionReference? FloatWindowPositionReference { get; set; } = null;
    }
}
