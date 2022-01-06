using ShortcutFloat.Common.Models;
using System;

namespace ShortcutFloat.Common.ViewModels
{

    public delegate void InvokeShortcutEventHandler(object sender, InvokeShortcutEventArgs e);

    public class InvokeShortcutEventArgs : EventArgs
    {
        public ShortcutDefinition ShortcutDefinition { get; set; }

        public Action HoldReleaseCallback { get; set; } = () => { };

        public InvokeShortcutEventArgs(ShortcutDefinition shortcutDefinition)
        {
            ShortcutDefinition = shortcutDefinition;
        }
    }
}
