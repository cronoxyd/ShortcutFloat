using ShortcutFloat.Common.Models.Actions;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace ShortcutFloat.Common.ViewModels.Actions
{
    public class KeystrokeDefinitionViewModel : ActionDefinitionViewModel
    {
        public new KeystrokeDefinition Model { get => base.Model as KeystrokeDefinition; set => base.Model = value; }

        public ModifierKeys ModifierKeys { get => Model.ModifierKeys; set => Model.ModifierKeys = value; }

        public bool ModifierCtrl
        {
            get => ModifierKeys.HasFlag(ModifierKeys.Control);
            set
            {
                if (value)
                    ModifierKeys |= ModifierKeys.Control;
                else
                    ModifierKeys &= ~ModifierKeys.Control;
            }
        }

        public bool ModifierShift
        {
            get => ModifierKeys.HasFlag(ModifierKeys.Shift);
            set
            {
                if (value)
                    ModifierKeys |= ModifierKeys.Shift;
                else
                    ModifierKeys &= ~ModifierKeys.Shift;
            }
        }

        public bool ModifierAlt
        {
            get => ModifierKeys.HasFlag(ModifierKeys.Alt);
            set
            {
                if (value)
                    ModifierKeys |= ModifierKeys.Alt;
                else
                    ModifierKeys &= ~ModifierKeys.Alt;
            }
        }

        public bool ModifierWindows
        {
            get => ModifierKeys.HasFlag(ModifierKeys.Windows);
            set
            {
                if (value)
                    ModifierKeys |= ModifierKeys.Windows;
                else
                    ModifierKeys &= ~ModifierKeys.Windows;
            }
        }

        public Key? Key { get => Model.Key; set => Model.Key = value; }

        public bool HoldAndRelease { get => Model.HoldAndRelease; set => Model.HoldAndRelease = value; }
        public int? HoldTimeLimitSeconds { get => Model.HoldTimeLimitSeconds; set => Model.HoldTimeLimitSeconds = value; }

        public KeystrokeReleaseTriggerType ReleaseTriggerType { get => Model.ReleaseTriggerType; set => Model.ReleaseTriggerType = value; }

        public bool ReleaseTriggerTypeMouse
        {
            get => ReleaseTriggerType.HasFlag(KeystrokeReleaseTriggerType.Mouse);
            set
            {
                if (value)
                    ReleaseTriggerType |= KeystrokeReleaseTriggerType.Mouse;
                else
                    ReleaseTriggerType &= ~KeystrokeReleaseTriggerType.Mouse;
            }
        }

        public bool ReleaseTriggerTypeKeyboard
        {
            get => ReleaseTriggerType.HasFlag(KeystrokeReleaseTriggerType.Keyboard);
            set
            {
                if (value)
                    ReleaseTriggerType |= KeystrokeReleaseTriggerType.Keyboard;
                else
                    ReleaseTriggerType &= ~KeystrokeReleaseTriggerType.Keyboard;
            }
        }

        public KeystrokeDefinitionViewModel([NotNull] KeystrokeDefinition Model) : base(Model) { }
    }

    public interface IKeystrokeDefinitionViewModel : IActionDefinitionViewModel
    {
        public ModifierKeys? ModifierKeys { get; set; }
        public Key? Key { get; set; }
        public uint HoldTimeLimitMilliseconds { get; set; }
        public KeystrokeReleaseTriggerType ReleaseTriggerType { get; set; }
        public bool ReleaseTriggerTypeMouse { get; set; }
        public bool ReleaseTriggerTypeKeyboard { get; set; }
    }
}
