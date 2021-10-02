using System.Collections.Generic;
using System.Drawing;

namespace ShortcutFloat.Common.Models
{
    public class ShortcutConfiguration
    {
        public ShortcutTarget Target { get; set; } = new();
        public List<ShortcutDefinition> ShortcutDefinitions { get; set; } = new();
        public PointF? FloatWindowLocation { get; set; } = null;
        public bool Enabled { get; set; } = true;
    }
}
