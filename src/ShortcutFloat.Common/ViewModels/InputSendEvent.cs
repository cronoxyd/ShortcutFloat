using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace ShortcutFloat.Common.ViewModels
{

    public delegate void InputSendEventHandler(object sender, InputSendEventArgs e);

    public class InputSendEventArgs : EventArgs
    {
        public Key[] Keys { get; set; }
        public MouseButton[] MouseButtons { get; set; }
        public bool HoldAndRelease { get; set; } = false;
        public uint? HoldAndReleaseTimeout { get; set; } = 0;
    }
}
