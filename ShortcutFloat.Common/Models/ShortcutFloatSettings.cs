using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShortcutFloat.Common.Models
{
    public class ShortcutFloatSettings
    {
        public bool UseDefaultConfiguration { get; set; } = false;
        public ShortcutConfiguration DefaultConfiguration { get; set; } = new();
        public List<ShortcutConfiguration> ShortcutConfigurations { get; set; } = new();
    }
}
