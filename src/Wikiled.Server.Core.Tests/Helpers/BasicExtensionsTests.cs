using NUnit.Framework;
using Wikiled.Server.Core.Helpers;

namespace Wikiled.Server.Core.Tests.Helpers
{
    [TestFixture]
    public class BasicExtensionsTests
    {
        [TestCase("One,Two", new[] { "One", "Two" })]
        [TestCase("One ,Two", new[] { "One", "Two" })]
        [TestCase("One", new[] { "One" })]
        [TestCase(null, new string[] { })]
        public void SplitCsv(string csv, string[] expected)
        {
            var result = BasicExtensions.SplitCsv(csv);
            Assert.AreEqual(expected, result);
        }
    }
}
