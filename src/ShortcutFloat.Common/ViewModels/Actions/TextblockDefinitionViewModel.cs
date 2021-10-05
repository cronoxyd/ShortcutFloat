using ShortcutFloat.Common.Models.Actions;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShortcutFloat.Common.ViewModels.Actions
{
    public class TextblockDefinitionViewModel : ActionDefinitionViewModel, IActionDefinitionViewModel
    {
        public new TextblockDefintion Model { get => base.Model as TextblockDefintion; set => base.Model = value; }
        public string Content { get => Model.Content; set => Model.Content = value; }

        public TextblockDefinitionViewModel([NotNull] TextblockDefintion Model) : base(Model) { }
    }

    public interface ITextblockDefinitionViewModel : IActionDefinitionViewModel
    {
        public string Content { get; set; }
    }
}
