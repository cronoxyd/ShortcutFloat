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
        public Key? Key { get => Model.Key; set => Model.Key = value; }

        public KeystrokeDefinitionViewModel([NotNull] KeystrokeDefinition Model) : base(Model) { }
    }

    public interface IKeystrokeDefinitionViewModel : IActionDefinitionViewModel
    {
        public ModifierKeys? ModifierKeys { get; set; }
        public Key? Key { get; set; }
    }
}
