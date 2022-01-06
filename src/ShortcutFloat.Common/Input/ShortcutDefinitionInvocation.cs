using ShortcutFloat.Common.Models; 
using ShortcutFloat.Common.Models.Actions;
using ShortcutFloat.Common.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace ShortcutFloat.Common.Input
{
    public class ShortcutDefinitionInvocation : ShortcutDefinition
    {
        /// <summary>
        /// Specifies the time at which the shortcut started to be held.
        /// </summary>
        public DateTime? HoldStart { get; set; } = null;

        /// <summary>
        /// Specifies a callback function that is called after the held shortcut has been released.
        /// </summary>
        /// <remarks>
        /// This will only get called if <see cref="ShortcutDefinition.HoldAndRelease"/> is set to <see langword="true"/>.
        /// </remarks>
        public Action HoldReleaseCallback { get; set; } = () => { };

        /// <summary>
        /// Indicates whether the specified <see cref="HoldTimeLimitSeconds"/> has been reached, 
        /// based on <see cref="HoldStart"/>.
        /// </summary>
        /// <remarks>
        /// This will always return false if <see cref="HoldStart"/> is <see langword="null"/> or if
        /// <see cref="ShortcutDefinition.HoldTimeLimitSeconds"/> is set to <c>0</c>.
        /// </remarks>
        public bool TimedOut
        {
            get
            {
                if (HoldStart == null || HoldTimeLimitSeconds <= 0) return false;
                return (DateTime.Now - HoldStart)?.TotalSeconds > HoldTimeLimitSeconds;
            }
        }

        public void StartTimeout()
        {
            if (HoldStart != null)
                throw new InvalidOperationException("Timeout has already been started");

            HoldStart = DateTime.Now;
        }

        public ShortcutDefinitionInvocation(ShortcutDefinition shortcutDefinition)
        {
            Name = shortcutDefinition.Name;
            Actions.AddRange(shortcutDefinition.Actions);
            HoldAndRelease = shortcutDefinition.HoldAndRelease;
            HoldTimeLimitSeconds = shortcutDefinition.HoldTimeLimitSeconds;
            ReleaseTriggerType = shortcutDefinition.ReleaseTriggerType;
        }

    }
}
