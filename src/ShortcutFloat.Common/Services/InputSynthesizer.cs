using ShortcutFloat.Common.Diagnostics;
using ShortcutFloat.Common.Input;
using ShortcutFloat.Common.Models.Actions;
using ShortcutFloat.Common.Runtime.Interop;
using ShortcutFloat.Common.Runtime.Interop.Input;
using System;
using System.Collections.Generic;
using System.Diagnostics;
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

        private Queue<InputItem> InputQueue { get; } = new();

        private List<InputHoldItem> HoldItems { get; } = new();

        public int QueueIntervalMilliseconds { get; set; } = 10;

        public bool Running => _running;

        //private LoopIntervalTuner synthesizerTuner { get; }

        private EnvironmentMonitor environmentMonitor { get; }

        public InputSynthesizer(EnvironmentMonitor environmentMonitor)
        {
            this.environmentMonitor = environmentMonitor;
            environmentMonitor.KeyUp += EnvironmentMonitor_KeyUp;
            environmentMonitor.MouseUp += EnvironmentMonitor_MouseUp;

            //synthesizerTuner = new(nameof(InputSynthesizer), QueueIntervalMilliseconds);
            //synthesizerTuner.IntervalAdjust += SynthesizerTuner_IntervalAdjust;
        }

        //private void SynthesizerTuner_IntervalAdjust(object sender, IntervalAdjustEventArgs e)
        //{
        //    QueueIntervalMilliseconds = (int)e.RecommendedLoopInterval;
        //    synthesizerTuner.ExpectedLoopInterval = QueueIntervalMilliseconds;
        //}

        private void EnvironmentMonitor_MouseUp(object sender, EnvironmentMonitor.MouseButtonEventArgs e)
        {
            // ToList() to avoid concurrency conflicts
            foreach (var item in HoldItems.ToList())
            {
                if (item.Item.ReleaseTriggerType.HasFlag(Models.Actions.KeystrokeReleaseTriggerType.Mouse))
                    ReleaseInputItem(item);
            }
        }

        private void EnvironmentMonitor_KeyUp(object sender, EnvironmentMonitor.MaskedKeyEventArgs e)
        {
            // ToList() to avoid concurrency conflicts
            foreach (var item in HoldItems.ToList())
            {
                if (item.Item.ReleaseTriggerType.HasFlag(Models.Actions.KeystrokeReleaseTriggerType.Keyboard))
                    ReleaseInputItem(item);
            }
        }

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
                // synthesizerTuner.LoopStart();

                if (InputQueue.Count > 0)
                {
                    var item = InputQueue.Dequeue();

                    Debug.WriteLine($"Processing {nameof(InputItem)} (hold: {item.HoldAndRelease})");

                    if (!item.HoldAndRelease)
                        SendKeys.SendWait(item.GetSendKeysString());
                    else
                    {
                        ReleaseAllHeld();
                        PressInputItem(item);
                    }
                }

                // ToList() to avoid concurrency issues
                foreach (var item in HoldItems.ToList())
                {
                    if (!item.TimedOut)
                        PressInputItem(item.Item, false); // Repeatedly press held keys (typematic)
                    else
                        ReleaseInputItem(item); // or remove the item if it timed out
                }

                Thread.Sleep(QueueIntervalMilliseconds);
                // synthesizerTuner.LoopEnd();
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

            Debug.WriteLine($"Sending keyboard event ({dwFlags})");

            // SetEnvironmentInputIgnore(true, item.ReleaseTriggerType);

            foreach (var key in item.Keys)
                InteropServices.keybd_event(key.ToVirtualKeyCode().Value,
                    dwFlags);

            foreach (var mouseButton in item.MouseButtons)
                InteropServices.keybd_event(mouseButton.ToVirtualKeyCode().Value,
                    dwFlags);

            // SetEnvironmentInputIgnore(false);
        }

        private void SetEnvironmentInputIgnore(bool enable, KeystrokeReleaseTriggerType releaseType = KeystrokeReleaseTriggerType.None)
        {
            environmentMonitor.IgnoreKeyboardEvents = enable && releaseType.HasFlag(KeystrokeReleaseTriggerType.Keyboard);
            environmentMonitor.IgnoreMouseEvents = enable && releaseType.HasFlag(KeystrokeReleaseTriggerType.Mouse);
        }

        public void EnqueueInputItem(InputItem item)
        {
            InputQueue.Enqueue(item);
        }

        public void Reset()
        {
            ClearQueue();
            ReleaseAllHeld();
            Debug.WriteLine($"Reset {nameof(InputSynthesizer)}");
        }

        public void ClearQueue()
        {
            InputQueue.Clear();
        }

        public void ReleaseAllHeld()
        {
            // ToList() to avoid concurrency conflicts
            foreach (var item in HoldItems.ToList())
                ReleaseInputItem(item);
        }
    }
}
