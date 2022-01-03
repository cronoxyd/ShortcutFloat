using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShortcutFloat.Common.Input
{
    public class InputHoldItem
    {
        public InputItem Item { get; }

        public DateTime KeyDownTime { get; }

        public bool TimedOut
        {
            get
            {
                if (Item.HoldTimeLimitSeconds <= 0) return false;
                return (DateTime.Now - KeyDownTime).TotalSeconds > Item.HoldTimeLimitSeconds;
            }
        }

        public InputHoldItem(InputItem item)
        {
            Item = item;
            KeyDownTime = DateTime.Now;
        }
    }
}
