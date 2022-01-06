using ShortcutFloat.Common.Helper;
using ShortcutFloat.Common.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShortcutFloat.Common.Models.Actions
{
    public class TextblockDefintion : ActionDefinition, ITextblockDefinition
    {
        public string Content { get; set; } = string.Empty;

        public TextblockDefintion() { }

        public TextblockDefintion(string Name, string Content)
        {
            this.Name = Name;
            this.Content = Content;
        }
    }

    public interface ITextblockDefinition : IActionDefinition
    {
        public string Content { get; set; }
    }
}
