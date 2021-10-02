using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShortcutFloat.Common.ViewModels
{

    public delegate void SendKeysEventHandler(object sender, SendKeysEventArgs e);

    public class SendKeysEventArgs : EventArgs
    {
        public string SendKeysString { get; set; }

        public SendKeysEventArgs(string SendKeysString) =>
            this.SendKeysString = SendKeysString;
    }
}
