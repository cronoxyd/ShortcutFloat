using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShortcutFloat.Common.Runtime.Interop.Input
{
    [Flags]
    public enum MSLLHOOKSTRUCTFlags : uint
    {
        LLMHF_INJECTED = 0x01,
        LLMHF_LOWER_IL_INJECTED = 0x02
    }
}
