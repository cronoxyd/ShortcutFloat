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
        public ModifierKeys? ModifierKeys { get => Model.ModifierKeys; set => Model.ModifierKeys = value; }
        public bool ModifierCtrl
        {
            get => ModifierKeys.Value.HasFlag(System.Windows.Input.ModifierKeys.Control);
            set
            {
                if (value)
                    ModifierKeys |= System.Windows.Input.ModifierKeys.Control;
                else
                    ModifierKeys &= ~System.Windows.Input.ModifierKeys.Control;
            }
        }
        public bool ModifierShift
        {
            get => ModifierKeys.Value.HasFlag(System.Windows.Input.ModifierKeys.Shift);
            set
            {
                if (value)
                    ModifierKeys |= System.Windows.Input.ModifierKeys.Shift;
                else
                    ModifierKeys &= ~System.Windows.Input.ModifierKeys.Shift;
            }
        }
        public bool ModifierAlt
        {
            get => ModifierKeys.Value.HasFlag(System.Windows.Input.ModifierKeys.Alt);
            set
            {
                if (value)
                    ModifierKeys |= System.Windows.Input.ModifierKeys.Alt;
                else
                    ModifierKeys &= ~System.Windows.Input.ModifierKeys.Alt;
            }
        }
        public bool ModifierWindows
        {
            get => ModifierKeys.Value.HasFlag(System.Windows.Input.ModifierKeys.Windows);
            set
            {
                if (value)
                    ModifierKeys |= System.Windows.Input.ModifierKeys.Windows;
                else
                    ModifierKeys &= ~System.Windows.Input.ModifierKeys.Windows;
            }
        }
        public Key? Key { get => Model.Key; set => Model.Key = value; }

        public KeystrokeDefinitionViewModel([NotNull] KeystrokeDefinition Model) : base(Model) { }
    }

    public interface IKeystrokeDefinitionViewModel : IActionDefinitionViewModel
    {
        public ModifierKeys? ModifierKeys { get; set; }
        public Key? Key { get; set; }
    }
}
