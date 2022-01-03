using System.Collections.Generic;
using ShortcutFloat.Common.Models.Actions;
using System.Windows.Controls.Primitives;

namespace ShortcutFloat.Common.Models
{
    public class ShortcutFloatSettings
    {
        /// <summary>
        /// Specifies whether the <see cref="DefaultConfiguration"/> should be used if no specific configuration applies to the current foreground window.
        /// </summary>
        public bool UseDefaultConfiguration { get; set; } = false;

        /// <summary>
        /// Specifies whether the float window sticks to the position of the targeted window.
        /// </summary>
        /// <remarks>
        /// This is a fallback value and can be overridden in <see cref="ShortcutConfiguration.StickyFloatWindow"/>.
        /// </remarks>
        public bool StickyFloatWindow { get; set; } = false;

        /// <summary>
        /// Specifies the positioning scheme of the float window.
        /// </summary>
        /// <remarks>
        /// This only applies if <see cref="StickyFloatWindow"/> is set to <see langword="true"/>.<br/>
        /// This is a fallback value and can be overridden in <see cref="ShortcutConfiguration.FloatWindowPositionReference"/>.
        /// </remarks>
        public FloatWindowPositionReference FloatWindowPositionReference { get; set; } = FloatWindowPositionReference.Absolute;

        /// <summary>
        /// Specifies the default configuration that will be used if no configuration applies to the current foreground window.
        /// </summary>
        /// <remarks>
        /// This only applies if <see cref="UseDefaultConfiguration"/> is set to <see langword="true"/>.
        /// </remarks>
        public ShortcutConfiguration DefaultConfiguration { get; set; } = new();

        /// <summary>
        /// The <see cref="ShortcutConfiguration"/>s that apply to specific process names or window titles.
        /// </summary>
        public List<ShortcutConfiguration> ShortcutConfigurations { get; set; } = new();

        /// <inheritdoc cref="UniformGrid.Columns"/>
        /// <remarks>
        /// This is a fallback value and can be overridden in <see cref="ShortcutConfiguration.FloatWindowGridColumns"/>.
        /// </remarks>
        public int FloatWindowGridColumns { get; set; } = 0;

        /// <inheritdoc cref="UniformGrid.Rows"/>
        /// <remarks>
        /// This is a fallback value and can be overridden in <see cref="ShortcutConfiguration.FloatWindowGridRows"/>.
        /// </remarks>
        public int FloatWindowGridRows { get; set; } = 0;

        /// <summary>
        /// Specifies the time limit for shortcut keys that are held.
        /// </summary>
        /// <remarks>
        /// This is a fallback value and can be overridden in <see cref="KeystrokeDefinition.HoldTimeLimitSeconds"/>.
        /// </remarks>
        public int KeyHoldTimeLimitSeconds { get; set; } = 5;
    }
}
