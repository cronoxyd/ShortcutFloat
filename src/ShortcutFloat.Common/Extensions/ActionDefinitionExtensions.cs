using ShortcutFloat.Common.Helper;
using ShortcutFloat.Common.Models.Actions;
using ShortcutFloat.Common.ViewModels.Actions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShortcutFloat.Common.Extensions
{
    public static class ActionDefinitionExtensions
    {
        public static string GetSendKeysString(this KeystrokeDefinition keystrokeDefinition)
        {
            return string.Join(string.Empty, new string[]
            {
                keystrokeDefinition.ModifierKeys.ToSendKeysString(),
                keystrokeDefinition.Key.ToSendKeysString()
            });
        }

        public static string GetSendKeysString(this KeystrokeDefinitionViewModel keystrokeDefinition) =>
            keystrokeDefinition.Model.GetSendKeysString();


        public static string GetSendKeysString(this TextblockDefintion textblockDefintion) => 
            SendKeysHelper.EscapeForSendKeys(textblockDefintion.Content);

        public static string GetSendKeysString(this TextblockDefinitionViewModel textblockDefintion) =>
            textblockDefintion.Model.GetSendKeysString();
    }
}
