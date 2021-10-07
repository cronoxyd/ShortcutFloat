using System.Linq;
using System.Windows.Input;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ShortcutFloat.Common.Extensions;

namespace ShortcutFloat.Tests.Common.Extensions
{
    [TestClass]
    public class KeyExtensionsTests
    {
        public void TestKey(Key testKey)
        {
            var sendKeysString = testKey.ToSendKeysString();

            switch (testKey)
            {
                case Key.LeftCtrl:
                case Key.RightCtrl:
                {
                    Assert.AreEqual("^", sendKeysString);
                    break;
                }

                case Key.LeftAlt:
                case Key.RightAlt:
                {                    
                    Assert.AreEqual("%", sendKeysString);
                    break;
                }

                case Key.LeftShift:
                case Key.RightShift:
                {                    
                    Assert.AreEqual("+", sendKeysString);
                    break;
                }

                default:
                {
                    Assert.AreEqual(testKey.ToString(), sendKeysString);
                    break;
                }
            }
        }

        [TestMethod]
        public void TestModifierKeys() 
        {
            Assert.AreEqual(ModifierKeys.Control.ToSendKeysString(), Key.LeftCtrl.ToSendKeysString());
            Assert.AreEqual(ModifierKeys.Alt.ToSendKeysString(), Key.LeftAlt.ToSendKeysString());
            Assert.AreEqual(ModifierKeys.Shift.ToSendKeysString(), Key.LeftShift.ToSendKeysString());
            Assert.AreEqual(ModifierKeys.Windows.ToSendKeysString(), Key.LWin.ToSendKeysString());

            var testCombo1 = ModifierKeys.Control | ModifierKeys.Shift;
            var testComboSendKeys1 = testCombo1.ToSendKeysString();
            Assert.AreEqual(2, testComboSendKeys1.Length);
            Assert.IsTrue(testComboSendKeys1.Contains(Key.LeftCtrl.ToSendKeysString()));
            Assert.IsTrue(testComboSendKeys1.Contains(Key.LeftShift.ToSendKeysString()));
        }
    }
}