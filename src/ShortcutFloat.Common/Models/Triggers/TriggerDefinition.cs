using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShortcutFloat.Common.Models.Triggers
{
    public abstract class TriggerDefinition : ITriggerDefinition
    {
        public string Name { get; set; }
    }

    public interface ITriggerDefinition
    {
        public string Name { get; set; }
    }
}
