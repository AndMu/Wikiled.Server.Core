using Moq;
using NUnit.Framework;
using System;
using Wikiled.Server.Core.Data;

namespace Wikiled.Server.Core.Tests.Data
{
    [TestFixture]
    public class ApiOkResponseTests
    {
        [Test]
        public void Construct()
        {
            var result = new ApiOkResponse("Test");
            Assert.AreEqual("Test", result.Result);
            Assert.AreEqual(ResponseType.Ok, result.ResponseType);
            Assert.AreEqual(200, result.Code);
        }
    }
}
