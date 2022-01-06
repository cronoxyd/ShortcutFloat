using ShortcutFloat.Common.Models;
using ShortcutFloat.Common.Models.Actions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace ShortcutFloat.Common.Extensions
{
    public static class ShortcutDefinitionExtensions
    {
        public static string GetSendKeysString(this ShortcutDefinition shortcutDefinition) =>
            string.Join(string.Empty,
                shortcutDefinition.Actions.Select(action =>
                    action switch
                    {
                        KeystrokeDefinition kd => kd.GetSendKeysString(),
                        TextblockDefintion td => td.GetSendKeysString(),
                        _ => throw new NotImplementedException()
                    }
                )
            );

        public static Key[] GetKeys(this ShortcutDefinition shortcutDefinition) =>
            shortcutDefinition.Actions.OfType<KeystrokeDefinition>().SelectMany(kd =>
            {
                var retVal = new List<Key>();
                retVal.AddRange(kd.ModifierKeys.ToKeys());
                if (kd.Key != null) retVal.Add(kd.Key.Value);
                return retVal;
            }).ToArray();
    }
}
