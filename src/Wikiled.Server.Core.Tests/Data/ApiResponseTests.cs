using NUnit.Framework;
using System;
using Wikiled.Server.Core.Data;

namespace Wikiled.Server.Core.Tests.Data
{
    [TestFixture]
    public class ApiResponseTests
    {
        [TestCase(200, "", ResponseType.Ok)]
        [TestCase(404, "Resource not found", ResponseType.Error)]
        [TestCase(500, "An unhandled error occurred", ResponseType.Error)]
        [TestCase(501, "Unknown Error", ResponseType.Error)]
        public void Construct(int code, string result, ResponseType type)
        {
            var response = new ApiResponse(code);
            Assert.AreEqual(result, response.Status);
            Assert.AreEqual(code, response.Code);
            Assert.AreEqual(type, response.ResponseType);
        }

        [Test]
        public void ConstructCustom()
        {
            var response = new ApiResponse(200, "Test");
            Assert.AreEqual("Test", response.Status);
            Assert.AreEqual(200, response.Code);
        }
    }
}