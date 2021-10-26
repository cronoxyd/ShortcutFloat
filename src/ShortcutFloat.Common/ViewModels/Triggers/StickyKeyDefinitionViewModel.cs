using ShortcutFloat.Common.Models.Triggers;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace ShortcutFloat.Common.ViewModels.Triggers
{
    public class StickyKeyDefinitionViewModel : TriggerDefinitionViewModel
    {
        public new StickyKeyDefinition Model { get => base.Model as StickyKeyDefinition; set => base.Model = value; }
        public Key? Key { get => Model.Key; set => Model.Key = value; }

        public StickyKeyDefinitionViewModel([NotNull] StickyKeyDefinition Model) : base(Model) { }
    }
}
