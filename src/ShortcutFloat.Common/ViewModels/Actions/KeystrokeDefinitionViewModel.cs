using ShortcutFloat.Common.Extensions;
using ShortcutFloat.Common.Models.Actions;
using System.Diagnostics.CodeAnalysis;
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

        public KeystrokeDefinitionViewModel([NotNull] KeystrokeDefinition Model) : base(Model) { }
    }

    public interface IKeystrokeDefinitionViewModel : IKeystrokeDefinition
    { }
}
