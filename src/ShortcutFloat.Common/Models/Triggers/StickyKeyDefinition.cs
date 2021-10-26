using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace ShortcutFloat.Common.Models.Triggers
{
    public class StickyKeyDefinition : TriggerDefinition
    {
        public Key? Key { get; set; } = null;

        public StickyKeyDefinition() { }

        public StickyKeyDefinition(string Name, Key Key)
        {
            this.Name = Name;
            this.Key = Key;
        }
    }
}
