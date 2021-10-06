using System.Collections.Generic;

namespace ShortcutFloat.Common.Models
{
    public class ShortcutFloatSettings
    {
        public bool UseDefaultConfiguration { get; set; } = false;
        public bool StickyFloatWindow { get; set; } = false;
        public FloatWindowPositionReference FloatWindowPositionReference { get; set; } = FloatWindowPositionReference.Absolute;
        public ShortcutConfiguration DefaultConfiguration { get; set; } = new();
        public List<ShortcutConfiguration> ShortcutConfigurations { get; set; } = new();
        public int FloatWindowGridColumns { get; set; } = 0;
        public int FloatWindowGridRows { get; set; } = 0;
    }
}
