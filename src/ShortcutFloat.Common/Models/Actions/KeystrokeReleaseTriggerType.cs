using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShortcutFloat.Common.Models.Actions
{
    [Flags]
    public enum KeystrokeReleaseTriggerType
    {
        None,
        Mouse,
        Keyboard
    }
}
