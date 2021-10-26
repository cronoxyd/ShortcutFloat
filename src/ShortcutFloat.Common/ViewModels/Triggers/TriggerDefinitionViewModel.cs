using ShortcutFloat.Common.Models.Triggers;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShortcutFloat.Common.ViewModels.Triggers
{
    public class TriggerDefinitionViewModel : TypedViewModel<TriggerDefinition>, ITriggerDefinitionViewModel
    {
        public TriggerDefinitionViewModel([NotNull] TriggerDefinition Model) : base(Model) { }

        public string Name { get => Model.Name; set => Model.Name = value; }
    }

    public interface ITriggerDefinitionViewModel : ITypedViewModel<TriggerDefinition>
    {
        public string Name { get; set; }
    }
}
