using ShortcutFloat.Common.Models.Triggers;
using System.Collections.Generic;
using System.Drawing;

namespace ShortcutFloat.Common.Models
{
    public class ShortcutConfiguration
    {
        /// <summary>
        /// Specifies the target of this configuration.
        /// </summary>
        public ShortcutTarget Target { get; set; } = new();

        /// <summary>
        /// Specifies the shortcut configurations to be displayed in the float window.
        /// </summary>
        public List<ShortcutDefinition> ShortcutDefinitions { get; set; } = new();

        /// <summary>
        /// Specifies if this configuration is enabled or not, i.e. whether the float window will be displayed if the target application is in the foreground.
        /// </summary>
        public bool Enabled { get; set; } = true;

        /// <summary>
        /// Specifies the offset from the top-left corner of the target applications window.
        /// </summary>
        public PointF FloatWindowAbsoluteOffset { get; set; } = Point.Empty;

        /// <summary>
        /// Specifies the offset from the center of the target applications window.
        /// </summary>
        public PointF FloatWindowRelativeOffset { get; set; } = Point.Empty;

        /// <summary>
        /// Specifies the number of columns in the <see cref="System.Windows.Controls.Primitives.UniformGrid"/> of the float window in which the buttons are arranged.
        /// </summary>
        /// <remarks>This property is nullable and must be set from the global configuration if <see langword="null"/>.</remarks>
        public int? FloatWindowGridColumns { get; set; } = null;

        /// <summary>
        /// Specifies the number of rows in the <see cref="System.Windows.Controls.Primitives.UniformGrid"/> of the float window in which the buttons are arranged.
        /// </summary>
        /// <remarks>This property is nullable and must be set from the global configuration if <see langword="null"/>.</remarks>
        public int? FloatWindowGridRows { get; set; } = null;

        /// <summary>
        /// Specifies whether the float window follows the position and size of the target applications window
        /// </summary>
        /// <remarks>This property is nullable and must be set from the global configuration if <see langword="null"/>.</remarks>
        public bool? StickyFloatWindow { get; set; } = null;

        /// <summary>
        /// Specifies the mode of the offset from the target applications window.
        /// </summary>
        public FloatWindowPositionReference? FloatWindowPositionReference { get; set; } = null;
    }
}
