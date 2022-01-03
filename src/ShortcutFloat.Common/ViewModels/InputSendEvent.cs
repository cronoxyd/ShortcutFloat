using ShortcutFloat.Common.Input;
using ShortcutFloat.Common.Models.Actions;
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
        public InputItem InputItem { get; set; }

        public InputSendEventArgs(InputItem inputItem)
        {
            InputItem = inputItem;
        }
    }
}
