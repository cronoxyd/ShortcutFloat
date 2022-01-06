using ShortcutFloat.Common.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShortcutFloat.Common.Models.Actions
{
    public abstract class ActionDefinition : IActionDefinition
    {
        public string Name { get; set; }
    }

    public interface IActionDefinition
    {
        public string Name { get; set; }
    }
}
