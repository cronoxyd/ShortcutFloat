using ShortcutFloat.Common.Input;
using ShortcutFloat.Common.Runtime.Interop;
using ShortcutFloat.Common.Runtime.Interop.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ShortcutFloat.Common.Services
{
    public class InputSynthesizer
    {
        private bool _running = false;

        public Queue<InputItem> InputQueue { get; } = new();

        private List<InputHoldItem> HoldItems { get; } = new();

        public int QueueIntervalMilliseconds { get; set; } = 10;

        public bool Running => _running;

        public void Start()
        {
            if (Running) return;
            new Thread(SynthesizerLoop).Start();
            _running = true;
        }

        public void Stop()
        {
            if (!Running) return;
            _running = false;
        }

        private void SynthesizerLoop()
        {
            if (QueueIntervalMilliseconds <= 0)
                throw new InvalidOperationException($"{nameof(QueueIntervalMilliseconds)} must be a positive integer.");

            while (Running)
            {
                if (InputQueue.Count > 0)
                {
                    var item = InputQueue.Dequeue();

                    if (!item.HoldAndRelease)
                    {
                        SendKeys.SendWait(item.GetSendKeysString());
                    }
                    else
                    {
                        PressInputItem(item);
                    }
                }

                foreach (var item in HoldItems.Where(item => item.TimedOut))
                {
                    ReleaseInputItem(item);
                }

                Thread.Sleep(QueueIntervalMilliseconds);
            }
        }

        private void PressInputItem(InputItem item, bool addToHoldList = true)
        {
            InputItemKeyboardEvent(item, KeyEventFlag.KEYEVENTF_NONE);

            if (addToHoldList)
                HoldItems.Add(new(item));
        }

        private void ReleaseInputItem(InputItem item)
        {
            InputItemKeyboardEvent(item, KeyEventFlag.KEYEVENTF_KEYUP);
        }

        private void ReleaseInputItem(InputHoldItem item, bool removeFromHoldList = true)
        {
            InputItemKeyboardEvent(item.Item, KeyEventFlag.KEYEVENTF_KEYUP);

            if (removeFromHoldList)
                HoldItems.Remove(item);
        }

        private void InputItemKeyboardEvent(InputItem item, KeyEventFlag dwFlags)
        {
            if (item.Text.Length > 0)
                throw new NotSupportedException("Cannot synthesize keyboard events for a text.");

            foreach (var key in item.Keys)
                InteropServices.keybd_event(key.ToVirtualKeyCode().Value,
                    dwFlags);

            foreach (var mouseButton in item.MouseButtons)
                InteropServices.keybd_event(mouseButton.ToVirtualKeyCode().Value,
                    dwFlags);
        }
    }
}
