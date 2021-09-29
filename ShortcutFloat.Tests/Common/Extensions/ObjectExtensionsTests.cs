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
    public class ObjectExtensionsTests
    {
        [TestMethod]
        public void TestDeepClone()
        {
            TestClass1 Reference = new() { FieldMember = "ghi", TestString = ".NET" };
            TestClass1 Difference = Reference.DeepClone();

            Difference.StringMember = "jkl";
            Difference.IntegerMember = 456;
            Difference.ReferenceMember.StringMember = "mno";
            Difference.ReferenceMember.DoubleMember = 7.89;

            Assert.AreNotEqual(Difference.StringMember, Reference.StringMember);
            Assert.AreNotEqual(Difference.IntegerMember, Reference.IntegerMember);
            Assert.AreNotEqual(Difference.ReferenceMember.StringMember, Reference.ReferenceMember.StringMember);
            Assert.AreNotEqual(Difference.ReferenceMember.DoubleMember, Reference.ReferenceMember.DoubleMember);
            Assert.AreEqual(Reference.TestString, Difference.TestString);
        }

        [Serializable]
        internal class TestClass1
        {
            public string TestString { get; set; } = string.Empty;
            public string StringMember { get; set; } = "abc";
            public int IntegerMember { get; set; } = 123;
            public TestClass2 ReferenceMember { get; set; } = new();
            public string FieldMember = "";
        }

        [Serializable]
        internal class TestClass2
        {
            public double DoubleMember { get; set; } = 1.23;
            public string StringMember { get; set; } = "def";
        }
    }
}
