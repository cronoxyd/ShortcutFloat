using System.Collections.Generic;

namespace ShortcutFloat.Common.Models
{
    public class ShortcutFloatSettings
    {
        public bool UseDefaultConfiguration { get; set; } = false;
        public ShortcutConfiguration DefaultConfiguration { get; set; } = new();
        public List<ShortcutConfiguration> ShortcutConfigurations { get; set; } = new();
    }
}
