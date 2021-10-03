using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShortcutFloat.Common.Models.Actions
{
    public class TextblockDefintion : ActionDefinition
    {
        public string Content { get; set; } = string.Empty;

        public TextblockDefintion() { }

        public TextblockDefintion(string Name, string Content)
        {
            this.Name = Name;
            this.Content = Content;
        }

        public override string GetSendKeysString()
        {
            var retVal = string.Empty;
            for (int i = 0; i < Content.Length; i++)
                retVal += EscapeForSendKeys(Content[i]);

            return retVal;
        }

        private string EscapeForSendKeys(char chr)
        {
            string EncloseChars = "+^%~()[]";
            if (EncloseChars.Contains(chr))
                return $"{{{chr}}}";
            else if (chr == '{')
                return "{{}";
            else if (chr == '}')
                return "{}}";
            else
                return chr.ToString();
        }
    }
}
