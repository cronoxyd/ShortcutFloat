using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShortcutFloat.Common.Helper
{
    public class SendKeysHelper
    {
        public static string EscapeForSendKeys(string str)
        {
            string retVal = string.Empty;
            foreach (char c in str)
                retVal += EscapeForSendKeys(c);
            return retVal;
        }

        public static string EscapeForSendKeys(char chr)
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
