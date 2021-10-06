using System.Collections.Generic;
using System.Drawing;

namespace ShortcutFloat.Common.Models
{
    public class ShortcutConfiguration
    {
        public ShortcutTarget Target { get; set; } = new();
        public List<ShortcutDefinition> ShortcutDefinitions { get; set; } = new();
        public bool Enabled { get; set; } = true;
        public PointF FloatWindowAbsoluteOffset { get; set; } = Point.Empty;
        public PointF FloatWindowRelativeOffset { get; set; } = Point.Empty;
        public int? FloatWindowGridColumns { get; set; } = null;
        public int? FloatWindowGridRows { get; set; } = null;
        public bool? StickyFloatWindow { get; set; } = null;
        public FloatWindowPositionReference? FloatWindowPositionReference { get; set; } = null;
    }
}
