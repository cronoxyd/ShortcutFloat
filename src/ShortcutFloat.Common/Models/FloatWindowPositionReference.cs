using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShortcutFloat.Common.Models
{
    public enum FloatWindowPositionReference
    {
        /// <summary>
        /// Positions the float window using an absolute offset from the top-left corner of the target applications window.
        /// </summary>
        Absolute,

        /// <summary>
        /// Positions the float window using a absolute offset from the center of the target applications window.
        /// </summary>
        Relative
    }
}
