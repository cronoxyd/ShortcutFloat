using Microsoft.VisualStudio.TestTools.UnitTesting;
using ShortcutFloat.Common.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShortcutFloat.Tests.Common.Extensions
{
    [TestClass]
    public class CollectionExtensionsTests
    {
        [TestMethod]
        public void TestReplace()
        {
            var testList = new List<string>()
            {
                "One",
                "Two",
                "Three",
                "Four",
                "Five",
                "Six"
            };

            testList.Replace("Four", "Kaboom");
            Assert.AreEqual("Kaboom", testList[3]);

            Assert.ThrowsException<ArgumentException>(() => testList.Replace("Nope", "Hell"));
        }
    }
}
