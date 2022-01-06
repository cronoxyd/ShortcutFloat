using ShortcutFloat.Common.Extensions;
using ShortcutFloat.Common.Input;
using ShortcutFloat.Common.Models;
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

        private Queue<ShortcutDefinitionInvocation> ShortcutQueue { get; } = new();

        private List<ShortcutDefinitionInvocation> HoldShortcuts { get; } = new();

        public int QueueIntervalMilliseconds { get; set; } = 50;

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
            foreach (var item in HoldShortcuts.ToList())
            {
                if (item.ReleaseTriggerType.HasFlag(KeystrokeReleaseTriggerType.Mouse))
                {
                    Debug.WriteLine($"Releasing shortcut \"{item.Name}\" (mouse event: {e.Button})");
                    ReleaseShortcutDefinition(item);
                }
            }
        }

        private void EnvironmentMonitor_KeyUp(object sender, EnvironmentMonitor.MaskedKeyEventArgs e)
        {
            // ToList() to avoid concurrency conflicts
            foreach (var item in HoldShortcuts.ToList())
            {
                if (item.ReleaseTriggerType.HasFlag(KeystrokeReleaseTriggerType.Keyboard))
                {
                    Debug.WriteLine($"Releasing shortcut \"{item.Name}\" (keyboard event: {e.Key})");
                    ReleaseShortcutDefinition(item);
                }
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

                if (ShortcutQueue.Count > 0)
                {
                    var item = ShortcutQueue.Dequeue();

                    Debug.WriteLine($"Processing shortcut \"{item.Name}\" (hold: {item.HoldAndRelease})");

                    if (!item.HoldAndRelease)
                        SendKeys.SendWait(item.GetSendKeysString());
                    else
                    {
                        ReleaseAllHeld();
                        item.StartTimeout();
                        PressShortcutDefinition(item);
                    }
                }

                // ToList() to avoid concurrency issues
                foreach (var item in HoldShortcuts.ToList())
                {
                    if (!item.TimedOut)
                        PressShortcutDefinition(item, false); // Repeatedly press held keys (typematic)
                    else
                    {
                        Debug.WriteLine($"Releasing shortcut \"{item.Name}\" (timeout)");
                        ReleaseShortcutDefinition(item); // or remove the item if it timed out
                    }
                }

                Thread.Sleep(QueueIntervalMilliseconds);
                // synthesizerTuner.LoopEnd();
            }
        }

        private void PressShortcutDefinition(ShortcutDefinitionInvocation item, bool addToHoldList = true)
        {
            ShortcutDefinitionItemKeyboardEvent(item, KeyEventFlag.KEYEVENTF_NONE);

            if (addToHoldList)
                HoldShortcuts.Add(item);
        }

        private void ReleaseShortcutDefinition(ShortcutDefinitionInvocation item, bool removeFromHoldList = true)
        {
            ShortcutDefinitionItemKeyboardEvent(item, KeyEventFlag.KEYEVENTF_KEYUP);

            if (removeFromHoldList)
                HoldShortcuts.Remove(item);

            item.HoldReleaseCallback();
        }

        private void ShortcutDefinitionItemKeyboardEvent(ShortcutDefinition item, KeyEventFlag dwFlags)
        {
            // Debug.WriteLine($"Sending keyboard event ({dwFlags})");

            foreach (var key in item.GetKeys())
            {
                environmentMonitor.KeyboardEventIgnoreCount++;
                InteropServices.keybd_event(key.ToVirtualKeyCode().Value, dwFlags);
            }
        }

        public void EnqueueShortcut(ShortcutDefinitionInvocation shortcutDefinition)
        {
            ShortcutQueue.Enqueue(shortcutDefinition);
        }

        public void Reset()
        {
            ClearQueue();
            ReleaseAllHeld();
            // Debug.WriteLine($"Reset {nameof(InputSynthesizer)}");
        }

        public void ClearQueue()
        {
            ShortcutQueue.Clear();
        }

        public void ReleaseAllHeld()
        {
            // ToList() to avoid concurrency conflicts
            foreach (var item in HoldShortcuts.ToList())
                ReleaseShortcutDefinition(item);
        }
    }
}
