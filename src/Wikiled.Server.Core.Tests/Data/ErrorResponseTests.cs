using Moq;
using NUnit.Framework;
using System;
using Wikiled.Server.Core.Data;

namespace Wikiled.Server.Core.Tests.Data
{
    [TestFixture]
    public class ErrorResponseTests
    {
        [Test]
        public void Construct()
        {
            var result = new ErrorResponse("Test");
            Assert.AreEqual("Test", result.Status);
            Assert.AreEqual(ResponseType.Error, result.ResponseType);
            Assert.AreEqual(400, result.Code);
        }
    }
}
