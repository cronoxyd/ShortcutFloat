using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShortcutFloat.Common.Models
{
    public class ShortcutConfiguration
    {
        public ShortcutTarget Target { get; set; } = new();
        public List<ShortcutDefinition> ShortcutDefinitions { get; set; } = new();
    }
}
